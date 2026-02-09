# [remuneraciones].[__SPRM_CargoMod_ActualizaJornadaFullTime]

## Objetivo
Este procedimiento almacenado actualiza la información relacionada con la jornada laboral "Full Time" de una solicitud de modificación de cargo. Gestiona casos especiales como el Artículo 22 y Jornadas Excepcionales, además de actualizar la cantidad de horas semanales.

## Parámetros de Entrada
| Nombre | Tipo | Descripción |
| :--- | :--- | :--- |
| `@USUARIOCREADOR` | `VARCHAR(MAX)` | Identificador del usuario que realiza la actualización. |
| `@CODIGOSOLICITUD` | `VARCHAR(MAX)` | Identificador de la solicitud (codificado en Base64). |
| `@HORARIOS` | `VARCHAR(MAX)` | Parámetro recibido pero **no utilizado** explícitamente en la lógica de actualización mostrada. |
| `@HORASSEMANALES` | `VARCHAR(MAX)` | Valor indicativo de las horas semanales o el tipo de jornada especial ('ART22', 'JornadaExcepcional', o número de horas). |

## Parámetros de Salida
| Nombre | Tipo | Descripción |
| :--- | :--- | :--- |
| `@CODE` | `VARCHAR(MAX)` | Código de respuesta ('200' éxito). |
| `@MESSAGE` | `VARCHAR(MAX)` | Mensaje de respuesta (vacío por defecto). |

## Variables Internas
| Nombre | Tipo | Descripción |
| :--- | :--- | :--- |
| `@__HORASSEMANLES` | `FLOAT` | Almacena el valor numérico procesado de las horas semanales. |
| `@__JORNADAEXCEPCIONAL` | `VARCHAR(MAX)` | Flag para indicar si es jornada excepcional ('S' o NULL). |

## Llamadas Internas
- **Funciones**:
    - `[TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE]`: Para decodificar el identificador de la solicitud.

## Lógica de Cálculo
1.  **Evaluación de Tipo de Jornada (`@HORASSEMANALES`)**:
    - **Caso 'ART22'**: Se asigna `NULL` tanto a horas semanales como al flag de jornada excepcional.
    - **Caso 'JornadaExcepcional'**: Se asigna `NULL` a horas semanales y `'S'` al flag de jornada excepcional.
    - **Caso Numérico** (cualquier otro valor): Se convierte el valor a `FLOAT` para horas semanales y se asigna `NULL` al flag excepcional.
2.  **Actualización**: Actualiza la tabla `RM_CargosMod` con:
    - `NHorasSemanales` (formateando decimales si es necesario).
    - `JornadaExcepcional`.
    - Datos de auditoría (`FechaUltimaActualizacion`, `UsuarioUltimaActualizacion`, `UltimoComentario`).
3.  **Respuesta**: Retorna código '200'.

## Tablas Afectadas
- `[remuneraciones].[RM_CargosMod]`: UPDATE (Campos `NHorasSemanales`, `JornadaExcepcional` y auditoría).

## Código Comentado
Existe un bloque de comentarios al inicio (líneas 16-19) que describe la lógica de negocio aplicada para 'ART22', '45 horas' y 'JornadaExcepcional'.
