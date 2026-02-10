import { __post } from '../../modules/ajax.js';
import { handleModalShow, handleModalHidden } from '../../modules/modal.js';

import { RowSolicitud, RowSolicitudLoading, PaginationSolicitud } from '../../components/Solicitud/RowSolicitud.js';
import { RenderAnulacionSolicitud } from '../../components/Solicitud/AnulacionSolicitud.js';

const __APP = "";
const __SERVERHOST = `${window.location.origin}${__APP}`;

const methodSolicitudListarRenovaciones = (codigoTransaction, filter, dataFilter, pagination) => {
    document.querySelector('#solicitudRenovaciones tbody').innerHTML = RowSolicitudLoading(2);

    __post(`${__SERVERHOST}/Solicitud/SolicitudListarRenovaciones`, {
        codigoTransaction: codigoTransaction,
        filter: filter,
        dataFilter: dataFilter,
        pagination: pagination
    })
    .then(resolve => {
        let htmlSolicitud = "";

        resolve.forEach(solicitud => {
            htmlSolicitud += RowSolicitud(solicitud, "Renovaciones");
        });

        document.querySelector('#solicitudRenovaciones tbody').innerHTML =
            htmlSolicitud;

        reloadButtonRechazadoSolicitudRenovaciones();
    })
    .catch(reject => {

    });
};

const methodPaginationSolicitudRenovaciones = (codigoTransaction, filter, dataFilter, pagination) => {
    document.querySelector('#pagination__solicitudRenovaciones').innerHTML =
        `Estamos cargando la información espere...`;

    __post(`${__SERVERHOST}/Solicitud/PaginationSolicitudRenovaciones`, {
        codigoTransaction: codigoTransaction,
        filter: filter,
        dataFilter: dataFilter,
        pagination: pagination
    })
    .then(resolve => {
        let htmlSolicitudPagination = "";

        resolve.forEach(page => {
            htmlSolicitudPagination += PaginationSolicitud(page, "Renovaciones");
        });

        document.querySelector('#pagination__solicitudRenovaciones').innerHTML =
            htmlSolicitudPagination;

        reloadPaginationRenovacionesEvent();
    })
    .catch(reject => {

    });
};

const methodAnulacionSolicitudRenovaciones = (codigoSolicitud, type, observacion, targetError, targetSuccess) => {
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

                    const codigoTx = (codigo.toLowerCase() !== "solicitud" && codigo.toLowerCase() !== "" && codigo.toLowerCase() !== "renovaciones")
                        ? codigo
                        : "";

                    handleModalHidden(
                        document.querySelector(".modal-tw"),
                        document.querySelector(".modal-tw .modal-tw-body")
                    );

                    methodSolicitudListarRenovaciones(atob(codigoTx), "", "", "1-5");
                    methodPaginationSolicitudRenovaciones(atob(codigoTx), "", "", "1-5");

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

setTimeout(() => {
    methodSolicitudListarRenovaciones("", "", "", "1-5");
    methodPaginationSolicitudRenovaciones("", "", "", "1-5");

}, 1000);

document.querySelector('#button__buscarSolicitudesRenovaciones').addEventListener("click", (e) => {
    const filter = document.getElementById(e.target.dataset.selector).value;
    const dataFilter = document.getElementById(e.target.dataset.input).value;

    methodSolicitudListarRenovaciones("", filter, dataFilter, "1-5");
    methodPaginationSolicitudRenovaciones("", filter, dataFilter, "1-5");
});

const loadButtonCancelModalRenovaciones = () => {
    document.querySelector(".button__cancel__modal").addEventListener("click", (e) => {
        handleModalHidden(
            document.querySelector(".modal-tw"),
            document.querySelector(".modal-tw .modal-tw-body")
        );
    });
};

const loadButtonConfirmarRechazoRenovaciones = () => {
    document.querySelector('#button__confirmacion__rechazo__SolicitudRenovaciones').addEventListener("click", (e) => {
        const codigoSolicitud = e.target.dataset.solicitud;
        const type = e.target.dataset.type;
        const input = document.getElementById(e.target.dataset.input).value;

        if (input !== "") {
            methodAnulacionSolicitudRenovaciones(codigoSolicitud, type, input, "target__error__conf", "target__notify__event__Renovaciones");
        }
        else {
            document.getElementById("target__error__conf").innerHTML = `<span class="alert alert-danger new-family-teamwork pdt-5 pdb-5 dspl-block">Debe indicar una observación de rechazo.</span>`;
        }

    });
};
 
const reloadButtonRechazadoSolicitudRenovaciones = () => {
    const buttonRechazar = document.querySelectorAll('.button__rechazarsolicitud__Renovaciones');

    buttonRechazar.forEach(event => {
        event.addEventListener("click", (e) => {
            handleModalShow(
                document.querySelector(".modal-tw"),
                document.querySelector(".modal-tw .modal-tw-body"),
                RenderAnulacionSolicitud(e.target.dataset)
            );
            loadButtonConfirmarRechazoRenovaciones();
            loadButtonCancelModalRenovaciones();
        });
    });
};

const reloadPaginationRenovacionesEvent = () => {
    const pagination = document.querySelectorAll('#pagination__solicitudRenovaciones .button__pagination__Renovaciones');

    pagination.forEach((event) => {
        event.addEventListener("click", (e) => {
            methodSolicitudListarRenovaciones("", e.target.filter, e.target.dataset.dfilter, e.target.dataset.page);
            methodPaginationSolicitudRenovaciones("", e.target.dataset.filter, e.target.dataset.dfilter, e.target.dataset.page);
        });
    });
};