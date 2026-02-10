# Licencia.cs

## Descripción General
Clase que consulta si un trabajador tiene **licencias médicas activas** en el sistema Softland. Verifica la existencia de licencias para una ficha y fecha específica.

## Namespace
`FiniquitosV2.Clases`

## Propiedades

| Propiedad | Tipo | Descripción |
|-----------|------|-------------|
| `ficha` | `string` | Número de ficha del trabajador |
| `Fechaconsulta` | `string` | Fecha para la cual se consulta la licencia |
| `tienelicencia` | `bool` | Resultado: `true` si tiene licencia vigente |

## Métodos

### `void Obtener(string connectionString)`
Consulta licencias médicas del trabajador en Softland.

```sql
SELECT COUNT(*) as licencia
FROM softland.sw_lmlicencia
WHERE ficha LIKE '{ficha}'
  AND FecFin >= CAST('{Fechaconsulta}' AS DATETIME)
  AND FecIni <= CAST('{Fechaconsulta}' AS DATETIME)
```

- Si el conteo es > 0, `tienelicencia = true`.
- Si es 0 o no hay datos, `tienelicencia = false`.

## Conexión a Softland
- **Tabla**: `softland.sw_lmlicencia` (módulo de licencias médicas de Softland).
- **Columnas usadas**: `ficha`, `FecFin`, `FecIni`.
- Verifica si la fecha de consulta cae dentro del rango de alguna licencia registrada.

## Manejo de Errores
- Log con `Utilidades.LogError()` proceso `"TW_Gratificacion.Obtener"` (incorrecto).

## Dependencias
- `Interface` — Ejecución SQL.
- `Utilidades` — Logging.

## Uso en el Sistema
Se usa durante el cálculo de finiquito para verificar si el trabajador tiene licencias activas que podrían afectar los montos o el proceso de desvinculación.

## Observaciones
- ⚠️ **SQL Injection**: `ficha` y `Fechaconsulta` concatenados directamente.
- ⚠️ Nombre de proceso de error incorrecto.
