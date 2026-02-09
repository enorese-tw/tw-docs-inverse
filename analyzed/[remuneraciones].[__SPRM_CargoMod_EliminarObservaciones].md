# [remuneraciones].[__SPRM_CargoMod_EliminarObservaciones]

## Objetivo
Este procedimiento almacenado elimina una observación específica del campo `Observaciones` de una solicitud de modificación de cargo. Gestiona la eliminación del texto tanto si está aislado como si está precedido por un salto de línea HTML.

## Parámetros de Entrada
| Nombre | Tipo | Descripción |
| :--- | :--- | :--- |
| `@USUARIOCREADOR` | `VARCHAR(MAX)` | Identificador del usuario (no utilizado en la lógica del procedimiento). |
| `@CODIGOSOLICITUD` | `VARCHAR(MAX)` | Identificador de la solicitud (codificado en Base64). |
| `@OBSERVACIONES` | `VARCHAR(MAX)` | La cadena de texto de la observación que se desea eliminar. |

## Parámetros de Salida
| Nombre | Tipo | Descripción |
| :--- | :--- | :--- |
| `@CODE` | `VARCHAR(MAX)` | Código de respuesta ('200' éxito, '500' error). |
| `@MESSAGE` | `VARCHAR(MAX)` | Mensaje de respuesta. |

## Variables Internas
No se utilizan variables internas.

## Llamadas Internas
- **Funciones**:
    - `[TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE]`: Para obtener el identificador decodificado de la solicitud.

## Lógica de Cálculo
1.  **Actualización**: Ejecuta un `UPDATE` sobre el campo `Observaciones` de la tabla `RM_CargosMod`.
    - Utiliza la función `REPLACE` de forma anidada para limpiar el texto:
        - Primero intenta eliminar la cadena `' <br/> ' + @OBSERVACIONES`, cubriendo casos donde la observación fue agregada con un salto de línea previo.
        - Luego intenta eliminar la cadena `@OBSERVACIONES` directamente, para casos donde aparece sola o sin el prefijo anterior.
2.  **Manejo de Errores**: Retorna código '500' con el mensaje de error del sistema si ocurre una excepción.

## Tablas Afectadas
- `[remuneraciones].[RM_CargosMod]`: UPDATE (Campo `Observaciones`).

## Código Comentado
No se observa código comentado en este archivo.
