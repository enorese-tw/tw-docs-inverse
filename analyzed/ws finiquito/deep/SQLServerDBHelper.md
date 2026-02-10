# Documentación Detallada: `SQLServerDBHelper.cs`

## Visión General
Esta clase es el componente central de acceso a datos (DAL) del sistema. Encapsula todas las interacciones con Microsoft SQL Server, manejando conexiones, ejecución de comandos (Queries y Stored Procedures) y la recuperación de resultados en objetos `DataTable`.

## Gestión de Conexiones
La clase gestiona dinámicamente la cadena de conexión basada en un identificador de cliente (`CLIENT`) pasado al constructor.

### Contextos Soportados (`CLIENT`)
Las credenciales se recuperan a través de la clase auxiliar `DatosXML`.

1.  **`"TW_SA"`**:
    *   Conecta a la base de datos interna (`BasedeDatos.BaseDeDatos`) en el servidor SA.
    *   Uso: Operaciones nativas del sistema de finiquitos (`CFiniquitos`, `CPagos`).
2.  **`"SOFTLAND_EST"`**:
    *   Conecta a la base de datos Softland EST (`BasedeDatos.SoftlandBaseDeDatosEST`).
    *   Uso: Integración ERP Softland (Estratégica).
3.  **`"SOFTLAND_OUT"`**:
    *   Conecta a la base de datos Softland OUT (`BasedeDatos.SoftlandBaseDeDatosOUT`).
    *   Uso: Integración ERP Softland (Outsourcing).
4.  **`"SOFTLAND_CONSULTORA"`**:
    *   Conecta a la base de datos Softland CONSULTORA (`BasedeDatos.SoftlandBaseDeDatosCONSULTORA`).
    *   Uso: Integración ERP Softland (Consultoría).

## Métodos Principales

### `ExecuteStoreProcedure(string StoreProcedure, List<Parametro> Parameters, string TableName)`
Ejecuta un procedimiento almacenado.
*   **Manejo de Parámetros**: Itera sobre una lista de objetos `Parametro` y los agrega al comando SQL (`da.SelectCommand.Parameters.AddWithValue`).
*   **Retorno**: `DataTable` con los resultados.
*   **Ciclo de Vida**: Utiliza `using(con = new SqlConnection(_conexion))` para asegurar el cierre y disposición de la conexión.

### `ExecuteQuery(string query, string[] parameters, string TableName)`
Ejecuta una consulta SQL en texto plano.
*   **Riesgo de Seguridad Crítico (SQL Injection)**:
    *   La consulta se construye utilizando `string.Format(query, parameters)`.
    *   Si `query` o `parameters` provienen de entrada de usuario no saneada, esto permite la inyección directa de código SQL malicioso.
    *   Este método debería ser refactorizado para usar `SqlParameter` al igual que `ExecuteStoreProcedure`.

## Observaciones Técnicas
*   **Código Comentado**: Existen bloques `finally { //con.Close(); }` comentados, lo que indica refactorizaciones previas hacia el uso de `using`, pero dejando "basura" en el código.
*   **Dependencias Ocultas**: Instancia `new DatosXML()` dentro del constructor, creando un acoplamiento fuerte con la configuración XML.
