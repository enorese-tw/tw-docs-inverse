# Cargo.cs

## Descripción General
Clase que representa el **Cargo** (puesto de trabajo) de un trabajador en el sistema Softland. Permite obtener la descripción del cargo asociado a una ficha de empleado.

## Namespace
`FiniquitosV2.Clases`

## Propiedades

| Propiedad | Tipo | Descripción |
|-----------|------|-------------|
| `ficha` | `string` | Número de ficha del trabajador |
| `cargo` | `string` | Descripción del cargo del trabajador |

## Métodos

### `Obtener(string connectionString)`
Consulta el cargo del trabajador desde Softland.

- **Parámetro**: `connectionString` — Cadena de conexión a la base de datos Softland.
- **Retorno**: `void` — Establece la propiedad `cargo`.
- **Tablas consultadas**: `softland.sw_cargoper` y `softland.cwtcarg`
- **Join**: `sw_cargoper.codCargo = cwtcarg.codCargo`
- **Filtro**: `ficha = '{ficha}'`

#### Query SQL Ejecutada
```sql
SELECT c.desCargo
FROM softland.sw_cargoper p
INNER JOIN softland.cwtcarg c ON c.codCargo = p.codCargo
WHERE ficha = '{ficha}'
```

## Conexión a Softland
- Utiliza `Interface.ExecuteDataSet()`.
- Consulta tablas del esquema `softland`:
  - `sw_cargoper`: Tabla de cargos por persona (relación ficha-cargo).
  - `cwtcarg`: Catálogo maestro de cargos con descripciones.

## Manejo de Errores
- Registra errores mediante `Utilidades.LogError()` con proceso `"TW_Cargo.Obtener"`.
- En caso de error, `cargo` recibe el mensaje de excepción.

## Dependencias
- `Interface` — Ejecución de queries.
- `Utilidades` — Logging de errores.

## Observaciones
- ⚠️ **SQL Injection**: Concatenación directa de `ficha` en la query.
- Se usa en el proceso de generación de finiquitos para incluir el cargo del trabajador en el documento legal.
