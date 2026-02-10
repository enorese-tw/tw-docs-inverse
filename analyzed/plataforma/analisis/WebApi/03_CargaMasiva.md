# Módulo Carga Masiva - Teamwork.WebApi

**Archivo**: `Teamwork.WebApi/CallAPICargaMasiva.cs`

Facilita la carga masiva de datos (contratos, asistencia, bonos, etc.) mediante plantillas (probablemente Excel o CSV).

## Gestión de Plantillas (`cargamasiva/Plantilla/`)

Encargados de subir, descargar y validar archivos de carga masiva.

- **`__PlantillaListar`** (`Listar`): Lista plantillas disponibles.
- **`__PlantillaSubir`** (`Subir`): Sube una plantilla.
    - **Lógica Específica**: Reemplaza comillas simples `'` por comillas dobles `"` en el parámetro `CargaMasiva` antes de enviarlo.
- **`__PlantillaDownload`** (`Download`): Descarga una plantilla.
- **`__PlantillaReportError`** (`ReportError`): Reporta errores en una transacción.
- **Validaciones**: Métodos para validar diferentes tipos de cargas:
    - `ValidarCargaContrato`, `ValidarCargaRenovacion`, `ValidarCargaBajas`
    - `ValidarCargaAsistencia`, `ValidarCargaHorasExtras`, `ValidarBono`
    - `ValidarCargaAsistenciaRetail`, `ValidarCargaAsistenciaRelojControlGeoVictoria`
- **Creación**: Métodos para procesar la creación tras la validación:
    - `CrearContrato`, `CrearRenovacion`, `CrearBaja`
    - `CrearAsistencia`, `CrearHorasExtras`, `CrearBono`
    - `ActualizaCreaFicha`

## Procesos y Solicitudes (`cargamasiva/Proceso/` y `cargamasiva/Solicitud/`)

Gestiona el ciclo de vida de las solicitudes generadas por cargas masivas.

- **`__ProcesoListarContratos`** / **`__SolicitudListarContratos`**: Lista contratos procesados.
- **`__PlantillaEmitirSolicitud`**: Emite una solicitud formal basada en una plantilla cargada.
- **`__SolicitudAnularSolicitud`**: Anulación de solicitudes.

## Paginación y Reportes

- **`__PaginationSolicitudContratos`**: Paginación de solicitudes.
- **`__ReporteRenovaciones`** / **`__ReporteContratos`**: Generación de reportes específicos (`cargamasiva/Reporte/`).

**Datos Relevantes**:
- `CodigoTransaction`: Identificador clave para rastrear el estado de una carga masiva.
- `PlantillaMasiva`: Identificador del tipo de plantilla o archivo.
