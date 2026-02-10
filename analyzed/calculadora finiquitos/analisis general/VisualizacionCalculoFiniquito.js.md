# Análisis de VisualizacionCalculoFiniquito.js

## Ubicación
`FiniquitosV2/Scripts/Finiquitos/ajax/VisualizacionCalculoFiniquito.js`

## Descripción
Controla la vista de detalle de un cálculo de finiquito específico. Es una de las pantallas con mayor densidad de datos.

## Funciones Principales
- **Carga Paralela**: Al inicio (`$(document).ready`), dispara ~14 llamadas AJAX secuenciales (encadenadas en el mismo hilo de JS pero asíncronas en red) para construir la vista:
    - `ajaxVisualizarDatosTrabajador`
    - `ajaxVisualizarDocumentos`
    - `ajaxVisualizaContratos`
    - `ajaxVisualizarPeriodosRemuneraciones` (Cálculo de promedios para indemnizaciones)
    - `ajaxVisualizarTotalesFiniquito`
- `genPDF`: Generación de PDF en el cliente usando `html2canvas` y `jsPDF`.
- `ajaxConfirmarRetiroFiniquito`: Finaliza el proceso, permitiendo editar el monto final (feature "Finiquito Part Time" u ajuste manual).

## Llamadas al Servidor (API/Backend)
- `VisualizacionCalculoFiniquito.aspx/GetObtenerPagosPorFiltroPagosService`
- `VisualizacionCalculoFiniquito.aspx/GetVisualizarContratosCalculoService`
- `VisualizacionCalculoFiniquito.aspx/GetVisualizarPeriodosCalculoService`: Retorna la matriz de remuneraciones (últimos x meses).
- `VisualizacionCalculoFiniquito.aspx/SetConfirmarRetiroFiniquitoService`

## Lógica de Negocio
- **Cálculo de Promedios en Cliente**: La función `ajaxVisualizarPromedioCalculo` calcula el promedio de remuneraciones en JavaScript (`sumaRemuneraciones / dias`). Esto es **CRÍTICO**, ya que lógica financiera debería estar en el backend.
- **Edición de Montos**: Permite al usuario sobreescribir el monto total del finiquito al momento de confirmar el pago (Cheque/Transferencia).
- **Logica de Presentación Dinámica**: Tablas de periodos se construyen dinámicamente según la cantidad de meses (1 a 6) devueltos por la BD.

## Observaciones
- **Riesgo**: El cálculo de promedios en JS es manipulable.
- **Rendimiento**: La carga secuencial de 14 llamadas AJAX puede ser lenta y propensa a fallos parciales (si falla una, la UI puede quedar inconsistente).
