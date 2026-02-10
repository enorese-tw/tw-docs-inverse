# Operaciones (AplicacionOperaciones)

## Objetivo
Esta capa representa la aplicación principal para la gestión de operaciones. Funciona como una aplicación **ASP.NET MVC** que orquesta la presentación y la lógica de negocio relacionada con las operaciones de la plataforma (contratos, finiquitos, asistencia, etc.).

Su objetivo principal es servir como interfaz de usuario para los operadores y administradores, consumiendo servicios de backend y APIs para realizar las transacciones.

## Estructura
La estructura del proyecto es la típica de una aplicación MVC:

- **`Controllers/`**: Contiene la lógica de control. Gestiona las peticiones HTTP, interactúa con la capa de servicios/datos y decide qué vista renderizar.
    - Ejemplo: `OperacionesController.cs`, `AsistenciaController.cs`, `FinanzasController.cs`.
- **`Collections/`**: Contiene clases auxiliares que actúan como "Adaptadores" o "Facades" para el consumo de APIs externas o internas. Estas clases encapsulan la llamada a servicios (a menudo estáticos como `CallAPIAsistencia`) y la deserialización de respuestas JSON en objetos del modelo local.
    - Ejemplo: `CollectionsAsistencia.cs` maneja la lógica de obtener, insertar y actualizar asistencias llamando a APIs.
- **`Models/`**: Contiene los modelos de datos y ViewModels utilizados por la aplicación.
- **`Views/`**: Contiene las plantillas Razor (.cshtml) para la interfaz de usuario.
- **`Scripts/`**: Contiene lógica de cliente (JavaScript) para interactividad en el navegador.

## Puntos Importantes
- **Consumo de Servicios**: La aplicación consume servicios WCF (`ServicioAuth`, `ServicioCorreo`, `ServicioOperaciones`) para funcionalidades core como autenticación y envío de correos.
- **Interacción con APIs**: Utiliza clases estáticas (ubicadas en otras capas como `Teamwork.WebApi`) para realizar llamadas HTTP a APIs RESTful/JSON. La deserialización se realiza con `Newtonsoft.Json`.
- **Manejo de Sesión**: Hace un uso intensivo de `Session` para mantener el estado del usuario, tokens de autenticación (`@AUTHENTICATE`) y datos de configuración (`@AGENTAPP`).
- **Lógica de Negocio en Controladores**: Gran parte de la lógica de orquestación reside en los controladores, incluyendo validaciones y preparación de datos para las vistas.
- **Código Legado**: Se evidencia presencia de código comentado (bloques `#region "BLOQUE DE CODIGO COMENTADO"`) que sugiere funcionalidades antiguas de "Procesos Contratos" y "Renovaciones" que podrían estar desactivadas o en migración.
- **Dashboards**: Implementa lógica para la visualización de indicadores (KPIs) y estadísticas, diferenciando vistas para distintos perfiles (ej. KAM, Empresa).
- **Seguridad**: Implementa un sistema de headers customizados para el control de acceso a métodos y servicios (`@AUTHENTICATE`, `@CODIFICARAUTH`, etc.).

## Conexiones y Dependencias Clave
- Depende de `Teamwork.Model` para las definiciones de entidades.
- Depende de `Teamwork.WebApi` para las utilidades de consumo de API (ej. `CallAPIAsistencia`).
- Depende de servicios externos (Auth, Correo, Operaciones) referenciados como `Connected Services`.
