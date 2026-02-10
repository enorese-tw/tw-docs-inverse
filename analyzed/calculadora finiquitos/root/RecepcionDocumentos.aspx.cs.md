# RecepcionDocumentos.aspx.cs

## Información General

| Campo | Valor |
|---|---|
| **Archivo** | `FiniquitosV2/RecepcionDocumentos.aspx.cs` |
| **Namespace** | `FiniquitosV2` |
| **Clase** | `RecepcionDocumentos : System.Web.UI.Page` |
| **Líneas de código** | 310 |
| **Dependencias clave** | `FiniquitosV2.Clases`, `ServicioCorreo` (WCF) |

## Descripción Funcional

Página para gestionar la **recepción de documentos** de finiquitos. Permite cambiar el estado de un documento de finiquito y enviar notificaciones por correo electrónico cuando se reciben documentos. Destinada a usuarios con rol tipo `3` (Recepción).

## Métodos

### `Page_Load(object sender, EventArgs e)`
- **Lógica**:
  1. Valida sesión.
  2. Control de acceso: Tipo `2` → Renuncias, Tipo `4`/`6` → Inicio, Tipo `7` → ocultar links.
  3. Tipo `3` → acceso permitido (es su página principal).

### `BtnCambiarEstado_Click(object sender, EventArgs e)`
- **Funcionalidad**: Cambia el estado de un documento de finiquito.
- **Lógica**:
  1. Llama a `svcFiniquitos.SetCambiarEstadoDesvinculacion(parametros, valores)`.
  2. Parámetros: idDesvinculacion, nuevo estado, usuario.
  3. Muestra SweetAlert con resultado.

### `btnEnviarMail_Click(object sender, EventArgs e)`
- **Funcionalidad**: Envía correo de notificación cuando se recibe un documento.
- **Servicio**: Usa `ServicioCorreo.ServicioCorreoClient` (WCF).
- **Lógica**:
  1. Construye cuerpo del correo con datos del finiquito.
  2. Llama a `svcCorreo.EnviarCorreoHTML(destinatarios, asunto, cuerpo)`.
  3. Los destinatarios están **hardcodeados** en el código.

### WebMethods

| Método | Propósito |
|---|---|
| `GetDesvinculacionesRecepcionService` | Lista finiquitos pendientes de recepción |
| `GetDesvinculacionesPorEstadoService` | Filtra finiquitos por estado |
| `SetCambiarEstadoRecepcion` | Cambia estado de recepción |
| `GetUserRole` | Retorna rol del usuario |

## Llamadas a Servicios WCF

| Servicio | Método | Propósito |
|---|---|---|
| `ServicioFiniquitosClient` | `SetCambiarEstadoDesvinculacion` | Cambiar estado de documento |
| `ServicioCorreoClient` | `EnviarCorreoHTML` | Envío de correos de notificación |

## Vulnerabilidades

> [!WARNING]
> **Destinatarios de correo hardcodeados**: Las direcciones de email están escritas directamente en el código fuente. Debería leerse de configuración o base de datos.

> [!NOTE]
> Este archivo es relativamente limpio en comparación con otros — no tiene conexiones SQL directas, todo va vía WCF.
