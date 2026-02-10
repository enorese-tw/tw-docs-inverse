# VariableRemuneracion.cs

## Descripción General
Clase que representa una **variable de remuneración** del trabajador. Consulta valores de remuneración por ficha, mes y código de variable directamente desde Softland. Es el componente central para obtener datos salariales del trabajador.

## Namespace
`FiniquitosV2.Clases`

## Propiedades

| Propiedad | Tipo | Descripción |
|-----------|------|-------------|
| `ficha` | `string` | Ficha del trabajador |
| `mes` | `int` | Mes de la remuneración |
| `codVariable` | `string` | Código de variable en Softland |
| `variable` | `string` | Descripción de la variable |
| `valorvariable` | `string` | Valor monetario de la variable |
| `fechames` | `string` | Fecha del mes para la consulta |

## Métodos

### `void Obtener(string connectionString)`
Obtiene el valor de una variable de remuneración para un mes específico.

```sql
SELECT s.ficha, s.codVariable, s.mes, s.valor, s.flag, f.FechaMes, f.IndiceMes
FROM softland.sw_variablepersona s
INNER JOIN softland.sw_vsnpRetornaFechaMesExistentes f
  ON s.mes = f.IndiceMes
WHERE s.ficha = '{ficha}'
  AND s.codVariable = '{codVariable}'
  AND f.FechaMes = '{fechames}'
```

- Lee `valor` (monto) y `flag` (descripción de la variable).

### `string FechaRemuneracion(string connectionString, string ficha, int mes)`
Obtiene la fecha del mes correspondiente al índice de mes proporcionado.

```sql
SELECT Top 1 FechaMes
FROM softland.sw_vsnpRetornaFechaMesExistentes
ORDER BY Fechames DESC
```

- **Nota**: El parámetro `mes` no se usa en la consulta (siempre obtiene la última fecha disponible).

## Conexión a Softland
- **Tablas**:
  - `softland.sw_variablepersona` — Variables personales por mes.
  - `softland.sw_vsnpRetornaFechaMesExistentes` — Mapeo de índice de mes a fecha.
- Se usa con distintas bases: `TWEST`, `TEAMRRHH`, `TEAMWORK`.

## Manejo de Errores
- Log con proceso `"TW_Gratificacion.Obtener"` (incorrecto).

## Uso en el Sistema
- Llamada por `Remuneracion.cs` en sus métodos `Listar()`, `ListarOUT()`, `ListarOUTFijo()`, `ListarCONSULTORA()`, `ListaCONSULTORAFijo()`.
- Cada variable de remuneración se mapea a un código específico (ej: sueldo base, bonos, colación).

## Observaciones
- ⚠️ **SQL Injection**: Múltiples parámetros concatenados directamente.
- ⚠️ `FechaRemuneracion()` ignora el parámetro `mes`.
- La lista de códigos de variables consultables se obtiene de tablas `FNVARIABLEREMUNERACION`, `FNVARIABLEREMUNERACIONOUT`, `FNVARIABLEREMUNERACIONCONSULTORA`.
