# Convertidor.cs

## Descripción General
Clase utilitaria que **convierte montos numéricos a texto en español** (Ej: `1500` → `"MIL QUINIENTOS"`). Se utiliza para la generación de documentos de finiquito donde los montos deben expresarse en letras.

## Namespace
`FiniquitosV2.Clases`

## Métodos Estáticos

### `static string enletras(string num)`
Método principal que convierte un número en formato string a su representación textual en español.

- **Parámetro**: `num` — Número como string (puede incluir separador decimal).
- **Retorno**: Texto en español del número.
- Ejemplo: `"1500"` → `"MIL QUINIENTOS PESOS"`

### `static string toText(double value)`
Método auxiliar interno que realiza la conversión recursiva.

- Soporta rangos desde unidades hasta millones.
- Manejo especial de centenas (100 = "CIEN", 101-199 = "CIENTO ...").
- Maneja correctamente "QUINIENTOS" (500), "SETECIENTOS" (700), "NOVECIENTOS" (900) con formas especiales.

## Lógica de Conversión
1. Separa parte entera y decimal por `","`.
2. Si tiene parte decimal, agrega `"CON {decimal}/100"`.
3. La conversión es recursiva: descompone el número en millones, miles, centenas, decenas y unidades.

## Observaciones
- ⚠️ **Código Duplicado**: La misma funcionalidad existe en `Interface.cs` (métodos `enletras` y `toText`). Ambas implementaciones son idénticas.
- La clase no tiene constructor ni estado — todos los métodos son estáticos.
- Se usa extensamente en el módulo de generación de documentos de finiquito (PDF).
- Correctamente maneja casos especiales del español como "VEINTIÚN", "VEINTIUNO", etc.
