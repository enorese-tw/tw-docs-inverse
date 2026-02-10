# Análisis de Reporte: XLSX_ReporteAsistencia.cs

## 1. Propósito
Este componente genera el reporte de asistencia más detallado del sistema. No solo lista datos, sino que realiza cálculos complejos de días trabajados, licencias, vacaciones y faltas basándose en la configuración de la jornada laboral y las bajas confirmadas.

## 2. Lógica de Negocio y Cálculos

### **Cálculo de Días Trabajados**
El reporte implementa una lógica para normalizar el mes a 30 días (`diasMes = 30`).
*   **Fórmula Base**: `diasTrabajados = diasMes - (diasSinContrato) - (diasFalta + diasLicencia)`.
*   **Ajuste por mes calendario**:
    *   Si el mes tiene 28 días (Febrero), suma 2 días a `diasSinContrato`.
    *   Si el mes tiene 29 días, suma 1 día.
    *   Si el mes tiene 31 días, resta 1 día.
*   **Validación de Topes**: Asegura que la suma de días trabajados y días informados no exceda los 30 días.

### **Determinación de Estados (Visual)**
El reporte aplica colores de fondo y códigos de texto basados en la situación contractual del trabajador para cada día del mes:
*   **SC (Sin Contrato)**: Color `#BCBCBC`. Se aplica a los días anteriores a la `FechaRealInicio`.
*   **BC (Baja Confirmada)**: Color `#595472` con fuente blanca. Se aplica cuando existe una baja confirmada para el trabajador.
*   **BK (Baja Informada KAM)**: Color `#B703FA`.
*   **PCK (Proyección de Baja / Pendiente)**: Color `#917a7a`.
*   **P (Presente)**: Valor por defecto si no hay inasistencias registradas.

### **Manejo de Inasistencias**
El sistema procesa una lista de tipos de ausencia y actualiza contadores:
*   **LM**: Licencia Médica.
*   **V**: Vacaciones.
*   **F**: Falta.
*   **L**: Libres.
*   **PGS/PSG**: Permisos con/sin goce de sueldo.

---

## 3. Estructura del Excel

### **Hojas Generadas**
1.  **[Periodo] (ej. 2023-10)**: La hoja principal con la grilla de asistencia.
    *   **Cabecera 1**: Iniciales del día de la semana (Lu, Ma, Mi, ...). Los fines de semana (Sábado/Domingo) se destacan con color `#BFFFBF`.
    *   **Cabecera 2**: Números de día (1 al 31) y nombres de columnas de datos.
2.  **Tipos Ausencias**: Una leyenda que explica los códigos utilizados (B, F, L, V, etc.) con sus respectivos colores.
3.  **Jornadas Laborales**: Detalle de las jornadas asociadas al área de negocio (Código, Descripción, Horas Semanales, % Pago).

---

## 4. Origen de Datos (Input)
El método `ReporteAsistencia` recibe múltiples listas de modelos:
*   `List<ReporteAsistencia> reporte`: Datos base de asistencia día a día.
*   `List<BajasConfirmadas> bajasConfirmadas`: Información de términos de contrato.
*   `List<HorasExtras> horaExtra`: Datos de HHEE por ficha.
*   `List<Leyenda> leyenda`: Configuración de tipos de ausencia.
*   `List<JornadaLaboral> jornadaLaboral`: Configuración de turnos.

---

## 5. Ejemplo de Formato de Salida
| Area Negocio | Nombres | Rut | Ficha | ... | 1 | 2 | 3 | ... | Dias Trabajados |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| Sucursal X | Juan Perez | 12.345.678-9 | F001 | ... | P | P | L | ... | 30 |

> [!IMPORTANT]
> Este archivo es crítico para el proceso de remuneraciones, ya que los "Días Trabajados" calculados aquí suelen ser el input directo para el cálculo de sueldos.
