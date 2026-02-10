import { handleFiniquitos, handleHeaderFiniquitos, handlePaginationFiniquitos, handleConsultarComplementos, eventListenerHiddenSelectConfigOpcionFiniquito } from '../../components/finiquitos/finiquitos.js';
import { eventListenerHiddenModal } from '../../modules/modal.js'; 
import { json } from '../../modules/config.js';

const config = [json][0].config;

const params = new URLSearchParams(config.search);

if (params.get('f') === null) {
    handleHeaderFiniquitos('.headerFiniquitos');
}

handleFiniquitos(
    '.finiquitos',
    '1-5',
    params.get('f') !== null ? "CodigoSolicitud" : ``,
    params.get('f') !== null ? params.get('f') : ``,
);

if (params.get('f') === null) {
    handlePaginationFiniquitos(
        '.paginationFiniquitos',
        '1-5'
    );
}

eventListenerHiddenModal();
eventListenerHiddenSelectConfigOpcionFiniquito();