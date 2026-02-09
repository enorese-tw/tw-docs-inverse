# [remuneraciones].[__SPRM_CargoMod_CambioEstadoSolicitud]

## Objetivo
Este procedimiento almacenado gestiona el ciclo de vida y los cambios de estado de una solicitud de modificación de cargo. Es el orquestador principal del flujo de aprobación, encargándose de actualizar el estado de la solicitud, notificar a los involucrados por correo electrónico y, en las etapas finales, sincronizar la información con sistemas externos (Softland, Firma Digital).

## Parámetros de Entrada
| Nombre | Tipo | Descripción |
| :--- | :--- | :--- |
| `@USUARIOCREADOR` | `VARCHAR(MAX)` | Usuario que ejecuta la acción (codificado en Base64). |
| `@ESTADO` | `VARCHAR(MAX)` | Nuevo estado al que se desea transicionar (ej. 'APROB', 'RZ', 'FD', 'TER'). |
| `@CODIGOCARGOMOD` | `VARCHAR(MAX)` | Identificador de la solicitud. |
| `@OBSERVACION` | `VARCHAR(MAX)` | Comentarios u observaciones (obligatorio para rechazos). |

## Parámetros de Salida
| Nombre | Tipo | Descripción |
| :--- | :--- | :--- |
| `@CODE` | `VARCHAR(MAX)` | Código de respuesta (ej. '200' éxito, '300' validación fallida). |
| `@MESSAGE` | `VARCHAR(MAX)` | Mensaje detallado (HTML) para el usuario. |
| `@MESSAGESIMPLE` | `VARCHAR(MAX)` | Mensaje de texto plano resumido. |
| `@CODINE` | `VARCHAR(MAX)` | Código interno generado o procesado. |
| `@GLOSA` | `VARCHAR(MAX)` | Glosa o nombre del cargo asociado. |
| `@EMPRESA` | `VARCHAR(MAX)` | Empresa asociada a la solicitud. |

## Flujos por Estado (`@ESTADO`)

### 1. Aprobación y Envío (`APROB`, `PDAPROB`)
- **Acción**: Valida que el cargo tenga sueldo y nombre definidos. Actualiza el estado a 'APROB' o 'PDAPROB' según el perfil (KAM vs Finanzas/Remuneraciones).
- **Notificaciones**: Envía correos de confirmación y pendientes de validación a KAMs, Finanzas o Remuneraciones según corresponda. Utiliza procedimientos auxiliares para generar el HTML (`__SPRM_CargoMod_Email_...`).

### 2. Rechazo (`RZ`)
- **Acción**: Devuelve la solicitud al estado 'CREATE' o 'PDAPROB' (para corrección). Registra el motivo del rechazo.
- **Notificaciones**: Informa al creador y a los responsables (KAM, Finanzas) sobre el rechazo y la razón (`@OBSERVACION`).

### 3. Rechazo de Remuneraciones (`RZR`)
- **Acción**: Rechazo específico desde el área de remuneraciones hacia etapas anteriores. Limpia tablas de estructura de renta (`RM_EstructuraRentaBase`, `RM_EstructuraBase...`) para reiniciar el cálculo/definición.
- **Notificaciones**: Similar al rechazo estándar pero notificando el retorno a Finanzas.

### 4. Firma Digital / Creación Softland (`FD`)
- **Acción**:
    - Genera un código alfanumérico único (`__SPRM_CargoMod_Code_AlfaNum`).
    - Actualiza el código definitivo en todas las tablas relacionadas (`RM_CargosMod`, `RM_BonosCargoMod`, etc.).
    - Inserta el nuevo cargo en tablas de integración (`TW_ACCESS_EST..cargomod` o `TW_ACCESS_OUT..cargomod` y `KM_CargosBPO`).
- **Notificaciones**: Informa que la solicitud está pendiente de firma digital.

### 5. Terminado (`TER`)
- **Acción**: Marca el proceso como finalizado.
- **Notificaciones**: Envía correo final de término a los involucrados.

## Llamadas Internas Relevantes
- **Correos**: `msdb.dbo.sp_send_dbmail`
- **Generación de Contenido Email**:
    - `[remuneraciones].[__SPRM_CargoMod_Email_EnvioValidacion]`
    - `[remuneraciones].[__SPRM_CargoMod_Email_Rechazo]`
    - `[remuneraciones].[__SPRM_CargoMod_Email_PendienteFD]`
    - `[remuneraciones].[__SPRM_CargoMod_Email_Terminado]`
    - Entre otros.
- **Utilidades**:
    - `[remuneraciones].[__SPRM_CargoMod_Code_AlfaNum]`: Generador de códigos.
    - `[dbo].[FNBase64Decode]`: Decodificación.

## Tablas Afectadas
Múltiples tablas son actualizadas dependiendo del estado, incluyendo:
- `[remuneraciones].[RM_CargosMod]` (Estado, códigos, observaciones)
- `[remuneraciones].[RM_EstructuraRentaBase]`
- `[remuneraciones].[RM_CargosModLog]`
- Tablas de detalles de estructura (`RM_EstructuraBaseH`, `RM_EstructuraBaseD`, etc.)
- Tablas de integración (`TW_ACCESS...`, `KM_CargosBPO`).
