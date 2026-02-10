import { renderDOM } from '../../../../modules/render.js';
import { json } from '../../../../modules/config.js';

const config = [json][0].config;

const __APP = config.app;
const __SERVERHOST = `${config.origin}${__APP}`;

const renderDashboard = (dashboard) => {

    const { Categoria, Valor, Descripcion, Background, Filter, DataFilter, Explicacion, OrderBy } = dashboard;

    return renderDOM(
        `
            <div class="col-6 col-sm-3 col-md-12 col-lg-3 ${Background}" style="position: relative; padding-left: 0; padding-right: 0; border: 1px solid #fff;">
                <button type="submit" style="background-color: rgba(0, 0, 0, 0); border: none; width: 100%; display: block;" class="button__event__dashboard" data-filter="${Filter}" data-dfilter="${DataFilter}">
                    <img src="${__SERVERHOST}/Resources/svgnuevos/pregunta.svg" width="20" height="20" style="position: absolute; right: 10px; top: 10px;" class="button__question__dashboard" data-dashboard="${OrderBy}" />
                    <span id="button__question__dashboard__${OrderBy}" class="container__question__dashboard ta-left new-family-teamwork" style="display: none; position: absolute; width: 200px; padding-left: 10px; padding-right: 10px; padding-top: 10px; 
                            padding-bottom: 10px; border-radius: 5px; z-index: 9; background-color: rgba(0, 0, 0, .8); top: 10px; right: 30px; font-size: 13px; font-weight: 100; color: rgb(250, 250, 250);">
                        ${Explicacion !== "" ? Explicacion : "Sin Explicación" }
                    </span>
                    <p class="family-teamwork color-fx3 mt-10 ta-center" style="font-size: 19px;" data-filter="${Filter}" data-dfilter="${DataFilter}">${Categoria}</p>
                    <p class="family-teamwork color-fx3 mt-10 ta-center mt-20neg" data-filter="${Filter}" data-dfilter="${DataFilter}">${Descripcion}</p>
                    <h1 class="family-teamwork ta-center color-fx3 mt-10neg" style="font-size: 50px;" data-filter="${Filter}" data-dfilter="${DataFilter}"><b>${Valor}</b></h1>
                </button>
            </div>
        `
    );
};

export { renderDashboard };