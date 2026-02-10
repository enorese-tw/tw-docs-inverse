# Otros Reportes Excel

Documentación de reportes menores y de soporte.

## 1. XLSX_ReporteValeVista
Reporte para pagos mediante Vale Vista bancario.

- **Datos**: Obtenidos mediante SP `CrudMantencionFiniquitos` con acción `REPORTEXCELVALEVISTA`.
- **Seguridad**: Protegido con contraseña `domCLt3am.W0rk`.
- **Estructura**: Hoja Única (`Hoja1`) con columnas dinámicas devueltas por el SP.

---

## 2. XLSX_ReporteFirmaDigital
Reporte del estado de firmas electrónicas/digitales para procesos de modificación de cargo (CargoMod).

- **API**: Llama a `CallAPIExcel.__ReporteCargoMod`.
- **Estructura**: Hoja Única (`Reporte`).
- **Campos Típicos**: Rut, Nombre Trabajador, Fecha de Firma, Estado (Firmado/Pendiente), Código Documento.

---

## 3. XLSX_ReporteSolicitudes
Reporte de solicitudes de bajas y modificaciones.

- **API**: Llama a `CollectionExcel.__ReporteSolicitud` (que invoca `EmitirSolicitud`).
- **Estructura**: Multi-hoja. Crea una hoja con el prefijo `Bajas ` + el valor de la columna 13 (posiblemente la fecha o motivo).
- **Lógica**: La primera fila del reporte JSON contiene los encabezados.

---

## 4. XLSX_ReporteFiniquitoProcesoMasivo
Reporte de bitácora o resultados de procesos masivos de carga de finiquitos.

- **API**: Llama a `CallAPIExcel.__ReporteFiniquitos`.
- **Propósito**: Validar si los registros cargados masivamente fueron procesados correctamente o presentan errores.
- **Estructura**: Hoja Única (`Reporte`) con columnas variables según la carga.
