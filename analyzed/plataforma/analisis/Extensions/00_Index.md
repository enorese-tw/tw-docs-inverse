# Análisis de la Capa Extensions: Reportes XLSX

## 1. Visión General
La capa `Teamwork.Extensions` (específicamente el espacio de nombres `Teamwork.Extensions.Excel`) se encarga de la generación de reportes en formato Excel (.xlsx) utilizando la librería **EPPlus**. 

Esta capa actúa como un puente entre los datos obtenidos del servidor (ya sea vía Repositorios, Servicios o llamadas directas a procedimientos almacenados) y la presentación final en hojas de cálculo.

## 2. Tecnologías y Librerías
*   **EPPlus**: Librería principal para la creación de archivos Excel. Se observa el uso de contextos comerciales y no comerciales.
*   **Newtonsoft.Json**: Utilizada para deserializar las respuestas de las APIs internas cuando los datos vienen en formato JSON.
*   **System.Data**: Manejo de `DataSet` y `DataTable` para reportes que vienen directamente de la base de datos.

## 3. Listado de Reportes Analizados

| Archivo | Propósito | Complejidad |
| :--- | :--- | :--- |
| [XLSX_ReporteAsistencia.md](./XLSX_ReporteAsistencia.md) | Gestión de asistencia, inasistencias y horas extras. | Alta |
| [XLSX_ReporteTransferencias.md](./XLSX_ReporteTransferencias.md) | Formatos de transferencia bancaria (TEF) con límites de seguridad. | Alta |
| [XLSX_ReporteSolicitudContratos.md](./XLSX_ReporteSolicitudContratos.md) | Solicitudes de nuevos contratos con datos detallados de trabajadores. | Media |
| [XLSX_ReporteFiniquitos.md](./XLSX_ReporteFiniquitos.md) | Consolidación y detalle de finiquitos. | Media |
| [XLSX_ReporteValeVista.md](./XLSX_ReporteValeVista.md) | Pagos vía Vale Vista. | Baja |
| [XLSX_ReporteContratos.md](./XLSX_ReporteContratos.md) | Reporte genérico de contratos. | Baja |
| [XLSX_ReporteRenovaciones.md](./XLSX_ReporteRenovaciones.md) | Reporte genérico de renovaciones. | Baja |
| [XLSX_ReporteSolicitudes.md](./XLSX_ReporteSolicitudes.md) | Reporte de solicitudes de bajas/egresos. | Baja |
| [XLSX_ReporteFiniquitoProcesoMasivo.md](./XLSX_ReporteFiniquitoProcesoMasivo.md) | Reporte de procesos masivos de finiquitos. | Baja |
| [XLSX_ReporteFirmaDigital.md](./XLSX_ReporteFirmaDigital.md) | Reporte de cargos y firmas digitales. | Baja |

## 4. Patrones Comunes
1.  **Protección de Hojas**: Los reportes financieros (Transferencias, Vale Vista) utilizan la contraseña `domCLt3am.W0rk` para proteger las celdas.
2.  **Mapeo Dinámico**: Muchos reportes asumen que el origen de datos (SQL) entrega columnas con nombres genéricos como `Column1`, `Column2`, etc., y realizan un mapeo secuencial en el Excel.
3.  **Lógica de Negocio en Reportes**: El reporte de asistencia (`XLSX_ReporteAsistencia.cs`) contiene lógica de cálculo de días trabajados que es crítica para la operación.
