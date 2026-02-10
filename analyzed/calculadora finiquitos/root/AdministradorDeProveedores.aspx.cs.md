# AdministradorDeProveedores.aspx.cs

## Información General

| Campo | Valor |
|---|---|
| **Archivo** | `FiniquitosV2/AdministradorDeProveedores.aspx.cs` |
| **Namespace** | `FiniquitosV2` |
| **Clase** | `AdministradorDeProveedores : System.Web.UI.Page` |
| **Líneas de código** | 57 |
| **Dependencias clave** | `FiniquitosV2.Clases`, `Newtonsoft.Json` |

## Descripción Funcional

Página de **administración del catálogo de proveedores**. Permite listar los proveedores registrados en el sistema. Es una página administrativa mínima.

## Métodos

### `Page_Load(object sender, EventArgs e)`
- Control de acceso estándar por `Session["Tipo"]`.
- Mismas redirecciones que otras páginas administrativas.

### `[WebMethod] GetObtenerProveedoresProveedorService()`
- **Funcionalidad**: Obtiene la lista completa de proveedores.
- **Retorna**: JSON con datos de proveedores vía `MethodServiceFiniquitos.GetObtenerProveedoresProveedorService`.

## Llamadas a Servicios WCF

| Propiedad | Propósito |
|---|---|
| `GetObtenerProveedoresProveedorService` | Lista de proveedores registrados |

## Observaciones

> [!NOTE]
> Archivo muy simple — solo 1 WebMethod. La lógica CRUD de proveedores (crear, editar, eliminar) está en `PagoProveedores.aspx.cs` (`SetInsertarProveedorService`). Esta página solo provee la lectura/listado.

> [!TIP]
> Podría unificarse con `PagoProveedores.aspx.cs` para tener toda la gestión de proveedores en una sola página.
