# Documentación del Procedimiento Almacenado: `[remuneraciones].[__SPRM_CargoMod_ValidaSeleccionCliente]`

## Objetivo
Este procedimiento almacenado verifica si una solicitud de "Cargo Modificado" específica ya tiene un **cliente asociado**. Es utilizado para validar pasos en el flujo de trabajo donde la asignación del cliente es un prerrequisito.

## Parámetros de Entrada

| Parámetro | Tipo | Descripción |
| :--- | :--- | :--- |
| `@USUARIOCREADOR` | VARCHAR(MAX) | Identificador del usuario (actualmente no utilizado en la lógica interna). |
| `@CODIGOSOLICITUD` | VARCHAR(MAX) | Código de la solicitud a validar. |

## Parámetros de Salida

| Parámetro | Tipo | Descripción |
| :--- | :--- | :--- |
| `@CODE` | VARCHAR(MAX) | Código de resultado: `'200'` si tiene cliente asignado, `'404'` si no. |

## Lógica de Procesamiento

1.  **Consulta de Validación**:
    *   Consulta la vista `[remuneraciones].[View_Solicitudes]`.
    *   Filtra por el `@CODIGOSOLICITUD` proporcionado.
    *   Verifica si la columna `CodigoCliente` no es nula.
2.  **Determinación del Resultado**:
    *   Si la consulta retorna un valor (cliente asignado), establece `@CODE = '200'`.
    *   Si la consulta no retorna valor o es nulo, establece `@CODE = '404'`.

## Tablas afectadas, si es que hay y con que operaciones

Este procedimiento es de **sólo lectura** y no realiza modificaciones (INSERT, UPDATE, DELETE) en la base de datos.
Consulta información de:
*   `[remuneraciones].[View_Solicitudes]`
