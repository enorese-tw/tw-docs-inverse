# Plantillas.cs

## Descripción General
Clase que genera las **plantillas de texto para documentos legales de finiquito** en español. Contiene métodos estáticos que retornan párrafos formateados del documento de finiquito según el tipo de empresa y tipo de finiquito.

## Namespace
`FiniquitosV2.Clases`

---

## Tipos de Empresa Soportados
| Código | Empresa | Representante Legal |
|--------|---------|---------------------|
| `"EST"` | Team-Work Empresa de Servicios Transitorios S.A. | Iván Alfredo González Muñoz |
| `"OUT"` | TeamWork Outsourcing SpA | Iván Alfredo González Muñoz |

## Tipos de Finiquito
| Tipo | Descripción |
|------|-------------|
| `1` | Finiquito estándar (≤30 días de contrato o renuncia) |
| `2` | Finiquito con indemnización (>30 días, por necesidades de la empresa) |

---

## Métodos Principales

### Encabezado y Datos del Documento

#### `static string titulo()`
Retorna: `"FINIQUITO DE CONTRATO DE TRABAJO"`

#### `static string parrafo1(string tipo, string NOMBREEMPRESA, string RUCTRABAJADOR, string NOMBRETRABAJADOR, ...)`
Genera el primer párrafo del finiquito con los datos de las partes.

- Incluye nombre legal de la empresa, RUT, representante legal.
- Diferenciación EST/OUT: diferente texto legal para cada empresa.
- Diferenciación tipo 1/2: distinta redacción según plazo del contrato.

#### `static string parrafo2(string tipo, string EMPRESA, string fecha)`
Genera el segundo párrafo con la causal de desvinculación.

### Cláusulas del Finiquito

#### `static string clausula1(string tipo, string EMPRESA, string articulo, string descripcionarticulo)`
Primera cláusula: término del contrato y causal legal.

#### `static string clausula2(string tipo)`
Segunda cláusula: declaraciones del trabajador.

#### `static string clausula3(string tipo)`
Tercera cláusula: no discriminación y plazos legales.

#### `static string clausula4(string tipo)`
Cuarta cláusula: finiquito por necesidades de la empresa (solo tipo 2).

#### `static string clausula5(string tipo)`
Quinta cláusula: resolución de controversias.

### Cierre del Documento

#### `static string PARRAFOFINAL(string tipo)`
Párrafo final con lugar y fecha.

---

## Utilidades

### `static string FormatearFecha(DateTime fecha)`
Convierte una fecha a texto en español.

- Ejemplo: `DateTime(2024, 3, 15)` → `"quince de marzo de dos mil veinticuatro"`
- Usa funciones auxiliares internas: `NumeroATexto()`, `NombreMes()`.
- Maneja correctamente decenas, centenas y millares en español.

---

## Contenido Legal
Los textos generados incluyen:
- Referencias al Código del Trabajo chileno.
- Artículos 159, 160, 161 del Código del Trabajo.
- Montos en texto y número (usando `Convertidor.cs`).
- Formalidades legales chilenas de fin de relación laboral.

## Observaciones
- Los textos legales están **hardcodeados** en el código fuente.
- La clase tiene **563 líneas** compuestas principalmente de strings largos.
- No consulta ninguna base de datos — es una clase de generación de contenido puro.
- ⚠️ Si cambia la legislación laboral chilena, los textos deben actualizarse manualmente en el código.
- ⚠️ Si se agrega una nueva empresa, se deben modificar todos los métodos.
- El formato de salida es texto plano (no HTML ni RTF) que luego se integra en el documento PDF del finiquito.
