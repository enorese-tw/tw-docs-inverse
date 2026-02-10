# Otrohaber.cs (singular)

## Descripción General
Clase DTO que representa un **otro haber** (beneficio adicional) con operaciones CRUD sobre la tabla `FNOTROSHABER`. Similar en estructura a `Descuento.cs`.

## Namespace
`FiniquitosV2.Clases`

## Propiedades

| Propiedad | Tipo | Descripción |
|-----------|------|-------------|
| `Id` | `int` | Identificador del haber |
| `Descripcion` | `string` | Nombre/descripción del haber |
| `Estado` | `int` | Estado (activo/inactivo) |

## Métodos

### `void Obtener(string connectionString)`
```sql
SELECT * FROM FNOTROSHABER WHERE ID = {Id}
```

### `void Guardar(string connectionString)`
```sql
INSERT INTO FNOTROSHABER (DESCRIPCION) VALUES ('{Descripcion}')
```

### `void Eliminar(string connectionString)`
```sql
DELETE FROM FNOTROSHABER WHERE ID = {Id}
```

## Base de Datos
- **Tabla**: `FNOTROSHABER` (base local Finiquitos2018).
- Misma tabla que consulta `OtrosHaberes.cs` (plural).

## Manejo de Errores
- Log con proceso `"TW_Gratificacion.Obtener"` (incorrecto).

## Observaciones
- ⚠️ **SQL Injection** en `Guardar()`.
- ⚠️ Inconsistencia de nombres: la clase se llama `Otrohaber` pero la tabla es `FNOTROSHABER` y la clase de listado es `OtrosHaberes`.
