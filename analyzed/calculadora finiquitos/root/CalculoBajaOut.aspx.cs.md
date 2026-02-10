# CalculoBajaOut.aspx.cs

## Información General

| Campo | Valor |
|---|---|
| **Archivo** | `FiniquitosV2/CalculoBajaOut.aspx.cs` |
| **Namespace** | `FiniquitosV2` |
| **Clase** | `CalculoBajaOut : System.Web.UI.Page` |
| **Líneas de código** | 4530 |
| **Dependencias clave** | `System.Data.SqlClient`, `OfficeOpenXml`, `iTextSharp`, `FiniquitosV2.Clases`, `Finiquitos.Clases`, `FiniquitosV2.api` |

## Descripción Funcional

Página de cálculo de finiquitos para la empresa **Outsourcing** (OUT). Es el archivo más largo del proyecto (4530 líneas, 50+ métodos). Funcionalmente es un **clon parametrizado** de `CalculoBajaEst.aspx.cs`, con las siguientes diferencias clave:

## Diferencias con CalculoBajaEst

| Aspecto | EST | OUT |
|---|---|---|
| Base de datos Softland | `Tw_Est` | `Tw_Out` |
| Empresa en servicio | `TWEST` | `TWOUT` |
| Firma empresa documento | `EST SERVICE LTDA.` / `RUT 76.760.090-9` | `OUTSOURCING LTDA.` / `RUT XX.XXX.XXX-X` |
| Cálculos adicionales | Solo feriado proporcional + haberes/descuentos | Incluye IAS, desahucio e indemnización voluntaria |
| Tipos de finiquito | 2 tipos (≤30 días, >30 días) | 2 tipos + párrafos adicionales para IAS |
| Líneas de código | 4211 | 4530 (319 líneas más por cálculos adicionales) |

## Métodos Principales (misma estructura que EST)

| Método | Líneas Aprox. | Descripción |
|---|---|---|
| `Page_Load` | 50 | Control de sesión y acceso |
| `buscarcontratos()` | 80 | Búsqueda Softland con `Initial Catalog=Tw_Out` |
| `btnCalcular_Click` | 450 | Cálculo de finiquito con IAS, desahucio |
| `BtnCalculaDobleFactor` | 400 | Cálculo con factor doble |
| `CalcularDescuento_Click` | 100 | Haberes y descuentos adicionales |
| `Grabar` | 80 | Registro en BD vía WCF |
| `btnExportar_Click` | 750 | Exportación Excel del documento |
| `btnExportarPDF_Click` | 600 | Exportación PDF del documento |

## Cálculos Adicionales vs EST

### IAS (Indemnización por Años de Servicio)
- Aplica para contratos > 1 año.
- Fórmula: `años de servicio × promedio últimas 3 liquidaciones` (tope 11 años por ley chilena).

### Desahucio (Mes de Aviso)
- Aplica cuando la causal de despido requiere aviso previo de 30 días.
- Equivale a 1 mes de remuneración promedio.

### Indemnización Voluntaria
- Monto adicional pactado entre las partes, si aplica.

## Conexiones a Base de Datos

### SQL Directo (Softland)

| Servidor | Catálogo | Credenciales |
|---|---|---|
| `conectorsoftland.team-work.cl\SQL2017` | `Tw_Out` | `Sa`/`Softland070` |

## Vulnerabilidades y Observaciones

> [!CAUTION]
> **Mismas vulnerabilidades que CalculoBajaEst**: Credenciales hardcodeadas, archivo monolítico, Thread.Sleep(1000).

> [!WARNING]
> **Duplicación masiva de código**: ~90% del código es compartido con `CalculoBajaEst.aspx.cs`. Solo cambian los parámetros de empresa, base de datos y textos del documento. 4530 líneas que podrían reducirse a una sola clase parametrizada.

> [!NOTE]
> Este archivo es ligeramente más grande que EST (4530 vs 4211) debido a los cálculos adicionales de IAS, desahucio e indemnización voluntaria que no aplican a EST.
