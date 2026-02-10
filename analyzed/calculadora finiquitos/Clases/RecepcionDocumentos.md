# RecepcionDocumentos.cs

## Descripción General
Clase que gestiona la **recepción de documentos de renuncia**. Permite registrar renuncias, consultar renuncias por KAM, contar registros y buscar el máximo ID por mes. Opera sobre la tabla `FN_DOCUMENTOSRECIBIDOS`.

## Namespace
`FiniquitosV2.Clases`

## Propiedades

| Propiedad | Tipo | Descripción |
|-----------|------|-------------|
| `ID` | `int` | ID del registro |
| `RUTTRABAJADOR` | `string` | RUT del trabajador |
| `IDEMPRESA` | `int` | ID de empresa |
| `NOMBRETRABAJADOR` | `string` | Nombre del trabajador |
| `NEGOCIO` | `string` | Área de negocio |
| `ESTADO` | `string` | Estado de la renuncia |
| `CAUSAL` | `string` | Causal de término |
| `DESDE` | `string` | Fecha de inicio |
| `LEGALIZADA` | `string` | Si está legalizada |
| `REGISTRADA` | `string` | Si está registrada |
| `VISTONOTIFICACION` | `string` | Si se ha visto la notificación |
| `KAM` | `string` | KAM asignado |
| `FECHAHORARECEPCION` | `string` | Fecha/hora de recepción |
| `MES` | `int` | Mes de consulta |
| `YEAR` | `int` | Año de consulta |
| `CONTADOR` | `int` | Resultado de conteo |
| `MAXIMO` | `int` | Máximo ID encontrado |

## Métodos

### `void GrabarRenuncia(string connectionString)`
Inserta un registro de renuncia recibida.

```sql
INSERT INTO FN_DOCUMENTOSRECIBIDOS VALUES (
  {ID}, '{RUTTRABAJADOR}', {IDEMPRESA}, '{NOMBRETRABAJADOR}',
  '{NEGOCIO}', '{ESTADO}', '{CAUSAL}', CAST('{DESDE}' AS DATETIME),
  '{LEGALIZADA}', '{OBSERVACION}', '{REGISTRADA}', '{VISTONOTIFICACION}',
  '{KAM}', CAST('{FECHAHORARECEPCION}' AS DATETIME)
)
```

> ⚠️ Usa `Properties.Settings.Default.connectionString` directamente (ignora el parámetro).

### `static List<Usuario> buscarKAM(string connectionString, string EMPRESA)`
Busca correos de KAMs asociados a una empresa.

```sql
SELECT Correo FROM [dbo].[View_KamCliente] WHERE NombreEmpresa = '{EMPRESA}'
```

### `int cantidadregistrosmes(string connectionString)`
Cuenta registros de un mes/año específico.

```sql
SELECT Count(ID) as conteo FROM FN_DOCUMENTOSRECIBIDOS
WHERE month(FECHAHORARECEPCION) like {MES}
  AND year(FECHAHORARECEPCION) like {YEAR}
```

### `int cantidadnoregistrado(string connectionString)`
Cuenta registros no registrados de un KAM.

```sql
SELECT Count(ID) as conteo FROM FN_DOCUMENTOSRECIBIDOS
WHERE REGISTRADA LIKE 'no' AND KAM LIKE '{KAM}'
```

### `int maxrenunciames(string connectionString)`
Obtiene el máximo ID de un mes/año.

```sql
SELECT MAX(ID) as MAXIMO FROM FN_DOCUMENTOSRECIBIDOS
WHERE month(FECHAHORARECEPCION) like {MES}
  AND year(FECHAHORARECEPCION) like {YEAR}
```

### `static List<Renuncia> recibidoxkam(string connectionString, string KAM)`
Lista renuncias recibidas por un KAM con datos de empresa.

```sql
SELECT D.ID, D.RUTTRABAJADOR, D.NOMBRETRABAJADOR, E.NOMBRE AS EMPRESA,
       D.NEGOCIO, D.DESDE, D.FECHAHORARECEPCION, D.LEGALIZADA, D.REGISTRADA
FROM FN_DOCUMENTOSRECIBIDOS D
INNER JOIN FN_EMPRESAS E ON E.ID LIKE D.IDEMPRESA
WHERE D.KAM LIKE '{KAM}'
ORDER BY D.ID DESC
```

## Base de Datos
- **Tablas**:
  - `FN_DOCUMENTOSRECIBIDOS` (principal — documentos recibidos).
  - `FN_EMPRESAS` (maestro de empresas, para JOIN).
  - `[dbo].[View_KamCliente]` (vista de relación KAM-Cliente).

## Manejo de Errores
- Log con procesos incorrectos: `"TelemedicionLib.Usuarios.Listar"` y `"Trabajador.ContratoActivo"`.

## Observaciones
- ⚠️ **SQL Injection masiva** en todos los métodos.
- ⚠️ `GrabarRenuncia()` ignora el parámetro `connectionString` y usa `Properties.Settings.Default.connectionString`.
- ⚠️ Nombres de procesos de error incorrectos en todos los métodos.
- ⚠️ Miembros estáticos causan condiciones de carrera.
