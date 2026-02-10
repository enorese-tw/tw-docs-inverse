# Análisis de Reportes Excel (Extensions)

Este documento detalla las clases generadoras de reportes en `Teamwork.Extensions.Excel`.

## Reportes de Asistencia
### `XLSX_ReporteAsistencia`
**Archivo**: `Excel/XLSX_ReporteAsistencia.cs`
Complejo generador de reportes de asistencia mensual.
-   **Método Principal**: `ReporteAsistencia`
    -   **Entradas**: Listas de `ReporteAsistencia` (datos), `BajasConfirmadas`, `HorasExtras`, `JornadaLaboral`, etc.
    -   **Lógica de Negocio Destacada**:
        -   Calcula días trabajados restando ausencias (Faltas, Licencias, Vacaciones) del total de días del mes.
        -   Maneja lógica compleja para "Días Sin Contrato" y periodos de "Baja".
        -   Colorea celdas según el tipo de asistencia (Feriados, Fines de semana, Ausencias).
        -   Genera hojas adicionales para "Tipos Ausencias" y "Jornadas Laborales".

## Reportes de Contratos
### `XLSX_ReporteContratos`
**Archivo**: `Excel/XLSX_ReporteContratos.cs`
-   **Entradas**: Filtros de cliente y fechas.
-   **API**: `CallAPICargaMasiva.__ReporteContratos`.
-   **Salida**: Excel plano con los datos retornados por la API.

### `XLSX_ReporteSolicitudContratos`
**Archivo**: `Excel/XLSX_ReporteSolicitudContratos.cs`
-   **Entradas**: `DataSet` con datos de contratos.
-   **Lógica**:
    -   Agrupa los contratos en hojas separadas por empresa y código (`"Contratos " + Codigo + Empresa`).
    -   Mapeo manual de columnas (35 columnas definidas) como "Rut Trabajador", "Cargo", "Sueldo Base", etc.

## Reportes de Finiquitos
### `XLSX_ReporteFiniquitos`
**Archivo**: `Excel/XLSX_ReporteFiniquitos.cs`
-   **Entradas**: Filtros de finiquito y `servicioOperaciones` (inyección de dependencia dinámica).
-   **Lógica Destacada**:
    -   Construye manualmente arreglos de parámetros (`@ACCION`, etc.) para llamar a un procedimiento almacenado vía `servicioOperaciones.CrudMantencionFiniquitos`.
    -   Genera dos hojas:
        1.  "Consolidado": Resumen general.
        2.  "Detalle de Folio": Detalle específico.

### `XLSX_ReporteFiniquitoProcesoMasivo`
**Archivo**: `Excel/XLSX_ReporteFiniquitoProcesoMasivo.cs`
-   **API**: `CollectionExcel.__ReporteFiniquitos`.
-   **Salida**: Reporte simple volcando el JSON a Excel.

### `XLSX_ReporteValeVista`
**Archivo**: `Excel/XLSX_ReporteValeVista.cs`
-   **Entradas**: `usuarioCreador`, `proceso`.
-   **Lógica**: Llama a `servicioOperaciones` con acción `REPORTEXCELVALEVISTA`.
-   **Seguridad**: Protege la hoja con contraseña (`"domCLt3am.W0rk"`).

## Reportes de Transferencias
### `XLSX_ReporteTransferencias`
**Archivo**: `Excel/XLSX_ReporteTransferencias.cs`
Genera archivos para transferencias bancarias (TEF).
-   **Lógica de Negocio Crítica**:
    -   **Límite de Monto**: Divide las transferencias si el total agrupado supera un límite (parece ser 7.000.000 o 10.000.000, hay dos variables contradictorias `limitAccount` y `tope`).
    -   **Cambio de Cuenta**: Reemplaza cuenta origen específica (`2504898` -> `87274039`).
    -   **Seguridad**: Protege con contraseña (`"domCLt3am.W0rk"`).
-   **Salida**: Objeto `Files` que contiene una lista de archivos Excel (byte arrays), ya que pueden generarse múltiples archivos por lote.

## Otros Reportes
### `XLSX_ReporteSolicitudes`
**Archivo**: `Excel/XLSX_ReporteSolicitudes.cs`
-   **Lógica**: Genera múltiples hojas prefijadas con "Bajas " basado en la columna 13 de los datos (probablemente la Sucursal o Empresa).

### `XLSX_ReporteRenovaciones`
**Archivo**: `Excel/XLSX_ReporteRenovaciones.cs`
-   **API**: `CallAPICargaMasiva.__ReporteRenovaciones`.
-   **Salida**: Volcado directo de JSON.

### `XLSX_ReporteFirmaDigital`
**Archivo**: `Excel/XLSX_ReporteFirmaDigital.cs`
-   **API**: `CollectionsReporte.__ReporteCargoMod`.
-   **Salida**: Volcado directo.
