# BasePagoProveedores.aspx.cs

## Información General

| Campo | Valor |
|---|---|
| **Archivo** | `FiniquitosV2/BasePagoProveedores.aspx.cs` |
| **Namespace** | `FiniquitosV2` |
| **Clase** | `BasePagoProveedores : System.Web.UI.Page` |
| **Líneas de código** | 206 |
| **Dependencias clave** | `FiniquitosV2.Clases`, `OfficeOpenXml`, `Newtonsoft.Json` |

## Descripción Funcional

Página de **base de datos de pagos a proveedores**. Permite consultar todos los pagos a proveedores con filtros y exportarlos a Excel. Es la contraparte de `BasePagosFiniquitos.aspx.cs` pero para proveedores.

## Métodos

### `Page_Load(object sender, EventArgs e)`
- Control de acceso estándar por `Session["Tipo"]`.

### `BtnImportarExcel(Object sender, EventArgs e)` — Exportación Excel
- **Funcionalidad**: Genera reporte Excel de pagos a proveedores.
- **Columnas del reporte** (12):

| Col | Nombre |
|-----|--------|
| A | PROVEEDOR |
| B | RUT PROVEEDOR |
| C | TIPO PROVEEDOR |
| D | OBSERVACION |
| E | MONTO |
| F | FECHA |
| G | TIPO DE PAGO |
| H | BANCO |
| I | EMPRESA TW |
| J | N° DE CHEQUE |
| K | ESTADO DE PAGO |
| L | NOMENCLATURA |

- Crea hojas agrupadas por empresa (`BASE PROVEEDORES + empresa`).
- Estado siempre marcado como `PAGADO`.

### WebMethods

| Método | Propósito |
|---|---|
| `GetObtenerPagosService(filtrarPor, filtro, filtro2, filtro3)` | Consulta pagos de proveedores con filtros (tipo `PROVEEDORES`) |

## Observaciones

> [!NOTE]
> Estructura casi idéntica a `BasePagosFiniquitos.aspx.cs`, con diferencias mínimas: tipo de pago `PROVEEDORES` vs `FINIQUITOS`, columnas diferentes, y estado fijo `PAGADO`.

> [!TIP]
> Candidato ideal para unificación con `BasePagosFiniquitos.aspx.cs` en una sola página parametrizada por tipo de pago.
