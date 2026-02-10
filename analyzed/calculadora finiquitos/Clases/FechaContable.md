# FechaContable.cs

## Descripción General
Clase que gestiona la **fecha contable** del trabajador para determinar el período de cálculo del finiquito. Contiene lógica compleja para determinar la fecha correcta basándose en tipos de desvinculación, licencias y suspensiones.

## Namespace
`FiniquitosV2.Clases`

## Propiedades

| Propiedad | Tipo | Descripción |
|-----------|------|-------------|
| `ficha` | `string` | Ficha del trabajador |
| `fechaContable` | `string` | Fecha contable calculada |
| `tieneLicencia` | `bool` | Si tiene licencia activa |
| `tieneSuspension` | `bool` | Si tiene suspensión activa |

## Métodos

### `void Obtener(string connectionString, string ficha, string tipo)`
Calcula la fecha contable según el tipo de desvinculación.

**Parámetros**:
- `connectionString` — Conexión a Softland.
- `ficha` — Ficha del trabajador.
- `tipo` — Tipo de desvinculación (`"1"`, `"2"`, `"3"`, `"4"`).

**Lógica**:
1. Si `tipo = "4"` (solicitud de baja): retorna sin acción.
2. Para otros tipos, consulta la fecha del último contrato desde Softland:

```sql
SELECT TOP 1 FecTermContrato as fechaContable
FROM softland.sw_personal
WHERE ficha = '{ficha}'
ORDER BY FecTermContrato DESC
```

3. Verifica si el trabajador tiene **licencia médica** activa (vía `Licencia.cs`).
4. Verifica si tiene **suspensión** activa (vía `Suspension.cs`).
5. Establece las banderas `tieneLicencia` y `tieneSuspension`.

### Lógica de Fecha Contable
- Para tipo `"1"` (TWEST): Usa `connectionStringTWEST`.
- Para tipo `"2"` (TEAMRRHH/OUT): Usa `connectionStringTEAMRRHH`.
- Para tipo `"3"` (TEAMWORK/Consultora): Usa `connectionStringTEAMWORK`.

## Conexiones a Softland
- **Tabla**: `softland.sw_personal` (para fecha de término de contrato).
- **Indirectas**: `softland.sw_lmlicencia` (vía `Licencia.cs`) y `softland.sw_suspension` (vía `Suspension.cs`).
- Usa diferentes `connectionString` según la empresa (settings de la aplicación).

## Manejo de Errores
- Log con `Utilidades.LogError()` proceso `"TW_Gratificacion.Obtener"` (incorrecto).

## Dependencias
- `Licencia` — Verificación de licencias médicas.
- `Suspension` — Verificación de suspensiones.
- `Interface` — Ejecución SQL.
- `Utilidades` — Logging.
- `Properties.Settings.Default` — Cadenas de conexión por empresa.

## Observaciones
- ⚠️ **SQL Injection**: `ficha` concatenada directamente.
- ⚠️ Gran cantidad de código comentado (versiones anteriores de lógica de fecha contable).
- ⚠️ Nombre de proceso de error incorrecto.
- La lógica de fecha contable es crítica para el cálculo correcto de vacaciones, indemnización y mes de aviso.
