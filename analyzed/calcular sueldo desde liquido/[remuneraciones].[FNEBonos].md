# Documentación de la Función: `[remuneraciones].[FNEBonos]`

## Objetivo
Esta función escalar calcula la **sumatoria total de los bonos** vigentes asociados a un "Cargo Modificado" específico.

## Parámetros de Entrada

| Parámetro | Tipo | Descripción |
| :--- | :--- | :--- |
| `@CODIGOCARGOMOD` | VARCHAR(MAX) | Código identificador del cargo modificado para el cual se desean sumar los bonos. |

## Variables Internas

| Variable | Tipo | Descripción |
| :--- | :--- | :--- |
| `@EBONOS` | FLOAT | Almacena el resultado de la suma de los valores de los bonos. |

## Llamadas Internas (Funciones y Vistas)

### Vistas y Tablas Consultadas
*   `[remuneraciones].[RM_BonosCargoMod]`: Tabla que contiene los registros de bonos asignados a los cargos.

## Lógica de Cálculo

1.  **Consulta y Agregación**:
    *   Realiza una consulta a la tabla `[remuneraciones].[RM_BonosCargoMod]`.
    *   Filtra los registros que coinciden con el `@CODIGOCARGOMOD` proporcionado.
    *   Suma los montos de la columna `Valor`.
2.  **Manejo de Nulos**:
    *   Utiliza `ISNULL(..., 0)` para garantizar que, si no existen bonos o la suma es nula, la función devuelva `0` y no `NULL`.
3.  **Retorno**:
    *   Devuelve el valor acumulado en `@EBONOS`.

## Retorno
*   Devuelve un valor de tipo `FLOAT` con el total de bonos.
