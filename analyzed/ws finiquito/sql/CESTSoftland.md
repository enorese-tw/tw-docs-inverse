# Documentación de Consultas: `CESTSoftland.cs`

## Contexto
Esta clase interactúa con la instancia **EST** (Estratégica) de Softland. Contiene lógica crítica para la recuperación de datos contractuales y variables financieras.

## Consultas Identificadas

### 1. Historial de Contratos
*   **Método**: `GetContratoActivoBaja`
*   **Query**:
    ```sql
    SELECT p.ficha as ficha, p.rut, p.nombres as nombres, p.fechaIngreso as fechaIngreso,
           e.descripcion as codEstudios, p.FecTermContrato as FecTermContrato 
    FROM softland.sw_personal p 
    INNER JOIN softland.sw_estudiosup e on e.codEstudios like p.codEstudios 
    WHERE p.rut like '{0}' 
    ORDER BY fectermcontrato DESC;
    ```
*   **Propósito**: Obtener todos los contratos (activos e inactivos) de un trabajador a partir de su RUT.
*   **Uso**: Permite seleccionar sobre qué contrato se realizará el finiquito. Ordenados por fehca de término para mostrar los más recientes primero.

### 2. Validación de Trabajador
*   **Método**: `GetRutTrabajadorSolicitudBaja`
*   **Query**:
    ```sql
    SELECT DISTINCT p.rut, p.nombres as nombres 
    FROM softland.sw_personal p ...
    ```
*   **Propósito**: Identificar un trabajador válido en la base EST.

### 3. Listado de Finiquitados
*   **Método**: `GetListarFiniquitados`
*   **Query**:
    ```sql
    SELECT p.ficha as ficha, p.rut, ... 
    FROM softland.sw_personal p 
    INNER JOIN softland.sw_estudiosup e on e.codEstudios like p.codEstudios 
    WHERE e.descripcion like '%BAJA%' ...
    ```
*   **Propósito**: Encontrar trabajadores que ya han sido marcados como "Baja" en el sistema.
*   **Lógica Crítica**: Se basa en que la *descripción de estudios* contenga la palabra "BAJA". Esto parece ser un "hack" o una convención muy específica para marcar estados, en lugar de usar un campo de estado dedicado.

### 4. Última Liquidación de Sueldo
*   **Método**: `GetUltimaLiquidacion`
*   **Query**:
    ```sql
    SELECT TOP(5) max(mes) as Mes, softland.sw_vsnpRetornaFechaMesExistentes(max(mes)) as Glosa 
    FROM softland.sw_variablepersona 
    WHERE ficha = '{0}' 
    GROUP BY mes 
    ORDER BY mes DESC;
    ```
*   **Propósito**: Obtener los últimos 5 meses procesados en nómina.
*   **Uso**: Para cálculos de promedios de sueldos variables.
*   **Función SQL**: Utiliza `softland.sw_vsnpRetornaFechaMesExistentes` para formatear la fecha.

### 5. Obtención de Causal de Despido
*   **Método**: `GetCausalFicha`
*   **Query**:
    ```sql
    SELECT TOP(1) valor 
    FROM TWEST.softland.sw_variablepersona 
    WHERE codVariable like 'P062' 
    AND FICHA LIKE '{0}' 
    ORDER BY MES DESC
    ```
*   **Propósito**: Recuperar la causal de despido asignada.
*   **Variable**: `P062` (Magic String).
*   **Uso**: Determina el cálculo de indemnizaciones.

### 6. Datos de Cargo y Centro de Costo
*   **Métodos**: `GetCentroCostos`, `GetCargo`, `GetAreaNegocio`.
*   **Queries**: JOINS complejos entre `sw_personal`, `sw_areanegper`, `cwtaren`, `sw_ccostoper`, `cwtccos`, `sw_cargoper`, `cwtcarg`.
*   **Propósito**: Obtener la estructura organizacional del trabajador al momento del finiquito.

### 7. Variables de Jornada (Part-Time)
*   **Método**: `GetJornadasParttimeEST`
*   **Query**:
    ```sql
    SELECT P119.valor 'DIAS JORNADA', H001.valor 'SUELDO BASE', 
           P030.valor 'PARTTIME', P120.valor 'HORAS JORNADA' ...
    ```
*   **Propósito**: Obtener parámetros de jornada y sueldo para el cálculo.
*   **Variables**: `P119`, `H001`, `P030`, `P120`.

### 8. Crédito CCAF
*   **Método**: `GetCreditoCCAFEST`
*   **Query**:
    ```sql
    SELECT valor 
    FROM softland.sw_variablepersona 
    WHERE codVariable like 'D023' ...
    ```
*   **Propósito**: Obtener descuento por crédito social.
*   **Variable**: `D023`.
