import { json } from '../../modules/config.js';
import { eventListenerHiddenModal } from '../../modules/modal.js';

import { handledHeaderConsulta, handledBodyConsulta } from '../../components/Solicitud/consulta.js';

const config = [json][0].config;

const params = new URLSearchParams(config.search);

if (params.get('filter') === null) {
    handledHeaderConsulta('.container__header__consulta');
}
else {
    handledBodyConsulta('.container__header__consulta');
}

eventListenerHiddenModal();