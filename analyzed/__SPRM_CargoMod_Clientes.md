# Documentación del Procedimiento Almacenado: `[remuneraciones].[__SPRM_CargoMod_Clientes]`

## Objetivo
Este procedimiento almacenado se encarga de **listar los clientes disponibles** para la asignación en solicitudes de "Cargo Modificado". Implementa **paginación** y **filtrado de seguridad** basado en el perfil del usuario (específicamente para el perfil KAM, restringiendo la vista a sus clientes asignados).

## Parámetros de Entrada

| Parámetro | Tipo | Descripción |
| :--- | :--- | :--- |
| `@EMPRESA` | VARCHAR(MAX) | Filtro opcional por empresa. |
| `@TYPEFILTER` | VARCHAR(MAX) | Tipo de filtro adicional (ej. 'Codigo'). |
| `@DATAFILTER` | VARCHAR(MAX) | Valor del filtro adicional. |
| `@PAGINATION` | VARCHAR(MAX) | Rango de paginación (codificado en Base64, formato 'Inicio-Fin'). |
| `@USUARIOCREADOR` | VARCHAR(MAX) | Identificador del usuario que consulta (codificado en Base64). |

## Variables Internas

| Variable | Tipo | Descripción |
| :--- | :--- | :--- |
| `@SQL` | VARCHAR(MAX) | Cadena para construir y ejecutar la consulta dinámica. |
| `@INIT` | VARCHAR(MAX) | Inicio del rango de paginación. |
| `@END` | VARCHAR(MAX) | Fin del rango de paginación. |
| `@PROFILE` | VARCHAR(MAX) | Perfil del usuario consultante. |
| `@__USER` | VARCHAR(MAX) | ID del usuario. |

## Lógica de Procesamiento

1.  **Paginación**:
    *   Si `@PAGINATION` no está vacío, decodifica el rango 'Inicio-Fin' en `@INIT` y `@END`.
    *   Si está vacío, por defecto usa `1 AND 1000`.
2.  **Identificación de Usuario**:
    *   Decodifica `@USUARIOCREADOR`.
    *   Obtiene `@PROFILE` y `@__USER` (ID) consultando tablas de autenticación (`TW_Usuarios`, `TW_Auth`, etc.).
3.  **Construcción de SQL Dinámico**:
    *   **CTE `Clientes`**: Selecciona desde `[remuneraciones].[View_Clientes]`.
    *   **Filtros Generales**:
        *   Filtra por `@EMPRESA` si se proporciona.
        *   Si `@TYPEFILTER` es 'Codigo', aplica filtro `LIKE` sobre la columna `Codigo`.
    *   **Seguridad por Perfil (KAM)**:
        *   Si el perfil es **KAM**, agrega una cláusula `AND` para filtrar solo los clientes asignados a ese usuario (Directos o como Asistente) consultando `[remuneraciones].[View_KamCliente]`.
    *   **Paginación**:
        *   Filtra los resultados usando `ROW_NUMBER BETWEEN @INIT AND @END`.
4.  **Selección Final**:
    *   Retorna `Codigo`, `Value` (Nombre) y `Empresa` de los clientes filtrados.
5.  **Ejecución**:
    *   Ejecuta la consulta construida mediante `EXEC (@SQL)`.

## Tablas afectadas, si es que hay y con que operaciones

Este procedimiento es de **sólo lectura** y no realiza modificaciones (INSERT, UPDATE, DELETE) en la base de datos.
Consulta información de:
*   `[remuneraciones].[View_Clientes]`
*   `[remuneraciones].[View_KamCliente]`
*   `[remuneraciones].[View_AsistenteKamCliente]`
*   Tablas de sistema de usuarios.
