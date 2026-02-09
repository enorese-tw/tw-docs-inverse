# [remuneraciones].[__SPRM_CargoMod_ValidaStageActual]

## Objetivo
Este procedimiento almacenado valida el estado actual de una solicitud de modificación de cargo para determinar si es posible realizar ediciones sobre ella o si, por el contrario, ya ha avanzado a una etapa donde no se permiten cambios (ej. ya aprobada y enviada a remuneraciones).

## Parámetros de Entrada
| Nombre | Tipo | Descripción |
| :--- | :--- | :--- |
| `@CODIGOSOLICITUD` | `VARCHAR(MAX)` | Identificador de la solicitud (codificado en Base64). |

## Parámetros de Salida
| Nombre | Tipo | Descripción |
| :--- | :--- | :--- |
| `@CODE` | `VARCHAR(MAX)` | Código de respuesta: '200' si es editable, '201' si no lo es, '404' si no existe. |
| `@MESSAGE` | `VARCHAR(MAX)` | Mensaje descriptivo (incluye HTML para el caso de bloqueo '201'). |

## Variables Internas
| Nombre | Tipo | Descripción |
| :--- | :--- | :--- |
| `@ESTADO` | `VARCHAR(MAX)` | Almacena el estado actual de la solicitud recuperado de la base de datos. |

## Llamadas Internas
- **Funciones**:
    - `[TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE]`: Para decodificar el ID de la solicitud.

## Lógica de Cálculo
1.  **Recuperación**: Obtiene el estado (`Estado`) de la tabla `RM_CargosMod` para el ID proporcionado.
2.  **Validación de Existencia**:
    - Si el registro no existe (`@ESTADO` es NULL), retorna `@CODE = '404'` y un mensaje de error.
3.  **Validación de Estado**:
    - Si el estado es **'PDAPROB'** (Pendiente aprobación) o **'SOLIC'** (Solicitado): Se considera editable. Retorna `@CODE = '200'` y mensaje vacío.
    - Si el estado es **'APROB'** (Aprobado): Se considera no editable. Retorna `@CODE = '201'` y un mensaje HTML informando al usuario que la solicitud ya fue enviada a remuneraciones y que para editarla debe ser rechazada previamente.

## Tablas Afectadas
No realiza modificaciones en tablas (solo lectura).

## Código Comentado
No se observa código comentado en este archivo.
