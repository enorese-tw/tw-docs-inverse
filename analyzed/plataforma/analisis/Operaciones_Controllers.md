# Análisis de Controladores: Capa Operaciones

## 1. Visión General
Los controladores en `AplicacionOperaciones` actúan como "Orquestadores Monolíticos". No siguen el patrón MVC tradicional donde el controlador es delgado; aquí, contienen:
1.  **Lógica de Sesión y Seguridad**: Verificación manual de sesión y roles en cada Acción.
2.  **Orquestación de Servicios**: Llamadas directas a servicios WCF (`ServicioAuth`, `ServicioOperaciones`) y construcción de parámetros complejos.
3.  **Construcción de Vistas**: Inyección masiva de datos en `ViewBag` para poblar Dropdowns y Tablas en el frontend.

## 2. OperacionesController.cs (El Orquestador Principal)
Este controlador (~15,500 líneas) es el punto de entrada principal para la gestión de RRHH.

### 2.1 Dashboard Dinámico (`Index`)
El método `Index` no muestra una vista estática. Actúa como un *Dispatcher* basado en el Rol del usuario:
*   **Lógica**: `switch (Session["Profile"])`
*   **Roles Identificados**: `Coordinador Procesos`, `Auditor`, `Analista Finiquitos`.
*   **Comportamiento**: Dependiendo del rol, inyecta diferentes parciales en `ViewBag.RenderizadoDashboard...`.

### 2.2 Motor de Carga Masiva (`CargaMasiva`)
Este método es el backend del script `ajaxFunctions.js`.
*   **Ruta**: `POST /Operaciones/CargaMasiva`
*   **Entrada**:
    *   `data`: String (XML construido en el cliente).
    *   `codigoTransaccion`: Identificador único generado por JS.
    *   `plantilla`: Tipo de carga (Contrato, Asistencia, etc.).
*   **Procesamiento**:
    1.  **Sanitización**: Reemplaza caracteres especiales (`&`, `"`, `'`) para evitar romper el XML.
    2.  **Envío a Servicio**: Llama a `servicioOperaciones.SetProcesoMasivo`.
    3.  **Respuesta**: Retorna JSON con el estado del procesamiento (`TotalProcesado`, `Code`).
*   **Observación**: Actúa como un "Proxy Passthrough", recibiendo XML crudo del cliente y pasándolo al servicio SOAP.

### 2.3 Gestión de Solicitudes (`Solicitud...`)
Maneja el ciclo de vida de las solicitudes de RRHH.
*   **Métodos**: `SolicitudContrato`, `SolicitudRenovacion`, `SolicitudBajas`.
*   **Lógica**: Valida el estado de la solicitud y determina qué acciones están disponibles (ej. "Enviar a Firma", "Anular").

## 3. FinanzasController.cs (Gestión de Gastos y Proveedores)
Se especializa en el flujo financiero y destaca por su uso intensivo de catálogos.

### 3.1 Carga de Catálogos (`AgregarGastos`)
Este método demuestra el patrón de "Carga Ansiosa" (Eager Loading) de datos para la UI.
*   **Servicios Consumidos**: Múltiples llamadas secuenciales a WCF.
    *   `GetObtenerConceptoGastos`
    *   `GetObtenerEstadoGasto`
    *   `GetObtenerBancos`
    *   `GetObtenerEmpresas`
*   **Salida**: Puebla ~10 listas en `ViewBag` (`ViewBag.RenderizadoBanco`, `ViewBag.RenderizadoEmpresa`, etc.) para que la vista renderice los `<select>` HTML.

### 3.2 Administración de Periodos (`AdministrarPeriodo`)
Permite abrir/cerrar periodos contables.
*   **Lógica**: Controla qué meses están habilitados para la carga de gastos.

## 4. BajasController.cs (Finiquitos y Legal)
Maneja el flujo de desvinculación, el más sensible legalmente.

### 4.1 Generación de Documentos (`Pdf`, `GeneratePdf`)
Usa la librería **Rotativa** para convertir vistas HTML (Razor) en archivos PDF.
*   **Uso**: Generación de Cartas de Aviso y Finiquitos.

### 4.2 Trazabilidad Física (`ReaderBarcodeStage`)
Contiene lógica para la lectura de códigos de barra.
*   **Intención**: Probablemente usada para escanear documentos físicos firmados y cambiar su estado en el sistema (ej. "Finiquito Firmado Recibido").

## 5. Patrones Comunes Identificados

### 5.1 Detección de Entorno
Los controladores contienen lógica "Hardcodeada" para determinar si están en Desarrollo (Localhost), QA o Producción, afectando la construcción de URLs y parámetros de servicios.
```csharp
if (!Request.Url.AbsoluteUri.Split('/')[2].Contains("localhost")) { ... }
```

### 5.2 Dependencia de Sesión
No hay inyección de dependencias para el usuario actual. Todo se extrae de `Session["NombreUsuario"]` y `Session["CodificarAuth"]`. Si la sesión expira, el controlador falla o redirige manualmente.

### 5.3 Servicios Híbridos
Conviven llamadas a servicios SOAP (WCF con referencia de servicio) y llamadas manuales REST/SOAP usando `WebClient` o clases helper internas.
