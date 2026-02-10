# Análisis de la Capa de Infraestructura

## Visión General
La capa de `Infraestructura` en este proyecto actúa principalmente como una **Capa de Acceso a Datos (Proxy/Gateway)** que intermedia entre la aplicación y una API Web (`Teamwork.WebApi`).

### Patrón de Diseño
El código sigue un patrón consistente en todos los módulos:
1.  **Colecciones (`Collections`)**: Clases estáticas que exponen métodos de negocio.
2.  **Autenticación**: Cada método comienza autenticándose mediante `Authenticate.__Authenticate()` para obtener un token.
3.  **Llamada a API**: Utiliza clases helper (`CallAPI*`) para realizar peticiones HTTP a la API, pasando el token y los parámetros necesarios.
4.  **Deserialización**: La respuesta de la API (generalmente JSON) se deserializa a objetos dinámicos `Newtonsoft.Json`.
5.  **Mapeo de Instancias**: Los objetos dinámicos se convierten a modelos fuertemente tipados utilizando clases `Instance*` (e.g., `InstanceAuth`, `InstanceFiniquitos`).
6.  **Retorno**: Se devuelve una lista de objetos de dominio.

### Componentes Principales
-   **Collections**: Contienen la lógica de orquestación (Auth -> API -> Mapping).
-   **Instances**: Responsables de mapear objetos dinámicos a modelos concretos.

### Observaciones
-   **No hay acceso directo a Base de Datos**: Esta capa no contiene `DbCondtext`, sentencias SQL ni repositorios directos. Todo el acceso a datos se delega a la API Web.
-   **Dependencias**: `Teamwork.Model`, `Teamwork.WebApi`, `Newtonsoft.Json`.

---
*Para detalles específicos de cada módulo, consulte `Collections.md`.*
