import { handleFiniquitos, handleHeaderFiniquitos, handlePaginationFiniquitos } from '../../components/finiquitos/finiquitos.js';
import { eventListenerHiddenModal } from '../../modules/modal.js'; 

const buttonPendientesConfirmacion = document.querySelectorAll('.button__finiquitos__pendientes');

buttonPendientesConfirmacion.forEach((event) => {
    event.addEventListener("click", (e) => {
        handleHeaderFiniquitos('.headerFiniquitos');
        handleFiniquitos('.finiquitos', '1-5', 'Estado', 'SENDPP');
        handlePaginationFiniquitos('.paginationFiniquitos', '1-5', 'Estado', 'SENDPP');
    });
});

eventListenerHiddenModal();