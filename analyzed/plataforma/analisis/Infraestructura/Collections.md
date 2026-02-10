# Análisis de Collections (Infraestructura)

Este documento detalla las clases `Collection` que actúan como gateways hacia la API.

## Módulo: Auth
### `CollectionAuth`
**Archivo**: `Collections/Auth/CollectionAuth.cs`
-   **Método**: `__CrearTokenConfianza`
    -   **Entrada**: `usuarioCreador` (string)
    -   **Lógica**: Autentica, obtiene token, y llama a `CallAPIToken.__CrearTokenConfianza`.
    -   **Salida**: `List<TokenConfianza>`

## Módulo: Dashboard
### `CollectionDashFiniquitos`
**Archivo**: `Collections/Dasboard/CollectionDashFiniquitos.cs`
-   **Método**: `__FiniquitosDashboardFiniquitos`
    -   **Entrada**: `usuarioCreador` (string)
    -   **Lógica**: Consulta datos para el dashboard de finiquitos.
    -   **API**: `CallAPIFiniquitos.__FiniquitosDashboardFiniquitos`
    -   **Salida**: `List<DashFiniquitos>`

## Módulo: Finiquitos
### `CollectionFiniquitos`
**Archivo**: `Collections/Finiquitos/CollectionFiniquitos.cs`
Clase extensa con múltiples operaciones de finiquitos.
-   **Consultas**:
    -   `__FiniquitosConsultaFiniquitos`: Filtra y lista finiquitos (`CallAPIFiniquitos.__FiniquitosConsultaFiniquitos`).
    -   `__FiniquitosConsultaComplementos`: Lista complementos.
    -   `__FiniquitosHistorialFiniquitos`: Obtiene historial por código.
    -   `__FiniquitosConsultaDatosTEF`: Obtiene datos para transferencia electrónica.
    -   `__FiniquitosBancos`: Lista bancos disponibles.
    -   `__FiniquitosConsultaSolicitudPago`: Consulta estado de pago.
    -   `__FiniquitosListarLiquidacionesSueldoYear` / `...Mes`: Lista liquidaciones.
-   **Operaciones (Requests)**:
    -   `__FiniquitosValidarFiniquito`: Valida un finiquito.
    -   `__FiniquitosAnularFiniquito`: Anula con observación.
    -   `__FiniquitosGestionEnvio...`: Métodos para gestionar envíos a notaría, firma, regiones, etc.
    -   `__FiniquitosSolicitudValeVista` / `...Cheque` / `...TEF`: Gestiona solicitudes de pago.
    -   `__FiniquitosComplemento...`: Crea, agrega haberes/descuentos a complementos.
    -   `__FiniquitosPagarFiniquito`: Ejecuta el pago.

## Módulo: General
### `CollectionExcel`
**Archivo**: `Collections/General/CollectionExcel.cs`
-   **Método**: `__ReporteFiniquitos`
    -   **Entrada**: `excel` (config?), `data`
    -   **API**: `CallAPIExcel.__ReporteFiniquitos`
    -   **Salida**: Objeto dinámico (JSON para reporte).
-   **Método**: `__ReporteSolicitud`
    -   **API**: `CallAPICargaMasiva.__PlantillaEmitirSolicitud`
    -   **Salida**: Objeto dinámico.

## Módulo: Paginations
### `CollectionPagination`
**Archivo**: `Collections/Paginations/CollectionPagination.cs`
Maneja la paginación para diferentes listados masivos.
-   **Métodos**: `__PaginationSolicitudContratos`, `__PaginationSolicitudRenovaciones`
    -   **API**: `CallAPICargaMasiva`
    -   **Salida**: `List<Pagination>`

## Módulo: Persona
### `CollectionPersona`
**Archivo**: `Collections/Persona/CollectionPersona.cs`
Gestión de Clientes (Personas/Empresas).
-   **Métodos**:
    -   `__PersonaConsultarCliente`: `CallAPIPersona.__PersonaConsultarCliente` -> `List<Cliente>`
    -   `__PersonaCrearCliente`: Crea cliente -> `List<Request>`
    -   `__PersonaEliminarCliente`: Elimina cliente -> `List<Request>`

## Módulo: Procesos
### `CollectionProceso`
**Archivo**: `Collections/Procesos/CollectionProceso.cs`
-   **Métodos**:
    -   `__ProcesoListarContratos`: Lista contratos en proceso. (`CallAPICargaMasiva`)
    -   `__ProcesoModificarNombre`: Modifica nombre de proceso.

## Módulo: Remuneraciones
### `CollectionsReporte`
**Archivo**: `Collections/Remuneraciones/CollectionsReporte.cs`
-   **Método**: `__ReporteCargoMod`
    -   **API**: `CallAPIExcel.__ReporteCargoMod`
    -   **Salida**: Dinámico para Excel.

## Módulo: Seleccion
### `CollectionSeleccion`
**Archivo**: `Collections/Seleccion/CollectionSeleccion.cs`
Gestión amplia de procesos de selección y reclutamiento.
-   **Tags**: Crear, Listar, Actualizar, Eliminar Tags de postulantes (`CallAPISeleccion`).
-   **Ofertas Laborales**: Listar, Actualizar.
-   **Tags Oferta**: Asociar tags a ofertas.
-   **OAuth**: `__CodigoOAuthCrear` para integraciones.
-   **Utilidades**: Listar Targets, Paginación específica de selección.

## Módulo: Solicitudes
### `CollectionSolicitud`
**Archivo**: `Collections/Solicitudes/CollectionSolicitud.cs`
Gestión de solicitudes de contratos, renovaciones y fichas.
-   **Listados**:
    -   `__SolicitudListarContratos` y `__SolicitudListarRenovaciones` (`CallAPICargaMasiva`).
-   **Acciones**:
    -   `__SolicitudAnularSolicitud`.
-   **Consultas KAM** (`CallAPIKam`):
    -   `__ConsultaFichaPersonal`, `__ConsultaContrato`, `__ConsultaRenovacion`, `__ConsultaBaja`.
