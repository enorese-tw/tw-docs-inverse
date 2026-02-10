# Documentación de Consultas: `CPagos.cs`

## Contexto
Esta clase gestiona el flujo de pagos y tesorería del proceso de finiquitos. A diferencia de otras clases, **no contiene consultas SQL directas (embebidas)**. Toda la lógica de persistencia y consulta está delegada exclusivamente a **Procedimientos Almacenados**.

## Procedimientos Almacenados Identificados

### 1. Gestión de Contratos por Pagar
*   **SP**: `SP_CONSULTAR_CONTRATOS_CALCULOS`
    *   **Método**: `GetContratosCalculoPagos`
    *   **Propósito**: Obtener la lista de finiquitos aprobados que están pendientes de pago.

### 2. Maestros de Pago
*   **SP**: `SP_FN_EMPRESAS`
    *   **Método**: `GetEmpresasPagos`
    *   **Propósito**: Listar empresas pagadoras.
*   **SP**: `SP_PG_BANCOS`
    *   **Método**: `GetBancosPagos`
    *   **Propósito**: Listar bancos disponibles para emisión de cheques o transferencias.
*   **SP**: `SP_PG_TIPO_PAGO`
    *   **Método**: `GetTiposPagoPagos`
    *   **Propósito**: Listar formas de pago (Cheque, Vale Vista, Transferencia, Efectivo).

### 3. Registro y Validación de Pagos
*   **SP**: `SP_PG_INSERTAR_REGISTRO_PAGO`
    *   **Método**: `SetInsertarRegistroPagoPagos`
    *   **Propósito**: Insertar el registro físico del pago asociado a un finiquito.
*   **SP**: `SP_PG_VALIDA_DOCUMENTO_CHEQUE`
    *   **Método**: `GetValidaDocumentoPagos`
    *   **Propósito**: Evitar duplicidad de números de documentos bancarios.
*   **SP**: `SP_GET_PG_VALIDAR_PAGO_REALIZADO`
    *   **Método**: `GetValidarRegistroPago`
    *   **Propósito**: Verificar si un pago se procesó correctamente.

### 4. Ciclo de Vida del Pago
*   **SP**: `SP_PG_CHEQUE_PAGADO`
    *   **Método**: `SetChequePagado`
    *   **Propósito**: Confirmar la efectividad del cobro.
*   **SP**: `SP_SET_PG_ANULAR_PAGO`
    *   **Método**: `SetAnularPagoPagos`
    *   **Propósito**: Cancelar un pago emitido.
*   **SP**: `SP_SET_PG_NOTARIADO`
    *   **Método**: `SetNotariadoPagos`
    *   **Propósito**: Registrar firma notarial.
*   **SP**: `SP_FN_SET_REVERTIR_CONFIRMACION`
    *   **Método**: `SetRevertirConfirmacionPagos`
    *   **Propósito**: Revertir estado de confirmación.

### 5. Proveedores y Procesos Masivos (Batch)
*   **SP**: `SP_P_OBTENER_PROVEEDOR` / `SP_P_SET_REGISTRAR_PAGO_PROVEEDORES`
    *   **Propósito**: Gestión de pagos a terceros.
*   **SP**: `SP_TMP_CARGA_CHEQUE_PROVEEDOR` / `SP_TMP_CARGA_CHEQUE_FINIQUITO`
    *   **Propósito**: Carga inicial a tablas temporales para procesos por lotes.
*   **SP**: `SP_TMP_VALIDATE_PROCESS_INIT` / `SP_TMP_VALIDATE_PROCESS_INIT_FINIQUITO`
    *   **Propósito**: Validación pre-procesamiento masivo.
*   **SP**: `SP_TMP_INIT_PROCESS_CHEQUE_PROVEEDOR` / `SP_TMP_INIT_PROCESS_CHEQUE_FINIQUITO`
    *   **Propósito**: Ejecución transaccional del pago masivo.

## Observaciones
El diseño de `CPagos` es "limpio" desde el punto de vista del código C#, ya que actua como un proxy puro hacia la base de datos, centralizando toda la lógica de negocio y reglas de integridad referencial en los Stored Procedures.
