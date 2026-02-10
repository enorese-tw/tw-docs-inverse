# Análisis de AdministradorDeProveedores.js

## Ubicación
`FiniquitosV2/Scripts/Finiquitos/ajax/AdministradorDeProveedores.js`

## Descripción
Script encargado de cargar y visualizar la lista de proveedores en la página `AdministradorDeProveedores.aspx`.

## Funciones Principales
- `ajaxObtenerProveedores(page)`: Realiza una llamada AJAX POST para obtener los proveedores.

## Llamadas al Servidor (API/Backend)
- **URL**: `AdministradorDeProveedores.aspx/GetObtenerProveedoresProveedorService`
- **Método**: POST
- **Parámetros**: `{ }` (Vacío)
- **Respuesta Esperada**: JSON con una lista de proveedores (`data.resultado`).

## Lógica de Negocio
- Renderiza una tabla HTML con la información de cada proveedor:
    - Nombre, RUT, Tipo de Proveedor.
    - Estado (Activo/Inactivo).
    - Botones de acción (no funcionales en este script, solo visuales).
- Maneja la visualización de errores mediante `swal` (SweetAlert).

## Observaciones
- La construcción del HTML se hace concatenando strings en el cliente, lo cual es propenso a errores y difícil de mantener.
- No hay lógica de paginación o filtrado complejo en el lado del cliente, solo iteración sobre los resultados.
