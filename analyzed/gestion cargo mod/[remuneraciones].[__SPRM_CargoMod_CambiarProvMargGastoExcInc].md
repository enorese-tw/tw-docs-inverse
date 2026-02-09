# [remuneraciones].[__SPRM_CargoMod_CambiarProvMargGastoExcInc]

## Objetivo
Este procedimiento almacenado gestiona la inclusión o exclusión de gastos/márgenes específicos en el cálculo de una solicitud de modificación de cargo. Permite "apagar" o "encender" conceptos financieros (marginar o no) para una solicitud dada.

## Parámetros de Entrada
| Nombre | Tipo | Descripción |
| :--- | :--- | :--- |
| `@USUARIOCREADOR` | `VARCHAR(MAX)` | Usuario que realiza la acción. |
| `@CODIGOCARGOMOD` | `VARCHAR(MAX)` | Identificador de la solicitud de modificación de cargo (codificado en Base64). |
| `@CODIGOVAR` | `VARCHAR(MAX)` | Código de la variable de gasto/margen a modificar. |
| `@EXCINC` | `VARCHAR(MAX)` | Acción a realizar: 'EXC' para excluir, 'INC' para incluir (remover la exclusión). |

## Parámetros de Salida
| Nombre | Tipo | Descripción |
| :--- | :--- | :--- |
| `@CODE` | `VARCHAR(MAX)` | Código de respuesta ('200' para exclusión exitosa, '400' para inclusión/eliminación exitosa). |
| `@MESSAGE` | `VARCHAR(MAX)` | Mensaje de resultado (actualmente se retorna vacío). |

## Variables Internas
No utiliza variables locales complejas.

## Llamadas Internas
- **Funciones**:
    - `[TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE]`: Decodifica el parámetro `@CODIGOCARGOMOD`.

## Lógica de Cálculo
1.  **Exclusión ('EXC')**:
    - Inserta un registro en la tabla `[remuneraciones].[RM_GastosMargenExc]`, indicando que el gasto `@CODIGOVAR` debe ser excluido para la solicitud `@CODIGOCARGOMOD`.
    - Registra metadatos como fecha, usuario y un comentario automático ("Se excluye gasto de finanzas...").
    - Retorna `@CODE = '200'`.
2.  **Inclusión ('INC')**:
    - Elimina el registro correspondiente de la tabla `[remuneraciones].[RM_GastosMargenExc]`, permitiendo que el gasto vuelva a ser considerado en el cálculo.
    - Retorna `@CODE = '400'` (Nota: Este código podría interpretarse como un estado distinto en el front-end, aunque la operación es exitosa).

## Tablas Afectadas
- `[remuneraciones].[RM_GastosMargenExc]`: INSERT (al excluir) o DELETE (al incluir).
