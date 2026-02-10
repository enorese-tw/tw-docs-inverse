# Global.asax.cs

## Información General

| Campo | Valor |
|---|---|
| **Archivo** | `FiniquitosV2/Global.asax.cs` |
| **Namespace** | `FiniquitosV2` |
| **Clase** | `Global : HttpApplication` |
| **Líneas de código** | 35 |
| **Dependencias clave** | `System.Web.Optimization`, `System.Web.Routing`, `System.Web.Security` |

## Descripción Funcional

Archivo de configuración global de la aplicación ASP.NET Web Forms. Define los eventos del ciclo de vida de la aplicación: inicio, cierre y error. Es el punto de entrada de configuración al iniciar la aplicación en IIS.

## Métodos

### `Application_Start(object sender, EventArgs e)`
- **Trigger**: Se ejecuta **una sola vez** cuando el Application Pool de IIS inicia la aplicación.
- **Lógica**:
  1. **`BundleConfig.RegisterBundles(BundleTable.Bundles)`** — Registra los bundles de CSS y JavaScript para minificación y combinación. Definido en `App_Start/BundleConfig.cs`.
  2. **`AuthConfig.RegisterOpenAuth()`** — Registra proveedores de autenticación OpenAuth (OAuth). Definido en `App_Start/AuthConfig.cs`.
  3. **`RouteConfig.RegisterRoutes(RouteTable.Routes)`** — Registra las rutas de URL amigables (Friendly URLs). Definido en `App_Start/RouteConfig.cs`.

### `Application_End(object sender, EventArgs e)`
- **Trigger**: Se ejecuta cuando la aplicación se cierra (reciclaje del Application Pool, despliegue, etc.).
- **Lógica**: **Vacío** — sin implementación. No se ejecuta lógica de limpieza.

### `Application_Error(object sender, EventArgs e)`
- **Trigger**: Se ejecuta cuando ocurre una **excepción no manejada** en cualquier parte de la aplicación.
- **Lógica**: **Vacío** — sin implementación.

## Dependencias de Configuración

| Clase de Configuración | Archivo Esperado | Propósito |
|---|---|---|
| `BundleConfig` | `App_Start/BundleConfig.cs` | Minificación y combinación de recursos estáticos |
| `AuthConfig` | `App_Start/AuthConfig.cs` | Configuración de autenticación OAuth |
| `RouteConfig` | `App_Start/RouteConfig.cs` | Rutas URL amigables (FriendlyUrls) |

## Vulnerabilidades y Observaciones

> [!WARNING]
> **`Application_Error` vacío**: Las excepciones no manejadas no se registran ni se loguean. En producción, esto significa que errores críticos podrían pasar desapercibidos. Debería al menos loguear el error en un archivo, base de datos o servicio de monitoreo. Ejemplo de implementación recomendada:
> ```csharp
> void Application_Error(object sender, EventArgs e) {
>     Exception ex = Server.GetLastError();
>     Utilidades.LogError(connectionString, "Application_Error", ex.ToString());
> }
> ```

> [!NOTE]
> **`Application_End` vacío**: Aceptable en la mayoría de aplicaciones. Solo es relevante si se necesita liberar recursos externos (conexiones persistentes, caches distribuidos, etc.).

> [!NOTE]
> La aplicación usa **FriendlyUrls** de ASP.NET (vía `RouteConfig`) y **OAuth** (vía `AuthConfig`), aunque la autenticación real observada en `Login.aspx.cs` es custom vía WCF, no OAuth.
