import { __post } from '../../modules/ajax.js';
import { json } from '../../modules/config.js';
import { handleModalShow, handleModalHidden } from '../../modules/modal.js';
import { handleRenderNotification } from '../../modules/notificacion.js';

import {
    renderClientes,
    renderHeader,
    renderLoading,
    renderModalEliminarCliente,
    renderNoCliente,
    renderLoadingContent,
    renderModalCrearCliente
} from './render/seguro.js';

const config = [json][0].config;

const __APP = config.app;
const __SERVERHOST = `${config.origin}${__APP}`;

const handleEventModal = () => {

    const buttonAceptar = document.querySelectorAll('.button__aceptar__modal__event');

    buttonAceptar.forEach((event) => {
        event.addEventListener("click", (e) => {
            switch (e.target.dataset.event) {
                case "EliminarCliente":
                    handleEliminarClientes('.container__modal__cliente', e.target.dataset.value);
                    break;
                case "CrearCliente":
                    const nameCliente = document.querySelector('#name__cliente');

                    if (nameCliente.value !== "") {
                        handleCrearClientes('.container__modal__cliente', nameCliente.value);
                    }
                    else {
                        handleRenderNotification("danger", "Debe indicar area de negocio a incorporar", ".alert__notification");
                    }
                    break;
            }
        });
    });

};

const handleEventHeaders = () => {

    const buttonBuscarCliente = document.querySelectorAll('.button__buscar__cliente');

    buttonBuscarCliente.forEach((event) => {
        event.addEventListener("click", (e) => {

            const filter = document.querySelector('#selected__filter');
            const dataFilter = document.querySelector('#data__filter');

            handleClientes('.personal', filter.value, dataFilter.value);

        });
    });

    const buttonRefreshClientes = document.querySelectorAll('.button__refresh__cliente');

    buttonRefreshClientes.forEach((event) => {
        event.addEventListener("click", (e) => {
            handleClientes('.personal');
        });
    });

    const buttonAddCliente = document.querySelectorAll('.button__add__cliente');

    buttonAddCliente.forEach((event) => {
        event.addEventListener("click", (e) => {
            handleModalShow(
                document.querySelector(".modal-tw"),
                document.querySelector(".modal-tw .modal-tw-body"),
                `
                    <div class="container__modal__cliente">
                        ${renderModalCrearCliente()}
                    </div>
                `
            );
            handleEventModal();
        });
    });

};

const handleEventClientes = () => {

    const buttonDeletedCliente = document.querySelectorAll('.button__deleted__cliente');

    buttonDeletedCliente.forEach((event) => {
        event.addEventListener("click", (e) => {
            handleModalShow(
                document.querySelector(".modal-tw"),
                document.querySelector(".modal-tw .modal-tw-body"),
                `
                    <div class="container__modal__cliente">
                        ${renderModalEliminarCliente(e.target.dataset.target)}
                    </div>
                `
            );
            handleEventModal();
        });
    });

    
};

const handleClientes = (container, filter = "", dataFilter = "") => {
    
    document.querySelector(container).innerHTML = renderLoading();

    __post(`${__SERVERHOST}/Finanzas/PersonaConsultarCliente`, {
        filter: filter,
        dataFilter: dataFilter
    })
    .then(resolve => {

        let html = "";

        if (resolve.length > 0)
        {
            resolve.forEach((cliente) => {
                html += renderClientes(cliente);
            });
        }
        else
        {
            html = renderNoCliente();
        }

        document.querySelector(container).innerHTML = html;
        handleEventClientes();

    })
    .catch(reject => {
        document.querySelector(container).innerHTML = "";
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });

};

const handleEliminarClientes = (container, cliente = "") => {

    document.querySelector(container).innerHTML = renderLoadingContent("Eliminando cliente, espere un momento");

    __post(`${__SERVERHOST}/Finanzas/PersonaEliminarCliente`, {
        cliente: cliente
    })
    .then(resolve => {

        if (resolve.length > 0) {
            resolve.forEach((result) => {

                const { Code, Message } = result;

                handleRenderNotification(Code === "200" ? "success" : "danger", Message, ".alert__notification");

                if (Code === "200") {

                    handleModalHidden(
                        document.querySelector(".modal-tw"),
                        document.querySelector(".modal-tw .modal-tw-body")
                    );

                    handleClientes('.personal');
                }
                else
                {
                    document.querySelector(container).innerHTML = renderModalEliminarCliente();
                    handleEventModal();
                }

            });
        }

    })
    .catch(reject => {
        document.querySelector(container).innerHTML = renderModalEliminarCliente();
        handleEventModal();
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });

};

const handleCrearClientes = (container, cliente = "") => {

    document.querySelector(container).innerHTML = renderLoadingContent("Creando cliente, espere un momento");

    __post(`${__SERVERHOST}/Finanzas/PersonaCrearCliente`, {
        cliente: cliente
    })
    .then(resolve => {

        if (resolve.length > 0) {
            resolve.forEach((result) => {

                const { Code, Message } = result;

                handleRenderNotification(Code === "200" ? "success" : "danger", Message, ".alert__notification");

                if (Code === "200") {

                    handleModalHidden(
                        document.querySelector(".modal-tw"),
                        document.querySelector(".modal-tw .modal-tw-body")
                    );

                    handleClientes('.personal');
                }
                else
                {
                    document.querySelector(container).innerHTML = renderModalCrearCliente(cliente);
                    handleEventModal();
                }

            });
        }

    })
    .catch(reject => {
        document.querySelector(container).innerHTML = renderModalCrearCliente(cliente);
        handleEventModal();
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });

};

const handleHeaders = (container) => {
    document.querySelector(container).innerHTML = renderHeader();
    handleEventHeaders();
};

export { handleClientes, handleHeaders };