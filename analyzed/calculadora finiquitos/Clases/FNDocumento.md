# FNDocumento.cs

## Descripción General
Clase que representa un **documento asociado a una desvinculación**. Permite operaciones CRUD sobre la tabla `FN_DOCUMENTO` de la base local, incluyendo obtener, guardar y eliminar documentos vinculados a un proceso de finiquito.

## Namespace
`FiniquitosV2.Clases`

## Propiedades

| Propiedad | Tipo | Descripción |
|-----------|------|-------------|
| `ID` | `int` | Identificador del documento |
| `Descripcion` | `string` | Nombre/descripción del documento |
| `IDDESVINCULACION` | `int` | ID del proceso de desvinculación asociado |
| `Estado` | `int` | Estado del documento |

## Métodos

### `void Guardar(string connectionString)`
Inserta un nuevo documento.

```sql
INSERT INTO FN_DOCUMENTO (DESCRIPCION) VALUES ('{Descripcion}')
```

### `void Obtener(string connectionString)`
Obtiene un documento por su `ID`.

```sql
SELECT * FROM FN_DOCUMENTO WHERE ID = {ID}
```

### `void Eliminar(string connectionString)`
Elimina un documento por su `ID`.

```sql
DELETE FROM FN_DOCUMENTO WHERE ID = {ID}
```

## Base de Datos
- **Tabla**: `FN_DOCUMENTO` (base local Finiquitos2018).
- Columnas: `ID`, `DESCRIPCION`.

## Manejo de Errores
- `Obtener()`: Log con proceso `"TW_Gratificacion.Obtener"` (incorrecto).
- `Guardar()` y `Eliminar()`: Sin logging de errores.

## Dependencias
- `Interface` — Ejecución SQL.
- `Utilidades` — Logging.

## Observaciones
- ⚠️ **SQL Injection** en `Guardar()`: `Descripcion` concatenada directamente.
- ⚠️ Nombre de proceso de error incorrecto.
- La clase `FNDocumentos.cs` (plural) lista todos los documentos.
