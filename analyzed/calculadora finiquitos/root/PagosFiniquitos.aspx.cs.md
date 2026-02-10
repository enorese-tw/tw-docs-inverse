# PagosFiniquitos.aspx.cs

## Información General

| Campo | Valor |
|---|---|
| **Archivo** | `FiniquitosV2/PagosFiniquitos.aspx.cs` |
| **Namespace** | `FiniquitosV2` |
| **Clase** | `PagosFiniquitos : System.Web.UI.Page` |
| **Líneas de código** | 304 |
| **Dependencias clave** | `FiniquitosV2.Clases`, `System.Web.Services`, `Newtonsoft.Json` |

## Descripción Funcional

Página de **registro de pagos** de finiquitos. Permite registrar el pago de un finiquito ya calculado, seleccionando tipo de pago (cheque, transferencia, vale vista), banco, empresa y número de documento.

## Métodos

### `Page_Load(object sender, EventArgs e)`
- **Lógica**:
  1. Inicializa `Session["applicationIsPosibleSavePago"] = false`.
  2. Valida sesiones requeridas: `nombretrabajadorFiniquito`, `iddesvinculacionFiniquito`, `empresaFiniquito`. Si alguna falta, redirige a `Inicio.aspx`.
  3. Carga contratos asociados en `gvContratosAsociados` vía WCF.
  4. Obtiene área de negocio del primer contrato.
  5. Llena DropDownLists de tipo de pago, bancos y empresas desde WCF.
  6. Muestra totales del finiquito según empresa (`TWEST` muestra solo feriado proporcional).

### `selected_tipo_pago(object sender, EventArgs e)`
- Muestra/oculta campos según tipo de pago seleccionado:
  - **CHEQUE**: muestra banco, empresa y número de cheque.
  - **TRANSFERENCIA**: muestra banco y empresa (sin número de cheque).
  - **VALE VISTA**: igual que transferencia.

### `selected_banco_correlativo(object sender, EventArgs e)`
- Al seleccionar un banco, obtiene el siguiente **correlativo disponible** para pagos.
- Genera número de serie de cheque con padding de ceros.

### `registrarPago_Click(object sender, EventArgs e)`
- **Funcionalidad principal**: Registra el pago de un finiquito.
- **Lógica**:
  1. Valida si ya existe un pago registrado para esta desvinculación.
  2. Si no existe, llama a `svcFiniquitos.SetInsertarRegistroPagoPagos()` con 15 parámetros.
  3. Parámetros incluyen: tipo pago, número documento, serie, banco, empresa, montos (vacaciones, IAS, desahucio, indemnización, total), usuario, trabajador, cliente.
  4. Según tipo de pago:
     - CHEQUE → estado "Disponible para impresión"
     - TRANSFERENCIA/VALE VISTA → estado "Pagado"
  5. Muestra SweetAlert con resultado.

### WebMethods

| Método | Propósito |
|---|---|
| `GetValidaDocumentoPagos(numeroDocumento)` | Valida si un número de documento ya existe |
| `validacionGuardarRegistroPago(resultValidaDocumento)` | Establece `Session["applicationIsPosibleSavePago"]` según validación |

### `numeroSerieCheque(string idcorrelativo)` — Utilidad
- Genera número de serie de cheque con padding de ceros hasta 7 dígitos.
- Ejemplo: `"42"` → `"0000042"`.

### `replaceCharacter(string texto)` — Utilidad
- Reemplaza entidades HTML por caracteres con acentos (`&#225;` → `á`, etc.).

## Llamadas a Servicios WCF

| Método | Propósito |
|---|---|
| `GetContratosCalculoPagosService` | Contratos del cálculo para pago |
| `GetAreaNegocio()` | Área de negocio del cliente |
| `GetTipoPagoService` | Lista tipos de pago |
| `GetBancosPagosService` | Lista bancos |
| `GetEmpresasPagosService` | Lista empresas |
| `GetTotalesPagosESTService` | Totales del cálculo (empresa EST) |
| `GetObtenerUltimoCorrelativoDisponiblePagos()` | Correlativo disponible |
| `GetValidarRegistroPago()` | Validar si ya existe pago |
| `SetInsertarRegistroPagoPagos()` | Registrar pago |
| `GetValidaPagosService` | Validar documento de pago |

## Variables de Sesión

| Variable | Uso |
|---|---|
| `Session["nombretrabajadorFiniquito"]` | Nombre del trabajador |
| `Session["iddesvinculacionFiniquito"]` | ID de desvinculación |
| `Session["empresaFiniquito"]` | Empresa (TWEST/TWOUT/TWCONSULTORA) |
| `Session["applicationIsPosibleSavePago"]` | Flag para permitir guardar pago |

## Observaciones

> [!WARNING]
> **Lógica de validación de guardado en sesión**: La variable `applicationIsPosibleSavePago` se establece desde un WebMethod AJAX, lo que podría generar problemas de concurrencia si múltiples usuarios operan simultáneamente.

> [!NOTE]
> Actualmente solo muestra totales para empresa `TWEST`. Los cases para TWOUT y TWCONSULTORA no están implementados en el `switch`.
