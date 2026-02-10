# Inventario de Procedimientos Almacenados (Stored Procedures)

Este documento detalla todos los Stored Procedures invocados desde el código fuente de la API, organizados por el Módulo (Controlador) que los consume y la Base de Datos destino.

## 1. Resumen de Hallazgos
-   **Arquitectura:** El 100% de los controladores analizados delegan la persistencia y lógica a Stored Procedures.
-   **Patrón:** Se utiliza predominantemente una clase wrapper (`CallExecutionAPI`) para ejecutar los SPs.
-   **Bases de Datos:** Se identificaron conexiones a:
    -   `DatabaseOperaciones` (Por defecto)
    -   `DatabaseSeleccion` (Módulo de Reclutamiento/Selección)
    -   `DBCoins` (Módulo de Coins/Formularios)
    -   `Database` (Configuración y Auth)
    -   `DatabaseSoftland` (Dinámica según cliente, ver análisis Softland)

## 2. Inventario Detallado

### 2.1. Módulo Operaciones y Remuneraciones

| Controlador | Stored Procedure Identificado | Base de Datos (Config Key) | Acciones / Métodos Detectados |
| :--- | :--- | :--- | :--- |
| **OperacionesController** | `remuneraciones.package_RM_CargoMod` | `DatabaseOperaciones` | `Listar`, `Crear`, `Actualizar`, `Eliminar`, `Simular`, `Aprobar` |
| | `remuneraciones.package_RM_Bonos` | `DatabaseOperaciones` | `Listar`, `Crear`, `Actualizar`, `Eliminar` |
| | `remuneraciones.package_RM_ANI` | `DatabaseOperaciones` | `Listar`, `Crear`, `Actualizar`, `Eliminar` |
| | `remuneraciones.package_RM_ProvMargen` | `DatabaseOperaciones` | `Listar`, `Crear`, `Actualizar`, `Eliminar` |
| **FiniquitosController** | `package.__Finiquitos` | `DatabaseOperaciones` | `ConsultaSimulaciones`, `ConsultaFiniquitos`, `ComplementoCrear`, `ValidarFiniquito`, `TerminarFiniquito`, `SolicitudTEF`, `PagarFiniquito`, etc. |
| **FinanzasController** | `finanzas.package_FZ_ValorDiario` | `DatabaseOperaciones` | `Crear`, `Listar`, `Actualizar`, `Eliminar` |
| **ExcelController** | `remuneraciones.package_RM_Excel` | `DatabaseOperaciones` | `ReporteCargoMod` (Generación de Excel) |
| | `package.__Excel` | `DatabaseOperaciones` | `ReporteFiniquitosProcesoMasivo`, `ReporteSegurosCovid` |
| **PDFController** | `Remuneraciones.package_RM_PDF` | `DatabaseOperaciones` | `EstructuraCargoMod` |

### 2.2. Módulo Asistencia y Turnos

| Controlador | Stored Procedure Identificado | Base de Datos (Config Key) | Acciones / Métodos Detectados |
| :--- | :--- | :--- | :--- |
| **AsistenciaController** | `package.__KM__Asistencia` | `DatabaseOperaciones` | **Masivo:** `AsistenciaCrear`, `ObtenerLicencias`, `ObtenerVacaciones`, `CierrePeriodoCrear`, `BonosIngresar`, `HorasExtraIngresar`, `JornadaLaboralIngresar`, `ArchivoAsistenciaCrear`, `FichaJornadaIngresar` |

### 2.3. Módulo Selección y Reclutamiento (Portal)

| Controlador | Stored Procedure Identificado | Base de Datos (Config Key) | Acciones / Métodos Detectados |
| :--- | :--- | :--- | :--- |
| **SeleccionController** | `package.__Postulante` | `DatabaseSeleccion` | **Gigante:** `ListarPostulaciones`, `CreaOActualizaFichaPostulante`, `Postular`, `CrearOfertaLaboral`, `CerrarOfertaLaboral`, `MoverPostulante...`, `ListarEtapas`, `BaneoPostulante`, `LoginOrganic`, `SignUp`, `Directorios...` |
| | `package.Auth` | `DatabaseSeleccion` | `CodigoOAuthCrear`, `CodigoOAuthValidar`, `IdentifyUser` |
| **PortalPostulantesController**| `package.__PortalPostulantes` | `DatabaseSeleccion` | `PostulanteObtener` |
| **PersonaController** | `package.__Persona` | `DatabaseOperaciones` | `ConsultarCliente`, `CrearCliente`, `EliminarCliente`, `ListarPolizasCovid`, `Dashboard` |
| **KamController** | `package.__Kam` | `DatabaseOperaciones` | `ConsultaFichaPersonal`, `ConsultaContrato`, `ConsultaRenovacion`, `ConsultaBaja` |

### 2.4. Módulo Configuración y Auth

| Controlador | Stored Procedure Identificado | Base de Datos (Config Key) | Acciones / Métodos Detectados |
| :--- | :--- | :--- | :--- |
| **AuthController** | `SP_API_SIGNIN` | `Database` | Login de usuario API (Valida credenciales) |
| **TokenController** | `package.auth` | `Database` | `CrearTokenConfianza` |
| **ConfigController** | `package.Config` | `Database` | `GroupAplicaciones`, `MenuAplicaciones`, `PermisionAplicacion`, `IdentifyUser`, `Notificaciones` |
| **TeamworkController** | `SP_CRUD_TW_MANTENEDORNOTICIASTEAMWORK` | `DatabaseOperaciones` |Gestión de noticias (`VIEWNOTICIAS`, etc.) |

### 2.5. Módulo Carga Masiva (Backoffice)

| Controlador | Stored Procedure Identificado | Base de Datos (Config Key) | Acciones / Métodos Detectados |
| :--- | :--- | :--- | :--- |
| **CargaMasivaController** | `package.__KM__CargaMasiva` | `DatabaseOperaciones` | **Workflow de Carga:** `PlantillaSubir`, `PlantillaValidarCarga...` (Contrato, Renovacion, Postular, Nomina, Bajas, Asistencia), `PlantillaCrear...` (Ejecuta la carga real), `SolicitudAnular...` |

### 2.6. Módulos Especializados

| Controlador | Stored Procedure Identificado | Base de Datos (Config Key) | Acciones / Métodos Detectados |
| :--- | :--- | :--- | :--- |
| **SoftlandController** | `dbo.package_TW_CargoMod` | `DatabaseSoftland` (Dinámica) | `Crear`, `Actualizar`, `Eliminar` cargos en ERP Softland. |
| **CoinsController** | `package.Formularios` | `DBCoins` | `ListarFormularios`, `CrearFormulario`, `CrearItemFlow`, `IniciarFlow` |
| | `package.Inventario` | `DBCoins` | `CrearEquipo`, `CrearItemData` |

## 3. Conclusiones para Mapeo de BD
Para realizar ingeniería inversa de la base de datos, se debe priorizar la extracción de código de los siguientes Paquetes Clave, que concentran el 90% de la lógica del sistema:

1.  `package.__Finiquitos` (Lógica crítica legal)
2.  `remuneraciones.package_RM_CargoMod` (Simulador de costos)
3.  `package.__Postulante` (Motor de Selección)
4.  `package.__KM__Asistencia` (Gestión de Tiempos)
5.  `package.__KM__CargaMasiva` (Procesador Batch)
