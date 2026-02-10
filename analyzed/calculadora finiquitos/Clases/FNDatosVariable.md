# FNDatosVariable.cs

## Descripción General
Clase que gestiona los **datos variables** asociados a un proceso de desvinculación. Los datos variables representan conceptos de remuneración variable (bonos, comisiones, etc.) con montos y períodos específicos.

## Namespace
`FiniquitosV2.Clases`

## Propiedades

| Propiedad | Tipo | Descripción |
|-----------|------|-------------|
| `ID` | `int` | ID del registro |
| `IDDesvinculacion` | `int` | ID del proceso de desvinculación |
| `VarDescripcion` | `string` | Descripción del concepto variable |
| `Monto` | `int` | Monto en pesos |
| `Mesano` | `string` | Mes/año del dato variable |
| `Fecha` | `string` | Fecha asociada |

## Métodos

### `void ObtenerId(string connectionString)`
Obtiene el ID por desvinculación.

```sql
SELECT TOP 1 ID FROM FN_DATOSVARIABLES WHERE IDDesvinculacion = {IDDesvinculacion}
```

### `void Guardar(string connectionString)`
Inserta un dato variable.

```sql
INSERT INTO FN_DATOSVARIABLES (IDDesvinculacion, VarDescripcion, Monto, MESANO)
VALUES ({IDDesvinculacion}, '{VarDescripcion}', {Monto}, '{Mesano}')
```

### `void Eliminar(string connectionString)`
Elimina por `ID`.

```sql
DELETE FROM FN_DATOSVARIABLES WHERE ID = {ID}
```

### `void EliminarxIDDesvinculacion(string connectionString)`
Elimina todos los datos variables de una desvinculación.

```sql
DELETE FROM FN_DATOSVARIABLES WHERE IDDesvinculacion = {IDDesvinculacion}
```

## Base de Datos
- **Tabla**: `FN_DATOSVARIABLES` (base local Finiquitos2018).
- Almacena la información variable de remuneración que se incluye en el cálculo del finiquito.

## Manejo de Errores
- Log con `Utilidades.LogError()` proceso `"TW_Gratificacion.Obtener"` (incorrecto).

## Dependencias
- `Interface` — Ejecución SQL.
- `Utilidades` — Logging.

## Observaciones
- ⚠️ **SQL Injection** en todas las queries.
- ⚠️ Nombre de proceso de error incorrecto.
