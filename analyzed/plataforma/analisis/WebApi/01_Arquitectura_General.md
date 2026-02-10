# Arquitectura General - Teamwork.WebApi

Esta capa actúa como un **Proxy Client** o capa de servicio que encapsula la comunicación con una API REST externa. No define controladores propios (en el sentido de recibir peticiones HTTP directas desde el exterior como un servidor), sino que expone métodos estáticos para consumir servicios remotos.

## 1. Configuración Base

La URL base de la API se obtiene del archivo de configuración `Web.config` (o `app.config`) a través de la clave `UrlApiRest`.

- **Clase Helper**: `Teamwork.WebApi.HelperCallAPI`
- **Responsabilidad**: Centralizar la creación y ejecución de las peticiones HTTP (`HttpWebRequest`).
- **Manejo de Errores**: Captura excepciones `WebException` (específicamente errores de protocolo como 400, 401, 500) y retorna el cuerpo de la respuesta de error.

## 2. Autenticación (`Auth/Authenticate.cs`)

El sistema utiliza un mecanismo de autenticación basado en Tokens.

### Servicio: `Authenticate`

- **Archivo**: `Teamwork.WebApi/Auth/Authenticate.cs`
- **Método**: `__Authenticate()`
- **Ruta del Servicio**: `/auth/authenticate`
- **Método HTTP**: `POST`
- **Datos de Entrada**:
    - `Username`: 'APIInOperaciones' (Valor estático en código)
    - `Password`: 'QVBJSW5PcGVyYWNpb25lcw==' (Valor estático en código)
- **Datos de Salida**: JSON con el Token de autenticación.
- **Descripción**: Obtiene un token de acceso para realizar operaciones subsiguientes. Las credenciales están hardcodeadas en la clase.

## 3. Helper de Llamadas (`HelperCallAPI.cs`)

Esta clase contiene métodos genéricos para realizar las peticiones a los distintos módulos de la API. Cada método añade el encabezado `Authorization: Base {token}`.

**Métodos Principales:**

| Método Helper | Prefijo de Ruta | Descripción |
| :--- | :--- | :--- |
| `__CallAPICargaMasivaReporte` | `cargamasiva/Reporte/` | Reportes de cargas masivas. |
| `__CallAPICargaMasivaPlantilla` | `cargamasiva/Plantilla/` | Gestión de plantillas de carga. |
| `__CallAPICargaMasivaProceso` | `cargamasiva/Proceso/` | Ejecución de procesos masivos. |
| `__CallAPICargaMasivaSolicitud` | `cargamasiva/Solicitud/` | Gestión de solicitudes masivas. |
| `__CallAPICargaMasivaPagination` | `cargamasiva/Pagination/` | Paginación de cargas masivas. |
| `__CallAPISeleccionPostulante` | `seleccion/Postulante/` | Gestión de postulantes. |
| `__CallAPIExcelReporte` | `excel/Reporte/` | Generación de reportes Excel. |
| `__CallAPIAsistencia` | `asistencia/` | Módulo de asistencia. |
| `__CallAPISeleccionAuth` | `seleccion/Auth/` | Autenticación específica de selección. |
| `__CallAPIFiniquitos` | `finiquitos/` | Módulo de finiquitos. |
| `__CallAPIPersona` | `persona/` | Gestión de personas/clientes. |
| `__CallAPIKamConsulta` | `kam/Consultar/` | Consultas KAM. |
| `__CallAPIToken` | `token/` | Gestión de tokens de confianza. |

**Nota Importante**: Todos los métodos configuran un `Timeout` de 1,000,000 ms (aprox 16 mins), indicando que se esperan operaciones de larga duración.
