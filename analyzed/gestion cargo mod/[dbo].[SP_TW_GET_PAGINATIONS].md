# Documentación del Procedimiento Almacenado: `[dbo].[SP_TW_GET_PAGINATIONS]`

## Objetivo
Este procedimiento almacenado actúa como un **motor centralizado de paginación**. Su objetivo es calcular y generar la estructura de navegación (botones de "Anterior", "Siguiente", números de página) para diversas listas y tablas del sistema. Construye dinámicamente consultas SQL para contar el total de registros basándose en filtros y perfiles de usuario, y retorna un conjunto de datos listo para renderizar los controles de paginación en el frontend.

## Parámetros de Entrada

| Parámetro | Tipo | Descripción |
| :--- | :--- | :--- |
| `@AUTHENTICATE` | VARCHAR(MAX) | Token de autenticación. |
| `@AGENTAPP` | VARCHAR(MAX) | Identificador del agente/aplicación. |
| `@TRABAJADOR` | VARCHAR(MAX) | Identificador (Nombre + Apellido) del usuario que realiza la consulta. |
| `@TYPEPAGINATION` | VARCHAR(MAX) | Identificador del contexto o tabla a paginar (ej. 'SolicitudBaja', 'Finiquitos'). |
| `@PAGINATION` | VARCHAR(MAX) | Estado actual de la paginación (Rango 'Inicio-Fin' codificado en Base64). |
| `@HASBAJA` | VARCHAR(MAX) | Filtro específico para solicitudes de baja. |
| `@TYPEFILTER` | VARCHAR(MAX) | Tipo de criterio de filtrado (Columna). |
| `@DATAFILTER` | VARCHAR(MAX) | Valor del filtro a aplicar. |

## Variables Internas

| Variable | Tipo | Descripción |
| :--- | :--- | :--- |
| `@AUTHENTICATION`, `@CODE`, `@MESSAGE` | VARIOS | Variables para el estado de la autenticación de seguridad. |
| `@PROFILE` | VARCHAR(MAX) | Perfil de seguridad del usuario (ej. 'KAM', 'ANALISTA'). |
| `@SQL` | VARCHAR(MAX) | Cadena que almacena el script SQL dinámico generado. |
| `#Pagination` | TABLE (Temp) | Tabla temporal donde se construyen las filas de la paginación (botones y páginas). |

## Lógica de Procesamiento

1.  **Seguridad**: Valida el token de autenticación mediante `SP_GET_AUTHENTICATIONTOKENSECURITY`.
2.  **Identificación de Perfil**: Obtiene el perfil del usuario consultando `TW_Usuarios`, `TW_Auth` y `TW_Propfiles`.
3.  **Construcción de SQL Dinámico (`@SQL`)**:
    *   **Cálculo de Registros**: Genera una subconsulta `SELECT COUNT(*)` específica para el `@TYPEPAGINATION` solicitado (ej. consulta `View_SolicitudesBajaNew` si es 'SolicitudBaja').
    *   **Aplicación de Filtros**: Inyecta cláusulas `WHERE` dinámicas según el `@PROFILE` (seguridad a nivel de fila) y los filtros de búsqueda (`@TYPEFILTER`/`@DATAFILTER`).
    *   **Lógica de Paginación**:
        *   Calcula el número total de páginas (`@PAGETOTAL`) basado en un rango fijo (`@RANGE = 5`).
        *   Genera filas para los botones de navegación ("Primero", "Anterior", "Siguiente", "Último") con lógica de habilitado/deshabilitado.
        *   Genera filas para los números de página visibles, usando un algoritmo de ventana deslizante para no mostrar todas las páginas si son muchas.
4.  **Ejecución**: Ejecuta el script dinámico construido mediante `EXEC(@SQL)`.
5.  **Retorno**: Devuelve el contenido de la tabla temporal `#Pagination`.

## Tablas afectadas, si es que hay y con que operaciones

*   Este procedimiento es de **sólo lectura** respecto a los datos del negocio.
*   Utiliza una tabla temporal `#Pagination` para estructurar la respuesta.
*   Consulta dinámicamente múltiples vistas y tablas según el contexto, incluyendo:
    *   `[dbo].[View_SolicitudesBajaNew]`
    *   `[cargamasiva].[View_KamClientes]`
    *   `[dbo].[FZ_Proveedores]`
    *   `[dbo].[View_Finiquitos]`
    *   `[dbo].[View_Simulaciones]`
    *   `[dbo].[View_Gastos]`, etc.
