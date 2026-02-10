# ControldeDocumento.cs

## Descripción General
Clase que gestiona el **control de documentos** asociados a un proceso de desvinculación. Permite CRUD sobre la tabla `FN_CONTROLDOCUMENTOS` para registrar qué documentos han sido entregados/verificados.

## Namespace
`FiniquitosV2.Clases`

## Propiedades

| Propiedad | Tipo | Descripción |
|-----------|------|-------------|
| `ID` | `int` | ID del registro |
| `IDDesvinculacion` | `int` | ID del proceso de desvinculación |
| `Descripcion` | `string` | Descripción del documento |
| `Usuario` | `string` | Usuario que registró el documento |
| `Estado` | `string` | Estado del documento |

## Métodos

### `void ObtenerId(string connectionString)`
```sql
SELECT TOP 1 ID FROM FN_CONTROLDOCUMENTOS WHERE IDDesvinculacion = {IDDesvinculacion}
```

### `void Guardar(string connectionString)`
```sql
INSERT INTO FN_CONTROLDOCUMENTOS (IDDesvinculacion, Descripcion, USUARIO)
VALUES ({IDDesvinculacion}, '{Descripcion}', '{Usuario}')
```

### `void Eliminar(string connectionString)` / `void EliminarxIDDesvinculacion(string connectionString)`
Eliminan por `ID` o `IDDesvinculacion`.

## Base de Datos
- **Tabla**: `FN_CONTROLDOCUMENTOS` (base local Finiquitos2018).
- Registra los documentos requeridos para cada proceso de finiquito y su estado de control.

## Manejo de Errores
- Log con proceso `"TW_Gratificacion.Obtener"` (incorrecto).

## Observaciones
- ⚠️ **SQL Injection** en todas las queries.
- ⚠️ Nombre de proceso de error incorrecto.
