# Documentación Detallada: `CESTSoftland.cs`

## Visión General
La clase `CESTSoftland` gestiona la integración con la instancia **EST** (Estratégica o similar) del ERP Softland. Contiene una amplia gama de métodos para recuperar información detallada sobre trabajadores, contratos, centros de costo, cargos y liquidaciones.

## Dependencias
*   `System.Data` (DataTable)
*   `SQLServerDBHelper`: Contexto de base de datos `"SOFTLAND_EST"`.

## Contexto de Base de Datos
*   **Identificador de Conexión**: `"SOFTLAND_EST"`
*   **Esquemas Utilizados**: `softland` y `TWEST.softland` (lo que sugiere accesos cruzados entre bases de datos o esquemas específicos).

## Análisis de Métodos y Lógica de Negocio

### 1. Información del Trabajador y Contrato

#### `GetContratoActivoBaja(string[] parametros)`
*   **Objetivo**: Obtener el historial de contratos de un trabajador, ordenados por fecha de término descendente.
*   **Tablas**: `softland.sw_personal` (p), `softland.sw_estudiosup` (e).
*   **Relación**: `e.codEstudios like p.codEstudios`
*   **Filtro**: `p.rut like '{0}'`
*   **Campos Clave**: `ficha`, `rut`, `nombres`, `fechaIngreso`, `codEstudios`, `FecTermContrato`.

#### `GetRutTrabajadorSolicitudBaja(string[] parametros)`
*   **Objetivo**: Obtener RUT y nombre para solicitud de baja.
*   **Lógica**: `SELECT DISTINCT` sobre `sw_personal` y `sw_estudiosup` filtrando por RUT.

#### `GetListarFiniquitados(string[] parametros)`
*   **Objetivo**: Listar trabajadores finiquitados.
*   **Lógica de Negocio Crítica**: Filtra donde `e.descripcion LIKE '%BAJA%'`. Esto implica que el estado de "Baja" se determina por la descripción del nivel de estudios (`codEstudios`), lo cual es una regla de negocio muy específica y poco convencional.

#### `GetListarContratosTerminados(string[] parametros)`
*   **Objetivo**: Listar contratos que han terminado antes de una fecha específica.
*   **Filtros**:
    *   `ficha = '{0}'`
    *   `FecTermContrato < '{1}'`
*   **Cálculo**: `DATEDIFF(day, fechaIngreso, FecTermContrato) as Dias` (Calcula la duración del contrato en días directamente en SQL).

### 2. Estructura Organizacional (Cargos y Centros de Costo)

#### `GetCentroCostos(string[] parametros)`
*   **Objetivo**: Obtener el Centro de Costo asociado a la ficha del trabajador.
*   **Cadena de Relación Compleja**:
    `sw_personal` -> `sw_areanegper` -> `cwtaren` (Área Negocio)
    `sw_personal` -> `sw_ccostoper` -> `cwtccos` (Centro de Costo)
*   **Retorno**: `ficha`, `codarn` (Código Área), `desarn` (Descripción Área), `DescCC` (Descripción Centro Costo).

#### `GetCargo(string[] parametros)`
*   **Objetivo**: Obtener información del cargo.
*   **Tablas**: `sw_codineper`, `sw_codine`, `sw_cargoper`, `cwtcarg`.
*   **Retorno**: Código INE, Glosa INE, Código Cargo (`carCod`), Nombre Cargo (`CarNom`).

#### `GetAreaNegocio(string[] parametros)`
*   **Objetivo**: Obtener el Área de Negocio vigente.
*   **Ordenamiento**: `vigHasta DESC` (Para obtener la asignación más reciente o vigente).

### 3. Liquidaciones y Variables (Lógica Financiera)

#### `GetUltimaLiquidacion(string[] parametros)`
*   **Objetivo**: Recuperar los últimos 5 meses con registros de liquidación.
*   **Lógica**: Consulta `sw_variablepersona` agrupando por mes y ficha. Depende de `sw_vsnpRetornaFechaMesExistentes` para traducir el índice del mes a fecha real.

#### `GetCausalFicha(string[] parametros)`
*   **Objetivo**: Obtener la causal de despido/término.
*   **Variable Crítica**: `P062`. El sistema asume que la variable `P062` en `sw_variablepersona` almacena la causal.
*   **Base de Datos Cruzada**: `TWEST.softland.sw_variablepersona`. Nótese el prefijo `TWEST`.

#### `GetJornadasParttimeEST(string[] parametros)`
*   **Objetivo**: Recuperar variables asociadas a jornadas y sueldos.
*   **Mapeo de Variables (Hardcoded)**:
    *   `P119`: **DIAS JORNADA**
    *   `H001`: **SUELDO BASE**
    *   `P030`: **PARTTIME**
    *   `P120`: **HORAS JORNADA**
*   **Importancia**: Estos códigos son reglas de negocio "duras" en el código.

#### `GetTrabajadorPartTimeEST(string[] parametros)`
*   **Objetivo**: Verificar si es Part-Time.
*   **Variable**: `P030`.

#### `GetCreditoCCAFEST(string[] parametros)`
*   **Objetivo**: Verificar si tiene crédito en Caja de Compensación (CCAF).
*   **Variable**: `D023`.

## Resumen de Variables Softland Identificadas
| Variable | Descripción en Código | Uso |
| :--- | :--- | :--- |
| **P062** | Causal | `GetCausalFicha` |
| **P119** | DIAS JORNADA | `GetJornadasParttimeEST` |
| **H001** | SUELDO BASE | `GetJornadasParttimeEST` |
| **P030** | PARTTIME | `GetJornadasParttimeEST`, `GetTrabajadorPartTimeEST` |
| **P120** | HORAS JORNADA | `GetJornadasParttimeEST` |
| **D023** | CREDITO CCAF | `GetCreditoCCAFEST` |
