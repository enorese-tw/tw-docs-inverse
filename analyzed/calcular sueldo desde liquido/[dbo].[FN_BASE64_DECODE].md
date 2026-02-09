# Documentación de la Función: `[dbo].[FN_BASE64_DECODE]`

## Objetivo
Esta función escalar tiene como objetivo descodificar una cadena de texto en formato **Base64** y devolver su representación original en texto plano.

## Parámetros de Entrada

| Parámetro | Tipo | Descripción |
| :--- | :--- | :--- |
| `@encoded_text` | VARCHAR(8000) | La cadena de texto codificada en Base64 que se desea descifrar. |

## Variables Internas

| Variable | Tipo | Descripción |
| :--- | :--- | :--- |
| `@output` | VARCHAR(8000) | Almacena la cadena resultante descodificada. |
| `@block_start` | INT | Índice para iterar sobre la cadena codificada (bloques de 4 caracteres). |
| `@encoded_length` | INT | Longitud de la cadena de entrada. |
| `@decoded_length` | INT | Longitud estimada de la cadena descodificada. |
| `@mapr` | BINARY(122) | Mapa binario inverso para convertir caracteres ASCII a sus valores Base64 (índice 0-63). |

## Lógica de Cálculo

1.  **Limpieza de Entrada**:
    *   Se eliminan espacios en blanco, tabulaciones y saltos de línea de la cadena de entrada.
2.  **Validación**:
    *   Se verifica si la cadena contiene caracteres inválidos (fuera del alfabeto Base64). Si es así, intenta retornar un error forzado (cast de string a int).
3.  **Inicialización del Mapa (`@mapr`)**:
    *   Se construye un mapa binario donde la posición del byte corresponde al valor ASCII del carácter, y el valor del byte es el índice Base64 correspondiente.
4.  **Decodificación por Bloques**:
    *   Se calcula la longitud decodificada preliminar (`@encoded_length / 4 * 3`).
    *   Se itera sobre la cadena en bloques de 4 caracteres.
    *   Cada bloque de 4 caracteres Base64 se convierte en 3 bytes de datos originales:
        *   Se buscan los valores Base64 usando el mapa `@mapr`.
        *   Se realizan operaciones de desplazamiento de bits (multiplicación por potencias de 2) para reconstruir los 24 bits originales.
        *   Se concatenan al `@output`.
5.  **Manejo de Padding**:
    *   Se ajusta la longitud final (`@decoded_length`) si la cadena termina en `==` (sobran 2 bytes) o `=` (sobra 1 byte).
6.  **Retorno**:
    *   Se devuelve la subcadena izquierda de `@output` con la longitud correcta (`@decoded_length`).

## Retorno
*   Devuelve un `VARCHAR(MAX)` con el texto descodificado.
