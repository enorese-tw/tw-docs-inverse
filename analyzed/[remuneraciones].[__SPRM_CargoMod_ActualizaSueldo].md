# Documentación del Procedimiento Almacenado: `[remuneraciones].[__SPRM_CargoMod_ActualizaSueldo]`

## Objetivo
Este procedimiento almacenado se encarga de **actualizar el sueldo** asociado a una solicitud de "Cargo Modificado". Permite ingresar el sueldo como **Sueldo Base (SB)** o **Sueldo Líquido (SL)**. Si se ingresa un sueldo líquido, el procedimiento calcula automáticamente el sueldo base bruto necesario para alcanzar ese líquido ("Alcance Líquido").

## Parámetros de Entrada

| Parámetro | Tipo | Descripción |
| :--- | :--- | :--- |
| `@USUARIOCREADOR` | VARCHAR(MAX) | ID del usuario que realiza la actualización. |
| `@CODIGOSOLICITUD` | VARCHAR(MAX) | Código de la solicitud de modificación de cargo (codificado en Base64). |
| `@SUELDO` | VARCHAR(MAX) | Valor del sueldo a actualizar (formato string con puntos o comas). |
| `@TYPE` | VARCHAR(10) | Tipo de actualización: 'SB' (Sueldo Base), 'SBPT' (Base Part-Time), 'SL' (Sueldo Líquido), 'SLPT' (Líquido Part-Time). |
| `@CODE` | VARCHAR(MAX) OUTPUT | Código de retorno (retorna el `@TYPE` procesado o 200). |
| `@MESSAGE` | VARCHAR(MAX) OUTPUT | Mensaje descriptivo del resultado de la operación. |

## Variables Internas

| Variable | Tipo | Descripción |
| :--- | :--- | :--- |
| `@NEWSUELDO` | FLOAT | Nuevo sueldo base calculado (cuando se ingresa líquido). |
| `@MESSAGEOUT` | VARCHAR(MAX) | Mensaje de salida de cálculo interno. |
| `@SUELDOINPUT` | FLOAT | Valor numérico del sueldo ingresado. |
| `@ALCANCELIQUIDO` | FLOAT | Sueldo líquido resultante verificado contra la estructura de haberes. |

## Lógica de Procesamiento

1.  **Inicio de Transacción**: Se inicia una transacción.
2.  **Validación**: Verifica que `@SUELDO` no esté vacío.
3.  **Procesamiento según Tipo (`@TYPE`)**:
    *   **Sueldo Base ('SB' o 'SBPT')**:
        *   Limpia el formato del sueldo y lo convierte a FLOAT.
        *   Actualiza `[remuneraciones].[RM_CargosMod]`:
            *   Establece `SueldoBase` con el valor ingresado.
            *   Limpia `SueldoLiquido`.
            *   Actualiza `TypeSueldoInput` basándose en la jornada ('F' -> 'SB', otros -> 'SBPT').
        *   Define mensaje de éxito.
    *   **Sueldo Líquido ('SL' o 'SLPT')**:
        *   Convierte el sueldo ingresado a FLOAT (`@SUELDOINPUT`).
        *   **Cálculo Inverso**: Ejecuta `[remuneraciones].[__SPRMINT_CargoMod_SueldoFromLiquido]` para obtener el `@NEWSUELDO` base necesario.
        *   **Primera Actualización**: Actualiza `[remuneraciones].[RM_CargosMod]` con el `SueldoBase` calculado y guarda el líquido esperado en `AuxSueldoLiquido`.
        *   **Verificación de Alcance**:
            *   Obtiene el líquido real resultante desde `[remuneraciones].[View_HaberesEstructura]`.
            *   Compara el líquido obtenido vs el ingresado.
        *   **Ajuste (si es necesario)**:
            *   Si la diferencia es mayor a 1 peso (positiva o negativa), re-ejecuta el cálculo inverso con parámetro de ajuste (flag 1).
            *   Actualiza nuevamente `[remuneraciones].[RM_CargosMod]` con el sueldo base ajustado.
        *   Define mensaje de éxito.
4.  **Finalización**:
    *   Establece `@CODE` (Nota: El código asigna `@TYPE` al final, sobrescribiendo '200' en algunos flujos).
    *   Confirma la transacción (`COMMIT`).
    *   Manejo de errores con `ROLLBACK`.

## Tablas afectadas, si es que hay y con que operaciones

*   `[remuneraciones].[RM_CargosMod]` (UPDATE): Actualiza sueldo base, sueldo líquido auxiliar, tipo de sueldo y metadatos de auditoría.
