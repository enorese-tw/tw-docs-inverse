# XLSX_ReporteSolicitudContratos

Reporte detallado que contiene toda la información necesaria para formalizar un nuevo contrato de trabajo solicitado a través de la plataforma.

## Propósito
Exportar los datos de las solicitudes de contratación para su revisión o carga en sistemas de RRHH externos.

## Estructura de Salida (Multi-hoja)
Crea una hoja por cada combinación de Código de área y Empresa (Ej: `Contratos XXXX Empresa YYYY`).

| Columna | Nombre del Campo | Descripción |
| :--- | :--- | :--- |
| A | Fecha Transacción | Fecha en que se emitió la solicitud. |
| B | Ficha | Correlativo de ficha (si aplica). |
| C | Area Negocio | Código del centro de costo / área. |
| D | Rut Empresa | RUT de la entidad contratante. |
| E | Rut Trabajador | RUT del futuro empleado. |
| F | Nombre Trabajador | Nombre completo. |
| G | Codigo BPO | Código de servicios externos. |
| H | Cargo Mod | Código de estructura de cargo. |
| I | Cargo | Nombre del cargo. |
| J | Sucursal | Lugar de trabajo. |
| K | Ejecutivo | KAM o ejecutivo responsable. |
| L | Fecha de Inicio | Fecha de ingreso. |
| M | Fecha de Termino | Fecha de fin de contrato (si es plazo fijo). |
| N | Causal | Artículo legal de contratación. |
| O | Reemplazo | Indica si reemplaza a otro trabajador. |
| P | TipoContrato | Plazo Fijo, Indefinido, etc. |
| Q | CC | Centro de Costo detallado. |
| R | Dirección | Dirección del trabajador. |
| S | Comuna | Comuna de residencia. |
| T | Ciudad | Ciudad. |
| U | Región | Región. |
| V | Horario | Jornada de trabajo (ej: 45h). |
| W | Horas Trabajadas | Cantidad de horas semanales. |
| X | Colación | Tiempo o asignación de colación. |
| Y | Nacionalidad | País de origen. |
| Z | Visa | Tipo de visa (extranjeros). |
| AA | Vencimiento Visa | Fecha de caducidad visa. |
| AB | Cliente Firmante | Nombre de la contraparte que firma por el cliente. |
| AC | División | División organizacional específica. |
| AD | Descripcion de funciones | Resumen de tareas del cargo. |
| AE | Fecha Inicio Primer Plazo | Rango de fechas para renovaciones. |
| ... | ... | ... |
| AI | Renovacion Automática | Flag de renovación automática. |

## Lógica Especial
- Define estáticamente 35 columnas (A-AI).
- No realiza llamadas directas a APIs, procesa un `DataSet` que se le entrega como parámetro.
- Utiliza `AutoFitColumns` para mejorar la legibilidad del archivo.
