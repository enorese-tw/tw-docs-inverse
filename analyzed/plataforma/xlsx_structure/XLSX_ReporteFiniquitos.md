# XLSX_ReporteFiniquitos

Este reporte genera un documento con dos hojas: un consolidado general y un detalle por cada folio de finiquito.

## Propósito
Proporcionar una visión financiera y operativa de los finiquitos procesados, listos para pago o en revisión.

## Llamadas a Servicios / APIs

| Servicio/Clase | Método | Acción (`@ACCION`) | Parámetros Clave | Propósito |
| :--- | :--- | :--- | :--- | :--- |
| `servicioOperaciones` | `CrudMantencionFiniquitos` | `REPORTEXCELFIN` | `usuarioCreador`, `tipoDoc`, `cliente`, `estado` | Datos para la hoja "Consolidado". |
| `servicioOperaciones` | `CrudMantencionFiniquitos` | `REPORTEXCELFINDETAILS` | `usuarioCreador`, `tipoDoc`, `cliente`, `estado` | Datos para la hoja "Detalle de Folio". |

## Estructura de Salida

### Hoja 1: Consolidado
Contiene el resumen de los finiquitos. Las columnas son devueltas por el SP bajo el alias `Column1`, `Column2`, etc.

| Columna | Contenido Típico |
| :--- | :--- |
| Column1...N | Datos de cabecera: Folio, Rut, Nombre, Empresa, Total Finiquito, Estado. |

### Hoja 2: Detalle de Folio
Contiene el desglose de haberes y descuentos por cada finiquito.

| Columna | Contenido Típico |
| :--- | :--- |
| Column1...N | Desglose: Concepto, Monto, Tipo (Haber/Descuento), Observaciones. |

## Lógica Especial
- El sistema utiliza un array de 34 parámetros para comunicarse con el SP `CrudMantencionFiniquitos`.
- Los espacios en el nombre del cliente se reemplazan por '+' antes de la consulta.
- Se utiliza `LicenseContext.NonCommercial`.
