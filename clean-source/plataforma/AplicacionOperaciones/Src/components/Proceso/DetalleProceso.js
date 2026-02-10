import { renderDOM } from '../../modules/render.js';

const DetalleProceso = (proceso) => {

    const { BorderColor, Color, Prioridad, CodigoTransaccion, NombreProceso, Creado, EjecutivoName, AsignadoTo, Comentarios } = proceso;

    return renderDOM(
        `
        <div class="new-family-teamwork" style="font-weight: 100;">
            <span class="${Color} pdt-5 pdb-5 pdr-20 pdl-20 color-fx3" style="border-radius: 50px;">${Prioridad}</span>
            <h1 style="color: rgb(70, 70, 70);"><b>${NombreProceso}</b></h1>
            <p>
               Comentarios Solicitud: ${Comentarios}
            </p>
            <div>
                <button class="btn btn-teamwork color-fx3 new-family-teamwork pdt-10 pdl-20 pdr-20 pdb-10 mb-10" style="font-weight: 100; border: none; border-radius: 50px;"
                        data-codigosolicitud="${CodigoTransaccion}" data-name="${NombreProceso}" data-type="SolicitudContratos" onclick="handleModalConfirmacionModificarNombre(this)">
                    Cambiar Nombre Proceso
                </button>
                <button class="btn btn-danger color-fx3 new-family-teamwork pdt-10 pdl-20 pdr-20 pdb-10 mb-10" style="font-weight: 100; border: none; border-radius: 50px;"
                        data-name="${NombreProceso}" data-codigosolicitud="${CodigoTransaccion}" data-type="SolicitudContratos" data-functionrechazo="handleClickAnulacionContratos"
                        onclick="handleModalConfirmacionAnulacion(this)">
                    Rechazar Proceso
                </button>
            </div>
            <div class="${BorderColor} pdl-10 pdt-10 pdb-10">
                <p class="mt-5">Creado <b>${Creado}</b></p>
                <p>Creado Por <b>${EjecutivoName}</b></p>
                <p class="mt-15neg">Ejecutivo Cuenta <b>${EjecutivoName}</b></p>
                <p class="mt-15neg">Asignado A <b>${AsignadoTo}</b></p>
            </div>
        </div>
        `
    );
};

export { DetalleProceso };