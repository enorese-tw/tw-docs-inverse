# [remuneraciones].[__SPRM_CargoMod_ActualizaObservaciones]

## Objetivo
Este procedimiento almacenado se encarga de actualizar el campo de observaciones secundarias (`Observaciones2`) de una solicitud de modificación de cargo, registrando además quién realizó la actualización y cuándo.

## Parámetros de Entrada
| Nombre | Tipo | Descripción |
| :--- | :--- | :--- |
| `@USUARIOCREADOR` | `VARCHAR(MAX)` | Identificador del usuario que realiza la actualización. |
| `@CODIGOSOLICITUD` | `VARCHAR(MAX)` | Identificador de la solicitud (codificado en Base64). |
| `@OBSERVACIONES` | `VARCHAR(8000)` | Texto con las observaciones a actualizar. |
| `@TYPE` | `VARCHAR(MAX)` | Parámetro recibido pero **no utilizado** en la lógica actual del procedimiento. |

## Parámetros de Salida
| Nombre | Tipo | Descripción |
| :--- | :--- | :--- |
| `@CODE` | `VARCHAR(MAX)` | Código de respuesta ('200' éxito). En caso de error, el procedimiento retorna un result set con el código '500'. |
| `@MESSAGE` | `VARCHAR(MAX)` | Mensaje de respuesta. |

## Variables Internas
No se declaran variables internas adicionales.

## Llamadas Internas
- **Funciones**:
    - `[dbo].[FNBase64Decode]`: Utilizada para decodificar `@CODIGOSOLICITUD`.
    - `GETDATE()`: Para registrar la fecha y hora actual de la actualización.

## Lógica de Cálculo
1.  **Actualización**: Se ejecuta un `UPDATE` en la tabla `[remuneraciones].[RM_CargosMod]` para el registro correspondiente al ID decodificado.
    - Se actualiza `Observaciones2` con el valor de `@OBSERVACIONES`.
    - Se actualiza `UsuarioUltimaActualizacion` con `@USUARIOCREADOR`.
    - Se actualiza `FechaUltimaActualizacion` con la fecha actual.
    - Se fija `UltimoComentario` en 'Se ha actualizado observaciones.'.
2.  **Retorno Exitoso**: Establece `@CODE` en '200' y `@MESSAGE` en vacío.
3.  **Manejo de Errores**: En caso de fallo (`CATCH`), se realiza un `ROLLBACK` y se selecciona un conjunto de resultados con código '500' y el mensaje de error del sistema.

## Tablas Afectadas
- `[remuneraciones].[RM_CargosMod]`: UPDATE (Campos `Observaciones2`, `UsuarioUltimaActualizacion`, `FechaUltimaActualizacion`, `UltimoComentario`).

## Código Comentado
No se observa código comentado en este archivo.
