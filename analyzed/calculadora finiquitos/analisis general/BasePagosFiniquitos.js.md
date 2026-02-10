# Análisis de BasePagosFiniquitos.js

## Ubicación
`FiniquitosV2/Scripts/Finiquitos/ajax/BasePagosFiniquitos.js`

## Descripción
Gestiona la visualización y filtrado de pagos de finiquitos. Es similar a `BasePagosProveedores.js` pero enfocado en trabajadores/finiquitos.

## Funciones Principales
- `ajaxObtenerClientePagos`, `ajaxObtenerTrabajadoresPagos`: Carga combos de filtros.
- `ajaxObtenerPagos`: Obtiene la lista de pagos.
- `ajaxPrintingCheque`: (Invocado por `.btnOrdenDeImpresion`) Prepara archivo de impresión de cheque.

## Llamadas al Servidor (API/Backend)
- `BasePagosFiniquitos.aspx/GetObtenerClientePagosService`
- `BasePagosFiniquitos.aspx/GetObtenerTrabajadoresPagosService`
- `BasePagosFiniquitos.aspx/GetObtenerPagosService`
- `BasePagosFiniquitos.aspx/PrintingCheque` (Inferido por `ajaxPrintingCheque` aunque no se ve la definición explícita en el snippet, se llama en la línea 88).

## Lógica de Negocio
- **Filtros Cascada**: La interfaz cambia según el tipo de filtro seleccionado (Empresa, Trabajador, Cliente).
- **Desglose de Montos**: Muestra detalle de haberes:
    - Finiquito
    - I.A.S. (Indemnización Años Servicio)
    - Desahucio (Mes de Aviso)
    - Indemnización Voluntaria
    - Vacaciones
- **Reglas de Visualización**: Muestra "No aplica." si los montos provienen vacíos.

## Observaciones
- Uso extensivo de concatenación de HTML.
- Lógica de presentación mezclada con lógica de orquestación de llamadas.
