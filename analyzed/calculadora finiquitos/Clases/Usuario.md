# Usuario.cs

## Descripción General
Clase que gestiona la **autenticación y datos de usuarios** del sistema de finiquitos. Permite login, consultas de usuarios y gestión de permisos mediante la tabla `FNUSUARIOS`.

## Namespace
`FiniquitosV2.Clases`

## Propiedades

| Propiedad | Tipo | Descripción |
|-----------|------|-------------|
| `nombre` | `string` | Nombre del usuario |
| `correo` | `string` | Correo electrónico |
| `clave` | `string` | Contraseña |
| `codigoacceso` | `string` | Nivel de acceso (1=admin, etc.) |
| `empresa` | `string` | Empresa asociada |
| `nivel` | `string` | Nivel de permisos |

## Métodos

### `DataTable Login(string nombre, string clave)`
Autentica un usuario contra la tabla `FNUSUARIOS`.

```sql
SELECT * FROM FNUSUARIOS WHERE NOMBRE LIKE '{nombre}' AND CLAVE LIKE '{clave}'
```

- Usa la conexión hardcodeada de `Usuarios.CadenaConexion()`.
- **Retorno**: `DataTable` con datos del usuario autenticado, o `null`.

### `DataTable Buscar(string connectionString, string usuario)`
Busca un usuario específico.

```sql
SELECT * FROM FNUSUARIOS WHERE NOMBRE LIKE '{usuario}'
```

### `static List<Usuario> Listar(string connectionString)`
Lista todos los usuarios del sistema.

```sql
SELECT * FROM FNUSUARIOS
```

#### Mapeo de Columnas
| Columna BD | Propiedad |
|------------|-----------|
| `NOMBRE` | `nombre` |
| `CORREO` | `correo` |
| `CODIGOACCESO` | `codigoacceso` |

## Base de Datos
- **Tabla**: `FNUSUARIOS` (base local Finiquitos2018).
- Almacena credenciales de acceso al sistema.

## Manejo de Errores
- `Login()`: Sin logging de errores, retorna `null`.
- `Listar()`: Log con proceso `"TelemedicionLib.Usuarios.Listar"` (incorrecto).

## Observaciones
- ⚠️ **SEGURIDAD CRÍTICA**: Login compara contraseñas en texto plano en la BD.
- ⚠️ **SQL Injection** en `Login()` y `Buscar()`.
- ⚠️ Usa conexión hardcodeada de `Usuarios.CadenaConexion()` en `Login()`.
- El `CODIGOACCESO` controla el nivel de permisos en la interfaz web.
