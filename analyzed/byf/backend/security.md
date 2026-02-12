# Análisis de Seguridad y Ciberseguridad: Backend BYF

Este documento detalla el análisis de seguridad realizado sobre el código fuente del proyecto **Backend BYF**. Se han identificado vulnerabilidades, vectores de ataque potenciales y se proponen medidas de mitigación.

## 1. Resumen Ejecutivo

La aplicación presenta una arquitectura backend típica basada en Express.js y SQL Server. Si bien existen buenas prácticas implementadas como el uso de consultas parametrizadas para evitar SQL Injection directo en valores de usuario, existen riesgos significativos relacionados con dependencias desactualizadas, manejo de errores verboso que filtra información y falta de controles de seguridad en capas HTTP (cabeceras, rate limiting).

## 2. Vulnerabilidades Identificadas

### 2.1. Dependencias Desactualizadas (Software Composition Analysis)
Se identificaron librerías críticas con versiones antiguas que podrían contener vulnerabilidades conocidas (CVEs).

*   **`jsonwebtoken`**: Versión `^8.5.1`. La versión actual es 9.x. Versiones antiguas pueden tener vulnerabilidades en la verificación de firmas.
*   **`express`**: Versión `^4.18.1`. Aunque es estable, se recomienda mantener la última versión de la rama 4.x o migrar a 5.x para parches de seguridad.
*   **`mssql`**: Versión `^8.1.1`. La versión actual es 10.x. Actualizarse garantiza soporte para nuevos protocolos de seguridad TLS y parches del driver.

**Riesgo**: Medio/Alto.
**Mitigación**: Ejecutar `npm audit` y actualizar las dependencias a sus versiones estables más recientes (`npm update`).

### 2.2. Autenticación y JWT
*   **Algoritmia Implícita**: En `src/middlewares/jwt.js`, `jwt.verify(token, config.secret)` no fuerza un algoritmo específico.
    *   *Ataque Potencial*: "Algorithm Confusion" o ataques de degradación a "none" si la librería lo permite (aunque versiones 8.x de `jsonwebtoken` mitigan "none", es mejor ser explícito).
*   **Fuga de Información en Errores**: El middleware devuelve `error.message` directamente al cliente en caso de falla (500). Esto puede revelar detalles de la estructura interna o librerías usadas.
*   **Gestión de Secretos**: El secreto (`SECRET`) se lee de variables de entorno, lo cual es correcto, pero debe asegurarse una rotación periódica y complejidad suficiente.

**Mitigación**:
*   Especificar el algoritmo en verify: `jwt.verify(token, config.secret, { algorithms: ['HS256'] })`.
*   Estandarizar mensajes de error genéricos para el cliente (e.g., "Autenticación fallida") y loguear el error real internamente.

### 2.3. Manejo de Errores y Fuga de Información
En múltiples controladores (e.g., `auth.controller.js`, `finiquitos.controller.js`), los bloques `catch` devuelven el mensaje de error "crudo":
```javascript
res.status(500).json({ message: error.message });
```
**Riesgo**: Medio. Permite a un atacante realizar reconocimiento del sistema (Fingerprinting) o entender por qué falla una inyección.
**Mitigación**: Implementar un middleware de manejo de errores centralizado que oculte los detalles técnicos en producción.

### 2.4. Seguridad en Capa HTTP
*   **Rate Limiting Ausente**: No se evidencia uso de librerías como `express-rate-limit`.
    *   *Ataque Potencial*: Fuerza bruta sobre endpoints de login (`/auth/signin`) o Denegación de Servicio (DoS) por agotamiento de recursos.
*   **Cabeceras de Seguridad (Helmet) Ausentes**: No se utiliza `helmet`.
    *   *Riesgo*: Exposición a ataques XSS, Clickjacking, y divulgación de tecnología (header `X-Powered-By: Express`).
*   **CORS Hardcod**: La configuración de CORS en `app.js` tiene una lista blanca explícita, lo cual es bueno, pero si se necesita escalar, debería ser configurable.

**Mitigación**: Instalar `helmet` y `express-rate-limit`. Configurar `helmet` al inicio de la aplicación.

### 2.5. Validación de Datos y Filtrado
En `finiquitos.controller.js`, el filtrado se realiza en memoria (JavaScript) sobre el set de resultados completo:
```javascript
bajas = result.recordset.filter((item) => ...)
```
**Riesgo**: Si la consulta SQL devuelve miles de registros, procesarlos en memoria bloquea el Event Loop de Node.js, causando una Denegación de Servicio (DoS) fácil de provocar.
**Mitigación**: Mover la lógica de filtrado (`WHERE ... LIKE ...`) a la consulta SQL/Stored Procedure para aprovechar el motor de base de datos y paginar desde la fuente.

### 2.6. Inyección SQL (Evaluación)
*   **Puntos Fuertes**: Se utiliza `pool.request().input()` consistentemente para pasar parámetros de usuario. Esto mitiga la Inyección SQL clásica de primer orden.
*   **Punto de Atención**: En `src/database/scripts/queriesBajas.js` (y otros), se inyectan nombres de base de datos mediante Template Strings:
    ```javascript
    FROM [${config.serverSoftland}].[${config.dbSoftlandTWEST}]...
    ```
    Si bien `config` viene de variables de entorno (confiables), si en algún momento estas variables se alimentaran de input externo, sería una vulnerabilidad crítica de Inyección SQL.
**Recomendación**: Mantener estricto control sobre quién puede modificar el archivo `.env` o las variables de entorno del servidor.

## 3. Matriz de Riesgos y Prioridades

| Vulnerabilidad | Impacto | Probabilidad | Prioridad |
| :--- | :--- | :--- | :--- |
| Dependencias Desactualizadas | Alto | Media | **Alta** |
| Fuga de Info en Errores | Medio | Alta | **Alta** |
| Ausencia de Rate Limiting | Alto (DoS) | Media | **Media** |
| Filtrado en Memoria (DoS) | Alto | Baja (depende del volumen) | **Media** |
| Cabeceras de Seguridad Faltantes | Medio | Alta | **Baja** |

## 4. Plan de Acción Recomendado

1.  **Inmediato**: Ejecutar actualización de dependencias críticas (`npm update`).
2.  **Corto Plazo**:
    *   Implementar `helmet` en `src/api/app.js`.
    *   Refactorizar el manejo de errores en controladores para no exponer `error.message`.
    *   Configurar `verify()` de JWT con algoritmo explícito.
3.  **Mediano Plazo**:
    *   Implementar `express-rate-limit` especialmente en rutas `/auth`.
    *   Refactorizar endpoints de listados grandes para realizar el filtrado y paginación a nivel de base de datos (SQL), no en memoria.

## 5. Conclusión
El proyecto tiene una base de seguridad aceptable en cuanto a la protección de consultas SQL, pero carece de defensas en profundidad (capas HTTP, gestión de errores segura). La actualización de librerías y la implementación de cabeceras de seguridad son "quick wins" que elevarán significativamente el perfil de seguridad de la aplicación.
