# WebApi (Teamwork.WebApi)

## Objetivo
Esta capa es la encargada de realizar la **comunicación HTTP de bajo nivel** con los servicios REST backend. Su responsabilidad es construir las peticiones HTTP, establecer las cabeceras (incluyendo autenticación) y retornar la respuesta cruda (string JSON) a la capa superior (`Teamwork.Infraestructura`).

## Estructura
- **`HelperCallAPI.cs`**: Clase central que contiene la lógica para instanciar `HttpWebRequest`, configurar verbos (POST), headers y leer el stream de respuesta.
    - Contiene métodos estáticos específicos por módulo (ej. `__CallAPIFiniquitos`, `__CallAPIAsistencia`) que apuntan a rutas base diferentes.
- **`CallAPI[Modulo].cs`** (ej. `CallAPIFiniquitos.cs`): Clases fachadas que convierten los parámetros de los métodos en un string JSON.

## Puntos Importantes
- **Construcción Manual de JSON**: Un hallazgo crítico es que **los JSON de los request se construyen concatenando strings** manualmente, en lugar de usar una librería de serialización (como Newtonsoft).
    - Código observado: `"{ UsuarioCreador: '" + usuarioCreador + "', ... }"`
    - Esto implica un riesgo de errores de formato si los parámetros contienen caracteres especiales que rompan el JSON (como comillas simples).
- **Uso de `HttpWebRequest`**: Utiliza la clase antigua `HttpWebRequest` en lugar de la más moderna `HttpClient`.
- **Manejo de Errores**: Captura `WebException`. Si el error es de protocolo, intenta leer el cuerpo de la respuesta de error y retornarlo como si fuera una respuesta exitosa, lo que delega el manejo del error de negocio a la capa superior.
- **Configuración**: La URL base del API se obtiene de `WebConfigurationManager.AppSettings["UrlApiRest"]`.

## Dependencias
- `System.Net`: Para las clases de red.
- `System.Web.Configuration`: Para leer el Web.config.
- No utiliza librerías externas para su funcionamiento core (ni siquiera Json.NET para el request, solo para el response en capas superiores).
