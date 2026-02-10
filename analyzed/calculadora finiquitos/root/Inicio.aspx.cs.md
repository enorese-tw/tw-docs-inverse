# Inicio.aspx.cs

## Información General

| Campo | Valor |
|---|---|
| **Archivo** | `FiniquitosV2/Inicio.aspx.cs` |
| **Namespace** | `FiniquitosV2` |
| **Clase** | `Inicio : System.Web.UI.Page` |
| **Líneas de código** | 2054 |
| **Dependencias clave** | `FiniquitosV2.Clases`, `Finiquitos.Clases`, `OfficeOpenXml`, `iTextSharp`, `System.Data.SqlClient`, `System.Drawing` |

## Descripción Funcional

Página principal del sistema tras login. Es la más grande del proyecto y concentra múltiples funcionalidades:
- **Dashboard** con grillas de finiquitos por estado (pendientes, calculados, pagados)
- **Generación de cheques** en formato Excel (Banco Chile/Edwards City)
- **Exportación a Excel** de reportes de finiquitos
- **Gestión de estados** de documentos de finiquitos
- **Comunicación con Softland** vía conexión SQL directa para consultas de remuneraciones

## Métodos Principales

### `Page_Load(object sender, EventArgs e)`
- **Lógica**:
  1. Valida sesión — si `Session["Usuario"]` es null, redirige a `Login.aspx`.
  2. Control de acceso por `Session["Tipo"]`:
     - Tipo `2` → redirige a `RenunciasRecibidas.aspx`
     - Tipo `3` → redirige a `RecepcionDocumentos.aspx`
     - Tipo `4` / `6` → oculta links de menú (solo acceso básico)
     - Tipo `7` → oculta renuncias, solicitudes y cálculos
     - Tipo `1` → acceso completo

### WebMethods (API AJAX — 17 métodos estáticos)

| Método | Propósito |
|---|---|
| `GetDesvinculacionesPendientesService` | Lista desvinculaciones pendientes |
| `GetDesvinculacionesRecientesService` | Lista desvinculaciones recientes |
| `GetDesvinculacionesService` | Busca desvinculaciones por filtro |
| `SetEditarDesvinculacion` | Actualiza datos de desvinculación |
| `GetCausalService` | Obtiene causales de despido |
| `SetCambiarEstadoDesvinculacion` | Cambia estado de una desvinculación |
| `SetFinalizarDesvinculacion` | Finaliza y cierra una desvinculación |
| `SetRegistrarFechaFirmaDesvinculacion` | Registra fecha de firma |
| `GetObtenerCorrelativoDisponible` | Obtiene siguiente correlativo de cheque disponible |
| `SetTMPCargaChequeFiniquito` | Carga datos temporales de cheque en cola de impresión |
| `SetTMPInitProcessCheque` | Inicializa proceso de generación de cheques |
| `GetTMPValidateProcessInit` | Valida si hay cheques en proceso |
| `GetTMPChequesInProcess` | Obtiene cheques actualmente en proceso |
| `GetMontoCifra` | Convierte monto numérico a texto (cifra en letras) |
| `GetContratoPersona` | Busca contratos de una persona en Softland |
| `GetUserRole` | Obtiene rol del usuario actual |
| `GetListadoEstados` | Lista estados disponibles para filtrado |

### `SetTMPCloseProcessCheque()` — Generación de Cheques Excel
- **Líneas**: ~500 líneas de lógica
- **Funcionalidad**: Genera documento Excel con formato de cheque para impresión en impresora matricial OKI DATA CORP ML320/1TURBO.
- **Proceso**:
  1. Consulta cheques pendientes vía `SetTMPCloseProcessChequeService`.
  2. Para cada cheque, crea una página de 92 filas con layout de cheque bancario.
  3. Formatea montos con separadores de miles espaciados para el formato de cheque.
  4. Incluye: monto, ciudad, fecha, nombre beneficiario, cifra en letras, protección de monto.
  5. Soporta cheques nominativos (con línea negra de anulación).
  6. Configura márgenes y área de impresión para impresora matricial.
  7. Protege la hoja Excel con contraseña.
  8. Guarda copia en servidor de archivos `\\cranky\FileServer\Sistemas\Ficha_Alta\Cheques\`.

### `BtnImportarExcel(Object sender, EventArgs e)` — Exportación a Excel
- **Funcionalidad**: Genera reporte Excel con datos de finiquitos filtrados.
- **Columnas**: Empleado, fechas contrato, empresa, rol, montos (finiquito, IAS, desahucio, indemnización, vacaciones), cliente, fecha pago, tipo pago, banco, estado.

## Conexiones a Base de Datos

### Conexión Directa SQL (Softland)

```csharp
string connectionString = "Data Source=conectorsoftland.team-work.cl\\SQL2017;Initial Catalog={0};Persist Security Info=True;User ID=Sa;Password=Softland070";
```

> [!CAUTION]
> **Credenciales hardcodeadas**: Usuario `Sa` con contraseña `Softland070` en texto plano dentro del código fuente. Este es un riesgo crítico de seguridad.

- **Uso**: Consulta de contratos en bases de datos Softland (el catálogo varía según empresa: `Tw_Est`, `Tw_Out`, `TW_CONSULTORA`).

### Conexión WCF (Servicio Finiquitos)

Todos los WebMethods usan `MethodServiceFiniquitos` (wrapper de WCF).

## Servidor de Archivos

| Ruta UNC | Propósito |
|---|---|
| `\\cranky\FileServer\Sistemas\Ficha_Alta\Cheques\` | Almacenamiento de cheques generados (subdirectorio por fecha) |

## Variables de Sesión Leídas

| Variable | Uso |
|---|---|
| `Session["Usuario"]` | Identificar usuario logueado |
| `Session["Tipo"]` | Control de acceso por rol |
| `Session["nombretrabajadorFiniquito"]` | Nombre del trabajador seleccionado |
| `Session["iddesvinculacionFiniquito"]` | ID de desvinculación seleccionada |
| `Session["empresaFiniquito"]` | Empresa seleccionada (TWEST/TWOUT/TWCONSULTORA) |

## Vulnerabilidades y Observaciones

> [!CAUTION]
> **Credenciales SQL hardcodeadas**: Contraseña de SQL Server `Sa` en texto plano. Debe moverse a `Web.config` con cifrado.

> [!CAUTION]
> **Contraseña de protección Excel hardcodeada**: `Satw.2018` — visible en código fuente.

> [!WARNING]
> **Ruta UNC hardcodeada**: Dependencia del servidor `\\cranky`. Si el servidor cambia de nombre, la funcionalidad de cheques falla.

> [!WARNING]
> **Archivo monolítico**: 2054 líneas en un solo archivo. Debería separarse en controladores más pequeños por funcionalidad (cheques, reportes, dashboard).

> [!NOTE]
> La clase `Convertidor` se usa para convertir montos numéricos a cifra en letras (ej: "CIENTO VEINTITRÉS MIL PESOS").
