# Módulo Finiquitos - Teamwork.WebApi

**Archivo**: `Teamwork.WebApi/CallAPIFiniquitos.cs`

Cubre el flujo completo de finiquitos, desde la consulta y simulación hasta el pago y firma.

## Consultas y Gestión Principal (`finiquitos/`)

- **Consultas**: `ConsultaFiniquitos`, `ConsultaComplementos`, `ConsultaSimulaciones`.
- **Operaciones sobre Finiquito**:
    - `AnularFiniquito`: Anula un finiquito.
    - `ValidarFiniquito`: Valida el cálculo o estado.
    - `HistorialFiniquitos`: Obtiene la traza de cambios.
    - `TerminarFiniquito`, `FirmarFiniquito`: Cierre del proceso.

## Logística de Documentos

Métodos para gestionar el envío físico y legalización de documentos:
- `GestionEnvioRegiones`, `GestionEnvioSantiagoNotaria`, `GestionEnvioSantiagoParaFirma`.
- `GestionRecepcionRegiones`, `GestionRecepcionLegalizacion`, etc.

## Gestión de Pagos (TEF, Vale Vista, Cheque)

- **`__FiniquitosConsultaDatosTEF`**: Obtiene datos para transferencia.
- **`__FiniquitosSolicitudTEF`**: Solicita pago por transferencia. Params: `Banco`, `NumeroCta`, `GastoAdm`.
- **`__FiniquitosSolicitudValeVista`**, **`__FiniquitosSolicitudCheque`**.
- **`__FiniquitosConfirmarProcesoPago`**: Confirma que el pago se ha procesado.
- **`__FiniquitosPagarFiniquito`**: Ejecuta la acción de pago final.

## Complementos (Haberes y Descuentos Adicionales)

Permite agregar items al cálculo del finiquito.
- `ComplementoListarHaberes`, `ComplementoListarDescuento`.
- `ComplementoAgregarHaber`, `ComplementoAgregarDescuento`.
- `ComplementoEliminarHaber`, `ComplementoEliminarDescuento`.

## Reversiones

Permite retroceder el estado del finiquito en caso de error.
- `RevertirValidacion`, `RevertirGestionEnvio`, `RevertirLegalizacion`.
- `RevertirSolicitudPago`, `RevertirConfirmacion`, `RevertirEmisionPago`.
- `RevertirValidacionFinanzas`.

**Estructura de Datos**:
La mayoría de los métodos reciben un `UsuarioCreador` y un `Codigo` (Identificador del finiquito), más parámetros opcionales de observación o filtros.
