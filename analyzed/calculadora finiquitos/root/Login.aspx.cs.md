# Login.aspx.cs

## Información General

| Campo | Valor |
|---|---|
| **Archivo** | `FiniquitosV2/Login.aspx.cs` |
| **Namespace** | `FiniquitosV2` |
| **Clase** | `Login : System.Web.UI.Page` |
| **Líneas de código** | 86 |
| **Dependencias clave** | `FiniquitosV2.Clases`, `System.Data` |

## Descripción Funcional

Página de inicio de sesión del sistema. Autentica usuarios contra una base de datos SQL Server a través del servicio WCF `ServicioFiniquitos`. Maneja dos tipos de autenticación: login directo y login por token (redireccionamiento desde otros sistemas).

## Métodos

### `Page_Load(object sender, EventArgs e)`
- **Trigger**: Carga de la página.
- **Lógica**:
  1. Si el query string contiene `token`, llama a `autenticarToken()`.
  2. Si no, carga normalmente la página de login.

### `ingresar_Click(object sender, EventArgs e)`
- **Trigger**: Click en botón de ingreso.
- **Lógica**:
  1. Valida credenciales vía `svcFiniquitos.GetLogin(parametros, valores)`.
  2. Si `VALIDACION == 0`, establece variables de sesión:
     - `Session["Usuario"]` — nombre del usuario
     - `Session["Tipo"]` — tipo/rol de usuario (controla permisos y redirecciones en todo el sistema)
  3. Redirige a `Inicio.aspx`.
  4. Si falla, muestra SweetAlert con error.

### `autenticarToken()`
- **Trigger**: Llamado desde `Page_Load` cuando existe un token en query string.
- **Lógica**:
  1. Extrae el `token` del query string.
  2. Valida el token mediante `svcFiniquitos.GetAutorizacionPorToken(parametros, valores)`.
  3. Si es válido, establece las mismas variables de sesión y redirige a `Inicio.aspx`.

## Llamadas a Servicios WCF

| Servicio | Método | Propósito |
|---|---|---|
| `ServicioFiniquitosClient` | `GetLogin` | Autenticación por usuario y contraseña |
| `ServicioFiniquitosClient` | `GetAutorizacionPorToken` | Autenticación por token |

## Variables de Sesión Establecidas

| Variable | Tipo | Descripción |
|---|---|---|
| `Session["Usuario"]` | `string` | Nombre/identificador del usuario autenticado |
| `Session["Tipo"]` | `string` | Tipo de usuario (1=Admin, 2=Op. Renuncias, 3=Recepción, 4/6=Básico, 7=Finanzas) |

## Vulnerabilidades y Observaciones

> [!CAUTION]
> **Sin protección contra fuerza bruta**: No hay mecanismo de bloqueo por intentos fallidos.

> [!WARNING]
> **Token en query string**: El token se transmite como parámetro URL, lo que lo expone en logs del servidor y historial del navegador.

> [!NOTE]
> El servicio WCF actúa como capa intermedia antes de la base de datos, lo cual es positivo respecto a inyección SQL directa.
