# MethodServiceSoftland.cs

## Descripción General
Clase **proxy/fachada** para consumir un servicio WCF de Softland. Wrapper que encapsula llamadas al servicio `ServiceSoftlandClient` para obtener datos de Softland de forma remota (centro de costos y cargos).

## Namespace
`FiniquitosV2.Clases`

## Campo de Instancia

| Campo | Tipo | Descripción |
|-------|------|-------------|
| `svcSoftland` | `ServiceSoftland.ServiceSoftlandClient` | Instancia del cliente WCF |

## Propiedades (parámetros de entrada)

| Propiedad | Tipo | Descripción |
|-----------|------|-------------|
| `FICHA` | `string` | Ficha del trabajador |
| `EMPRESA` | `string` | Identificador de empresa |

## Métodos Públicos (Properties como Service)

### `GetCentroCostosService` / `GetCentroCostosOUTService`
Obtiene centro de costos para empresa EST / OUT.

### `GetCargoService` / `GetCargoOUTService`
Obtiene cargo del trabajador para empresa EST / OUT.

## Métodos Privados

### `DataSet GetCentroCostos(string ficha)` / `DataSet GetCentroCostosOUT(string ficha)`
```
Parámetros WCF: @FICHA
Servicio: svcSoftland.GetCentroCostos() / GetCentroCostosOUT()
```

### `DataSet GetCargo(string ficha)` / `DataSet GetCargoOUT(string ficha)`
```
Parámetros WCF: @FICHA
Servicio: svcSoftland.GetCargo() / GetCargoOUT()
```

## Arquitectura
```
MethodServiceSoftland (Facade)
    └── ServiceSoftland.ServiceSoftlandClient (WCF Proxy)
        └── Servicio WCF Softland (remoto)
            └── Base de datos Softland (SQL Server)
```

## Patrón de Diseño
- **Fachada (Facade)**: Simplifica el acceso al servicio WCF.
- Usa propiedades con getter como "service properties" para exponer métodos.
- Los parámetros se pasan como arrays de strings `parametros[]` y `valores[]`.

## Observaciones
- Misma estructura que `MethodServiceFiniquitos.cs` pero más pequeña.
- Diferencia entre EST/OUT: cada empresa tiene su propio método en el servicio WCF remoto.
- No accede directamente a la BD — todo va a través del servicio WCF.
