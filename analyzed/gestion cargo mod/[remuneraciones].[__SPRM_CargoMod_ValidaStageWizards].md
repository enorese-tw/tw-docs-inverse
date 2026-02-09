# [remuneraciones].[__SPRM_CargoMod_ValidaStageWizards]

## Objetivo
Este procedimiento almacenado valida si una etapa específica del "Wizard" de creación de cargo mod se encuentra disponible o habilitada para ser utilizada, basándose en la configuración existente.

## Parámetros de Entrada
| Nombre | Tipo | Descripción |
| :--- | :--- | :--- |
| `@CODIGOSOLICITUD` | `VARCHAR(MAX)` | Identificador de la solicitud. |
| `@ESTADO` | `VARCHAR(MAX)` | Valor numérico (en texto) que representa la etapa del wizard a validar. |

## Parámetros de Salida
| Nombre | Tipo | Descripción |
| :--- | :--- | :--- |
| `@CODE` | `VARCHAR(MAX)` | Código de respuesta: '200' si la etapa está disponible/válida, '403' si no lo está. |
| `@MESSAGE` | `VARCHAR(MAX)` | Mensaje explicativo (vacío en caso de éxito). |

## Variables Internas
| Nombre | Tipo | Descripción |
| :--- | :--- | :--- |
| `@__STAGEMAX` | `NUMERIC` | Variable declarada y asignada (valor 7), pero **no utilizada** en la consulta de validación actual. |

## Llamadas Internas
- **Vistas**:
    - `[remuneraciones].[View_StageWizards]`: Vista utilizada para verificar la existencia y validez de la etapa para la solicitud dada.

## Lógica de Cálculo
1.  **Consulta de Validación**: Verifica si existe algún registro en `View_StageWizards` que cumpla con:
    - `Stage`: Debe ser mayor o igual al valor numérico de `@ESTADO`.
    - `CodigoCargoMod`: Debe coincidir con `@CODIGOSOLICITUD`.
2.  **Resultado**:
    - Si el conteo es mayor a 0 (existe registro válido), retorna `@CODE = '200'`.
    - Si no existe, retorna `@CODE = '403'` y un mensaje indicando que la etapa no está disponible.

## Tablas Afectadas
No realiza modificaciones en tablas (solo lectura).

## Código Comentado
No se observa código comentado en este archivo.
