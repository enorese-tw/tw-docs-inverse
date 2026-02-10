# solicitudBajaCONSULTORA.aspx.cs

## Información General

| Campo | Valor |
|---|---|
| **Archivo** | `FiniquitosV2/solicitudBajaCONSULTORA.aspx.cs` |
| **Namespace** | `FiniquitosV2` |
| **Clase** | `solicitudBajaCONSULTORA : System.Web.UI.Page` |
| **Líneas de código** | 168 |
| **Dependencias clave** | `FiniquitosV2.Clases`, `Newtonsoft.Json` |

## Descripción Funcional

Formulario de solicitud de baja para la empresa **Consultora**. Funcionalmente idéntico a `solicitudBajaOUT` — usa WCF para búsqueda de contratos y registro de solicitudes.

## Métodos

### `Page_Load(object sender, EventArgs e)`
- Mismo patrón de control de acceso por rol.

### `buscar_Click(object sender, EventArgs e)`
- Usa `MethodServiceFiniquitos` con empresa `CONSULTORA`.
- Llama a `web.GetContratoPersonaService`.

### `solicitar_Click(object sender, EventArgs e)`
- Registra solicitud con empresa `TWCONSULTORA` vía `svcFiniquitos.SetSolicitudBaja()`.

### `[WebMethod] GetUserRole()`
- Retorna `Session["Tipo"]` como JSON.

## Llamadas a Servicios WCF

| Método | Propósito |
|---|---|
| `GetContratoPersonaService` | Busca contratos (empresa: CONSULTORA) |
| `GetCausal()` | Carga causales de despido |
| `SetSolicitudBaja()` | Registra solicitud de desvinculación |

## Observaciones

> [!NOTE]
> Este archivo es casi un clon de `solicitudBajaOUT.aspx.cs`. La única diferencia es la empresa (`CONSULTORA` vs `OUT`). Candidato ideal para refactorización en una sola clase parametrizada.
