# Análisis de Reporte: XLSX_ReporteSolicitudContratos.cs

## 1. Propósito
Genera reportes detallados sobre las solicitudes de contratos, incluyendo toda la información necesaria para el alta del trabajador, desde datos personales hasta condiciones contractuales y plazos.

## 2. Estructura de Datos (Mapping)
Este reporte tiene una estructura fija y detallada de 35 columnas (de la A a la AI).

### **Columnas Detalladas (Cabecera)**
| Letra | Nombre Columna | Clave de Dato |
| :--- | :--- | :--- |
| A | Fecha Transacción | `FechaTransaccion` |
| B | Ficha | `Ficha` |
| C | Area Negocio | `Codigo` |
| D | Rut Empresa | `RutEmpresa` |
| E | Rut Trabajador | `Rut` |
| F | Nombre Trabajador | `Nombre` |
| G | Codigo BPO | `CargoBPO` |
| ... | ... | ... |
| L | Fecha de Inicio | `FechaInicio` |
| M | Fecha de Termino | `FechaTermino` |
| P | CC | `CC` (Centro de Costo) |
| V | Horas Trabajadas | `HorasTrab` |
| AE | Fecha Inicio Primer Plazo | `FechaInicioPPlazo` |
| AI | Renovacion Automática | `RenovacionAutomatica` |

---

## 3. Lógica de Negocio
*   **Separación por Hojas**: El reporte genera dinámicamente una nueva pestaña por cada combinación de **Código de Área de Negocio** y **Empresa**. El nombre de la hoja sigue el formato: `Contratos [Codigo] [Empresa]`.
*   **Ajuste Visual**: Utiliza `AutoFitColumns()` para asegurar que todos los datos sean legibles según su longitud.

---

## 4. Origen de Datos (Input)
El componente recibe un `DataSet` directamente. No realiza llamadas a APIs o Base de Datos de forma interna, asumiendo que el llamador (Controller) ya obtuvo la información.

## 5. Ejemplo de Formato
**Hoja: Contratos 123 Teamwork**
| Fecha Transacción | Ficha | Area Negocio | Rut Trabajador | Nombre Trabajador | ... |
| :--- | :--- | :--- | :--- | :--- | :--- |
| 2023-11-01 | F10293 | 123 | 12.345.678-9 | Juan Perez | ... |
