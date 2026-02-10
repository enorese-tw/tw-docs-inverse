# Reporte Firma Digital - Teamwork.Extensions

**Archivo**: `Teamwork.Extensions/Excel/XLSX_ReporteFirmaDigital.cs`

Genera un reporte relacionado con el estado de firmas digitales de modificaciones de cargo.

## Método Principal: `__xlsx`

### Parámetros de Entrada

- `string excels`: Parámetro opaco (posiblemente filtro o IDs).
- `string codigoCargoMod`: Identificador de la modificación de cargo.

### Lógica

1.  **Obtención de Datos**:
    *   Llama a `Teamwork.Infraestructura.Collections.Remuneraciones.CollectionsReporte.__ReporteCargoMod`.
    *   Retorna una colección dinámica `report`.
2.  **Generación Excel**:
    *   Crea hoja "Reporte".
    *   Volcado estándar de propiedades `Column1`...`ColumnN`.

### Salida

- **`byte[]`**: Archivo Excel binario.
