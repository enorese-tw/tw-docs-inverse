# [remuneraciones].[__SPRM_CargoMod_ProvMargenEstructura]

## Objetivo
Este procedimiento almacenado es el motor de cálculo de costos para una solicitud de modificación de cargo. Su función principal es calcular dinámicamente las provisiones, gastos, márgenes y costos totales (Costo Empresa y Costo a Facturar), basándose en la configuración paramétrica del cliente y la empresa. Además, persiste estos cálculos en la base de datos y retorna la estructura detallada para su visualización, aplicando filtros de seguridad por usuario si corresponde.

## Parámetros de Entrada
| Nombre | Tipo | Descripción |
| :--- | :--- | :--- |
| `@USUARIOCREADOR` | `VARCHAR(MAX)` | Identificador del usuario (codificado en Base64) que realiza la consulta. Se usa para aplicar restricciones de visualización de costos sensibles. |
| `@CODIGOCARGOMOD` | `VARCHAR(MAX)` | Identificador de la solicitud de modificación de cargo. |

## Variables Internas
El procedimiento utiliza múltiples variables para manejar los cálculos y la lógica dinámica, destacando:
- `@MONTOCALCULO`, `@PROVISION`, `@COSTOEMPRESA`, `@GASTOS`: Acumuladores de montos.
- `@SQL`: Cadena para ejecución de SQL dinámico.
- `@CLIENTE`, `@__EMPRESA`: Contexto de la solicitud.
- `@__USERID`, `@__PROVMARGENRESTRINGED`: Variables para control de acceso y seguridad.
- Cursores (`CURSOR_PROVISIONES`, `CURSOR_MARGENES`, `CURSOR_MARGEN`): Para iterar sobre los conceptos configurados.

## Llamadas Internas
- **Tablas y Vistas de Configuración**:
    - `[remuneraciones].[View_ConstAsignProv]`, `View_ConstAsignCalculo`, `View_ConstCalculoTopeProv`, `View_ConstAsignMargen`: Definen qué conceptos (provisiones/márgenes) aplican y cómo se calculan.
    - `[remuneraciones].[VariablesCostoAFacturar]`, `RM_Constantes`: Costos variables adicionales.
- **Tablas de Negocio**:
    - `[remuneraciones].[RM_CargosMod]`: Se actualiza el `ValorDiario`.
    - `[remuneraciones].[RM_ProvMargen]`: Se almacenan el detalle de los cálculos.
- **Vistas de Estructura y Seguridad**:
    - `[remuneraciones].[View_CargoModCliente]`, `View_HeaderEstructura`, `View_HaberesEstructura`.
    - `[remuneraciones].[View_UserRestringido]`, `View_OrderByProvMargen`.
- **Funciones**:
    - `[dbo].[FN_CONVERTMONEY]`, `[remuneraciones].[FNConvertMoney]`: Formato de moneda.
    - `[dbo].[FNBase64Decode]`: Decodificación de usuario.

## Lógica de Cálculo
1.  **Inicialización**: Determina Cliente, Empresa y Tipo de Sueldo asociados a la solicitud.
2.  **Cálculo de Provisiones**:
    - Itera sobre las provisiones configuradas.
    - Si el cálculo es porcentual ('P'), construye y ejecuta SQL dinámico para obtener la base de cálculo (ej. Total Imponible) desde la tabla correspondiente, aplicando topes si existen (`View_ConstCalculoTopeProv`).
    - Almacena los resultados en una tabla temporal `#ProvMargen`.
3.  **Cálculo de Gastos y Márgenes**:
    - Itera sobre gastos y márgenes, verificando si algún gasto ha sido excluido explícitamente (`View_GastosMargenExc`).
    - Calcula montos fijos o porcentuales sobre bases dinámicas.
4.  **Consolidación de Costos**:
    - Suma Provisiones.
    - Calcula **Costo Empresa** = Provisiones + Gastos + Total Haberes.
    - Calcula **Margen** (puede ser sobre Costo Empresa u otra base).
    - Calcula **Costo a Facturar** = Costo Empresa + Márgenes + Costos Variables adicionales.
    - Calcula **Valor Diario** dividiendo el Costo a Facturar por la constante de días (ej. 30).
5.  **Persistencia y Retorno**:
    - Si la solicitud está **activa** (no aprobada/finalizada):
        - Actualiza el `ValorDiario` en la tabla `RM_CargosMod`.
        - Elimina cálculos antiguos y guarda los nuevos en `RM_ProvMargen`.
        - Retorna los datos desde `#ProvMargen`, ordenados según configuración.
    - Si la solicitud está **finalizada**:
        - Retorna los datos históricos desde `[remuneraciones].[View_ProvMargen]`.
6.  **Seguridad**: Verifica si el usuario (`@USUARIOCREADOR`) tiene restricciones (`View_UserRestringido`). Si es así, filtra el resultado para mostrar solo los conceptos permitidos.

## Tablas Afectadas
- `[remuneraciones].[RM_CargosMod]`: UPDATE (ValorDiario).
- `[remuneraciones].[RM_ProvMargen]`: DELETE e INSERT (Detalle de costos).

## Manejo de Errores
Implementa un bloque `TRY...CATCH` transaccional. En caso de error, hace `ROLLBACK` y retorna un dataset con el código '500' y el mensaje de error detallado.
