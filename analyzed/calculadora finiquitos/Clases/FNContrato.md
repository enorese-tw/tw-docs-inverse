# FNContrato.cs

## Descripción General
Clase que gestiona los **contratos asociados a una desvinculación**. Permite CRUD sobre la tabla `FN_CONTRATOS` y consultas a Softland para obtener datos de contratos específicos por ficha.

## Namespace
`FiniquitosV2.Clases`

## Propiedades

| Propiedad | Tipo | Descripción |
|-----------|------|-------------|
| `ID` | `int` | ID del contrato en tabla local |
| `IDDesvinculacion` | `int` | ID del proceso de desvinculación |
| `Ficha` | `string` | Ficha del trabajador |
| `FechaInicio` | `string` | Fecha inicio del contrato |
| `FechaFinal` | `string` | Fecha fin del contrato |
| `Dias` | `int` | Días totales del contrato |
| `Causal` | `string` | Causal de desvinculación |

## Métodos

### `void ObtenerId(string connectionString)`
Obtiene el ID de un contrato por ID de desvinculación.

```sql
SELECT TOP 1 ID FROM FN_CONTRATOS WHERE IDDesvinculacion = {IDDesvinculacion}
```

### `void Guardar(string connectionString)`
Inserta un nuevo contrato.

```sql
INSERT INTO FN_CONTRATOS (IDDesvinculacion, Ficha, FechaInicio, FechaFinal, Dias, Causal)
VALUES ({IDDesvinculacion}, '{Ficha}', CAST('{FechaInicio}' AS DATETIME),
        CAST('{FechaFinal}' AS DATETIME), {Dias}, '{Causal}')
```

### `void Eliminar(string connectionString)`
Elimina un contrato por su `ID`.

```sql
DELETE FROM FN_CONTRATOS WHERE ID = {ID}
```

### `void EliminarxIDDesvinculacion(string connectionString)`
Elimina **todos** los contratos de una desvinculación.

```sql
DELETE FROM FN_CONTRATOS WHERE IDDesvinculacion = {IDDesvinculacion}
```

### `DataTable ObtenerContratosxFicha(string connectionString, string ficha)`
Consulta Softland para obtener los contratos de un empleado por su ficha.

```sql
SELECT FechaContratoV as Inicio, FecTermContrato as Termino,
       DATEDIFF(DAY, FechaContratoV, FecTermContrato) as Dias
FROM softland.sw_personal
WHERE ficha = '{ficha}'
ORDER BY FechaContratoV ASC
```

## Base de Datos
- **Tabla local**: `FN_CONTRATOS` (Finiquitos2018).
- **Tabla Softland**: `softland.sw_personal` (para consulta de contratos históricos).

## Manejo de Errores
- Log con `Utilidades.LogError()` proceso `"TW_Gratificacion.Obtener"` (incorrecto).

## Dependencias
- `Interface` — Ejecución SQL.
- `Utilidades` — Logging.

## Observaciones
- ⚠️ **SQL Injection** en todas las queries.
- ⚠️ `ObtenerContratosxFicha()` consulta Softland directamente, a diferencia de los otros métodos que trabajan con la BD local.
- Los datos de contratos se copian de Softland a la tabla local `FN_CONTRATOS` durante el proceso de cálculo del finiquito.
