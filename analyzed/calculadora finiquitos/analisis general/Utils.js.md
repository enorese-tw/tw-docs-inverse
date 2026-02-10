# Análisis de Utils.js

## Ubicación
`FiniquitosV2/Scripts/Finiquitos/ajax/Utils.js`

## Descripción
Biblioteca de funciones utilitarias de propósito general para formateo y manejo de datos en el frontend.

## Funciones Principales
- `formatRutTypeSoftland(rut)`: Formatea RUTs agregando puntos y guión. Lógica específica para manejo de ceros iniciales.
- `extensionFile(extension)`: Devuelve una descripción legible del tipo de archivo según su extensión.
- `extensionFileIcon(extension)`: Devuelve la clase CSS de un icono para el tipo de archivo.
- `extensionFileType(extension)`: Devuelve la clase CSS para el color de botón según el tipo de archivo.
- `convertFechaPalabra(fecha)`: Convierte una fecha `YYYY-MM-DD` a formato texto "DD de Mes de YYYY".
- `standarFileName(fileName)`: Limpia nombres de archivo (reemplaza comillas simples por espacios).
- `diffDays(fecha)`: Calcula la diferencia de días entre una fecha dada y la fecha actual.
- `maskRutTypeSoftland(rut)`: Aplica una máscara parcial al RUT mientras se escribe.
- `daysList()`, `MonthList()`, `YearList()`: Generan opciones HTML (`<option>`) para selectores de fecha.

## Llamadas al Servidor
- **Ninguna**. Es lógica puramente algorítmica y de presentación.

## Lógica de Negocio
- Contiene reglas de negocio implícitas sobre cómo presentar los datos (e.g., mapeo de extensiones a descripciones, formato de RUT).
