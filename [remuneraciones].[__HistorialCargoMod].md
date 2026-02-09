# [remuneraciones].[__HistorialCargoMod]

## Objetivo
Este procedimiento almacenado recupera y formatea el historial de cambios y estados de una solicitud de modificación de cargo, presentando la información de manera legible para el usuario final (fechas formateadas, descripciones de estado y nombres de usuario resueltos).

## Parámetros de Entrada
| Nombre | Tipo | Descripción |
| :--- | :--- | :--- |
| `@CODIGOCARGOMOD` | `VARCHAR(MAX)` | Identificador de la solicitud (codificado en Base64). |

## Parámetros de Salida
No declara parámetros de salida explícitos, retorna un result set.

## Variables Internas
No utiliza variables internas locales.

## Llamadas Internas
- **Funciones**:
    - `[dbo].[FNBase64Decode]`: Utilizada para decodificar el ID de la solicitud y, condicionalmente, los identificadores de usuario (nombre/correo) si no son 'Sistema'.
- **Vistas**:
    - `[remuneraciones].[View_CargosModLog]`: Fuente principal del historial.
    - `[cargamasiva].[View_Usuarios]`: Utilizada en una subconsulta para resolver el nombre completo del usuario basándose en su login o correo.

## Lógica de Cálculo
1.  **Consulta Principal**: Selecciona registros de `View_CargosModLog` filtrando por el ID decodificado.
2.  **Formato de Fecha**: Formatea la fecha del log a 'dd-MM-yyyy HH:mm' y añade el sufijo " horas.".
3.  **Mapeo de Estados**: Traduce los códigos de estado internos a descripciones legibles:
    - 'CREATE'/'SOLIC' -> 'Solicitud creada'
    - 'PDAPROB' -> 'Pendiente validación finanzas'
    - 'APROB' -> 'Pendiente creación en Softland'
    - 'FD' -> 'Pendiente de creación en firma digital'
    - 'TER' -> 'Solicitud Terminada'
    - Otros -> 'Estado Desconocido'
4.  **Resolución de Usuario**:
    - Si el usuario es 'Sistema', lo muestra tal cual.
    - Si es diferente, intenta buscar el Nombre y Apellido en `View_Usuarios` coincidiendo por `NombreUsuario` o `Correo` (decodificando el valor del log si es necesario).
5.  **Ordenamiento**: Retorna los resultados ordenados por fecha descendente.

## Tablas Afectadas
No realiza modificaciones en tablas (solo lectura).

## Código Comentado
No se observa código comentado en este archivo.
