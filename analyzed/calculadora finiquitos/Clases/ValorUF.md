# ValorUF.cs

## Descripción General
Clase que gestiona el **valor de la UF** (Unidad de Fomento) utilizado en los cálculos de finiquito. Permite obtener el último valor de UF para un mes y año específico desde la tabla `FNUF`.

## Namespace
`FiniquitosV2.Clases`

## Propiedades

| Propiedad | Tipo | Descripción |
|-----------|------|-------------|
| `UF` | `string` | Valor de la UF obtenido |

## Métodos

### `void UFULTIMA(string connectionString, string MES, string AÑO)`
Obtiene el último valor de UF registrado para un mes y año dados.

```sql
SELECT TOP 1 VALOR FROM FNUF
WHERE MES LIKE '{MES}' AND AÑO LIKE '{AÑO}'
ORDER BY FECHA DESC
```

- **Parámetros**:
  - `connectionString` — Cadena de conexión a BD local.
  - `MES` — Mes de consulta.
  - `AÑO` — Año de consulta.
- **Resultado**: Establece la propiedad `UF` con el valor encontrado.

## Base de Datos
- **Tabla**: `FNUF` (base local Finiquitos2018).
- **Columnas**: `VALOR`, `MES`, `AÑO`, `FECHA`.
- La UF se almacena con una fecha, y se toma la más reciente del período consultado.

## Manejo de Errores
- Registra errores con `Utilidades.LogError()` proceso `"TW_Gratificacion.Obtener"` (nombre incorrecto heredado).

## Dependencias
- `Interface` — Ejecución SQL.
- `Utilidades` — Logging.

## Observaciones
- ⚠️ **SQL Injection**: Parámetros `MES` y `AÑO` se concatenan directamente en la query.
- ⚠️ Nombre de proceso de error incorrecto (`"TW_Gratificacion.Obtener"`).
- El valor de la UF se usa en el módulo de cálculo de finiquitos para convertir montos entre pesos y UF.
