import { handleModalShow } from '../../../modules/modal.js';
import { json } from '../../../modules/config.js';

import { handleConsultaFichaPersonal, handleConsultaContrato } from '../interface/I_Consulta.js';

const config = [json][0].config;

const __APP = config.app;
const __SERVERHOST = `${config.origin}${__APP}`;

const handleEventFichaPersonal = () => {

    const buttonConsultarFichaPersonal = document.querySelectorAll('.button__consultar__ficha');

    buttonConsultarFichaPersonal.forEach((event) => {
        event.addEventListener("click", (e) => {
            handleModalShow(
                document.querySelector(".modal-tw"),
                document.querySelector(".modal-tw .modal-tw-body"),
                `
                    <div class="container__modal__fichapersonal new-family-teamwork" style="font-weight: 100;">
                    </div>
                `
            );

            handleConsultaFichaPersonal(
                '.container__modal__fichapersonal',
                e.target.dataset.ficha,
                e.target.dataset.empresa,
                e.target.dataset.rut,
                e.target.dataset.filter,
                true
            );
        });
    });

};

const handledEventContrato = () => {

    const buttonContratoCompleto = document.querySelectorAll('.button__consulta__contrato');

    buttonContratoCompleto.forEach((event) => {
        event.addEventListener("click", (e) => {
            handleModalShow(
                document.querySelector(".modal-tw"),
                document.querySelector(".modal-tw .modal-tw-body"),
                `
                    <div class="container__modal__ficha new-family-teamwork" style="font-weight: 100;">
                    </div>
                `
            );

            handleConsultaContrato(
                '.container__modal__ficha',
                e.target.dataset.ficha,
                e.target.dataset.empresa,
                "",
                'ficha',
                true
            );
        });
    });

};

const handledEventHeaderConsulta = () => {

    const buttonConsultarDatos = document.querySelectorAll('.button__consultar__datos');

    buttonConsultarDatos.forEach((event) => {
        event.addEventListener("click", (e) => {
            let filters = "";
            const rut = document.querySelector('#rut__filter');
            const ficha = document.querySelector('#ficha__filter');
            const empresa = document.querySelector('#empresa__filter');
            
            filters = `filter=${rut.value !== "" ? `rut` : `ficha`}&${rut.value !== "" ? `rut=${rut.value}` : `ficha=${ficha.value}&empresa=${empresa.value}`}`;

            if (rut.value !== "" || (ficha.value !== "" && empresa.value !== "")) {
                window.open(`${__SERVERHOST}/Solicitud/Consulta?${filters}`);
            }
        });
    });

    const buttonChangeTypeFiltro = document.querySelectorAll('.type__filter');

    buttonChangeTypeFiltro.forEach((event) => {
        event.addEventListener("change", (e) => {
            const container = document.querySelectorAll('.container__ficha');

            switch (e.target.value) {
                case "Rut":
                    document.querySelector('.container__rut').style.display = "inline-block";
                    container.forEach((element) => {
                        element.style.display = "none";
                    });
                    break;
                case "Ficha":
                    document.querySelector('.container__rut').style.display = "none";
                    container.forEach((element) => {
                        element.style.display = "inline-block";
                    });
                    break;
            }
        });
    });

};

export { handleEventFichaPersonal, handledEventContrato, handledEventHeaderConsulta };