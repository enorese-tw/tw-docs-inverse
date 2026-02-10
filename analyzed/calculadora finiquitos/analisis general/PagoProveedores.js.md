# Análisis de PagoProveedores.js

## Ubicación
`FiniquitosV2/Scripts/Finiquitos/ajax/PagoProveedores.js`

## Descripción
Módulo complejo para la generación masiva o individual de cheques para proveedores. Maneja un proceso de "Sesión de Trabajo" (InitProcess/CloseProcess).

## Funciones Principales
- `ajaxValidateInitProcess`, `ajaxInitProcess`, `ajaxCloseProcess`: Manejo del ciclo de vida de la generación de cheques (probablemente usa tablas temporales en BD).
- `ajaxAgregarProveedor`: Crea nuevos proveedores.
- `ajaxCargaChequeProveedores`: Guarda los datos de un cheque a generar.
- `ajaxKnowChequesInProcess`: Consulta el estado actual de los cheques en la "cola" de procesamiento.

## Llamadas al Servidor (API/Backend)
- `PagoProveedores.aspx/SetInsertarProveedorService`
- `PagoProveedores.aspx/GetMontoCifra`: Servicio curioso que convierte montos numéricos a texto (e.g. "Cien mil pesos") para el cheque.
- `PagoProveedores.aspx/GetObtenerProveedorService`
- `PagoProveedores.aspx/GetObtenerCorrelativoDisponibleProveedoresService`: Obtiene el siguiente número de cheque disponible por empresa.
- `PagoProveedores.aspx/SetTMP...`: Varios servicios con prefijo `TMP`, indicando manipulación de tablas temporales por sesión/usuario.

## Lógica de Negocio
- **Validación de Relación Empresa-Banco**: Verifica si hay cheques disponibles para la empresa seleccionada.
- **Conversión de Montos**: Delega al servidor la conversión de número a letras (`GetMontoCifra`).
- **Estados de Proceso**: Bloquea/Desbloquea la UI dependiendo de si hay un proceso de generación de cheques abierto.
- **Cheque Nominativo**: Opción para marcar cheques como nominativos o no.

## Observaciones
- Lógica de estado muy acoplada a la sesión del servidor (TMP tables).
- Múltiples llamadas encadenadas para refrescar la UI (crear cheque -> refrescar lista).
