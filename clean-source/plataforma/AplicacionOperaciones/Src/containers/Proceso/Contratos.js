import { __post } from '../../modules/ajax.js';
import { handleModalShow, handleModalHidden } from '../../modules/modal.js';
import { __SERVER_APPLICATION } from '../../modules/config.js';

import { RowProceso, RowProcesoLoading } from '../../components/Proceso/RowProceso.js';
import { DetalleProceso } from '../../components/Proceso/DetalleProceso.js';
import { RenderAnularProceso } from '../../components/Proceso/AnularProceso.js';
import { CambiarNombre } from '../../components/Proceso/CambiarNombre.js';


const methodProcesoListarContratos = (codigoTransaction, filter, dataFilter, pagination, querySelector, view) => {
    let loader = "";

    switch (view) {
        case "all":
            loader = RowProcesoLoading(2);
            break;
        case "owner":

            break;
    }

    document.querySelector(querySelector).innerHTML = loader;

    __post(`${__SERVER_APPLICATION}/Proceso/ProcesoListarContratos`, {
        codigoTransaction: codigoTransaction,
        filter: filter,
        dataFilter: dataFilter,
        pagination: pagination
    })
    .then(resolve => {
        let htmlProceso = "";
        let viewHtml = "";

        resolve.forEach(proceso => {
            switch (view) {
                case "all":
                    viewHtml = RowProceso(proceso);
                    break;
                case "owner":
                    document.getElementById('filterProcesos').style.display = "none";
                    viewHtml = DetalleProceso(proceso);
                    break;
            }


            htmlProceso += viewHtml;

        });

        document.querySelector(querySelector).innerHTML =
            htmlProceso;
    })
    .catch(reject => {

    });
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

const methodAnulacionSolicitud = (codigoSolicitud, type, observacion, targetError, targetSuccess) => {
    __POSTData(`${__SERVER_APPLICATION}/Solicitud/SolicitudAnularSolicitud`, {
        codigoSolicitud: codigoSolicitud,
        type: type,
        observacion: observacion
    })
    .then(resolve => {
        resolve.forEach(rechazo => {
            switch (rechazo.Code) {
                case "200":
                    document.getElementById(targetSuccess).innerHTML = rechazo.Message;
                    handleModalHidden();
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

console.log(__SERVER_APPLICATION);

const handleModalConfirmacion = (e) => {
    handleModalShow(
        RenderAnularProceso(e.dataset)
    );
};

document.querySelector('html').addEventListener("click", (e) => {
    const classList = [...e.target.classList];
    classList.forEach(properties => {
        if (properties === "hidden-modal-tw") {
            handleModalHidden();
        }
    });
});