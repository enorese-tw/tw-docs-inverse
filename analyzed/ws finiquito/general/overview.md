# Documentación General del Proyecto ServicioFiniquitos

Este documento proporciona un análisis exhaustivo y detallado del proyecto `wsServiceFiniquitos` (ServicioFiniquitos), un servicio WCF diseñado para gestionar el ciclo de vida de los finiquitos, incluyendo cálculos, pagos y la integración con el sistema ERP Softland.

## 1. Visión General del Proyecto

El proyecto es una solución basada en **WCF (Windows Communication Foundation)** que expone un conjunto de servicios SOAP para la gestión de finiquitos. Su objetivo principal es actuar como capa intermedia entre las aplicaciones cliente (posiblemente un portal web o aplicación de escritorio) y las bases de datos de la empresa, incluyendo la base de datos propia del sistema de finiquitos y la base de datos del ERP Softland.

### Tecnologías Clave
*   **.NET Framework**: Utilizado para la implementación del servicio WCF.
*   **WCF (SOAP)**: Protocolo de comunicación para exponer la lógica de negocio.
*   **SQL Server**: Motor de base de datos para persistencia y consultas.
*   **ADO.NET (System.Data.SqlClient)**: Utilizado para el acceso a datos a través de la clase helper `SQLServerDBHelper`.

## 2. Arquitectura y Componentes Principales

La solución se estructura en un único proyecto `ServicioFiniquitos` que contiene:
*   **Contratos de Servicio**: Definidos en `IServicioFiniquitos.cs`.
*   **Implementación del Servicio**: `wsServicioFiniquitos.svc.cs`.
*   **Lógica de Negocio y Acceso a Datos**: Clases especializadas como `CFiniquitos`, `CPagos`, `CSolB4J`, `CESTSoftland`, `COUTSoftland`.
*   **DTOs (Data Transfer Objects)**: `ServicioFiniquito` (wrapper de `DataSet`) y `Parametro`.
*   **Helper de Base de Datos**: `SQLServerDBHelper`.

### Flujo de Datos
1.  El cliente consume el servicio WCF llamando a los métodos definidos en `IServicioFiniquitos`.
2.  `wsServicioFiniquitos` recibe la petición y delega la lógica a una clase especializada (`CFiniquitos`, `CPagos`, etc.).
3.  La clase especializada construye la consulta SQL o llama a un Procedimiento Almacenado.
4.  `SQLServerDBHelper` ejecuta la operación contra la base de datos configurada (`TW_SA`, `SOFTLAND_EST`, `SOFTLAND_OUT`).
5.  Los resultados se retornan como un `DataTable` que se empaqueta en un `DataSet` dentro del objeto `ServicioFiniquito`.

---

## 3. Análisis Detallado de Componentes

### 3.1. Servicio WCF (`wsServicioFiniquitos`)

Es el punto de entrada. Implementa la interfaz `IServicioFiniquitos` y organiza los métodos en regiones funcionales:

*   **GET**: Métodos de consulta.
*   **SET**: Métodos de actualización/inserción.
*   **SOFTLAND**: Métodos específicos de integración con el ERP.
*   **GENERICOS**: Utilidades varias.

Cada método sigue un patrón similar: instancia la clase de lógica correspondiente, llama al método de negocio, empaqueta el resultado en un `DataSet` y lo retorna.

### 3.2. Lógica de Finiquitos (`CFiniquitos.cs`)

Maneja la lógica central de los finiquitos.

#### Consultas Directas (SQL Embebido)
Se detectaron varias consultas SQL directas en el código, lo cual es crítico para el mantenimiento:
*   **Carga Variable**: `SELECT * FROM FNVARIABLEREMUNERACION`
*   **Causales**: `SELECT ID, NUMERO, DESCRIPCION... FROM FN_CAUSAL`
*   **Documentos**: `SELECT * FROM FN_Documentos`
*   **Historial UF**: Consulta compleja para obtener la UF del mes anterior o fechas específicas desde `[dbo].[HISTORIALUF]`.
*   **Otros Haberes/Descuentos**: Consultas a `FNOtrosHaberes` y `FNDESCUENTO`.
*   **Encabezado Finiquito**: `SELECT ... FROM FN_ENCDESVINCULACION ... INNER JOIN FN_ESTADOS ... INNER JOIN FN_EMPRESAS`
*   **Excepción AFC**: `SELECT ... FROM [dbo].[ExcepcionAfc]`

#### Procedimientos Almacenados
La mayor parte de la lógica compleja reside en SPs:
*   `SP_FN_VALIDA_CONTRATOS_EN_CALCULO`
*   `SP_GET_FN_CONTRATOS_CALCULO`
*   `SP_FN_GET_FINIQUITOS_CONFIRMADOS`
*   `SP_GET_CL_VISUALIZAR_*`: Un conjunto amplio de SPs para visualizar distintos aspectos del cálculo (Datos Trabajador, Periodos, Contratos, Documentos, Totales, Haberes, etc.).

### 3.3. Lógica de Pagos (`CPagos.cs`)

Gestiona el proceso de pago de los finiquitos. Sus operaciones se basan casi exclusivamente en Procedimientos Almacenados.

*   **Gestión de Cheques y Pagos**: `SP_PG_INSERTAR_REGISTRO_PAGO`, `SP_PG_CHEQUE_PAGADO`, `SP_PG_ANULAR_PAGO`.
*   **Proveedores**: `SP_P_OBTENER_PROVEEDOR`, `SP_P_SET_REGISTRAR_PAGO_PROVEEDORES`.
*   **Procesos Temporales**: Métodos para carga masiva o temporal de cheques (`SP_TMP_CARGA_CHEQUE_*`, `SP_TMP_INIT_PROCESS_*`).

### 3.4. Integración con Softland (`CESTSoftland` y `COUTSoftland`)

Este es un punto crítico. La integración se realiza mediante consultas SQL directas a las tablas de Softland. Se distinguen dos contextos: `EST` y `OUT`, probablemente refiriéndose a distintas empresas o instancias (ej. *Estratégica* y *Outsourcing*).

> **IMPORTANTE**: Las consultas acceden a esquemas `softland.*` y `TWEST.softland.*`.

#### Análisis de Consultas a Softland (`CESTSoftland`)
*   **Centro de Costos**:
    ```sql
    SELECT ... FROM softland.sw_personal p 
    INNER JOIN softland.sw_areanegper a ...
    INNER JOIN softland.cwtaren c ...
    INNER JOIN softland.sw_ccostoper ct ...
    INNER JOIN softland.cwtccos cs ...
    WHERE p.ficha = '{0}'
    ```
    Obtiene la relación de ficha, área de negocio y centro de costo.

*   **Datos del Contrato**:
    Consulta a `softland.sw_personal` y `softland.sw_estudiosup` para obtener RUT, Nombres, Fecha Ingreso y Fecha Término.

*   **Finiquitados y Terminados**:
    Filtra personal con estado "BAJA" en `sw_estudiosup` o con fecha de término de contrato anterior a una fecha dada.

*   **Causales (Variable P062)**:
    Busca específicamente la variable `P062` en `softland.sw_variablepersona`.

*   **Cargos y Áreas de Negocio**:
    Cruces entre `sw_codineper`, `sw_codine`, `sw_cargoper`, `cwtcarg` para obtener la descripción del cargo.

*   **Ultima Liquidación**:
    Busca las últimas 5 liquidaciones en `sw_variablepersona` agrupando por mes.

*   **Información Part-Time y Jornadas**:
    Consulta variables específicas:
    *   `P119`: DIAS JORNADA
    *   `H001`: SUELDO BASE
    *   `P030`: PARTTIME
    *   `P120`: HORAS JORNADA
    *   `D023`: CREDITO CCAF

#### Análisis de Consultas a Softland (`COUTSoftland`)
Similar a `CESTSoftland` pero conectando a la base de datos `SOFTLAND_OUT`.
*   Variables específicas consultadas:
    *   `D046`: Retención Judicial.
    *   `P034`: PARTTIME (nótese la diferencia con `EST` que usa `P030` para lo mismo o similar).

## 4. Acceso a Datos e Infraestructura

La clase `SQLServerDBHelper` centraliza la conexión. Maneja dinámicamente la cadena de conexión según el parámetro `CLIENT`.

### Cadenas de Conexión
Las credenciales y servidores se obtienen de una clase `DatosXML` (no analizada en detalle, pero se infiere que lee de un XML de configuración).

Contextos identificados:
1.  **TW_SA**: Base de datos interna de la aplicación ("Servidor SATW").
    *   Usada por `CFiniquitos` y `CPagos`.
2.  **SOFTLAND_EST**: Base de datos Softland para la instancia "EST".
    *   Usada por `CESTSoftland`.
3.  **SOFTLAND_OUT**: Base de datos Softland para la instancia "OUT".
    *   Usada por `COUTSoftland`.
4.  **SOFTLAND_CONSULTORA**: Contexto adicional preparado pero con uso limitado en el código analizado.

## 5. Conclusiones y Puntos de Atención

1.  **Dependencia Fuerte de Stored Procedures**: La lógica de negocio compleja ("Calculo de Finiquito") está delegada a la base de datos (`SP_GET_CL_*`). Esto significa que para entender *cómo* se calcula el finiquito exacto, se debe revisar el código SQL de esos procedimientos.
2.  **SQL Embebido**: Existen consultas SQL "hardcoded" en C# (`SELECT * FROM...`), lo que dificulta el mantenimiento y la optimización.
3.  **Acoplamiento con Softland**: La aplicación consulta directamente las tablas de Softland (`sw_personal`, `sw_variablepersona`, etc.). Cualquier cambio en la estructura de Softland romperá esta integración.
4.  **Manejo de Variables**: El sistema depende de códigos de variables específicos (`P062`, `P030`, `H001`, `D023`, `D046`) que actúan como "Magic Strings". Es crucial documentar qué significa cada variable en el contexto de negocio.

---
*Documento generado automáticamente por el Agente de Análisis de Código - 2026*
