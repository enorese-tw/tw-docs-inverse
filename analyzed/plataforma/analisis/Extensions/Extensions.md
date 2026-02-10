# Documentación de Teamwork.Extensions

Este documento analiza la capa `Teamwork.Extensions`, que en este proyecto se centra exclusivamente en la generación de reportes en formato Excel (.xlsx) utilizando la librería `EPPlus`.

## Estructura General

La extensión contiene un único submódulo: `Excel`. Todas las clases dentro de este módulo siguen un patrón similar:
1.  Recibir parámetros (filtros, datos).
2.  Obtener datos desde una API o Servicio (generalmente retornando JSON o DataSet).
3.  Utilizar `ExcelPackage` para construir el archivo.
4.  Retornar un arreglo de bytes (`byte[]`) para la descarga del archivo.

## Archivos Analizados

### 1. `XLSX_ReporteAsistencia.cs`
Este es el archivo más complejo del módulo, encargado de generar el reporte mensual de asistencia.

-   **Entrada**: Listas de `ReporteAsistencia`, `BajasConfirmadas`, `HorasExtras`, `JornadaLaboral`, `FichaBonos`, `Leyenda`.
-   **Lógica de Negocio Principal**:
    -   **Generación de Calendario**: Construye dinámicamente las columnas del reporte basándose en los días del mes del periodo consultado, coloreando automáticamente los fines de semana.
    -   **Cálculo de Asistencia**:
        -   Itera día a día para cada trabajador.
        -   Cruza información con fechas de contrato, bajas confirmadas y tipos de ausencia.
        -   Calcula contadores: `DiasTrabajados`, `DiasLibres`, `DiasFaltas`, `DiasLicenciaMedica`, `DiasVacaciones`, `DiasSinContrato`.
    -   **Códigos de Ausencia**:
        -   `LM`: Licencia Médica.
        -   `V`: Vacaciones.
        -   `F`: Faltas.
        -   `L`: Libres.
        -   `SL`, `PGS`, `PSG`, `PLM`, `PF`, `CPR`: Varios tipos de permisos y ausencias.
    -   **Estados Especiales**:
        -   `SC` (Sin Contrato): Si la fecha consultada está fuera del rango del contrato.
        -   `BC` (Baja Confirmada): Si existe una baja confirmada para esa fecha.
        -   `PCK` (Pre-Check): Estado intermedio de validación.
        -   `BK` (Baja KAM): Baja informada por el Key Account Manager.
    -   **Formato Visual**: Aplica colores de fondo y fuente (`Style`, `Color`) definidos en los datos o calculados (ej: rojo para bajas).
    -   **Secciones Adicionales**: Agrega columnas para Horas Extras y Bonos si existen datos para el trabajador.
    -   **Hojas Extra**: Genera hojas de "Tipos Ausencias" (Leyenda) y "Jornadas Laborales".

### 2. `XLSX_ReporteTransferencias.cs`
Genera archivos de transferencias bancarias (TEF), con una lógica compleja de agrupación y división de archivos.

-   **Lógica de Negocio**:
    -   **Límites de Cuenta**: Maneja límites de monto máximo por archivo/lote para ciertas cuentas (ej: límite de 10.000.000). Si un lote supera el límite, divide el pago en múltiples registros o archivos.
    -   **Agrupación**: Agrupa transferencias por cuenta origen.
    -   **Manejo de Cuentas**: Tiene lógica hardcoded para cambiar cuentas de origen específicas (ej: de "2504898" a "87274039").
    -   **Generación Múltiple**: Puede retornar una lista de archivos Excel (`List<byte[]>`) si los montos o la cantidad de registros lo requieren.
    -   **Protección**: El archivo generado está protegido con contraseña (`"domCLt3am.W0rk"`).

### 3. `XLSX_ReporteFiniquitos.cs`
Genera el reporte consolidado y detallado de finiquitos.

-   **Datos**: Obtiene datos ejecutando el procedimiento almacenado a través de `servicioOperaciones.CrudMantencionFiniquitos`.
-   **Estructura**:
    -   Hoja "Consolidado": Resumen de finiquitos.
    -   Hoja "Detalle de Folio": Detalle desglosado por folio.
-   **Parametrización**: Construye un array de 34 parámetros (`@ACCION`, `@FECHA`, etc.) para consultar la base de datos. Acción: `REPORTEXCELFIN`.

### 4. `XLSX_ReporteValeVista.cs`
Reporte específico para pagos por Vale Vista.

-   **Acción**: Llama a `CrudMantencionFiniquitos` con acción `REPORTEXCELVALEVISTA`.
-   **Seguridad**: Archivo protegido con contraseña (`"domCLt3am.W0rk"`).

### 5. `XLSX_ReporteSolicitudContratos.cs`
Reporte de solicitudes de contrato.

-   **Lógica**: Define manualmente 35 columnas (A-AI) con encabezados específicos (`Rut Trabajador`, `Cargo`, `Sueldo`, etc.).
-   **Múltiples Hojas**: Crea una hoja nueva por cada combinación de Código y Empresa (`"Contratos " + rows["Codigo"] + ...`).

### 6. Otros Reportes
Reportes más simples que vuelcan datos JSON o DataSet directamente a Excel:
-   **`XLSX_ReporteContratos.cs`**: Llama a API `__ReporteContratos`.
-   **`XLSX_ReporteRenovaciones.cs`**: Llama a API `__ReporteRenovaciones`.
-   **`XLSX_ReporteFirmaDigital.cs`**: Llama a `CollectionsReporte.__ReporteCargoMod`.
-   **`XLSX_ReporteFiniquitoProcesoMasivo.cs`**: Reporte de errores o estado de cargas masivas.
-   **`XLSX_ReporteSolicitudes.cs`**: Reporte dinámico de solicitudes, agrupando por tipo en hojas separadas.
