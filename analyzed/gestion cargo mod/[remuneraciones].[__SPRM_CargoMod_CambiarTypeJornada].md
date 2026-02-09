# [remuneraciones].[__SPRM_CargoMod_CambiarTypeJornada]

## Objetivo
Este procedimiento almacenado actualiza el tipo de jornada laboral asociada a una solicitud de modificación de cargo, ajustando también los horarios o el tipo de cálculo part-time según corresponda.

## Parámetros de Entrada
| Nombre | Tipo | Descripción |
| :--- | :--- | :--- |
| `@JORNADA` | `VARCHAR(MAX)` | Código del nuevo tipo de jornada (ej. 'F', 'P', o códigos específicos para part-time). |
| `@CODIGOCARGOMOD` | `VARCHAR(MAX)` | Identificador de la solicitud de modificación de cargo (codificado en Base64). |

## Parámetros de Salida
| Nombre | Tipo | Descripción |
| :--- | :--- | :--- |
| `@CODE` | `VARCHAR(MAX)` | Código de respuesta (ej. '200'). |
| `@MESSAGE` | `VARCHAR(MAX)` | Mensaje de resultado. |

## Variables Internas
No utiliza variables locales complejas.

## Llamadas Internas
- **Funciones**:
    - `[TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE]`: Decodifica el parámetro `@CODIGOCARGOMOD`.

## Lógica de Cálculo
1.  **Detección de Tipo de Jornada**:
    - **Jornada Completa o Parcial Estándar ('F' o 'P')**:
        - Actualiza el campo `TypeJornada` en `[remuneraciones].[RM_CargosMod]`.
        - Si la jornada es 'F' (Full), intenta limpiar el campo `Horarios` eliminando prefijos antes del primer guion (`-`), asumiendo una normalización del texto. En otros casos, mantiene el horario existente.
    - **Otros Tipos (Part Time Específico)**:
        - Actualiza el campo `TypeCalculoPT` (Tipo de Cálculo Part Time) con el valor de `@JORNADA`, manteniendo `TypeJornada` sin cambios directos en esta rama.
2.  **Respuesta**: Devuelve código '200' y mensaje de éxito.

## Tablas Afectadas
- `[remuneraciones].[RM_CargosMod]`: UPDATE (Campos `TypeJornada`, `Horarios` o `TypeCalculoPT`).
