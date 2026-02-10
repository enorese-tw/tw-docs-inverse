# Renuncia.cs

## Descripción General
Clase DTO (Data Transfer Object) que representa una **renuncia** o solicitud de desvinculación de un trabajador. No contiene lógica de negocio.

## Namespace
`FiniquitosV2.Clases`

## Propiedades

| Propiedad | Tipo | Descripción |
|-----------|------|-------------|
| `ID` | `int` | Identificador único de la renuncia |
| `RUTTRABAJADOR` | `string` | RUT del trabajador |
| `EMPRESA` | `string` | Nombre de la empresa |
| `NOMBRETRABAJADOR` | `string` | Nombre completo del trabajador |
| `NEGOCIO` | `string` | Área de negocio |
| `ESTADO` | `string` | Estado actual de la renuncia |
| `CAUSAL` | `string` | Causal de desvinculación |
| `DESDE` | `string` | Fecha desde la cual aplica |
| `LEGALIZADA` | `string` | Si la renuncia está legalizada (sí/no) |
| `REGISTRADA` | `string` | Si la renuncia está registrada (sí/no) |

## Uso en el Sistema
- Utilizada por `RecepcionDocumentos.recibidoxkam()` para devolver la lista de renuncias recibidas por un KAM específico.
- Los datos provienen de la tabla `FN_DOCUMENTOSRECIBIDOS` con JOIN a `FN_EMPRESAS`.

## Observaciones
- Clase sin lógica, solo estructura de datos.
- A pesar del nombre "Renuncia", se usa para representar documentos de recepción de renuncias en general.
