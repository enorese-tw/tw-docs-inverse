# [remuneraciones].[__SPRM_CargoMod_CambiarInputSueldo]

## Objetivo
Este procedimiento almacenado actualiza el tipo de sueldo de entrada (`TypeSueldoInput`) para una solicitud de modificación de cargo, considerando la jornada laboral asociada. Si la jornada no es completa ('F'), ajusta el valor agregando el sufijo 'PT' (Part Time).

## Parámetros de Entrada
| Nombre | Tipo | Descripción |
| :--- | :--- | :--- |
| `@SUELDO` | `VARCHAR(MAX)` | El nuevo tipo de sueldo a asignar (ej. 'SB', 'SL'). |
| `@CODIGOCARGOMOD` | `VARCHAR(MAX)` | Identificador de la solicitud de modificación de cargo. |

## Parámetros de Salida
| Nombre | Tipo | Descripción |
| :--- | :--- | :--- |
| `@CODE` | `VARCHAR(MAX)` | Código de respuesta (ej. '200'). |
| `@MESSAGE` | `VARCHAR(MAX)` | Mensaje de resultado. |

## Variables Internas
| Nombre | Tipo | Descripción |
| :--- | :--- | :--- |
| `@__TYPEJORNADA` | `VARCHAR(MAX)` | Almacena el tipo de jornada recuperado de la cabecera de la solicitud ('F': Full, otros: Part Time). |

## Llamadas Internas
- **Vistas**:
    - `[remuneraciones].[View_HeaderEstructura]`: Para obtener el tipo de jornada (`TypeJornada`) asociado a la solicitud.
- **Funciones**:
    - `[dbo].[FNBase64Decode]`: Decodifica el parámetro `@CODIGOCARGOMOD`.

## Lógica de Cálculo
1.  **Obtención de Jornada**: Consulta `TypeJornada` en la vista `View_HeaderEstructura` filtrando por el `@CODIGOCARGOMOD`.
2.  **Actualización**: Actualiza el campo `TypeSueldoInput` en `[remuneraciones].[RM_CargosMod]` aplicando lógica condicional:
    - Si `@__TYPEJORNADA` es `'F'` (Full), asigna el valor directo de `@SUELDO`.
    - Si es diferente (ej. Part Time), concatena `'PT'` al valor de `@SUELDO` (ej. convertir 'SB' en 'SBPT').
3.  **Respuesta**: Retorna código '200' y mensaje de éxito.

## Tablas Afectadas
- `[remuneraciones].[RM_CargosMod]`: UPDATE (Campo `TypeSueldoInput`).
