# Causales.cs

## Descripción General
Clase de **acceso a datos** que lista todas las causales de desvinculación desde la base de datos local. Sirve como repositorio estático para recuperar el catálogo de causales del Código del Trabajo chileno.

## Namespace
`FiniquitosV2.Clases`

## Campos Estáticos

| Campo | Tipo | Descripción |
|-------|------|-------------|
| `strSql` | `string` (private static) | Query SQL a ejecutar |
| `ds` | `DataSet` (private static) | Dataset con resultados |

## Métodos

### `static List<Causal> Listar(string connectionString)`
Retorna todas las causales de la tabla `FNCAUSAL`.

- **Parámetro**: `connectionString` — Cadena de conexión a la BD local (Finiquitos2018).
- **Retorno**: `List<Causal>` con todas las causales, o `null` si no hay datos o hay error.

#### Query SQL Ejecutada
```sql
SELECT * FROM FNCAUSAL
```

#### Mapeo de Columnas
| Columna BD | Propiedad Causal |
|------------|------------------|
| `ID` | `id` |
| `ARTICULO` | `articulo` |
| `DESCRIPCION` | `descripcion` |
| `ARTICULODOCUMENTO` | `articuloDocumento` |
| `DESCRIPCIONDOCUMENTO` | `descripcionDocumento` |

## Conexión
- Base de datos local `Finiquitos2018` (no Softland).
- Usa `Interface.ExecuteDataSet()`.

## Manejo de Errores
- Registra errores mediante `Utilidades.LogError()` con proceso `"TelemedicionLib.Usuarios.Listar"` (nombre heredado de otro proyecto).

## Dependencias
- `Causal` — DTO que estructura los datos.
- `Interface` — Ejecución de queries.
- `Utilidades` — Logging de errores.

## Observaciones
- ⚠️ El nombre del proceso en el log de errores (`"TelemedicionLib.Usuarios.Listar"`) es incorrecto; fue copiado de otro proyecto.
- ⚠️ Los miembros estáticos (`strSql`, `ds`) generan riesgo de **condiciones de carrera** en entornos concurrentes (aplicación web).
