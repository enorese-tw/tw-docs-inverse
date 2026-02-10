import { __post } from '../../modules/ajax.js';
import { handleModalShow, handleModalHidden } from '../../modules/modal.js';

import { RowSolicitud, RowSolicitudLoading, PaginationSolicitud } from '../../components/Solicitud/RowSolicitud.js';
import { RenderAnulacionSolicitud } from '../../components/Solicitud/AnulacionSolicitud.js';

const __APP = "";
const __SERVERHOST = `${window.location.origin}${__APP}`;

const methodSolicitudListarContratos = (codigoTransaction, filter, dataFilter, pagination) => {
    document.querySelector('#solicitudcontratos tbody').innerHTML = RowSolicitudLoading(2);

    __post(`${__SERVERHOST}/Solicitud/SolicitudListarContratos`, {
        codigoTransaction: codigoTransaction,
        filter: filter,
        dataFilter: dataFilter,
        pagination: pagination
    })
    .then(resolve => {
        let htmlSolicitud = "";
        let viewHtml = "";

        resolve.forEach(solicitud => {
            htmlSolicitud += RowSolicitud(solicitud, "Contratos");
        });

        document.querySelector('#solicitudcontratos tbody').innerHTML =
            htmlSolicitud;

        reloadButtonRechazadoSolicitudContratos();
    })
    .catch(reject => {

    });
};

const methodPaginationSolicitudContratos = (codigoTransaction, filter, dataFilter, pagination) => {
    document.querySelector('#pagination__solicitudcontratos').innerHTML =
        `Estamos cargando la información espere...`;

    __post(`${__SERVERHOST}/Solicitud/PaginationSolicitudContratos`, {
        codigoTransaction: codigoTransaction,
        filter: filter,
        dataFilter: dataFilter,
        pagination: pagination
    })
    .then(resolve => {
        let htmlSolicitudPagination = "";

        resolve.forEach(page => {
            htmlSolicitudPagination += PaginationSolicitud(page, "Contratos");
                
        });

        document.querySelector('#pagination__solicitudcontratos').innerHTML =
            htmlSolicitudPagination;

        reloadPaginationContratosEvent();
    })
    .catch(reject => {

    });
};

const methodAnulacionSolicitudContratos = (codigoSolicitud, type, observacion, targetError, targetSuccess) => {
    __post(`${__SERVERHOST}/Solicitud/SolicitudAnularSolicitud`, {
        codigoSolicitud: codigoSolicitud,
        type: type,
        observacion: observacion
    })
    .then(resolve => {
        resolve.forEach(rechazo => {
            switch (rechazo.Code) {
                case "200":
                    document.getElementById(targetSuccess).innerHTML = `<span class="alert alert-success new-family-teamwork pdt-5 pdb-5 dspl-block ml-auto mr-auto mt-20" style="width: 92%;">${rechazo.Message}</span>`;
                    const path = window.location.pathname;
                    const codigo = path.split('/')[path.split('/').length - 1];

                    const codigoTx = (codigo.toLowerCase() !== "solicitud" && codigo.toLowerCase() !== "" && codigo.toLowerCase() !== "contratos")
                        ? codigo
                        : "";

                    handleModalHidden(
                        document.querySelector(".modal-tw"),
                        document.querySelector(".modal-tw .modal-tw-body")
                    );
                    methodSolicitudListarContratos(atob(codigoTx), "", "", "1-5");
                    methodPaginationSolicitudContratos(atob(codigoTx), "", "", "1-5");

                    setTimeout(() => {
                        document.getElementById(targetSuccess).innerHTML = "";
                    }, 5000);
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

const path = window.location.pathname;
const codigo = path.split('/')[path.split('/').length - 1];

const codigoTx = (codigo.toLowerCase() !== "solicitud" && codigo.toLowerCase() !== "" && codigo.toLowerCase() !== "contratos")
    ? codigo
    : "";

methodSolicitudListarContratos(atob(codigoTx), "", "", "1-5");
methodPaginationSolicitudContratos(atob(codigoTx), "", "", "1-5");

document.querySelector('#button__buscarSolicitudesContratos').addEventListener("click", (e) => {
    const filter = document.getElementById(e.target.dataset.selector).value;
    const dataFilter = document.getElementById(e.target.dataset.input).value;

    methodSolicitudListarContratos("", filter, dataFilter, "1-5");
    methodPaginationSolicitudContratos("", filter, dataFilter, "1-5");
});

document.querySelector('html').addEventListener("click", (e) => {
    const classList = [...e.target.classList];
    classList.forEach(properties => {
        if (properties === "hidden-modal-tw") {
            handleModalHidden(
                document.querySelector(".modal-tw"),
                document.querySelector(".modal-tw .modal-tw-body")
            );
        }
    });
});

const loadButtonCancelModalContratos = () => {
    document.querySelector(".button__cancel__modal").addEventListener("click", (e) => {
        handleModalHidden(
            document.querySelector(".modal-tw"),
            document.querySelector(".modal-tw .modal-tw-body")
        );
    });
};

const loadButtonConfirmarRechazoContratos = () => {
    document.querySelector('#button__confirmacion__rechazo__SolicitudContratos').addEventListener("click", (e) => {
        const codigoSolicitud = e.target.dataset.solicitud;
        const type = e.target.dataset.type;
        const input = document.getElementById(e.target.dataset.input).value;

        if (input !== "") {
            methodAnulacionSolicitudContratos(codigoSolicitud, type, input, "target__error__conf", "target__notify__event__Contratos");
        }
        else {
            document.getElementById("target__error__conf").innerHTML = `<span class="alert alert-danger new-family-teamwork pdt-5 pdb-5 dspl-block">Debe indicar una observación de rechazo.</span>`;
        }

    });
};

const reloadButtonRechazadoSolicitudContratos = () => {
    const buttonRechazar = document.querySelectorAll('.button__rechazarsolicitud__Contratos');

    buttonRechazar.forEach(event => {
        event.addEventListener("click", (e) => {
            handleModalShow(
                document.querySelector(".modal-tw"),
                document.querySelector(".modal-tw .modal-tw-body"),
                RenderAnulacionSolicitud(e.target.dataset)
            );
            loadButtonConfirmarRechazoContratos();
            loadButtonCancelModalContratos();
        });
    });
};

const reloadPaginationContratosEvent = () => {
    const pagination = document.querySelectorAll('#pagination__solicitudcontratos .button__pagination__Contratos');

    pagination.forEach((event) => {
        event.addEventListener("click", (e) => {
            methodSolicitudListarContratos("", e.target.filter, e.target.dataset.dfilter, e.target.dataset.page);
            methodPaginationSolicitudContratos("", e.target.dataset.filter, e.target.dataset.dfilter, e.target.dataset.page);
        });
    });
};