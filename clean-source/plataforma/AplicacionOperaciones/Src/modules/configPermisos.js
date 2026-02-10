let permisos = {};

permisos.button = [
    {
        Key: 'OptAnularFiniquito',
        Clase: 'btn-danger color-fx3 mt-10 mb-10 ml-auto mr-auto',
        Name: 'Anular Finiquito',
        Resource: 'delete.svg',
        Masive: true,
        EventMasive: ''
    },
    {
        Key: 'OptRevertirValidacion',
        Clase: 'btn-anulado color-fx3 mt-10 mb-10 ml-auto mr-auto',
        Name: 'Revertir Validación',
        Resource: 'revertir.svg',
        Masive: true,
        EventMasive: 'button__revval__finiquito'
    },
    {
        Key: 'OptRevertirGestionEnvio',
        Clase: 'btn-anulado color-fx3 mt-10 mb-10 ml-auto mr-auto',
        Name: 'Revertir Gestión de Envio',
        Resource: 'revertir.svg',
        Masive: true,
        EventMasive: ''
    },
    {
        Key: 'OptRevertirLegalizacion',
        Clase: 'btn-anulado color-fx3 mt-10 mb-10 ml-auto mr-auto',
        Name: 'Revertir Legalización',
        Resource: 'revertir.svg',
        Masive: true,
        EventMasive: ''
    },
    {
        Key: 'OptRevertirSolicitudPago',
        Clase: 'btn-anulado color-fx3 mt-10 mb-10 ml-auto mr-auto',
        Name: 'Revertir Solicitud de Pago',
        Resource: 'revertir.svg',
        Masive: true,
        EventMasive: ''
    },
    {
        Key: 'OptRevertirConfirmacion',
        Clase: 'btn-anulado color-fx3 mt-10 mb-10 ml-auto mr-auto',
        Name: 'Revertir Confirmación',
        Resource: 'revertir.svg',
        Masive: true,
        EventMasive: ''
    },
    {
        Key: 'OptRevertirEmisionPago',
        Clase: 'btn-anulado color-fx3 mt-10 mb-10 ml-auto mr-auto',
        Name: 'Revertir Emisión de Pago',
        Resource: 'revertir.svg',
        Masive: true,
        EventMasive: ''
    },
    {
        Key: 'OptCaratulaFiniquito',
        Clase: 'btn-danger color-fx3 mt-10 mb-10 ml-auto mr-auto',
        Name: 'Caratula Finiquito',
        Resource: 'pdf.svg',
        Masive: false,
        EventMasive: ''
    },
    {
        Key: 'OptDocFiniquito',
        Clase: 'btn-danger color-fx3 mt-10 mb-10 ml-auto mr-auto',
        Name: 'Documento Finiquito',
        Resource: 'pdf.svg',
        Masive: false,
        EventMasive: ''
    },
    {
        Key: 'OptHistorialFiniquito',
        Clase: 'btn-create color-fx3 mt-10 mb-10 ml-auto mr-auto',
        Name: 'Historial Finiquito',
        Resource: 'reloj.svg',
        Masive: false,
        EventMasive: ''
    },
    {
        Key: 'OptSolTef',
        Clase: 'btn-teamwork color-fx3 mt-10 mb-10 ml-auto mr-auto',
        Name: 'Solicitud de Transferencia',
        Resource: 'tef.svg',
        Masive: true,
        EventMasive: 'button__soltef__finiquitos'
    },
    {
        Key: 'OptRecepcionFiniquitoRegiones',
        Clase: 'btn-success mt-10 mb-10 ml-auto mr-auto',
        Name: 'Recepcionar Finiquito Legalizado Regiones',
        Resource: 'recibir.svg',
        Masive: true,
        EventMasive: 'button__reclegreg__finiquitos'
    },
    {
        Key: 'OptRecepcionFiniquitoNotaria',
        Clase: 'btn-success mt-10 mb-10 ml-auto mr-auto',
        Name: 'Recepcionar Finiquito Legalizado Notaria',
        Resource: 'recibir.svg',
        Masive: true,
        EventMasive: 'button__reclegnot__finiquitos'
    },
    {
        Key: 'OptRecepcionFiniquitoStgoFirma',
        Clase: 'btn-success mt-10 mb-10 ml-auto mr-auto',
        Name: 'Recepcionar Firma Trabajador',
        Resource: 'recibir.svg',
        Masive: true,
        EventMasive: 'button__recstgofirma__finiquitos'
    },
    {
        Key: 'OptSolCheque',
        Clase: 'btn-teamwork color-fx3 mt-10 mb-10 ml-auto mr-auto',
        Name: 'Solicitar Cheque',
        Resource: 'cheque.svg',
        Masive: true,
        EventMasive: 'button__solchq__finiquitos'
    },
    {
        Key: 'OptSolValeVista',
        Clase: 'btn-teamwork color-fx3 mt-10 mb-10 ml-auto mr-auto',
        Name: 'Solicitar Vale Vista',
        Resource: 'valevista.svg',
        Masive: true,
        EventMasive: 'button__solvv__finiquitos'
    },
    {
        Key: 'OptConfirmarFiniquito',
        Clase: 'btn-warning color-fx3 mt-10 mb-10 ml-auto mr-auto',
        Name: 'Confirmar Finiquito',
        Resource: 'aprobar.svg',
        Masive: true,
        EventMasive: ''
    },
    {
        Key: 'OptPagarFiniquito',
        Clase: 'btn-success mt-10 mb-10 ml-auto mr-auto',
        Name: 'Pagar Finiquitos',
        Resource: 'aprobar.svg',
        Masive: true,
        EventMasive: ''
    },
    {
        Key: 'OptCrearComplemento',
        Clase: 'btn-teamwork color-fx3 mt-10 mb-10 ml-auto mr-auto',
        Name: 'Crear Complemento',
        Resource: 'rompecabezas.svg',
        Masive: false,
        EventMasive: ''
    },
    {
        Key: 'OptValidarFiniquito',
        Clase: 'btn-warning color-fx3 mt-10 mb-10 ml-auto mr-auto',
        Name: 'Validar Finiquito',
        Resource: 'aprobar.svg',
        Masive: true,
        EventMasive: 'button__val__finiquito'
    },
    {
        Key: 'OptGestionEnvioFiniquito',
        Clase: 'btn-info color-fx3 mt-10 mb-10 ml-auto mr-auto',
        Name: 'Gestionar Envio Finiquito',
        Resource: 'enviar.svg',
        Masive: true,
        EventMasive: ''
    },
    {
        Key: 'OptEnvioLegalizacion',
        Clase: 'btn-success color-fx3 mt-10 mb-10 ml-auto mr-auto',
        Name: 'Enviar a Legalización',
        Resource: 'enviar.svg',
        Masive: true,
        EventMasive: ''
    },
    {
        Key: 'OptRecepcionLegalizacion',
        Clase: 'btn-success color-fx3 mt-10 mb-10 ml-auto mr-auto',
        Name: 'Recepcionar Finiquito Legalizado',
        Resource: 'recibir.svg',
        Masive: true,
        EventMasive: 'button__recleg__finiquitos'
    },
    {
        Key: 'OptActualizarMontoAdministrativo',
        Clase: 'btn-warning color-fx3 mt-10 mb-10 ml-auto mr-auto',
        Name: 'Actualizar Gasto Administrativo',
        Resource: 'editar.svg',
        Masive: true,
        EventMasive: ''
    },
    {
        Key: 'OptLiquidacionesSueldo',
        Clase: 'btn-danger color-fx3 mt-10 mb-10 ml-auto mr-auto',
        Name: 'Liquidaciones de Sueldo',
        Resource: 'pdf.svg',
        Masive: false,
        EventMasive: ''
    }
];

export { permisos };