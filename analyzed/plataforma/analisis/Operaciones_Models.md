# Documentación de Modelos - Capa Operaciones

Esta documentación detalla los modelos encontrados en la capa de `AplicacionOperaciones/Models` y sus subdirectorios (`Finanzas`, `Finiquitos`).
Los modelos en esta capa actúan principalmente como **DTOs (Data Transfer Objects)** y **View Models**, transportando datos entre los controladores y las vistas, o estructurando respuestas de APIs.
Se observa una fuerte convención de nomenclatura para propiedades de control de UI (prefijo `Opt`, `Html`, `Renderizado`), lo que indica que **la lógica de presentación reside parcialmente en estos modelos**.

## Índice
1. [Modelos Raíz](#modelos-raíz)
2. [Subdirectorio Finanzas](#subdirectorio-finanzas)
3. [Subdirectorio Finiquitos](#subdirectorio-finiquitos)

---

## Modelos Raíz
Ubicación: `AplicacionOperaciones/Models/`

### Accesos
Modelo de respuesta estandarizada para solicitudes de permisos o acceso.
- **Uso**: Retorna el resultado de validar si un usuario puede acceder a un recurso.
- **Datos de Salida**: `Code`, `Message`, `CodeAction` (acción a tomar), `PreController`/`Controller` (redirección), `Titulo`.

### Anexos
Modelo para la visualización y gestión de archivos anexos en la UI.
- **Lógica UI**: Contiene propiedades pre-renderizadas (`RenderizadoTituloOne`, `ButtonClass`) lo que sugiere que el HTML o clases CSS se calculan en el servidor antes de llenar el modelo.
- **Datos**: `Path` (ruta del archivo), `Code`/`Message` (estado).

### Banco
Maestro de entidades bancarias.
- **Datos**: `Id`, `Nombre`, `Vigente`.

### CargaMasiva
Modelo complejo para gestionar la interfaz de Carga Masiva de datos.
- **Lógica UI**: Controla exhaustivamente la apariencia de la vista de carga y descarga (`UploadRenderizadoColor`, `DownloadRenderizadoGlyphicon`).
- **Lógica de Negocio**: Diferencia entre proceso de `Upload` (carga) y `Download` (descarga de resultados/errores), con propiedades específicas para cada flujo (`UploadNombrePlantilla`, `DownloadCifrado`).

### Cliente
Representa la entidad Cliente en el sistema.
- **Datos Clave**: `Rut`, `NombreCliente`, `Empresa`, `Kam` (Ejecutivo asignado).
- **Relaciones**: Vinculado a `Empresa` y `Kam`.

### ClientesAsignados
Modelo de relación (Link Table representation) entre Ejecutivos y Clientes.
- **Datos**: `IdCliente`, `Id` (del ejecutivo), `Nombre`/`Apellido` (del ejecutivo o cliente según contexto).

### ConceptoGasto
Maestro de tipos de gastos.
- **Datos**: `Id`, `Nombre`, `Descripcion`.

### Cuentas
Representación de cuentas asociadas a empresas.
- **Datos**: `Codigo`, `Empresa`, `ColorEvent` (para distinción visual).

### DatosBaja
Modelo extenso que representa la información y estado de una solicitud de Baja (Término de Contrato).
- **Datos del Trabajador**: `NombreTrabajador`, `Rut`, `Ficha`, `CargoReal`, `FechaInicio`, `FechaTermino`.
- **Datos de la Baja**: `Causal` (motivo legal), `ObservacionesCausal`, `ComentariosSolicitud`.
- **Lógica de Estado y UI**:
  - `Estado`: Estado actual del flujo.
  - **Opciones (Lógica de Negocio)**: Múltiples propiedades booleanas/string (`Opt...`) determinan las acciones permitidas para el usuario:
    - `OptSgteEtapa`: Avanzar flujo.
    - `OptCalcular`: Permitir cálculo de finiquito.
    - `OptViewDocFiniquito`/`OptDescargarDocFiniquito`: Acceso a documentos generados.
    - `OptMoverAFiniquito`: Transición de Baja a proceso de Finiquito.
    - `OptRechazar`: Opción de rechazo.

### EjecutivosAsignados
Modelo para mostrar la jerarquía o asignación de un KAM.
- **Datos**: `JefeOperaciones` (superior), `Kam` (el ejecutivo), `Nombre`, `Apellido`.

### Empresa
Maestro de empresas.
- **Datos**: `Id`, `Nombre`, `Codigo`, `Vigente`.

### Error
Modelo genérico para transporte de errores.
- **Datos**: `Code`, `Message`.

### EstadisticaKam
Modelo para Dashboard de indicadores de gestión (KPIs) del KAM.
- **Métricas**: `TotalSolicitudes`, `TotalSolContratos`, `TotalProcesos`.
- **Cálculos**: Incluye porcentajes pre-calculados (`PercentageSolContratos`, `PercentageProcRenovaciones`).

### EstadisticaKamEmpresa
Desglose detallado de estadísticas por empresa y nivel de servicio (SLA/Estado).
- **Segmentación**: Clasifica los datos en categorías de negocio:
  - **PCS**: Proceso normal.
  - **CRITICO**: Fuera de plazo crítico.
  - **URGENTE**: Próximo a vencer.
  - **NORMAL**, **SP** (Sin Prioridad?), **ANULADA**, **ERROR**.
- **Visualización**: Para cada categoría incluye `Glyphicon`, `BarColor`, `Concepto`, indicando que la lógica de semaforización semántica viene del servidor.

### EstadisticaTotal
Modelo de resumen estadístico consolidado.
- **Datos**: `Total`, `Twest`, `Twrrhh`, `Twc`, `Analista`. Desglosa totales por área responsable.

### EstadisticaXContratos
Modelo comparativo de gestión de Contratos vs Renovaciones.
- **Estructura**: Separa métricas para "Concepto C" (Contratos) y "Concepto R" (Renovaciones).
- **Métricas**: `Solicitado`, `Terminado`, `Total` y desglose por área (`Twest`, `Twrrhh`, `Twc`).

### Estado
Maestro de estados del sistema.
- **Datos**: `Codigo`, `Descripcion`, `Vigente`.

### Gasto
Modelo principal para el flujo de rendición y aprobación de gastos.
- **Datos Financieros**: `MontoNeto`, `MontoAprobado`, `IVA`.
- **Datos de Negocio**: `Proveedor`, `RutProveedor`, `TipoDocumento` (Boleta/Factura), `CentroCosto` (implícito en relaciones), `Cliente`, `Empresa`.
- **Flujo de Aprobación**: `Aprobador`, `FechaAprobacion`, `EstadoGasto`.
- **Auditoría**: `FechaRendicion`, `FechaPublicacion`, `FechaUltimoCambio`, `NombreUsuario`.
- **Opciones UI**: `OptBorrar`, `OptModificar`.

### HeaderProcesos
Modelo para renderizar el encabezado de la bandeja de procesos.
- **UI**: Transporta HTML pre-renderizado (`HtmlRenderizado`, `HtmlPagination`, `HtmlSearchElement`).

### HeaderSolicitud
Modelo para renderizar el encabezado de la bandeja de solicitudes.
- **UI**: Similar al de procesos, agrega datos contextuales (`Estado`, `EjecutivoKam`, `Cliente`) y elementos visuales (`Glyphicon`, `GlyphiconColor`).

### Historial
Entrada de registro de auditoría o traza de cambios.
- **Datos**: `Fecha`, `Usuario`, `Estado` (nuevo estado), `Comentario`.

### Kam
Información de un Key Account Manager y su cartera.
- **Datos**: Identificación básica.
- **Relación**: Contiene una lista de objetos `Cliente` (`public List<Cliente> clientes`).

### Modal
Datos de configuración para ventanas modales (probablemente Bootstrap o jQuery UI).
- **Datos**: `UpdateTargetId` (ID del DOM a actualizar), `OnSuccess` (callback JS).

### MotivosAnulacion
Maestro de causas de anulación.
- **Datos**: `Descripcion`.

### Option
Modelo genérico para elementos de selección o menús en la UI.
- **Datos UI**: `Glyphicon`, `GlyphiconColor`, `RenderizadoFirstTexto`.

### Pagination
Modelo robusto para gestionar paginación de grillas/listas.
- **Datos de Estado**: `NumeroPagina`, `TotalItems`, `FirstItem`, `LastItem`.
- **Renderizado**: Transporta bloques completos de HTML para la navegación (`HtmlPagination`, `HtmlSearchElement`), indicando generación de UI en servidor.

### Periodo
Maestro de periodos operativos (ej. mes comercial).
- **Datos**: `PeriodoVigente`, `FechaApertura`, `FechaCierre`.

### PlantillasCargaMasiva
Definición de estructuras para importación de datos.
- **Estructura**: Define cómo mapear un archivo (`Columnas`) a una estructura jerárquica (`NodoPadre`, `NodoHijo`).

### Prioridades
Maestro de niveles de prioridad de solicitudes.
- **Datos UI**: Define colores y estilos asociados a cada prioridad (`ColorFont`, `BorderColor`).

### Proceso
Entidad central que agrupa flujos operativos.
- **Identificación**: `CodigoTransaccion`, `NombreProceso`, `TipoEvento`.
- **Lógica de Negocio (Opciones)**: Define las operaciones disponibles sobre el proceso mediante un extenso set de propiedades `Opt...`:
  - `OptAsignarProceso`: Asignación a analista.
  - `OptTerminarProceso` / `OptAnularProceso`: Cierre de ciclo.
  - `OptRevertirTermino` / `OptRevertirAnulacion`: Reversa de estados finales (Capacidad administrativa crítica).
  - `OptDescargarDatosCargados`: Exportación de datos.

### Proveedor
Maestro de proveedores.
- **Datos**: `Rut`, `Nombre`, `Vigente`.

### RenderHtml
Modelo auxiliar para inyección de HTML dinámico.
- **Uso**: Permite envolver contenido (`TextoGenerado`) en etiquetas dinámicas (`EtiquetaApertura`, `EtiquetaCierre`).

### Solicitud
Entidad central para solicitudes de servicio (Contratos, Renovaciones, Bajas, etc.).
- **Identificación**: `CodigoSolicitud`, `TipoEvento`, `NombreSolicitud`.
- **SLA**: `FechasCompromiso`, `Prioridad`.
- **Lógica de Negocio (Opciones e Integración)**:
  - **Integraciones**: `IntegrateSimulate`, `IntegrateSettlement` (indica conexión con sistemas de cálculo/finiquito).
  - **Opciones Contrato/Renovación**: `OptAsignarSolicitud`, `OptDescargarSolicitudContratoIndividual`.
  - **Opciones Baja**: `OptCalcular`, `OptLiquidacionSueldo`, `OptSimulacion`, `OptRevertirCalculo`.
  - **Anulación**: `OptAnularKam`, `OptAnularSolicitud`.

### SolicitudContrato
Modelo detallado con TODOS los datos necesarios para generar un contrato laboral.
- **Datos Personales**: Completos (Rut, Nombre, Dirección, CivilState, etc.).
- **Datos Previsionales**: `Afp`, `Salud`, `MontoIsapre`.
- **Datos de Pago**: `Bank`, `BankAccount`.
- **Datos Contractuales**: `StartDate`, `CargoReal`, `Sueldo` (implícito en condiciones), `TipoContrato`, `HorasTrab`, `Colacion`.
- **Datos de Negocio**: `IdCliente`, `Empresa`, `BusinessCenter`, `EjecutivoCarga`.

### SolicitudRenovacion
Modelo específico para renovaciones de contrato.
- **Datos**: Se centra en la extensión de plazos (`FechaInicioRenov`, `FechaTerminoRenov`) y la causa (`Causal`), manteniendo referencia al empleado (`Rut`, `Ficha`).

### SolicitudRenovacion
(Nota: Hay duplicidad de nombre en la lista de archivos o análisis, pero la descripción corresponde al archivo analizado).

### Tipos (Maestros)
- **TipoCuenta**: Tipos de cuenta bancaria.
- **TipoDocumento**: Tipos de documentos tributarios/legales (`IdSII`).
- **TipoReembolso**: Clasificación de reembolsos.

---

## Subdirectorio Finanzas
Ubicación: `AplicacionOperaciones/Models/Finanzas/`
Modelos enfocados en la tesorería y gestión de pagos masivos o individuales.

### Cheques
Datos para emisión o registro de cheques.
- **Datos**: `Folio`, `Monto`, `Rut`, `Beneficiario` (`Nombres`), `CodigoPago`.

### CuentasOrigen
Cuentas bancarias de la empresa pagadora.
- **Datos**: `Banco`, `NumeroCuenta`, `Glosa`.

### DatosDespacho
Información logística para envío de documentos/pagos.
- **Datos**: `Sucursal`, `Direccion`, `Base64Barcode` (etiqueta de despacho).

### DatosProceso
Resumen económico de un proceso de pago.
- **Datos**: `MontoTotal`, `Cantidad` (de pagos), `Cifra`.

### Despacho
Detalle de un envío específico.
- **Datos**: `Beneficiario`, `Monto`, `Contenido` (tipo de doc).

### IndiceCheque
Resumen de estado de chequera o lote.
- **Métricas**: `Disponibles`, `Emitidos`, `Totales`.

### Pagos
Registro de una transacción de pago.
- **Datos**: `Destinatario`, `Monto`, `FechaTransaccion`, `Folio`, `CodigoPago`.

### PlantillaCheque
Configuración para la impresión física de cheques.
- **Coordenadas/Datos**: `Dia`, `Mes`, `Year`, `CifraFirst`, `CifraSecond`, `Base64Barcode`.

### SolicTEF
Solicitud de Transferencia Electrónica de Fondos.
- **Datos**: `CuentaOrigen`, `CuentaDestino`, `MontoTotal`.
- **Lógica de Acción**: `OptPagar`, `OptNotariar`, `OptIncluir`/`OptExcluir` (selección para lotes).

### Sucursal
Maestro simplificado de sucursales.
- **Datos**: `Nombre`, `Direccion`.

### Transferencia
Detalle de una transferencia realizada o por realizar.
- **Desglose Montos**: `TotalFiniquito`, `GastoAdministrativo`, `TotalPago`.
- **Estado**: `Notariado` (flag), `CodigoTEF`.
- **Opciones**: `OptRevertirIncorporacionTefPago` (lógica de reversa compleja), `OptNotariado`.

### ValeVista
Similar a Transferencia pero para pagos vía Vale Vista.
- **Datos**: Idéntica estructura base que Transferencia, cambia `CodigoTEF` por `CodigoVV`.

---

## Subdirectorio Finiquitos
Ubicación: `AplicacionOperaciones/Models/Finiquitos/`
Modelos específicos para el módulo de cálculo y pago de finiquitos.

### Cheque (Finiquitos)
Versión simplificada del cheque para este contexto.
- **Datos**: `Nombres`, `Rut`, `MontoTotal`, `CodigoPago`.

### Dashboard
KPIs operativos exclusivos de finiquitos.
- **Estados de Flujo**: `PendientesVerificacion`, `PendientesConfirmacion`.

### Finiquitos
Modelo principal de la gestión de Finiquitos (Settlements). Es uno de los modelos con mayor carga de lógica de control de flujo.
- **Datos del Caso**: `Folio`, `CodigoFiniquito`, `Causal`, `TotalFiniquito`.
- **Gestión Documental**: `ViewCaratula`, `DownloadDocumento`, `ViewCarta`. Propiedades duplicadas como `OptView...` indican control de acceso a estas acciones.
- **Flujo de Pago y Aprobación**:
  - `ConfirmarPago`, `OptAnularPago`.
  - `OptVerificarPago`: Paso de verificación previo al pago.
  - `OptEnviarProcesoPago`: Integración final con tesorería.
- **Opciones y Reversas**: Gran granularidad en acciones de reversa (`OptRevertirConfirmacion`, `OptRevVerificarPago`, `OptRevEnvioProcesoPago`) indicando un flujo transaccional complejo y reversible paso a paso.

### SolicitudTransferencia
Detalle para solicitar pago de finiquito vía transferencia.
- **Datos Bancarios**: `Banco`, `Cuenta`, `TipoDeposito`.
- **Datos de Pago**: `TotalFiniquito`, `GastoAdministrativo`, `TotalPago`.

### ValeVista (Finiquitos)
Detalle para solicitar pago vía Vale Vista.
- **Estructura**: Similar a SolicitudTransferencia pero orientado a retiro en sucursal/banco (`Location`, `Documento`).
