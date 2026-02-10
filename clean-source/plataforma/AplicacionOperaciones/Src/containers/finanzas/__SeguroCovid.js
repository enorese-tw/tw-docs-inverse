import { handleClientes, handleHeaders } from '../../components/finanzas/persona.js';
import { eventListenerHiddenModal } from '../../modules/modal.js';


handleClientes('.personal');
handleHeaders('.headerPersonal');

eventListenerHiddenModal();