# Documentación del Controlador de Finanzas (FinanzasController)

## Descripción General
El `FinanzasController` es un controlador de API REST (`ApiController`) diseñado para gestionar operaciones financieras específicas, actualmente enfocado en la administración de **"Valores Diarios"**.

El controlador permite realizar operaciones CRUD (Crear, Leer, Actualizar, Eliminar) sobre estos valores diarios, los cuales parecen estar asociados a una combinación de **Cliente**, **Cargo** (CargoMod) y **Empresa**.

## Comunicación con otros Componentes
El controlador interactúa con los siguientes componentes del sistema para llevar a cabo sus tareas:

1.  **FinanzasRepository**: Componente encargado de la interacción directa con la base de datos. El controlador instancia esta clase para llamar al método `ExecuteStoreProcedure`.
    *   *Nota*: A diferencia del `FiniquitosController` que usaba `CallExecutionAPI`, este controlador utiliza un repositorio específico `FinanzasRepository`.
2.  **PackageValorDiario**: Clase auxiliar (Helper) utilizada para:
    *   Obtener los nombres de los parámetros esperados por el procedimiento almacenado (`PackageValorDiario.Param()`).
    *   Formatear y ordenar los valores de los parámetros según la acción a realizar (`PackageValorDiario.Values(...)`).
3.  **DataPackageValorDiario**: Modelo de datos (`Teamwork.Model.Parametros`) que estructura la información recibida en el cuerpo de las solicitudes HTTP (Body).
4.  **ArrayToListParametros**: Utilidad para convertir los arreglos de parámetros y valores en el formato requerido por el repositorio.

## Procedimientos Almacenados y Queries
El controlador utiliza un **único procedimiento almacenado** para todas las operaciones expuestas, variando su comportamiento mediante un parámetro de acción (texto) que se envía como primer argumento.

*   **Esquema**: `finanzas`
*   **Procedimiento Almacenado**: `package_FZ_ValorDiario`

### Lista de Llamadas y Acciones
A continuación se detallan las acciones específicas invocadas sobre el procedimiento `finanzas.package_FZ_ValorDiario` para cada endpoint:

| Endpoint | Acción (Parámetro SP) | Descripción |
| :--- | :--- | :--- |
| `ValorDiario/Crear` | `"Crear"` | Crea un nuevo registro de valor diario. Utiliza `Cliente`, `CargoMod`, `Empresa` y `Monto`. |
| `ValorDiario/Listar` | `"Listar"` | Lista los valores diarios existentes. Utiliza parámetros de paginación (`Pagination`) y filtros (`TypeFilter`, `DataFilter`). |
| `ValorDiario/Actualizar` | `"Actualizar"` | Actualiza un registro existente. Utiliza `Cliente`, `CargoMod`, `Empresa` y `Monto`. |
| `ValorDiario/Eliminar` | `"Eliminar"` | Elimina lógicamente o físicamente un registro. Utiliza `CargoMod` y `Empresa` como identificadores clave. |
