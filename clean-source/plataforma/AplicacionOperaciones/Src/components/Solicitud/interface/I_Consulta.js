import { json } from '../../../modules/config.js';
import { __post } from '../../../modules/ajax.js';
import { handleRenderNotification } from '../../../modules/notificacion.js';

import { renderFichaPersonal, renderContrato, renderRenovacion, renderFichaCompleta, renderLoadingTable, renderBaja, renderContratoFull, renderLoading } from '../render/R_Consulta.js';

import { handleEventFichaPersonal, handledEventContrato } from '../event/E_Consulta.js';

const config = [json][0].config;

const __APP = config.app;
const __SERVERHOST = `${config.origin}${__APP}`;

const handleConsultaFichaPersonal = (container, ficha = "", empresa = "", rut = "", filter = "", full = false) => {

    document.querySelector(container).innerHTML = renderLoading();

    __post(`${__SERVERHOST}/Solicitud/ConsultaFichaPersonal`, {
        ficha: ficha,
        empresa: empresa,
        rut: rut,
        filter: filter
    })
    .then(resolve => {

        let html = "";

        if (resolve.length > 0) {
            resolve.forEach((fichas) => {
                html += !full ? renderFichaPersonal(fichas, filter, ficha, empresa, rut) : renderFichaCompleta(fichas);
            });
        }

        document.querySelector(container).innerHTML = html;

        if (!full) {
            handleEventFichaPersonal();
        }
        
    })
    .catch(reject => {
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });
};

const handleConsultaContrato = (container, ficha = "", empresa = "", rut = "", filter = "", full = false) => {

    document.querySelector(container).innerHTML = !full ? renderLoadingTable(9) : renderLoading();

    __post(`${__SERVERHOST}/Solicitud/ConsultaContrato`, {
        ficha: ficha,
        empresa: empresa,
        rut: rut,
        filter: filter
    })
    .then(resolve => {

        let html = "";

        if (resolve.length > 0) {
            resolve.forEach((ficha) => {
                html += !full ? renderContrato(ficha) : renderContratoFull(ficha);
            });
        }

        document.querySelector(container).innerHTML = html;

        if (!full) {
            if (resolve.length > 0) {
                resolve.forEach((ficha) => {
                    const { Ficha, Empresa } = ficha;
                    handleConsultaRenovacion(`.container__add__renov__${Ficha}`, Ficha, Empresa);
                    handleConsultaBaja(`.container__add__baja__${Ficha}`, Ficha, Empresa);
                });
            }
            handledEventContrato();
        }
        
    })
    .catch(reject => {
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });
};

const handleConsultaRenovacion = (container, ficha = "", empresa = "") => {

    document.querySelector(container).innerHTML = renderLoadingTable();

    __post(`${__SERVERHOST}/Solicitud/ConsultaRenovacion`, {
        ficha: ficha,
        empresa: empresa
    })
    .then(resolve => {

        let html = "";

        if (resolve.length > 0) {
            resolve.forEach((ficha) => {
                html += renderRenovacion(ficha);
            });
        }

        document.querySelector(container).innerHTML = html;

    })
    .catch(reject => {
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });
};

const handleConsultaBaja = (container, ficha = "", empresa = "") => {

    document.querySelector(container).innerHTML = renderLoadingTable();

    __post(`${__SERVERHOST}/Solicitud/ConsultaBaja`, {
        ficha: ficha,
        empresa: empresa
    })
    .then(resolve => {

        let html = "";

        if (resolve.length > 0) {
            resolve.forEach((ficha) => {
                html += renderBaja(ficha);
            });
        }

        document.querySelector(container).innerHTML = html;

    })
    .catch(reject => {
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });
};

export { handleConsultaFichaPersonal, handleConsultaContrato, handleConsultaRenovacion };