# Model (Teamwork.Model)

## Objetivo
Esta capa contiene la definición de las **entidades y modelos de datos** que se utilizan en toda la aplicación para transportar información entre las distintas capas (API -> Infraestructura -> Operaciones -> Vista).

Su principal función es proveer estructuras de datos fuertemente tipadas (clases POCO - Plain Old CLR Objects) que representen los conceptos del negocio.

## Estructura
La capa está organizada en directorios que espejan los módulos funcionales de la aplicación:

- **`Operaciones/`**: Modelos relacionados con procesos, solicitudes y gestión operativa.
    - Ejemplo: `Proceso.cs` (define propiedades como `Code`, `Message`, `NombreProceso`, `Estado`).
- **`Autentificacion/`**: Modelos para el flujo de login y seguridad.
    - Ejemplo: `TokenConfianza` (aunque definido en `AuthRedirect.cs`, representa las credenciales).
- **`Asistencia/`**, **`Bajas/`**, **`Finanzas/`**: Modelos específicos para cada una de estas áreas.
- **`Paginations/`**: Modelos para manejar estructuras de paginación en las vistas.

## Puntos Importantes
- **Clases POCO**: La gran mayoría de las clases son contenedores de datos simples con propiedades auto-implementadas (`get; set;`) y sin lógica de negocio compleja.
- **Uso de Strings**: Se observa un uso extensivo de `string` para la mayoría de las propiedades, incluso para fechas o booleanos (ej. `Estado`, `Creado`). Esto sugiere que el tipado fuerte se relaja para facilitar la deserialización directa desde JSON o el binding en las vistas, delegando el parseo a quien consume el dato.
- **Acoplamiento**: Es la capa más transversal, siendo referenciada por `Teamwork.Infraestructura`, `AplicacionOperaciones` y `Teamwork.Extensions`.

## Dependencias
- Prácticamente no tiene dependencias externas, lo que facilita su reutilización.
