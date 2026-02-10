# PagoProveedores.aspx.cs

## Información General

| Campo | Valor |
|---|---|
| **Archivo** | `FiniquitosV2/PagoProveedores.aspx.cs` |
| **Namespace** | `FiniquitosV2` |
| **Clase** | `PagoProveedores : System.Web.UI.Page` |
| **Líneas de código** | 1285 |
| **Dependencias clave** | `FiniquitosV2.Clases`, `Finiquitos.Clases`, `OfficeOpenXml`, `iTextSharp`, `Newtonsoft.Json` |

## Descripción Funcional

Página de **gestión de pagos a proveedores**. Permite registrar pagos a proveedores externos (no trabajadores) y generar cheques en formato Excel para impresión. Incluye la generación completa del documento de cheque con formato para impresora matricial.

## Métodos

### `Page_Load(object sender, EventArgs e)`
- Vacío — sin lógica de carga ni validación de sesión en este método.

### WebMethods — Gestión de Proveedores

| Método | Propósito |
|---|---|
| `SetInsertarProveedorService(rut, nombre, tipo)` | Registrar nuevo proveedor |
| `GetVisualizarTiposProveedoresService()` | Listar tipos de proveedores |
| `GetObtenerProveedorService(rutProveedor)` | Buscar proveedor por RUT |
| `GetMontoCifra(monto)` | Convertir monto a cifra en letras (usa `Convertidor`) |
| `GetObtenerCorrelativoDisponibleProveedoresService(empresa)` | Obtener correlativo de cheque |

### WebMethods — Gestión de Cheques

| Método | Propósito |
|---|---|
| `SetTMPCargaChequeProveedorProveedoresService(...)` | Cargar cheque en cola temporal (14 parámetros) |
| `SetTMPInitProcessChequeProveedorProveedoresService()` | Inicializar proceso de generación de cheques |
| `GetTMPValidateProcessInitProveedoresService()` | Validar si hay cheques en proceso |
| `GetTMPChequesInProcessProveedorService()` | Obtener cheques en proceso |

### `SetTMPCloseProcessChequeProveedorService()` — Generación de Cheques (~600 líneas)
- **Funcionalidad principal**: Genera documento Excel con formato de cheque bancario para proveedores.
- **Proceso**:
  1. Consulta cheques pendientes en tabla temporal vía WCF.
  2. Para cada cheque, crea layout de 92 filas en la hoja Excel.
  3. **Secciones del cheque**:
     - Comprobante superior (motivo, beneficiario, monto, fecha)
     - Monto en cifra con separadores de miles espaciados (switch por longitud 1-9 dígitos)
     - Ciudad y fecha formateada
     - Nominativo (línea negra de anulación opcional)
     - Nombre del beneficiario
     - Cifra en letras (con algoritmo de división para textos largos > 56 caracteres)
     - Protección de cheque (monto repetido en formato grande)
     - Segundo comprobante inferior izquierdo
  4. **Configuración de impresión**:
     - Impresora OKI DATA CORP ML320/1TURBO
     - Papel tamaño carta, márgenes ajustados
     - Protección de hoja con contraseña `Satw.2018`
  5. **Almacenamiento**: Guarda copia en `\\cranky\FileServer\Sistemas\Ficha_Alta\Cheques\Proveedores\`
     - Subdirectorio por fecha: `{día} de {mes_texto} de {año}\`

## Servidor de Archivos

| Ruta UNC | Propósito |
|---|---|
| `\\cranky\FileServer\Sistemas\Ficha_Alta\Cheques\Proveedores\` | Almacenamiento de cheques de proveedores |

## Vulnerabilidades y Observaciones

> [!CAUTION]
> **Contraseña de protección Excel hardcodeada**: `Satw.2018` — visible en código fuente.

> [!CAUTION]
> **Sin validación de sesión en Page_Load**: Cualquier usuario (o visitante no autenticado) podría acceder a los WebMethods.

> [!WARNING]
> **Ruta UNC hardcodeada**: Dependencia del servidor `\\cranky`.

> [!WARNING]
> **Bug en case 9 del formateo de monto** (línea 295): Usa `Substring(7, 1)` duplicado en lugar de `Substring(8, 1)` para el último dígito de montos de 9 cifras. Este mismo bug existe en el bloque de "PROTECCION DE CHEQUE" (línea 583).

> [!WARNING]
> **Código comentado extenso**: ~500 líneas de código comentado (método de cheque de producción antiguo) que debería eliminarse.

> [!NOTE]
> La lógica de generación de cheques es casi idéntica a la de `Inicio.aspx.cs` pero para proveedores en vez de finiquitos. Podría unificarse en una clase de generación de cheques.
