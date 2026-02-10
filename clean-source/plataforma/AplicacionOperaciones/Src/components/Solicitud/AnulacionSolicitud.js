import { renderDOM } from '../../modules/render.js';

const RenderAnulacionSolicitud = (dataset) => {

    const { name, codigosolicitud, type } = dataset;

    return renderDOM(
        `
        <div class="ta-center">
            <h2 class="new-family-teamwork mt-50 pdt-40 color-70x3" style="font-weight: 100;">Confirmación de Rechazo</h2>
        </div>
        <div style="width: 90%;" class="m-auto" id="target__error__conf">
        </div>
        <div style="width: 90%;" class="m-auto">
            <p class="new-family-teamwork" style="font-weight: 100;">Se encuentra a un paso de rechazar la solicitud: </p>
            <h3 class="new-family-teamwork mt-10neg" style="font-weight: 100; color: #f00;">${name}</h3>
            <p class="new-family-teamwork" style="font-weight: 100;">Indica el motivo del rechazo</p>
            <textarea class="new-family-teamwork form-control" required id="obs__rechazo"></textarea>
            <p class="new-family-teamwork" style="font-weight: 100;">Importante: Se le notificará al area correspondiente el rechazo del documento, antes de realizar esta acción debe estar 
                rechazado el documento en plataforma de firma digital, con el fin de evitar que el
                documento sea firmado y no pueda ser eliminado por el area correspondiente.</p>
            <div class="pdb-30 pdt-30 ta-center">
                <button class="btn btn-success color-fx3 new-family-teamwork pdt-10 pdl-20 pdr-20 pdb-10" style="font-weight: 100; border: none; border-radius: 50px;"
                        data-solicitud="${codigosolicitud}" data-type="${type}" data-input="obs__rechazo" id="button__confirmacion__rechazo__${type}">
                    Confirmar Anulación
                </button>
                <button class="btn btn-anulado color-fx3 new-family-teamwork pdt-10 pdl-20 pdr-20 pdb-10 button__cancel__modal" style="font-weight: 100; border: none; border-radius: 50px;">
                    Cancelar
                </button>
            </div>
        </div>
        `
    );

};

export { RenderAnulacionSolicitud };