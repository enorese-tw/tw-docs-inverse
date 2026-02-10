# Análisis de XmlSolB4J.js

## Ubicación
`FiniquitosV2/Scripts/Finiquitos/ajax/XmlSolB4J.js`

## Descripción
Dashboard de gestión de Solicitudes de Baja. Permite ver, aprobar, rechazar y anular solicitudes.

## Funciones Principales
- `ajaxObtenerSolicitudesB4J`: "Super-función" que carga listas, detalles, archivos adjuntos y trazas de auditoría dependiendo de los parámetros `etapa` y `section`.
- `ajaxAdministrarProcesosSolB4J`: Ejecuta transiciones de estado (APROVAR, RECHAZAR, ANULAR, REVERTIR).
- `ajaxDownloadFile`, `ajaxTemporaryPDF`: Manejo de documentos.

## Llamadas al Servidor (API/Backend)
- `xmlsolb4j.aspx/GetObtenerSolicitudesB4J`: Endpoint polimórfico (hace muchas cosas distintas según params).
- `xmlsolb4j.aspx/SetAdministrarProcesosSolB4J`: Cambios de estado.
- `DownloadFiles.ashx`, `PDFTemporal.ashx`: Handlers HTTP.

## Lógica de Negocio
- **Flujo de Aprobación**:
    - Inicio -> Validación -> Aprobado/Rechazado.
    - Rol de auditoría implícito en "Revertir Estado".
- **Visualización Condicional**:
    - Botones de acción (Aprobar, Rechazar, etc.) se muestran u ocultan según flags que vienen del backend (e.g. `BTN_APROV_SOLB4J`). Esto indica que la lógica de permisos está en el servidor. (Buena práctica).

## Observaciones
- El renderizado HTML en JS es muy extenso y complejo, dificultando cambios de diseño.
- Usa `FormData` para descargas, lo cual es moderno comparado con otros scripts.
