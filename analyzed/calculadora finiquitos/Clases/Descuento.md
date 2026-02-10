# Descuento.cs

## Descripción General
Clase que representa un **tipo de descuento** aplicable en el proceso de finiquito. Permite operaciones CRUD sobre la tabla `FNDESCUENTO` de la base de datos local.

## Namespace
`FiniquitosV2.Clases`

## Propiedades

| Propiedad | Tipo | Descripción |
|-----------|------|-------------|
| `Id` | `int` | Identificador del descuento |
| `Descripcion` | `string` | Nombre/descripción del descuento |
| `Estado` | `int` | Estado del descuento (activo/inactivo) |

## Métodos

### `void Obtener(string connectionString)`
Obtiene un descuento por su `Id`.

```sql
SELECT * FROM FNDESCUENTO WHERE ID = {Id}
```

### `void Guardar(string connectionString)`
Inserta un nuevo descuento. Usa `Interface.ExecuteQuery()`.

```sql
INSERT INTO FNDESCUENTO (DESCRIPCION) VALUES ('{Descripcion}')
```

### `void Eliminar(string connectionString)`
Elimina un descuento por su `Id`.

```sql
DELETE FROM FNDESCUENTO WHERE ID = {Id}
```

## Base de Datos
- **Tabla**: `FNDESCUENTO` (base local Finiquitos2018).
- No tiene relación directa con Softland.

## Manejo de Errores
- `Obtener()`: Registra error con `Utilidades.LogError()` proceso `"TW_Gratificacion.Obtener"` (nombre incorrecto heredado).
- `Guardar()` y `Eliminar()`: No registran errores (catch vacío / sin catch).

## Dependencias
- `Interface` — Ejecución SQL.
- `Utilidades` — Logging.

## Observaciones
- ⚠️ **SQL Injection**: `Guardar()` concatena `Descripcion` directamente en la query.
- ⚠️ El nombre del proceso de log `"TW_Gratificacion.Obtener"` es incorrecto.
- La clase `Descuentos.cs` (plural) es la que lista todos los descuentos.
