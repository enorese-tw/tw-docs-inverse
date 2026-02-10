# Reporte Finiquito Proceso Masivo - Teamwork.Extensions

**Archivo**: `Teamwork.Extensions/Excel/XLSX_ReporteFiniquitoProcesoMasivo.cs`

Genera un reporte de finiquitos procesados masivamente.

## Método Principal: `__xlsx`

### Parámetros de Entrada

- `string excels`: Identificador o datos de los reportes a consolidar.
- `string data`: Datos adicionales para el filtro o generación.

### Lógica

1.  **Obtención de Datos**:
    *   Llama a `Teamwork.Infraestructura.Collections.General.CollectionExcel.__ReporteFiniquitos(excels, data)`.
    *   Almacena el resultado en `dynamic report`.
2.  **Generación Excel**:
    *   Crea una hoja "Reporte".
    *   Itera dinámicamente sobre las propiedades del objeto JSON (`Column1`, `Column2`...) y las vuelca fila por fila.

### Salida

- **`byte[]`**: Archivo Excel binario.
