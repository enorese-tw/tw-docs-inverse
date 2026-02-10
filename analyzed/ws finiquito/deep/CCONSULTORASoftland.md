# Documentación Detallada: `CCONSULTORASoftland.cs`

## Visión General
La clase `CCONSULTORASoftland` encapsula la lógica de acceso a datos para la instancia **CONSULTORA** del ERP Softland. Su función principal es recuperar información básica del trabajador (RUT y Nombre) desde esta instancia específica.

## Dependencias
*   `System.Data` (DataTable)
*   `SQLServerDBHelper`: Utilizado para la ejecución de consultas SQL.

## Contexto de Base de Datos
*   **Identificador de Conexión**: `"SOFTLAND_CONSULTORA"`
*   Esta cadena de conexión debe estar configurada en el archivo `config.xml` (según el análisis de `DatosXML.cs`).

## Análisis de Métodos

### `GetRutTrabajadorSolicitudBajaCONSULTORA(string[] parametros)`
Obtiene el RUT y nombre de un trabajador específico desde la base de datos de la consultora.

*   **Parámetros**:
    *   `parametros[0]`: El RUT del trabajador a buscar (formato esperado con puntos y guion, o solo guion, dependiendo de cómo se almacene en Softland, a juzgar por el `LIKE`).
*   **Consulta SQL**:
    ```sql
    SELECT DISTINCT p.rut, p.nombres as nombres 
    FROM softland.sw_personal p 
    INNER JOIN softland.sw_estudiosup e 
    ON e.codEstudios like p.codEstudios 
    WHERE p.rut like '{0}'
    ```
*   **Tablas Involucradas**:
    *   `softland.sw_personal` (`p`): Tabla maestra de personal.
    *   `softland.sw_estudiosup` (`e`): Tabla de estudios superiores (aunque el JOIN parece usarse para filtrar o validar existencia, la relación por `codEstudios` es curiosa para obtener datos básicos).
*   **Retorno**: `DataTable` con columnas `rut` y `nombres`.

## Observaciones de Negocio
*   El uso de `LIKE` en el filtro `WHERE p.rut like '{0}'` implica que el sistema podría buscar por coincidencias parciales, aunque en lógica de negocio estricta se esperaría una igualdad exacta para un RUT.
*   El `DISTINCT` sugiere que un mismo RUT podría aparecer múltiples veces en el cruce de `sw_personal` y `sw_estudiosup`.
