# Documentación de la Función: `[remuneraciones].[FNPorcObligaciones]`

## Objetivo
Esta función escalar calcula un **factor de obligaciones previsionales** que representa la proporción del sueldo que permanece después de descontar las cotizaciones obligatorias de AFP, Salud y Seguro de Cesantía (AFC). Este factor es útil para estimaciones inversas o cálculos de sueldo líquido aproximado.

## Parámetros de Entrada

| Parámetro | Tipo | Descripción |
| :--- | :--- | :--- |
| `@TIPOCONTRATO` | INT | Tipo de contrato (ej. 2 = Plazo Fijo/Obra, otros = Indefinido). Determina si se descuenta AFC al trabajador. |
| `@EMPRESA` | VARCHAR(MAX) | Identificador de la empresa ('TWEST', 'TWRRHH', 'TWC') para buscar el % de AFP correcto. |
| `@AFP` | VARCHAR(MAX) | Nombre de la AFP para obtener su porcentaje de cotización. |

## Variables Internas

| Variable | Tipo | Descripción |
| :--- | :--- | :--- |
| `@OBLIGACIONES` | FLOAT | Almacena el factor final calculado (ej: 0.82 para un descuento total del 18%). |
| `@PORCAFP` | FLOAT | Porcentaje de cotización de la AFP. |
| `@SALUD` | FLOAT | Porcentaje de cotización de Salud (obtenido de constantes). |
| `@AFC` | FLOAT | Porcentaje de cotización de Seguro de Cesantía a cargo del trabajador. |

## Llamadas Internas (Funciones y Vistas)

### Vistas y Tablas Consultadas
*   `[remuneraciones].[View_AfpTWEST]`, `[remuneraciones].[View_AfpTWRRHH]`, `[remuneraciones].[View_AfpTWC]`: Vistas para obtener el porcentaje de AFP según la empresa.
*   `[remuneraciones].[RM_Constantes]`:
    *   `D005`: Porcentaje de Salud.
    *   `D007`: Porcentaje de AFC.

## Lógica de Cálculo

1.  **Obtención de Porcentaje AFP**:
    *   Selecciona el porcentaje desde la vista de AFP correspondiente a la `@EMPRESA`.
2.  **Obtención de Salud**:
    *   Obtiene el valor constante de descuento de saludo (`D005`).
3.  **Obtención de AFC**:
    *   Si `@TIPOCONTRATO` es distinto de `2` (es decir, no es Plazo Fijo, asumiendo Indefinido u otro sujeto a cobro), se obtiene el porcentaje de AFC (`D007`).
    *   Si es `2`, el descuento AFC trabajador se asume `0` (inicializado al declarar).
4.  **Cálculo del Factor**:
    *   Suma los porcentajes: `TotalDescuentos = @PORCAFP + @SALUD + @AFC`.
    *   Calcula el remanente sobre 100: `100 - TotalDescuentos`.
    *   Divide por 100 para obtener el factor decimal: `(100 - TotalDescuentos) / 100`.

## Retorno
*   Devuelve un `FLOAT` representando el factor (ej: `0.817` si los descuentos suman 18.3%).
