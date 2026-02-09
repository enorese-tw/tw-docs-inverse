# Documentación del Procedimiento Almacenado: `[remuneraciones].[__SPRM_CargoMod_Solicitudes]`

## Objetivo
Este procedimiento almacenado se encarga de **listar las solicitudes** de "Cargo Modificado", aplicando lógica de negocio compleja para filtrar y mostrar información relevante según el **perfil del usuario** que consulta. Utiliza SQL Dinámico para construir la consulta final.

## Parámetros de Entrada

| Parámetro | Tipo | Descripción |
| :--- | :--- | :--- |
| `@USUARIOCREADOR` | VARCHAR(MAX) | Identificador del usuario que consulta (codificado en Base64). Se usa para determinar el perfil y permisos. |
| `@PAGINATION` | VARCHAR(MAX) | Rango de paginación (codificado en Base64, formato 'Inicio-Fin'). |
| `@TYPEFILTER` | VARCHAR(MAX) | Tipo de filtro a aplicar (ej. 'CodigoSolicitud', 'Estado'). |
| `@DATAFILTER` | VARCHAR(MAX) | Valor del filtro. |

## Variables Internas

| Variable | Tipo | Descripción |
| :--- | :--- | :--- |
| `@PROFILE` | VARCHAR(MAX) | Perfil del usuario (KAM, FINANZAS, etc.) obtenido de las tablas de seguridad. |
| `@__USERID` | VARCHAR(MAX) | ID numérico del usuario en la tabla `TW_Usuarios`. |
| `@SQL` | VARCHAR(MAX) | Cadena que almacena la consulta SQL dinámica a ejecutar. |
| `@__EXCEPPROVMARG` | NUMERIC | Indicador de si el usuario tiene excepción para ver márgenes/provisiones. |

## Lógica de Procesamiento

1.  **Obtención de Contexto de Usuario**:
    *   Decodifica `@USUARIOCREADOR`.
    *   Obtiene `@PROFILE` y `@__USERID` cruzando información con tablas de autenticación (`TW_Auth`, `TW_Profiles`).
    *   Verifica si el usuario tiene permisos especiales en `View_ExcepcionProvMargen`.
2.  **Configuración de Filtros por Defecto**:
    *   Si no se especifica `@TYPEFILTER`, se asigna uno por defecto según el perfil:
        *   **KAM**: Filtra por 'SOLIC'.
        *   **FINANZAS**: Filtra por 'PDAPROB'.
        *   **ANALISTA REM/COORD**: Filtra por 'APROB'.
        *   **ANALISTA CONTRATOS/ADMIN**: Filtra por 'FD' o rango amplio.
3.  **Construcción de SQL Dinámico**:
    *   **CTE `Solicitudes`**: Selecciona desde `[remuneraciones].[View_Solicitudes]`.
    *   **Filtrado por Perfil**:
        *   **KAM**: Solo ve solicitudes de sus clientes asignados (Directos o como Asistente).
        *   **FINANZAS/ADMIN**: Ve solicitudes en estados de flujo administrativo ('PDAPROB', 'APROB', etc.).
        *   **ANALISTA REM**: Ve solicitudes de empresas asignadas a su cargo.
    *   **Filtrado Específico (`@TYPEFILTER`)**: Aplica cláusulas `WHERE` adicionales según el filtro solicitado (Código, Nombre, Estado, etc.).
4.  **Selección de Columnas y Permisos (Opciones)**:
    *   La consulta final proyecta columnas de datos (`CodigoCargoMod`, `Empresa`, etc.).
    *   Calcula columnas de "Opciones" (`OptEditar`, `OptRechazar`, etc.) dinámicamente según el perfil y el estado de la solicitud. Por ejemplo, un KAM solo puede editar si el estado es 'CREATE'.
5.  **Ejecución**:
    *   Ejecuta la cadena `@SQL` mediante `EXEC (@SQL)`.

## Tablas afectadas, si es que hay y con que operaciones

Este procedimiento es de **sólo lectura** y no realiza modificaciones (INSERT, UPDATE, DELETE) en la base de datos.
Consulta información principalmente de:
*   `[remuneraciones].[View_Solicitudes]`
*   `[cargamasiva].[View_Clientes]`
*   `[kam].[View_KamCliente]`
