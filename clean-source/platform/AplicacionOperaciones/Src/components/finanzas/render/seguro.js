import { renderDOM } from '../../../modules/render.js';
import { json } from '../../../modules/config.js';

const config = [json][0].config;

const __APP = config.app;
const __SERVERHOST = `${config.origin}${__APP}`;

const renderLoading = () => {
    return renderDOM(
        `
        <div style="text-align: center;" class="mt-40">
            <div class="holder">
                <div class="preloader"><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div></div>
            </div>
        </div>
        `
    );
};

const renderLoadingContent = (content) => {
    return renderDOM(
        `
        <div style="text-align: center;" class="mt-20">
            <div class="holder">
                <div class="preloader"><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div></div>
            </div>
        </div>
        <h3 class="new-family-teamwork ta-center" style="font-weight: 100;">${content}</h3>
        `
    );
};

const renderClientes = (cliente) => {

    const { Nombre } = cliente;

    return renderDOM(
        `
            <div class="new-family-teamwork ta-center mb-20" style="font-weight: 100;">
                <i style="font-style: normal; font-size: 30px; vertical-align: middle;" class="pdl-30 pdr-30">${ Nombre }</i>
                <button type="submit" class="dspl-inline-block btn btn-danger new-family-teamwork button__deleted__cliente" 
                          style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100;" data-target="${Nombre}">
                    <img src="${__SERVERHOST}/Resources/svgnuevos/delete.svg" width="20" height="20" style="position: relative; left: 1%; margin-top: -4%; margin-right: 5px;"  data-target="${Nombre}">
                    <span style="position: relative; float: right;"  data-target="${Nombre}">Eliminar Cliente</span>
                </button>
            </div>
        `
    );
};

const renderHeader = () => {
    return renderDOM(
        `
            <select class="form-control new-family-teamwork" readonly id="selected__filter" style="width: 200px; font-weight: 100; display: inline-block;">
                <option value="0">Seleccione Filtro</option>
                <option value="Folio" selected="selected">Cliente</option>
            </select>
            <input type="text" class="form-control new-family-teamwork" id="data__filter" style="font-weight: 100; width: 300px; display: inline-block;" />
            <button type="submit" class="dspl-inline-block btn btn-teamwork new-family-teamwork color-fx3 button__buscar__cliente" style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; ">
                <img src="${__SERVERHOST}/Resources/svgnuevos/lupa.svg" width="20" height="20" style="position: relative; left: 1%; margin-top: -5%; margin-right: 5px;">
                <span style="position: relative; float: right;">Buscar</span>
            </button>
            <button type="submit" class="dspl-inline-block btn btn-warning new-family-teamwork color-fx3 button__refresh__cliente" style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; ">
                <img src="${__SERVERHOST}/Resources/svgnuevos/refresh.svg" width="20" height="20" style="position: relative; left: 1%; margin-top: -4%; margin-right: 5px;">
                <span style="position: relative; float: right;">Restablecer</span>
            </button>
            <button type="submit" class="dspl-inline-block btn btn-teamwork new-family-teamwork color-fx3 button__add__cliente" style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; ">
                <img src="${__SERVERHOST}/Resources/svgnuevos/add.svg" width="20" height="20" style="position: relative; left: 1%; margin-top: -2%; margin-right: 5px;">
                <span style="position: relative; float: right;">Agregar Cliente</span>
            </button>
            
        `

    );
};

const renderModalEliminarCliente = (cliente) => {
    return renderDOM(
        `
            <div class="m-auto" style="width: 80%;">
                <h3 class="new-family-teamwork pdt-40" style="font-weight: 100; text-align: center;">Eliminar Cliente ${cliente}</h3>
                <p class="new-family-teamwork pdt-20 pdb-40" style="font-weight: 100; text-align: center;">
                    Le comunicamos que esta a punto de eliminar permanentemente el cliente del listado para seguro covid, declara entender esta información.
                </p>
                <div style="text-align: center;">
                    <button type="button" class="btn btn-success mb-10 new-family-teamwork button__aceptar__modal__event" data-event="EliminarCliente" data-value="${cliente}"
                        style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                        <img src="../Resources/svgnuevos/comprobar.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-event="EliminarCliente" data-value="${cliente}" />
                        <span style="position: relative; float: right;" data-event="EliminarCliente" data-value="${cliente}">Aceptar</span> 
                    </button>
                </div>
            </div>
        `
    );
};

const renderModalCrearCliente = (cliente = "") => {
    return renderDOM(
        `
            <div class="m-auto" style="width: 80%;">
                <h3 class="new-family-teamwork pdt-40" style="font-weight: 100; text-align: center;">Crear Cliente</h3>
                <p class="new-family-teamwork pdt-20 pdb-40" style="font-weight: 100; text-align: center;">
                    Indique area de negocio a incorporar
                </p>
                <input type="text" class="new-family-teamwork form-control mb-30" style="font-weight: 100;" id="name__cliente" value="${cliente}" />
                <div style="text-align: center;">
                    <button type="button" class="btn btn-success mb-10 new-family-teamwork button__aceptar__modal__event" data-event="CrearCliente"
                        style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                        <img src="../Resources/svgnuevos/comprobar.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-event="CrearCliente"/>
                        <span style="position: relative; float: right;" data-event="CrearCliente">Aceptar</span> 
                    </button>
                </div>
            </div>
        `
    );
};

const renderNoCliente = () => {
    return renderDOM(
        `
            <div class="mt-50 ta-center">
                <img src="${__SERVERHOST}/Resources/svgnuevos/nocliente.svg" width="100" height="100">
                <h1 class="new-family-teamwork" style="font-weight: 100; color: rgb(150, 150, 150);">No se han encontrado clientes asociados</h1>
            </div>
        `
    );
};

export {
    renderClientes,
    renderHeader,
    renderLoading,
    renderModalEliminarCliente,
    renderNoCliente,
    renderLoadingContent,
    renderModalCrearCliente
};