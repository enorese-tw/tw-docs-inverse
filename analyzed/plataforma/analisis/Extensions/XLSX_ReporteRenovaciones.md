# Reporte Renovaciones - Teamwork.Extensions

**Archivo**: `Teamwork.Extensions/Excel/XLSX_ReporteRenovaciones.cs`

Genera un reporte de renovaciones de contrato.

## Método Principal: `__xlsx`

### Parámetros de Entrada

- `string cliente`: Filtro cliente.
- `string fechaInicioFilter`: Inicio rango.
- `string FechaTerminoFilter`: Fin rango.
- `string empresa`: Empresa contexto.

### Lógica

1.  **Autenticación**: `Authenticate.__Authenticate()`.
2.  **Obtención de Datos**:
    *   Llama a `CallAPICargaMasiva.__ReporteRenovaciones`.
    *   Este endpoint probablemente retorna un JSON con las renovaciones pendientes o procesadas.
3.  **Generación Excel**:
    *   Crea hoja "Reporte".
    *   Iteración simple sobre el JSON de respuesta.
    *   Mapeo dinámico `Column1`, `Column2`, etc.

### Salida

- **`byte[]`**: Archivo Excel binario.
