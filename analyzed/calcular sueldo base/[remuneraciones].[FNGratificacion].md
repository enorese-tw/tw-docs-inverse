# Documentación de la Función: `[remuneraciones].[FNGratificacion]`

## Descripción General
Esta función escalar calcula el monto de la gratificación legal o convenida para un cargo modificado, basándose en el tipo de contrato, sueldo base, bonos y topes legales.

## Parámetros de Entrada

| Parámetro | Tipo | Descripción |
| :--- | :--- | :--- |
| `@CONVENIDA` | VARCHAR(1) | Indica si la gratificación es convenida ('S') o no ('N'). |
| `@CODIGOCARGOMOD` | VARCHAR(MAX) | Código del cargo modificado. |
| `@SUELDOBASEXDIASTRABAJADOS` | FLOAT | Monto del sueldo base calculado proporcional a los días trabajados. |
| `@COSTOCERO` | VARCHAR(1) | Indicador de "Costo Cero" ('N' = Normal, 'S' = Costo Cero). Si es 'S' y no es convenida, la gratificación es 0. |
| `@TYPESUELDO` | VARCHAR(5) | Tipo de sueldo ('SB', 'SBPT', 'SL', 'SLPT') que determina la fórmula de cálculo. |

## Variables Internas

| Variable | Tipo | Descripción |
| :--- | :--- | :--- |
| `@GRATIFICACION` | FLOAT | Valor final de la gratificación a retornar. |
| `@CONSTANTE` | FLOAT | Factor de cálculo de la gratificación (Variable `C013`, usualmente 25% o 0.25). |
| `@TOPEGRATIFICACION` | FLOAT | Valor máximo legal de la gratificación (Variable `C002`, ej. 4.75 IMM / 12). |
| `@EBONOS` | FLOAT | Suma de los bonos vigentes asociados al cargo. |
| `@CALCULOGRATIFICACION25` | FLOAT | Resultado preliminar del cálculo del 25% (legal). |
| `@__WITHEXCEPCION` | NUMERIC | Indicador (Conteo) de si la empresa/cliente tiene una excepción de gratificación. |
| `@__CLIENTE` | VARCHAR(MAX) | Identificador del cliente obtenido del cargo. |
| `@__EMPRESA` | VARCHAR(MAX) | Identificador de la empresa obtenido del cargo. |

## Llamadas Internas (Funciones y Vistas)

### Vistas y Tablas Consultadas
*   `[remuneraciones].[View_CargoModCliente]`: Utilizada para obtener el Cliente y Empresa asociados al `CodigoCargoMod`.
*   `[remuneraciones].[RM_Constantes]`: Consultada para obtener valores de configuración:
    *   `C013`: Factor de gratificación (Constante).
    *   `C002`: Tope de gratificación.
*   `[remuneraciones].[View_ExcepcionGratificacion]`: Verifica si existe alguna excepción para la combinación Cliente/Empresa.
*   `[remuneraciones].[RM_BonosCargoMod]`: Obtiene y suma los bonos vigentes para el cargo.
*   `[remuneraciones].[RM_GratificacionConvenida]`: Obtiene el valor de la gratificación si está marcada como "Convenida" (`@CONVENIDA = 'S'`).

### Funciones Escalares
*   `[dbo].[FNBase64Encode]`: Se utiliza en el `WHERE` al consultar `View_CargoModCliente`, lo que sugiere que el código en la vista está almacenado o indexado en Base64.

## Lógica de Cálculo

1.  **Gratificación Convenida (`@CONVENIDA = 'S'`):**
    *   Se busca el valor directamente en la tabla `[remuneraciones].[RM_GratificacionConvenida]`.

2.  **Gratificación Legal (`@CONVENIDA = 'N'`):**
    *   Si `@COSTOCERO = 'S'`, la gratificación es 0.
    *   Si `@COSTOCERO = 'N'`, se procede al cálculo estándar:
        *   Se obtienen constantes y datos de empresa.
        *   Se verifica si hay excepción.
        *   **Sin Excepción:**
            *   Se suman los bonos (`@EBONOS`).
            *   Se calcula el 25% (`@CALCULOGRATIFICACION25`) usando la fórmula según `@TYPESUELDO`:
                *   Si es Sueldo Base (`SB`, `SBPT`): `(SueldoBase + Bonos) * Constante`.
                *   Si es Sueldo Líquido (`SL`, `SLPT`): `(SueldoBase * Constante) - Bonos`.
            *   Se aplica el tope (`@TOPEGRATIFICACION`): Se elige el menor valor entre el cálculo y el tope.
        *   **Con Excepción:**
            *   La gratificación se fija automáticamente en el tope (`@TOPEGRATIFICACION`).

3.  **Retorno:**
    *   El resultado se redondea a 0 decimales (`ROUND(@GRATIFICACION, 0)`).
