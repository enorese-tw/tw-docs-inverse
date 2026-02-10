# Documentación de Consultas SQL y Datos

Este documento detalla todas las interacciones con bases de datos identificadas en el código fuente, incluyendo consultas SQL directas, llamadas a Stored Procedures y el uso de los datos recuperados.

> [!CAUTION]
> **Vulnerabilidad Crítica de Inyección SQL**: Se detectó el uso extensivo de consultas SQL construidas mediante concatenación de strings o `string.Format` con datos de entrada no sanitizados (e.g., RUT, Ficha). Esto expone la aplicación a ataques de Inyección SQL.
>
> **Ejemplo Vulnerable (`Clases/Contrato.cs`):**
> ```csharp
> strSql = string.Format("SELECT ... WHERE p.rut like '" + rut + "' ...");
> ```

## 1. Clases de Negocio (`FiniquitosV2.Clases`)

### `Contrato.cs`

Esta clase gestiona la recuperación de información de empleados y contratos desde Softland.

| Método | Consulta SQL (Simplificada) | Propósito | Uso de Datos |
|---|---|---|---|
| `ContratoActivo` | `SELECT p.ficha, p.rut, p.nombres, p.fechaIngreso, e.descripcion, p.FecTermContrato FROM softland.sw_personal p ... WHERE p.rut like '{rut}' and p.FecTermContrato >= '{hoy}'` | Obtener datos del contrato activo de un trabajador. | Llena las propiedades `ficha`, `nombres`, `fechaIngreso`, `estudios`, `FecTermContrato` para mostrarlos en la UI. |
| `centrodecosto` | `SELECT c.DescCC FROM ... softland.sw_ccostoper ... WHERE p.ficha = '{ficha}'` | Obtener la descripción del centro de costo. | Llena la propiedad `centrocosto` utilizada en reportes y visualización. |
| `ConsultarRutExistenteRecepcion` | `SELECT COUNT(*) 'EXISTE' FROM softland.sw_personal WHERE rut LIKE '{rut}'` | Verificar si un RUT existe en la base de datos. | Retorna `1` si existe, usado para validaciones de entrada. |
| `RutTrabajadorSolicitudBaja` | `SELECT DISTINCT p.rut, p.nombres FROM softland.sw_personal ... WHERE rut like '{rut}'` | Buscar trabajador por RUT para solicitudes de baja. | Muestra el nombre del trabajador en la UI de solicitud. |
| `ContratosTerminados` | `SELECT ... DATEDIFF(day, fechaIngreso, FecTermContrato) as Dias FROM softland.sw_personal WHERE ficha = '{ficha}' ...` | Obtener detalles de contratos terminados, incluyendo cálculo de días trabajados. | Calcula `MesesParaVacaciones` y `TotalDiaHabiles` para el finiquito. |
| `ContratoSeguimiento` | `SELECT ... FN_Contratos ... FN_Finiquito ...` | Consultar estado de seguimiento en tablas `TW_OPERACIONES`. | Recupera el estado (`Seguimiento`) para mostrar flujo de aprobación. |
| `CausalFicha` | `SELECT VALOR FROM softland.sw_variablepersona_nuevo WHERE ficha='{ficha}' AND codvariable='P062'` | Obtener la causal de despido registrada en variables de personal. | Asigna la propiedad `Causal` (aunque depende de una variable específica P062). |
| `ListarFiniquitados` | `SELECT p.ficha, p.rut FROM softland.sw_personal ... WHERE e.descripcion LIKE '%BAJA%' ...` | Listar empleados con contratos marcados como "BAJA". | Retorna lista para grillas de selección de finiquitos a calcular. |

### `AreaNegocio.cs`

| Método | Consulta SQL | Propósito | Uso de Datos |
|---|---|---|---|
| `Obtener` | `SELECT ... softland.cwtaren.DesArn FROM softland.cwtaren ... WHERE ficha = '{ficha}'` | Obtener el Área de Negocio de un empleado. | Llena `codanrg` y `areanegocio` para lógica de negocio y reportes. |

### `Cargo.cs`

| Método | Consulta SQL | Propósito | Uso de Datos |
|---|---|---|---|
| `Obtener` | `SELECT ... softland.cwtcarg.CarNom ... FROM ... WHERE ficha = '{ficha}'` | Obtener el Cargo y Cargo Mod. | Muestra el cargo en la UI y documentos generados. |

### `Causales.cs`

| Método | Consulta SQL | Propósito | Uso de Datos |
|---|---|---|---|
| `cargarCausales` | `SELECT * FROM FN_CAUSAL ORDER BY ID ASC` | Listar todas las causales de despido disponibles. | Puebla DropDownLists para selección de causal. |
| `cargarCausalDocumento` | `SELECT ... FROM FN_CAUSALDOCUMENTO WHERE NUMERO LIKE ...` | Obtener descripción asociada a un código de causal legal. | Usado para generar el texto legal en los documentos PDF. |

### `Remuneraciones.cs`

| Método | Consulta SQL | Propósito | Uso de Datos |
|---|---|---|---|
| `liquidaciones` | `SELECT ficha, mes FROM TWEST.softland.sw_variablepersona WHERE ficha = '{ficha}' ...` | Obtener meses con liquidaciones (Bug: solo retorna 1). | Intentaba listar períodos de remuneración. |

---

## 2. Formularios Web (`.aspx.cs`)

### `CalculoBajaEst.aspx.cs` y `CalculoBajaOut.aspx.cs`

Estos archivos contienen lógica compleja de cálculo con SQL incrustado.

#### 1. Búsqueda de Contratos (`buscarcontratos`)
Se instancian objetos `Clases.Contrato` que ejecutan las consultas mencionadas arriba (`Contrato.ListarFiniquitados`) para poblar el `GridView1` con contratos disponibles para calcular.

#### 2. Cálculo de Variables (`BtnCalculaDobleFactor`, `btnCalcular_Click`)

**En `CalculoBajaOut.aspx.cs` (Lógica más compleja):**

*   **Identificación de Variables**:
    *   `SELECT DISTINCT ', ''' + var.codVariable +'''' FROM [dbo].[FNVARIABLEREMUNERACIONOUT] ...`
    *   `SELECT STUFF(...)`
    *   **Propósito**: Construye dinámicamente una lista de códigos de variables (haberes) a considerar para el promedio de remuneraciones variables (Art. 172).

*   **Valores Mensuales (Loop N+1)**:
    *   El código itera por cada "mes completo" hacia atrás (3 meses para variables, o más).
    *   **Consulta**:
        ```sql
        SELECT sv.descripcion, MesProceso, Valor
        FROM [softland].[Sw_VariablePersona_NUEVO] svpn
        INNER JOIN [softland].[sw_variable] sv ON ...
        WHERE svpn.ficha = '{ficha}'
          AND svpn.CodVariable IN ({variables_detectadas})
          AND svpn.MesProceso = cast('{fecha}' as date)
        ```
    *   **Uso de Datos**:
        *   Llena una `DataTable` dinámica (`dt`).
        *   Calcula el **Promedio de Remuneración Variable**.
        *   Detecta "DIAS TRABAJADOS" para normalizar sueldos si hay licencias/ausencias.
        *   El resultado final (`sumTotalRemuneraciones`) se usa para el cálculo de indemnizaciones.

*   **Obtención de Fechas/Meses**:
    *   `SELECT ... FechaMes FROM softland.sw_variablepersona ... INNER JOIN softland.sw_vsnpRetornaFechaMesExistentes ...`
    *   Probablemente usado para mapear índices de meses (enteros) a fechas reales.

#### 3. Información Adicional

*   **Alertas Informativas**:
    *   Usa `svcFiniquitos.GetAlertasInformativas` (WCF) pero en algunos casos hay consultas directas comentadas o legacy.
    *   **Propósito**: Detectar Sobregiros, Licencias ACHS, Retenciones Judiciales.

---

## 3. Stored Procedures (`api/ResponseApi.cs`)

La generación de documentos PDF delega la lógica de obtención de datos a Stored Procedures.

| Método C# | Stored Procedure | Propósito |
|---|---|---|
| `CreatePdfDocument` (caratula) | `[finiquitos].[__PdfFiniquitoCaratula] @idfiniquito` | Generar datos para la carátula del finiquito. |
| `CreatePdfDocument` (documento) | `[finiquitos].[__PdfFiniquitoDocumento] @idfiniquito` | Generar el cuerpo principal del finiquito legal. |
| `CreatePdfDocument` (propuesta) | `[finiquitos].[__PdfCarta] @idfiniquito` | Generar la carta de aviso/propuesta. |

> [!NOTE]
> Estos SPs (`[finiquitos].*`) encapsulan gran parte de la lógica de presentación de los documentos, extrayendo datos de las tablas `FN_Finiquito`, `FN_Contratos`, y tablas de Softland.

---

## 4. Tablas Principales Impactadas

Basado en las consultas, el sistema lee/escribe principalmente en:

**Base de Datos Softland (Lectura)**:
*   `sw_personal`: Datos maestros de empleados.
*   `sw_estudiosup`: Usado para filtrar estado ("BAJA").
*   `sw_areanegper`, `cwtaren`: Áreas de negocio.
*   `sw_cargoper`, `cwtcarg`, `sw_codineper`: Cargos.
*   `sw_ccostoper`: Centros de costo.
*   `sw_variablepersona`, `Sw_VariablePersona_NUEVO`: Liquidaciones, haberes variables.

**Base de Datos Operaciones (Lectura/Escritura)**:
*   `FN_Contratos`: Gestión de contratos en el sistema de finiquitos.
*   `FN_Finiquito`: Cabecera de finiquitos calculados.
*   `FN_CAUSAL`, `FN_CAUSALDOCUMENTO`: Maestros de causales.
*   `FNVARIABLEREMUNERACION*`: Configuración de variables a considerar.

## 5. Resumen de Flujo de Datos

1.  **Entrada**: Usuario ingresa RUT.
2.  **Búsqueda**: Se consulta `sw_personal` para verificar existencia y contratos.
3.  **Cálculo**:
    *   Se recuperan datos base (Cargo, Área) de Softland.
    *   Se recuperan remuneraciones históricas (`sw_variablepersona`) para calcular promedios (Topes, Variables).
    *   Se aplican fórmulas en C# (`TotalDiaHabiles`, `FeriadoProporcional`).
4.  **Persistencia**: (No detallada aquí pero implícita en `btnGrabar`) Se guarda en `FN_Finiquito`.
5.  **Salida**: Se llama a SPs `[finiquitos].__Pdf*` pasándoles el ID generado para crear el PDF.
