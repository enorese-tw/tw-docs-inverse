# Documentación Detallada: `COUTSoftland.cs`

## Visión General
La clase `COUTSoftland` gestiona la integración con la instancia **OUT** (Outsourcing o similar) del ERP Softland. Su estructura es similar a `CESTSoftland` pero adaptada para un contexto de negocio diferente, probablemente manejando una nómina o empresa distinta.

## Dependencias
*   `System.Data` (DataTable)
*   `SQLServerDBHelper`: Contexto de base de datos `"SOFTLAND_OUT"`.

## Contexto de Base de Datos
*   **Identificador de Conexión**: `"SOFTLAND_OUT"`
*   **Esquemas Utilizados**: `softland`.

## Análisis de Métodos y Lógica de Negocio

### 1. Información del Trabajador y Contrato

#### `GetRutTrabajadorSolicitudBajaOUT(string[] parametros)`
*   **Objetivo**: Obtener el RUT y nombre del trabajador para procesos de baja.
*   **Lógica**: `SELECT DISTINCT` sobre `sw_personal` y `sw_estudiosup`. Idéntica a la implementación de `CESTSoftland`.

### 2. Estructura Organizacional

#### `GetCargoOUT(string[] parametros)`
*   **Objetivo**: Obtener información detallada del cargo del trabajador.
*   **Consulta Compleja**:
    ```sql
    SELECT ... FROM softland.sw_codineper
    INNER JOIN softland.sw_codine ...
    INNER JOIN softland.sw_cargoper ...
    INNER JOIN softland.cwtcarg ...
    WHERE (softland.sw_codineper.ficha = '{0}')
    ```
    Recupera códigos INE y descripciones internas del cargo.

#### `GetCentroCostosOUT(string[] parametros)`
*   **Objetivo**: Obtener el Centro de Costo asociado.
*   **Relación**: Ficha -> Área Negocio -> Centro de Costo.
*   **Tablas**: `sw_personal`, `sw_areanegper`, `cwtaren`, `sw_ccostoper`, `cwtccos`.

### 3. Variables Financieras y Judiciales

#### `GetRetencionJudicialOUT(string[] parametros)`
*   **Objetivo**: Verificar si el trabajador tiene **Retención Judicial**.
*   **Variable Crítica**: `D046`.
*   **Lógica**:
    *   Busca la variable `D046` en `sw_variablepersona`.
    *   Ordena por mes descendente (`ORDER BY MES DESC`).
    *   Toma el último valor (`TOP(1)`).
    *   Retorna un indicador textual "Finqiuito contiene retención judicial".

#### `GetJornadasParttimeOUT(string[] parametros)`
*   **Objetivo**: Recuperar variables de jornada y sueldo base.
*   **Mapeo de Variables (Hardcoded)**:
    *   `H001`: **SUELDO BASE**
    *   `P034`: **PARTTIME**
*   **Diferencia Clave con EST**: En este contexto, la variable para Part-Time es `P034`, mientras que en `CESTSoftland` era `P030`. Esto indica una configuración de nómina distinta entre ambas instancias.

## Resumen de Variables Softland Identificadas (Contexto OUT)
| Variable | Descripción en Código | Uso | Diferencia con EST |
| :--- | :--- | :--- | :--- |
| **D046** | RETENCIÓN JUDICIAL | `GetRetencionJudicialOUT` | No presente en EST |
| **H001** | SUELDO BASE | `GetJornadasParttimeOUT` | Igual |
| **P034** | PARTTIME | `GetJornadasParttimeOUT` | EST usa **P030** |

## Observaciones de Negocio
*   La existencia de métodos específicos para `OUT` con diferentes códigos de variables (`P034` vs `P030`) sugiere que el sistema maneja múltiples empresas o configuraciones de Softland que no están estandarizadas entre sí.
*   El manejo de retenciones judiciales (`D046`) parece ser específico de este contexto o módulo.
