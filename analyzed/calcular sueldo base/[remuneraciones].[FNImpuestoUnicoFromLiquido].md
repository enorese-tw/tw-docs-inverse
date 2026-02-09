# Documentación de la Función: `[remuneraciones].[FNImpuestoUnicoFromLiquido]`

## Objetivo
Esta función escalar calcula el monto del **Impuesto Único** de Segunda Categoría a pagar, basándose en el monto tributable presentado y la empresa asociada. Utiliza tablas de tramos de impuesto específicas por empresa y el valor de la UTM.

## Parámetros de Entrada

| Parámetro | Tipo | Descripción |
| :--- | :--- | :--- |
| `@TRIBUTARIO` | FLOAT | Monto de la base tributable (en pesos) sobre la cual se calculará el impuesto. |
| `@EMPRESA` | VARCHAR(MAX) | Identificador de la empresa ('TWEST', 'TWRRHH', 'TWC') para seleccionar la tabla de impuestos correcta. |

## Variables Internas

| Variable | Tipo | Descripción |
| :--- | :--- | :--- |
| `@VALORUTM` | FLOAT | Valor actual de la Unidad Tributaria Mensual (UTM). |
| `@PORCIMPUESTO` | FLOAT | Factor (porcentaje) del impuesto a aplicar según el tramo. |
| `@IMPUESTOUNICO` | FLOAT | Monto final del impuesto calculado. |
| `@CONSTANTE` | FLOAT | Valor de la base tributable convertida a unidades de UTM (`@TRIBUTARIO / @VALORUTM`). |
| `@REBAJA` | FLOAT | Monto de la rebaja impositiva (en UTM) correspondiente al tramo. |

## Llamadas Internas (Funciones y Vistas)

### Vistas Consultadas
*   `[remuneraciones].[View_UltimaUTM]`: Obtiene el valor vigente de la UTM.
*   `[remuneraciones].[View_ImpuestoUnicoTWEST]`: Tabla de tramos de impuesto para la empresa 'TWEST'.
*   `[remuneraciones].[View_ImpuestoUnicoTWRRHH]`: Tabla de tramos de impuesto para la empresa 'TWRRHH'.
*   `[remuneraciones].[View_ImpuestoUnicoTWC]`: Tabla de tramos de impuesto para la empresa 'TWC'.

## Lógica de Cálculo
1.  **Obtención de UTM**: Se recupera el valor de la UTM desde la vista correspondiente.
2.  **Conversión a UTM**: Se convierte el monto tributable (`@TRIBUTARIO`) a su equivalente en UTM (`@CONSTANTE`).
3.  **Selección de Tramo**:
    *   Dependiendo del valor de `@EMPRESA`, se consulta la vista de impuestos específica.
    *   Se busca el registro donde `@CONSTANTE` se encuentre dentro del rango (`TopeMin <= @CONSTANTE < TopeMax`).
    *   Se obtienen el porcentaje (`@PORCIMPUESTO`) y la rebaja (`@REBAJA`) correspondientes.
4.  **Cálculo Final**:
    *   Se aplica la fórmula: `(Base en UTM * Valor UTM * Porcentaje) - (Rebaja en UTM * Valor UTM)`.
    *   Esto es equivalente a: `(Base Tributable * Porcentaje) - (Rebaja * Valor UTM)`.

## Retorno
*   Devuelve un valor de tipo `FLOAT` que representa el impuesto único calculado.
