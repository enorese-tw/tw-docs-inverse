# [remuneraciones].[__SPRM_CargoMod_ActualizaValorDiaPT]

## Objetivo
Este procedimiento almacenado actualiza los valores y parámetros de cálculo para cargos con jornada Part-Time (PT). Realiza el cálculo del sueldo base mensualizado (`SueldoBase`) dependiendo del tipo de modalidad PT (Mensual, Diario u Hora) y actualiza la descripción de los horarios.

## Parámetros de Entrada
| Nombre | Tipo | Descripción |
| :--- | :--- | :--- |
| `@USUARIOCREADOR` | `VARCHAR(MAX)` | Identificador del usuario (no utilizado en la lógica de actualización). |
| `@CODIGOSOLICITUD` | `VARCHAR(MAX)` | Identificador de la solicitud (codificado en Base64). |
| `@VALORDIARIO` | `VARCHAR(MAX)` | Valor unitario del sueldo (puede representar valor día, hora o mes según el tipo). Se le eliminan los puntos para conversión numérica. |
| `@HORASEMANALES` | `VARCHAR(MAX)` | Cantidad de horas semanales a asignar. |

## Parámetros de Salida
| Nombre | Tipo | Descripción |
| :--- | :--- | :--- |
| `@CODE` | `VARCHAR(MAX)` | Código de respuesta ('200' para éxito). |
| `@MESSAGE` | `VARCHAR(MAX)` | Mensaje de respuesta (vacío por defecto). |

## Variables Internas
| Nombre | Tipo | Descripción |
| :--- | :--- | :--- |
| `@SUELDOBASE` | `FLOAT` | Almacena el resultado del cálculo del sueldo base mensualizado. |
| `@TYPEJORNADAPT` | `VARCHAR(50)` | Tipo de cálculo PT recuperado ('PTM', 'PTD', 'PTH'). |
| `@NHORASSEMANALES` | `FLOAT` | Horas semanales convertidas a numérico. |
| `@VALORJORNADAPT` | `FLOAT` | Valor unitario convertido a numérico. |

## Llamadas Internas
- **Funciones**:
    - `[TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE]`: Para decodificar el ID de solicitud.

## Lógica de Cálculo
1.  **Pre-actualización**: Actualiza numéricamente los campos `NumeroHorasSemanalesPT` y `ValorSueldoPT` en la tabla `RM_CargosMod`, limpiando el formato del valor monetario (quitando puntos).
2.  **Recuperación de Datos**: Consulta los valores recién actualizados y el `TypeCalculoPT` de la misma tabla.
3.  **Cálculo de Sueldo Base**:
    - Si es **PTM** (Mensual): `ValorSueldoPT * 30`.
    - Si es **PTD** (Diario): Se asigna directamente `ValorSueldoPT`.
    - Si es **PTH** (Hora): `((ValorSueldoPT * NumeroHorasSemanalesPT) / 7)`.
4.  **Actualización Final**:
    - Actualiza `SueldoBase` con el valor calculado.
    - Establece `TypeSueldoInput` como 'SBPT' (Sueldo Base Part Time).
    - Reconstruye la cadena de `Horarios` insertando la cantidad de horas semanales (ej. 'JORNADA PART TIME XX HORA(S) - ...').

## Tablas Afectadas
- `[remuneraciones].[RM_CargosMod]`: UPDATE (Campos `NHorasSemanales`, `NumeroHorasSemanalesPT`, `ValorSueldoPT`, `SueldoBase`, `TypeSueldoInput`, `Horarios`).

## Código Comentado
Existe código comentado en el archivo referente a:
- Factores de multiplicación (`--* 30`) en los cálculos de PTD y PTH, indicando posibles lógicas de mensualización alternativas o antiguas.
- Una asignación anulada del campo `TypeSueldo` (`--TypeSueldo = 'D'`).
