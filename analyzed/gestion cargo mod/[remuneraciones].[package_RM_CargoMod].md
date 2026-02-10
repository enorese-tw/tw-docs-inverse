# Documentación del Procedimiento Almacenado: `[remuneraciones].[package_RM_CargoMod]`

## Objetivo
Este procedimiento almacenado actúa como un **Dispatcher** o **Controlador Central** para la gestión de "Cargos Modificados". Su función principal es recibir una instrucción a través del parámetro `@OPCION` y los datos necesarios, para luego enrutar la ejecución hacia procedimientos almacenados más específicos (`__SPRM_CargoMod_...`). Centraliza el manejo de transacciones y errores para múltiples operaciones.

## Parámetros de Entrada

| Parámetro | Tipo | Descripción |
| :--- | :--- | :--- |
| `@OPCION` | VARCHAR(MAX) | **Obligatorio**. Determina la acción a ejecutar (ej. 'CrearSolicitud', 'ActualizaSueldo', etc.). |
| `@SUELDO` | VARCHAR(MAX) | Monto del sueldo (usado en actualizaciones de sueldo o cambios de input). |
| `@EMPRESA` | VARCHAR(MAX) | Identificador de la empresa. |
| `@CLIENTE` | VARCHAR(MAX) | Identificador del cliente. |
| `@CARGOMOD` | VARCHAR(MAX) | (No utilizado explícitamente en el cuerpo visible, posible deprecación o uso futuro). |
| `@NOMBRECARGO` | VARCHAR(MAX) | Nuevo nombre para actualización de cargo. |
| `@GRATIFICACION` | VARCHAR(MAX) | Monto o indicador de gratificación. |
| `@TYPEGRATIF` | VARCHAR(MAX) | Tipo de gratificación. |
| `@OBSERVGRATICONV` | VARCHAR(MAX) | Observaciones sobre gratificación convenida. |
| `@USUARIOCREADOR` | VARCHAR(MAX) | Usuario que realiza la acción (para auditoría o control). |
| `@PAGINATION` | VARCHAR(MAX) | Parámetros de paginación para listados. |
| `@TYPEFILTER` | VARCHAR(MAX) | Tipo de filtro para búsquedas. |
| `@DATAFILTER` | VARCHAR(MAX) | Dato del filtro para búsquedas. |
| `@CODIGOSOLICITUD` | VARCHAR(MAX) | Identificador principal de la solicitud de cargo sobre la cual se opera. |
| `@AFP` | VARCHAR(MAX) | Nombre de la AFP. |
| `@TYPESUELDO` | VARCHAR(MAX) | Tipo de sueldo (ej. 'SB', 'SL'). |
| `@EXCINC` | VARCHAR(MAX) | Parametro para inclusión/exclusión (ExcInc). |
| `@CODIGOVAR` | VARCHAR(MAX) | Código de variable para configuraciones. |
| `@ESTADO` | VARCHAR(MAX) | Estado para actualizaciones de flujo/wizards. |
| `@OBSERVACIONES` | VARCHAR(MAX) | Texto de observaciones. |
| `@HORARIOS` | VARCHAR(MAX) | Cadena de configuración de horarios. |
| `@VALORDIARIO` | VARCHAR(MAX) | Valor diario (para jornada parcial). |
| `@HORASEMANALES` | VARCHAR(MAX) | Horas semanales. |
| `@TYPE` | VARCHAR(MAX) | Tipo genérico (usado en creación, observaciones, etc.). |
| `@DIAS` | VARCHAR(MAX) | Configuración de días semanales. |

## Opciones Disponibles y Llamadas Internas

El SP evalúa `@OPCION` y ejecuta lo siguiente:

| Opción (`@OPCION`) | Procedimiento Llamado | Descripción Corta |
| :--- | :--- | :--- |
| `CrearSolicitud` | `__SPRM_CargoMod_CrearSolicitud` | Crea una nueva solicitud de cargo. |
| `DeshacerSolicitud` | `__SPRM_CargoMod_DeshacerSolicitud` | Revierte una solicitud existente. |
| `Solicitudes` | `__SPRM_CargoMod_Solicitudes` | Lista solicitudes (con filtros/paginación). |
| `ValidaSeleccionCliente` | `__SPRM_CargoMod_ValidaSeleccionCliente` | Valida si un cliente ha sido seleccionado. |
| `Clientes` | `__SPRM_CargoMod_Clientes` | Lista clientes disponibles. |
| `PaginationClientes` | `[dbo].[SP_TW_GET_PAGINATIONS]` | Obtiene metadatos de paginación para clientes. |
| `ActualizaCliente` | `__SPRM_CargoMod_ActualizaCliente` | Actualiza el cliente asignado a la solicitud. |
| `ActualizaNombreCargo` | `__SPRM_CargoMod_ActualizaNombreCargo` | Actualiza el nombre del cargo. |
| `ActualizaSueldo` | `__SPRM_CargoMod_ActualizaSueldo` | Actualiza el sueldo base/líquido y tipo. |
| `ActualizaSueldoLiquido` | `__SPRM_CargoMod_ActualizaSueldo` | Actualiza específicamente forzando tipo 'SL'. |
| `ActualizaGratificacion` | `__SPRM_CargoMod_ActualizaGratificacion` | Configura la gratificación. |
| `Dashboard` | `__SPRM_CargoMod_Dashboard` | Obtiene datos para el dashboard de usuario. |
| `HeaderEstructura` | `__SPRM_CargoMod_HeaderEstructura` | Obtiene cabeceras de estructura de remuneración. |
| `HaberesEstructura` | `__SPRM_CargoMod_HaberesEstructura` | Obtiene estructura de haberes. |
| `DescuentosEstructura` | `__SPRM_CargoMod_DescuentosEstructura` | Obtiene estructura de descuentos. |
| `MargenProvisionEstructura` | `__SPRM_CargoMod_ProvMargenEstructura` | Obtiene estructura de provisiones y margen. |
| `AFP` | `__SPRM_CargoMod_AFP` | Lista AFPs disponibles por empresa. |
| `ActualizarAFP` | `__SPRM_CargoMod_ActualizarAFP` | Actualiza la AFP seleccionada. |
| `CambiarInputSueldo` | `__SPRM_CargoMod_CambiarInputSueldo` | Modifica el monto de sueldo ingresado. |
| `CambiarCalculoTypeSueldo` | `__SPRM_CargoMod_CambiarCalculoTypeSueldo` | Cambia el método de cálculo del sueldo. |
| `CambiarProvMargGastoExcInc` | `__SPRM_CargoMod_CambiarProvMargGastoExcInc` | Configura inclusión/exclusión en provisiones/gastos. |
| `CambiarTypeJornada` | `__SPRM_CargoMod_CambiarTypeJornada` | Cambia el tipo de jornada laboral. |
| `CambioEstadoSolicitud` | `__SPRM_CargoMod_CambioEstadoSolicitud` | Avanza o cambia el estado de la solicitud en el flujo. |
| `ValidaStageActual` | `__SPRM_CargoMod_ValidaStageActual` | Valida el estado/etapa actual. |
| `ActualizaValorDiaPT` | `__SPRM_CargoMod_ActualizaValorDiaPT` | Actualiza valor día para Part-Time. |
| `ActualizaObservaciones` | `__SPRM_CargoMod_ActualizaObservaciones` | Agrega o modifica observaciones. |
| `EliminarObservaciones` | `__SPRM_CargoMod_EliminarObservaciones` | Elimina observaciones. |
| `ValidaCadenaObservaciones` | `__SPRM_CargoMod_ValidaCadenaObservaciones` | Valida integridad/formato de observaciones. |
| `ActualizaHorario` | `__SPRM_CargoMod_ActualizaHorario` | Actualiza configuración de horarios. |
| `ActualizaJornadaFullTime` | `__SPRM_CargoMod_ActualizaJornadaFullTime` | Configura jornada Full Time. |
| `ListObservaciones` | `__SPRM_CargoMod_ListObservaciones` | Lista historial de observaciones. |
| `ActualizaWizards` | `__SPRM_CargoMod_ActualizaWizards` | Actualiza estado de asistentes (wizards) de UI. |
| `ValidaStageWizards` | `__SPRM_CargoMod_ValidaStageWizards` | Valida etapas de wizards. |
| `ActualizaDiasSemanales` | `__SPRM_CargoMod_ActualizaDiasSemanales` | Configura días de trabajo semanal. |
| `HistorialCargoMod` | `__HistorialCargoMod` | Obtiene el historial de cambios. |


## Variables Internas y Manejo de Errores

*   **Transacciones**: Todo el bloque está envuelto en `BEGIN TRANSACTION` y `COMMIT TRANSACTION`. Si ocurre un error, se ejecuta `ROLLBACK TRANSACTION` en el bloque `CATCH`.
*   **Retornos**: Algunos bloques devuelven datasets con columnas estandarizadas como `Code`, `Message`, `Codine` para que el frontend o capa de aplicación procese la respuesta uniformemente.
