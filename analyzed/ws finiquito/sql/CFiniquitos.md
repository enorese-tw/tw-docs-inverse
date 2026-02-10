# Documentación de Consultas: `CFiniquitos.cs`

## Contexto
Esta clase gestiona la lógica principal del cálculo y gestión de finiquitos en la base de datos interna `TW_SA`. Combina consultas directas para maestros con Procedimientos Almacenados complejos para el cálculo.

## 1. Consultas Directas (SQL Embebido)

### Gestión de Variables y Maestros
*   **Método**: `GetCargaVariable`
*   **Query**: `select * from FNVARIABLEREMUNERACION`
*   **Propósito**: Obtener parámetros globales de remuneración.

*   **Método**: `GetCausales`
*   **Query**: `SELECT ID, NUMERO + ' | ' + DESCRIPCION as UNIFICADO FROM FN_CAUSAL`
*   **Propósito**: Listar causales de despido concatenando código y descripción para mostrar en UI.

*   **Método**: `GetListarDocumentos`
*   **Query**: `select * from FN_Documentos`
*   **Propósito**: Listar tipos de documentos disponibles.

*   **Método**: `GetOtrosHaberes` / `GetListarDescuentos`
*   **Query**: `select * from FNOtrosHaberes` / `select * from FNDESCUENTO`
*   **Propósito**: Listar haberes y descuentos configurables.

### Cálculo de UF
*   **Método**: `GetUltimaUF`
*   **Query (Compleja)**:
    ```sql
    SELECT TOP(1) Valor 
    FROM [dbo].[HISTORIALUF] 
    WHERE YEAR(Fecha) = YEAR(@FechaFiniquito) 
    AND MONTH(Fecha)= MONTH(@FechaFiniquito)
    -- O lógica de fallback al mes anterior
    ```
*   **Propósito**: Obtener el valor de la UF para la fecha de cálculo, crítico para la conversión de montos.

### Gestión de Solicitudes
*   **Método**: `GetObtenerENCFiniquito`
*   **Query**:
    ```sql
    SELECT F.ID, F.IDESTADO, ... 
    FROM FN_ENCDESVINCULACION F 
    INNER JOIN FN_ESTADOS E ON F.IDESTADO = E.ID 
    INNER JOIN FN_EMPRESAS M ON M.RUT = F.RUTEMPRESA 
    WHERE FICHATRABAJADOR = '{0}' ...
    ```
*   **Propósito**: Obtener el encabezado y estado actual de una solicitud de finiquito.

### Excepciones de AFC
*   **Método**: `GetIsExceptionAfc`
*   **Query**: `select count(1) from ExcepcionAfc where rutEmpresa = '{0}' and cod_division = '{1}'`
*   **Propósito**: Verificar reglas especiales para el seguro de cesantía.

## 2. Procedimientos Almacenados (Stored Procedures)

### Flujo de Cálculo y Validación
*   **SP**: `SP_FN_VALIDA_CONTRATOS_EN_CALCULO`
    *   **Uso**: `GetValidaContratosEnCalculo`
    *   **Propósito**: Evitar duplicidad de cálculos para un mismo contrato.

*   **SP**: `SP_GET_FN_CONTRATOS_CALCULO`
    *   **Uso**: `GetListarContratosCalculo`
    *   **Propósito**: Listar contratos en proceso.

*   **SP**: `SP_GET_FN_FACTOR_CALCULO`
    *   **Uso**: `GetFactorCalculo125`
    *   **Propósito**: Obtener factor de incremento (probablemente el 1.25 o 2.5 legal).

### Visualización de Resultados (Reporting)
Estos SPs alimentan la vista detallada del finiquito:
*   `SP_GET_CL_VISUALIZAR_DATOSTRABAJADOR`: Datos personales y contractuales procesados.
*   `SP_GET_CL_VISUALIZAR_PERIODOS`: Periodos laborados.
*   `SP_GET_CL_VISUALIZAR_CONTRATOS`: Detalle de contratos considerados.
*   `SP_GET_CL_VISUALIZAR_DOCUMENTOS`: Documentos generados.
*   `SP_GET_CL_VISUALIZAR_TOTALDIAS`: Días totales calculados (vacaciones, proporcional, aviso).
*   `SP_GET_CL_VISUALIZAR_DESCUENTOS_FINIQUITO`: Desglose de descuentos aplicados.
*   `SP_GET_CL_VISUALIZAR_OTROS_HABERES_FINIQUITO`: Haberes adicionales.
*   `SP_GET_CL_VISUALIZAR_DIAS_VACACIONES`: Detalle del cálculo de feriado proporcional.
*   `SP_GET_CL_VISUALIZAR_TOTALES_FINIQUITO`: Resumen final de montos.
*   `SP_GET_CL_VISUALIZAR_HABERES_MENSUAL_FINIQUITO`: Detalle mensual para cálculo de promedios.

### Operaciones Transaccionales (Escritura)
*   **SP**: `SP_SET_FN_INSERTAR_CONTRATOS_CALCULO`
    *   **Uso**: `SetInsertaContratoCalculo`
    *   **Acción**: Inicia un proceso de cálculo para un contrato.

*   **SP**: `SP_INSERTAR_MODIFICAR`
    *   **Uso**: `SetCrearModificarEncDesvinculacion`
    *   **Acción**: Crea o actualiza la solicitud de desvinculación.

*   **SP**: `SP_SET_CL_CONFIRMAR_RETIRO`
    *   **Uso**: `SetConfirmarRetiroCalculo`
    *   **Acción**: Finaliza y "congela" el cálculo, dejándolo listo para pago.

*   **SP**: `SP_FN_SET_ANULAR_FOLIO`
    *   **Uso**: `SetAnularFolioFiniquito`
    *   **Acción**: Anula lógica y/o fiscalmente el finiquito.

*   **SP**: `SP_SET_PG_FINIQUITO_PAGADO`
    *   **Uso**: `SetFiniquitoPagado`
    *   **Acción**: Marca el finiquito como pagado (cambio de estado).
