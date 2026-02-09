# [remuneraciones].[__SPRM_CargoMod_DescuentosEstructura]

## Objetivo
Este procedimiento almacenado obtiene el detalle estructurado de los descuentos (previsionales, salud, impuestos, etc.) asociados a una solicitud de modificación de cargo específica. Retorna los valores formateados como porcentajes o montos en moneda local, adaptando la fuente de datos según el estado de la solicitud.

## Parámetros de Entrada
| Nombre | Tipo | Descripción |
| :--- | :--- | :--- |
| `@CODIGOCARGOMOD` | `VARCHAR(MAX)` | Identificador único de la solicitud de modificación de cargo. |

## Variables Internas
| Nombre | Tipo | Descripción |
| :--- | :--- | :--- |
| `@__ESTADO` | `VARCHAR(MAX)` | Almacena el estado actual de la solicitud (ej. 'TER', 'FD', 'CREATE'). |

## Llamadas Internas (Funciones y Vistas)
- Funciones Escalares:
    - `[TW_GENERAL_TEAMWORK].[dbo].[FN_CONVERTMONEY]`: Formatea valores numéricos a formato moneda.
    - `[dbo].[FNConvertMoney]`: Formatea valores numéricos a formato moneda (usada en el bloque ELSE).
    - `[dbo].[FNBase64Decode]`: Decodifica el código de modificación de cargo en las subconsultas.
- Vistas:
    - `[remuneraciones].[View_Solicitudes]`: Para obtener el estado de la solicitud.
    - `[remuneraciones].[View_DescuentosEstructura]`: Fuente principal de datos de descuentos.
    - `[remuneraciones].[View_EstructuraRentaDescuento]`: Fuente detallada de ítems de descuento (usada para estados finalizados).

## Lógica de Cálculo
1.  **Obtención de Estado**: Consulta el estado (`EstadoReal`) de la solicitud en `[remuneraciones].[View_Solicitudes]`.
2.  **Bifurcación por Estado**:
    - **Caso Activo (No 'TER' ni 'FD')**:
        - Consulta directamente la vista `[remuneraciones].[View_DescuentosEstructura]`.
        - Retorna los campos pre-calculados de la vista, aplicando formato de moneda (`$ ...`) y porcentajes (`%`).
    - **Caso Finalizado ('TER' - Terminado, 'FD' - Firma Digital)**:
        - Consulta la vista base `[remuneraciones].[View_DescuentosEstructura]`.
        - Para cada campo de valor (AFP, Salud, Seguro Cesantía, Impuesto), ejecuta una subconsulta a la vista histórica/detallada `[remuneraciones].[View_EstructuraRentaDescuento]` filtrando por:
            - Código de modificación decodificado.
            - Código de concepto específico (D001: AFP, D005: Salud, D006: Impuesto, D007: Seguro Cesantía, etc.).
        - Retorna los valores históricos guardados en esa estructura.

## Retorno
Retorna un registro con la información de descuentos formateada para visualización:
- `PorcAFP`, `AFP`, `CLPAFP`
- `PorcFondoPensiones`
- `PorcSegInvalidez`
- `PorcSalud`, `CLPSalud`
- `CLPImpuestoUnico`
- `PorcSeguroDesempleo`, `CLPSeguroDesempleo`
- `TotalDescuentos`
- `CodigoCargoMod`

## Tablas Afectadas
El procedimiento es de solo lectura (SELECT). No realiza modificaciones en la base de datos.
