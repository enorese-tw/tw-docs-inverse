# [remuneraciones].[__SPRM_CargoMod_ActualizaWizards]

## Objetivo
Este procedimiento almacenado actualiza el estado de avance o etapa ("Wizards") de una solicitud de modificación de cargo, asegurando que no exceda un límite máximo predefinido.

## Parámetros de Entrada
| Nombre | Tipo | Descripción |
| :--- | :--- | :--- |
| `@CODIGOSOLICITUD` | `VARCHAR(MAX)` | Identificador de la solicitud (codificado en Base64). |
| `@ESTADO` | `VARCHAR(MAX)` | Valor numérico (en formato texto) que representa la nueva etapa a asignar. |
| `@USUARIOCREADOR` | `VARCHAR(MAX)` | Usuario que realiza la actualización. |

## Parámetros de Salida
No declara parámetros de salida.

## Variables Internas
| Nombre | Tipo | Descripción |
| :--- | :--- | :--- |
| `@__WIZARDS` | `NUMERIC` | Almacena el valor numérico de la etapa a actualizar. |
| `@__MAXSTAGE` | `NUMERIC` | Define el tope máximo de etapas (fijado en 7). |

## Llamadas Internas
- **Funciones**:
    - `[TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE]`: Para decodificar el ID de la solicitud.

## Lógica de Cálculo
1.  **Inicialización**: Establece `@__MAXSTAGE` en 7 y convierte `@ESTADO` a numérico en `@__WIZARDS`.
2.  **Validación de Tope**: Si el valor de `@__WIZARDS` supera a `@__MAXSTAGE`, se fuerza el valor a 7.
3.  **Actualización**: Actualiza el campo `Wizards` en la tabla `RM_CargosMod` con el valor resultante, registrando también la fecha y usuario de la actualización.

## Tablas Afectadas
- `[remuneraciones].[RM_CargosMod]`: UPDATE (Campos `Wizards`, `FechaUltimaActualizacion`, `UsuarioUltimaActualizacion`).

## Código Comentado
No se observa código comentado en este archivo.
