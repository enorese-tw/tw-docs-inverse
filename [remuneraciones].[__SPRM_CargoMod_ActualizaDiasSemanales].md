# [remuneraciones].[__SPRM_CargoMod_ActualizaDiasSemanales]

## Objetivo
Este procedimiento almacenado actualiza la cantidad de días semanales de trabajo asociados a una solicitud de modificación de cargo, registrando la auditoría del cambio.

## Parámetros de Entrada
| Nombre | Tipo | Descripción |
| :--- | :--- | :--- |
| `@USUARIOCREADOR` | `VARCHAR(MAX)` | Identificador del usuario que realiza la actualización. |
| `@CODIGOSOLICITUD` | `VARCHAR(MAX)` | Identificador de la solicitud (codificado en Base64). |
| `@DIAS` | `VARCHAR(MAX)` | Cantidad de días semanales a asignar (formato texto, puede requerir limpieza). |

## Parámetros de Salida
| Nombre | Tipo | Descripción |
| :--- | :--- | :--- |
| `@CODE` | `VARCHAR(MAX)` | Código de respuesta ('200' éxito). |
| `@MESSAGE` | `VARCHAR(MAX)` | Mensaje de confirmación. |

## Variables Internas
No utiliza variables internas locales.

## Llamadas Internas
- **Funciones**:
    - `[TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE]`: Para decodificar el identificador de la solicitud.

## Lógica de Cálculo
1.  **Actualización**: Ejecuta un `UPDATE` en la tabla `RM_CargosMod` para el registro decodificado.
    - Convierte el parámetro `@DIAS` a numérico, reemplazando previamente cualquier caracter vacío (aunque la lógica `REPLACE(@DIAS, '', '.')` tal como está escrita no tendría efecto práctico si la cadena vacía es el target, posiblemente intentaba reemplazar comas por puntos o similar, pero la instrucción actual busca un string vacío).
    - Actualiza `NumeroDias` y los campos de auditoría (`FechaUltimaActualizacion`, `UsuarioUltimaActualizacion`, `UltimoComentario`).
2.  **Respuesta**: Retorna `@CODE = '200'` con un mensaje de éxito.

## Tablas Afectadas
- `[remuneraciones].[RM_CargosMod]`: UPDATE (Campos `NumeroDias` y auditoría).

## Código Comentado
No se observa código comentado en este archivo.
