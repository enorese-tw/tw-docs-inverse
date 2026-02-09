# Documentación de la Función: `[dbo].[FNBase64Encode]`

## Objetivo
Esta función escalar tiene como objetivo codificar una cadena de texto (`VARCHAR`) en formato **Base64**. Convierte los datos binarios de la cadena de entrada en una representación ASCII utilizando el conjunto de caracteres estándar de Base64.

## Parámetros de Entrada

| Parámetro | Tipo | Descripción |
| :--- | :--- | :--- |
| `@plain_text` | VARCHAR(500) | La cadena de texto original que se desea codificar. El límite es de 500 caracteres. |

## Variables Internas

| Variable | Tipo | Descripción |
| :--- | :--- | :--- |
| `@output` | VARCHAR(8000) | Almacena la cadena resultante codificada en Base64. |
| `@input_length` | INTEGER | Longitud de la cadena de entrada. |
| `@block_start` | INTEGER | Índice para iterar sobre la cadena de entrada. |
| `@partial_block_start` | INTEGER | Posición donde comienza el último bloque parcial (si la longitud no es múltiplo de 3). |
| `@partial_block_length` | INTEGER | Longitud del bloque parcial final (1 o 2 caracteres). |
| `@block_val` | INTEGER | Valor numérico combinado de los 3 bytes (o menos) del bloque actual siendo procesado. |
| `@map` | CHAR(64) | Mapa de caracteres Base64 estándar (`A-Z`, `a-z`, `0-9`, `+`, `/`). |

## Llamadas Internas (Funciones y Vistas)
Esta función **no realiza llamadas** a otras funciones, vistas o procedimientos almacenados. Es una función puramente lógica que utiliza funciones nativas de SQL Server:
*   `LEN()`: Para calcular longitud.
*   `SUBSTRING()`: Para extraer partes de la cadena.
*   `CAST()`: Para conversión de tipos.
*   `REPLICATE()`: Para rellenar caracteres nulos en el bloque final.
*   `REPLACE()`: Para manejar el padding (`=`) en el bloque final.

## Lógica de Cálculo
1.  **Inicialización**: Se define el mapa de caracteres Base64 y se inicializa la variable de salida.
2.  **Cálculo de Bloques**:
    *   Se determina la longitud total y se calcula cuántos bloques completos de 3 caracteres existen.
    *   `@partial_block_length` almacena el residuo (`% 3`) para manejar el final de la cadena si no es múltiplo de 3.
3.  **Procesamiento de Bloques Completos**:
    *   Se itera un bucle `WHILE` procesando grupos de 3 caracteres.
    *   Cada grupo de 3 caracteres se convierte a un valor binario de 24 bits (`BINARY(3)`) y luego a entero (`@block_val`).
    *   Este valor se divide en 4 particiones de 6 bits.
    *   Cada partición de 6 bits se usa como índice para buscar el carácter correspondiente en `@map`.
4.  **Procesamiento del Bloque Parcial (Padding)**:
    *   Si sobra 1 o 2 caracteres al final (`@partial_block_length > 0`), se rellenan con `CHAR(0)` para completar los 3 bytes.
    *   Se realiza un proceso similar de codificación.
    *   Se añade el carácter de padding `=` al final de la cadena resultante según corresponda (1 `=` si faltan 1 byte, 2 `=` si faltan 2 bytes). Esto se maneja reemplazando 'A' (que es índice 0) por '=' en los últimos caracteres generados.

## Retorno
*   Devuelve un `VARCHAR(MAX)` que contiene la cadena codificada en Base64.
