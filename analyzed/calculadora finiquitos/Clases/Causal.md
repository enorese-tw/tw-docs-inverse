# Causal.cs

## Descripción General
Clase DTO (Data Transfer Object) que representa una **causal de desvinculación** laboral. Almacena el artículo del Código del Trabajo chileno y la descripción asociada a la causa de término del contrato.

## Namespace
`FiniquitosV2.Clases`

## Propiedades

| Propiedad | Tipo | Descripción |
|-----------|------|-------------|
| `id` | `int` | Identificador único de la causal |
| `articulo` | `string` | Artículo del Código del Trabajo (ej: `"Art.159 N5"`) |
| `descripcion` | `string` | Texto descriptivo de la causal (ej: `"Conclusión del trabajo o servicio que dio origen al contrato"`) |
| `articuloDocumento` | `string` | Formato del artículo para uso en documentos legales |
| `descripcionDocumento` | `string` | Descripción formateada para uso en documentos legales |

## Métodos
Esta clase no tiene métodos propios. Es un DTO puro utilizado por `Causales.cs` para transportar datos.

## Uso en el Sistema
- `Causales.Listar()` retorna una `List<Causal>` con todas las causales desde la tabla `FNCAUSAL`.
- Los campos `articuloDocumento` y `descripcionDocumento` se usan en `Plantillas.cs` para generar el texto legal del finiquito.
- El `id` se usa como `IDCAUSAL` en la tabla `FN_ENCDESVINCULACION`.

## Base de Datos
- **Tabla fuente**: `FNCAUSAL` (base de datos local Finiquitos2018).
- Los datos de esta tabla son mantenidos manualmente y corresponden a las causales del Código del Trabajo chileno.

## Observaciones
- Clase sin lógica, solo estructura de datos.
- Diferencia entre `articulo`/`descripcion` y `articuloDocumento`/`descripcionDocumento` permite separar el formato interno del formato para documentos legales.
