# [remuneraciones].[__SPRM_CargoMod_ActualizaHorario]

## Objetivo
Este procedimiento almacenado tiene como función actualizar el campo de horarios asociado a una solicitud de modificación de cargo específica.

## Parámetros de Entrada
| Nombre | Tipo | Descripción |
| :--- | :--- | :--- |
| `@USUARIOCREADOR` | `VARCHAR(MAX)` | Usuario que realiza la operación (aunque no se utiliza explícitamente en el cuerpo del procedimiento). |
| `@CODIGOSOLICITUD` | `VARCHAR(MAX)` | Identificador de la solicitud (codificado en Base64). |
| `@HORARIOS` | `VARCHAR(MAX)` | Cadena de texto que contiene la nueva información de horarios a asignar. |

## Parámetros de Salida
| Nombre | Tipo | Descripción |
| :--- | :--- | :--- |
| `@CODE` | `VARCHAR(MAX)` | Código de respuesta de la operación (ej. '200'). |
| `@MESSAGE` | `VARCHAR(MAX)` | Mensaje de respuesta (retorna vacío por defecto). |

## Variables Internas
| Nombre | Tipo | Descripción |
| :--- | :--- | :--- |
| `@CONSTPT` | `VARCHAR(MAX)` | Variable declarada pero **no utilizada** en la lógica actual. |

## Llamadas Internas
- **Funciones**:
    - `[TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE]`: Se utiliza para decodificar el parámetro `@CODIGOSOLICITUD` y obtener el ID real para el filtro `WHERE`.

## Lógica de Cálculo
1.  **Actualización**: Se ejecuta una sentencia `UPDATE` sobre la tabla `[remuneraciones].[RM_CargosMod]`, asignando el valor del parámetro `@HORARIOS` al campo homónimo, para el registro cuyo `CodigoCargoMod` coincide con el ID decodificado.
2.  **Retorno**: Se establecen los parámetros de salida `@CODE` en '200' y `@MESSAGE` en vacío.

## Tablas Afectadas
- `[remuneraciones].[RM_CargosMod]`: Operación `UPDATE` sobre el campo `Horarios`.

## Código Comentado
No se observa código comentado en este archivo.
