# [remuneraciones].[__SPRM_CargoMod_Dashboard]

## Objetivo
Este procedimiento almacenado genera un resumen de indicadores (dashboard) con conteos de solicitudes y modificaciones de cargo en diferentes estados. La información retornada se personaliza dinámicamente según el perfil del usuario que realiza la consulta (KAM, Finanzas, Analista, etc.) y sus asignaciones (clientes o empresas).

## Parámetros de Entrada
| Nombre | Tipo | Descripción |
| :--- | :--- | :--- |
| `@USUARIOCREADOR` | `VARCHAR(MAX)` | Identificador del usuario codificado en Base64 (puede ser correo o nombre de usuario). Se utiliza para identificar al usuario y su perfil. |

## Variables Internas
| Nombre | Tipo | Descripción |
| :--- | :--- | :--- |
| `@PROFILE` | `VARCHAR(MAX)` | Almacena el nombre del perfil del usuario identificado (ej. 'KAM', 'FINANZAS'). |
| `@SQL` | `VARCHAR(MAX)` | Almacena la consulta SQL dinámica que se ejecutará finalmente. |
| `@__USERID` | `VARCHAR(MAX)` | ID numérico del usuario en el sistema. |
| `@__FILTERCUENTA` | `VARCHAR(MAX)` | Fragmento de SQL que contiene las condiciones de filtrado por cliente/empresa según la asignación KAM/Asistente. |

## Llamadas Internas (Funciones y Vistas)
- `[dbo].[FNBase64Decode]`: Utilizada para decodificar el parámetro `@USUARIOCREADOR`.
- Vistas de sistema y autenticación:
    - `[TW_GENERAL_TEAMWORK].[dbo].[TW_Usuarios]`
    - `[TW_GENERAL_TEAMWORK].[dbo].[TW_Auth]`
    - `[TW_GENERAL_TEAMWORK].[dbo].[TW_AuthProfiles]`
    - `[TW_GENERAL_TEAMWORK].[dbo].[TW_Profiles]`
- Vistas de negocio y control:
    - `[cargamasiva].[View_Clientes]`
    - `[kam].[View_KamCliente]`
    - `[kam].[View_AsistenteKamCliente]`
    - `[remuneraciones].[View_CargosModEstructura]`
    - `[remuneraciones].[View_EstructuraRentaBase]`
    - `[remuneraciones].[View_Dashboard]`
    - `[remuneraciones].[View_AsignAnalista]`

## Lógica de Cálculo
1.  **Identificación de Usuario**: Decodifica `@USUARIOCREADOR` y consulta las tablas de autenticación para obtener el `@PROFILE` y `@__USERID`.
2.  **Construcción de Filtro KAM (`@__FILTERCUENTA`)**: Si aplica, genera un filtro SQL para limitar los resultados a los clientes donde el usuario es KAM o Asistente.
3.  **Generación de Consulta Dinámica (`@SQL`)**: Construye una consulta diferente según el perfil:
    - **KAM**: Calcula conteos (Creaciones, Rechazados, Pendientes, Simulaciones, etc.) filtrando por `@__FILTERCUENTA`.
    - **FINANZAS / ADMINISTRADOR**: Obtiene sumatorias generales desde `[remuneraciones].[View_Dashboard]`.
    - **ANALISTA DE REMUNERACIONES / COORDINADOR CONTRATOS**: Calcula pendientes y terminados filtrando por las empresas asignadas al analista (`[remuneraciones].[View_AsignAnalista]`).
    - **ANALISTA CONTRATOS**: Solo visualiza conteos de 'Terminados'.
4.  **Ejecución**: Ejecuta la cadena `@SQL` mediante `EXEC`.

## Retorno
Retorna un conjunto de resultados con columnas variables según el perfil, típicamente incluyendo conteos para estados como:
- `Creaciones`
- `RechazadosKam` / `RechazadosRem`
- `PendientesFinanzas` / `PendientesRemuneraciones` / `PendientesFirmaDigital`
- `Terminados`
- `Simulaciones` / `ValidacionCotizaciones` / `CotizacionesAprobadas`
- `Profile` (El perfil detectado)

## Tablas Afectadas
El procedimiento es de solo lectura (SELECT). No modifica datos en las tablas.
