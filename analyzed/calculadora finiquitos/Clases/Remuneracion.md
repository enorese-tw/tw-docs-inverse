# Remuneracion.cs

## Descripción General
Clase que gestiona la obtención de **datos de remuneración** de los trabajadores. Lista las variables de remuneración (sueldo base, bonos, colación, movilización, etc.) consultando códigos de variables desde tablas locales y valores desde Softland. Soporta 3 empresas y 2 tipos de variables (fijas y variables).

## Namespace
`FiniquitosV2.Clases`

## Métodos (6 variantes de listado)

### Para empresa TWEST (EST):
#### `List<VariableRemuneracion> Listar(string connectionStringFN, string ficha, int mes)`
Lee códigos de variables de la tabla local `FNVARIABLEREMUNERACION` y obtiene valores de Softland.

```sql
SELECT codVariable FROM FNVARIABLEREMUNERACION
```

### Para empresa TEAMRRHH (OUT):
#### `List<VariableRemuneracion> ListarOUT(string connectionStringFN, string ficha, int mes, string areaNegocio)`
Lee códigos de `FNVARIABLEREMUNERACIONOUT` (tipo `'v'` = variable). Si el área de negocio es `"MOV"`, incluye "BONO EXTRAORDINARIO".

```sql
-- Si areaNegocio != "MOV":
SELECT codVariable FROM FNVARIABLEREMUNERACIONOUT WHERE tipo = 'v' AND CAST(descripcion AS VARCHAR(MAX)) <> 'BONO EXTRAORDINARIO'
-- Si areaNegocio == "MOV":
SELECT codVariable FROM FNVARIABLEREMUNERACIONOUT WHERE tipo = 'v'
```

#### `List<VariableRemuneracion> ListarOUTFijo(string connectionStringFN, string ficha, int mes)`
Lee variables fijas (tipo `'F'`).

```sql
SELECT codVariable FROM FNVARIABLEREMUNERACIONOUT WHERE tipo = 'F'
```

### Para empresa TEAMWORK (Consultora):
#### `List<VariableRemuneracion> ListarCONSULTORA(string connectionStringFN, string ficha, int mes)`
Lee códigos de `FNVARIABLEREMUNERACIONCONSULTORA` (tipo `'V'`).

#### `List<VariableRemuneracion> ListaCONSULTORAFijo(string connectionStringFN, string ficha, int mes)`
Lee variables fijas (tipo `'F'`).

### Utilitario:
#### `static DataTable ConvertListToDataTable(List<Remuneracion[]> list)`
Convierte una lista de arrays de `Remuneracion` a `DataTable`.

## Tablas de Configuración de Variables

| Tabla | Empresa | Descripción |
|-------|---------|-------------|
| `FNVARIABLEREMUNERACION` | TWEST | Variables para EST |
| `FNVARIABLEREMUNERACIONOUT` | TEAMRRHH | Variables para OUT |
| `FNVARIABLEREMUNERACIONCONSULTORA` | TEAMWORK | Variables para Consultora |

### Tipos de Variables
| Tipo | Descripción |
|------|-------------|
| `'V'` / `'v'` | Variable (bonos, comisiones) |
| `'F'` | Fija (sueldo base, colación, movilización) |

## Cadenas de Conexión Usadas
| Setting | Empresa |
|---------|---------|
| `connectionString` | Tabla local (lecturas de códigos) |
| `connectionStringTEAMRRHH` | Softland TEAMRRHH (valores OUT) |
| `connectionStringTEAMWORK` | Softland TEAMWORK (valores Consultora) |

## Flujo de Datos
1. Lee los **códigos de variables** de la tabla local (ej: `FNVARIABLEREMUNERACIONOUT`).
2. Para cada código, crea un `VariableRemuneracion` y llama a su método `Obtener()`.
3. `VariableRemuneracion.Obtener()` consulta **Softland** con ese código para obtener el valor real.

## Manejo de Errores
- Log con proceso `"Remuneraciones.Listar"` — pero con connectionString hardcodeado `"Hola"`.

## Observaciones
- ⚠️ **ConnectionString "Hola"** usado en el log de errores (no funcional).
- ⚠️ Patrón N+1 queries: por cada código de variable se ejecuta una query adicional a Softland.
- ⚠️ Miembros estáticos causan condiciones de carrera.
- ⚠️ Gran cantidad de código duplicado entre los 6 métodos — solo cambian la tabla y la conexión.
- Contiene un método `mesquebrado()` completamente comentado (no implementado).
