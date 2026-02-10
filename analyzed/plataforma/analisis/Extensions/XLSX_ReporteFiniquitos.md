# Análisis de Reporte: XLSX_ReporteFiniquitos.cs

## 1. Propósito
Genera reportes detallados y consolidados de la gestión de finiquitos. El archivo resultante tiene dos enfoques: una visión general (Consolidado) y un detalle por folio (Detalle de Folio).

## 2. Lógica de Negocio y Datos

### **Llamadas a Base de Datos (vía Servicio)**
El componente interactúa con `servicioOperaciones.CrudMantencionFiniquitos` utilizando diferentes acciones:
1.  **Acción `REPORTEXCELFIN`**: Obtiene los datos para la hoja "Consolidado".
2.  **Acción `REPORTEXCELFINDETAILS`**: Obtiene los datos detallados para la hoja "Detalle de Folio".

### **Parámetros de Entrada**
El servicio de base de datos recibe un array de 34 parámetros, de los cuales los más relevantes para este reporte son:
*   `@TIPODOC`: Tipo de documento de finiquito.
*   `@FECHAINICIOFILTER` / `@FECHATERMINOFILTER`: Rango de fechas.
*   `@CLIENTE`: Filtro por cliente (se reemplazan espacios por `+`).
*   `@ESTADO`: Estado del finiquito (ej. Pendiente, Firmado, Pagado).

---

## 3. Estructura del Excel
El archivo Excel contiene dos hojas de cálculo:

### **Hoja 1: Consolidado**
Mapeo dinámico de columnas desde la base de datos. Representa un resumen de los finiquitos procesados.

### **Hoja 2: Detalle de Folio**
Contiene información pormenorizada de cada registro. Aunque el código fuente parece mapear los mismos datos del consolidado accidentalmente o por diseño preliminar (usa `dataMantencion` en lugar de `dataMantencion2` en el bucle de la hoja 2), el propósito declarado es el detalle.

---

## 4. Origen de Datos
Los datos provienen de un `DataSet` retornado por el servicio de operaciones, el cual mapea directamente los resultados de un procedimiento almacenado en el servidor SQL.
