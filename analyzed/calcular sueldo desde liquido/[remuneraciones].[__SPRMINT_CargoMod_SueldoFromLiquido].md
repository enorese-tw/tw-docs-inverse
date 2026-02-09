# Documentación del Procedimiento Almacenado: `[remuneraciones].[__SPRMINT_CargoMod_SueldoFromLiquido]`

## Objetivo
Este procedimiento almacenado tiene como objetivo **calcular el Sueldo Base ("Bruto") necesario para alcanzar un Sueldo Líquido objetivo** ingresado por el usuario. Dado que el cálculo de remuneraciones incluye variables complejas (impuestos, gratificaciones, topes), este SP utiliza un **método iterativo** de aproximación para encontrar el valor correcto.

## Parámetros de Entrada

| Parámetro | Tipo | Descripción |
| :--- | :--- | :--- |
| `@SUELDO` | FLOAT | El sueldo líquido objetivo que se desea alcanzar. |
| `@CODIGOSOLICITUD` | VARCHAR(MAX) | Código de la solicitud (codificado en Base64) que contiene el identificador del cargo (`CodigoCargoMod`). |
| `@WITHAJUSTE` | BIT | Bandera (1 o 0) que indica si se debe aplicar un ajuste especial relacionado con el Seguro de Cesantía (AFC) para contratos indefinidos. |

## Parámetros de Salida

| Parámetro | Tipo | Descripción |
| :--- | :--- | :--- |
| `@NEWSUELDO` | FLOAT OUTPUT | El nuevo sueldo base calculado para llegar al líquido objetivo. |
| `@MESSAGE` | VARCHAR(MAX) OUTPUT | Mensaje de retorno, utilizado principalmente para notificar errores (ej. si el sueldo calculado es menor al mínimo legal). |

## Variables Internas Clave

| Variable | Tipo | Descripción |
| :--- | :--- | :--- |
| `@CODIGOCARGOMOD` | VARCHAR(MAX) | Identificador del cargo decodificado desde `@CODIGOSOLICITUD`. |
| `@SUELDOINPUT` | FLOAT | Almacena el sueldo líquido objetivo para comparaciones. |
| `@OTROSHABERESINPUT` | FLOAT | Variable de ajuste principal durante la iteración. Se modifica para aumentar o disminuir el total imponible. |
| `@DIF` | FLOAT | Diferencia absoluta entre el líquido calculado en la iteración actual y el objetivo (`@SUELDOINPUT`). |
| `@CONTAR` | FLOAT | Contador de iteraciones del bucle de aproximación (Límite: 30). |
| `@PORCOBLIGACIONES` | FLOAT | Factor estimado de descuentos legales (Salud + AFP + etc.) para estimar cuánto ajustar `@OTROSHABERESINPUT`. |
| `@OUTLIQUIDO` | FLOAT | Sueldo líquido resultante de una iteración de cálculo (`[remuneraciones].[__SPRMINT_CargoMod_CalcularSueldoBase]`). |
| `@AJUSTE`, `@AJUSTESUM` | FLOAT | Variables para ajustes finos relacionados con AFC en contratos indefinidos. |

## Llamadas Internas

### Procedimientos Almacenados
*   **`[remuneraciones].[__SPRMINT_CargoMod_CalcularSueldoBase]`**: Es el motor de cálculo. Se llama repetidamente dentro del bucle para verificar qué sueldo líquido resulta de los parámetros actuales.

### Funciones Escalares
*   `[TW_GENERAL_TEAMWORK].[dbo].[FN_BASE64_DECODE]`: Decodifica el parámetro de entrada `@CODIGOSOLICITUD`.
*   `[remuneraciones].[FNPorcObligaciones]`: Obtiene el porcentaje total de descuentos previsionales para estimar los ajustes.
*   `[remuneraciones].[FNEBonos]`: Obtiene la suma de bonos asociados al cargo.
*   `[TW_GENERAL_TEAMWORK].[dbo].[FN_CONVERTMONEY]`: Formatea el monto del sueldo mínimo para el mensaje de error.

### Vistas y Tablas
*   `[remuneraciones].[RM_Constantes]`: Para obtener `C001` (Sueldo Mínimo), `C002` (Tope Gratificación), `C006` y `C005` (AFC).
*   `[remuneraciones].[RM_CargosMod]`: Datos del cargo (Contrato, Empresa, AFP, Tipo de Gratificación).
*   `[remuneraciones].[View_UltimaUF]`: Valor UF actual.
*   `[remuneraciones].[RM_GratificacionConvenida]`: Valor de gratificación si es convenida.

## Lógica del Proceso (Iteración)

1.  **Inicialización**: Se decodifica el cargo y se ejecuta una primera vuelta de cálculo base (`[remuneraciones].[__SPRMINT_CargoMod_CalcularSueldoBase]`).
2.  **Bucle de Aproximación (`WHILE`)**:
    *   Se ejecuta mientras la diferencia (`@DIF`) entre el líquido calculado y el objetivo sea mayor a 1 peso y no se superen las 30 iteraciones.
    *   **Ajuste**:
        *   Si `Líquido Calculado < Objetivo`: Se **aumenta** `@OTROSHABERESINPUT`.
        *   Si `Líquido Calculado > Objetivo`: Se **disminuye** `@OTROSHABERESINPUT`.
        *   El monto del ajuste se estima usando la diferencia dividida/multiplicada por el factor de obligaciones (`@PORCOBLIGACIONES`).
    *   Se recalcula el sueldo llamando nuevamente al SP de cálculo base.
3.  **Cálculo Final**:
    *   Una vez convergido el valor (o terminado el bucle), se realizan ajustes finales considerando si el contrato paga AFC y si requiere ajuste (`@WITHAJUSTE`).
    *   Se recalcula la gratificación final (`@NEWGRATIFICACION`) según si es legal (tope 4.75 IMM) o convenida.
    *   Se determina `@NEWSUELDO` restando bonos y ajustando componentes para aislar el sueldo base.
4.  **Validación**:
    *   Si `@NEWSUELDO` es menor al ingreso mínimo mensual (`@SUELDOINGRESOMINIMO`), se retorna un mensaje de error en `@MESSAGE`.
