# Análisis de PagosFiniquitos.js

## Ubicación
`FiniquitosV2/Scripts/Finiquitos/ajax/PagosFiniquitos.js`

## Descripción
Maneja la validación de documentos (cheques) en la página `PagosFiniquitos.aspx`.

## Funciones Principales
- `validaDocumento(page, numeroDocumento)`: Verifica si un número de documento es válido.
- `validaDocumentoSession(page, resultado)`: Guarda el resultado de la validación en la sesión del servidor.

## Llamadas al Servidor (API/Backend)
1. **Validación de Documento**
    - **URL**: `PagosFiniquitos.aspx/GetValidaDocumentoPagos`
    - **Parámetros**: `{ numeroDocumento: "..." }`
    - **Retorno**: JSON con resultado de validación (`VALIDACION` "0" o distinto).

2. **Persistencia de Validación**
    - **URL**: `PagosFiniquitos.aspx/validacionGuardarRegistroPago`
    - **Parámetros**: `{ resultValidaDocumento: "true"/"false" }`

## Lógica de Negocio
- Se activa al perder el foco (`blur`) del input `#numeroCheque`.
- Si la validación devuelve "0" (Éxito), llama a `validacionGuardarRegistroPago` con "true".
- Si falla, muestra el mensaje de error en `#contentValidaDocumento` y llama a `validacionGuardarRegistroPago` con "false".
