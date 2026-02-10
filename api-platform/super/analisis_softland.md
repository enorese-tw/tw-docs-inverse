# Análisis de Integración: Softland

## 1. Resumen Ejecutivo
La integración con el ERP **Softland** es directa y específica. Su propósito principal en este código es la **sincronización de Cargos (Puestos de Trabajo)**.
Al igual que el resto del sistema, sigue el patrón de "Delegación a Base de Datos", donde la lógica de inserción/validación en las tablas de Softland reside en un Stored Procedure.

## 2. Componentes de Integración

### 2.1. Controlador (`SoftlandController`)
Es el punto de entrada para las operaciones. Expone endpoints para gestionar la entidad "Cargo" en el ERP:
-   `CargoMod/Crear`
-   `CargoMod/Actualizar` (y variantes `ActualizarCodine`, `ActualizarGlosa`)
-   `CargoMod/Eliminar`
-   `CargoMod/ValidarExistencia...`

### 2.2. Repositorio (`SoftlandRepository`) - **Punto Crítico**
Este archivo contiene la lógica más interesante de la integración: el **Enrutamiento Dinámico de Base de Datos (Multi-Tenant)**.

El método `ExecuteStoreProcedure` recibe un parámetro `Client`. Basado en este valor, selecciona dinámicamente la cadena de conexión desde el `Web.config`:
-   `Client = "TWEST"` -> Usa `DatabaseTWEST`
-   `Client = "TWRRHH"` -> Usa `DatabaseTWRRHH`
-   `Client = "TWC"` -> Usa `DatabaseTWC`

Esto indica que la API es capaz de interactuar con **múltiples instancias o bases de datos de Softland** dependiendo del contexto del usuario o cliente.

### 2.3. Base de Datos (Stored Procedure)
Todas las acciones apuntan a un único SP: **`dbo.package_TW_CargoMod`**.
Este procedimiento recibe una `@OPCION` (ej: "Crear", "Eliminar") y los datos del cargo (`Codigo`, `Glosa`).
Es razonable deducir que este SP contiene los `INSERT`/`UPDATE` directos a las tablas nativas de Softland (probablemente tablas como `cwtcarg` o similares, típicas de Softland RRHH).

## 3. Seguridad y Configuración
-   **Credenciales:** Las conexiones a Softland usan un usuario/password específico definido en `Web.config` (`UsernameDatabaseSoftland`, `PasswordDatabaseSoftand`).
-   **Seguridad:** Las credenciales están ofuscadas en Base64, lo cual es una medida básica de protección.

## 4. Conclusión
-   **Interacción:** Alta cohesión con la BD de Softland, pero bajo acoplamiento en código C#.
-   **Lógica de Negocio:** **No existe en la API**. La API no valida si el cargo existe, si tiene empleados asociados, ni reglas de negocio de Softland. Todo eso lo delega al Stored Procedure `dbo.package_TW_CargoMod`.
-   **Riesgo:** Si cambia el esquema de Softland, se debe actualizar el Stored Procedure, no la API.
