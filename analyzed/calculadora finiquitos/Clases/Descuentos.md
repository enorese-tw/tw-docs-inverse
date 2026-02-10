# Descuentos.cs

## Descripción General
Clase de **acceso a datos** que lista todos los tipos de descuento disponibles desde la tabla `FNDESCUENTO`. Funciona como repositorio estático complementario a `Descuento.cs`.

## Namespace
`FiniquitosV2.Clases`

## Campos Estáticos

| Campo | Tipo | Descripción |
|-------|------|-------------|
| `strSql` | `string` (private static) | Query SQL |
| `ds` | `DataSet` (private static) | Dataset de resultados |

## Métodos

### `static List<Descuento> Listar(string connectionString)`
Retorna todos los descuentos de la tabla.

```sql
SELECT * FROM FNDESCUENTO
```

#### Mapeo de Columnas
| Columna BD | Propiedad |
|------------|-----------|
| `ID` | `Id` |
| `DESCRIPCION` | `Descripcion` |

- **Retorno**: `List<Descuento>` o `null` si no hay datos/error.

## Manejo de Errores
- Registra con `Utilidades.LogError()` proceso `"TelemedicionLib.Usuarios.Listar"` (nombre heredado incorrecto).

## Dependencias
- `Descuento` — DTO de descuento.
- `Interface` — Ejecución SQL.
- `Utilidades` — Logging.

## Observaciones
- ⚠️ Miembros estáticos (`strSql`, `ds`) causan riesgo de **condiciones de carrera** en web.
- ⚠️ Nombre de proceso de error incorrecto.
