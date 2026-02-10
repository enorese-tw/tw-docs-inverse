# solicitudBajaOUT.aspx.cs

## Información General

| Campo | Valor |
|---|---|
| **Archivo** | `FiniquitosV2/solicitudBajaOUT.aspx.cs` |
| **Namespace** | `FiniquitosV2` |
| **Clase** | `solicitudBajaOUT : System.Web.UI.Page` |
| **Líneas de código** | 166 |
| **Dependencias clave** | `FiniquitosV2.Clases`, `Newtonsoft.Json` |

## Descripción Funcional

Formulario de solicitud de baja para la empresa **Outsourcing** (OUT). Funcionalmente idéntico a `solicitudBajaEST`, pero parametrizado para la empresa OUT. A diferencia de EST, utiliza únicamente **WCF** para buscar contratos (no hace conexión SQL directa).

## Métodos

### `Page_Load(object sender, EventArgs e)`
- Mismo patrón de control de acceso por `Session["Tipo"]` que EST.
- Carga causales de despido desde WCF.

### `buscar_Click(object sender, EventArgs e)` — Búsqueda de Trabajador
- **Diferencia clave vs EST**: Usa `MethodServiceFiniquitos` (WCF wrapper) en lugar de conexión SQL directa.
- Llama a `web.GetContratoPersonaService` pasando RUT y empresa `OUT`.
- Carga contratos en GridView.

### `solicitar_Click(object sender, EventArgs e)` — Registro de Solicitud
- Mismo flujo que EST: itera contratos seleccionados y llama a `svcFiniquitos.SetSolicitudBaja()`.
- Envía empresa `TWOUT`.

### `[WebMethod] GetUserRole()`
- Retorna `Session["Tipo"]` como JSON.

## Llamadas a Servicios WCF

| Método | Propósito |
|---|---|
| `GetContratoPersonaService` | Busca contratos del trabajador (via wrapper WCF) |
| `GetCausal()` | Carga causales de despido |
| `SetSolicitudBaja()` | Registra solicitud de desvinculación |

## Diferencias con solicitudBajaEST

| Aspecto | EST | OUT |
|---|---|---|
| Búsqueda contratos | SQL directo a Softland | WCF Service |
| Base de datos | `Tw_Est` | N/A (vía servicio) |
| Empresa enviada | `TWEST` | `TWOUT` |
| Credenciales hardcodeadas | Sí | No |

## Observaciones

> [!TIP]
> Este archivo es el patrón más limpio de los tres (EST/OUT/CONSULTORA) porque NO tiene conexión SQL directa — todo va vía WCF.
