# Vacaciones.cs

## Descripción General
Clase que consulta las **vacaciones tomadas** por un trabajador en Softland. Obtiene el total de días de vacaciones solicitadas hasta la fecha de desvinculación.

## Namespace
`FiniquitosV2.Clases`

## Propiedades

| Propiedad | Tipo | Descripción |
|-----------|------|-------------|
| `ficha` | `string` | Número de ficha del trabajador |
| `fechaDesvinculacion` | `string` | Fecha de desvinculación del trabajador |
| `DiasVacaciones` | `int` | Total de días de vacaciones tomadas |

## Métodos

### `int ObtenerVacaciones(string connectionString)`
Obtiene la cantidad de días de vacaciones tomadas.

```sql
SELECT SUM(cantDias) as cantDias
FROM softland.sw_vacsolic
WHERE ficha LIKE '{ficha}'
  AND SolicEstado LIKE 'A'
  AND FechaDesdeSol <= CAST('{fechaDesvinculacion}' AS DATETIME)
```

- **Retorno**: Suma de días de vacaciones, o `0` si no hay vacaciones registradas.
- Filtra solo solicitudes con estado `'A'` (aprobadas).
- Filtra solicitudes cuya fecha de inicio sea anterior o igual a la fecha de desvinculación.

## Conexión a Softland
- **Tabla**: `softland.sw_vacsolic` (módulo de solicitudes de vacaciones de Softland).
- **Columnas usadas**: `cantDias`, `ficha`, `SolicEstado`, `FechaDesdeSol`.

## Manejo de Errores
- Log con `Utilidades.LogError()` proceso `"TW_Vacaciones.Obtener"`.
- Retorna `-1` en caso de excepción.

## Dependencias
- `Interface` — Ejecución SQL.
- `Utilidades` — Logging.

## Uso en el Sistema
- El resultado se usa para calcular el **saldo de días de vacaciones** pendientes en el finiquito.
- Fórmula general: `DiasHábilesPendientes = DiasProgresivos - DiasVacacionesTomadas`.

## Observaciones
- ⚠️ **SQL Injection**: `ficha` y `fechaDesvinculacion` concatenados directamente.
- El método retorna `-1` como indicador de error, a diferencia de otros métodos que retornan `0`.
