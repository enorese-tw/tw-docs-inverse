# Documentación de la Función: `[dbo].[FN_CONVERTMONEY]`

## Objetivo
Esta función escalar convierte un valor numérico (`NUMERIC`) en una cadena de texto (`VARCHAR`) con formato de moneda, utilizando el punto (`.`) como separador de miles y sin decimales.

## Parámetros de Entrada

| Parámetro | Tipo | Descripción |
| :--- | :--- | :--- |
| `@MONTO` | NUMERIC | El valor numérico que se desea formatear. |

## Variables Internas

| Variable | Tipo | Descripción |
| :--- | :--- | :--- |
| `@MONEY` | VARCHAR(MAX) | Almacena la cadena resultante formateada. |

## Lógica de Cálculo

1.  **Conversión y Formateo**:
    *   `CAST(@MONTO AS MONEY)`: Convierte el input a tipo `MONEY`.
    *   `CONVERT(VARCHAR, ..., 1)`: Convierte el tipo `MONEY` a `VARCHAR` con estilo `1` (que incluye comas como separador de miles y dos decimales, ej: `12,345.00`). *Nota: El código usa estilo `1` implícitamente o por defecto regional, aunque en el script se ve `-1` que podría ser un error tipográfico o un estilo específico no estándar, pero la intención es obtener separadores*. Revisando el código: `CONVERT(VARCHAR, CAST(@MONTO AS MONEY), 1)`.
    *   **Corrección sobre el código observado**: El código usa `CONVERT(..., 1)`. *Corrección*: El código proporcionado dice `CONVERT(..., -1)`. Sin embargo, la lógica de `REPLACE` sugiere que busca manipular los separadores.
    *   El código hace:
        1.  Convierte a Money.
        2.  Convierte a Varchar.
        3.  Busca la posición del punto decimal (`CHARINDEX('.')`).
        4.  Toma la subcadena (`SUBSTRING`) desde el inicio hasta antes del punto (eliminando decimales).
        5.  Reemplaza (`REPLACE`) las comas (`,`) por puntos (`.`) para usar el formato de miles chileno/europeo (ej: `1.000` en lugar de `1,000`).

## Retorno
*   Devuelve un `VARCHAR(MAX)` con el número formateado (ej: si entra `1000`, sale `'1.000'`).
