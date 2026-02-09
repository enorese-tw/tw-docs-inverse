# Documentación del Procedimiento Almacenado: `[remuneraciones].[__SPRM_CargoMod_DeshacerSolicitud]`

## Objetivo
Este procedimiento almacenado se encarga de **eliminar permanentemente** una solicitud de "Cargo Modificado" y toda su información relacionada. Se utiliza para revertir o cancelar una creación de solicitud.

## Parámetros de Entrada

| Parámetro | Tipo | Descripción |
| :--- | :--- | :--- |
| `@CODIGOSOLICITUD` | VARCHAR(MAX) | Código identificador de la solicitud a eliminar (codificado en Base64). |

## Parámetros de Salida

| Parámetro | Tipo | Descripción |
| :--- | :--- | :--- |
| `@CODE` | VARCHAR(MAX) | Código de estado de la operación (ej. '200'). |
| `@MESSAGE` | VARCHAR(MAX) | Mensaje descriptivo del resultado. |

## Lógica de Procesamiento

1.  **Decodificación del Identificador**:
    *   Utiliza `[TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE]` para obtener el código real de la solicitud desde `@CODIGOSOLICITUD`.
2.  **Eliminación en Cascada (Manual)**:
    *   Elimina registros asociados en `[remuneraciones].[RM_BonosCargoMod]`.
    *   Elimina registros asociados en `[remuneraciones].[RM_ANICargoMod]`.
    *   Elimina el registro principal en `[remuneraciones].[RM_CargosMod]`.
    *   Elimina registros de log en `[remuneraciones].[RM_CargosModLog]`.
3.  **Retorno**:
    *   Establece `@CODE` = '200'.
    *   Establece un mensaje indicando que la eliminación es irreversible.

## Tablas afectadas, si es que hay y con que operaciones

| Tabla | Operación | Descripción |
| :--- | :--- | :--- |
| `[remuneraciones].[RM_BonosCargoMod]` | **DELETE** | Elimina bonos asociados a la solicitud. |
| `[remuneraciones].[RM_ANICargoMod]` | **DELETE** | Elimina haberes no imponibles asociados. |
| `[remuneraciones].[RM_CargosMod]` | **DELETE** | Elimina el registro principal de la solicitud. |
| `[remuneraciones].[RM_CargosModLog]` | **DELETE** | Elimina los logs históricos de la solicitud. |
