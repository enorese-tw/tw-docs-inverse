# Documentación Detallada: `IServicioFiniquitos.cs`

## Visión General
Esta interfaz define el **Contrato de Servicio (Service Contract)** WCF para `wsServiceFiniquitos`. Especifica todas las operaciones disponibles que los clientes externos pueden invocar a través de SOAP. Actúa como el catálogo público de funcionalidades del sistema.

## Estructura del Contrato
El contrato está decorado con `[ServiceContract]` y sus métodos con `[OperationContract]`, habilitando su exposición en WCF.

Los métodos se agrupan funcionalmente en regiones:

### 1. Operaciones de Contrato Internas (Base de Datos Propia)

#### GET (Consultas)
*   **Proveedores y Mantención**:
    *   `GetObtenerProveedoresProveedor`
*   **Solicitud de Baja (B4J)**:
    *   `GetObtenerSolicitudesB4J`
*   **EST (Estratégica)**:
    *   Finiquitos: `GetCargaVariable`, `GetCausales`, `GetListarDocumentos`, `GetObtenerENCFiniquito`.
    *   Pagos: `GetContratosCalculoPagos`, `GetTotalesPagosEST`, `GetValidaDocumentoPagos`.
    *   Cálculo: `GetVisualizarDatosTrabajadorCalculo`, `GetVisualizarTotalesFiniquitoCalculo`, etc. (Gran cantidad de métodos para visualizar detalles).
*   **OUT (Outsourcing)**:
    *   Sección reservada pero vacía en la interfaz (`#region "OUT"`).
*   **Genéricos**:
    *   `GetToken`, `GetPlantillasCorreo`, `GetAlertasInformativas`.

#### SET (Transacciones)
*   **Solicitud de Baja**:
    *   `SetCrearSolicitudB4J`, `SetAdministrarProcesosSolB4J`.
*   **EST**:
    *   Creación de datos: `SetInsertaContratoCalculo`, `SetInsertaDiasVacaciones`.
    *   Confirmación: `SetChequePagado`, `SetConfirmarRetiroCalculo`.
    *   Procesos Temporales: `SetTMPCargaChequeProveedorProveedores`, `SetTMPInitProcessChequeFiniquitos`.
*   **OUT (Outsourcing)**:
    *   Métodos específicos para guardar datos OUT: `SetContratosOUT`, `SetHaberesYDescuentosOUT`.
*   **Genéricos**:
    *   `SetAnularFolioFiniquito`, `SetFiniquitoPagado`, `SetNotariadoPagos`.

### 2. Operaciones de Contrato Softland (Integración ERP)

#### GET (Softland)
Consultas directas al ERP Softland, segregadas por contexto:
*   **EST**: `GetCentroCostos`, `GetContratoActivoBaja`, `GetListarFiniquitados`, `GetCausalFicha`.
*   **OUT**: `GetJornadasParttimeOUT`, `GetRutTrabajadorSolicitudBajaOUT`.
*   **CONSULTORA**: `GetRutTrabajadorSolicitudBajaCONSULTORA` (Método único para este contexto).

## Definición de Datos (DataContract)
El contrato define una única estructura de intercambio de datos compleja:

### Clase `ServicioFiniquito`
*   Decorada con `[DataContract]`.
*   Encapsula un `DataSet` (propiedad `Table` con `[DataMember]`).
*   **Implicación**: Todas las respuestas del servicio retornan un `DataSet` genérico, lo que significa que el cliente debe conocer la estructura de tablas y columnas (schema) de antemano, ya que no hay tipado fuerte en la respuesta.

## Observaciones de Diseño
*   **Firma de Métodos**: La inmensa mayoría de los métodos aceptan `string[] Parametros, string[] valores`. Esto sugiere un patrón de "pares clave-valor" para pasar argumentos, evitando la definición de objetos de petición tipados.
*   **Flexibilidad vs. Seguridad**: El uso de arrays de strings y DataSets hace el servicio muy flexible a cambios de esquema (no rompe el contrato WCF fácilmente), pero muy frágil en tiempo de ejecución (errores de tipos, nombres de columnas, número de argumentos).
