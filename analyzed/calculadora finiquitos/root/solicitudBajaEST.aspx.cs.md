# solicitudBajaEST.aspx.cs

## Información General

| Campo | Valor |
|---|---|
| **Archivo** | `FiniquitosV2/solicitudBajaEST.aspx.cs` |
| **Namespace** | `FiniquitosV2` |
| **Clase** | `solicitudBajaEST : System.Web.UI.Page` |
| **Líneas de código** | 242 |
| **Dependencias clave** | `FiniquitosV2.Clases`, `System.Data.SqlClient`, `Newtonsoft.Json` |

## Descripción Funcional

Formulario de solicitud de baja (desvinculación) para la empresa **EST Service**. Permite buscar trabajadores en la base de datos Softland, visualizar sus contratos y registrar una solicitud de desvinculación con causal de despido.

## Métodos

### `Page_Load(object sender, EventArgs e)`
- **Lógica**:
  1. Valida sesión — si `Session["Usuario"]` es null, redirige a `Login.aspx`.
  2. Control de acceso por `Session["Tipo"]`:
     - Tipo `2` → redirige a `RenunciasRecibidas.aspx`
     - Tipo `3` → redirige a `RecepcionDocumentos.aspx`
     - Tipo `4`/`6` → redirige a `Inicio.aspx`
     - Tipo `7` → oculta links de menú
  3. Carga el DropDownList de causales de despido vía `svcFiniquitos.GetCausal()`.
  4. **Solo en primer load** (no PostBack): carga causales.

### `buscar_Click(object sender, EventArgs e)` — Búsqueda de Trabajador
- **Funcionalidad**: Busca contratos de un trabajador por RUT en Softland.
- **Conexión SQL directa**:
  ```
  Data Source=conectorsoftland.team-work.cl\SQL2017;Initial Catalog=Tw_Est;User ID=Sa;Password=Softland070
  ```
- **Lógica**:
  1. Crea instancia de `Clases.Contrato`.
  2. Usa `contrato.validarPersonaExistente(connectionString)` para verificar si el RUT existe.
  3. Si existe, carga datos del trabajador en campos del formulario.
  4. Llama a `contrato.buscarContratos(connectionString)` para obtener contratos.
  5. Muestra contratos en `GridView1`.

### `solicitar_Click(object sender, EventArgs e)` — Registro de Solicitud
- **Funcionalidad**: Registra la solicitud de baja para los contratos seleccionados.
- **Lógica**:
  1. Valida que haya una causal seleccionada y contratos chequeados.
  2. Itera por las filas del GridView con CheckBox seleccionado.
  3. Para cada contrato seleccionado, llama a `svcFiniquitos.SetSolicitudBaja(parametros, valores)`.
  4. Parámetros enviados: checklist datos personales, causal, empresa, usuario.
  5. Muestra SweetAlert con resultado.

### `[WebMethod] GetUserRole()` — Rol de Usuario
- Retorna `Session["Tipo"]` como JSON para consultas AJAX.

## Conexiones a Base de Datos

### SQL Directo (Softland)

| Servidor | Catálogo | Credenciales |
|---|---|---|
| `conectorsoftland.team-work.cl\SQL2017` | `Tw_Est` | `Sa` / `Softland070` |

### WCF (Servicio Finiquitos)

| Método | Propósito |
|---|---|
| `GetCausal()` | Carga causales de despido |
| `SetSolicitudBaja()` | Registra solicitud de desvinculación |

## Vulnerabilidades

> [!CAUTION]
> **Credenciales SQL hardcodeadas**: Mismo patrón de credenciales `Sa`/`Softland070` en texto plano.

> [!WARNING]
> **Sin validación de RUT**: No se valida el formato ni dígito verificador del RUT ingresado.
