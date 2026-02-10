# Documentación Detallada: `CPagos.cs`

## Visión General
La clase `CPagos` encapsula la lógica de negocio para la gestión de pagos, cheques y proveedores dentro del flujo de finiquitos. Actúa como capa de acceso a datos para operaciones financieras críticas, delegando la ejecución a Procedimientos Almacenados en la base de datos `TW_SA`.

## Dependencias
*   `System.Data` (DataTable)
*   `SQLServerDBHelper`: Contexto de base de datos `"TW_SA"`.

## Análisis de Funcionalidad y Stored Procedures

### 1. Gestión de Contratos y Pagos

#### `GetContratosCalculoPagos`
*   **SP**: `SP_CONSULTAR_CONTRATOS_CALCULOS`
*   **Objetivo**: Recuperar los contratos listos para ser pagados o en proceso de cálculo de pago.

#### `GetEmpresasPagos`, `GetBancosPagos`, `GetTiposPagoPagos`
*   **SPs**: `SP_FN_EMPRESAS`, `SP_PG_BANCOS`, `SP_PG_TIPO_PAGO`.
*   **Objetivo**: Obtener maestros para desplegables en la UI (Empresas, Bancos, Tipos de Pago).

#### `GetTotalesPagosEST`
*   **SP**: `SP_PG_TOTALES_CALCULADOS_EST`
*   **Objetivo**: Obtener los montos totales calculados para un finiquito específico (contexto EST).

### 2. Operaciones con Cheques y Documentos

#### `SetInsertarRegistroPagoPagos`
*   **SP**: `SP_PG_INSERTAR_REGISTRO_PAGO`
*   **Crítico**: Registra el pago efectivo en el sistema.

#### `GetValidaDocumentoPagos`
*   **SP**: `SP_PG_VALIDA_DOCUMENTO_CHEQUE`
*   **Objetivo**: Validar si un número de cheque o documento ya existe o es válido.

#### `SetChequePagado`
*   **SP**: `SP_PG_CHEQUE_PAGADO`
*   **Flujo**: Marca un cheque emitido como efectivamente cobrado o pagado.

#### `SetAnularPagoPagos`
*   **SP**: `SP_SET_PG_ANULAR_PAGO`
*   **Flujo**: Revierte un pago realizado, probablemente reactivando la deuda o el estado pendiente del finiquito.

#### `GetValidarRegistroPago`
*   **SP**: `SP_GET_PG_VALIDAR_PAGO_REALIZADO`
*   **Objetivo**: Verificar integridad post-pago.

### 3. Gestión de Proveedores

El sistema maneja pagos a terceros (proveedores), posiblemente relacionados con descuentos o servicios externos asociados al finiquito.

#### `GetObtenerProveedores`
*   **SP**: `SP_P_OBTENER_PROVEEDOR`

#### `SetRegistrarPagoProveedores`
*   **SP**: `SP_P_SET_REGISTRAR_PAGO_PROVEEDORES`
*   **Objetivo**: Registrar el pago a un proveedor.

#### `GetObtenerCorrelativoDisponibleProveedores`
*   **SP**: `SP_P_OBTENER_CORRELATIVO_DISPONIBLE`
*   **Objetivo**: Obtener el siguiente número de folio/documento disponible para proveedores.

### 4. Procesos Masivos y Temporales (Batch)

Esta sección sugiere la existencia de procesos de carga masiva o validación por lotes para cheques, tanto de finiquitos como de proveedores.

#### Procesos de Proveedores
*   `SetTMPCargaChequeProveedorProveedores` (`SP_TMP_CARGA_CHEQUE_PROVEEDOR`): Carga inicial a tabla temporal.
*   `GetTMPValidateProcessInitProveedores` (`SP_TMP_VALIDATE_PROCESS_INIT`): Valida si se puede iniciar el proceso masivo.
*   `SetTMPInitProcessChequeProveedorProveedores` / `SetTMPCloseProcessChequeProveedor`: Controlan el inicio y fin transaccional del proceso masivo.
*   `GetTMPChequesInProcessProveedor`: Lista los cheques actualmente en la cola de procesamiento.

#### Procesos de Finiquitos
*   Simetría total con los métodos de proveedores, pero apuntando a SPs de finiquitos:
    *   `SP_TMP_CARGA_CHEQUE_FINIQUITO`
    *   `SP_TMP_VALIDATE_PROCESS_INIT_FINIQUITO`
    *   `SP_TMP_INIT_PROCESS_CHEQUE_FINIQUITO`
    *   `SP_TMP_CLOSE_PROCESS_CHEQUE_FINIQUITO`

### 5. Lógica de Negocio Adicional

#### `SetNotariadoPagos`
*   **SP**: `SP_SET_PG_NOTARIADO`
*   **Negocio**: Marca el finiquito como firmado ante notario, un paso legal obligatorio en Chile para la validez del finiquito.

#### `SetRevertirConfirmacionPagos`
*   **SP**: `SP_FN_SET_REVERTIR_CONFIRMACION`
*   **Negocio**: Permite "deshacer" la confirmación de un pago, devolviendo el finiquito a un estado previo para correcciones.

## Observaciones
*   La clase maneja dos flujos paralelos de carga masiva (Proveedores y Finiquitos) implementados con tablas temporales o de paso en la base de datos (`TMP`).
*   Existe una separación clara entre la *emisión* del pago (`SetInsertarRegistroPagoPagos`) y la *confirmación* del cobro (`SetChequePagado`).
*   La lógica de negocio reside casi enteramente en los Procedimientos Almacenados, actuando C# solo como un pasamanos de parámetros.
