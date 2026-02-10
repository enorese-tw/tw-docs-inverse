# Documentación de Consultas: `COUTSoftland.cs`

## Contexto
Esta clase maneja la integración con la instancia **OUT** (Outsourcing) de Softland. Aunque comparte estructura con EST, tiene queries específicos y variables de negocio distintas.

## Consultas Identificadas

### 1. Validación de Trabajador (OUT)
*   **Método**: `GetRutTrabajadorSolicitudBajaOUT`
*   **Query**:
    ```sql
    SELECT DISTINCT p.rut, p.nombres as nombres 
    FROM softland.sw_personal p 
    INNER JOIN softland.sw_estudiosup e on e.codEstudios like p.codEstudios 
    WHERE p.rut like '{0}'
    ```
*   **Propósito**: Validar existencia y obtener nombre oficial del trabajador en la base OUT.

### 2. Estructura Organizacional (Cargo y Centro de Costo)
*   **Método**: `GetCargoOUT`
*   **Query**:
    ```sql
    SELECT softland.sw_codine.codIne as CodigoINE, 
           softland.sw_codine.descripcion as GlosaINE, 
           softland.cwtcarg.carCod as carCod, 
           softland.cwtcarg.CarNom as CarNom 
    FROM softland.sw_codineper 
    INNER JOIN softland.sw_codine ...
    INNER JOIN softland.sw_cargoper ...
    INNER JOIN softland.cwtcarg ...
    WHERE (softland.sw_codineper.ficha = '{0}')
    ```
*   **Propósito**: Obtener detalles del cargo y clasificación INE para reportes legales.

*   **Método**: `GetCentroCostosOUT`
*   **Query**: Similar a `GetCargoOUT` pero orientado a `cwtaren` (Área de Negocio) y `cwtccos` (Centro de Costo).
*   **Propósito**: Asignación de costos contables.

### 3. Retención Judicial
*   **Método**: `GetRetencionJudicialOUT`
*   **Query**:
    ```sql
    SELECT TOP(1) 'Finqiuito contiene retención judicial' 'RETENCION', valor 
    FROM softland.sw_variablepersona 
    WHERE codVariable like 'D046' 
    AND FICHA LIKE '{0}' 
    ORDER BY MES DESC
    ```
*   **Propósito**: Alertar si el trabajador tiene retenciones judiciales que deben descontarse del finiquito.
*   **Variable Crítica**: `D046`.
*   **Lógica**: Si la consulta retorna filas, existe retención.

### 4. Jornada y Sueldo Base (Part-Time)
*   **Método**: `GetJornadasParttimeOUT`
*   **Query**:
    ```sql
    SELECT H001.valor 'SUELDO BASE', P034.valor 'PARTTIME' ...
    FROM softland.sw_variablepersona H001, 
         softland.sw_variablepersona P034 ...
    WHERE H001.codVariable = 'H001' AND P034.codVariable = 'P034' ...
    ```
*   **Propósito**: Obtener parámetros para cálculo de sueldo proporcional.
*   **Variables**:
    *   `H001`: Sueldo Base.
    *   `P034`: Indicador Part-Time (Diferente a EST que usa P030).
