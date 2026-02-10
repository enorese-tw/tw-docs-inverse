# Documentación de Consultas: `CSolB4J.cs`

## Contexto
Esta clase gestiona un módulo específico de solicitudes de baja (identificado como "B4J"), posiblemente un portal o flujo de trabajo paralelo. Se basa enteramente en **Procedimientos Almacenados**.

## Procedimientos Almacenados Identificados

### 1. Gestión de Solicitudes (CRUD)
*   **SP**: `SP_SET_FN_CREAR_SOLB4J`
    *   **Método**: `SetCrearSolicitudB4J`
    *   **Propósito**: Crear una nueva solicitud de baja en el sistema.
    *   **Datos Esperados**: Probablemente el RUT del trabajador, causal, fecha de baja y usuario solicitante.

*   **SP**: `SP_FN_GET_OBTENER_SOLB4J`
    *   **Método**: `GetObtenerSolicitudesB4J`
    *   **Propósito**: Recuperar solicitudes existentes.
    *   **Uso**: Para listar solicitudes en una bandeja de entrada o reporte de gestión.

### 2. Flujo de Trabajo (Workflow)
*   **SP**: `SP_FN_SET_ADMINISTRAR_PROCESO_SOLB4J`
    *   **Método**: `SetAdministrarProcesosSolB4J`
    *   **Propósito**: Avanzar el estado de una solicitud (Aprobar, Rechazar, Derivar).
    *   **Lógica**: Centraliza la máquina de estados del proceso de baja.

### 3. Comunicaciones y Seguridad
*   **SP**: `SP_GET_PLANTILLAS_CORREO`
    *   **Método**: `GetPlantillasCorreo`
    *   **Propósito**: Obtener el cuerpo y asunto de correos electrónicos predefinidos.
    *   **Uso**: Enviar notificaciones automáticas a trabajadores o jefaturas.

*   **SP**: `SP_FN_GET_ENCRIPTAR_DATOS`
    *   **Método**: `GetToken`
    *   **Propósito**: Generar un token criptográfico o hash.
    *   **Uso**: Probablemente para generar enlaces seguros de un solo uso (ej. para firma electrónica o validación externa).

## Observaciones
El uso de SPs específicos para "B4J" sugiere que este es un módulo desacoplado del flujo principal de finiquitos (`CFiniquitos`), con su propia lógica de persistencia y reglas de negocio, aunque comparten la misma base de datos física (`TW_SA`).
