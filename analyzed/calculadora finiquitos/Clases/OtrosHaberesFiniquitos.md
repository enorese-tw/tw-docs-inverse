# OtrosHaberesFiniquitos.cs

## Descripción General
Clase que gestiona los **otros haberes específicos de un finiquito**. Permite CRUD sobre la tabla `FN_OTROSHABERES` que almacena los haberes adicionales asignados a cada proceso de desvinculación.

## Namespace
`FiniquitosV2.Clases`

## Propiedades

| Propiedad | Tipo | Descripción |
|-----------|------|-------------|
| `ID` | `int` | ID del registro |
| `IDDesvinculacion` | `int` | ID de desvinculación |
| `Descripcion` | `string` | Descripción del haber |
| `Monto` | `int` | Monto en pesos |

## Métodos

### `void ObtenerId(string connectionString)`
```sql
SELECT TOP 1 ID FROM FN_OTROSHABERES WHERE IDDesvinculacion = {IDDesvinculacion}
```

### `void Guardar(string connectionString)`
```sql
INSERT INTO FN_OTROSHABERES (IDDesvinculacion, Descripcion, Monto)
VALUES ({IDDesvinculacion}, '{Descripcion}', {Monto})
```

### `void Eliminar(string connectionString)` / `void EliminarxIDDesvinculacion(string connectionString)`
Eliminan por `ID` o por `IDDesvinculacion`.

## Base de Datos
- **Tabla**: `FN_OTROSHABERES` (base local).
- Diferente de `FNOTROSHABER` (catálogo de tipos). Esta tabla almacena los haberes **efectivamente aplicados** a cada finiquito con sus montos.

## Manejo de Errores
- Log con proceso `"TW_Gratificacion.Obtener"` (incorrecto).

## Observaciones
- ⚠️ **SQL Injection** en todas las queries.
- Relación: `FNOTROSHABER` (catálogo) → `FN_OTROSHABERES` (instancias aplicadas a un finiquito).
