# BasePagosFiniquitos.aspx.cs

## Información General

| Campo | Valor |
|---|---|
| **Archivo** | `FiniquitosV2/BasePagosFiniquitos.aspx.cs` |
| **Namespace** | `FiniquitosV2` |
| **Clase** | `BasePagosFiniquitos : System.Web.UI.Page` |
| **Líneas de código** | 314 |
| **Dependencias clave** | `FiniquitosV2.Clases`, `Finiquitos.Clases`, `OfficeOpenXml`, `Newtonsoft.Json` |

## Descripción Funcional

Página de **base de datos de pagos de finiquitos**. Permite consultar todos los pagos registrados con filtros avanzados y exportarlos a un archivo Excel. Es una página de reportería y consulta.

## Métodos

### `Page_Load(object sender, EventArgs e)`
- **Control de acceso idéntico** al patrón estándar:
  - Tipo `2` → Renuncias, Tipo `3` → Recepción, Tipo `4`/`6` → Inicio, Tipo `7` → links ocultos.

### `BtnImportarExcel(Object sender, EventArgs e)` — Exportación Excel
- **Funcionalidad**: Genera un reporte Excel con todos los pagos de finiquitos.
- **Lógica**:
  1. Lee `Session["applicationGenerateDocument"]` para determinar qué exportar.
  2. Usa `MethodServiceFiniquitos.GetObtenerPagosService` con tipo `FINIQUITOS`.
  3. Filtra por `Session["applicationFiltrarPor"]`, `applicationFiltro`, `applicationFiltro2`, `applicationFiltro3`.
  4. Crea una hoja por empresa (`BASE FINIQUITOS + empresa`).
  5. **Columnas del reporte** (19):

| Col | Nombre |
|-----|--------|
| A | EMPLEADO |
| B | FECHA INICIO CONTRATO |
| C | FECHA TERMINO CONTRATO |
| D | EMPRESA TW |
| E | ROL PRIVADO |
| F | MONTO FINIQUITO |
| G | IAS |
| H | MES DE AVISO |
| I | INDEMNIZACION VOLUNTARIA |
| J | VACACIONES |
| K | CLIENTE |
| L | AREA NEGOCIO |
| M | FECHA |
| N | TIPO DE PAGO |
| O | BANCO |
| P | EMPRESA TW |
| Q | N° DE CHEQUE |
| R | ESTADO DE PAGO |
| S | NOMENCLATURA |

  6. Mapea estados: `DISPONIBLE A GENERAR` → `DISPONIBLE PARA IMPRESIÓN`, `GENERADO` → `DISPONIBLE PARA PAGO`.
  7. Descarga el archivo `.xlsx` al navegador.

### WebMethods

| Método | Propósito |
|---|---|
| `GetObtenerPagosService(filtrarPor, filtro, filtro2, filtro3)` | Consulta pagos con filtros, guarda filtros en sesión |
| `GetObtenerClientePagosService()` | Lista clientes para filtrado |
| `GetObtenerTrabajadoresPagosService()` | Lista trabajadores para filtrado |

## Observaciones

> [!WARNING]
> **Código condicional redundante**: Las ramas `if/else` para montos vacíos (líneas 201-244) ejecutan exactamente el mismo código en ambos casos. Es código muerto que no aporta lógica diferenciada.

> [!NOTE]
> La exportación crea múltiples hojas agrupadas por empresa, lo cual es una buena práctica para reportes multi-empresa.
