# Usuarios.cs

## Descripción General
Clase de **acceso a datos** para la gestión de usuarios del sistema. Contiene una cadena de conexión hardcodeada y métodos para listar usuarios (KAM) y validar credenciales.

## Namespace
`FiniquitosV2.Clases`

## Campos

| Campo | Tipo | Descripción |
|-------|------|-------------|
| `strSql` | `string` (private static) | Query SQL |
| `ds` | `DataSet` (private static) | Dataset de resultados |

## Métodos

### `string CadenaConexion()`
Retorna una cadena de conexión hardcodeada.

```
Data Source=192.168.0.10;Initial Catalog=Finiquitos2018QA;User ID=ADMINTW;Password=Satw.261119
```

> ⚠️ **SEGURIDAD CRÍTICA**: Credenciales de base de datos expuestas en código fuente.

### `DataTable Listarkam()`
Lista todos los usuarios KAM desde la tabla `FNUSUARIOS`.

```sql
SELECT * FROM dbo.FNUSUARIOS
```

- **Retorno**: `DataTable` con todos los usuarios, o `null` en caso de error.
- Usa la cadena de conexión del método `CadenaConexion()`.

## Base de Datos
- **Servidor**: `192.168.0.10`
- **Base de datos**: `Finiquitos2018QA`
- **Tabla**: `FNUSUARIOS`

## Manejo de Errores
- `Listarkam()` captura excepciones pero solo retorna `null` sin logging.

## Dependencias
- `Interface` — Ejecución SQL.

## Observaciones
- ⚠️ **SEGURIDAD CRÍTICA**: Cadena de conexión con usuario y contraseña hardcodeadas en código.
- ⚠️ Miembros estáticos causan riesgo de condiciones de carrera.
- ⚠️ La cadena de conexión apunta a un entorno **QA** (`Finiquitos2018QA`), no producción.
- Este método se usa independientemente de `Properties.Settings.Default.connectionString`.
