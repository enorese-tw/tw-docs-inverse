# Documentación de Consultas: `SQLServerDBHelper.cs`

## Contexto
Esta clase **no contiene consultas SQL de negocio**.

## Función
Actúa como **motor de ejecución**. Es la clase encargada de:
1.  Abrir la conexión a la base de datos adecuada (según el parámetro `CLIENT`).
2.  Ejecutar las consultas (`ExecuteQuery`) o procedimientos almacenados (`ExecuteStoreProcedure`) definidos en las otras clases (`CFiniquitos`, `CPagos`, etc.).
3.  Retornar los resultados en formato `DataTable`.

## Observaciones de Seguridad
*   Contiene el método `ExecuteQuery` que acepta consultas en formato string y parámetros string.
*   **Vulnerabilidad**: Al usar `string.Format` para construir la consulta final, es susceptible a **SQL Injection** si los inputs no están sanitizados previamente.
