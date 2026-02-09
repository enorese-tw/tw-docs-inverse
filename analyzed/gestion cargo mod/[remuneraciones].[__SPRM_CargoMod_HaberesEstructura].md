# [remuneraciones].[__SPRM_CargoMod_HaberesEstructura]

## Objetivo
Este procedimiento almacenado obtiene el detalle estructurado de los haberes (sueldo base, gratificación, totales imponibles, etc.) asociados a una solicitud de modificación de cargo específica. Retorna los valores monetarios formateados, adaptando la fuente de datos a tablas históricas cuando la solicitud ya ha sido finalizada.

## Parámetros de Entrada
| Nombre | Tipo | Descripción |
| :--- | :--- | :--- |
| `@CODIGOCARGOMOD` | `VARCHAR(MAX)` | Identificador de la solicitud de modificación de cargo. |

## Variables Internas
| Nombre | Tipo | Descripción |
| :--- | :--- | :--- |
| `@__ESTADO` | `VARCHAR(MAX)` | Almacena el estado actual de la solicitud (ej. 'TER', 'FD', 'CREATE') para determinar la lógica de consulta. |

## Llamadas Internas (Funciones y Vistas)
- Funciones Escalares:
    - `[TW_GENERAL_TEAMWORK].[dbo].[FN_CONVERTMONEY]`: Formatea valores numéricos a formato moneda para solicitudes activas.
    - `[dbo].[FNConvertMoney]`: Formatea valores numéricos a formato moneda (usada en el bloque ELSE para históricas).
    - `[dbo].[FNBase64Decode]`: Decodifica el código de modificación en las subconsultas.
- Vistas:
    - `[remuneraciones].[View_Solicitudes]`: Obtención del estado.
    - `[remuneraciones].[View_HaberesEstructura]`: Vista principal de haberes actuales.
    - `[remuneraciones].[View_EstructuraBaseHaberes]`: Vista detallada/histórica de ítems de haberes por concepto.

## Lógica de Cálculo
1.  **Consulta de Estado**: Verifica el campo `EstadoReal` de la solicitud en `[remuneraciones].[View_Solicitudes]`.
2.  **Selección de Fuente de Datos**:
    - **Solicitudes en Proceso** (Estado distinto de 'TER' y 'FD'):
        - Obtiene los datos directamente de `[remuneraciones].[View_HaberesEstructura]`.
        - Formatean los montos (Sueldo Base, Líquido, Gratificación, etc.) usando `FN_CONVERTMONEY`.
    - **Solicitudes Finalizadas** (Estado 'TER' o 'FD'):
        - Utiliza `[remuneraciones].[View_HaberesEstructura]` como base para campos estáticos.
        - Para los montos, realiza subconsultas específicas a la vista histórica `[remuneraciones].[View_EstructuraBaseHaberes]`, mapeando cada concepto a su código correspondiente:
            - `H001`: Sueldo Base
            - `H008`: Sueldo Líquido
            - `H002`: Gratificación
            - `H003`: Base Imponible
            - `H004`: Base Imponible AFC
            - `H005`: Total Imponible
            - `H006`: Total Tributable
            - `H007`: Total Haberes

## Retorno
Retorna un registro con la información financiera del cargo formateada (`$ ...`), incluyendo:
- Desglose de Haberes: `SueldoBase`, `SueldoLiquido`, `Gratificacion`.
- Totales Calculados: `BaseImponible`, `TotalImponible`, `TotalTributable`, `TotalHaberes`.
- Campos de Texto Cifra (vacíos en la lógica actual).
- Información Adicional: `GratifCC`, `GratificacionPactada`, Mensajes informativos (`MessageGratif`, `MessageSueldoBase`, etc.).

## Tablas Afectadas
Operación de solo lectura (SELECT). No modifica datos.
