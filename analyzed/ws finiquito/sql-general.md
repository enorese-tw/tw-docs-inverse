# Inventario General de Consultas SQL y Procedimientos Almacenados

Este documento consolida todas las interacciones con bases de datos identificadas en el proyecto `wsServiceFiniquitos`. Las consultas se han categorizado por la base de datos de destino.

## 1. Base de Datos Interna (`TW_SA`)

Esta base de datos gestiona la lógica propia del sistema de finiquitos: solicitudes, cálculos, pagos y flujos de aprobación.

### Consultas Directas (SQL Embebido)
Estas consultas se encuentran principalmente en `CFiniquitos.cs`.

| Tabla / Vista | Consulta / Propósito | Clase Origen |
| :--- | :--- | :--- |
| `FNVARIABLEREMUNERACION` | `SELECT * FROM FNVARIABLEREMUNERACION` (Maestro Variables) | `CFiniquitos` |
| `FN_CAUSAL` | `SELECT ID, NUMERO + ' \| ' + DESCRIPCION ...` (Maestro Causales) | `CFiniquitos` |
| `FN_Documentos` | `SELECT * FROM FN_Documentos` (Maestro Documentos) | `CFiniquitos` |
| `FNOtrosHaberes` | `SELECT * FROM FNOtrosHaberes` (Maestro Haberes) | `CFiniquitos` |
| `FNDESCUENTO` | `SELECT * FROM FNDESCUENTO` (Maestro Descuentos) | `CFiniquitos` |
| `HISTORIALUF` | `SELECT TOP(1) Valor ...` (Obtener valor UF) | `CFiniquitos` |
| `FN_ENCDESVINCULACION` | `SELECT ... FROM FN_ENCDESVINCULACION ...` (Encabezado Solicitud) | `CFiniquitos` |
| `ExcepcionAfc` | `SELECT COUNT(1) ...` (Validación AFC) | `CFiniquitos` |

### Procedimientos Almacenados (Stored Procedures)
La lógica transaccional y de cálculo reside aquí.

#### Gestión de Finiquitos (`CFiniquitos.cs`)
*   **Cálculo**:
    *   `SP_FN_VALIDA_CONTRATOS_EN_CALCULO`
    *   `SP_GET_FN_CONTRATOS_CALCULO`
    *   `SP_GET_FN_FACTOR_CALCULO`
*   **Visualización (Reporting)**:
    *   `SP_GET_CL_VISUALIZAR_DATOSTRABAJADOR`
    *   `SP_GET_CL_VISUALIZAR_PERIODOS`
    *   `SP_GET_CL_VISUALIZAR_CONTRATOS`
    *   `SP_GET_CL_VISUALIZAR_DOCUMENTOS`
    *   `SP_GET_CL_VISUALIZAR_TOTALDIAS`
    *   `SP_GET_CL_VISUALIZAR_DESCUENTOS_FINIQUITO`
    *   `SP_GET_CL_VISUALIZAR_OTROS_HABERES_FINIQUITO`
    *   `SP_GET_CL_VISUALIZAR_DIAS_VACACIONES`
    *   `SP_GET_CL_VISUALIZAR_TOTALES_FINIQUITO`
    *   `SP_GET_CL_VISUALIZAR_HABERES_MENSUAL_FINIQUITO`
*   **Transacciones**:
    *   `SP_SET_FN_INSERTAR_CONTRATOS_CALCULO`
    *   `SP_INSERTAR_MODIFICAR` (Solicitudes)
    *   `SP_SET_CL_CONFIRMAR_RETIRO`
    *   `SP_FN_SET_ANULAR_FOLIO`
    *   `SP_SET_PG_FINIQUITO_PAGADO`

#### Gestión de Pagos (`CPagos.cs`)
*   **Consulta**:
    *   `SP_CONSULTAR_CONTRATOS_CALCULOS`
    *   `SP_PG_TOTALES_CALCULADOS_EST`
    *   `SP_GET_PG_VALIDAR_PAGO_REALIZADO`
*   **Maestros**:
    *   `SP_FN_EMPRESAS`
    *   `SP_PG_BANCOS`
    *   `SP_PG_TIPO_PAGO`
*   **Transacciones (Cheques/Pagos)**:
    *   `SP_PG_INSERTAR_REGISTRO_PAGO`
    *   `SP_PG_VALIDA_DOCUMENTO_CHEQUE`
    *   `SP_PG_CHEQUE_PAGADO`
    *   `SP_SET_PG_ANULAR_PAGO`
    *   `SP_SET_PG_NOTARIADO`
    *   `SP_FN_SET_REVERTIR_CONFIRMACION`
*   **Proveedores y Batch**:
    *   `SP_P_OBTENER_PROVEEDOR`
    *   `SP_P_SET_REGISTRAR_PAGO_PROVEEDORES`
    *   `SP_TMP_CARGA_CHEQUE_PROVEEDOR` / `_FINIQUITO`
    *   `SP_TMP_VALIDATE_PROCESS_INIT` / `_FINIQUITO`
    *   `SP_TMP_INIT_PROCESS_CHEQUE_PROVEEDOR` / `_FINIQUITO`

#### Módulo Solicitudes B4J (`CSolB4J.cs`)
*   `SP_SET_FN_CREAR_SOLB4J`
*   `SP_FN_GET_OBTENER_SOLB4J`
*   `SP_FN_SET_ADMINISTRAR_PROCESO_SOLB4J`
*   `SP_GET_PLANTILLAS_CORREO`
*   `SP_FN_GET_ENCRIPTAR_DATOS`

---

## 2. ERP Softland (Integración Externa)

Todas las interacciones con Softland se realizan mediante **Consultas SQL Directas (Ad-Hoc)**. No se detectaron Stored Procedures en estas integraciones.

### Instancia: `SOFTLAND_EST` (Estratégica)
Clase: `CESTSoftland.cs`

| Tabla Principal | Consulta / Propósito | Variables Críticas |
| :--- | :--- | :--- |
| `sw_personal` | `SELECT ... FROM softland.sw_personal ...` (Historial Contratos) | - |
| `sw_personal` | `SELECT DISTINCT ...` (Validar Rut) | - |
| `sw_estudiosup` | `... WHERE e.descripcion like '%BAJA%'` (Listar Finiquitados) | - |
| `sw_variablepersona` | `SELECT ... FROM softland.sw_variablepersona ...` (Última Liquidación) | - |
| `sw_variablepersona` | `SELECT ... WHERE codVariable like 'P062'` (Causal Despido) | **P062** |
| `sw_variablepersona` | `SELECT ... 'DIAS JORNADA', 'SUELDO BASE' ...` (Jornada) | **P119, H001, P030, P120** |
| `sw_variablepersona` | `SELECT ... WHERE codVariable like 'D023'` (Crédito CCAF) | **D023** |
| `sw_ccostoper` | Joins complejos para Centro de Costo | - |

### Instancia: `SOFTLAND_OUT` (Outsourcing)
Clase: `COUTSoftland.cs`

| Tabla Principal | Consulta / Propósito | Variables Críticas | Observación |
| :--- | :--- | :--- | :--- |
| `sw_personal` | `SELECT DISTINCT ...` (Validar Rut) | - | Igual a EST |
| `sw_codineper` | Joins complejos para Cargo INE | - | - |
| `sw_variablepersona` | `SELECT ... WHERE codVariable like 'D046'` (Retención Judicial) | **D046** | Específico OUT |
| `sw_variablepersona` | `SELECT ... 'SUELDO BASE', 'PARTTIME' ...` (Jornada) | **H001, P034** | Usa **P034** en vez de P030 |

### Instancia: `SOFTLAND_CONSULTORA`
Clase: `CCONSULTORASoftland.cs`

| Tabla Principal | Consulta / Propósito | Variables Críticas |
| :--- | :--- | :--- |
| `sw_personal` | `SELECT DISTINCT ...` (Validar Rut) y Nombre | - |

---

## 3. Resumen de Hallazgos

1.  **Dualidad de Diseño**:
    *   La base interna (`TW_SA`) usa casi exclusivamente **Stored Procedures** para la lógica de negocio y transaccionalidad.
    *   La integración con Softland usa exclusivamente **SQL Directo (Text)** con construcciones dinámicas de strings.

2.  **Riesgos de Integración**:
    *   Las consultas a Softland dependen fuertemente de la estructura de tablas (`sw_*`) y de códigos de variables (`P062`, `P030`, `D046`). Un cambio en la configuración de haberes/descuentos en Softland romperá la integración sin aviso.

3.  **Seguridad**:
    *   El uso de concatenación de strings (`String.Format`) en las consultas a Softland y maestros internos (`SQLServerDBHelper.ExecuteQuery`) presenta un riesgo potencial de **Inyección SQL**.
