# Análisis de Ejecución Local para wsServiceFiniquitos

Este documento detalla los requisitos y configuraciones necesarias para ejecutar el proyecto `wsServiceFiniquitos` en un entorno local, basado en el análisis del código fuente.

## 1. Resumen del Proyecto
*   **Tipo**: Servicio WCF (.NET Framework 4.0).
*   **Punto de Entrada**: `wsServicioFiniquitos.svc`.
*   **Configuración**: Personalizada, basada en lectura de archivo XML (`DatosXML.cs`).

## 2. Configuración Crítica (Archivo Faltante)
El proyecto **no utiliza** `Web.config` para las cadenas de conexión. En su lugar, utiliza una clase `DatosXML.cs` que busca un archivo llamado `config.xml` en el directorio `bin` de la aplicación.

**Este archivo no está presente en el repositorio y debe ser creado manualmente.**

### Ubicación Esperada:
`{DirectorioDelProyecto}\bin\config.xml`

### Estructura XML Requerida (Schema):
Basado en el código de `DatosXML.cs`, el archivo `config.xml` debe tener la siguiente estructura exacta:

```xml
<tw>
  <sqlserver>
    <nombre usuario="USUARIO_SA">
      <passwordsa>CLAVE_SA</passwordsa>
      <servidorsa>SERVIDOR_SA</servidorsa>
      <basedatos>NOMBRE_BD_SA</basedatos>
    </nombre>
  </sqlserver>
  <softland>
    <nombre usuario="USUARIO_SOFTLAND">
      <servidor>SERVIDOR_SOFTLAND</servidor>
      <basededatosout>NOMBRE_BD_OUT</basededatosout>
      <basededatosest>NOMBRE_BD_EST</basededatosest>
      <basededatosconsultora>NOMBRE_BD_CONSULTORA</basededatosconsultora>
      <password>CLAVE_SOFTLAND</password>
    </nombre>
  </softland>
</tw>
```

## 3. Conexiones a Base de Datos
El sistema maneja dos perfiles principales de conexión:

### A. Base de Datos Interna ("TW_SA")
Utilizada para la lógica principal del servicio (Finiquitos, Pagos, Solicitudes).
*   **Clase**: `SQLServerDBHelper` usando caso "TW_SA".
*   **Parámetros XML**:
    *   `usuario`: Usuario de la BD.
    *   `passwordsa`: Clave del usuario.
    *   `servidorsa`: Dirección del servidor SQL (ej. `localhost`, `.\SQLEXPRESS`).
    *   `basedatos`: Nombre de la base de datos principal.

### B. Base de Datos Softland ("SOFTLAND_*")
Utilizada para integraciones con Softland ERP.
*   **Clase**: `SQLServerDBHelper` usando casos "SOFTLAND_EST", "SOFTLAND_OUT", "SOFTLAND_CONSULTORA".
*   **Parámetros XML**:
    *   `usuario`: Usuario de la BD Softland.
    *   `password`: Clave del usuario.
    *   `servidor`: Dirección del servidor Softland (El código maneja automáticamente el reemplazo de `\\` por `\`).
    *   `basededatosest`: Base de datos para perfil EST.
    *   `basededatosout`: Base de datos para perfil OUT.
    *   `basededatosconsultora`: Base de datos para perfil CONSULTORA.

## 4. Dependencias de Servicios Externos
*   No se han encontrado llamadas a APIs REST/SOAP externas en el código analizado (`wsServicioFiniquitos.svc.cs`, `CPagos.cs`, `CFiniquitos.cs`, `CSolB4J.cs`).
*   Toda la interacción externa parece limitarse a las bases de datos SQL Server mencionadas arriba.

## 5. Pasos para Ejecución Local Exitoso
1.  **Compilar la Solución**: Abra `ServicioFiniquitos.sln` en Visual Studio.
2.  **Crear `config.xml`**:
    *   Cree un archivo `config.xml` en la raíz del proyecto web.
    *   Copie la estructura XML provista arriba.
    *   Reemplace los valores con las credenciales de su SQL Server local.
    *   Configure el archivo para que se copie al directorio de salida (`Copy to Output Directory: Copy Always`) o péguelo manualmente en la carpeta `bin`.
3.  **Base de Datos**: Asegúrese de tener restauradas las bases de datos requeridas o apunte a un servidor de desarrollo existente.
4.  **Ejecutar**: Inicie el proyecto en IIS Express. El servicio debería estar disponible en `http://localhost:PUERTO/wsServicioFiniquitos.svc`.
