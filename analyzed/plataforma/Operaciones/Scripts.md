# Análisis de Scripts - Operaciones (JavaScript)

Este documento detalla el análisis de los archivos JavaScript ubicados en `AplicacionOperaciones/Scripts/teamwork`.

## 1. Archivos Analizados

### 1.1 `_AdministrarCuentas.js`
**Propósito:** Gestionar la visualización de información de cuentas.
**Rutas de Servicio (API):**
- `InformacionCuenta`: Invocada por `ajaxInformacionCuenta`.
**Lógica Principale:**
- Escucha eventos `click` en elementos `.modalCuenta`.
- Muestra el modal `#modalCuenta`.

### 1.2 `_AdministrarPeriodos.js`
**Propósito:** Administración de periodos contables (cierre).
**Rutas de Servicio (API):**
- `CerrarPeriodo`: Invocada por `ajaxCerrarPeriodo`.
**Lógica Principal:**
- Maneja eventos para cerrar periodos (`.closePeriodo`).
- Muestra confirmación con `swal`.

### 1.3 `_AdministrarProveedor.js`
**Propósito:** ABM (Alta, Baja, Modificación) de Proveedores.
**Rutas de Servicio (API):**
- `AgregarProveedor`: Crea un nuevo proveedor.
- `UpdateProveedor`: Actualiza un proveedor existente.
- `DeleteProveedor`: Elimina (o desactiva) un proveedor.
**Lógica Principal:**
- Validación de campos (RUT, Nombre, etc.).
- Manejo de modales para agregar/editar.

### 1.4 `_AgregarGasto.js`
**Propósito:** Gestión completa del ingreso de gastos (Finanzas).
**Rutas de Servicio (API):**
- `CargarGastoFinanzas`: Endpoint principal para guardar el gasto (`ajaxCargaGastoFinanza` en `ajaxFuncionesFZ.js`).
**Lógica Principal:**
- **Validaciones exhaustivas:** Empresa, Cliente (según "Apertura" o no), Montos, Proveedor (Rut), Documentos (Factura/Boleta).
- **Cálculos:** Cálculo de impuestos (Retención, IVA) basado en el tipo de documento.
- **Interacción Dinámica:** Habilita/deshabilita campos según la empresa y periodo vigente (`ajaxObtenerPeriodoVigente`).
- Manejo de múltiples clientes para distribución de gastos ("Apertura").

### 1.5 `_Anexos.js`
**Propósito:** Gestión de subida de archivos adjuntos.
**Rutas de Servicio (API):**
- `RenderizaAnexos`: Endpoint para procesar la subida.
**Lógica Principale:**
- Manejo del input file `#fileAnexos`.
- Validación simple de selección de archivo.

### 1.6 `_ArchivoAsistencia.js`
**Propósito:** Gestión de archivos de asistencia (Carga/Descarga/Eliminación) por Ficha y Periodo.
**Rutas de Servicio (API):**
- `GetAreaNegocio`: Obtiene áreas de negocio.
- `ObtenerRutaFichero`: Obtiene rutas configuradas.
- `ObtenerNombreArchivo`: Busca archivos existentes.
- `ObtenerArchivoAsistencia`: Obtiene lista de archivos para la grilla.
- `InsertaArchivoAsistencia`: Sube el archivo.
- `EliminaArchivoAsistencia`: Elimina el archivo.
- `DownloadArchivo`: Descarga directa.
**Lógica Principal:**
- Validación de Empresa, Area Negocio y Periodo.
- Filtrado por tipo de acción (Cargar vs Buscar).
- Uso de `FormData` para envío de archivos.

### 1.7 `_Asistencia.js`
**Propósito:** Control de Asistencia mensual por trabajador (Malla de turnos/asistencias).
**Rutas de Servicio (API):**
- `ObtenerAsistencia`: Obtiene la malla de asistencia, licencias, vacaciones y bajas.
- `GetDataPersonal`: Obtiene datos del trabajador (Cabecera).
- `InsertaAsistencia`: Guarda cambios en la asistencia.
- `ActualizaAsistencia`: Modifica un registro específico.
- `EliminaAsistencia`: Elimina un registro.
- `GetTipoAsistencia`: Obtiene tipos de asistencia para selects.
- `GetLegend`: Obtiene leyenda de colores/códigos.
- `ViewPartialLoaderTransaccion`: Carga parcial de loader.
- `GenerateExcel`: Reporte Excel.
**Lógica Principal:**
- **Renderizado de Calendario:** Genera dinámicamente una tabla HTML (`calendarioAsistencia`) representando el mes seleccionado.
- **Manejo de Estados:** Identifica días trabajados, faltas, licencias, vacaciones, finiquitos y bajas confirmadas/pendientes.
- **Interacción:** Permite hacer clic en celdas para cambiar el estado (Falta, etc.) o editar/eliminar.
- **Validaciones:** Cruce de fechas de contrato vs periodo visualizado. Inconsistencias entre Baja Confirmada y Baja Informada KAM.

### 1.8 `_Bonos.js`
**Propósito:** Administración y asignación de Bonos.
**Rutas de Servicio (API):**
- `GetAreaNegocio`: Listado de áreas.
- `ObtenerBonos`: Lista tipos de bonos.
- `ObtenerBonoCliente`: Lista bonos asignados a cliente/empresa.
- `IngresarBonos`: Crea nuevo tipo de bono.
- `IngresarBonoCliente`: Asigna bono a cliente.
- `ActualizaTipoBono`: Edita tipo de bono.
- `ActualizaBonoCliente`: Estado de asignación.
- `EliminarTipoBono`: Borra tipo bono.
- `EliminarBonoCliente`: Borra asignación.
- `IngresarBonosFicha` / `ActualizaBonosFicha` / `EliminaBonosFicha` (Inferido de handlers): Asignación directa a ficha.
**Lógica Principal:**
- CRUD completo de Tipos de Bonos y Asignaciones.
- Manejo de dos contextos de búsqueda: Por Ficha o Por Área de Negocio.
- Modales dinámicos para Crear/Editar.

### 1.9 `_CargaMasivaFinanzas.js`
**Propósito:** Configuración inicial para módulo de Finanzas.
**Lógica Principal:**
- Establece el prefijo de dominio "Finanzas".

### 1.10 `_Finiquitos.js`
**Propósito:** Lógica de UI para módulo de Finiquitos (Buscador/Filtros).
**Lógica Principal:**
- **Filtros Dinámicos:** Muestra/oculta inputs (`#filterDatos`, `#filterEmpresa`, etc.) según el criterio de búsqueda seleccionado (Rut, Empresa, Fecha, etc.).
- Control de tipo de pago (Cheque vs TEF).

### 1.11 `_HorasExtras.js`
**Propósito:** Gestión de solicitudes de horas extras.
**Rutas de Servicio (API):**
- `GetHorasExtras`: Obtiene listado.
- `UpdateHoraExtra`: Actualiza información.
- `DeleteHoraExtra`: Elimina solicitud.
**Lógica Principal:**
- Modal para asignación.
- Cálculo de horas.
- Interacción AJAX para persistencia.

### 1.12 `_JornadaLaboral.js`
**Propósito:** Gestión de jornadas laborales.
**Rutas de Servicio (API):**
- `IngresarJornada`: Crea nueva jornada.
- `UpdateJornada`: Actualiza jornada.
- `DeleteJornada`: Elimina jornada.
**Lógica Principal:**
- Renderizado dinámico de formularios (Agregar/Editar).
- Validaciones de días y horas.

### 1.13 `__AjaxCargaMasiva.js`
**Propósito:** Orquestación AJAX para procesos de carga masiva.
**Rutas de Servicio (API):**
- `PlantillaValidationData`: Valida datos.
- `PlantillaCreationData`: Crea registros.
- `PlantillaEmissionData`: Emite registros.
- `PlantillaEnvioData`: Envíos finales.
**Lógica Principal:**
- Flujo secuencial de etapas (Validación -> Creación -> Emisión -> Envío).
- Manejo de estados de carga y feedback visual.
- Descarga de reportes de error (`GenerarExcelErrores`).

### 1.14 `__CargaMasivaDinamica.js`
**Propósito:** Controlador AngularJS para cargas masivas dinámicas.
**Rutas de Servicio (API):**
- `PlantillaSubir`: Sube datos procesados.
**Lógica Principal:**
- Lectura de Excel cliente con libreria `XLSX`.
- Conversión de datos Excel a XML.
- Envío de datos al servidor.

### 1.15 `AjaxPostulante.js`
**Propósito:** Formulario externo/público para postulantes.
**Rutas de Servicio (API):**
- `PostulanteCreaOActualizaFichaPostulante`: Guarda datos básicos.
- `PostulanteConsultaFieldEncuesta`: Obtiene campos dinámicos de encuesta.
- `PostulanteValidaFichaPersonal`: Valida DNI.
- `PostulanteConsultaFichaPersonal`: Obtiene datos por DNI.
**Lógica Principal:**
- Manejo de prefijos de dominio para localhost vs producción.
- Renderizado dinámico de campos de encuesta.
- Validación y autocompletado por RUT.

### 1.16 `CargaMasiva.js`
**Propósito:** Controlador AngularJS específico para "Carga Masiva" (similar a la dinámica pero específica con hoja 'Carga Masiva').
**Rutas de Servicio (API):**
- `PlantillaSubir`: Carga de datos XML.
**Lógica Principal:**
- Validación de existencia de hoja "Carga Masiva".
- Transformación Excel a XML con estructura nodo padre/hijo configurada en dataset.

### 1.17 `ProcesosGeneric.js`
**Propósito:** Manejadores de eventos genéricos para flujos de Procesos (Anular, Terminar, Revertir).
**Rutas de Servicio (API):**
- `ajaxAnularProceso`, `ajaxTerminarProceso`, `ajaxRevertirTerminoProceso` (funciones globales inferidas).
- `ajaxRenderizaProcesos`: Renderizado de tablas/grillas.
**Lógica Principal:**
- Delegación de eventos para botones de acción en listados.
- Modales de confirmación con motivos de anulación.
- Lógica de paginación y filtros para dashboards de procesos.

### 1.18 `SolicitudesGeneric.js`
**Propósito:** Manejadores de eventos genéricos para flujos de Solicitudes.
**Rutas de Servicio (API):**
- `ajaxAnularSolicitud`, `ajaxRevertirTerminoSolicitud`.
- `ajaxRenderizaSolicitudesPro`: Renderizado de listados.
**Lógica Principal:**
- Similar a `ProcesosGeneric.js` pero orientado a "Solicitudes" individuales.
- Manejo de anulación y reversión.

### 1.19 `Utils.js`
**Propósito:** Librería de funciones utilitarias.
**Lógica Principal:**
- `formatRutTypeSoftland`: Formateo de RUT (puntos y guión).
- `extensionFile` / `extensionFileIcon`: Helpers para iconos y descripciones de archivos.
- `convertFechaPalabra`: Convierte fecha `YYYY-MM-DD` a texto "DD de Mes de YYYY".
- `diffDays`: Calcula diferencia de días entre fechas.
- Generadores de HTML `option` para días, meses y años.

### 1.20 `_Index.js`
**Propósito:** Lógica principal del Dashboard y navegación.
**Rutas de Servicio (API):**
- `ajaxRenderizaDashboardTotal` (implícito).
- `ajaxRenderizaDashboardAnalistaContratos` (implícito).
**Lógica Principal:**
- Manejo de clicks en botones de filtro KAM (`.dashboardBtnKAM`) y Eventos (`.dashboardEvent`).
- Renderizado de partes del dashboard según selección.

### 1.21 `_Procesos.js`
**Propósito:** Lógica específica para la vista de Procesos.
**Lógica Principal:**
- Verificación de mensajes de éxito en `sessionStorage` al cargar.
- Manejo de visualización de campo "Otro Motivo" en modales.

### 1.22 `_Renovacion.js`
**Propósito:** Lógica específica para Renovaciones.
**Rutas de Servicio (API):**
- `CreatePlantillaExcelDocManual` (via `ajaxCreatePlantillaExcelDocManual`).
**Lógica Principal:**
- Disparadores para carga de archivos.
- Descarga de plantillas Excel.

### 1.23 `_SignIn.js`
**Propósito:** Lógica de Autenticación e Inicio de Sesión.
**Rutas de Servicio (API):**
- `SignIn`: Valida credenciales.
- `ViewPartialErrorSignIn`, `ViewPartialLoadingSignIn`, `ViewPartialFormSignIn`: Vistas parciales login.
**Lógica Principal:**
- Detección de navegador compatible.
- Autenticación dual (ActiveDirectory vs Default) según formato de usuario.
- Redirección post-login.

### 1.24 `_Solicitudes.js`
**Propósito:** Lógica específica para la vista de Solicitudes (mínima).
**Lógica Principal:**
- Manejo de visualización de campo "Otro Motivo".

### 1.25 `ajaxFuncionesFZ.js`
**Propósito:** Funciones AJAX específicas para el módulo de Finanzas.
**Rutas de Servicio (API):**
- `CargarGastoFinanzas`: Crea gasto.
- `ViewDDLClientes` / `ViewDDLClientesDistintos`: Listas de clientes.
- `ViewDDLProveedores`: Lista de proveedores.
- `ObtenerTipoReembolsoDDL`: Tipos de reembolso.
- `ObtenerProveedorRut`: Busca datos de proveedor.
- `ObtenerPeriodoVigente`: Verifica periodo contable.
- `ObtenerClienteByNombre`: Búsqueda cliente.
- `GetExisteDocumento`: Valida duplicidad de factura.
**Lógica Principal:**
- `ajaxCargaGastoFinanza`: Función core para crear gastos.
- `ajaxObtenerPeriodoVigente`: Habilita/deshabilita formulario según estado del periodo.
- Validaciones de duplicidad de documentos.

### 1.26 `ajaxFunctions.js`
**Propósito:** Biblioteca principal de funciones AJAX para la aplicación. Maneja Cargas Masivas, Procesos, Dashboards y Solicitudes.
**Rutas de Servicio (API):**
- **Carga Masiva:** `CargaMasiva`, `ValidacionCargaMasiva`, `CreaOActualizaFichaPersonal`, `CreaContratoDeTrabajo`, `CreaRenovacion`, `CreaAnexo`, `CrearBaja`, `LoaderCrearSolicitud`, `CreaSolicitudBaja`, `CreaSolicitudRenovacion`, `CreaSolicitudContrato`, `LoaderEnviarSolicitud`, `EnviaSolicitudRenovacion`, `EnviaSolicitudContrato`, `EnviaSolicitudBajas`.
- **Procesos:** `AnularProceso`, `TerminarProceso`, `EnviaInformacionProcesoTerminado`, `RevertirTerminoProceso`, `EnviaInformacionProcesoReversionTermino`.
- **Solicitudes:** `AnularSolicitud`, `CreaSolicitudContratoAnulada`, `EnviaSolicitudContratoAnulado`, `CreaSolicitudRenovacionAnulada`, `EnviaSolicitudRenovacionAnulado`, `EnviaSolicitudContratoAnuladoInd`, `EnviaSolicitudRenovacionAnuladoInd`, `RevertirTerminoSolicitud`.
- **Dashboard/Vistas:** `RenderizaProcesos`, `RenderizaSolicitudes`, `RenderizaHeaderProcesos`, `ViewPartialProcesos`, `ViewPartialSolicitudes`, `ViewPartialSolicitudesPro`, `ViewPartialHeaderProcesos`, `ViewPartialPagination`, `RenderizaAnexos`, `ViewPartialAnexos`, `RenderizaDashboardTotal`, `RenderizaDashboardAnalistaContratos`, `ViewPartialDashboardSolicitudes`, `ViewPartialDashboardAnalistaContartos`, `InformacionCuenta`.
- **Cargadores/Errores:** `ViewPartialLoaderProcesoMasivo`, `ViewPartialLoaderErrorGeneric`, `ViewPartialLoaderComplete`, `ViewPartialLoaderTransaccion`, `ViewPartialLoaderErrorGenericModal`, `ViewPartialNoEncontrado`.
- **Finanzas:** `CrearGastosFinanzas`.
**Lógica Principal:**
- **Carga Masiva:** Implementa un flujo recursivo/secuencial para procesar grandes volúmenes de datos (Validación -> Creación Ficha -> Creación Contrato -> Creación Solicitud -> Envío Solicitud).
- **Procesos:** Gestiona transiciones de estado complejas (Anular, Terminar, Revertir), actualizando el DOM en tiempo real par reflejar los cambios de estado sin recargar la página completa.
- **Dashboards:** Renderiza componentes parciales (vistas parciales) traídos desde el servidor para construir tableros dinámicos.

### 1.27 `ajaxFuntionsBJ.js`
**Propósito:** Funciones AJAX específicas (o duplicadas/versionadas) para renderizado de Solicitudes (posiblemente Bajas/Generales).
**Rutas de Servicio (API):**
- `RenderizaSolicitudes`: Obtiene lista de solicitudes.
- `ViewPartialSolicitudes`: Obtiene vista parcial de una fila de solicitud.
**Lógica Principal:**
- Renderizado de tablas de solicitudes y paginación. Similar a `ajaxFunctions.js` pero enfocado en una vista específica.

### 1.28 `_SolicitudBaja.js`
**Propósito:** Lógica específica para la gestión de Solicitudes de Bajas.
**Rutas de Servicio (API):**
- `GenerateExcel`: Generación de reportes Excel.
**Lógica Principal:**
- Filtros dinámicos por Empresa (`#selectEmpresa`) y Tipo (`#selectTodas`).
- **Generación de Reportes:** Construcción compleja de URLs de descarga para `GenerateExcel` con parámetros codificados en base64 (`btoa`), manejando filtros de fecha y tipo de baja ("BajaConfirmada", "BajaNoConfirmada").

### 1.29 `_SolicitudBajas.js`
**Propósito:** Funcionalidad mínima para listado de bajas.
**Lógica Principal:**
- Modal para visualización de documentos PDF (`.eventViewPdf`).

### 1.30 `_SolicitudContratos.js`
**Propósito:** Lógica para la gestión de Solicitudes de Contratos.
**Rutas de Servicio (API):**
- `CreatePlantillaExcelDocManual` (via `ajaxCreatePlantillaExcelDocManual`).
**Lógica Principal:**
- Manejo de subida de archivos (`#fileUpload`).
- Descarga de plantilla de carga manual (`#downloadUpload`).

### 1.31 `__CargaMasivaRelojControl.js`
**Propósito:** Controlador AngularJS (`myController`) específico para Carga Masiva de Reloj Control.
**Rutas de Servicio (API):**
- `PlantillaSubir`: Carga de datos procesados.
**Lógica Principal:**
- **Lectura Excel:** Usa `FileReader` y `XLSX` para leer archivos binarios localmente.
- **Validación Estructura:** Verifica existencia de hojas específicas y columnas.
- **Transformación:** Convierte filas Excel a XML concatenado, manejando eliminación de diacríticos (`handleEliminarDiacriticos`).
- **Configuración Dinámica:** Se adapta según atributos `dataset` del elemento HTML (cifrado, nodo padre/hijo, columnas).

