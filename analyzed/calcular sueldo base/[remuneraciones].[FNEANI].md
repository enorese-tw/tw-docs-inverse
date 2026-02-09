# Documentación de la Función: `[remuneraciones].[FNEANI]`

## Objetivo
Esta función escalar tiene como objetivo calcular el monto total de las **Asignaciones No Imponibles** (EANIS) asociadas a un "Cargo Modificado" específico. Suma los valores de las asignaciones configuradas para dicho cargo.

## Parámetros de Entrada

| Parámetro | Tipo | Descripción |
| :--- | :--- | :--- |
| `@CODIGOCARGOMOD` | VARCHAR(MAX) | Código identificador del cargo modificado para el cual se desean calcular las asignaciones. |

## Variables Internas

| Variable | Tipo | Descripción |
| :--- | :--- | :--- |
| `@EANIS` | FLOAT | Almacena la suma total de las asignaciones no imponibles encontradas. |

## Llamadas Internas (Funciones y Vistas)

### Vistas y Tablas Consultadas
*   `[remuneraciones].[RM_ANICargoMod]`: Tabla que almacena las asignaciones no imponibles por cargo. Se consulta para obtener los valores (`Valor`) asociados al `@CODIGOCARGOMOD`.

### Funciones Escalares
Esta función no realiza llamadas a otras funciones escalares de usuario, solo utiliza funciones nativas de agregación y manejo de nulos:
*   `SUM()`: Para sumar los valores encontrados.
*   `ISNULL()`: Para asegurar que el retorno sea 0 en caso de no encontrar registros o que la suma sea nula.

## Lógica de Cálculo
1.  **Consulta y Agregación**:
    *   Se realiza una consulta a la tabla `[remuneraciones].[RM_ANICargoMod]`.
    *   Se filtran los registros que coinciden con el `@CODIGOCARGOMOD`.
    *   Se suman los valores de la columna `Valor`.
2.  **Manejo de Nulos**:
    *   Si no existen registros o la suma es `NULL`, la función `ISNULL` asigna un valor de `0`.
3.  **Asignación**:
    *   El resultado se almacena en la variable `@EANIS`.

## Retorno
*   Devuelve un valor de tipo `FLOAT` que representa la suma total de las asignaciones no imponibles para el cargo consultado.
