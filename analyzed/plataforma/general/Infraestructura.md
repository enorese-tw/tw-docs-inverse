# Infraestructura (Teamwork.Infraestructura)

## Objetivo
Esta capa actúa como un intermediario o capa de servicios de infraestructura para la aplicación. Su función principal es **orquestar la comunicación con APIs externas/internas** y transformar los datos recibidos (generalmente JSON) en objetos del modelo de dominio de la aplicación.

Aísla a la capa de presentación (`AplicacionOperaciones`) de los detalles de cómo se consumen los servicios web y cómo se construyen los objetos.

## Estructura
La capa se divide principalmente en dos grandes componentes:

- **`Collections/`**: Contiene clases estáticas que agrupan métodos por funcionalidad de negocio (ej. `CollectionFiniquitos`, `CollectionAuth`).
    - **Responsabilidad**:
        1.  Obtener tokens de autenticación (llamando a `Authenticate.__Authenticate()`).
        2.  Llamar a los métodos de la capa `Teamwork.WebApi` (`CallAPI...`) pasando los parámetros necesarios y el token.
        3.  Deserializar la respuesta JSON (`Newtonsoft.Json`).
        4.  Iterar sobre los resultados y llamar a los `Instances` para mapear los datos a objetos.
- **`Instances/`**: Contiene clases "Factory" o "Mapper" estáticas (ej. `InstanceFiniquitos`, `InstanceAuth`).
    - **Responsabilidad**: Recibir un objeto dinámico (resultado de la deserialización JSON) y mapear propiedad por propiedad a una instancia de una clase del Modelo (`Teamwork.Model`). Realiza conversiones de tipos (`ToString()`, `Convert`, etc.) y manejo de nulos.

## Puntos Importantes
- **Patrón de Diseño**: Utiliza un patrón similar a *Repository* o *Service Agent*, donde `Collections` expone métodos de negocio que encapsulan la complejidad de la llamada remota.
- **Manejo de Autenticación**: Cada llamada a servicio parece requerir un token fresco o validado, el cual se obtiene mediante `Authenticate.__Authenticate()` antes de realizar la operación principal.
- **Dependencia de WebApi**: Depende fuertemente de `Teamwork.WebApi` para realizar las peticiones HTTP reales.
- **Mapeo Manual**: El mapeo de objetos se realiza de manera manual y explícita en las clases `Instance...`, lo que permite un control fino sobre cómo se transforman los datos recibidos del API hacia el modelo local.

## Dependencias
- `Teamwork.Model`: Para instanciar los objetos de retorno.
- `Teamwork.WebApi`: Para invocar los métodos de comunicación HTTP (`CallAPI...`).
- `Newtonsoft.Json`: Para la deserialización de respuestas.
