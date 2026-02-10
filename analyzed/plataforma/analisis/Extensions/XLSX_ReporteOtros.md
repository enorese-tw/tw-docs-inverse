# Análisis de Reportes: Proceso Masivo y Firma Digital

## 1. XLSX_ReporteFiniquitoProcesoMasivo.cs

### **Propósito**
Proporciona un reporte detallado del estado de un proceso masivo de finiquitos. Es útil para auditar cargas de cientos o miles de registros simultáneamente.

### **Lógica e Integración**
*   **Origen**: `CollectionExcel.__ReporteFiniquitos`.
*   **Interacción**: Recibe un string de `excels` y `data` para procesar y consolidar la información.
*   **Formato**: Tabular simple mapeando columnas dinámicas (`Column1`, `Column2`, etc.).

---

## 2. XLSX_ReporteFirmaDigital.cs

### **Propósito**
Genera reportes relacionados con las modificaciones de cargos (`CargoMod`) y el estado de las firmas digitales asociadas a estos documentos.

### **Lógica e Integración**
*   **Origen**: `CollectionsReporte.__ReporteCargoMod`.
*   **Parámetros**:
    *   `excels`: Datos adicionales de procesamiento.
    *   `codigoCargoMod`: Identificador único de la modificación de cargo.
*   **Formato**: Una sola hoja denominada **Reporte** con mapeo dinámico de propiedades JSON.
