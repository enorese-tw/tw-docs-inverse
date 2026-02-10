# Reporte Contratos - Teamwork.Extensions

**Archivo**: `Teamwork.Extensions/Excel/XLSX_ReporteContratos.cs`

Genera un reporte de contratos basado en datos obtenidos desde la API de Carga Masiva.

## Método Principal: `__xlsx`

### Parámetros de Entrada

- `string cliente`: Filtro por cliente.
- `string fechaInicioFilter`: Filtro de fecha inicio.
- `string FechaTerminoFilter`: Filtro de fecha término.
- `string empresa`: Código de empresa.

### Lógica

1.  **Autenticación**:
    *   Llama internamente a `Authenticate.__Authenticate()` para obtener un token.
2.  **Obtención de Datos**:
    *   Llama a `CallAPICargaMasiva.__ReporteContratos` pasando los filtros y el token.
    *   Deserializa la respuesta JSON (`dynamic objects`).
3.  **Generación Excel**:
    *   Crea una hoja llamada "Reporte".
    *   Itera sobre la lista de objetos JSON.
    *   Escribe en las celdas usando un mapeo simple: `objects[i]["Column" + j]`. Esto sugiere que la API devuelve un JSON genérico con propiedades `Column1`, `Column2`, etc.

### Salida

- **`byte[]`**: Archivo Excel binario.
