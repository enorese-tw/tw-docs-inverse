# Documentación de la Función: `[remuneraciones].[FNPorcentajeAFP]`

## Objetivo
Esta función escalar obtiene el porcentaje de cotización previsional correspondiente a una **AFP** específica y una **Empresa** determinada. Permite recuperar el valor correcto consultando vistas segregadas por empresa.

## Parámetros de Entrada

| Parámetro | Tipo | Descripción |
| :--- | :--- | :--- |
| `@AFP` | VARCHAR(100) | Nombre de la Administradora de Fondos de Pensiones (AFP). |
| `@EMPRESA` | VARCHAR(10) | Identificador de la empresa ('TWEST', 'TWRRHH', 'TWC') para seleccionar la fuente de datos correcta. |

## Variables Internas

| Variable | Tipo | Descripción |
| :--- | :--- | :--- |
| `@PORCENTAJE` | FLOAT | Almacena el valor del porcentaje de cotización recuperado. |

## Llamadas Internas (Funciones y Vistas)

### Vistas Consultadas
*   `[remuneraciones].[View_AfpTWEST]`: Vista de AFPs para la empresa 'TWEST'.
*   `[remuneraciones].[View_AfpTWRRHH]`: Vista de AFPs para la empresa 'TWRRHH'.
*   `[remuneraciones].[View_AfpTWC]`: Vista de AFPs para la empresa 'TWC'.

## Lógica de Cálculo
1.  **Validación de Empresa**:
    *   La función evalúa el parámetro `@EMPRESA` a través de una estructura `IF...ELSE IF`.
2.  **Selección de Datos**:
    *   Dependiendo de la empresa, realiza una consulta `SELECT` a la vista correspondiente.
    *   Filtra por el nombre de la AFP (`WHERE VAFP.Nombre = @AFP`).
    *   Obtiene el campo `CargoModAfpPorc`.
3.  **Asignación**:
    *   El valor obtenido se asigna a la variable `@PORCENTAJE`.

## Retorno
*   Devuelve un valor de tipo `FLOAT` que representa el porcentaje de cotización de la AFP para la empresa indicada.
