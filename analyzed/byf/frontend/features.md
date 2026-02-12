# Documentación Funcional del Proyecto (Features)

Este documento detalla los módulos y funcionalidades de la aplicación `frontend-byf`. La aplicación está diseñada para la gestión integral de procesos de recursos humanos relacionados con desvinculaciones, finiquitos, pagos judiciales y documentos laborales.

## 1. Autenticación y Control de Acceso

El sistema cuenta con un módulo de seguridad robusto que gestiona el acceso de los usuarios.

*   **Inicio de Sesión (Login)**: Autenticación mediante credenciales (usuario/contraseña) contra una API (`/auth/signin`).
*   **Gestión de Sesiones**: Uso de tokens `Bearer` almacenados en `sessionStorage` (`glcid`) para autorizar todas las peticiones subsecuentes.
*   **Permisos Dinámicos (RBAC)**:
    *   Al iniciar sesión, el sistema consulta los permisos del usuario (`/menu/apps`).
    *   El menú de navegación y el acceso a rutas se construyen dinámicamente según las aplicaciones devueltas por el backend.
    *   Si el usuario tiene acceso a una sola aplicación, es redirigido automáticamente.
*   **Integración con Buk**: Recuperación automática de la foto de perfil del colaborador desde la plataforma Buk.

## 2. Módulo de Finiquitos

Es el módulo central de la aplicación, encargado de gestionar el ciclo de vida completo de los finiquitos.

### Funcionalidades Principales
*   **Listado y Gestión (Grid)**: Vista tabular paginada de finiquitos con filtros avanzados (empresa, estado, rut, etc.).
*   **Ficha de Finiquito**: Vista detallada (`/finiquito/:folio`) organizada en pestañas:
    *   **Resumen**: Datos del cálculo, causales, montos.
    *   **Documentos**: Gestión de PDFs generados.
    *   **Liquidaciones**: Historial de liquidaciones asociadas.
    *   **Historial y Comentarios**: Trazabilidad del proceso.
*   **Flujo de Aprobación**:
    *   **Validación**: Envío a validar, validación por jefaturas.
    *   **Visto Bueno (V°B°)**: Aprobación financiera.
    *   **Firma**: Proceso de firma digital o gestión de firma manual.
*   **Gestión Logística de Documentos**: Control del envío y recepción de documentos físicos hacia/desde Notarías y Regiones (`postGestionEnvioSantiagoNotaria`, `postGestionEnvioRegiones`).

## 3. Módulo de Avenimientos (Pagos Judiciales)

Módulo especializado para la gestión de acuerdos legales y pagos judiciales.

### Funcionalidades
*   **Gestión de Pagos**: Creación, edición y eliminación de pagos judiciales.
*   **Estados del Pago**:
    *   *Creado* -> *Pendiente V°B°* -> *Aprobado (Pago Pendiente)* -> *Pagado*.
    *   Posibilidad de **Rechazar** o **Restaurar** pagos en diferentes etapas.
*   **Acciones Masivas**: Selección múltiple para aprobar, validar o eliminar varios pagos simultáneamente.
*   **Reportabilidad**: Generación de reportes específicos de avenimientos.
*   **Validaciones**: Verificación de finiquitos vinculados al RUT antes de crear un avenimiento.

## 4. Módulo de Simulaciones

Permite realizar cálculos tentativos de finiquitos sin afectar los registros oficiales inmediatos.

### Funcionalidades
*   **Creación de Simulaciones**: Interface para ingresar parámetros de cálculo y visualizar resultados proyectados.
*   **Conversión**: Funcionalidad para **promover una simulación a propuesta** (`putChangeSimulacionToPropuesta`), integrándola al flujo oficial.
*   **Gestión**: Listado independiente para separar pruebas de casos reales.

## 5. Módulo de Cartas y Propuestas

Gestiona las "Propuestas de Finiquito", que son el paso previo a la consolidación de un finiquito legal.

### Funcionalidades
*   **Bandeja de Entrada**: Visualización de nuevas propuestas generadas.
*   **Flujos de Conversión**:
    *   Propuesta -> Simulación (`putChangePropuestaToSimulacion`): Si se requiere re-evaluar.
    *   Validación Masiva: Permite validar múltiples propuestas de una sola vez (`putValidarPropuestaMasive`), ideal para procesos de desvinculación masiva.
*   **Opciones Masivas**: Configuración de opciones para múltiples registros simultáneamente.

## 6. Módulo de Complementos

Gestiona pagos o documentos adicionales que complementan un finiquito o sueldo.

### Funcionalidades
*   **Asistente (Wizard) de Creación**: Flujo paso a paso (`GuiaComplemento`) para la creación de complementos:
    1.  Creación de la instancia.
    2.  Vinculación de ficha.
    3.  Definición de Haberes y Descuentos.
    4.  Definición de fechas.
*   **Activación**: Proceso para "activar" el complemento y hacerlo efectivo en el sistema.

## 7. Módulo de Cálculos

Enfocado en el procesamiento de "Bajas" (solicitudes de término de contrato).

### Funcionalidades
*   **Listado de Bajas**: Visualización de bajas pendientes de cálculo.
*   **Motor de Cálculo**: Al parecer invoca servicios externos (`REACT_APP_PATHCALC`/TokenAuth) para ejecutar la lógica de cálculo de montos.

## 8. Reportes

Centro de generación de informes para la toma de decisiones.

*   **Filtros Personalizables**:
    *   **Tipo**: Finiquito, Complemento.
    *   **Estado**: Creado, Pendiente Pago, Pendiente Legalizar, Pagado.
    *   **Empresa**: Filtros por división (TWEST, TWRRHH, TWC).
*   **Exportación**: Descarga de reportes (probablemente en Excel/XLSX dado que la librería está instalada).
