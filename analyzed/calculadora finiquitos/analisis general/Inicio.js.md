# Análisis de Inicio.js

## Ubicación
`FiniquitosV2/Scripts/Finiquitos/ajax/Inicio.js`

## Descripción
Script principal del Dashboard o página de inicio del sistema de finiquitos. Centraliza la gestión de estados de los finiquitos recientes y el flujo de aprobación/pago.

## Funciones Principales
- `ajaxConfirmados`: Carga tablas de finiquitos confirmados separados por medio de pago (Cheque, Transferencia, Nómina).
- `ajaxRecientes`: Muestra finiquitos recientes con filtros (RUT, Nombre, Folio).
- `ajaxInitProcess`, `ajaxValidateInitProcess`: Gestiona el "semáforo" o estado del proceso de generación masiva de cheques.
- `ajaxAnularFolio`, `ajaxRevertirConfirmacion`, `ajaxNotariadoFiniquito`, `ajaxPagarFiniquito`: Acciones directas sobre el estado del finiquito.
- `ajaxAnularPago`: Permite anular un pago ya emitido, solicitando motivo obligatorio.

## Llamadas al Servidor (API/Backend)
- `Inicio.aspx/GetFiniquitosConfirmadosService`
- `Inicio.aspx/SetTMPInitProcessChequeFiniquitosService`
- `Inicio.aspx/GetTMPChequesInProcessFiniquitoService`
- `Inicio.aspx/SetAnularFolioService`
- `Inicio.aspx/SetRevertirConfirmacionPagoService`
- `Inicio.aspx/SetNotariadoFiniquitoService`
- `Inicio.aspx/SetPagarFiniquitoService`
- `Inicio.aspx/SetAnularPagoService`

## Lógica de Negocio
- **Polimorfismo de Filtros**: El comportamiento de búsqueda cambia drásticamente según el filtro seleccionado (Nombre, RUT, Folio, Empresa), ocultando/mostrando inputs.
- **Flujo de Pago**:
    - Generación de cheques tiene un ciclo de vida: Init -> Adding Cheques -> Close (Genera archivo impresión).
    - Validación de monto antes de generar cheque/pago (`ajaxObtenerMontoCheque`/`ajaxObtenerMontoPago`).
- **Seguridad / Confirmación**:
    - Uso intensivo de `swal` (SweetAlert) para confirmar acciones destructivas (Anular, Revertir).

## Observaciones
- Tiene lógica de "Polling" o carga diferida (`setTimeout`) para la lista de recientes, probablemente para no bloquear la carga inicial de la página.
