# FNDocumentos.cs

## Descripción General
Clase de **acceso a datos** que lista todos los tipos de documentos disponibles para los procesos de finiquito. Repositorio estático complementario a `FNDocumento.cs`.

## Namespace
`FiniquitosV2.Clases`

## Métodos

### `static List<FNDocumento> Listar(string connectionString)`
Retorna todos los documentos de la tabla `FN_DOCUMENTO`.

```sql
SELECT * FROM FN_DOCUMENTO
```

#### Mapeo de Columnas
| Columna BD | Propiedad |
|------------|-----------|
| `ID` | `ID` |
| `DESCRIPCION` | `Descripcion` |

- **Retorno**: `List<FNDocumento>` o `null`.

## Manejo de Errores
- Log con proceso `"TelemedicionLib.Usuarios.Listar"` (incorrecto).

## Observaciones
- ⚠️ Miembros estáticos causan condiciones de carrera.
- ⚠️ Nombre de proceso de error incorrecto.
