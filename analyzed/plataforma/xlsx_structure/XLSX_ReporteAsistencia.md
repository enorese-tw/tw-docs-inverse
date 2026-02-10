# XLSX_ReporteAsistencia

Este reporte es el más complejo del sistema, encargado de consolidar la asistencia mensual de los trabajadores, cruzando datos de contratos, licencias, vacaciones y jornadas labores.

## Propósito
Generar una matriz de asistencia donde cada columna representa un día del mes, permitiendo visualizar el estado de cada trabajador (Presente, Falta, Licencia, etc.) y calcular totales para remuneraciones.

## Llamadas a Servicios / APIs
Este componente utiliza los datos pasados como parámetros desde el controlador o servicio que lo invoca.

| Servicio/Clase | Método/Acción | Parámetros Entrada | Salida | Propósito |
| :--- | :--- | :--- | :--- | :--- |
| N/A | `ReporteAsistencia` | `reporte`, `bajasConfirmadas`, `horaExtra`, `columnas`, `leyenda`, `jornadaLaboral`, `fichaBonos`, `periodo` | `byte[]` | Genera el binario del Excel principal. |

## Estructura de Salida (Hoja principal: Periodo YYYY-MM)

| Columna | Nombre | Descripción |
| :--- | :--- | :--- |
| A | Area Negocio | Area de negocio a la que pertenece el trabajador. |
| B | Nombres | Nombre completo del trabajador. |
| C | Rut | RUN del trabajador. |
| D | Ficha | Código de ficha interna. |
| E | Cargo Mod | Código de modificación de cargo activo. |
| F | Nombre Cargo | Descripción del cargo. |
| G | Fecha Real Inicio | Fecha de inicio de contrato. |
| H | Fecha Real Termino | Fecha de término de contrato o anexo. |
| I | Tope Legal | Tope de días legales. |
| J | Causal | Causal de término (si aplica). |
| K | Estado | Estado actual del contrato/trabajador. |
| L... | 1, 2, 3... | Columnas dinámicas por cada día del mes (1 al 28/30/31). |
| Dynamic | Horas Extras | Columnas dinámicas si el trabajador posee HE. |
| Dynamic | Bonos | Columnas dinámicas si posee bonos asignados. |
| Static | Dias Trabajados | Total de días trabajados calculados (Lógica 30 días). |
| Static | Dias Libres | Total de días marcados como Libres ('L'). |
| Static | Dias Faltas | Total de días marcados como Falta ('F', 'SL', 'PSG', etc.). |
| Static | Dias Licencia | Total de días con Licencia Médica ('LM'). |
| Static | Dias Vacaciones | Total de días en Vacaciones ('V'). |
| Static | Dias Sin Contrato | Días fuera del rango de vigencia de contrato. |

### Códigos de Asistencia
- **P**: Presente (Blanco)
- **L**: Libre (BFFFBF)
- **F**: Falta (Rojo)
- **LM**: Licencia Médica (Cian)
- **V**: Vacaciones (Verde)
- **SC**: Sin Contrato (Gris)
- **BC**: Baja Confirmada (Púrpura Oscuro)
- **BK**: Baja KAM (Violeta)

## Lógica Especial
- **Cálculo de Días Trabajados**: El sistema ajusta el mes a base 30. Los días "Sin Contrato" se ajustan según si el mes tiene 28, 29 o 31 días.
- **Detección de Bajas**: Si un trabajador tiene una `BajaConfirmada` con fecha menor al día evaluado, se marca como `BC` o `PCK` (Pre-check).
- **Hojas adicionales**:
    - **Tipos Ausencias**: Muestra la leyenda de colores y códigos.
    - **Jornadas Laborales**: Detalla las jornadas (Código 200) asociadas al reporte.
