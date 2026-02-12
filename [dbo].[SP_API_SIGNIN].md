# [dbo].[SP_API_SIGNIN]

## Descripción
El procedimiento almacenado `[dbo].[SP_API_SIGNIN]` tiene como objetivo autenticar a un usuario para el uso de la API. Realiza validaciones de existencia del usuario, vigencia de acceso y correspondencia de la contraseña proporcionada.

## Parámetros
| Nombre | Tipo | Descripción |
| :--- | :--- | :--- |
| `@USERNAME` | `VARCHAR(MAX)` | Nombre de usuario que intenta iniciar sesión. |
| `@PASSWORD` | `VARCHAR(MAX)` | Contraseña del usuario en formato Base64. |

## Tablas Afectadas
| Tabla | Operación | Descripción |
| :--- | :--- | :--- |
| `[dbo].[TW_APIUsuarios]` | SELECT | Se consulta para validar la existencia del usuario, su vigencia y verificar la contraseña. |

## Dependencias
*   `[dbo].[FN_BASE64_DECODE]`: Función utilizada para decodificar la contraseña recibida.
*   `HASHBYTES`: Función de sistema utilizada para hashear la contraseña con el algoritmo SHA2_512.

## Lógica del Procedimiento
1.  **Inicio de Transacción**: Se inicia una transacción para asegurar la integridad de las operaciones.
2.  **Validación de Existencia**: Verifica si existe algún registro en `[dbo].[TW_APIUsuarios]` que coincida con el `@USERNAME`.
    *   Si no existe, retorna un error 401 ("Usuario no autorizado").
3.  **Validación de Vigencia**: Si el usuario existe, verifica si la fecha actual (`GETDATE()`) se encuentra dentro del rango `ValidoDesde` y `ValidoHasta` definido para ese usuario.
    *   `ValidoHasta` se trata como '9999-31-12' si es `NULL`.
    *   Si la fecha no es válida, retorna un error 401 ("Usuario no autorizado, debido a fechas de autorización de acceso a API").
4.  **Validación de Contraseña**: Si el usuario es vigente, se procede a verificar la contraseña.
    *   Decodifica `@PASSWORD` usando `[dbo].[FN_BASE64_DECODE]`.
    *   Genera un hash SHA2_512 de la contraseña decodificada.
    *   Compara este hash con el campo `Password` almacenado en la base de datos.
    *   Si coinciden (y revalidando la fecha), retorna éxito (Código 200).
    *   Si no coinciden, retorna un error 401 ("Usuario no autorizado, debido a contraseña incorrecta").
5.  **Manejo de Errores**: Todo el proceso está envuelto en un bloque `TRY...CATCH`. Si ocurre un error inesperado, se hace rollback de la transacción y se retorna un código 500 con el mensaje de error del sistema.

## Retorno
El procedimiento retorna un conjunto de datos (SELECT) con las siguientes columnas, dependiendo del resultado:

### Éxito
| Code | Message |
| :--- | :--- |
| 200 | Autorizado |

### Error de Autenticación (Usuario no encontrado)
| Code | Message | Exception |
| :--- | :--- | :--- |
| 401 | Usuario no autorizado | Unauthorized |

### Error de Vigencia (Fechas inválidas)
| Code | Message | Exception |
| :--- | :--- | :--- |
| 401 | Usuario no autorizado, debido a fechas de autorización de acceso a API | Unauthorized |

### Error de Contraseña
| Code | Message | Exception |
| :--- | :--- | :--- |
| 401 | Usuario no autorizado, debido a contraseña incorrecta | Unauthorized |

### Error del Servidor
| Code | Message | Exception |
| :--- | :--- | :--- |
| 500 | Ha ocurrido un problema inesperado en el servidor, intentelo nuevamente más tarde | *<Mensaje de error del sistema>* |
