# Módulo Asistencia - Teamwork.WebApi

**Archivo**: `Teamwork.WebApi/CallAPIAsistencia.cs`

Esta clase gestiona todas las operaciones relacionadas con la asistencia, jornadas laborales, horas extras y bonos. Consume los servicios del módulo `asistencia`.

## Tabla de Operaciones

| Método Local | Ruta Servicio API (`asistencia/`) | Descripción | Datos de Entrada (JSON) |
| :--- | :--- | :--- | :--- |
| `__AsistenciaObtener` | `Obtener` | Obtiene registros de asistencia. | `Empresa`, `Ficha`, `Fecha` |
| `__AsistenciaInsertar` | `Crear` | Inserta un nuevo registro de asistencia. | `Empresa`, `Ficha`, `Asistencia`, `Observacion`, `UsuarioCarga` |
| `__AsistenciaActualizar` | `Actualizar` | Actualiza un registro de asistencia. | `Empresa`, `Ficha`, `Fecha`, `CodigoAsistencia`, `Observacion`, `UsuarioUltModificacion` |
| `__AsistenciaEliminar` | `Eliminar` | Elimina un registro de asistencia. | `Empresa`, `Ficha`, `Fecha`, `CodigoAsistencia`, `Observacion`, `UsuarioUltModificacion` |
| `__AsistenciaLicencia` | `Licencia/Obtener` | Consulta licencias médicas. | `Empresa`, `Ficha`, `Fecha` |
| `__AsistenciaVacaciones` | `Vacaciones/Obtener` | Consulta vacaciones. | `Empresa`, `Ficha`, `Fecha` |
| `__ObtenerBajaConfirmada` | `BajasConfirmadas/Obtener` | Consulta bajas confirmadas. | `Empresa`, `Ficha`, `Fecha`, `AreaNegocio` |
| `__PersonalDataObtener` | `PersonalData/Obtener` | Obtiene datos personales básicos del empleado. | `Empresa`, `Ficha`, `Fecha`, `UsuarioCarga` |
| `__TipoAsistenciaObtener` | `TipoAsistencia/Obtener` | Listado de tipos de asistencia. | *Sin parámetros* |
| `__LeyendaObtener` | `Leyenda/Obtener` | Obtiene leyendas de asistencia. | *Sin parámetros* |
| `__AsistenciaReporte` | `Reporte` | Genera reporte de asistencia. | `Empresa`, `Fecha` |

### Operaciones V2 y Avanzadas

| Método Local | Ruta Servicio API (`asistencia/`) | Descripción | Datos de Entrada (JSON) |
| :--- | :--- | :--- | :--- |
| `__AreaNegocioObtener` | `AreaNegocio/Obtener` | Obtiene áreas de negocio. | `Empresa`, `UsuarioCarga`, `CodigoAsistencia` |
| `__PersonalDataObtenerV2` | `V2PersonalData/Obtener` | Datos personales con paginación. | `Empresa`, `Ficha`, `Fecha`, `AreaNegocio`, `UsuarioCarga`, `Pagination` |
| `__AsistenciaObtenerV2` | `V2Obtener` | Obtención de asistencia V2. | `Empresa`, `Ficha`, `Fecha`, `AreaNegocio`, `UsuarioCarga`, `Pagination` |
| `__AsistenciaLicenciaV2` | `V2Licencia/Obtener` | Licencias V2. | *Mismos que V2Obtener* |
| `__AsistenciaVacacionesV2` | `V2Vacaciones/Obtener` | Vacaciones V2. | *Mismos que V2Obtener* |
| `__AsistenciaInsertarV2` | `V2Crear` | Inserción V2. | `Empresa`, `Ficha`, `Asistencia`, `Observacion`, `UsuarioCarga` |
| `__AsistenciaReporteV2` | `V2Reporte` | Reporte V2. | `Empresa`, `Ficha`, `Fecha`, `AreaNegocio`, `UsuarioCarga` |
| `__AsistenciaPagination` | `Pagination/Obtener` | Paginación genérica de asistencia. | `Empresa`, `Ficha`, `Fecha`, `AreaNegocio`, `UsuarioCarga`, `Pagination`, `TypePagination` |

### Cierre de Periodos y Archivos

| Método Local | Ruta Servicio API (`asistencia/`) | Datos de Entrada (JSON) |
| :--- | :--- | :--- |
| `__AsistenciaObtenerCierrePeriodo` | `CierrePeriodo/Obtener` | `Empresa`, `Fecha`, `AreaNegocio` |
| `__AsistenciaInsertarCierrePeriodo` | `CierrePeriodo/Crear` | `Empresa`, `Fecha`, `AreaNegocio`, `UsuarioCarga` |
| `__AsistenciaObtenerPeriodos` | `CierrePeriodo/ObtenerPeriodos` | `Fecha`, `FechaCreacion`, `Empresa`, `AreaNegocio` |
| `__AsistenciaInsertarExcepcionPeriodo` | `ExcepcionPeriodo/Crear` | `Empresa`, `Fecha`, `AreaNegocio`, `Excepion`, `UsuarioCarga` |
| `__AsistenciaObtenerArchivosAsistencia` | `ArchivosAsistencia/Obtener` | `Empresa`, `Ficha`, `AreaNegocio`, `Fecha` |
| `__AsistenciaInsertarArchivosAsistencia` | `ArchivosAsistencia/Crear` | `Empresa`, `Ficha`, `AreaNegocio`, `Fecha`, `UsuarioCarga`, `CodigoRuta`, `NombreArchivo`, `NombreRealArchivo`, `Extension` |

### Jornada Laboral y Horas Extras

Incluye lógica para gestionar jornadas, fichas de jornada y horas extras. Rutas: `JornadaLaboral/Obtener`, `JornadaLaboral/Actualizar`, `JornadaLaboral/Ingresar`, `HorasExtra/Ingresar`, `HorasExtra/Obtener`.

### Bonos y Bonos Clientes

Operaciones CRUD sobre bonos asignados a empleados y clientes. Rutas: `Bonos/Obtener`, `Bonos/Ingresar`, `BonoCliente/Obtener`, `FichaBonos/Ingresar`, etc.

**Observaciones**:
- La mayoría de los métodos devuelven un **string JSON** directamente desde la API.
- Se hace uso intensivo de `HelperCallAPI`.
