# OtrosHaberes.cs

## Descripción General
Clase de **acceso a datos** que lista los "otros haberes" (beneficios adicionales) asociados a un proceso de desvinculación. Consulta la tabla `FNOTROSHABER` y retorna objetos `Otroshaber`.

## Namespace
`FiniquitosV2.Clases`

## Métodos

### `static List<Otroshaber> Listar(string connectionString)`
Lista todos los otros haberes disponibles.

```sql
SELECT * FROM FNOTROSHABER
```

#### Mapeo de Columnas
| Columna BD | Propiedad |
|------------|-----------|
| `ID` | `Id` |
| `DESCRIPCION` | `Descripcion` |

### `static List<Otroshaber> Listar(string connectionString, int IDdes)`
Lista los otros haberes disponibles (mismo comportamiento que la sobrecarga anterior).

```sql
SELECT * FROM FNOTROSHABER
```

> ⚠️ **Bug**: El parámetro `IDdes` se recibe pero **nunca se usa** en la consulta. Probablemente debería filtrar por ID de desvinculación.

## Base de Datos
- **Tabla**: `FNOTROSHABER` (base local Finiquitos2018).

## Manejo de Errores
- Log con proceso `"TelemedicionLib.Usuarios.Listar"` (incorrecto).

## Dependencias
- `Otroshaber` (singular) — DTO de haber individual.
- `Interface` — Ejecución SQL.
- `Utilidades` — Logging.

## Observaciones
- ⚠️ **Bug**: El parámetro `IDdes` en la segunda sobrecarga de `Listar()` no se utiliza.
- ⚠️ Miembros estáticos causan condiciones de carrera.
- ⚠️ Nombre de proceso de error incorrecto.
