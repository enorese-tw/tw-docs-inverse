# [remuneraciones].[__SPRM_CargoMod_ValidaCadenaObservaciones]

## Objetivo
Este procedimiento almacenado verifica si una cadena de texto específica existe dentro de las observaciones registradas para una solicitud de modificación de cargo.

## Parámetros de Entrada
| Nombre | Tipo | Descripción |
| :--- | :--- | :--- |
| `@USUARIOCREADOR` | `VARCHAR(MAX)` | Identificador del usuario (no utilizado en la lógica de validación). |
| `@CODIGOSOLICITUD` | `VARCHAR(MAX)` | Identificador de la solicitud (se utiliza directamente para filtrar la vista, sin decodificación explícita en este SP). |
| `@OBSERVACIONES` | `VARCHAR(MAX)` | La subcadena de texto que se desea buscar dentro de las observaciones existentes. |

## Parámetros de Salida
| Nombre | Tipo | Descripción |
| :--- | :--- | :--- |
| `@CODE` | `VARCHAR(MAX)` | Código de respuesta: '200' si el texto existe, '404' si no se encuentra. |
| `@MESSAGE` | `VARCHAR(MAX)` | Mensaje de respuesta (vacío por defecto). |

## Variables Internas
| Nombre | Tipo | Descripción |
| :--- | :--- | :--- |
| `@__CADENAOBSERVACIONES` | `VARCHAR(MAX)` | Almacena el texto completo de las observaciones recuperado de la base de datos. |

## Llamadas Internas
- **Vistas**:
    - `[remuneraciones].[View_HeaderEstructura]`: Fuente de datos para obtener las observaciones de la solicitud.

## Lógica de Cálculo
1.  **Recuperación**: Consulta el campo `Observaciones` de la vista `View_HeaderEstructura` filtrando por `@CODIGOSOLICITUD`.
2.  **Validación**: Utiliza la función `CHARINDEX` para buscar la ocurrencia de la cadena `@OBSERVACIONES` dentro de `@__CADENAOBSERVACIONES`.
3.  **Resultado**:
    - Si `CHARINDEX > 0` (se encontró coincidencias), retorna `@CODE = '200'`.
    - Si no se encuentra, retorna `@CODE = '404'`.

## Tablas Afectadas
No realiza modificaciones en tablas (solo lectura).

## Código Comentado
No se observa código comentado en este archivo.
