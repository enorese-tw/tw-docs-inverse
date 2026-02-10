# Análisis de SolicitudBaja.js

## Ubicación
`FiniquitosV2/Scripts/Finiquitos/ajax/SolicitudBaja.js`

## Descripción
Formulario principal para iniciar el proceso de finiquito (Solicitud de Baja). Es uno de los scripts más críticos del frontend.

## Funciones Principales
- `ajaxValidarPermitidoSolBj4`: Verifica si el trabajador ya tiene una baja en curso.
- `ajaxCrearSolicitudB4J`: Envía el formulario completo. Nota: "B4J" podría referirse a un sistema legado o nomenclatura interna.
- `ajaxUploadFile`: Sube archivos adjuntos a `UploadFiles.ashx`.
- `diffDays`: Valida fechas de despido respecto a la fecha actual (avisa si es retroactivo o futuro).

## Llamadas al Servidor (API/Backend)
- `GetValidarPermitidoSolBj4`
- `GetListarContratosFiniquitados`: Obtiene fichas desde Softland (probablemente).
- `GetContratoActivoBaja`: Obtiene detalles del contrato seleccionado.
- `SetCrearSolicitudB4J`: Endpoint transaccional principal. Recibe muchos parámetros incluyendo listas de archivos y fechas.
- `GetObtenerCausalesDespido`.

## Lógica de Negocio
- **Validación Cruzada**:
    - Si la causal es "Art.160 N3 (No concurrencia)", OBLIGA a ingresar fechas específicas de inasistencia y adjuntar archivos.
- **Alertas de Fecha**:
    - Muestra advertencias visuales (colores) si la fecha de baja es muy antigua o futura.
- **Carga de Archivos**:
    - Valida extensiones y visualiza iconos según tipo de archivo.
    - Sube archivos de forma asíncrona separado de los datos del formulario, usando un ID de transacción devuelto por `SetCrearSolicitudB4J`.

## Endpoint Externo
- Referencia a `http://angelsysdoc.com/images/esperar.png` para un spinner de carga. Esto es un riesgo (dependencia externa).

## Observaciones
- Manejo de fechas de "No Concurrencia" en un array en memoria (`dateNotConcur.fechas`) que luego se serializa a string delimitado por `;`.
