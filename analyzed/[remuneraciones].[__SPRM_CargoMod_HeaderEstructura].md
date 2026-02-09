# [remuneraciones].[__SPRM_CargoMod_HeaderEstructura]

## Objetivo
Este procedimiento almacenado obtiene la información de cabecera o encabezado principal asociada a una solicitud de modificación de cargo específica. Retorna todos los datos disponibles en la vista de estructura para el código proporcionado.

## Parámetros de Entrada
| Nombre | Tipo | Descripción |
| :--- | :--- | :--- |
| `@CODIGOCARGOMOD` | `VARCHAR(MAX)` | Identificador de la solicitud de modificación de cargo sobre la cual se consulta la cabecera. |

## Variables Internas
No se declaran variables internas adicionales.

## Llamadas Internas (Vistas)
- `[remuneraciones].[View_HeaderEstructura]`: Vista que consolida la información de cabecera de las solicitudes de modificación.

## Lógica de Cálculo
El procedimiento realiza una consulta directa (`SELECT *`) a la vista `[remuneraciones].[View_HeaderEstructura]`, filtrando los registros donde el campo `CodigoCargoMod` coincide con el parámetro de entrada `@CODIGOCARGOMOD`.
La operación está envuelta en un bloque `TRY...CATCH` con manejo de transacciones (`BEGIN/COMMIT TRANSACTION`), aunque es una operación de solo lectura.

## Retorno
Retorna todas las columnas disponibles en la vista `[remuneraciones].[View_HeaderEstructura]` para el registro seleccionado.

## Tablas Afectadas
Operación de solo lectura (SELECT). No modifica datos.
