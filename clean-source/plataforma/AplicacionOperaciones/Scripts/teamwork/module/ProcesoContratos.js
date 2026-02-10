import { __post } from './ajax.js';

const renderLoaderProcesosAll = () => {
    return `
        
        <tr style="border-left: 5px solid rgb(200, 200, 200);" class="br-1-solid-200x3 bb-1-solid-200x3 bt-1-solid-200x3 mb-5">
            <td style="width: 10%;" class="ta-center">
                <span class="pdt-5 pdb-5 pdr-20 pdl-20 color-fx3" style="border-radius: 50px; background-color: rgb(200, 200, 200);"></span>
            </td>
            <td style="width: 50%;">
                <h1 style="font-size: 20px; height: 20px; width: 70%; background-color: rgb(200, 200, 200);" class="mt-20 color-70x3"></h1>
                <p>Creado <b>Cargando información...</b></p>
                <p class="mt-15neg">Nombre Proceso: Cargando información...<b></b></p>
                <p class="mt-15neg">Fechas de compromiso: <b>Cargando información...</b>, Con Prioridad <b>Cargando información...</b></p>
                <p>Comentarios Solicitud: Cargando información...</p>
            </td>
            <td></td>
        </tr>
    `;
};

const path = window.location.pathname;
const codigo = path.split('/')[path.split('/').length - 1];

const codigoTx = (codigo.toLowerCase() !== "solicitud" && codigo.toLowerCase() !== "" && codigo.toLowerCase() !== "contratos")
    ? codigo
    : "";

methodProcesoListarContratos(
    atob(codigoTx),
    "",
    "",
    "1-5",
    codigoTx === "" ? '#procesoscontratos tbody' : '#ownerProcesosContratos',
    codigoTx === "" ? 'all' : 'owner'
);

handleModificarNombreProceso = (e) => {
    const codigoSolicitud = e.dataset.solicitud;
    const type = e.dataset.type;
    const nombreProceso = document.getElementById('input__nombreProceso').value;
    methodModificarNombreProceso(codigoSolicitud, type, nombreProceso, "target__error__conf", "target__notify__event");
};

methodModificarNombreProceso = (codigoSolicitud, type, nombreProceso, targetError, targetSuccess) => {
    Ajax.__post(`${window.location.origin}/Proceso/ProcesoModificarNombre`, {
        codigoTransaction: codigoSolicitud,
        type: type,
        nombreProceso: nombreProceso
    })
    .then(resolve => {
        resolve.forEach(rechazo => {
            switch (rechazo.Code) {
                case "200":
                    document.getElementById(targetSuccess).innerHTML = `<span class="alert alert-success new-family-teamwork pdt-5 pdb-5 dspl-block ml-auto mr-auto mt-20" style="width: 92%;">${rechazo.Message}<span>`;
                    handleModalHidden();
                    methodProcesoListarContratos("", "", "", "1-5");
                    setTimeout(() => {
                        document.getElementById(targetSuccess).innerHTML = '';
                    }, 4000);
                    break;
                default:
                    document.getElementById(targetError).innerHTML = `<span class="alert alert-danger new-family-teamwork pdt-5 pdb-5 dspl-block">${rechazo.Message}</span>`;
                    break;
            }
        });
    })
    .catch(reject => {
    });
};

handleModalConfirmacionAnulacion = (e) => {
    handleModalShow(
        `
            <div class="ta-center">
                <h2 class="new-family-teamwork mt-50 pdt-40 color-70x3" style="font-weight: 100;">Confirmación de Rechazo</h2>
            </div>
            <div style="width: 90%;" class="m-auto" id="target__error__conf">
            </div>
            <div style="width: 90%;" class="m-auto">
                <p class="new-family-teamwork" style="font-weight: 100;">Se encuentra a un paso de rechazar el proceso: </p>
                <h3 class="new-family-teamwork mt-10neg" style="font-weight: 100; color: #f00;">${e.dataset.name}</h3>
                <p class="new-family-teamwork" style="font-weight: 100;">Indica el motivo del rechazo</p>
                <textarea class="new-family-teamwork form-control" required id="obs__rechazo"></textarea>
                <p class="new-family-teamwork" style="font-weight: 100;">Importante: Se le notificará al area correspondiente el rechazo de los documentos, antes de realizar esta acción deben estar
                    rechazados los documentos en plataforma de firma digital, con el fin de evitar que los
                    documentos sean firmados y no puedan ser eliminados por el area correspondiente.</p>
                <div class="pdb-30 pdt-30 ta-center">
                    <button class="btn btn-success color-fx3 new-family-teamwork pdt-10 pdl-20 pdr-20 pdb-10" style="font-weight: 100; border: none; border-radius: 50px;"
                            data-solicitud="${e.dataset.codigosolicitud}" data-type="${e.dataset.type}" data-input="obs__rechazo" onclick="${e.dataset.functionrechazo}(this)">
                        Confirmar Rechazo
                    </button>
                    <button class="btn btn-anulado color-fx3 new-family-teamwork pdt-10 pdl-20 pdr-20 pdb-10" style="font-weight: 100; border: none; border-radius: 50px;"
                        onclick="handleModalHidden()">
                        Cancelar
                    </button>
                </div>
            </div>

        `
    );
};

handleModalConfirmacionModificarNombre = (e) => {
    handleModalShow(
        `
            <div class="ta-center">
                <h2 class="new-family-teamwork mt-50 pdt-40 color-70x3" style="font-weight: 100;">Confirmación de Cambio de Nombre</h2>
            </div>
            <div style="width: 90%;" class="m-auto" id="target__error__conf">
            </div>
            <div style="width: 90%;" class="m-auto">
                <p class="new-family-teamwork" style="font-weight: 100;">El nombre actual del proceso es: </p>
                <h3 class="new-family-teamwork mt-10neg" style="font-weight: 100; color: #f00;">${e.dataset.name}</h3>
                <p class="new-family-teamwork" style="font-weight: 100;">Indique el nuevo nombre del proceso:</p>
                <input class="new-family-teamwork form-control" required id="input__nombreProceso" />
                <p class="new-family-teamwork" style="font-weight: 100;">Importante: al modificar el nombre del proceso, este impactara a todas las solicitudes que esten dentro del mismo.</p>
                <div class="pdb-30 pdt-30 ta-center">
                    <button class="btn btn-success color-fx3 new-family-teamwork pdt-10 pdl-20 pdr-20 pdb-10" style="font-weight: 100; border: none; border-radius: 50px;"
                            data-solicitud="${e.dataset.codigosolicitud}" data-type="${e.dataset.type}" onclick="handleModificarNombreProceso(this)">
                        Confirmar Nuevo Nombre
                    </button>
                    <button class="btn btn-anulado color-fx3 new-family-teamwork pdt-10 pdl-20 pdr-20 pdb-10" style="font-weight: 100; border: none; border-radius: 50px;"
                        onclick="handleModalHidden()">
                        Cancelar
                    </button>
                </div>
            </div>

        `
    );
}
