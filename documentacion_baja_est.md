# Documentación de Lógica de Cálculo Baja EST

Este documento describe la lógica de negocio, flujo de datos y arquitectura técnica detrás del módulo de **Cálculo de Baja EST** (Empresas de Servicios Transitorios) en la aplicación de Calculadora de Finiquitos.

## 1. Visión General
El módulo `CalculoBajaEst.aspx` permite a los usuarios gestionar el proceso de finiquito para trabajadores EST. Sus funciones principales incluyen:
- Selección de trabajadores y contratos.
- Cálculo de días trabajados, vacaciones proporcionales y progresivas.
- Aplicación de factores de cálculo (1.25, 1.75) según causales y normativa.
- Gestión de haberes y descuentos.
- Generación y persistencia de documentos de finiquito.

## 2. Arquitectura de Datos
La aplicación sigue una arquitectura en capas:
1.  **Capa de Presentación (Web)**: `CalculoBajaEst.aspx.cs`. Maneja la interacción con el usuario y la orquestación de llamadas a servicios.
2.  **Capa de Servicios (WCF)**: `wsFiniquito` (Interfaz `IServicioFiniquitos`). Actúa como fachada para la lógica de negocio y acceso a datos.
3.  **Capa de Acceso a Datos (DAL)**: Clases `CFiniquitos.cs`, `CPagos.cs`, etc. dentro del servicio WCF. Ejecutan consultas SQL y Stored Procedures.
4.  **Base de Datos**: SQL Server (`TW_SA` para la aplicación, `softland` para datos de RRHH).

## 3. Flujo de Información y Origen de Datos

### 3.1. Carga Inicial
Al cargar la página, se obtienen datos maestros mediante el servicio `svcFiniquitos`:
- **Causales de Despido**: `GetCausales` -> `SELECT ... FROM FN_CAUSAL`.
- **Documentos**: `GetListarDocumentos` -> `SELECT ... FROM FN_Documentos`.
- **Valor UF**: `GetUltimaUF` -> `SELECT ... FROM [dbo].[HISTORIALUF]`.

### 3.2. Selección de Trabajador y Contratos
- Se utiliza `wsServicioFiniquitos.GetFiniquitosSolicitudBaja` que invoca el SP `SP_FN_GET_FINIQUITO_SOLICITUD_BAJA`.
- Los contratos asociados se validan y listan mediante `SP_FN_VALIDA_CONTRATOS_EN_CALCULO` y `SP_GET_FN_CONTRATOS_CALCULO`.

### 3.3. Obtención de Remuneraciones (Softland)
El sistema consulta directamente las tablas de Softland para obtener variables de remuneración.
- **Clase**: `Remuneracion.cs` y `VariableRemuneracion.cs`.
- **Tablas/Vistas**: `softland.sw_variable`, `softland.sw_variablepersona`, `softland.sw_vsnpRetornaFechaMesExistentes`.
- **Lógica**: Se recuperan los valores históricos de remuneraciones para el cálculo de promedios.

## 4. Lógica de Cálculo (`btnCalcular_Click`)

### 4.1. Días Trabajados y Vacaciones
El sistema itera sobre los contratos seleccionados en la grilla (`GridView1`):
- **Días Trabajados**: Suma de los días de cada contrato.
- **Vacaciones**:
  - Utiliza `Utilidades.TotaldiasVacaciones` para calcular días hábiles e inhábiles.
  - Consulta `[dbo].[View_Festivos]` para excluir feriados.
  - Calcula el saldo básico y progresivo.

### 4.2. Factores de Cálculo
Dependiendo de la configuración y causal, se aplican factores multiplicadores al sueldo base o indemnizaciones:
- **Factor 1.25 y 1.75**: Gestionados en `BtnCalculaDobleFactor`.
- Se consulta el SP `SP_GET_FN_FACTOR_CALCULO` para obtener el factor aplicable según la configuración.

### 4.3. Haberes y Descuentos
- **Descuentos**: Se listan mediante `GetListarDescuentos` (`SELECT * FROM FNDESCUENTO`).
- **Otros Haberes**: Se listan mediante `GetOtrosHaberes` (`SELECT * FROM FNOtrosHaberes`).
- **Gratificaciones y Bonos**: Se calculan en base a los datos recuperados de Softland y lógica interna en `CalcularDescuento_Click`.

## 5. Persistencia (Guardado)
Al finalizar el cálculo y confirmar, se guardan los datos mediante Stored Procedures en `TW_SA`:
- **Encabezado**: `SP_INSERTAR_MODIFICAR`.
- **Contratos**: `SP_SET_FN_INSERTAR_CONTRATOS_CALCULO`.
- **Vacaciones**: `SP_SET_FN_INSERTAR_DIAS_VACACIONES`.
- **Haberes**: `SP_SET_FN_INSERTAR_TOTAL_HABERES`.
- **Pagos**: `SP_PG_INSERTAR_REGISTRO_PAGO` (si aplica).

## 6. Inventario de Procedimientos Almacenados (Stored Procedures)
Los principales procedimientos almacenados identificados en `CFiniquitos.cs` y `CPagos.cs` son:

### Finiquitos y Solicitudes
- `SP_FN_GET_FINIQUITO_SOLICITUD_BAJA`
- `SP_FN_GET_FINIQUITOS_SOLICITUDES_BAJA`
- `SP_FN_GET_FINIQUITOS_CONFIRMADOS`
- `SP_FN_GET_FINIQUITOS_RECIENTES`
- `SP_FN_VALIDA_CONTRATOS_EN_CALCULO`
- `SP_GET_FN_CONTRATOS_CALCULO`
- `SP_GET_FN_ULTIMO_ENC_DESVINCULACION`

### Visualización y Cálculos
- `SP_GET_CL_VISUALIZAR_DATOSTRABAJADOR`
- `SP_GET_CL_VISUALIZAR_PERIODOS`
- `SP_GET_CL_VISUALIZAR_CONTRATOS`
- `SP_GET_CL_VISUALIZAR_DOCUMENTOS`
- `SP_GET_CL_VISUALIZAR_TOTALDIAS`
- `SP_GET_CL_VISUALIZAR_DESCUENTOS_FINIQUITO`
- `SP_GET_CL_VISUALIZAR_OTROSHABERES_FINIQUITO`
- `SP_GET_CL_VISUALIZAR_DIASVACACIONES`
- `SP_GET_CL_VISUALIZAR_TOTALESFINIQUITO`
- `SP_GET_FN_FACTOR_CALCULO`
- `SP_GET_FN_Gratificacion`

### Persistencia (SET)
- `SP_INSERTAR_MODIFICAR`
- `SP_SET_FN_INSERTAR_CONTRATOS_CALCULO`
- `SP_SET_FN_INSERTAR_DIAS_VACACIONES`
- `SP_SET_FN_INSERTAR_TOTAL_HABERES`
- `SP_SET_CL_CONFIRMAR_RETIRO`
- `SP_FN_SET_ANULAR_FOLIO`

### Pagos
- `SP_PG_INSERTAR_REGISTRO_PAGO`
- `SP_PG_VALIDA_DOCUMENTO_CHEQUE`
- `SP_GET_PG_OBTENER_PAGOS`
- `SP_GET_PG_DATA_CHEQUE`
- `SP_PG_CHEQUE_PAGADO`

## 7. Dependencias Externas
- **Servicio WCF**: `wsFiniquito` (expone `IServicioFiniquitos`).
- **Softland**: Base de datos externa utilizada para validación de contratos y remuneraciones históricas.
