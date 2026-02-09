# [remuneraciones].[__SPRM_CargoMod_CambiarCalculoTypeSueldo]

## Objetivo
Este procedimiento almacenado actualiza el campo `TypeSueldo` de una solicitud de modificación de cargo. Este campo define la base de cálculo del sueldo (ej. mensual, diario, por hora) distinta a la entrada de sueldo (Input).

## Parámetros de Entrada
| Nombre | Tipo | Descripción |
| :--- | :--- | :--- |
| `@SUELDO` | `VARCHAR(MAX)` | Código del nuevo tipo de cálculo de sueldo a asignar. |
| `@CODIGOCARGOMOD` | `VARCHAR(MAX)` | Identificador de la solicitud de modificación de cargo (codificado en Base64). |

## Parámetros de Salida
| Nombre | Tipo | Descripción |
| :--- | :--- | :--- |
| `@CODE` | `VARCHAR(MAX)` | Código de respuesta (ej. '200'). |
| `@MESSAGE` | `VARCHAR(MAX)` | Mensaje de resultado (Nota: el mensaje 'Se ha modificado la afp asociada' parece ser un texto heredado erróneo, debería referirse al tipo de sueldo). |

## Variables Internas
No utiliza variables internas locales para cálculo complejo.

## Llamadas Internas
- **Funciones**:
    - `[TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE]`: Decodifica el parámetro `@CODIGOCARGOMOD`.

## Lógica de Cálculo
1.  **Actualización Directa**: Ejecuta un `UPDATE` en la tabla `[remuneraciones].[RM_CargosMod]`, asignando el valor de `@SUELDO` al campo `TypeSueldo` para el registro identificado.
2.  **Respuesta**: Devuelve código '200' y mensaje de éxito.

## Tablas Afectadas
- `[remuneraciones].[RM_CargosMod]`: UPDATE (Campo `TypeSueldo`).
