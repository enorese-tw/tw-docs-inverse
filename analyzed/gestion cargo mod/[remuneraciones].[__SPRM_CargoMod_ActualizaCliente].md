# Documentación del Procedimiento Almacenado: `[remuneraciones].[__SPRM_CargoMod_ActualizaCliente]`

## Objetivo
Este procedimiento se encarga de **actualizar el cliente** asignado a una solicitud de "Cargo Modificado". Al cambiar el cliente, también **reinicia la glosa del Cargo Mod** usando el código del nuevo cliente como prefijo.

## Parámetros de Entrada

| Parámetro | Tipo | Descripción |
| :--- | :--- | :--- |
| `@USUARIOCREADOR` | VARCHAR(MAX) | ID del usuario que realiza la actualización. |
| `@CODIGOSOLICITUD` | VARCHAR(MAX) | Código de la solicitud de modificación de cargo (codificado en Base64). |
| `@CLIENTE` | VARCHAR(MAX) | Nuevo código de cliente a asignar. |
| `@CODE` | VARCHAR(MAX) OUTPUT | Código de retorno de la operación (200). |

## Variables Internas

No se declaran variables internas adicionales en este procedimiento.

## Lógica de Procesamiento

1.  **Inicio de Transacción**.
2.  **Actualización**:
    *   Decodifica el ID de la solicitud usando `[TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE]`.
    *   Actualiza `[remuneraciones].[RM_CargosMod]`:
        *   `Cliente`: Asigna el nuevo código de cliente.
        *   `CargoMod`: Reinicia el nombre del cargo mod con el formato `@CLIENTE + ' '`.
        *   Actualiza fecha, usuario y comentario de auditoría.
3.  **Finalización**:
    *   Establece `@CODE = '200'`.
    *   Confirma la transacción (`COMMIT`).
    *   En caso de error, realiza `ROLLBACK`.

## Tablas afectadas, si es que hay y con que operaciones

*   `[remuneraciones].[RM_CargosMod]` (UPDATE): Actualiza la asignación de cliente y reinicia la glosa del cargo mod.
