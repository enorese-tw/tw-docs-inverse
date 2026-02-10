# CalculoBajaConsultora.aspx.cs

## Información General

| Campo | Valor |
|---|---|
| **Archivo** | `FiniquitosV2/CalculoBajaConsultora.aspx.cs` |
| **Namespace** | `FiniquitosV2` |
| **Clase** | `CalculoBajaConsultora : System.Web.UI.Page` |
| **Líneas de código** | 2591 |
| **Dependencias clave** | `System.Data.SqlClient`, `OfficeOpenXml`, `iTextSharp`, `FiniquitosV2.Clases`, `Finiquitos.Clases` |

## Descripción Funcional

Página de cálculo de finiquitos para la empresa **Consultora**. Es un **clon simplificado** de `CalculoBajaOut.aspx.cs` con 2591 líneas (45+ métodos).

## Diferencias con CalculoBajaOut

| Aspecto | OUT | CONSULTORA |
|---|---|---|
| Base de datos Softland | `Tw_Out` | `TW_CONSULTORA` |
| Empresa en servicio | `TWOUT` | `TWCONSULTORA` |
| Firma empresa documento | `OUTSOURCING LTDA.` | `CONSULTORA LTDA.` |
| Líneas de código | 4530 | 2591 |
| Diferencia principal | Completo con todos los cálculos | Posiblemente menos tipos de finiquito documentados |

## Métodos Principales

| Método | Descripción |
|---|---|
| `Page_Load` | Control de sesión y acceso |
| `buscarcontratos()` | Búsqueda Softland con `Initial Catalog=TW_CONSULTORA` |
| `btnCalcular_Click` | Cálculo de finiquito |
| `BtnCalculaDobleFactor` | Cálculo con factor doble |
| `CalcularDescuento_Click` | Haberes y descuentos |
| `Grabar` | Registro en BD vía WCF |
| `btnExportar_Click` | Exportación Excel |
| `btnExportarPDF_Click` | Exportación PDF |

## Conexiones a Base de Datos

### SQL Directo (Softland)

| Servidor | Catálogo | Credenciales |
|---|---|---|
| `conectorsoftland.team-work.cl\SQL2017` | `TW_CONSULTORA` | `Sa`/`Softland070` |

## Vulnerabilidades y Observaciones

> [!CAUTION]
> **Mismas vulnerabilidades que los otros CalculoBaja**: Credenciales hardcodeadas, código duplicado, archivo monolítico.

> [!WARNING]
> **11.332 líneas combinadas entre los 3 archivos CalculoBaja** (Est:4211 + Out:4530 + Consultora:2591). Todo este código podría reducirse a ~1500-2000 líneas con una sola clase parametrizada por empresa.

> [!TIP]
> **Oportunidad de refactorización**: Crear una clase base `CalculoBajaBase` con la lógica compartida y tres subclases mínimas que solo definan los parámetros específicos de cada empresa (catálogo Softland, nombre empresa, RUT, textos del documento).
