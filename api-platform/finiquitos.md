# Documentación del Controlador de Finiquitos (FiniquitosController)

## Descripción General
El `FiniquitosController` es un controlador de API REST (`ApiController`) encargado de gestionar todas las operaciones relacionadas con el proceso de finiquitos. Su función principal es actuar como una capa de intermediación que recibe solicitudes HTTP, empaqueta los parámetros necesarios y delega la lógica de negocio a la base de datos a través de procedimientos almacenados.

El controlador expone diversos endpoints para consultar, crear, modificar, validar, gestionar envíos/recepciones (notaría, regiones, firmas), y procesar pagos de finiquitos.

## Comunicación con otros Componentes
El controlador interactúa principalmente con los siguientes componentes del sistema:

1.  **CallExecutionAPI**: Componente encargado de la ejecución de procedimientos almacenados en la base de datos. El controlador instancia esta clase para llamar a `ExecuteStoreProcedure`.
2.  **PackageFiniquitos**: Clase auxiliar (Helper) utilizada para:
    *   Obtener los nombres de los parámetros (`PackageFiniquitos.Param()`).
    *   Formatear los valores de los parámetros según la acción a realizar (`PackageFiniquitos.Values(...)`).
3.  **DataPackageFiniquitos**: Modelo de datos (`Teamwork.Model.Parametros.Finiquitos`) que estructura la información recibida en el cuerpo de las solicitudes (Body).
4.  **Base de Datos**: La comunicación final es con el motor de base de datos para ejecutar la lógica de negocio encapsulada.

## Procedimientos Almacenados y Queries
El controlador utiliza un **único procedimiento almacenado principal** para todas sus operaciones, diferenciando la acción a realizar mediante un parámetro de texto (el primer argumento pasado a `PackageFiniquitos.Values`).

*   **Procedimiento Almacenado**: `package.__Finiquitos`

### Lista de Acciones (Operaciones)
A continuación se listan las acciones específicas que se envían al procedimiento almacenado para cada endpoint:

*   `ConsultaFiniquitos`
*   `ConsultaComplementos`
*   `ConsultaSimulaciones`
*   `PaginationFiniquitos`
*   `HistorialFiniquitos`
*   `AnularFiniquito`
*   `ValidarFiniquito`
*   `GestionEnvioRegiones`
*   `GestionEnvioSantiagoNotaria`
*   `GestionEnvioSantiagoParaFirma`
*   `GestionEnvioLegalizacion`
*   `GestionRecepcionRegiones`
*   `GestionRecepcionSantiagoNotaria`
*   `GestionRecepcionSantiagoParaFirma`
*   `GestionRecepcionLegalizacion`
*   `ConsultaDatosTEF`
*   `SolicitudTEF`
*   `SolicitudValeVista`
*   `SolicitudCheque`
*   `ConsultaSolicitudPago`
*   `ConfirmarProcesoPago`
*   `Bancos`
*   `PagarFiniquito`
*   `DashboardFiniquitos`
*   `ListarLiquidacionesSueldoYear`
*   `ListarLiquidacionesSueldoMes`
*   `LiquidacionSueldoBase64`
*   `ComplementoCrear`
*   `ComplementoListarHaberes`
*   `ComplementoListarDescuento`
*   `ComplementoAgregarHaber`
*   `ComplementoAgregarDescuento`
*   `ComplementoEliminarHaber`
*   `ComplementoEliminarDescuento`
*   `ComplementoDejarActivoCreado`
*   `ActualizarMontoAdministrativo`
*   `RevertirValidacion`
*   `RevertirGestionEnvio`
*   `RevertirLegalizacion`
*   `RevertirSolicitudPago`
*   `RevertirConfirmacion`
*   `RevertirEmisionPago`
*   `ConfigOpcionesMovimientoMasivos`
*   `ActualizarComentariosAnotaciones`
*   `ConsultaComentariosAnotaciones`
*   `TerminarFiniquito`
*   `FirmarFiniquito`
*   `ReprocesarDocumentosFiniquito`
*   `ValidarFinanzas`
*   `RevertirValidacionFinanzas`
