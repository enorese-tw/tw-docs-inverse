# FN_ENCDESVINCULACIONES.cs

## Descripción General
Clase de **acceso a datos** que lista los procesos de desvinculación existentes en la tabla `FN_ENCDESVINCULACION`. Funciona como repositorio estático para búsquedas generales de finiquitos.

## Namespace
`FiniquitosV2.Clases`

## Métodos

### `static List<FN_ENCDESVINCULACION> Listar(string connectionString)`
Lista todas las desvinculaciones (limitado a TOP 200) ordenadas por ID descendente.

```sql
SELECT TOP 200 *
FROM FN_ENCDESVINCULACION
ORDER BY ID DESC
```

#### Mapeo de Columnas
| Columna BD | Propiedad |
|------------|-----------|
| `ID` | `ID` |
| `RutTrabajador` | `RutTrabajador` |
| `NombreCompletoTrabajador` | `NombreCompletoTrabajador` |
| `EstadoSolicitud` | `EstadoSolicitud` |
| `FechaEstado` | `FechaEstado` |
| `IdCausal` | `IdCausal` |
| `Observaciones` | `Observaciones` |
| `FechaAvisoDesvinculacion` | `FechaAvisoDesvinculacion` |
| `IdEmpresa` | `IdEmpresa` |
| `UsuarioSolicitud` | `UsuarioSolicitud` |

### `static List<FN_ENCDESVINCULACION> Listar2(string connectionString, string empresa)`
Lista desvinculaciones filtradas por empresa y excluyendo estados anulados.

```sql
SELECT TOP 200 *
FROM FN_ENCDESVINCULACION
WHERE IdEmpresa LIKE '{empresa}'
  AND EstadoSolicitud NOT LIKE '%ANU%'
ORDER BY ID DESC
```

### `static List<FN_ENCDESVINCULACION> Listarconfirmados(string connectionString, string empresa, string formapago)`
Lista desvinculaciones confirmadas filtradas por empresa y forma de pago.

```sql
SELECT TOP 200 *
FROM FN_ENCDESVINCULACION
WHERE IdEmpresa LIKE '{empresa}'
  AND EstadoSolicitud NOT LIKE '%ANU%'
  AND EstadoSolicitud LIKE '%CONFIRMADO%'
  AND FormaPago LIKE '{formapago}'
ORDER BY ID DESC
```

## Base de Datos
- **Tabla**: `FN_ENCDESVINCULACION`.
- Usa diferentes filtros para las 3 variantes de listado.

## Manejo de Errores
- Log con proceso `"TelemedicionLib.Usuarios.Listar"` (incorrecto).

## Observaciones
- ⚠️ **SQL Injection** en `Listar2()` y `Listarconfirmados()`.
- ⚠️ Miembros estáticos causan condiciones de carrera.
- Limita resultados a TOP 200 — puede perder registros antiguos.
