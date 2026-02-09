# Documentación del Procedimiento Almacenado: `[remuneraciones].[__SPRM_CargoMod_ActualizaNombreCargo]`

## Objetivo
Este procedimiento se encarga de **actualizar el nombre del cargo (función)** o el **nombre del "Cargo Mod"** (glosa técnica) asociado a una solicitud.
Cuando se actualiza el nombre del cargo base ('CF'), el sistema **sugiere y asigna automáticamente** un nombre para el Cargo Mod basándose en reglas de negocio (jornada, sueldo, cliente). También permite actualizar directamente el nombre del Cargo Mod ('CM').

## Parámetros de Entrada

| Parámetro | Tipo | Descripción |
| :--- | :--- | :--- |
| `@USUARIOCREADOR` | VARCHAR(MAX) | ID del usuario que realiza la actualización. |
| `@CODIGOSOLICITUD` | VARCHAR(MAX) | Código de la solicitud de modificación de cargo (codificado en Base64). |
| `@NOMBRECARGO` | VARCHAR(MAX) | Nuevo nombre a asignar (Cargo Función o Cargo Mod). |
| `@TYPE` | VARCHAR(MAX) | Tipo de actualización: 'CF' (Cargo Función), 'CM' (Cargo Mod). |
| `@CODE` | VARCHAR(MAX) OUTPUT | Código de retorno (200). |
| `@MESSAGE` | VARCHAR(MAX) OUTPUT | Mensaje descriptivo del resultado. |

## Variables Internas

| Variable | Tipo | Descripción |
| :--- | :--- | :--- |
| `@_PT` | BIT | Indicador de jornada Part-Time calculada. |
| `@_SD` | BIT | Indicador de sueldo diario calculado. |
| `@_CLIENTE` | VARCHAR(3) | Identificador del cliente asociado. |
| `@_CODIGOCARGOMOD` | VARCHAR(100) | ID decodificado de la solicitud. |
| `@_SUGERENCIANOMBRE` | VARCHAR(30) | Nombre sugerido automáticamente para el Cargo Mod. |

## Lógica de Procesamiento

1.  **Inicio de Transacción**.
2.  **Decodificación**: Obtiene el ID real de la solicitud (`@_CODIGOCARGOMOD`).
3.  **Actualización Cargo Función ('CF')**:
    *   Consulta la configuración actual (Jornada, Tipo Sueldo, Cliente) para determinar reglas de sugerencia.
    *   Genera un nombre sugerido (`@_SUGERENCIANOMBRE`) usando `[remuneraciones].[FNSugerirNombreCargoMod]`.
    *   Actualiza `[remuneraciones].[RM_CargosMod]`:
        *   `NombreCargo`: Asigna el nombre ingresado (en mayúsculas).
        *   `CargoMod`: Asigna el nombre sugerido, formateado según el tipo de solicitud ('E' structurada o 'S' simple) y limpiado de tildes.
    *   Define mensaje informativo sobre la asignación automática.
4.  **Actualización Cargo Mod ('CM')**:
    *   Actualiza `[remuneraciones].[RM_CargosMod]`:
        *   `CargoMod`: Asigna el nombre ingresado, aplicando formato (glosa vs nombre directo) y limpieza de tildes/mayúsculas.
    *   Define mensaje de éxito.
5.  **Finalización**:
    *   Establece `@CODE = '200'`.
    *   Confirma la transacción (`COMMIT`) o realiza `ROLLBACK` en error.

## Tablas afectadas, si es que hay y con que operaciones

*   `[remuneraciones].[RM_CargosMod]` (UPDATE): Actualiza nombre del cargo, glosa del cargo mod y metadatos.
