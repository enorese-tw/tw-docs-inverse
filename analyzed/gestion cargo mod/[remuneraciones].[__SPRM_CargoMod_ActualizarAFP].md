# [remuneraciones].[__SPRM_CargoMod_ActualizarAFP]

## Objetivo
Este procedimiento almacenado permite actualizar la AFP asociada a una solicitud de modificación de cargo (`RM_CargosMod`). Además, si la solicitud se basó en un sueldo líquido (`TypeSueldoInput = 'SL'`), recalcula el sueldo base necesario para mantener ese líquido con la nueva AFP seleccionada.

## Parámetros de Entrada
| Nombre | Tipo | Descripción |
| :--- | :--- | :--- |
| `@CODIGOSOLICITUD` | `VARCHAR(MAX)` | Identificador de la solicitud de modificación de cargo (codificado en Base64). |
| `@AFP` | `VARCHAR(MAX)` | Nuevo código o nombre de la AFP a asignar. |

## Parámetros de Salida
| Nombre | Tipo | Descripción |
| :--- | :--- | :--- |
| `@CODE` | `VARCHAR(MAX)` | Código de respuesta de la operación (ej. '200' para éxito). |
| `@MESSAGE` | `VARCHAR(MAX)` | Mensaje descriptivo del resultado. |

## Variables Internas
| Nombre | Tipo | Descripción |
| :--- | :--- | :--- |
| `@NEWSUELDO` | `FLOAT` | Almacena el nuevo sueldo base calculado si aplica recálculo. |
| `@MESSAGEOUT` | `VARCHAR(MAX)` | Mensaje retornado por el procedimiento de cálculo interno. |
| `@TYPESUELDO` | `VARCHAR(MAX)` | Tipo de entrada de sueldo ('SB': Sueldo Base, 'SL': Sueldo Líquido). |
| `@SUELDO` | `FLOAT` | Monto del sueldo auxiliar (líquido) para realizar el cálculo inverso. |

## Llamadas Internas
- **Funciones**:
    - `[TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE]`: Decodifica el parámetro `@CODIGOSOLICITUD`.
- **Procedimientos Almacenados**:
    - `[remuneraciones].[__SPRMINT_CargoMod_SueldoFromLiquido]`: Se invoca si el tipo de sueldo es 'SL' para recalcular el sueldo base a partir del líquido deseado y las nuevas condiciones (AFP).

## Lógica de Cálculo
1.  **Actualización Inicial**: Actualiza el campo `AFP` en la tabla `[remuneraciones].[RM_CargosMod]` para el registro correspondiente a la solicitud decodificada.
2.  **Verificación de Tipo de Sueldo**: Consulta el tipo de sueldo (`TypeSueldoInput`) y el sueldo líquido auxiliar (`AuxSueldoLiquido`) de la solicitud.
3.  **Recálculo Condicional ('SL')**:
    - Si `TypeSueldoInput` es igual a `'SL'` (Sueldo Líquido), ejecuta el procedimiento `__SPRMINT_CargoMod_SueldoFromLiquido` pasando el sueldo líquido objetivo.
    - Captura el nuevo sueldo base calculado en `@NEWSUELDO`.
4.  **Actualización Final**: Actualiza el campo `SueldoBase` en `[remuneraciones].[RM_CargosMod]`:
    - Si el tipo de entrada era 'SB' (Sueldo Base), mantiene el valor original.
    - Si era 'SL', actualiza con el nuevo valor calculado (`@NEWSUELDO`).
5.  **Respuesta**: Establece `@CODE = '200'` y un mensaje de éxito.

## Tablas Afectadas
- `[remuneraciones].[RM_CargosMod]`: UPDATE (Campos `AFP` y potencialmente `SueldoBase`).
