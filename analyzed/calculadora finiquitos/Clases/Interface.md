# Interface.cs

## Descripción General
Clase de **infraestructura de acceso a datos** que provee métodos estáticos para ejecutar queries SQL directas y trabajar con DataSets. Incluye soporte para SQL Server y Access (OleDb). También contiene funciones utilitarias de conversión de números a texto.

## Namespace
`FiniquitosV2.Clases`

## Campos Estáticos

| Campo | Tipo | Descripción |
|-------|------|-------------|
| `connection` | `SqlConnection` (static) | Conexión SQL Server reutilizable |
| `command` | `SqlCommand` (static) | Comando SQL reutilizable |

> ⚠️ **RIESGO DE CONCURRENCIA**: Los campos estáticos generan condiciones de carrera en un entorno web concurrente.

## Métodos Principales

### `static DataSet ExecuteDataSet(string connectionString, string strSql)`
Ejecuta una consulta SQL y retorna un `DataSet`.

- Crea nueva `SqlConnection` y `SqlCommand` por cada llamada.
- Usa `SqlDataAdapter.Fill()` para llenar el DataSet.
- **Método más utilizado** en toda la aplicación — prácticamente todas las clases lo usan.

### `static void ExecuteQuery(string connectionString, string strSql)`
Ejecuta una query SQL sin retorno (INSERT, UPDATE, DELETE).

- Crea nueva conexión por cada llamada.
- Usa `SqlCommand.ExecuteNonQuery()`.

### `static DataSet ExecuteDataSetACCESS(string connectionString, string strSql)`
Versión para bases de datos Microsoft Access (OleDb).

- Usa `OleDbConnection` y `OleDbDataAdapter`.
- Indica que alguna parte del sistema originalmente usaba Access.

## Métodos de Conversión de Números a Texto

### `static string enletras(string num)` / `static string toText(double value)`
- **Idénticos** a los métodos de `Convertidor.cs`.
- Convierten montos numéricos a texto en español.
- ⚠️ **Código duplicado** — existe la misma implementación en ambas clases.

## Patrón de Uso
```csharp
// Consulta
DataSet ds = Interface.ExecuteDataSet(connectionString, "SELECT * FROM tabla");
// Ejecución
Interface.ExecuteQuery(connectionString, "INSERT INTO tabla VALUES (...)");
```

## Dependencias
- `System.Data.SqlClient` — SQL Server.
- `System.Data.OleDb` — Microsoft Access.

## Observaciones Críticas
- ⚠️ **CONCURRENCIA**: `connection` y `command` estáticos no se usan en la implementación actual de `ExecuteDataSet`/`ExecuteQuery` (crean nuevas instancias), pero su existencia es confusa.
- ⚠️ **Código Duplicado**: `enletras`/`toText` duplicados con `Convertidor.cs`.
- ⚠️ **SQL Injection**: No tiene protección contra SQL injection — recibe queries armadas en string.
- ⚠️ **Sin parametrización**: No ofrece sobrecargas con parámetros SQL.
- Esta clase es el **punto central de acceso a datos** del sistema — todas las demás clases dependen de ella.
