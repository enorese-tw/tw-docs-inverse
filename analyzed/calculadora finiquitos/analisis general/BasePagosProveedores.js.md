# Análisis de BasePagosProveedores.js

## Ubicación
`FiniquitosV2/Scripts/Finiquitos/ajax/BasePagosProveedores.js`

## Descripción
Gestiona la visualización y filtrado de pagos a proveedores en `BasePagoProveedores.aspx`.

## Funciones Principales
- `ajaxObtenerPagos(...)`: Función central que obtiene los datos de pagos mediante AJAX.
- `createAnalytics(...)`: Genera un resumen estadístico de los resultados obtenidos.
- `formatNumber`: Objeto utilitario local para formatear moneda.

## Llamadas al Servidor (API/Backend)
- **URL**: `BasePagoProveedores.aspx/GetObtenerPagosService`
- **Método**: POST
- **Parámetros**:
    - `filtrarPor`: Criterio de filtro (vacío, "FECHAS", etc.).
    - `filtro`, `filtro2`, `filtro3`: Valores de los filtros (e.g., fechas desde/hasta).

## Lógica de Negocio
- **Filtrado Dinámico**: Cambia el comportamiento de la UI según `#agregarFiltroBusqueda` (0 o FECHAS).
- **Validación de Fechas**: Valida formato y consistencia (Desde < Hasta) antes de llamar al servicio.
- **Renderizado Adaptativo**: Construye filas de tabla complejas que muestran:
    - Estado de pago (icono).
    - Datos del proveedor (RUT, Nombre, Banco).
    - Detalles del pago (Tipo, Serie Cheque, Fecha, Monto).
    - Botón de importar a Excel (solo visible al final de la carga).
- **Manejo de Errores**: Muestra SweetAlert si la llamada falla o si el servicio retorna un error lógico.

## Observaciones
- Configura `datepicker` con localización en español.
- El formateo de moneda se hace manualmente en JS (`formatNumber`).
