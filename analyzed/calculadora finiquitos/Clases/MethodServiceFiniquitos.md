# MethodServiceFiniquitos.cs

## Descripción General
Clase **proxy/fachada** para consumir el servicio WCF `ServicioFiniquitos`. Es la **clase más grande del sistema** (1833 líneas, ~75 métodos) y actúa como intermediario entre la capa de presentación web y el servicio WCF remoto que ejecuta operaciones CRUD en la base de datos.

## Namespace
`FiniquitosV2.Clases`

---

## Campo de Instancia

| Campo | Tipo | Descripción |
|-------|------|-------------|
| `svcFiniquitos` | `ServicioFiniquitos.ServicioFiniquitosClient` | Cliente WCF |

---

## Propiedades de Entrada (~130 propiedades)

Las propiedades funcionan como **parámetros de entrada** que se establecen antes de llamar a los "Service properties". Se organizan por módulo:

### Propiedades Principales
| Propiedad | Tipo | Descripción |
|-----------|------|-------------|
| `IDDESVINCULACION` | `string` | ID del proceso de desvinculación |
| `RUTPROVEEDOR` | `string` | RUT del proveedor |
| `IDEMPRESA` | `string` | ID de la empresa |
| `USUARIO` | `string` | Usuario que opera |
| `FICHA` | `string` | Ficha del trabajador |
| `EMPRESA` | `string` | Nombre de la empresa |
| `MONTO` | `string` | Monto genérico |
| `MES` | `string` | Mes de consulta |
| `TIPOPAGO` | `string` | Tipo de pago |
| `NUMERODOCUMENTO` | `string` | Número de documento de pago |
| `NSERIECHEQUE` | `string` | Serie del cheque |
| `BANCO` | `string` | Banco |
| `MONTOVACACIONES` | `string` | Monto vacaciones |
| `MONTOIAS` | `string` | Monto IAS |
| `MONTODESHAUCIO` | `string` | Monto desahucio |
| `MONTOINDEMNIZACION` | `string` | Monto indemnización |
| `MONTOFINIQUITO` | `string` | Monto total finiquito |
| ... | ... | (~130 propiedades más) |

---

## Módulos Funcionales

### 1. Módulo OUT (Outsourcing)
Operaciones específicas para la empresa "OUT" (TEAMRRHH).

| Service Property | Método Privado | Descripción |
|-----------------|----------------|-------------|
| `SetContratoOUTService` | `SetContratoOUT()` | Grabar contrato OUT |
| `SetDescuentoOUTService` | `SetDescuentoOUT()` | Grabar descuento OUT |
| `SetHaberMensualOUTService` | `SetHaberMensualOUT()` | Grabar haber mensual OUT |
| `SetOtroHaberFiniquitoOUTService` | `SetOtroHaberFiniquitoOUT()` | Grabar otro haber OUT |
| `SetDiasVacacionOUTService` | `SetDiasVacacionOUT()` | Grabar días vacación OUT |
| `SetTotalHaberFiniquitoOUTService` | `SetTotalHaberFiniquitoOUT()` | Grabar total haber OUT |
| `SetConceptoAdicionalOUTService` | `SetConceptoAdicionalOUT()` | Grabar concepto adicional OUT |
| `SetConfirmarFiniquitoOUTService` | `SetConfirmarFiniquitoOUT()` | Confirmar finiquito OUT |
| `GetBonoCalculoOUTService` | `GetBonoCalculoOUT()` | Obtener bono de cálculo OUT |
| `GetAnticipoxDesvinculacionOUTService` | `GetAnticipoxDesvinculacionOUT()` | Obtener anticipo OUT |

### 2. Módulo EST (Empresa de Servicios Transitorios)
| Service Property | Descripción |
|-----------------|-------------|
| `SetContratoESTService` | Grabar contrato EST |
| `SetDescuentoESTService` | Grabar descuento EST |
| `SetHaberMensualESTService` | Grabar haber mensual EST |
| `SetOtroHaberFiniquitoESTService` | Grabar otro haber EST |
| `SetDiasVacacionESTService` | Grabar días vacación EST |
| `SetTotalHaberFiniquitoESTService` | Grabar total haber EST |
| `SetConfirmarFiniquitoESTService` | Confirmar finiquito EST |
| `GetBonoCalculoESTService` | Obtener bono de cálculo EST |
| `GetAnticipoxDesvinculacionESTService` | Obtener anticipo EST |

### 3. Módulo Pagos
Gestión completa de pagos de finiquitos.

| Service Property | Descripción |
|-----------------|-------------|
| `SetInsertarRegistroPagoPagosService` | Registrar un pago |
| `GetContratosCalculoPagosService` | Obtener contratos para cálculo |
| `SetChequeDisponiblePagoPagosService` | Marcar cheque como disponible |
| `GetEmpresasPagoService` | Listar empresas para pagos |
| `GetBancosPagoService` | Listar bancos |
| `GetTipoPagosService` | Listar tipos de pago |
| `GetTotalesPagosESTService` | Totales de pagos EST |
| `GetValidaDocumentoPagosService` | Validar documento de pago |
| `GetObtenerPagoPagosService` | Obtener detalle de pago |
| `GetDataChequePagosService` | Datos de cheque |
| `GetObtenerMontoChequePagosService` | Monto del cheque |

### 4. Módulo Proveedores
| Service Property | Descripción |
|-----------------|-------------|
| `GetObtenerProveedorService` | Buscar proveedor por RUT |
| `SetRegistrarPagoProveedoresService` | Registrar pago a proveedor |
| `GetObtenerCorrelativoDisponibleProveedoresService` | Obtener correlativo disponible |
| `GetObtenerClientePagosService` | Obtener clientes para pagos |
| `GetObtenerTrabajadoresPagosService` | Obtener trabajadores para pagos |

### 5. Módulo Cálculo/Visualización
Visualización de datos del cálculo del finiquito.

| Service Property | Descripción |
|-----------------|-------------|
| `GetVisualizarDatosTrabajadorCalculoService` | Datos del trabajador |
| `GetValidarConfirmadoFiniquitoCalculoService` | Validar si está confirmado |
| `GetVisualizarContratosCalculoService` | Contratos del cálculo |
| `GetVisualizarDocumentosCalculoService` | Documentos del cálculo |
| `GetVisualizarPeriodosCalculoService` | Períodos del cálculo |
| `GetVisualizarTotalDiasCalculoCalculoService` | Total días |
| `GetVisualizarOtrosHaberesFiniquitoCalculoService` | Otros haberes |
| `GetVisualizarDescuentosFiniquitoCalculoService` | Descuentos |
| `GetVisualizarDiasVacacionesCalculoService` | Días de vacaciones |
| `GetVisualizarTotalesFiniquitoCalculoService` | Totales del finiquito |
| `GetVisualizarHabaeresMensualFiniquitoCalculoService` | Haberes mensuales |
| `GetVisualizarConceptosAdicionalesFiniquitoCalculoService` | Conceptos adicionales |
| `GetVisualizarBonosAdicionalesFiniquitoCalculoService` | Bonos adicionales |
| `GetVisualizarValorUfFiniquitoCalculoService` | Valor UF |

---

## Arquitectura y Patrón de Diseño

```
Página Web (.aspx)
    └── MethodServiceFiniquitos (Facade)
        ├── Establece Propiedades (IDDESVINCULACION, FICHA, etc.)
        └── Lee Service Properties (GetXxxService)
            └── Llama Método Privado (GetXxx() / SetXxx())
                └── svcFiniquitos WCF Client
                    └── Servicio WCF Remoto
                        └── Base de Datos (SQL Server)
```

### Patrón de Comunicación WCF
Todos los métodos privados siguen el mismo patrón:
```csharp
private DataSet SetXxx(string param1, string param2, ...)
{
    string[] parametros = { "@PARAM1", "@PARAM2", ... };
    string[] valores = { param1, param2, ... };
    return svcFiniquitos.SetXxx(parametros, valores).Table;
}
```

## Observaciones Críticas
- ⚠️ **1833 líneas** — clase extremadamente grande.
- ⚠️ **75+ métodos** — mezcla Get/Set para múltiples módulos funcionales.
- ⚠️ **~130 propiedades** — todas `string`, sin validación.
- ⚠️ **Código duplicado masivo**: Los métodos EST y OUT son prácticamente idénticos, solo cambian el nombre del servicio WCF.
- ⚠️ Uso de **propiedades como métodos** (patrón "service property") dificulta la comprensión y el testing.
- ⚠️ **Sin manejo de errores** — no hay try/catch en los métodos privados.
- ⚠️ **Bug en `GetDataChequePagos()`**: usa la propiedad `IDDESVINCULACION` en vez del parámetro `idDesvinculacion` (línea 1009).
- No accede directamente a la BD — todo pasa a través del servicio WCF.
- Es el punto de entrada principal para operaciones CRUD complejas del finiquito.
