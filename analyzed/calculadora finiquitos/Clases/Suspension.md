# Suspension.cs

## Descripción General
Clase que consulta si un trabajador tiene **suspensiones laborales activas** en Softland. Las suspensiones (como permisos sin goce de sueldo) pueden afectar el cálculo del finiquito.

## Namespace
`FiniquitosV2.Clases`

## Propiedades

| Propiedad | Tipo | Descripción |
|-----------|------|-------------|
| `ficha` | `string` | Número de ficha del trabajador |
| `Fechaconsulta` | `string` | Fecha para consultar suspensiones |
| `tienesuspension` | `bool` | `true` si tiene suspensión vigente |

## Métodos

### `void Obtener(string connectionString)`
Consulta suspensiones activas en Softland.

```sql
SELECT COUNT(*) as conteo
FROM softland.sw_suspension
WHERE ficha LIKE '{ficha}'
  AND FecFin >= CAST('{Fechaconsulta}' AS DATETIME)
  AND FecIni <= CAST('{Fechaconsulta}' AS DATETIME)
```

- Si `conteo > 0`: `tienesuspension = true`.

## Conexión a Softland
- **Tabla**: `softland.sw_suspension` (módulo de suspensiones de Softland).
- Verifica si la fecha de consulta cae dentro del rango de alguna suspensión.

## Manejo de Errores
- Log con proceso `"TW_Gratificacion.Obtener"` (incorrecto).

## Observaciones
- ⚠️ **SQL Injection**: Parámetros concatenados directamente.
- Estructura prácticamente idéntica a `Licencia.cs` pero para suspensiones en vez de licencias médicas.
