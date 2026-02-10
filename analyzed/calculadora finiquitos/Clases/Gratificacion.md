# Gratificacion.cs

## Descripción General
Clase que consulta el valor de **gratificación** de un trabajador desde Softland. La gratificación es un componente obligatorio de la remuneración en Chile.

## Namespace
`FiniquitosV2.Clases`

## Propiedades

| Propiedad | Tipo | Descripción |
|-----------|------|-------------|
| `ficha` | `string` | Número de ficha del trabajador |
| `gratificacion` | `string` | Valor de la gratificación obtenido |

## Métodos

### `void Obtener(string connectionString)`
Obtiene el valor de gratificación desde la variable personal `P045` en Softland.

```sql
SELECT VALOR FROM softland.sw_variablepersona
WHERE ficha = '{ficha}' AND codVariable = 'P045'
```

- El código `P045` corresponde a la variable de gratificación en Softland.
- El resultado se almacena en la propiedad `gratificacion`.

## Conexión a Softland
- **Tabla**: `softland.sw_variablepersona` (variables por persona de Softland).
- **Columnas**: `ficha`, `codVariable`, `VALOR`.
- La variable `P045` es específica de la configuración de Softland de esta empresa.

## Manejo de Errores
- Log con `Utilidades.LogError()` proceso `"TW_Gratificacion.Obtener"`.
- En caso de error, `gratificacion` recibe el mensaje de excepción.

## Dependencias
- `Interface` — Ejecución SQL.
- `Utilidades` — Logging.

## Uso en el Sistema
- Componente del cálculo de **haberes mensuales** del finiquito.
- La gratificación se suma al sueldo base, bonos variables, colación y movilización para determinar la base de cálculo.

## Observaciones
- ⚠️ **SQL Injection**: `ficha` concatenada directamente.
- El código de variable `P045` está hardcodeado.
