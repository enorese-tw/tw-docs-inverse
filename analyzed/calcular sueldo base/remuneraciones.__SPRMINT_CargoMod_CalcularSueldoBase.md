# Documentación del Procedimiento Almacenado: `[remuneraciones].[__SPRMINT_CargoMod_CalcularSueldoBase]`

## Descripción General
Este procedimiento calcula el sueldo base y otros componentes de la remuneración (gratificación, salud, AFP, AFC, impuesto, líquido) basándose en un sueldo esperado y otros parámetros de entrada para un cargo modificado específico.

## Parámetros de Entrada

| Parámetro | Tipo | Descripción |
| :--- | :--- | :--- |
| `@SUELDOESPERADO` | FLOAT | Sueldo esperado (Aparentemente no utilizado directamente en la lógica mostrada, posible parámetro legado o para cálculos futuros). |
| `@OTROIMPONIBLEINPUT` | FLOAT | Monto de otros haberes imponibles ingresados manualmente. |
| `@CODIGOCARGOMOD` | VARCHAR(50) | Código identificador del cargo modificado para buscar sus propiedades en la base de datos. |
| `@OUTGRATIFICACION` | FLOAT OUTPUT | Variable de salida para el monto de la gratificación calculada. |
| `@OUTTOTALES` | FLOAT OUTPUT | Variable de salida para el total de haberes (Sueldo base ajustado + Otros imponibles). |
| `@OUTTOTALESIMP` | FLOAT OUTPUT | Variable de salida para el total imponible (Incluye gratificación). |
| `@OUTOTROSHABERES` | FLOAT OUTPUT | Variable de salida para la suma de otros haberes (Bonos + Asignaciones no imponibles). |
| `@OUTHABERES` | FLOAT OUTPUT | Variable de salida para el total de haberes. |
| `@OUTSALUD` | FLOAT OUTPUT | Variable de salida para el descuento de salud. |
| `@OUTAFP` | FLOAT OUTPUT | Variable de salida para el descuento de AFP. |
| `@OUTAFC` | FLOAT OUTPUT | Variable de salida para el descuento de AFC. |
| `@OUTIMPUESTO` | FLOAT OUTPUT | Variable de salida para el impuesto calculado. |
| `@OUTDESCUENTOS` | FLOAT OUTPUT | Variable de salida para el total de descuentos legales. |
| `@OUTLIQUIDO` | FLOAT OUTPUT | Variable de salida para el sueldo líquido final. |

## Variables Internas

| Variable | Tipo | Descripción |
| :--- | :--- | :--- |
| `@INGRESOMINIMOMENSUAL` | FLOAT | Valor del ingreso mínimo mensual obtenido de constantes (`C001`). Luego ajustado por el tipo de sueldo. |
| `@TYPESUELDOCONSTANTE` | FLOAT | Constante divisor asociada al tipo de sueldo del cargo. |
| `@GRATIFICACION` | FLOAT | Variable declarada para gratificación (Uso secundario o redundante respecto a `@T2`). |
| `@EBONOS` | FLOAT | Suma de bonos (Inicializado en 0 en el código actual). |
| `@EANIS` | FLOAT | Suma de Asignaciones No Imponibles (Calculado vía función). |
| `@TOPEIMPONIBLEUF` | FLOAT | Valor del tope imponible en UF para salud y AFP. |
| `@TOPEIMPONIBLEAFCUF` | FLOAT | Valor del tope imponible en UF para AFC. |
| `@VALORUF` | FLOAT | Valor actual de la UF. |
| `@PORCSALUD` | FLOAT | Porcentaje de descuento de salud (`D005`). |
| `@PORCAFP` | FLOAT | Porcentaje de descuento de AFP. |
| `@PORCAFC` | FLOAT | Porcentaje de descuento de AFC. |
| `@EMPRESA` | VARCHAR(MAX) | Identificador o nombre de la empresa asociada al cargo. |
| `@TYPECONTRATO` | INT | Tipo de contrato (1 para contrato que paga AFC, otros no). |
| `@TOPEAFP` | FLOAT | Monto base topeado para cálculo de AFP y Salud. |
| `@TOPEAFC` | FLOAT | Monto base topeado para cálculo de AFC. |
| `@T1` - `@T11` | FLOAT | Variables temporales de cálculo (T1: Base, T2: Gratificación, T3: Total Imponible, T4: Salud, T6: AFP, T7: AFC, T8: Tributable, T9: Impuesto, T10: Líquido, T11: Descuentos). |

## Llamadas Internas (Funciones y Vistas)

### Vistas y Tablas Consultadas
*   `[remuneraciones].[View_Constantes]`: Para obtener valores constantes como Ingreso Mínimo (`C001`), Porcentaje AFC (`C006`), Porcentaje Salud (`D005`).
*   `[remuneraciones].[RM_CargosMod]`: Para obtener detalles del cargo (Gratificación pactada, empresa, tipo contrato, AFP, etc.).
*   `[remuneraciones].[RM_TypeSueldo]`: Para obtener el valor divisor del sueldo.
*   `[remuneraciones].[View_UltimaUF]`: Para obtener el valor actualizado de la UF.

### Funciones Escalares
*   `[remuneraciones].[FNGratificacion]`: Calcula el monto de la gratificación.
    *   *Inputs*: Gratificación Pactada, Código Cargo, Ingreso Mínimo, GratifCC, TypeSueldoInput.
*   `[remuneraciones].[FNEANI]`: Calcula Asignaciones No Imponibles.
    *   *Inputs*: Código Cargo.
*   `[remuneraciones].[FNUFTopeImponible]`: Obtiene los topes imponibles en UF.
    *   *Inputs*: 'IMP' (General) o 'AFC'.
*   `[remuneraciones].[FNPorcentajeAFP]`: Obtiene el porcentaje de descuento de la AFP.
    *   *Inputs*: Código AFP, Empresa.
*   `[remuneraciones].[FNImpuestoUnicoFromLiquido]`: Calcula el impuesto único.
    *   *Inputs*: Base Tributable (`@T8`), Empresa.
