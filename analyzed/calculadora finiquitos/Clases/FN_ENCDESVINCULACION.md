# FN_ENCDESVINCULACION.cs

## Descripción General
Clase central que gestiona el **encabezado de desvinculación** — el registro principal de cada proceso de finiquito. Permite crear, modificar, buscar y listar desvinculaciones en la tabla `FN_ENCDESVINCULACION`. Es una de las clases más críticas del sistema.

## Namespace
`FiniquitosV2.Clases`

## Propiedades Principales

| Propiedad | Tipo | Descripción |
|-----------|------|-------------|
| `ID` | `int` | ID de la desvinculación |
| `FechaHoraSolicitud` | `string` | Fecha/hora de la solicitud |
| `FechaAvisoDesvinculacion` | `string` | Fecha de aviso de desvinculación |
| `UsuarioSolicitud` | `string` | Usuario que creó la solicitud |
| `RutTrabajador` | `string` | RUT del trabajador |
| `FichaTrabajador` | `string` | Ficha del trabajador |
| `NombreCompletoTrabajador` | `string` | Nombre completo |
| `EstadoSolicitud` | `string` | Estado actual |
| `IdCausal` | `int` | ID de la causal de desvinculación |
| `IdEmpresa` | `int` | ID de empresa |
| `Observaciones` | `string` | Observaciones generales |
| `CargoMod` | `string` | Cargo del trabajador (desde Softland) |
| `AreaNegocio` | `string` | Área de negocio |
| `EsPartTime` | `string` | Si es part-time |
| `EsZonaExt` | `string` | Si es zona extrema |
| `TotalFiniquito` | `string` | Total calculado del finiquito |

## Métodos

### `void Guardar(string connectionString)`
Inserta o actualiza una desvinculación usando el stored procedure `SP_INSERTAR_MODIFICAR`.

```sql
EXEC SP_INSERTAR_MODIFICAR @pIDENDESVINCULACION, @pFECHAHORASO, @pFECHAAVISODES, ...
```

- Usa `SqlCommand` con parámetros parametrizados (a diferencia de la mayoría del código).
- El SP determina si es INSERT o UPDATE basándose en el ID.

### `static DataTable buscarkam(string connectionString, string kam)`
Busca desvinculaciones filtradas por KAM (ejecutivo de cuenta).

```sql
SELECT TOP 200
  enc.ID, enc.RutTrabajador, enc.NombreCompletoTrabajador,
  CASE enc.IdEmpresa WHEN 1 THEN 'TWEST' WHEN 2 THEN 'TEAMRRHH' ELSE 'TEAMWORK' END AS Empresa,
  enc.EstadoSolicitud, enc.FechaEstado, enc.IdCausal, enc.Observaciones,
  enc.FechaAvisoDesvinculacion
FROM FN_ENCDESVINCULACION enc
INNER JOIN FN_KAMCLIENTE kc ON kc.IDCLIENTE = enc.IDCLIENTE
INNER JOIN FN_KAM k ON k.IDKAM = kc.IDKAM
WHERE k.NOMBREKAM LIKE '{kam}'
ORDER BY enc.ID DESC
```

> ⚠️ **SQL Injection**: `kam` concatenado directamente.

### `static DataTable buscarxrut(string connectionString, string rut)`
Busca desvinculaciones por RUT del trabajador.

```sql
SELECT TOP 200
  enc.ID, enc.RutTrabajador, enc.NombreCompletoTrabajador, ...
FROM FN_ENCDESVINCULACION enc
WHERE enc.RutTrabajador LIKE '{rut}'
ORDER BY enc.ID DESC
```

> ⚠️ **SQL Injection**: `rut` concatenado directamente.

### `string ObtenerEstado(string connectionString, int ID)`
Obtiene el estado actual de una desvinculación.

```sql
SELECT EstadoSolicitud FROM FN_ENCDESVINCULACION WHERE ID = {ID}
```

## Stored Procedures Usados

| Stored Procedure | Uso |
|------------------|-----|
| `SP_INSERTAR_MODIFICAR` | Crear/actualizar el encabezado de desvinculación |

## Base de Datos
- **Tablas**:
  - `FN_ENCDESVINCULACION` (principal — encabezado de desvinculación).
  - `FN_KAMCLIENTE` (relación KAM-cliente).
  - `FN_KAM` (maestro de KAMs).
  - `FN_EMPRESAS` (maestro de empresas).

## Conexiones
- Base local `Finiquitos2018` para tabla principal.
- Dos cadenas de conexión hardcodeadas adicionales dentro de la clase:
  ```
  Data Source=192.168.0.10;Initial Catalog=Finiquitos2018QA;...
  ```

## Manejo de Errores
- `Guardar()`: Log con `Utilidades.LogError()` proceso `"TW_Gratificacion.Obtener"` (incorrecto).
- `buscarkam()`/`buscarxrut()`: Retornan `null` en caso de error, sin logging.

## Observaciones Críticas
- ⚠️ **SQL Injection** en `buscarkam()` y `buscarxrut()`.
- ⚠️ **Cadenas de conexión hardcodeadas** con credenciales.
- ⚠️ `Guardar()` usa parametrización correcta (stored procedure) — es el método mejor implementado.
- ⚠️ Nombre de proceso de error incorrecto en varios métodos.
- Tabla central del sistema — el `ID` de desvinculación se referencia en todas las tablas `FN_*`.
