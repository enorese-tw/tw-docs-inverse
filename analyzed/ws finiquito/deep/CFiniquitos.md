# Documentación Detallada: `CFiniquitos.cs`

## Visión General
`CFiniquitos` es una de las clases centrales del sistema. Encapsula la lógica de negocio relacionada con el **cálculo, visualización y gestión de finiquitos**. A diferencia de los componentes de Softland que son solo de lectura, este componente realiza operaciones de lectura y escritura (GET/SET) críticas para el flujo de trabajo.

## Dependencias
*   `System.Data` (DataTable)
*   `SQLServerDBHelper`: Contexto de base de datos `"TW_SA"` (Base de Datos Interna `SATW`).

## Estructura y Regiones
El código está organizado en regiones por tipo de operación (`GET`, `SET`) y por área funcional (`EST`, `OUT`, `CONSULTORA`, `GENERICOS`).

## Análisis de Lógica de Negocio

### 1. Consultas Directas (SQL Embebido) - *Puntos Críticos*
Estas operaciones evitan los Stored Procedures y acceden directamente a las tablas.

#### **Variables y Maestros**
*   `GetCargaVariable`: Obtiene variables de remuneración desde `FNVARIABLEREMUNERACION`.
*   `GetCausales`: Recupera causales de despido desde `FN_CAUSAL`.
    *   *Regla de Negocio*: Crea un campo calculado `UNIFICADO` concatenando `NUMERO + ' | ' + DESCRIPCION`.
*   `GetListarDocumentos`: Lista documentos desde `FN_Documentos`.
*   `GetOtrosHaberes` / `GetListarDescuentos`: Maestros de haberes y descuentos.

#### **Cálculo de UF (`GetUltimaUF`)**
*   **Lógica Compleja**:
    1.  Si se pasan más de 2 parámetros, busca la UF exacta para una fecha dada (comparando Año/Mes).
    2.  Si no, busca la UF del mes anterior (`DATEADD(MONTH, -1, ...)`) o la máxima fecha disponible.
*   **Query**: Consulta compleja sobre `[dbo].[HISTORIALUF]`.

#### **Encabezado de Finiquito (`GetObtenerENCFiniquito`)**
*   Recupera el estado actual de una solicitud de finiquito.
*   **Joins Críticos**: `FN_ENCDESVINCULACION` (F) + `FN_ESTADOS` (E) + `FN_EMPRESAS` (M).
*   **Filtrado**: Por `FICHATRABAJADOR`.

#### **Reglas de Excepción (`GetObtenerExcepcionAFC`)**
*   Verifica si un cliente/división tiene excepciones para el Seguro de Cesantía (AFC).
*   **Retorno**: Booleano simulado (`CASE WHEN COUNT(1) > 0 THEN 1 ELSE 0 END 'HasException'`).

### 2. Lógica de Cálculo (Stored Procedures)

La lógica pesada del cálculo de finiquitos está delegada a procedimientos almacenados. Estos métodos actúan como *wrappers*.

| Método C# | Stored Procedure | Descripción Funcional |
| :--- | :--- | :--- |
| `GetValidaContratosEnCalculo` | `SP_FN_VALIDA_CONTRATOS_EN_CALCULO` | Valida si un contrato ya está siendo procesado. |
| `GetListarContratosCalculo` | `SP_GET_FN_CONTRATOS_CALCULO` | Obtiene contratos en proceso de cálculo. |
| `GetFiniquitosConfirmados` | `SP_FN_GET_FINIQUITOS_CONFIRMADOS` | Lista finiquitos ya confirmados. |
| `GetFactorCalculo125` | `SP_GET_FN_FACTOR_CALCULO` | Obtiene factores para cálculos especiales (probablemente indemnizaciones). |

#### **Visualización de Resultados del Cálculo**
Un conjunto de métodos prefijados con `GetVisualizar...` invoca SPs (`SP_GET_CL_VISUALIZAR_*`) para mostrar el desglose del finiquito calculado:
*   `DatosTrabajador`, `Periodos`, `Contratos`, `Documentos`, `TotalDias`.
*   `DescuentosFiniquito` / `OtrosHaberesFiniquito`.
*   `DiasVacaciones` (Saldo de vacaciones).
*   `TotalesFiniquito` (Resumen final).
*   `HaberesMensualFiniquito` (Desglose mensual).

### 3. Operaciones de Escritura (SET)

Gestiona la persistencia de los datos del finiquito.

*   **Creación de Solicitud/Contrato**:
    *   `SetInsertaContratoCalculo` (`SP_SET_FN_INSERTAR_CONTRATOS_CALCULO`).
    *   `SetCrearModificarEncDesvinculacion` (`SP_INSERTAR_MODIFICAR`).

*   **Detalles del Finiquito**:
    *   `SetInsertaDiasVacaciones`
    *   `SetInsertaTotalHaber`

*   **Confirmación y Flujo**:
    *   `SetConfirmarRetiroCalculo` (`SP_SET_CL_CONFIRMAR_RETIRO`): Finaliza el cálculo.
    *   `SetAnularFolioFiniquito` (`SP_FN_SET_ANULAR_FOLIO`): Revierte un finiquito.
    *   `SetFiniquitoPagado` (`SP_SET_PG_FINIQUITO_PAGADO`): Marca como pagado.

### 4. Integración Específica por Tipo (OUT)

Existen métodos `Set*OUT` (`SetContratosOUT`, `SetHaberesYDescuentosOUT`, etc.) que invocan SPs específicos para el flujo "OUT" (`SP_FN_SET_*_OUT`). Esto confirma que, aunque la base de datos es la misma (`TW_SA`), el sistema maneja flujos diferenciados para distintos tipos de negocio (EST vs OUT) incluso al guardar datos locales.

## Observaciones Finales
*   La clase es monolítica y mezcla responsabilidades de lectura (maestros), lógica de cálculo (llamadas a SPs complejos) y persistencia.
*   La dependencia de Stored Procedures es total para la lógica crítica ("Caja Negra" en la base de datos).
*   El manejo de "OUT" como un conjunto separado de métodos y SPs sugiere una duplicidad de lógica o reglas muy específicas que no pudieron generalizarse.
