# TokenAuth.aspx.cs

## Información General

| Campo | Valor |
|---|---|
| **Archivo** | `FiniquitosV2/TokenAuth.aspx.cs` |
| **Namespace** | `FiniquitosV2` |
| **Clase** | `TokenAuth : System.Web.UI.Page` |
| **Líneas de código** | 26 |
| **Dependencias clave** | `FiniquitosV2.Clases`, `System.Web.Services`, `Newtonsoft.Json` |

## Descripción Funcional

Endpoint de autenticación por token. Provee un `WebMethod` estático accesible via AJAX para generar tokens de autenticación desde sistemas externos.

## Métodos

### `Page_Load(object sender, EventArgs e)`
- **Trigger**: Carga de la página.
- **Lógica**: Vacío — sin lógica de carga.

### `[WebMethod] GetTokenAuthService(string user)`
- **Tipo**: `static` — expuesto como servicio web AJAX.
- **Parámetros**: `user` — nombre de usuario que solicita el token.
- **Lógica**:
  1. Crea instancia de `MethodServiceFiniquitos`.
  2. Asigna `web.USUARIO = user`.
  3. Llama a `web.GetTokenAuthService` (propiedad del wrapper WCF).
  4. Serializa el resultado a JSON con `Newtonsoft.Json`.
- **Retorna**: JSON con datos del token generado.

## Llamadas a Servicios WCF

| Servicio | Método/Propiedad | Propósito |
|---|---|---|
| `MethodServiceFiniquitos` | `GetTokenAuthService` | Generar token de autenticación para un usuario |

## Observaciones

> [!WARNING]
> **Sin validación de origen**: El WebMethod no valida quién puede generar tokens. Cualquier petición AJAX podría solicitar un token para cualquier usuario.

> [!NOTE]
> Este endpoint se usa en conjunto con `Login.aspx.cs` para el flujo de autenticación por token desde sistemas externos.
