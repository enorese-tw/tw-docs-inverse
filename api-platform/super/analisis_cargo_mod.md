# Análisis Profundo del Componente: Cargo Mod

## 1. ¿Qué es "Cargo Mod"?
Basado en el análisis del código fuente, **Cargo Mod** es un módulo central del sistema diseñado para la **gestión, simulación y aprobación de estructuras de cargos y remuneraciones**. No es simplemente un mantenedor de cargos, sino un motor complejo que maneja el ciclo de vida de una "Solicitud" de creación o modificación de condiciones laborales.

Permite definir y calcular escenarios salariales (Sueldos, Gratificaciones, Bonos, Provisiones) y sus costos asociados (Margen, Costo Empresa) antes de que se oficialicen.

## 2. Arquitectura Técnica
El componente se distribuye en varios controladores, pero su núcleo lógico reside casi exclusivamente en **Stored Procedures (SQL Server)**.

### 2.1. Controladores Involucrados
1.  **`OperacionesController` (Principal):**
    -   Maneja el 90% de la funcionalidad.
    -   Ruta base: `api/operaciones/CargoMod/*`.
    -   Gestiona el flujo de la solicitud, cálculos financieros, asignación de bonos y configuración de jornada.
2.  **`SoftlandController` (Integración):**
    -   Ruta base: `api/softland/CargoMod/*`.
    -   Se encarga de la **sincronización con el ERP Softland**.
    -   Permite Crear, Actualizar y Validar cargos directamente en la base de datos de Softland (`dbo.package_TW_CargoMod`).
3.  **`ExcelController` (Reportes):**
    -   Genera reportes exportables de Cargo Mod (`remuneraciones.package_RM_Excel`).

### 2.2. Base de Datos (Stored Procedures)
La lógica pesada se delega a los siguientes paquetes en base de datos:
-   **`remuneraciones.package_RM_CargoMod`:** El "cerebro" del módulo. Maneja estados, cálculos de sueldo, actualizaciones de jornada, etc.
-   **`remuneraciones.package_RM_Bonos`:** Gestión específica de bonos asociados a la simulación.
-   **`remuneraciones.package_RM_ANI`:** Gestión de "ANI" (Asignación No Imponible, posiblemente).
-   **`remuneraciones.package_RM_ProvMargen`:** Cálculos de provisiones y márgenes de ganancia.
-   **`dbo.package_TW_CargoMod`:** Interacción directa con tablas de Softland.

## 3. Funcionalidades y Acciones Detalladas

### 3.1. Gestión de Solicitudes (Workflow)
El módulo opera mediante "Solicitudes", lo que implica un flujo de trabajo (Workflow) con estados.
-   **Acciones:** `CrearSolicitud`, `DeshacerSolicitud`, `Solicitudes` (Listar), `CambioEstadoSolicitud`, `ValidaStageActual`.
-   **Wizards:** Existen pasos guiados (`ActualizaWizards`, `ValidaStageWizards`) para completar la definición del cargo.

### 3.2. Gestión Financiera y Cálculos
Es la parte más compleja. Permite ajustar variables para llegar a un sueldo o costo objetivo.
-   **Sueldo Base:** `ActualizaSueldo`, `CambiarInputSueldo`, `CambiarCalculoTypeSueldo`.
-   **Gratificación:** `ActualizaGratificacion`.
-   **Valor Diario:** `ActualizaValorDiaPT`.
-   **Estructura de Costos:** Endpoints para obtener detalles desglosados:
    -   `Estructura/Haberes`
    -   `Estructura/Descuentos`
    -   `Estructura/MargenProvision`

**Nota sobre Cálculos:** **NO existen fórmulas de cálculo en el código C#**. Acciones como `CambiarCalculoTypeSueldo` simplemente envían el nuevo valor al SP `remuneraciones.package_RM_CargoMod`, el cual realiza la matemática interna (probablemente cálculos inversos de sueldo líquido a bruto o viceversa) y devuelve los resultados recalculados.

### 3.3. Configuración Laboral
-   **Jornada y Horario:** `ActualizaHorario`, `ActualizaJornadaFullTime`, `ActualizaDiasSemanales`, `CambiarTypeJornada`.
-   **Previsión:** `AFP`, `ActualizarAFP`.

### 3.4. Ítems Adicionales (Componentes)
El módulo permite agregar componentes arbitrarios a la estructura de costos:
-   **Bonos:** Agregar, Eliminar, Validar Asignación (`Bonos/AgregarBono`).
-   **ANI (Asignaciones No Imponibles):** Gestión de montos no tributables (`ANI/AgregarANI`).
-   **Provisiones y Márgenes:** Configuración fina de qué se considera costo o margen (`ProvMargen/Crear`, `CambiarProvMargGastoExcInc`).

## 4. Complejidad y Riesgos
-   **Complejidad de Negocio:** **ALTA**. Cubre múltiples aristas de la legislación laboral y financiera (tributación, jornadas, previsión).
-   **Complejidad de Código (C#):** **BAJA**. El código C# es transparente; solo empaqueta parámetros y llama a la BD.
-   **Riesgo de Mantenimiento:** **ALTO en Base de Datos**. Cualquier cambio en la lógica de cálculo requiere modificar Stored Procedures complejos. La depuración es difícil sin acceso directo a SQL Server Profiler o al código de los SPs.

## 5. Conclusión
"Cargo Mod" es un **simulador y gestor de costos laborales**. Su propósito es permitir a la empresa modelar cuánto costará un cargo, definiendo sueldo, jornada, bonos y margenes, para luego integrarlo (vía Softland) o usarlo como base para contrataciones. Toda la "inteligencia" reside en los Procedimientos Almacenados de SQL Server.
