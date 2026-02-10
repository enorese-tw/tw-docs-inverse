# Análisis de Vistas - Capa Operaciones

Este documento detalla el análisis de las vistas (`.cshtml`) dentro de la capa `AplicacionOperaciones`. Se agrupan por carpetas funcionales.

## Finanzas

### `AdministrarChequera.cshtml`
*   **Propósito**: Gestionar chequeras digitales y cuentas bancarias para transferencias.
*   **Funcionalidad Clave**:
    *   Formulario para agregar cheques a un talonario digital (`ValesVista`).
    *   Visualización de información de cuenta bancaria (`DatosCuenta`).
    *   Uso de `Ajax.BeginForm` para envíos asíncronos.
    *   Renderizado de vistas parciales: `_TalonarioDigital`, `_InfoCtaBanco`.
*   **Datos de Entrada/Salida**:
    *   Input: `Monto`, `Serie`, `Banco`, `FechaEmision` (para cheque).
    *   Output: Actualización de parciales `Talonario` y `DatosCuenta`.
*   **Dependencias**: Bundles `~/bundles/jqueryval`, `~/bundles/administrarchequera`.

### `AdministrarGastos.cshtml`
*   **Propósito**: Administración y reporte de gastos.
*   **Funcionalidad Clave**:
    *   Filtrado de gastos por Código, N° Documento o Periodo.
    *   Botón para generar reporte ("Descargar Reporte").
    *   Tabla de resultados renderizada en parcial `_AdministrarGastos`.
*   **Datos de Entrada/Salida**:
    *   Input: Filtros de búsqueda.
    *   Output: Lista de gastos en tabla.
*   **Dependencias**: `Finanzas/AdministrarGastos` (Action).

### `AdministrarProveedor.cshtml`
*   **Propósito**: Gestión de proveedores.
*   **Funcionalidad Clave**:
    *   Búsqueda de proveedores (código comentado para formulario de búsqueda).
    *   Formulario para agregar nuevo proveedor: Rut, Nombre, Giro, Dirección, etc.
    *   Listado de proveedores en parcial `_AdministrarProveedor`.
*   **Datos de Entrada/Salida**:
    *   Input: Datos del proveedor (`Proveedor`).
    *   Output: Confirmación y actualización de lista.
*   **Dependencias**: `Finanzas/AdministrarProveedor` (Action).

### `AgregarGasto.cshtml`
*   **Propósito**: Ingreso de un nuevo gasto individual.
*   **Funcionalidad Clave**:
    *   Formulario detallado: Empresa, Proveedor, Tipo DTE, Montos (Neto/IVA/Total), Concepto, Centro de Costo, Cliente.
    *   Opción para adjuntar comprobante (PDF/Imagen).
    *   Opción "¿Es factura única?" para desglose.
    *   Validación de duplicidad de documento.
*   **Datos de Entrada/Salida**:
    *   Input: Modelo de Gasto.
    *   Output: Creación de registro de gasto.
*   **Dependencias**: `Finanzas/AgregarGasto` (Action), `_ModalDuplicidad`.

### `AgregarGastos.cshtml`
*   **Propósito**: Ingreso masivo o alternativo de gastos (estructura similar pero distinta disposición).
*   **Funcionalidad Clave**:
    *   Campos para DTE, Proveedor, Monto, Concepto, Banco.
    *   Carga de archivo Excel y Respaldo Fotográfico.
*   **Datos de Entrada/Salida**:
    *   Input: Datos de gasto y archivos adjuntos.
*   **Dependencias**: Scripts de `dropzone` (posiblemente para carga de archivos).

### `CargoMod.cshtml`
*   **Propósito**: Enrutador para funcionalidades de 'CargoMod' en Finanzas.
*   **Funcionalidad Clave**:
    *   Renderiza diferentes parciales según `ViewBag.OptionCargoMod`:
        *   `SolicitudItem`: `_SolicitudItem`.
        *   `Create`: `_WizardEmpresa` (Selección de empresa).
        *   `Detail`: `_DetalleSolicitud`.
        *   Otros estados (`Edit`, `OK`, `Send`).
*   **Lógica**: Actúa como un switch de vistas basado en el estado del proceso.

### `Index.cshtml`
*   **Propósito**: Página principal (Dashboard) del módulo Finanzas.
*   **Funcionalidad Clave**:
    *   Menú de navegación con accesos directos a sub-módulos: Ingresar Gasto, Administrar Gastos, Periodos, Proveedores, Pagos, Chequera, Despachos, CargoMod.
    *   Detección de entorno (Localhost vs Servidor) para construcción de URLs.
    *   Manejo de rutas dinámicas mediante JavaScript (`handleClickRedirectAction`).

### `MisGastos.cshtml`
*   **Propósito**: Visualización de los gastos del usuario ("Rendición de Gastos").
*   **Funcionalidad Clave**:
    *   Tabla con columnas: Tipo, N° Documento, Concepto, Area Negocio, Cliente, Montos, Estado.
    *   Acciones por fila (Ver, Editar, Eliminar - placeholders).
*   **Datos**: Itera sobre un modelo de ejemplo (actualmente estático en la vista o `ViewBag`).

### `Procesos.cshtml`
*   **Propósito**: Gestión de procesos de pago masivo.
*   **Funcionalidad Clave**:
    *   Pestañas/Secciones para: Pagos Pendientes, Procesos de Pago (Nóminas), Pagos Incluidos/Excluidos.
    *   Botones para iniciar proceso, descargar nómina, cerrar proceso.
    *   Visualización de detalles de nómina (Total, Cantidad de documentos).
*   **Dependencias**: `Finanzas/Procesos` (Action), parciales `_PagosPendientes`, `_ProcesosPago`.

### `CargaMasivaFinanzas.cshtml` y `CargaMasivaKam.cshtml`
*   **Propósito**: Interfaces para carga masiva de datos financieros.
*   **Funcionalidad Clave**:
    *   Reutilizan la vista parcial `_CargaMasiva`.
    *   Configuran parámetros distintos para la parcial: `Controller`, `Method`, `UrlDownload`, `PlantillaName`.
*   **Uso**: Facilitan la subida de Excel para procesar múltiples registros a la vez.

### `Data.cshtml`
*   **Propósito**: Configuración de datos financieros.
*   **Funcionalidad Clave**:
    *   Switch para renderizar: `Provisiones`, `Margenes` o `AsignProvMargen` (Asignación Provisión/Margen).
    *   Selectores de Empresa y Año.

### `SeguroCovid.cshtml`
*   **Propósito**: Gestión de seguros COVID (Clientes).
*   **Funcionalidad Clave**:
    *   Listado de clientes/seguros.
    *   Modales para agregar/editar.
*   **Dependencias**: Bundle `~/bundles/segurocovid`.

## Operaciones

### `AdministrarCuentas.cshtml`
*   **Propósito**: Administración de cuentas de ejecutivos (KAM).
*   **Funcionalidad Clave**:
    *   Visualización de cuentas por empresa (TWEST, TWRRHH, TWC).
    *   Asignación de ejecutivos a cuentas.
    *   Modales para agregar/editar cuentas.
*   **Dependencias**: Parciales `_CuentasTWEST`, `_CuentasTWRRHH`, etc.

### `Anexos.cshtml`
*   **Propósito**: Solicitud de Anexos de Contrato.
*   **Funcionalidad Clave**:
    *   Selección de tipo de anexo (Cambio AFP, Isapre, Domicilio, Sueldo, etc.).
    *   Opción de carga masiva de anexos (switch `CheckMasiva`).
    *   Renderiza parciales `_Anexos` o `_CargaMasivaAnexos`.

### `CargoMod.cshtml` (Operaciones)
*   **Propósito**: Enrutador para 'CargoMod' (mismo nombre que en Finanzas, contexto Operaciones).
*   **Funcionalidad Clave**:
    *   Similar a Finanzas, gestiona el flujo de solicitudes de cambio de cargo/condiciones.
    *   Maneja pasos: Selección Empresa -> Ingreso Datos -> Confirmación.

### `Index.cshtml` (Operaciones)
*   **Propósito**: Dashboard principal de Operaciones.
*   **Funcionalidad Clave**:
    *   **Lógica de Roles**: Renderiza contenido diferente según el perfil del usuario (`Coordinador Procesos`, `Auditor`, `SoloLectura`, etc.).
    *   Accesos a: Dashboard Solicitudes, Contratos, Renovaciones, Anexos, Bajas.
    *   Filtros avanzados por Empresa, Estado, Fecha.
    *   Contadores de estado (Semaforo): Pendientes, En Proceso, Finalizados.
    *   Carga asíncrona de dashboards parciales (`_DashboardSolicitudes`, `_DashboardContratos`, etc.).

### `Procesos.cshtml` (Operaciones)
*   **Propósito**: Gestión de procesos operativos (Contratos, Renovaciones).
*   **Funcionalidad Clave**:
    *   Control de flujo para `Contratos` y `Renovacion`.
    *   Renderiza parciales específicos del proceso seleccionado.
    *   Modales de confirmación y feedback.

### `SolicitudBajas.cshtml`
*   **Propósito**: Gestión de solicitudes de baja (desvinculaciones).
*   **Funcionalidad Clave**:
    *   Opción de carga masiva (`_CargaMasivaBajas`).
    *   Formulario individual para emitir solicitud (si `query=Manual`).
    *   Validación de causales y fechas.

### `SolicitudContrato.cshtml`, `SolicitudRenovacion.cshtml`
*   **Propósito**: Interfaces específicas para iniciar solicitudes de Contrato y Renovación.
*   **Funcionalidad Clave**:
    *   Configuración de parámetros para carga masiva y formularios manuales.
    *   Reutilización de componentes de Carga Masiva.

## Remuneraciones

### `Index.cshtml`
*   **Propósito**: Dashboard principal de Remuneraciones.
*   **Funcionalidad Clave**:
    *   Similar a Finanzas y Operaciones, ofrece un menú con accesos.
    *   Accesos: `Periodo`, `GenerateExcel`.

### `CargoMod.cshtml` (Remuneraciones)
*   **Propósito**: Router para modificaciones de cargo en contexto de Remuneraciones.
*   **Funcionalidad Clave**:
    *   Gestiona solicitudes de cambio de remuneraciones.

### `GenerateExcel.cshtml`
*   **Propósito**: Vista auxiliar para la generación de reportes excel.
*   **Funcionalidad Clave**:
    *   Probablemente actúa como un disparador o contenedor para la descarga de archivos.

## Asistencia

### `AdministracionBonos.cshtml` y `Bonos.cshtml`
*   **Propósito**: Gestión de bonos de asistencia.
*   **Funcionalidad Clave**:
    *   Búsqueda de bonos por ficha, area de negocio o periodo.
    *   Creación de nuevos bonos.
    *   Uso de parciales `_ModalAdministraBonos`.

### `Asistencia.cshtml` y `Calendario.cshtml`
*   **Propósito**: Gestión central de la asistencia (Inasistencia Múltiple y Por Ficha).
*   **Funcionalidad Clave**:
    *   Calendarios visuales de asistencia.
    *   Leyendas de estados (asistió, faltó, licencia, etc.).
    *   Modales de edición manual de asistencia `_ModalCambioAsistencia`.

### `CargasMasivas.cshtml`
*   **Propósito**: Menú para diferentes tipos de cargas masivas de asistencia.
*   **Funcionalidad Clave**:
    *   Accesos a: Inasistencia, Horas Extras, Reloj Control, Bonos.
    *   Redirección a vistas específicas como `SolicitudAsistencia`, `SolicitudHorasExtras`.

### `Reporte.cshtml`
*   **Propósito**: Generación de reportes de asistencia.
*   **Funcionalidad Clave**:
    *   Filtros por Empresa, Periodo, Area de Negocio.
    *   Botón para emitir Excel.

### `HorasExtras.cshtml`
*   **Propósito**: Solicitud y gestión de horas extras.
*   **Funcionalidad Clave**:
    *   Calendario para visualizar y cargar horas extras.
    *   Modal `_HorasExtras`.

## Bajas

### `Index.cshtml`
*   **Propósito**: Dashboard de gestión de Bajas y Finiquitos.
*   **Funcionalidad Clave**:
    *   Filtros avanzados (Rut, Causal, Estado, etc.).
    *   Listado de solicitudes.
    *   Acciones: Calcular finiquito, Ver documento, Cargar a notaría, Pagar.
    *   Integra múltiples parciales (`_SolicitudBaja`, `_Finiquitos`, `_Complementos`).

### `Finiquitos.cshtml`
*   **Propósito**: Visualización y gestión de finiquitos ya calculados.
*   **Funcionalidad Clave**:
    *   Permite filtrar por estados de pago o firma.
    *   Opciones para pago con Cheque o TEF (`_ModalPaymentMethods`).

### `Simulacion.cshtml`
*   **Propósito**: Simulación de cálculo de finiquitos.
*   **Funcionalidad Clave**:
    *   Permite pre-visualizar montos sin generar el documento final.

### `Visualizador.cshtml`
*   **Propósito**: Visualizador genérico de documentos PDF (contratos, finiquitos).
*   **Funcionalidad Clave**:
    *   Embed de PDF usando `ViewBag.PathViewPdf`.

### `Barcode.cshtml`
*   **Propósito**: Lectura inteligente de códigos de barra (probablemente para documentos físicos).
*   **Funcionalidad Clave**:
    *   Input oculto que captura el escaneo.
    *   Envío automático al detectar "Enter".

## Proceso

### `Index.cshtml` y `Contratos.cshtml`
*   **Propósito**: Gestión centralizada de contratos.
*   **Funcionalidad Clave**:
    *   Contenedores para la carga de parciales de proceso (`_Contratos`).
