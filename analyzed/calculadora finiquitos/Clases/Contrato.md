# Contrato.cs

## Descripción General
Clase **central de gestión de contratos** laborales. Es una de las clases más grandes y complejas del sistema (771 líneas). Consulta datos de contratos activos, terminados y su historial desde Softland, y ejecuta procesos de desvinculación. Maneja 3 empresas distintas con conexiones Softland independientes.

## Namespace
`FiniquitosV2.Clases`

## Propiedades Principales

| Propiedad | Tipo | Descripción |
|-----------|------|-------------|
| `rut` | `string` | RUT del trabajador |
| `ficha` | `string` | Ficha del trabajador |
| `nombre` | `string` | Nombre completo |
| `apellidoPaterno` | `string` | Apellido paterno |
| `apellidoMaterno` | `string` | Apellido materno |
| `correo` | `string` | Correo electrónico |
| `centrocosto` | `string` | Centro de costos |
| `cargo` | `string` | Cargo |
| `areaNegocio` | `string` | Área de negocio |
| `fechaContratoV` | `string` | Fecha vigencia contrato |
| `FecTermContrato` | `string` | Fecha término contrato |
| `gratificacion` | `string` | Valor gratificación |
| `cuenta` | `string` | Cuenta bancaria |
| `espartTime` | `bool` | Si es part-time |

---

## Métodos (14 principales)

### 1. `DataTable ContratoActivo(string connectionString, string rut)`
Obtiene el contrato activo de un trabajador por RUT.

```sql
SELECT DISTINCT TOP 1
  sw_personal.rut, sw_personal.ficha, sw_personal.Nombres, sw_personal.ApPaterno,
  sw_personal.ApMaterno, sw_personal.Email, sw_personal.FechaContratoV,
  sw_personal.FecTermContrato
FROM softland.sw_personal
WHERE rut = '{rut}'
ORDER BY ficha ASC
```

### 2. `string centrodecosto(string connectionString, string ficha)`
Obtiene el centro de costos del trabajador.

```sql
SELECT TOP 1 CenCos FROM softland.sw_personal WHERE ficha = '{ficha}'
```

### 3. `DataTable ContratosTerminados(string connectionString, string ficha)`
Lista contratos finalizados del trabajador.

```sql
SELECT FechaContratoV as Inicio, FecTermContrato as Termino,
       DATEDIFF(DAY, FechaContratoV, FecTermContrato) as Dias
FROM softland.sw_personal
WHERE ficha = '{ficha}'
ORDER BY ficha ASC
```

### 4. `DataTable ListarFiniquitados(string connectionString, string RUTTRABAJADOR)`
Busca trabajadores finiquitados en **EST (TWEST)** con todos sus datos asociados.

- Usa **3 cadenas de conexión hardcodeadas**:
  ```
  Data Source=conectorsoftland.team-work.cl\SQL2017;
  Initial Catalog=TWEST;User ID=Sa;Password=Softland070
  ```
- Consulta: `softland.sw_personal`, `softland.sw_estudiosup_nivest`
- Obtiene datos complementarios de: `Cargo.cs`, `AreaNegocio.cs`, `Gratificacion.cs`
- Busca en la tabla local `TW_OPERACIONES.dbo.FN_Contratos` para verificar el historial.

### 5. `DataTable ListarFiniquitadosOUT(string connectionString, string RUTTRABAJADOR)`
Busca trabajadores finiquitados en **OUT (TEAMRRHH)**.
- Conexión: `TEAMRRHH` Softland.
- Misma lógica que `ListarFiniquitados` pero para empresa 2.

### 6. `DataTable ListarFiniquitadosCONSULTORA(string connectionString, string RUTTRABAJADOR)`
Busca trabajadores finiquitados en **CONSULTORA (TEAMWORK)**.
- Conexión: `TEAMWORK` Softland.
- Misma lógica para empresa 3.

### 7. `DataTable SolicitudDeBaja(string connectionString, string RUTTRABAJADOR, int empresa)`
Busca datos para solicitud de baja, similar a `ListarFiniquitados` pero con lógica diferente.
- Usa un switch por empresa (1=TWEST, 2=TEAMRRHH, 3=TEAMWORK).

### 8-14. Otros Métodos
| Método | Descripción |
|--------|-------------|
| `ObtenerEstudiosSuperior()` | Lista estudios superiores desde Softland |
| `ListarContratosxFicha()` | Lista contratos por ficha |
| `ListarContratosxFichaOUT()` | Idem para OUT |
| `ListarTODOS()` | Lista todos los trabajadores |
| `ListarTODOSOUT()` | Lista todos OUT |
| `ListarTODOSCONSULTORA()` | Lista todos Consultora |
| `EsPartTime()` | Determina si es part-time |

---

## Conexiones a Softland (Hardcodeadas)

| Variable | Servidor | Base de Datos | Empresa |
|----------|----------|---------------|---------|
| `conStr` | `conectorsoftland.team-work.cl\SQL2017` | `TWEST` | EST |
| `conStr2` | `conectorsoftland.team-work.cl\SQL2017` | `TEAMRRHH` | OUT |
| `conStr3` | `conectorsoftland.team-work.cl\SQL2017` | `TEAMWORK` | Consultora |

**Credenciales**: `User ID=Sa; Password=Softland070`

---

## Tablas Softland Consultadas

| Tabla | Uso |
|-------|-----|
| `softland.sw_personal` | Datos personales, contratos |
| `softland.sw_estudiosup_nivest` | Estudios superiores |
| `softland.sw_variablepersona` | Variables de persona (gratificación) |
| `softland.sw_areanegper` | Área de negocio por persona |
| `softland.sw_cargoper` / `cwtcarg` | Cargo del trabajador |

## Tablas Locales

| Tabla | Uso |
|-------|-----|
| `TW_OPERACIONES.dbo.FN_Contratos` | Cross-database: historial de contratos |

---

## Manejo de Errores
- Log inconsistente: usa diferentes nombres de proceso (`"Trabajador.ContratoActivo"`, `"Trabajador.CentroDeCosto"`, generales).

## Observaciones Críticas
- ⚠️ **SEGURIDAD CRÍTICA**: 3 cadenas de conexión hardcodeadas con credenciales SA (`Sa`/`Softland070`).
- ⚠️ **SQL Injection** en todos los métodos.
- ⚠️ **Código duplicado masivo**: `ListarFiniquitados()`, `ListarFiniquitadosOUT()`, y `ListarFiniquitadosCONSULTORA()` son 95% idénticos.
- ⚠️ **Acceso cross-database**: Consultas directas a `TW_OPERACIONES.dbo.FN_Contratos`.
- ⚠️ **800+ líneas** en una sola clase con responsabilidades mezcladas.
- Esta es la clase que orquesta la obtención de **todos los datos del trabajador** necesarios para el finiquito.
