# Documentación del Procedimiento Almacenado: `[remuneraciones].[__SPRM_CargoMod_ActualizaGratificacion]`

## Objetivo
Este procedimiento almacenado se encarga de **actualizar la configuración de gratificación** de una solicitud de modificación de cargo ("Cargo Mod"). Dependiendo del tipo de gratificación seleccionada, actualiza los indicadores correspondientes y, si es necesario, gestiona los registros de gratificación convenida y recalcula el sueldo base.

## Parámetros de Entrada

| Parámetro | Tipo | Descripción |
| :--- | :--- | :--- |
| `@USUARIOCREADOR` | VARCHAR(MAX) | ID del usuario que realiza la acción (se asume que puede ser usado para auditoría). |
| `@CODIGOSOLICITUD` | VARCHAR(MAX) | Código de la solicitud de modificación de cargo (codificado en Base64). |
| `@GRATIFICACION` | VARCHAR(MAX) | Valor de la gratificación (usado en gratificación convenida 'PCC'). |
| `@TYPEGRATIF` | VARCHAR(MAX) | Tipo de gratificación seleccionada ('L', 'CG', 'CC', 'GP', 'PCC'). |
| `@OBSERVACIONES` | VARCHAR(MAX) | Observaciones o comentarios sobre la actualización. |
| `@CODE` | VARCHAR(MAX) OUTPUT | Código de retorno de la operación (ej. '200'). |

## Variables Internas

| Variable | Tipo | Descripción |
| :--- | :--- | :--- |
| `@NEWSUELDO` | FLOAT | Nuevo sueldo base calculado. |
| `@MESSAGEOUT` | VARCHAR(MAX) | Mensaje de salida de procedimientos internos. |
| `@TYPESUELDO` | VARCHAR(MAX) | Tipo de entrada de sueldo ('SB' o 'SL'). |
| `@SUELDO` | FLOAT | Sueldo líquido auxiliar almacenado en la solicitud. |
| `@LIQUIDO` | FLOAT | Sueldo líquido calculado verificación. |

## Lógica de Procesamiento

1.  **Inicio de Transacción**: Se inicia una transacción para asegurar la integridad de los datos.
2.  **Actualización según Tipo de Gratificación (`@TYPEGRATIF`)**:
    *   **Legal ('L') o Convencional Garantizada ('CG')**:
        *   Actualiza `[remuneraciones].[RM_CargosMod]` indicando que NO es pactada ni costo cero (`GratifCC = 'N'`).
    *   **Costo Cero ('CC')**:
        *   Actualiza `[remuneraciones].[RM_CargosMod]` indicando que NO es pactada pero SI es costo centro (`GratifCC = 'S'`).
    *   **Gratificación Pactada ('GP')**:
        *   Actualiza `[remuneraciones].[RM_CargosMod]` indicando que SI es pactada, y NO es costo cero.
    *   **Pactada con Cliente ('PCC')**:
        *   Verifica si ya existe una gratificación convenida vigente (`Estado = 'VIG'`) en `[remuneraciones].[RM_GratificacionConvenida]`.
        *   **Si NO existe**:
            *   Actualiza `[remuneraciones].[RM_CargosMod]` (Pactada = 'S').
            *   Inserta un nuevo registro en `[remuneraciones].[RM_GratificacionConvenida]` con el monto y observaciones.
        *   **Si EXISTE**:
            *   Actualiza `[remuneraciones].[RM_CargosMod]`. Nota: Si el tipo de sueldo no es 'SB', establece provisionalmente el sueldo base (aunque `@NEWSUELDO` podría ser NULL en este punto).
            *   Actualiza el registro existente en `[remuneraciones].[RM_GratificacionConvenida]` con el nuevo valor y vigencia.
3.  **Recálculo de Sueldo Base**:
    *   Obtiene el tipo de sueldo (`TypeSueldoInput`) y el líquido auxiliar (`AuxSueldoLiquido`) de la solicitud.
    *   **Si el tipo es Sueldo Líquido ('SL')**:
        *   Ejecuta `[remuneraciones].[__SPRMINT_CargoMod_SueldoFromLiquido]` para calcular el sueldo base a partir del líquido deseado.
        *   Actualiza el `SueldoBase` en `[remuneraciones].[RM_CargosMod]` con el valor calculado.
        *   **Verificación**:
            *   Calcula el líquido resultante consultando `[remuneraciones].[View_HaberesEstructura]`.
            *   Si hay una diferencia mayor a 1 peso con el líquido esperado, re-ejecuta el cálculo de sueldo (con parámetro de ajuste) y actualiza nuevamente el `SueldoBase`.
4.  **Finalización**:
    *   Establece `@CODE = '200'`.
    *   Confirma la transacción (`COMMIT`).
    *   En caso de error, realiza `ROLLBACK`.

## Tablas afectadas, si es que hay y con que operaciones

*   `[remuneraciones].[RM_CargosMod]` (UPDATE): Actualiza banderas de gratificación, comentarios, fechas de actualización y sueldo base.
*   `[remuneraciones].[RM_GratificacionConvenida]` (INSERT, UPDATE): Gestiona los registros de gratificaciones convenidas asociadas a la solicitud.
