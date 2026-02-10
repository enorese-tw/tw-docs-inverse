# FNTotalHaber.cs

## Descripción General
Clase que gestiona los **haberes/descuentos del finiquito**. Registra conceptos como "haber" o "descuento" con su descripción y monto en la tabla `FN_TOTALHABERFINIQUITO`.

## Namespace
`FiniquitosV2.Clases`

## Propiedades

| Propiedad | Tipo | Descripción |
|-----------|------|-------------|
| `ID` | `int` | ID del registro |
| `IDDesvinculacion` | `int` | ID del proceso de desvinculación |
| `Tipo` | `string` | Tipo: `"HABER"` o `"DESCUENTO"` |
| `Descripcion` | `string` | Descripción del concepto |
| `Monto` | `int` | Monto en pesos |

## Métodos

### `void ObtenerId(string connectionString)`
```sql
SELECT TOP 1 ID FROM FN_TOTALHABERFINIQUITO WHERE IDDesvinculacion = {IDDesvinculacion}
```

### `void Guardar(string connectionString)`
```sql
INSERT INTO FN_TOTALHABERFINIQUITO (IDDesvinculacion, TIPO, Descripcion, Monto)
VALUES ({IDDesvinculacion}, '{Tipo}', '{Descripcion}', {Monto})
```

### `void Eliminar(string connectionString)` / `void EliminarxIDDesvinculacion(string connectionString)`
Eliminan por `ID` o por `IDDesvinculacion`.

## Base de Datos
- **Tabla**: `FN_TOTALHABERFINIQUITO` (base local).
- Almacena tanto haberes como descuentos usando el campo `Tipo` como discriminador.

## Manejo de Errores
- Log con proceso `"TW_Gratificacion.Obtener"` (incorrecto).

## Dependencias
- `Interface`, `Utilidades`.

## Observaciones
- ⚠️ **SQL Injection** en todas las queries.
- La tabla unifica haberes y descuentos en una sola estructura diferenciada por `Tipo`.
