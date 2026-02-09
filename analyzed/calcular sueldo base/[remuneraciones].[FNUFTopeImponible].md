# Documentación de la Función: `[remuneraciones].[FNUFTopeImponible]`

## Objetivo
Esta función escalar obtiene el valor del **Tope Imponible** en UF vigente, ya sea para fines previsionales generales (salud y pensiones) o para el Seguro de Cesantía (AFC).

## Parámetros de Entrada

| Parámetro | Tipo | Descripción |
| :--- | :--- | :--- |
| `@TYPE` | VARCHAR(MAX) | Tipo de tope a consultar. Puede ser 'IMP' (Imponible General) o 'AFC' (Seguro de Cesantía). |

## Variables Internas

| Variable | Tipo | Descripción |
| :--- | :--- | :--- |
| `@TOPEIMPONIBLEUF` | FLOAT | Almacena el valor recuperado del tope imponible. |

## Llamadas Internas (Funciones y Vistas)

### Vistas y Tablas Consultadas
*   `[remuneraciones].[RM_Constantes]`: Tabla que almacena las constantes del sistema de remuneraciones. Se consultan las siguientes variables según el tipo:
    *   `C004`: Para el tope imponible general (`@TYPE = 'IMP'`).
    *   `C005`: Para el tope imponible de AFC (`@TYPE = 'AFC'`).

## Lógica de Cálculo
1.  **Selección de Variable**:
    *   Se utiliza una expresión `CASE` en la cláusula `WHERE` para determinar qué código de variable (`CodigoVariable`) buscar en la tabla de constantes.
        *   Si `@TYPE` es 'IMP' -> busca 'C004'.
        *   Si `@TYPE` es 'AFC' -> busca 'C005'.
2.  **Filtrado por Vigencia**:
    *   Se asegura de recuperar solo el valor vigente (`Estado = 'VIG'`).
3.  **Asignación**:
    *   El valor encontrado (`Valor`) se asigna a la variable `@TOPEIMPONIBLEUF`.

## Retorno
*   Devuelve un valor de tipo `FLOAT` que representa el tope imponible en Unidades de Fomento (UF) solicitado.
