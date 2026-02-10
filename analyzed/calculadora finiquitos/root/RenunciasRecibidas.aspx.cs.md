# RenunciasRecibidas.aspx.cs

## Información General

| Campo | Valor |
|---|---|
| **Archivo** | `FiniquitosV2/RenunciasRecibidas.aspx.cs` |
| **Namespace** | `FiniquitosV2` |
| **Clase** | `RenunciasRecibidas : System.Web.UI.Page` |
| **Líneas de código** | 113 |
| **Dependencias clave** | `System.Web.Services`, `Newtonsoft.Json`, `FiniquitosV2.Clases` |

## Descripción Funcional

Página para gestionar las **renuncias recibidas**. Página principal para usuarios con rol tipo `2` (Operadores de Renuncias). Muestra un listado de renuncias y permite cambiar estados vía AJAX.

## Métodos

### `Page_Load(object sender, EventArgs e)`
- **Control de acceso**:
  - Tipo `2` → acceso permitido (su página principal)
  - Tipo `3` → redirigir a RecepcionDocumentos
  - Tipo `4`/`6` → redirigir a Inicio
  - Tipo `7` → ocultar links

### WebMethods

| Método | Propósito |
|---|---|
| `GetDesvinculacionesService()` | Obtiene todas las desvinculaciones/renuncias |
| `SetEditarRenuncia(...)` | Edita datos de una renuncia (fecha firma, estado) |
| `GetUserRole()` | Retorna rol del usuario |

## Llamadas a Servicios WCF

Todos los WebMethods usan `MethodServiceFiniquitos` como wrapper:

| Propiedad/Método | Propósito |
|---|---|
| `GetDesvinculacionesService` | Lista de renuncias |
| `SetEditarRenunciaService` | Actualizar renuncia |
| `GetUserRoleService` | Obtener rol |

## Observaciones

> [!NOTE]
> Archivo simple y limpio. Toda la lógica está en los servicios WCF. El code-behind solo actúa como proxy JSON para las llamadas AJAX del frontend.
