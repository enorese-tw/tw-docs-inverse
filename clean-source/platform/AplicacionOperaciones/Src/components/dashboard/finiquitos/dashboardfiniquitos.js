import { __post } from '../../../modules/ajax.js';
import { json } from '../../../modules/config.js';
import { renderDashboard } from '../../dashboard/finiquitos/render/dashfin.js';
import { renderLoadingFiniquito } from '../../../components/finiquitos/render/finiquito.js';
import { handleFiniquitos, handlePaginationFiniquitos, handleHeaderFiniquitos  } from '../../../components/finiquitos/finiquitos.js';
import { handleModalShow, handleModalHidden } from '../../../modules/modal.js';
import { handleRenderNotification } from '../../../modules/notificacion.js';

const config = [json][0].config;

const params = new URLSearchParams(config.search);

const __APP = config.app;
const __SERVERHOST = `${config.origin}${__APP}`;

const handleEventDashboard = () => {
    const buttonFiltrar = document.querySelectorAll('.button__event__dashboard');

    buttonFiltrar.forEach((event) => {
        event.addEventListener("click", (e) => {
            const containerGoal = document.querySelector('.finiquitos');
            
            window.scrollTo({ top: containerGoal.offsetTop, behavior: 'smooth' });

            handleFiniquitos(
                '.finiquitos',
                '1-5',
                e.target.dataset.filter,
                e.target.dataset.dfilter
            );
            handlePaginationFiniquitos(
                '.paginationFiniquitos',
                '1-5',
                e.target.dataset.filter,
                e.target.dataset.dfilter
            );
        });
    });

    const buttonQuestion = document.querySelectorAll('.button__question__dashboard');

    buttonQuestion.forEach((event) => {
        event.addEventListener("mouseenter", (e) => {
            document.querySelector(`#button__question__dashboard__${e.target.dataset.dashboard}`).style.display = "block";
        });
    });
    
    buttonQuestion.forEach((event) => {
        event.addEventListener("mouseout", (e) => {
            document.querySelector(`#button__question__dashboard__${e.target.dataset.dashboard}`).style.display = "none";
        });
    });
};

const handleDashboard = (container) => {
    document.querySelector(container).innerHTML = renderLoadingFiniquito("Cargando Dashboard espere un momento");

    __post(`${__SERVERHOST}/Dashboard/FiniquitosDashboardFiniquitos`)
    .then(resolve => {

        let html = "";
        let contenedores = [];

        resolve.forEach((dashboard) => {
            const { Categoria } = dashboard;

            const plantillas = contenedores.filter(dash => dash === Categoria);

            if (plantillas.length === 0) {
                contenedores.push(Categoria);
            }
        });
        
        contenedores.forEach((contenedor) => {

            const arrayDashboardCategoria = resolve.filter(dash => dash.Categoria === contenedor);

            html += `
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 mt-20">
                    <h1 class="new-family-teamwork color-100x3" style="font-weight: 100;">Dashboard de ${contenedor}</h1>
                </div>
            `;

            arrayDashboardCategoria.forEach((dashboard) => {
                html += renderDashboard(dashboard);
            });

        });
            

        document.querySelector(container).innerHTML =
            `<div class="row">
                ${html}
             </div>
            `;

            handleEventDashboard();
    })
    .catch(reject => {
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });
};

export { handleDashboard };