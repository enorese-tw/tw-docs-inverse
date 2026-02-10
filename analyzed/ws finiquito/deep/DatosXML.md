# Documentación Detallada: `DatosXML.cs`

## Visión General
`DatosXML` es una clase de infraestructura crítica encargada de leer la configuración de la aplicación desde un archivo XML externo (`config.xml`). Su función principal es proporcionar credenciales de acceso a bases de datos y parámetros de conexión para los distintos entornos (Interno, Softland EST, Softland OUT, Softland Consultora).

## Dependencias
*   `System.Xml`: Para manipulación de documentos XML.
*   `System.AppDomain`: Para localizar la ruta de ejecución.

## Archivo de Configuración
*   **Nombre de Archivo**: `config.xml`
*   **Ubicación**: Esperado en el directorio `bin` de la aplicación web (`AppDomain.CurrentDomain.BaseDirectory + "bin"`).
*   **Nodo Raíz**: `<tw>`

## Análisis de Estructura XML Esperada
Basado en las rutas XPath utilizadas en el código (`BuscaNodos`):

### Conexión a Base de Datos Interna (SQL Server SA)
*   **Usuario**: Atributo `usuario` en `/tw/sqlserver/nombre`.
*   **Clave SA**: Valor del nodo `/tw/sqlserver/nombre/passwordsa`.
*   **Servidor SA**: Valor del nodo `/tw/sqlserver/nombre/servidorsa`.
*   **Base de Datos**: Valor del nodo `/tw/sqlserver/nombre/basedatos`.

### Conexión a Softland (ERP)
*   **Servidor**: Valor del nodo `/tw/softland/nombre/servidor`.
    *   *Nota*: Incluye lógica de sanitización para convertir `\\` en `\`.
*   **Usuario**: Atributo `usuario` en `/tw/softland/nombre`.
*   **Clave**: Valor del nodo `/tw/softland/nombre/password`.
*   **Base de Datos EST**: `/tw/softland/nombre/basededatosest`.
*   **Base de Datos OUT**: `/tw/softland/nombre/basededatosout`.
*   **Base de Datos CONSULTORA**: `/tw/softland/nombre/basededatosconsultora`.

## Vulnerabilidades y Riesgos
1.  **Credenciales en Texto Plano**: Aunque no se ve el XML, la estructura sugiere que las contraseñas se almacenan en texto plano o con una codificación reversible simple en el archivo `config.xml`.
2.  **Manejo de Errores Deficiente**:
    *   El método `RutaArchivoWeb` tiene un bloque `catch` que retorna cadena vacía y *luego* lanza una excepción: `throw new Exception(ex.Message);` después de un `return` (código inalcanzable).
    *   Si el XML no tiene el formato esperado, los métodos podrían fallar silenciosamente o lanzar excepciones no controladas al acceder a índices (`ChildNodes[0]`) sin validación previa.

## Observaciones de Negocio
*   La clase confirma la existencia de al menos 4 bases de datos o esquemas principales con los que interactúa el sistema al mismo tiempo, todos configurables externamente.
*   Es un punto único de falla (SPOF): si el XML se corrompe o falta, todo el servicio dejará de funcionar.
