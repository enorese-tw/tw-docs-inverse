import { json } from '../../modules/config.js';
import { renderHeaderConsulta, renderBodyConsulta } from './render/R_Consulta.js';
import { handleConsultaFichaPersonal, handleConsultaContrato } from './interface/I_Consulta.js';
import { handledEventHeaderConsulta } from './event/E_Consulta.js';


const config = [json][0].config;

const params = new URLSearchParams(config.search);

const handledHeaderConsulta = (container) => {
    document.querySelector(container).innerHTML = renderHeaderConsulta();
    handledEventHeaderConsulta();
};

const handledBodyConsulta = (container) => {
    document.querySelector(container).innerHTML = renderBodyConsulta();

    handleConsultaFichaPersonal(
        '.container__ficha__personal',
        params.get('filter') === 'ficha'
            ? params.get('ficha') : ``,
        params.get('filter') === 'ficha'
            ? params.get('empresa') : ``,
        params.get('filter') === 'rut'
            ? params.get('rut') : ``,
        params.get('filter')
    );

    handleConsultaContrato(
        '.container__contrato__personal',
        params.get('filter') === 'ficha'
            ? params.get('ficha') : ``,
        params.get('filter') === 'ficha'
            ? params.get('empresa') : ``,
        params.get('filter') === 'rut'
            ? params.get('rut') : ``,
        params.get('filter')
    );
};

export { handledHeaderConsulta, handledBodyConsulta };