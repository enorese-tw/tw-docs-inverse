# FNDiasVacacion.cs

## Descripción General
Clase que gestiona el **cálculo y almacenamiento de días de vacaciones** en el proceso de finiquito. Permite CRUD sobre la tabla `FN_DIASVACACION` que registra el desglose completo de vacaciones del trabajador.

## Namespace
`FiniquitosV2.Clases`

## Propiedades

| Propiedad | Tipo | Descripción |
|-----------|------|-------------|
| `ID` | `int` | ID del registro |
| `IDDesvinculacion` | `int` | ID del proceso de desvinculación |
| `TotalDiasFiniquito` | `int` | Total de días para el finiquito |
| `DiasHabiles` | `string` | Días hábiles calculados |
| `DiasVacTomadas` | `int` | Días de vacaciones ya tomadas |
| `Saldodiashabiles` | `string` | Saldo de días hábiles |

## Métodos

### `void ObtenerId(string connectionString)`
Obtiene el ID por desvinculación.

```sql
SELECT TOP 1 ID FROM FN_DIASVACACION WHERE IDDesvinculacion = {IDDesvinculacion}
```

### `void Guardar(string connectionString)`
Inserta el registro de días de vacación.

```sql
INSERT INTO FN_DIASVACACION (IDDesvinculacion, TotalDiasFiniquito, DiasHabiles, DiasVacTomadas, Saldodiashabiles)
VALUES ({IDDesvinculacion}, {TotalDiasFiniquito}, '{DiasHabiles}', {DiasVacTomadas}, '{Saldodiashabiles}')
```

### `void Eliminar(string connectionString)` / `void EliminarxIDDesvinculacion(string connectionString)`
Eliminan registros por `ID` o por `IDDesvinculacion`.

## Base de Datos
- **Tabla**: `FN_DIASVACACION` (base local Finiquitos2018).
- Almacena el cálculo detallado de vacaciones para cada finiquito.

## Cálculo de Vacaciones (contexto de uso)
1. Se obtienen los días totales trabajados desde `Contrato.cs`.
2. Se calcula: `MesesParaVacaciones = Dias / 30`.
3. Se calcula: `TotalDiaHabiles = MesesParaVacaciones * 1.25`.
4. Se consultan las vacaciones tomadas desde `Vacaciones.cs` (Softland `sw_vacsolic`).
5. El saldo se almacena en esta tabla.

## Manejo de Errores
- Log con `Utilidades.LogError()` proceso `"TW_Gratificacion.Obtener"` (incorrecto).

## Dependencias
- `Interface` — Ejecución SQL.
- `Utilidades` — Logging.

## Observaciones
- ⚠️ **SQL Injection** en todas las queries.
- ⚠️ `DiasHabiles` y `Saldodiashabiles` son `string` en vez de numérico, lo que puede causar problemas de formato.
