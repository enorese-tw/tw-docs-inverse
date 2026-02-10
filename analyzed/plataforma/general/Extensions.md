# Extensions (Teamwork.Extensions)

## Objetivo
Esta capa actúa como una librería de utilidades y extensiones para el proyecto, especializándose principalmente en la **generación de reportes en formato Excel**. Aísla la complejidad de la creación de archivos `.xlsx` del resto de la lógica de negocio.

## Estructura
Actualmente, la capa parece estar compuesta casi exclusivamente por el módulo de Excel:

- **`Excel/`**: Directorio principal que contiene clases estáticas para la generación de distintos tipos de reportes.
    - Ejemplo: `XLSX_ReporteAsistencia.cs`, `XLSX_ReporteContratos.cs`, `XLSX_ReporteFiniquitos.cs`.

## Puntos Importantes
- **Librería Utilizada**: Utiliza `OfficeOpenXml` (EPPlus) para la manipulación de archivos Excel.
- **Lógica de Presentación en Reportes**: Las clases no solo vuelcan datos, sino que contienen lógica de presentación específica para Excel:
    - **Estilos Dinámicos**: Aplica colores de fondo y fuentes según el tipo de dato (ej. tipos de ausencia en asistencia, días feriados).
    - **Cálculos en Generación**: Realiza cálculos "al vuelo" durante la generación del reporte, como el conteo de días trabajados, licencias, vacaciones y manejo de topes de días mes (30 días).
    - **Manejo de Estructura**: Define manualmente encabezados, anchos de columna y celdas combinadas.
- **Independencia**: Las clases reciben estructuras de datos (Listas de Objetos) ya procesadas y se encargan únicamente de la representación visual en el archivo Excel.

## Dependencias
- Depende de `Teamwork.Model` para conocer la estructura de los datos que debe reportar.
- Depende de `EPPlus` (`OfficeOpenXml`) para la generación de archivos.
