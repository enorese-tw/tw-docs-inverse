# Análisis Profundo del Proyecto ApiTeamwork

## 1. Visión General
El proyecto **ApiTeamwork** es una aplicación backend construida sobre **ASP.NET Web API 2** (.NET Framework). Su arquitectura sigue un patrón de capas (Api, Model, Repository), pero destaca por delegar **casi la totalidad de la lógica de negocio y cálculos a Stored Procedures en SQL Server**.

La aplicación funciona principalmente como una pasarela (Gateway) que expone endpoints HTTP RESTful para invocar procedimientos almacenados, transformar los resultados y devolverlos en formato JSON.

## 2. Tecnologías Identificadas
-   **Framework:** .NET Framework (ASP.NET Web API 2).
-   **Lenguaje:** C#.
-   **Base de Datos:** SQL Server.
-   **Acceso a Datos:** ADO.NET puro (`SqlConnection`, `SqlDataAdapter`, `DataTable`). No se utiliza Entity Framework ni Dapper de manera estructural, sino una implementación propia de ejecución de comandos.
-   **Autenticación:** Tokens Bearer (indicado por `TokenValidationHandler` y `[Authorize]`).
-   **Dependencias Clave:** `Newtonsoft.Json` (inferido para serialización).

## 3. Arquitectura del Sistema

### 3.1. Estructura de Capas
El proyecto se divide en tres bibliotecas de clases principales:

1.  **Teamwork.Api:**
    -   Contiene los **Controladores** (`Controllers/*.cs`) que definen los endpoints.
    -   Maneja la configuración de rutas, filtros y seguridad (`App_Start`).
    -   Punto de entrada: `Global.asax.cs`.
2.  **Teamwork.Repository:**
    -   Contiene la lógica de acceso a datos.
    -   **CallExecutionAPI.cs:** Clase utilitaria genérica utilizada por la mayoría de los controladores para ejecutar Stored Procedures.
    -   **Repositorios Específicos:** Clases como `SoftlandRepository` o `FinanzasRepository` para lógicas más particulares.
3.  **Teamwork.Model:**
    -   Contiene clases POCO (Plain Old CLR Objects) que actúan como DTOs (Data Transfer Objects).
    -   Define estructuras para pasar parámetros a los procedimientos almacenados (Clases en `Parametros/`).

### 3.2. Flujo de Datos
El flujo típico de una petición es:
1.  **Request:** El cliente envía un JSON al endpoint (e.g., `POST /asistencia/Crear`).
2.  **Controller:** El controlador (`AsistenciaController`) recibe el modelo (`DataPackageAsistencia`).
3.  **Parameter Mapping:** Se transforman los datos del modelo a un arreglo de parámetros SQL usando clases helper (e.g., `PackageAsistencia.Values`).
4.  **Repository Execution:** Se llama a `CallExecutionAPI.ExecuteStoreProcedure` (o un repositorio específico).
5.  **Database:** Se ejecuta el Stored Procedure (e.g., `package.__KM__Asistencia`).
6.  **Response:** El SP retorna un `DataTable`, que es serializado a JSON y devuelto al cliente.

## 4. Análisis de la Capa de Datos (Crítico)

**Hallazgo Principal:** La aplicación es "Database-Driven". No hay lógica de negocio compleja en C#.

### 4.1. Ejecución Genérica (`CallExecutionAPI`)
La clase `Teamwork.Repository.Repository.CallExecutionAPI` es el corazón del acceso a datos.
-   Método: `ExecuteStoreProcedure`.
-   **Seguridad / Configuración:** Lee cadenas de conexión desde `Web.config`. Las credenciales y nombres de base de datos están almacenados en **Base64** en las `AppSettings` (`HostDatabase`, `UsernameDatabase`, `PasswordDatabase`, etc.).
-   **Manejo de Errores:** Captura excepciones y las relanza, pero no tiene una estrategia de logging o reintento visible en el código analizado.

### 4.2. Integración con Softland (`SoftlandController`)
Se detectó una integración explícita con el ERP Softland.
-   **Controlador:** `Teamwork.Api.Controllers.SoftlandController`.
-   **Repositorio:** `Teamwork.Repository.Repository.SoftlandRepository`.
-   **Lógica Dinámica:** El repositorio selecciona la base de datos de destino basándose en el parámetro `Client`:
    -   `TWEST` -> Base de datos de pruebas (configurada en `DatabaseTWEST`).
    -   `TWRRHH` -> Base de datos de recursos humanos (configurada en `DatabaseTWRRHH`).
    -   `TWC` -> Otra base de datos (configurada en `DatabaseTWC`).
-   **Funcionalidad:** Gestión de "Cargos" (`dbo.package_TW_CargoMod`), permitiendo Crear, Actualizar, Eliminar y Validar existencia.

### 4.3. Cálculos y Lógica de Negocio
Se realizó una búsqueda exhaustiva de lógica de cálculo en el código fuente C# (términos como "Calcular", "Impuesto", operadores matemáticos complejos) con resultados **negativos**.
-   **Conclusión:** Todos los cálculos (sueldos, finiquitos, asistencia, horas extras) se realizan dentro de los **Procedimientos Almacenados**.
-   **Ejemplos:**
    -   **Finiquitos:** `package.__Finiquitos` maneja lógica de validación, cálculo de montos, gestión de envíos y pagos.
    -   **Asistencia:** `package.__KM__Asistencia` maneja el cálculo de jornadas (`JornadaLaboralIngresar`), horas extras (`HorasExtraIngresar`) y bonos.
    -   **Finanzas:** `finanzas.package_FZ_ValorDiario` maneja valores diarios.

## 5. Endpoints Principales

A continuación se listan los controladores más relevantes y su función aparente:

| Controlador | Ruta Base | Descripción | SP Asociado |
| :--- | :--- | :--- | :--- |
| `SoftlandController` | `/softland` | Integración con ERP Softland (Cargos). | `dbo.package_TW_CargoMod` |
| `FinanzasController` | `/finanzas` | Gestión de valores diarios y datos financieros. | `finanzas.package_FZ_ValorDiario` |
| `FiniquitosController` | `/finiquitos` | Ciclo de vida completo de finiquitos (cálculo, firma, pago). | `package.__Finiquitos` |
| `AsistenciaController` | `/asistencia` | Gestión de asistencia, jornadas, horas extra y licencias. | `package.__KM__Asistencia` |
| `CoinsController` | `/coins` | Gestión de formularios dinámicos e inventario. | `package.Formularios`, `package.Inventario` |
| `AuthController` | `/auth` | (Inferido) Gestión de autenticación y emisión de tokens. | N/A |

## 6. Seguridad
-   **Autenticación:** La mayoría de los controladores tienen el atributo `[Authorize]`, lo que implica que requieren un usuario autenticado.
-   **Tokens:** Se utiliza un `TokenValidationHandler`, sugiriendo uso de JWT u otro mecanismo de token bearer.
-   **Datos Sensibles:** Las cadenas de conexión y credenciales están ofuscadas en Base64 en el `Web.config`, lo cual es una medida básica de seguridad (security by obscurity), pero no robusta.

## 7. Recomendaciones para el Mantenimiento
1.  **Documentar SPs:** Dado que la lógica reside en la base de datos, es vital documentar los Stored Procedures (`package.__Finiquitos`, `package.__KM__Asistencia`, etc.).
2.  **Manejo de Secretos:** Migrar las credenciales de Base64 a un gestor de secretos más seguro si es posible (e.g., Azure Key Vault) o variables de entorno.
3.  **Logs:** Implementar un sistema de logging robusto (e.g., Serilog) en `CallExecutionAPI` para trazar errores de base de datos.
