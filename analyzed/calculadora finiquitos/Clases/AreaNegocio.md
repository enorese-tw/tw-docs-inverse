# AreaNegocio.cs

## Descripción General
Clase que representa el **Área de Negocio** de un trabajador en el sistema Softland. Permite obtener el código de área de negocio asociado a una ficha de empleado consultando directamente la base de datos Softland.

## Namespace
`FiniquitosV2.Clases`

## Propiedades

| Propiedad | Tipo | Descripción |
|-----------|------|-------------|
| `ficha` | `string` | Número de ficha del trabajador en Softland |
| `codanrg` | `string` | Código del área de negocio asignada |

## Métodos

### `Obtener(string connectionString)`
Consulta el área de negocio del trabajador en Softland.

- **Parámetro**: `connectionString` — Cadena de conexión a la base de datos Softland.
- **Retorno**: `void` — Establece la propiedad `codanrg` con el resultado.
- **Tabla consultada**: `softland.sw_areanegper`
- **Columna leída**: `codarn`
- **Filtro**: `ficha = '{ficha}'`

#### Query SQL Ejecutada
```sql
SELECT codarn FROM softland.sw_areanegper WHERE ficha = '{ficha}'
```

## Conexión a Softland
- Utiliza `Interface.ExecuteDataSet()` para ejecutar la consulta.
- La conexión se pasa como parámetro, pero en la práctica se usa con cadenas hacia bases Softland (TWEST, TEAMRRHH, TEAMWORK).
- Se usa desde `Contrato.cs` con connection strings hardcodeados como:
  ```
  Data Source=conectorsoftland.team-work.cl\SQL2017;Initial Catalog={BD};User ID=Sa;Password=Softland070
  ```

## Manejo de Errores
- Registra errores mediante `Utilidades.LogError()` con proceso `"TW_AreaNgocio.Obtener"`.
- En caso de error, `codanrg` se asigna con el mensaje de excepción.

## Dependencias
- `Interface` — Para ejecución de consultas SQL.
- `Utilidades` — Para logging de errores.

## Observaciones
- ⚠️ **SQL Injection**: La consulta concatena directamente el valor de `ficha` sin parametrización.
- La clase se usa principalmente desde `Contrato.ListarFiniquitados()`, `Contrato.SolicitudDeBaja()`, `Contrato.ListarFiniquitadosOUT()` y `Contrato.ListarFiniquitadosCONSULTORA()`.
