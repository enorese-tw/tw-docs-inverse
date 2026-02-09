# [remuneraciones].[__SPRM_CargoMod_AFP]

## Objetivo
Este procedimiento almacenado tiene como objetivo obtener el listado de nombres de AFPs (Administradoras de Fondos de Pensiones) disponibles, filtrando la consulta según la empresa especificada.

## Parámetros de Entrada
| Nombre | Tipo | Descripción |
| :--- | :--- | :--- |
| `@EMPRESA` | `VARCHAR(MAX)` | Identificador de la empresa para la cual se requiere consultar las AFPs. Valores esperados: 'TWEST', 'TWRRHH', 'TWC'. |

## Variables Internas
No se declaran variables internas adicionales en el cuerpo del procedimiento.

## Llamadas Internas (Vistas)
El procedimiento consulta las siguientes vistas dependiendo del valor del parámetro `@EMPRESA`:
- `[remuneraciones].[View_AfpTWEST]` (Si @EMPRESA = 'TWEST')
- `[remuneraciones].[View_AfpTWRRHH]` (Si @EMPRESA = 'TWRRHH')
- `[remuneraciones].[View_AfpTWC]` (Si @EMPRESA = 'TWC')

## Lógica de Cálculo
El flujo lógico es una estructura condicional `IF...ELSE IF` basada en el parámetro `@EMPRESA`:
1.  **Si `@EMPRESA` es 'TWEST'**: Selecciona el campo `Nombre` de la vista `[remuneraciones].[View_AfpTWEST]`.
2.  **Si `@EMPRESA` es 'TWRRHH'**: Selecciona el campo `Nombre` de la vista `[remuneraciones].[View_AfpTWRRHH]`.
3.  **Si `@EMPRESA` es 'TWC'**: Selecciona el campo `Nombre` de la vista `[remuneraciones].[View_AfpTWC]`.
4.  La transacción se maneja dentro de un bloque `TRY...CATCH` para asegurar la integridad, aunque es solo lectura. Se hace `COMMIT` al finalizar o `ROLLBACK` en caso de error.

## Retorno
Retorna un conjunto de resultados con una única columna:
- `Nombre`: El nombre de la AFP.

## Tablas Afectadas
No se realizan operaciones de modificación (INSERT, UPDATE, DELETE) en ninguna tabla. Solo se realizan operaciones de lectura (SELECT).
