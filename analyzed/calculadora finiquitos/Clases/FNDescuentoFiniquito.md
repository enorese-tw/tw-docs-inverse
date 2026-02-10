# FNDescuentoFiniquito.cs

## Descripción General
Clase que gestiona los **descuentos aplicados a un finiquito**. Permite CRUD sobre la tabla `FN_DESCUENTOFINIQUITO` que almacena los descuentos específicos de cada proceso de desvinculación.

## Namespace
`FiniquitosV2.Clases`

## Propiedades

| Propiedad | Tipo | Descripción |
|-----------|------|-------------|
| `ID` | `int` | ID del registro |
| `IDDesvinculacion` | `int` | ID del proceso de desvinculación |
| `Descripcion` | `string` | Descripción del descuento |
| `Monto` | `int` | Monto del descuento en pesos |

## Métodos

### `void ObtenerId(string connectionString)`
Obtiene el ID por desvinculación.

```sql
SELECT TOP 1 ID FROM FN_DESCUENTOFINIQUITO WHERE IDDesvinculacion = {IDDesvinculacion}
```

### `void Guardar(string connectionString)`
Inserta un descuento de finiquito.

```sql
INSERT INTO FN_DESCUENTOFINIQUITO (IDDesvinculacion, Descripcion, Monto)
VALUES ({IDDesvinculacion}, '{Descripcion}', {Monto})
```

### `void Eliminar(string connectionString)`
Elimina por `ID`.

```sql
DELETE FROM FN_DESCUENTOFINIQUITO WHERE ID = {ID}
```

### `void EliminarxIDDesvinculacion(string connectionString)`
Elimina todos los descuentos de una desvinculación.

```sql
DELETE FROM FN_DESCUENTOFINIQUITO WHERE IDDesvinculacion = {IDDesvinculacion}
```

## Base de Datos
- **Tabla**: `FN_DESCUENTOFINIQUITO` (base local Finiquitos2018).
- Almacena descuentos como préstamos, anticipos u otros conceptos que se restan del total de finiquito.

## Manejo de Errores
- Log con `Utilidades.LogError()` proceso `"TW_Gratificacion.Obtener"` (incorrecto).

## Dependencias
- `Interface` — Ejecución SQL.
- `Utilidades` — Logging.

## Observaciones
- ⚠️ **SQL Injection** en todas las queries.
- ⚠️ Nombre de proceso de error incorrecto.
