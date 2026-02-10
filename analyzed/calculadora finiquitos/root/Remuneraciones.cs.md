# Remuneraciones.cs

## Información General

| Campo | Valor |
|---|---|
| **Archivo** | `FiniquitosV2/Remuneraciones.cs` |
| **Namespace** | `FiniquitosV2.Clases` |
| **Clase** | `Remuneraciones` |
| **Líneas de código** | 101 |
| **Dependencias clave** | `System.Data`, `System.Data.SqlClient`, `FiniquitosV2.Clases.Interface`, `FiniquitosV2.Clases.Utilidades` |

## Descripción Funcional

Clase de negocio para consultar **liquidaciones de remuneraciones** de trabajadores desde la base de datos **Softland**. Contiene un método activo y un método comentado (código muerto). La clase forma parte del namespace `FiniquitosV2.Clases` y se utiliza para obtener datos de liquidaciones del sistema de remuneraciones Softland.

## Propiedades

| Propiedad | Tipo | Descripción |
|---|---|---|
| `ficha` | `string` | Ficha (ID) del trabajador en Softland |

## Campos Estáticos

| Campo | Tipo | Descripción |
|---|---|---|
| `strSql` | `string` (static) | Almacena la consulta SQL actual |
| `ds` | `DataSet` (static) | Almacena los resultados de la consulta |

## Métodos

### `liquidaciones(string connectionString)` → `List<Remuneracion[]>`
- **Propósito**: Consultar las liquidaciones mensuales de un trabajador.
- **Parámetros**: `connectionString` — cadena de conexión a la base de datos Softland.
- **Consulta SQL**:
  ```sql
  SELECT ficha, mes 
  FROM TWEST.softland.sw_variablepersona 
  WHERE ficha = '{ficha}' 
  GROUP BY ficha, mes 
  ORDER BY mes DESC
  ```
- **Tablas de Softland consultadas**: `TWEST.softland.sw_variablepersona`
- **Lógica**:
  1. Ejecuta la consulta vía `Interface.ExecuteDataSet(connectionString, strSql)`.
  2. Itera sobre los resultados.
  3. Crea instancias de `Remuneracion` por cada fila.
  4. **⚠️ Retorna en la primera iteración del foreach** — solo procesa la primera fila y retorna inmediatamente. Esto parece ser un **bug** o código incompleto.
- **Manejo de errores**: Captura excepciones y las loguea vía `Utilidades.LogError()`.

### Método Comentado: `Listar(string connectionString)` (líneas 56-98)
- **Estado**: Completamente comentado — código muerto.
- **Propósito original**: Similar a `liquidaciones()` pero intentaba llamar a `Remuneracion.Listar()` con parámetros hardcodeados como `"juan"` y `"JuN"`.
- **Observación**: Los parámetros `"juan"` y `"JuN"` son claramente valores de prueba que se dejaron en el código.

## Conexiones a Base de Datos

### SQL Directo (Softland)

| Esquema | Tabla | Columnas Consultadas | Uso |
|---|---|---|---|
| `TWEST.softland` | `sw_variablepersona` | `ficha`, `mes` | Obtener meses con liquidaciones del trabajador |

> [!NOTE]
> La conexión no está hardcodeada en esta clase — se recibe como parámetro `connectionString`. Sin embargo, los llamadores (como `CalculoBajaEst.aspx.cs`) sí pasan el string con credenciales hardcodeadas.

## Clases y Dependencias Referenciadas

| Clase | Uso |
|---|---|
| `Interface` | `ExecuteDataSet()` — ejecuta consultas SQL y retorna un DataSet |
| `Utilidades` | `LogError()` — registro de errores |
| `Remuneracion` | Modelo de datos para una remuneración individual (no se usa completamente) |

## Vulnerabilidades y Observaciones

> [!CAUTION]
> **Inyección SQL**: La consulta usa `string.Format()` con interpolación directa del valor de `ficha`:
> ```csharp
> string.Format("...where ficha = '{0}'...", ficha);
> ```
> Si `ficha` proviene de input no sanitizado, es vulnerable a inyección SQL. Debería usar parámetros SQL:
> ```csharp
> "...where ficha = @ficha"
> ```

> [!WARNING]
> **Campos estáticos (`static`)**: `strSql` y `ds` son `static`, lo que significa que son compartidos entre todas las instancias y todos los hilos de ejecución. En un servidor web multi-usuario, esto genera **condiciones de carrera** — dos solicitudes simultáneas corromperían los datos de la otra. Deberían ser variables locales del método.

> [!WARNING]
> **Bug en `liquidaciones()`**: El `return rem` dentro del `foreach` hace que solo se procese **la primera fila** del resultado. El bucle nunca itera más de una vez. La lista `rem` siempre se retorna vacía (nunca se agrega nada con `rem.Add()`).

> [!WARNING]
> **Código muerto**: El método `Listar` (líneas 56-98) está totalmente comentado con parámetros de prueba hardcodeados (`"juan"`, `"JuN"`). Debería eliminarse o implementarse correctamente.

> [!NOTE]
> Esta clase parece estar **incompleta o abandonada**. El método `liquidaciones()` no funciona correctamente (retorna lista vacía en la primera iteración). Es probable que los cálculos de remuneraciones se hagan por otro camino (como `Clases.Contrato.ObtenerLiquidaciones()` en los archivos `CalculoBaja*.aspx.cs`).
