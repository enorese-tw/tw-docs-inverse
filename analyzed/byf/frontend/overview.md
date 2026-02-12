# Visión General del Proyecto

Este documento proporciona una descripción detallada del proyecto `frontend-byf`. El objetivo principal es documentar la arquitectura, módulos, conexiones a servicios externos y flujos de datos identificados en el código fuente.

## 1. Arquitectura del Proyecto

El proyecto es una **Single Page Application (SPA)** construida con **React**.

- **Framework**: React (v17.0.2)
- **Enrutamiento**: `react-router-dom` (v6.3.0)
- **Gestión de Estado**: React Context API (`src/contexts`)
- **Cliente HTTP**: Axios (para comunicación con APIs)
- **Estilos**: `@emotion/react`, `@emotion/styled`, `@mui/icons-material`
- **Utilidades**: `xlsx` (manejo de Excel), `uuid`, `base-64`

### Estructura de Directorios

- `src/common`: Utilidades y funciones compartidas (ej. `Rut.js`, `Sessions.js`).
- `src/components`: Componentes reutilizables de la UI.
- `src/containers`: Vistas principales y lógica de presentación (Módulos).
- `src/contexts`: Lógica de negocio y gestión de estado global, organizado por módulos.
- `public`: Activos estáticos.

## 2. Configuración y Entorno

La configuración del proyecto se maneja a través de variables de entorno (archivos `.env`).

### Variables de Entorno Clave

| Variable | Descripción | Valor (Dev) | Valor (Prod) |
| :--- | :--- | :--- | :--- |
| `REACT_APP_API` | URL base de la API Backend principal | `http://localhost:4001` | `http://api.team-work.cl:82/byf` |
| `REACT_APP_TOKENBUK` | Token de autenticación/servicio (Buk?) | `Cm4UM65SVGeMwBgWdjbxnLJv` | `Cm4UM65SVGeMwBgWdjbxnLJv` |
| `REACT_APP_PDF` | Servicio de generación/visualización de PDFs | `http://localhost:24564/pdf/` | `http://181.190.6.196:82/pdf/pdf` |
| `REACT_APP_PATHCALC` | Servicio de cálculos / TokenAuth | `http://localhost:8080/TokenAuth` | `http://finiquitos.team-work.cl:82/TokenAuth` |
| `REACT_APP_ME` | URL de la propia aplicación (cliente) | `http://localhost:3000/` | `http://byf.team-work.cl:82/` |

## 3. Módulos del Proyecto

La aplicación se divide en varios módulos funcionales, identificados por sus contenedores y contextos asociados. El acceso a estos módulos está controlado por un sistema de permisos definido en `src/App.js`.

### Lista de Módulos

1.  **Finiquitos**
    -   **Descripción**: Gestión completa de procesos de finiquitos.
    -   **Rutas**: `/finiquitos`, `/finiquitos/:page`
    -   **Contexto**: `FiniquitosState`
    -   **Funcionalidades**: Listar, filtrar, ver detalles (`/finiquito/:folio`), gestionar periodos, haberes, descuentos, vacaciones, y documentos asociados.

2.  **Avenimientos**
    -   **Descripción**: Gestión de avenimientos (acuerdos legales).
    -   **Rutas**: `/avenimientos`
    -   **Contexto**: `AvenimientosState`

3.  **Simulaciones**
    -   **Descripción**: Módulo para realizar simulaciones de cálculos.
    -   **Rutas**: `/simulaciones`
    -   **Contexto**: `SimulacionesState` (anidado en `FiniquitosState`)

4.  **Complementos**
    -   **Descripción**: Gestión de complementos de sueldo o documentos adicionales.
    -   **Rutas**: `/complementos`, `/complementos/:page`, `/complemento/:codigo/:event/:stage` (Guía de complemento)
    -   **Funcionalidades**: Crear, actualizar fichas, haberes, descuentos y fechas de documentos complementarios.

5.  **Cartas**
    -   **Descripción**: Generación y gestión de cartas (probablemente de aviso o despido).
    -   **Rutas**: `/cartas`, `/cartas/:page`
    -   **Contexto**: `PropuestasState` (anidado en `FiniquitosState`)

6.  **Cálculos**
    -   **Descripción**: Visualización o ejecución de cálculos específicos.
    -   **Rutas**: `/calculos`, `/calculos/:page`

7.  **Propuestas**
    -   **Contexto**: `PropuestasState`
    -   **Uso**: Utilizado dentro del módulo de Cartas.

8.  **Reportes**
    -   **Rutas**: `/report`
    -   **Container**: `Report`

9.  **PDF**
    -   **Rutas**: `/pdf/:document/:type/:filename`
    -   **Uso**: Componente dedicado a la visualización de documentos PDF.

10. **Autenticación (User)**
    -   **Contexto**: `UserState` / `UserContext`
    -   **Funcionalidades**: Login (`SignIn`), verificación de token (`getVerifyToken`), manejo de permisos (`apps`, `unauthorized`). Integración con `sessionStorage` (`glcid`).

## 4. Integraciones y Servicios Externos

El frontend no se conecta directamente a una base de datos. Interactúa con el mundo exterior exclusivamente a través de APIs HTTP.

### API Backend Principal (`REACT_APP_API`)

Es el núcleo de la lógica de negocio. El frontend realiza peticiones REST (GET, POST, PUT) a endpoints como:

-   `/bajas`: Listado de bajas.
-   `/finiquitos`: Gestión de finiquitos.
-   `/complementos`: Gestión de complementos.
-   `/finiquito/*`: Operaciones CRUD sobre finiquitos específicos (periodos, haberes, total, documentos).

### Servicio de PDF (`REACT_APP_PDF`)

Servicio dedicado presumiblemente a la generación o recuperación de archivos PDF físicos para su visualización o descarga.

### Servicio de Cálculos (`REACT_APP_PATHCALC`)

Parece ser un servicio separado o un endpoint específico para validación de tokens (`/TokenAuth`) o cálculos complejos delegados al servidor.

## 5. Detalles de Implementación Relevantes

-   **Autenticación**: Se utiliza un token (Bearer) almacenado en `sessionStorage` (`glcid`) que se envía en los encabezados `Authorization` de las peticiones Axios.
-   **Manejo de Errores**: La aplicación cuenta con manejo de errores 404 (`Error404`, `Error404App`) y estados de carga (`Loading`).
-   **Permisos Dinámicos**: En `App.js`, se renderizan las rutas basándose en una lista de `apps` permitidas obtenidas del contexto de usuario (`UserContext`), lo que sugiere un sistema de control de acceso dinámico (RBAC) gestionado desde el backend.
