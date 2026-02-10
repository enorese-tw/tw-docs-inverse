import { renderDOM } from '../../../modules/render.js';
import { json } from '../../../modules/config.js';
import { ButtonCenterBlock } from './button.js';
import { permisos } from '../../../modules/configPermisos.js';

const config = [json][0].config;

const params = new URLSearchParams(config.search);

const __APP = config.app;
const __SERVERHOST = `${config.origin}${__APP}`;

const renderHeaderFiniquitos = () => {
    return renderDOM(
        `
            <select class="form-control new-family-teamwork" id="selected__filter" style="width: 200px; font-weight: 100; display: inline-block;">
                <option value="0">Seleccione Filtro</option>
                <option value="Folio">Folio</option>
                <option value="Rut">Rut Trabajador</option>
                <option value="Ficha">Ficha Trabajador</option>
                <option value="Empresa">Empresa</option>
                <option value="CC">Costo $0</option>
                <option value="Cliente">Area Negocio</option>
                <option value="Nombre">Nombre Trabajador</option>
                <option value="Causal">Causal Desvinculación</option>
            </select>
            <input type="text" class="form-control new-family-teamwork" id="data__filter" style="font-weight: 100; width: 300px; display: inline-block;" />
            <button type="submit" class="dspl-inline-block btn btn-teamwork new-family-teamwork color-fx3 button__buscar__finiquitos" style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; ">
                <img src="${__SERVERHOST}/Resources/svgnuevos/lupa.svg" width="20" height="20" style="position: relative; left: 1%; margin-top: -5%; margin-right: 5px;">
                <span style="position: relative; float: right;">Buscar</span>
            </button>
            <button type="submit" class="dspl-inline-block btn btn-warning new-family-teamwork color-fx3 button__refresh__finiquitos" style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; ">
                <img src="${__SERVERHOST}/Resources/svgnuevos/refresh.svg" width="20" height="20" style="position: relative; left: 1%; margin-top: -5%; margin-right: 5px;">
                <span style="position: relative; float: right;">Restablecer</span>
            </button>
            <button type="submit" class="dspl-inline-block btn btn-teamwork new-family-teamwork color-fx3 button__barcodebusqueda__finiquitos" style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; ">
                <img src="${__SERVERHOST}/Resources/svgnuevos/barcode.svg" width="20" height="20" style="position: relative; left: 1%; margin-top: -2%; margin-right: 5px;">
                <span style="position: relative; float: right;">Buscar Por Código Barra</span>
            </button>
            <button type="submit" class="dspl-inline-block btn btn-teamwork new-family-teamwork color-fx3 button__adminmasivo__finiquitos" style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; ">
                <img src="${__SERVERHOST}/Resources/svgnuevos/seleccion.svg" width="20" height="20" style="position: relative; left: 1%; margin-top: -2%; margin-right: 5px;">
                <span style="position: relative; float: right;">Proceso Masivo</span>
            </button>
            <button class="dspl-inline-block btn btn-success new-family-teamwork color-fx3 button__filter__masive__finiquitos" style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100;">
                <img src="${__SERVERHOST}/Resources/svgnuevos/seleccion.svg" width="20" height="20" style="position: relative; left: 1%; margin-top: -2%; margin-right: 5px;">
                <span style="position: relative; float: right;">Selección Multiple</span>
            </button>
            <button class="dspl-inline-block btn btn-danger new-family-teamwork color-fx3 button__deleted__masive__finiquitos" style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; ">
                <img src="${__SERVERHOST}/Resources/svgnuevos/delete.svg" width="20" height="20" style="position: relative; left: 1%; margin-top: -2%; margin-right: 5px;">
                <span style="position: relative; float: right;">Eliminar Selección Multiple</span>
            </button>
        `
        
    );
};

const renderBusquedaBarcodeFiniquitos = () => {
    return renderDOM(
        `
            <div class="pdt-60">
                <img src="${__SERVERHOST}/Resources/svgnuevos/barcodeIL.svg" width="80" height="80" style="display: block; oppacity: .7;" class="ml-auto mr-auto" />
                <h2 class="new-family-teamwork ta-center color-100x3" style="font-weight: 100;">Búsqueda de Finiquito por Lectura de Código de Barra</h2>
                
                <p class="new-family-teamwork ta-center ml-auto mr-auto" style="font-weight: 100; width: 80%;">
                    En la parte superior derecha del finiquito, se encuentra un código de barra que representa el folio del mismo, el cual puede buscarlo en sistema solo leyendo con la herramienta correspondiente.
                </p>
    
                <input type="text" style="width: 100%; top: 0; background-color: transparent; border: none; color: transparent; outline: none; position: absolute; height: 100%;" id="barcode__read" />
                
            </div>
        `
    );
};

const renderProcesoMasivoBarcodeFiniquitosNone = () => {
    return renderDOM(
        `
            <div class="pdt-60">
                <img src="${__SERVERHOST}/Resources/svgnuevos/barcodeIL.svg" width="80" height="80" style="display: block; oppacity: .7;" class="ml-auto mr-auto" />
                <h2 class="new-family-teamwork ta-center color-100x3 ml-auto mr-auto" style="font-weight: 100; width: 90%;">Proceso Masivo de Finiquito por Lectura de Código de Barra</h2>
                
                <p class="new-family-teamwork ta-center ml-auto mr-auto" style="font-weight: 100; width: 80%;">
                    En la parte superior derecha del finiquito, se encuentra un código de barra que representa el folio del mismo, el cual al leerlo con la herramienta correspondiente lo agregara.
                    <br/><br/>
                    Importante: Si el finiquito ya se encuentra agregado dentro del proceso masivo y es nuevamente leido, automaticamente el sistema lo excluira del proceso.
                </p>
    
                <input type="text" style="width: 100%; top: 0; background-color: transparent; border: none; color: transparent; outline: none; position: absolute; height: 100%;" id="barcode__read__masive" />
                
            </div>
        `
    );
};

const renderProcesoMasivoBarcodeFiniquitos = (count) => {
    return renderDOM(
        `
            <div class="pdt-60 pdb-60">
                <img src="${__SERVERHOST}/Resources/svgnuevos/barcodeIL.svg" width="50" height="50" style="display: block; oppacity: .7;" class="ml-auto mr-auto" />
                <h4 class="new-family-teamwork ta-center color-100x3 ml-auto mr-auto" style="font-weight: 100; width: 90%;">Proceso Masivo de Finiquito por Lectura de Código de Barra</h4>
                
                <p class="new-family-teamwork ta-center ml-auto mr-auto" style="font-weight: 100; width: 80%; font-size: 14px;">
                    En la parte superior derecha del finiquito, se encuentra un código de barra que representa el folio del mismo, el cual al leerlo con la herramienta correspondiente lo agregara.
                    <br/><br/>
                    Importante: Si el finiquito ya se encuentra agregado dentro del proceso masivo y es nuevamente leido, automaticamente el sistema lo excluira del proceso.
                </p>
                
                <div class="ta-center">
                    <button type="button" class="btn btn-teamwork color-fx3 mb-10 new-family-teamwork button__acetar__modal__event" data-event="ConfirmarProcesoMasivo"
                        style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle; z-index: 999; position: relative;">
                        <span style="position: relative; float: right;" data-event="ConfirmarProcesoMasivo">Continuar</span> 
                        <img src="${__SERVERHOST}/Resources/svgnuevos/arrowright.svg" width="20" height="20" style="position: relative; right: 1%; margin-left: 5px;" data-event="ConfirmarProcesoMasivo" />
                    </button>
                </div>
                

                <h4 class="new-family-teamwork ml-auto mr-auto" style="font-weight: 100; width: 80%;">Ha incluido ${count} ${count > 1 ? `finiquitos.` : `finiquito.`}</h4>

                <table class="content__finiquito__masive new-family-teamwork ml-auto mr-auto" style="font-weight: 100; max-width: 80%;">
                </table>
    
                <input type="text" style="width: 100%; top: 0; background-color: transparent; border: none; color: transparent; outline: none; position: absolute; height: 100%;" id="barcode__read__masive" />
                
            </div>
        `
    );
};

const renderConfirmacionProcesoMasivo = (count, excel = "") => {
    return renderDOM(
        `
            <div class="pdt-30 pdb-60">
                <h3 class="new-family-teamwork ta-center color-100x3 ml-auto mr-auto" style="font-weight: 100; width: 90%;">Confirmación de Proceso Masivo</h3>
                <p class="new-family-teamwork pdt-20 pdb-40" style="font-weight: 100; text-align: center;">
                    Podrá excluir y revisar los finiquitos que estan incluidos dentro del proceso masivo, tambien deberá seleccionar que opción masiva desea realizar con los finiquitos, una vez que se procesen y se actualicen de manera satisfactoria, 
                    desapareceran del listado de proceso masivo.
                </p>
                <div class="ta-center">
                    <div style="position: relative; width: 50%; display: inline-block;">
                        <button type="button" class="btn new-family-teamwork btn-teamwork color-fx3 ml-auto mr-auto button__acetar__modal__event selected__opcion__masiva__finiquito" 
                                style="border: none; border-radius: 25px; text-align: center; font-weight: 100; align-items: center; 
                                vertical-align: middle; width: 95%; height: 50px;" data-event="SeleccionarOpcionMasiva">
                            <img src="${__SERVERHOST}/Resources/svgnuevos/configuraciones.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" class="selected__opcion__masiva__finiquito" data-event="SeleccionarOpcionMasiva">
                            <span style="position: relative;" class="selected__opcion__masiva__finiquito" data-event="SeleccionarOpcionMasiva">Seleccionar Opción Masiva</span> 
                        </button>
                    </div>
                    <div style="position: relative; width: 50%; display: inline-block;">
                        <div style="position: absolute; top: 0; left: 0; box-shadow: 0px 0px 10px rgba(0, 0, 0, .3); border-radius: 10px; background-color: #fff; display: none; width: 100%;" class="content__options__admin">
                            
                        </div>
                    </div>
                </div>
                
                <h4 class="new-family-teamwork ml-auto mr-auto" style="font-weight: 100; width: 80%;">Ha incluido ${count} ${count > 1 ? `finiquitos.` : `finiquito.`}</h4>
                
                <div class="ta-center">
                    ${renderButtonReporteProcesoMasivo(excel)}
                </div>

                <table class="content__finiquito__masive new-family-teamwork ml-auto mr-auto" style="font-weight: 100; max-width: 80%;">
                </table>
    
            </div>
        `
    );
};

const renderProcesingProcesoMasivo = (count) => {
    return renderDOM(
        `
            <div class="pdt-30 pdb-60">
                <h3 class="new-family-teamwork ta-center color-100x3 ml-auto mr-auto" style="font-weight: 100; width: 90%;">Procesando Finiquitos</h3>

                <h4 class="new-family-teamwork ml-auto mr-auto" style="font-weight: 100; width: 80%;">Ha incluido ${count} ${count > 1 ? `finiquitos.` : `finiquito.`}</h4>
                                    
                <div>
                    <h3 class="new-family-teamwork ml-auto mr-auto content__correct__finiquitos" style="font-weight: 100; width: 80%;">Finiquitos Exitosos 0</h3>
                    <div class="container__button__report__result ml-auto mr-auto" style="width: 80%;">
                    </div>
                    <h3 class="new-family-teamwork ml-auto mr-auto content__error__finiquitos" style="font-weight: 100; width: 80%;">Finiquitos con Problemas 0</h3>
                </div>

                <table class="content__finiquito__masive new-family-teamwork ml-auto mr-auto" style="font-weight: 100; max-width: 80%;">
                </table>
    
            </div>
        `
    );
};

const renderProcesoMasivoCargaFile = () => {
    return renderDOM(
        `
            <div class="pdt-50 pdb-80">
                <div style="width: 40%; box-shadow: 0px 0px 10px rgba(0, 0, 0, .3); border-radius: 6px; background-color: rgb(250, 250, 250); min-height: 400px;" class="mt-30 ml-auto mr-auto">
                    <input type="file" id="uploadFile" style="display: none;" />
                    <button class="btn btn-teamwork color-fx3 new-family-teamwork button__acetar__modal__event" data-event="UploadFile"
                            style="font-weight: 100; width: 100%; height: 100px; border-top-left-radius: 6px; border-top-right-radius: 6px; border-bottom-left-radius: 0px; border-bottom-right-radius: 0px;">
                        <img src="${__SERVERHOST}/Resources/svgnuevos/cloud-computing.svg" width="50" hegith="50" /> Subir Archivo Masivo
                    </button>
                    <p class="ml-auto mr-auto new-family-teamwork ta-center pdt-40" style="width: 90%; font-weight: 100;">
                        Para poder realizar la carga masiva de finiquitos, debe realizarse con un archivo estandar, si no tienes el archivo puedes descagarlo a continuacion
                        
                        <a href="${__SERVERHOST}/Operaciones/DownloadFileCargaMasiva?template=NTc=" style="display: block;" class="mt-20 ml-auto mr-auto">
                            <img src="${__SERVERHOST}/Resources/svgnuevos/excelfile.svg" width="30" hegith="30" />
                            <br/>
                            Descargar Archivo Masivo Aqui
                        </a>

                    </p>
                </div>
            </div>
        `
    );
};

const renderProcesosMasivo = () => {
    return renderDOM(
        `
            <div class="pdt-40 pdl-30 pdr-30 pdb-30">
                <h2 class="new-family-teamwork ta-center color-150x3 pdb-50" style="font-weight: 100;">Seleccione Opción Masiva</h2>
                <div style="width: calc(100% / 3 - 10px); display: inline-block; vertical-align: top;">
                    <img src="${__SERVERHOST}/Resources/svgnuevos/seleccionIL.svg" width="80" height="80" style="display: block; oppacity: .7;" class="ml-auto mr-auto" />
                    <h4 class="new-family-teamwork ta-center color-100x3" style="font-weight: 100;">Selección Multiple</h4>
                    <p class="new-family-teamwork ta-center color-100x3" style="font-weight: 100;">Proceso Masivo mediante los finiquitos que seleccione.</p>
                    <div style="text-align: center;">
                        <button type="button" class="btn btn-success mb-10 new-family-teamwork button__acetar__modal__event" data-event="ConfirmarProcesoMasivo"
                            style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                            <img src="../Resources/svgnuevos/comprobar.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-event="ConfirmarProcesoMasivo" />
                            <span style="position: relative; float: right;" data-event="ConfirmarProcesoMasivo">Elegir</span> 
                        </button>
                    </div>
                </div>
                <div style="width: calc(100% / 3 - 10px); display: inline-block; vertical-align: top;">
                    <img src="${__SERVERHOST}/Resources/svgnuevos/barcodescannerIL.svg" width="80" height="80" style="display: block; oppacity: .7;" class="ml-auto mr-auto" />
                    <h4 class="new-family-teamwork ta-center color-100x3" style="font-weight: 100;">Lectura Código de Barra</h4>
                    <p class="new-family-teamwork ta-center color-100x3" style="font-weight: 100;">Proceso Masivo mediante lectura de codigo de barra.</p>
                    <div style="text-align: center;">
                        <button type="button" class="btn btn-success mb-10 new-family-teamwork button__acetar__modal__event" data-event="MasivoBarcode"
                            style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                            <img src="../Resources/svgnuevos/comprobar.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-event="MasivoBarcode" />
                            <span style="position: relative; float: right;" data-event="MasivoBarcode">Elegir</span> 
                        </button>
                    </div>
                </div>
                <div style="width: calc(100% / 3 - 10px); display: inline-block; vertical-align: top;">
                    <img src="${__SERVERHOST}/Resources/svgnuevos/subirIL.svg" width="80" height="80" style="display: block; oppacity: .7;" class="ml-auto mr-auto" />
                    <h4 class="new-family-teamwork ta-center color-100x3" style="font-weight: 100;">Subir Archivo Masivo</h4>
                    <p class="new-family-teamwork ta-center color-100x3" style="font-weight: 100;">Proceso Masivo mediante subida de archivo excel (el cual podrá obtener dentro de la misma sección).</p>
                    <div style="text-align: center;">
                        <button type="button" class="btn btn-success mb-10 new-family-teamwork button__acetar__modal__event" data-event="CargaMasiva"
                            style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                            <img src="../Resources/svgnuevos/comprobar.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-event="CargaMasiva" />
                            <span style="position: relative; float: right;" data-event="CargaMasiva">Elegir</span> 
                        </button>
                    </div>
                </div>
            </div>
        `
    );
};

const renderButtonReporteProcesoMasivo = (excel = "") => {
    return renderDOM(
        `
            <a href="${__SERVERHOST}/Finiquitos/GenerateExcel?excel=FiniquitosProcesoMasivo&data=${excel}" class="btn btn-success new-family-teamwork mb-10" 
                style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                <img src="${__SERVERHOST}/Resources/svgnuevos/excel.svg" width="20" height="20" style="left: 1%; margin-right: 7px;">
                <span style="float: right;">Reporte de Finiquito Proceso Masivo</span> 
            </a>
        `
    )
}

const renderPaginationFiniquitos = (page, type) => {

    const { Class, Rango, Filter, DataFilter, NumeroPagina, Properties } = page;

    return renderDOM(
        `
            <button style="border: none; font-weight: 100; ${Class !== "active " ? `background-color: transparent; ` : `border-radius: 50%; width: 30px; height: 30px; `}" class="${Class === "active " ? `btn-teamwork color-fx3` : ``} 
                button__pagination__${type} new-family-teamwork"
                data-page="${Rango}" data-filter="${Filter}" data-dfilter="${DataFilter}" ${Properties}>${NumeroPagina}</button>
        `
    );
};

const renderHistorialFiniquito = (historial) => {

    const { Fecha, Usuario, Comentarios } = historial;

    return renderDOM(
        `
            <tr>
                <td class="bl-1-solid-200x3">${Fecha}</td>
                <td>${Usuario}</td>
                <td>${Comentarios}</td>
            </tr>
        `
    );
};

const renderLoadingFiniquitoProcess = () => {
    return renderDOM(
        `
        <div style="text-align: center;">
            <div class="holder">
                <div class="preloader"><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div></div>
            </div>
        </div>
        `
    );
};

const renderLoadingFiniquito = (content) => {
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

const renderValidacionFiniquito = (codigo = "") => {
    return renderDOM(
        `
            <div class="validar__finiquito m-auto" style="width: 80%;">
                <h3 class="new-family-teamwork pdt-40" style="font-weight: 100; text-align: center;">Validación de Finiquito</h3>
                <p class="new-family-teamwork pdt-20 pdb-40" style="font-weight: 100; text-align: center;">
                    Le comunicamos que esta a punto de validar un finiquito, esto quiere decir no podra ser anulado a no ser que revierta la validación, lo puede hacer desde el propio finiquito, declara entender esta información.
                </p>
                <div style="text-align: center;">
                    <button type="button" class="btn btn-success mb-10 new-family-teamwork button__acetar__modal__event" data-event="ValidarFiniquito" data-value="${codigo}"
                        style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                        <img src="../Resources/svgnuevos/comprobar.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-event="ValidarFiniquito" data-value="${ codigo}" />
                        <span style="position: relative; float: right;" data-event="ValidarFiniquito" data-value="${ codigo }">Aceptar</span> 
                    </button>
                </div>
            </div>
        `
    );
};

const renderAnulacionFiniquito = (codigo = "", observacion = "") => {
    return renderDOM(
        `
            <div class="anular__finiquito m-auto" style="width: 80%;">
                <h3 class="new-family-teamwork pdt-40" style="font-weight: 100; text-align: center;">Anulación de Finiquito</h3>
                <p class="new-family-teamwork pdt-20 pdb-10" style="font-weight: 100; text-align: center;">
                    El siguiente finiquito será anulado, lo que indica que todos los documentos asociados serán anulados (solo es un borrado de sistema, por ende puede ser recuperado en el futuro si es que fuera necesario, 
                    para ese proceso debe comunicarse con area de sistemas para la recuperación de la información), debe indicar la observación asociada a la anulación del finiquito:
                </p>
                <textarea id="observacion__anulacion" class="form-control new-family-teamwork mb-20" style="font-weight: 100; min-height: 200px;" placeholder="Debe indicar el motivo de anulación (Obligatorio)">${observacion}</textarea>
                <div style="text-align: center;">
                    <button type="button" class="btn btn-success mb-10 new-family-teamwork button__acetar__modal__event" data-event="AnularFiniquito" data-value="${codigo}"
                        style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                        <img src="../Resources/svgnuevos/comprobar.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-event="AnularFiniquito" data-value="${ codigo}" />
                        <span style="position: relative; float: right;" data-event="AnularFiniquito" data-value="${ codigo}">Aceptar</span> 
                    </button>
                </div>
            </div>
        `
    );
};

const renderGestionEnvioRegion = (codigo = "") => {
    return renderDOM(
        `
            <div class="anular__finiquito m-auto" style="width: 80%;">
                <h3 class="new-family-teamwork pdt-40" style="font-weight: 100; text-align: center;">Gestión Envio Región de Finiquito</h3>
                <p class="new-family-teamwork pdt-20 pdb-10" style="font-weight: 100; text-align: center;">
                    La gestión quedará registrada una vez que acepte, al utilizar esta opción de envio deberá indicar rol y nombre con quien coordino dicha gestión, una vez recepcionado el finiquito este no quedará legalizado, quedara a la espera de enviarlo a 
                    legalizar y recepcionar dicha legalización, para continuar a la siguiente etapa. Importante, si requiere revertir la gestión por cualquier motivo, tendra disponible la opción en el propio finiquito, declara entender la información entregada.
                </p>
                <select class="form-control new-family-teamwork" style="font-weight: 100;" id="select__metodo__envio">
                    <option value="">Seleccione Método de Envio a Regiones</option>
                    <option value="CHILEX">Chile Express</option>
                    <option value="EMAIL">Correo Electrónico</option>
                </select>
                <div style="text-align: center;" class="mt-20">
                    <button type="button" class="btn btn-success mb-10 new-family-teamwork button__acetar__modal__event" data-event="GestionEnvioRegionAceptar" data-value="${codigo}"
                        style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                        <img src="../Resources/svgnuevos/comprobar.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-event="GestionEnvioRegionAceptar" data-value="${ codigo}" />
                        <span style="position: relative; float: right;" data-event="GestionEnvioRegionAceptar" data-value="${ codigo}">Aceptar</span> 
                    </button>
                </div>
            </div>
        `
    );
};

const renderGestionEnvioSantiagoNotaria = (codigo = "") => {
    return renderDOM(
        `
            <div class="anular__finiquito m-auto" style="width: 80%;">
                <h3 class="new-family-teamwork pdt-40" style="font-weight: 100; text-align: center;">Gestión Coordinación con Trabajador en Notaria Santiago</h3>
                <p class="new-family-teamwork pdt-20 pdb-10" style="font-weight: 100; text-align: center;">
                    La gestión quedará registrada una vez que acepte, una vez recepcionado automaticamente el finiquito quedará legalizado. 
                    Importante, si requiere revertir la gestión por cualquier motivo, tendra disponible la opción en el propio finiquito, declara entender la información entregada.
                </p>
                <div style="text-align: center;">
                    <button type="button" class="btn btn-success mb-10 new-family-teamwork button__acetar__modal__event" data-event="EnvioSantiagoNotariaAceptar" data-value="${codigo}"
                        style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                        <img src="../Resources/svgnuevos/comprobar.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-event="EnvioSantiagoNotariaAceptar" data-value="${ codigo}" />
                        <span style="position: relative; float: right;" data-event="EnvioSantiagoNotariaAceptar" data-value="${ codigo}">Aceptar</span> 
                    </button>
                </div>
            </div>
        `
    );
};

const renderGestionEnvioSantiagoFirma = (codigo = "") => {
    return renderDOM(
        `
            <div class="anular__finiquito m-auto" style="width: 80%;">
                <h3 class="new-family-teamwork pdt-40" style="font-weight: 100; text-align: center;">Gestión Coordinación Firma de Trabajador Santiago</h3>
                <p class="new-family-teamwork pdt-20 pdb-10" style="font-weight: 100; text-align: center;">
                    La gestión quedará registrada una vez que acepte, al utilizar esta opción de envio deberá indicar rol y nombre con quien coordino dicha gestión, una vez recepcionado el finiquito este no quedará legalizado, quedara a la espera de enviarlo a 
                    legalizar y recepcionar dicha legalización, para continuar a la siguiente etapa. Importante, si requiere revertir la gestión por cualquier motivo, tendra disponible la opción en el propio finiquito, declara entender la información entregada.
                </p>
                <select class="form-control new-family-teamwork mb-20" style="font-weight: 100;" id="select__rol__coordinador">
                    <option value="">Seleccione Rol con quien se coordina</option>
                    <option value="Kam">KAM</option>
                    <option value="Supervisor">Supervisor</option>
                </select>
                <input type="text" class="new-family-teamwork form-control" style="font-weight: 100;" id="name__coordinador" placeholder="Indique el nombre con quien se coordino firma del trabajador" />
                <div style="text-align: center;" class="mt-20">
                    <button type="button" class="btn btn-success mb-10 new-family-teamwork button__acetar__modal__event" data-event="EnvioSantiagoFirmaAceptar" data-value="${codigo}"
                        style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                        <img src="../Resources/svgnuevos/comprobar.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-event="EnvioSantiagoFirmaAceptar" data-value="${ codigo}" />
                        <span style="position: relative; float: right;" data-event="EnvioSantiagoFirmaAceptar" data-value="${ codigo}">Aceptar</span> 
                    </button>
                </div>
            </div>
        `
    );
};

const renderGestionEnvioFiniquito = (codigo = "") => {
    return renderDOM(
        `
            <div class="gestionar__envio__finiquito m-auto" style="width: 80%;">
                <h3 class="new-family-teamwork pdt-40" style="font-weight: 100; text-align: center;">Gestionar Envio de Finiquito</h3>
                <p class="new-family-teamwork pdt-20 pdb-10" style="font-weight: 100; text-align: center;">
                    Seleccione que gestión de envio es la que realizara sobre el finiquito.
                </p>
                <button type="submit" class="dspl-inline-block btn btn-info new-family-teamwork color-fx3 m-auto mb-5 button__acetar__modal__event" data-event="GestionEnvioRegion" data-value="${codigo}"
                        style="display: block; width: 98%; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; text-align: center;">
                    <img src="../Resources/svgnuevos/enviar.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 5px;" data-event="GestionEnvioRegion" data-value="${codigo}">
                    <span style="position: relative;" data-event="GestionEnvioRegion" data-value="${codigo}">Envio Regiones</span>
                </button>

                <button type="submit" class="dspl-inline-block btn btn-info new-family-teamwork color-fx3 m-auto mb-5 button__acetar__modal__event" data-event="EnvioSantiagoNotaria" data-value="${codigo}"
                        style="display: block; width: 98%; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; text-align: center;">
                    <img src="../Resources/svgnuevos/enviar.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 5px;" data-event="EnvioSantiagoNotaria" data-value="${codigo}">
                    <span style="position: relative;" data-event="EnvioSantiagoNotaria" data-value="${codigo}">Santiago Notaria</span>
                </button>

                <button type="submit" class="dspl-inline-block btn btn-info new-family-teamwork color-fx3 m-auto mb-5 button__acetar__modal__event" data-event="EnvioSantiagoFirma" data-value="${codigo}"
                        style="display: block; width: 98%; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; text-align: center;">
                    <img src="../Resources/svgnuevos/enviar.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 5px;" data-event="EnvioSantiagoFirma" data-value="${codigo}">
                    <span style="position: relative;" data-event="EnvioSantiagoFirma" data-value="${codigo}">Envio Santiago Firma Trabajador</span>
                </button>
            </div>
        `
    );
};

const renderRecepcionarFiniquitoSantiagoNotaria = (codigo = "") => {
    return renderDOM(
        `
            <div class="validar__finiquito m-auto" style="width: 80%;">
                <h3 class="new-family-teamwork pdt-40" style="font-weight: 100; text-align: center;">Recepción de Finiquito Legalizado de Santiago</h3>
                <p class="new-family-teamwork pdt-20 pdb-40" style="font-weight: 100; text-align: center;">
                    Le comunicamos que esta a punto de validar un finiquito, esto quiere decir no podra ser anulado a no ser que revierta la validación, lo puede hacer desde el propio finiquito, declara entender esta información.
                </p>
                <div style="text-align: center;">
                    <button type="button" class="btn btn-success mb-10 new-family-teamwork button__acetar__modal__event" data-event="RecepcionarFiniquitoSantiagoNotaria" data-value="${codigo }"
                        style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                        <img src="../Resources/svgnuevos/comprobar.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-event="RecepcionarFiniquitoSantiagoNotaria" data-value="${ codigo }" />
                        <span style="position: relative; float: right;" data-event="RecepcionarFiniquitoSantiagoNotaria" data-value="${ codigo}">Aceptar</span> 
                    </button>
                </div>
            </div>
        `
    );
};

const renderRecepcionarFiniquitoRegiones = (codigo = "") => {
    return renderDOM(
        `
            <div class="validar__finiquito m-auto" style="width: 80%;">
                <h3 class="new-family-teamwork pdt-40" style="font-weight: 100; text-align: center;">Recepción de Finiquito Legalizado de Regiones</h3>
                <p class="new-family-teamwork pdt-20 pdb-40" style="font-weight: 100; text-align: center;">
                    Le comunicamos que esta a punto de validar un finiquito, esto quiere decir no podra ser anulado a no ser que revierta la validación, lo puede hacer desde el propio finiquito, declara entender esta información.
                </p>
                <div style="text-align: center;">
                    <button type="button" class="btn btn-success mb-10 new-family-teamwork button__acetar__modal__event" data-event="RecepcionarFiniquitoRegiones" data-value="${codigo}"
                        style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                        <img src="../Resources/svgnuevos/comprobar.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-event="RecepcionarFiniquitoRegiones" data-value="${ codigo}" />
                        <span style="position: relative; float: right;" data-event="RecepcionarFiniquitoRegiones" data-value="${ codigo}">Aceptar</span> 
                    </button>
                </div>
            </div>
        `
    );
};

const renderRecepcionarFiniquitoSantiagoFirmaTrabajador = (codigo = "") => {
    return renderDOM(
        `
            <div class="validar__finiquito m-auto" style="width: 80%;">
                <h3 class="new-family-teamwork pdt-40" style="font-weight: 100; text-align: center;">Recepción Santiago Firmado por Trabajador</h3>
                <p class="new-family-teamwork pdt-20 pdb-40" style="font-weight: 100; text-align: center;">
                    Le comunicamos que esta a punto de validar un finiquito, esto quiere decir no podra ser anulado a no ser que revierta la validación, lo puede hacer desde el propio finiquito, declara entender esta información.
                </p>
                <div style="text-align: center;">
                    <button type="button" class="btn btn-success mb-10 new-family-teamwork button__acetar__modal__event" data-event="RecepcionarFiniquitoSantiagoFirmaTrabajador" data-value="${codigo}"
                        style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                        <img src="../Resources/svgnuevos/comprobar.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-event="RecepcionarFiniquitoSantiagoFirmaTrabajador" data-value="${ codigo}" />
                        <span style="position: relative; float: right;" data-event="RecepcionarFiniquitoSantiagoFirmaTrabajador" data-value="${ codigo}">Aceptar</span> 
                    </button>
                </div>
            </div>
        `
    );
};

const renderGestionEnvioLegalizar = (codigo = "") => {
    return renderDOM(
        `
            <div class="anular__finiquito m-auto" style="width: 80%;">
                <h3 class="new-family-teamwork pdt-40" style="font-weight: 100; text-align: center;">Gestión de Envio a Legalizar</h3>
                <p class="new-family-teamwork pdt-20 pdb-10" style="font-weight: 100; text-align: center;">
                    La gestión quedará registrada una vez que acepte, una vez recepcionado automaticamente el finiquito quedará legalizado. 
                    Importante, si requiere revertir la gestión por cualquier motivo, tendra disponible la opción en el propio finiquito, declara entender la información entregada.
                </p>
                <div style="text-align: center;" class="mt-20">
                    <button type="button" class="btn btn-success mb-10 new-family-teamwork button__acetar__modal__event" data-event="GestionEnvioLegalizar" data-value="${codigo}"
                        style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                        <img src="../Resources/svgnuevos/comprobar.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-event="GestionEnvioLegalizar" data-value="${ codigo}" />
                        <span style="position: relative; float: right;" data-event="GestionEnvioLegalizar" data-value="${ codigo}">Aceptar</span> 
                    </button>
                </div>
            </div>
        `
    );
};

const renderGestionRecepcionLegalizacion = (codigo = "") => {
    return renderDOM(
        `
            <div class="validar__finiquito m-auto" style="width: 80%;">
                <h3 class="new-family-teamwork pdt-40" style="font-weight: 100; text-align: center;">Recepción de Finiquito Legalizado</h3>
                <p class="new-family-teamwork pdt-20 pdb-40" style="font-weight: 100; text-align: center;">
                    Le comunicamos que esta a punto de legalizar el finiquito, si requiere revertir el estado lo puede hacer desde el propio finiquito, declara entender esta información.
                </p>
                <div style="text-align: center;">
                    <button type="button" class="btn btn-success mb-10 new-family-teamwork button__acetar__modal__event" data-event="GestionRecepcionLegalizacion" data-value="${codigo}"
                        style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                        <img src="../Resources/svgnuevos/comprobar.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-event="GestionRecepcionLegalizacion" data-value="${ codigo}" />
                        <span style="position: relative; float: right;" data-event="GestionRecepcionLegalizacion" data-value="${ codigo}">Aceptar</span> 
                    </button>
                </div>
            </div>
        `
    );
};

const renderSolicitarValeVista = (codigo = "") => {
    return renderDOM(
        `
            <div class="validar__finiquito m-auto" style="width: 80%;">
                <h3 class="new-family-teamwork pdt-40" style="font-weight: 100; text-align: center;">Solicitar Vale Vista</h3>
                <p class="new-family-teamwork pdt-20 pdb-40" style="font-weight: 100; text-align: center;">
                    Le comunicamos que esta a punto de solicitar el pago mediante Vale Vista, el finiquito quedara pendiente de aprobación para seguir al proceso de pago por parte de finanzas, declara entender esta información.
                </p>
                <div style="text-align: center;">
                    <button type="button" class="btn btn-success mb-10 new-family-teamwork button__acetar__modal__event" data-event="SolicitarValeVista" data-value="${codigo}"
                        style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                        <img src="../Resources/svgnuevos/comprobar.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-event="SolicitarValeVista" data-value="${ codigo}" />
                        <span style="position: relative; float: right;" data-event="SolicitarValeVista" data-value="${ codigo}">Aceptar</span> 
                    </button>
                </div>
            </div>
        `
    );
};

const renderSolicitarCheque = (codigo = "") => {
    return renderDOM(
        `
            <div class="validar__finiquito m-auto" style="width: 80%;">
                <h3 class="new-family-teamwork pdt-40" style="font-weight: 100; text-align: center;">Solicitar Cheque</h3>
                <p class="new-family-teamwork pdt-20 pdb-40" style="font-weight: 100; text-align: center;">
                    Le comunicamos que esta a punto de solicitar el pago mediante Cheque, el finiquito quedara pendiente de aprobación para seguir al proceso de pago por parte de finanzas, declara entender esta información.
                </p>
                <div style="text-align: center;">
                    <button type="button" class="btn btn-success mb-10 new-family-teamwork button__acetar__modal__event" data-event="SolicitarCheque" data-value="${codigo}"
                        style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                        <img src="../Resources/svgnuevos/comprobar.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-event="SolicitarCheque" data-value="${ codigo}" />
                        <span style="position: relative; float: right;" data-event="SolicitarCheque" data-value="${ codigo}">Aceptar</span> 
                    </button>
                </div>
            </div>
        `
    );
};

const renderConsultaDatosTEF = (tef, codigo = "") => {

    const { Rut, Beneficiario, Banco, Cuenta, Total, TotalGlosa } = tef;

    return renderDOM(
        `
            <div class="validar__finiquito m-auto" style="width: 80%;">
                <h3 class="new-family-teamwork pdt-40" style="font-weight: 100; text-align: center;">Solicitar Transferencia</h3>  
                <div class="ta-center">
                    <button type="button" class="btn btn-warning color-fx3 mb-10 new-family-teamwork button__acetar__modal__event" data-event="EditarDatosBancariosBeneficiario"
                        style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                        <img src="../Resources/svgnuevos/editar.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-event="EditarDatosBancariosBeneficiario" />
                        <span style="position: relative; float: right;" data-event="EditarDatosBancariosBeneficiario">Editar Datos Bancarios</span> 
                    </button>
                </div>
                <h4 class="new-family-teamwork pdt-10 color-150x3" style="font-weight: 100; text-align: center;">Datos del Beneficiario</h4>
                                

                <p class="new-family-teamwork mb-0" style="font-weight: 100; text-align: center; font-size: 25px;">${Rut}</p>
                <p class="new-family-teamwork" style="font-weight: 100; text-align: center; font-size: 20px;">${Beneficiario}</p>

                <h4 class="new-family-teamwork pdt-20 color-150x3" style="font-weight: 100; text-align: center;">Datos Bancarios del Beneficiario</h4>
                
                <select id="banco__selected__beneficario" ${ Banco !== '' ? `disabled` : `` } class="form-control new-family-teamwork mb-10" style="font-weight: 100; font-size: 17px; background-color: #fff;">
                    <option>Selecione Banco</option>
                </select>

                <input type="text" class="new-family-teamwork form-control" ${ Cuenta !== '' ? `disabled` : `` } id="cuenta__beneficiario" style="font-weight: 100; font-size: 20px; background-color: #fff;" value="${Cuenta}" />

                <h4 class="new-family-teamwork pdt-20 color-150x3" style="font-weight: 100; text-align: center;">Monto Finiquito</h4>

                <p class="new-family-teamwork" style="font-weight: 100; text-align: center; font-size: 25px;">${Total}</p>
                <p class="new-family-teamwork" style="font-weight: 100; text-align: center;">${TotalGlosa}</p>

                <h4 class="new-family-teamwork pdt-20 color-150x3" style="font-weight: 100; text-align: center;">Gasto Administrativo</h4>
                <input type="text" id="gastoadm__tef" class="form-control new-family-teamwork ta-center" style="font-weight: 100;" placeholder="0" />

                <textarea id="observacion__tef" class="form-control new-family-teamwork mt-10 mb-20" style="font-weight: 100; min-height: 100px;" placeholder="Observaciones, anotaciones y/o comentarios"></textarea>

                <div style="text-align: center;">
                    <button type="button" class="btn btn-success mb-10 new-family-teamwork button__acetar__modal__event" data-event="SolicitarTef" data-value="${codigo}"
                        style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                        <img src="../Resources/svgnuevos/comprobar.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-event="SolicitarTef" data-value="${ codigo}" />
                        <span style="position: relative; float: right;" data-event="SolicitarTef" data-value="${ codigo}">Aceptar</span> 
                    </button>
                </div>
            </div>
        `
    );
};

const renderBancosSelect = (bancos, selected) => {

    const { Nombre } = bancos;

    return renderDOM(
       `<option value="${Nombre}" ${ selected === Nombre ? `selected="selected"` : `` }>${Nombre}</option>`
    );
};

const renderBancoSelectHeader = (header) => {
    return renderDOM(
        `<option value="">${header}</option>`
    );
};

const renderConsultaSolicitudPago = (solicitud, codigo = "") => {

    let tipoPago = "";
    const { Rut, Beneficiario, Banco, Cuenta, Total, TotalGlosa, Tipo, MontoFiniquito, MontoAdm } = solicitud;

    switch (Tipo) {
        case "TEF":
            tipoPago = 'Transferencia';
            break;
        case "VV":
            tipoPago = 'Vale Vista';
            break;
        case "CHQ":
            tipoPago = 'Cheque';
            break;
    }

    return renderDOM(
        `
            <div style="width: 50%; display: inline-block; vertical-align: top;">
                <embed src="${__SERVERHOST}/Bajas/ViewPdf?pdf=Q2FyYXR1bGFGaW5pcXVpdG8=&data=${codigo}" style="width: 100%; height: 700px;" />
                <div style="margin-top: -600px; width: 100%; height: 600px; text-align: center;">
                    <img src="${__SERVERHOST}/Resources/pdfcolorreal-x2.png" style="width: 50px; display: block; margin: auto;" />
                    <h1 class="family-teamwork color-100x3">Cargando PDF<br />Espere un momento...</h1>
                </div>
            </div>
            <div class="validar__finiquito m-auto" style="width: calc(100% - 50% - 10px); display: inline-block; vertical-align: top;">
                <h3 class="new-family-teamwork pdt-40" style="font-weight: 100; text-align: center;">Solicitud de Pago Pendiente de Confirmación</h3>
                <h4 class="new-family-teamwork pdt-10" style="font-weight: 100; text-align: center;">${tipoPago}</h4>            

                <h4 class="new-family-teamwork pdt-10 color-150x3" style="font-weight: 100; text-align: center;">Datos del Beneficiario</h4>
                                
                <p class="new-family-teamwork mb-0" style="font-weight: 100; text-align: center; font-size: 25px;">${Rut}</p>
                <p class="new-family-teamwork" style="font-weight: 100; text-align: center; font-size: 20px;">${Beneficiario}</p>
                ${
                    Tipo === 'TEF'
                        ? `
                            <h4 class="new-family-teamwork pdt-20 color-150x3" style="font-weight: 100; text-align: center;">Datos Bancarios del Beneficiario</h4>

                            <p class="new-family-teamwork mb-0" style="font-weight: 100; text-align: center; font-size: 20px;">${Banco}</p>
                            <p class="new-family-teamwork" style="font-weight: 100; text-align: center; font-size: 25px;">${Cuenta}</p>
                            `
                        : ``
                }
               
                <h4 class="new-family-teamwork pdt-20 color-150x3" style="font-weight: 100; text-align: center;">Montos</h4>

                <p class="new-family-teamwork" style="font-weight: 100; text-align: center; font-size: 19px;">Monto Finiquito: ${MontoFiniquito}</p>
                <p class="new-family-teamwork" style="font-weight: 100; text-align: center; font-size: 19px;">Gasto Administrativo: ${MontoAdm}</p>

                <h4 class="new-family-teamwork pdt-20 color-150x3" style="font-weight: 100; text-align: center;">Monto A Pago</h4>

                <p class="new-family-teamwork" style="font-weight: bold; text-align: center; font-size: 20px;">$ ${Total}</p>
                

                <div style="text-align: center;">
                    <button type="button" class="btn btn-success mb-10 new-family-teamwork button__acetar__modal__event" data-event="ConfirmarPago" data-value="${codigo}"
                        style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                        <img src="../Resources/svgnuevos/comprobar.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-event="ConfirmarPago" data-value="${ codigo}" />
                        <span style="position: relative; float: right;" data-event="ConfirmarPago" data-value="${ codigo}">Aceptar</span> 
                    </button>
                </div>
            </div>
        `
    );
};

const renderLoadingBancosSelect = () => {
    return renderDOM(
        `<option>Cargando Bancos, espere un momento...</option>`
    );
};

const renderConsultaDatosTEFNo = (tef) => {

    const { Message } = tef; 

    return renderDOM(
        `
            <div class="validar__finiquito m-auto" style="width: 80%;">
                <h3 class="new-family-teamwork pdt-40" style="font-weight: 100; text-align: center;">Solicitar Transferencia</h3>  
                <p class="new-family-teamwork pdt-20 pdb-40" style="font-weight: 100; text-align: center;">
                    ${Message}
                </p>
            </div>
        `
    );
};

const renderPagarFiniquito = (codigo = "") => {
    return renderDOM(
        `
            <div class="anular__finiquito m-auto" style="width: 80%;">
                <h3 class="new-family-teamwork pdt-40" style="font-weight: 100; text-align: center;">Pagar Finiquito</h3>
                <p class="new-family-teamwork pdt-20 pdb-10" style="font-weight: 100; text-align: center;">
                    La gestión quedará registrada una vez que acepte automaticamente quedará en estado pagado y luego terminado. 
                    Importante, ya no podrá revertir el estado de pagado, ya que el finiquito quedara automaticamente terminado, debido a que fue legalizado previamente en sistema.
                </p>
                <div style="text-align: center;" class="mt-20">
                    <button type="button" class="btn btn-success mb-10 new-family-teamwork button__acetar__modal__event" data-event="PagarFiniquito" data-value="${codigo}"
                        style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                        <img src="../Resources/svgnuevos/comprobar.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-event="PagarFiniquito" data-value="${ codigo}" />
                        <span style="position: relative; float: right;" data-event="PagarFiniquito" data-value="${ codigo}">Aceptar</span> 
                    </button>
                </div>
            </div>
        `
    );
};

const renderTerminarFiniquito = (codigo = "") => {
    return renderDOM(
        `
            <div class="anular__finiquito m-auto" style="width: 80%;">
                <h3 class="new-family-teamwork pdt-40" style="font-weight: 100; text-align: center;">Terminar Finiquito</h3>
                <p class="new-family-teamwork pdt-20 pdb-10" style="font-weight: 100; text-align: center;">
                    Una vez que acepte automaticamente quedará en estado terminado. 
                    Importante, al terminar el finiquito este no podrá volver a pasos anteriores.
                </p>
                <div style="text-align: center;" class="mt-20">
                    <button type="button" class="btn btn-success mb-10 new-family-teamwork button__acetar__modal__event" data-event="TerminarFiniquito" data-value="${codigo}"
                        style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                        <img src="../Resources/svgnuevos/comprobar.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-event="TerminarFiniquito" data-value="${ codigo}" />
                        <span style="position: relative; float: right;" data-event="TerminarFiniquito" data-value="${ codigo}">Aceptar</span> 
                    </button>
                </div>
            </div>
        `
    );
};

const renderFirmarFiniquito = (codigo = "") => {
    return renderDOM(
        `
            <div class="anular__finiquito m-auto" style="width: 80%;">
                <h3 class="new-family-teamwork pdt-40" style="font-weight: 100; text-align: center;">Firmar Finiquito</h3>
                <p class="new-family-teamwork pdt-20 pdb-10" style="font-weight: 100; text-align: center;">
                    Una vez que acepte automaticamente quedará firmado el documento.
                </p>
                <div style="text-align: center;" class="mt-20">
                    <button type="button" class="btn btn-success mb-10 new-family-teamwork button__acetar__modal__event" data-event="FirmarFiniquito" data-value="${codigo}"
                        style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                        <img src="../Resources/svgnuevos/comprobar.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-event="FirmarFiniquito" data-value="${codigo}" />
                        <span style="position: relative; float: right;" data-event="FirmarFiniquito" data-value="${codigo}">Aceptar</span> 
                    </button>
                </div>
            </div>
        `
    );
};

const renderReprocesarDocumentosFiniquito = (codigo = "") => {
    return renderDOM(
        `
            <div class="anular__finiquito m-auto" style="width: 80%;">
                <h3 class="new-family-teamwork pdt-40" style="font-weight: 100; text-align: center;">Actualizar Documentos Finiquito</h3>
                <p class="new-family-teamwork pdt-20 pdb-10" style="font-weight: 100; text-align: center;">
                    Al aceptar los documentos (la caratula y el finiquito) se actualizaran de acuerdo a los parametros que esten en ese momento incluidos en el finiquito.
                </p>
                <div style="text-align: center;" class="mt-20">
                    <button type="button" class="btn btn-success mb-10 new-family-teamwork button__acetar__modal__event" data-event="ReprocesarDocumentosFiniquito" data-value="${codigo}"
                        style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                        <img src="../Resources/svgnuevos/comprobar.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-event="ReprocesarDocumentosFiniquito" data-value="${codigo}" />
                        <span style="position: relative; float: right;" data-event="ReprocesarDocumentosFiniquito" data-value="${codigo}">Aceptar</span> 
                    </button>
                </div>
            </div>
        `
    );
};

const renderValidacionFinanzas = (codigo = "") => {
    return renderDOM(
        `
            <div class="validar__finiquito m-auto" style="width: 80%;">
                <h3 class="new-family-teamwork pdt-40" style="font-weight: 100; text-align: center;">V°B de Finanzas</h3>
                <p class="new-family-teamwork pdt-20 pdb-40" style="font-weight: 100; text-align: center;">
                    Le comunicamos que esta a punto de dar V°B al finiquito, esto quiere decir no podra ser anulado a no ser que revierta el V°B, declara entender esta información.
                </p>
                <div style="text-align: center;">
                    <button type="button" class="btn btn-success mb-10 new-family-teamwork button__acetar__modal__event" data-event="ValidarFinanzas" data-value="${codigo}"
                        style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                        <img src="../Resources/svgnuevos/comprobar.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-event="ValidarFinanzas" data-value="${ codigo}" />
                        <span style="position: relative; float: right;" data-event="ValidarFinanzas" data-value="${ codigo}">Aceptar</span> 
                    </button>
                </div>
            </div>
        `
    );
};

const renderLiquidacionesSueldoYear = (liquidacion, codigo = "") => {
    const { Year } = liquidacion;

    return renderDOM(
        `
            <div class="ml-auto mr-auto" style="width: 92%;">
                <h2 class="pdt-20 new-family-teamwork color-100x3" style="font-weight: 100;">
                    Liquidaciones de Sueldo ${ Year }
                </h2>
                <table class="new-family-teamwork" style="font-weight: 100;">
                    <tbody class="content__liquidaciones__${Year}">
                    
                    </tbody>
                </table>
            </div>
        `
    );
};

const renderLiquidacionesSueldoMes = (liquidacion, codigo) => {
    const { FechaMes, IdArchivo } = liquidacion;

    return renderDOM(
        `
            <tr>
                <td>Liquidación de Sueldo</td>
                <td class="pdl-20 pdr-20">${FechaMes}</td>
                <td>
                    <a href="${__SERVERHOST}/Bajas/ViewPdf?pdf=TGlxdWlkYWNpb25TdWVsZG8=&data=${IdArchivo}&code=${codigo}&type=FNQ" target="_blank" class="btn btn-danger new-family-teamwork mb-10" 
                        style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                        <img src="${__SERVERHOST}/Resources/pdf.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;">
                        <span style="position: relative; float: right;">Liquidacion de Sueldo</span> 
                    </a>
                </td>
            </tr>
        `
    );
};

const renderCrearComplemento = (codigo) => {
    return renderDOM(
        `
            <div class="validar__finiquito m-auto" style="width: 80%;">
                <h3 class="new-family-teamwork pdt-40" style="font-weight: 100; text-align: center;">Creación de Complemento</h3>
                <p class="new-family-teamwork pdt-20 pdb-40" style="font-weight: 100; text-align: center;">
                   ¿Quiere iniciar el proceso de creación de complemento de finiquito?, Se generará la instancia de creación de documento complemento de finiquito, indicando haberes y descuentos, para la posterior creación del documento.
                </p>
                <div style="text-align: center;">
                    <button type="button" class="btn btn-success mb-10 new-family-teamwork button__acetar__modal__event" data-event="CrearDocumentoComplemento" data-value="${codigo}"
                        style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                        <img src="../Resources/svgnuevos/comprobar.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-event="CrearDocumentoComplemento" data-value="${ codigo}" />
                        <span style="position: relative; float: right;" data-event="CrearDocumentoComplemento" data-value="${ codigo}">Aceptar</span> 
                    </button>
                </div>
            </div>
        `
    );
};

const renderFormularioHaberes = (codigo = "") => {
    return renderDOM(
        `
            <input type="text" class="new-family-teamwork form-control" id="concepto__descripcion" placeholder="Indicar Haber" style="vertical-align: middle;display: inline-block; font-wight: 100; width: 48%; " />
            <input type="number" class="new-family-teamwork form-control" id="concepto__monto" placeholder="Indicar Monto" style="vertical-align: middle;display: inline-block; font-wight: 100; width: calc(100% - 65% - 10px);" />
            <button type="button" class="btn btn-teamwork color-fx3 new-family-teamwork button__acetar__modal__event" data-event="AgregarHaberComplemento" data-value="${codigo}"
                style="vertical-align: middle; min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                <img src="${__SERVERHOST}/Resources/svgnuevos/add.svg" width="20" height="20" style="position: relative; right: 1%; margin-left: 3px;" data-event="AgregarHaberComplemento" data-value="${codigo}" />
                <span style="position: relative; float: right;" data-event="AgregarHaberComplemento" data-value="${codigo}">Agregar</span> 
            </button>
        `
    );
};

const renderFormularioDescuentos = (codigo = "") => {
    return renderDOM(
        `
            <input type="text" class="new-family-teamwork form-control" id="concepto__descripcion"  placeholder="Indicar Descuento" style="vertical-align: middle;display: inline-block; font-wight: 100; width: 48%; " />
            <input type="number" class="new-family-teamwork form-control" id="concepto__monto" placeholder="Indicar Monto" style="vertical-align: middle;display: inline-block; font-wight: 100; width: calc(100% - 65% - 10px);" />
            <button type="button" class="btn btn-teamwork color-fx3 new-family-teamwork button__acetar__modal__event" data-event="AgregarDescuentoComplemento" data-value="${codigo}"
                style="vertical-align: middle; min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                <img src="${__SERVERHOST}/Resources/svgnuevos/add.svg" width="20" height="20" style="position: relative; right: 1%; margin-left: 3px;" data-event="AgregarDescuentoComplemento" data-value="${codigo}" />
                <span style="position: relative; float: right;" data-event="AgregarDescuentoComplemento" data-value="${codigo}">Agregar</span> 
            </button>
        `
    );
};

const renderCrearComplementoStageHaberes = (codigo) => {
    return renderDOM(
        `
            <div class="validar__finiquito m-auto" style="width: 80%;">
                <div class="ta-center new-family-teamwork pdt-30" style="font-weight: 100;">
                    <div style="width: 15%; display: inline-block; vertical-align: top;">
                        <div style="display: flex-inline; align-items: center; vertical-align: middle; width: 50px; height: 30px; border-radius: 50px;" class="m-auto warning">
                            <img src="${__SERVERHOST}/Resources/svgnuevos/reloj.svg" width="20" height="20" class="mt-3" />
                        </div>
                        <span style="font-size: 15px;">Haberes</span>
                    </div>

                    <div style="width: 20%; display: inline-block; vertical-align: middle;">
                        <span style="width: 100%; display: block; height: 5px; border-radius: 5px;" class="disabled"></span>
                    </div>

                    <div style="width: 15%; display: inline-block; vertical-align: top;">
                        <div style="width: 50px; height: 30px; border-radius: 50px;" class="m-auto disabled"></div>
                        <span style="font-size: 15px;" class="color-150x3">Descuentos</span>
                    </div>

                    <div style="width: 20%; display: inline-block; vertical-align: middle;">
                        <span style="width: 100%; display: block; height: 5px; border-radius: 5px;" class="disabled"></span>
                    </div>

                    <div style="width: 15%; display: inline-block;  vertical-align: top;">
                        <div style="width: 50px; height: 30px; border-radius: 50px;" class="m-auto disabled"></div>
                        <span style="font-size: 15px;" class="color-150x3">Fecha Documento</span>
                    </div>
                </div>
                <h3 class="new-family-teamwork" style="font-weight: 100;">Agregar Haberes al Complemento</h3>
                <div class="container__agregar__concepto">
                    ${renderFormularioHaberes(codigo)}
                </div>
                
                <h4 class="new-family-teamwork" style="font-weight: 100;">Listado de Haberes Incorporados en Complemento</h4>

                <div>
                    
                    <table class="new-family-teamwork ml-auto mr-auto content__haberes__complemento" style="font-weight: 100; font-size: 17px;">
                        <tbody>
                            
                        </tbody>
                    </table>

                </div>

                <div class="mt-30" style="text-align: center;">
                    <button type="button" class="btn btn-teamwork color-fx3 mb-10 new-family-teamwork button__acetar__modal__event" data-event="ContinuarEtapaDescuentosComplemento" data-value="${codigo}"
                        style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                        <span style="position: relative; float: right;" data-event="ContinuarEtapaDescuentosComplemento" data-value="${ codigo}">Continuar</span> 
                        <img src="${__SERVERHOST}/Resources/svgnuevos/arrowright.svg" width="20" height="20" style="position: relative; right: 1%; margin-left: 5px;" data-event="ContinuarEtapaDescuentosComplemento" data-value="${ codigo}" />
                    </button>
                </div>
            </div>
        `
    );
};

const renderHaberesComplemento = (haberes) => {
    const { Valor, Nombre, CodigoHaber, CodigoComplemento } = haberes;

    return renderDOM(
        `
        <tr>
            <td>${Nombre}</td>
            <td class="pdl-30 pdr-30">${Valor}</td>
            <td>
                <button type="button" class="btn btn-danger new-family-teamwork button__acetar__modal__event" data-event="EliminarComplementoHaber" data-complemento="${CodigoComplemento}" data-value="${CodigoHaber}"
                    style="vertical-align: middle; min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                    <img src="${__SERVERHOST}/Resources/svgnuevos/delete.svg" width="20" height="20" style="position: relative; right: 1%; margin-left: 3px;" data-event="EliminarComplementoHaber" data-complemento="${CodigoComplemento}" data-value="${CodigoHaber}" />
                    <span style="position: relative; float: right;" data-event="EliminarComplementoHaber" data-complemento="${CodigoComplemento}" data-value="${CodigoHaber}">Eliminar</span> 
                </button>
            </td>
        </tr>
        `
    );
};

const renderDescuentosComplemento = (descuentos) => {
    const { Valor, Nombre, CodigoDescuento, CodigoComplemento } = descuentos;

    return renderDOM(
        `
        <tr>
            <td>${Nombre}</td>
            <td class="pdl-30 pdr-30">${Valor}</td>
            <td>
                <button type="button" class="btn btn-danger new-family-teamwork button__acetar__modal__event" data-event="EliminarComplementoDescuento" data-complemento="${CodigoComplemento}" data-value="${CodigoDescuento}"
                    style="vertical-align: middle; min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                    <img src="${__SERVERHOST}/Resources/svgnuevos/delete.svg" width="20" height="20" style="position: relative; right: 1%; margin-left: 3px;" data-event="EliminarComplementoDescuento" data-complemento="${CodigoComplemento}" data-value="${CodigoDescuento}" />
                    <span style="position: relative; float: right;" data-event="EliminarComplementoDescuento" data-complemento="${CodigoComplemento}" data-value="${CodigoDescuento}">Eliminar</span> 
                </button>
            </td>
        </tr>
        `
    );
};  

const renderCrearComplementoStageDescuentos = (codigo) => {
    return renderDOM(
        `
            <div class="validar__finiquito m-auto" style="width: 80%;">
                <div class="ta-center new-family-teamwork pdt-30" style="font-weight: 100;">
                    <div style="width: 15%; display: inline-block; vertical-align: top;">
                        <div style="display: flex-inline; align-items: center; vertical-align: middle; width: 50px; height: 30px; border-radius: 50px;" class="m-auto success">
                            <img src="${__SERVERHOST}/Resources/svgnuevos/aprobar.svg" width="20" height="20" class="mt-3" />
                        </div>
                        <span style="font-size: 15px;">Haberes</span>
                    </div>

                    <div style="width: 20%; display: inline-block; vertical-align: middle;">
                        <span style="width: 100%; display: block; height: 5px; border-radius: 5px;" class="warning"></span>
                    </div>

                    <div style="width: 15%; display: inline-block; vertical-align: top;">
                        <div style="display: flex-inline; align-items: center; vertical-align: middle; width: 50px; height: 30px; border-radius: 50px;" class="m-auto warning">
                            <img src="${__SERVERHOST}/Resources/svgnuevos/reloj.svg" width="20" height="20" class="mt-3" />
                        </div>
                        <span style="font-size: 15px;" class="color-150x3">Descuentos</span>
                    </div>

                    <div style="width: 20%; display: inline-block; vertical-align: middle;">
                        <span style="width: 100%; display: block; height: 5px; border-radius: 5px;" class="disabled"></span>
                    </div>

                    <div style="width: 15%; display: inline-block;  vertical-align: top;">
                        <div style="width: 50px; height: 30px; border-radius: 50px;" class="m-auto disabled"></div>
                        <span style="font-size: 15px;" class="color-150x3">Fecha Documento</span>
                    </div>
                </div>
                <h3 class="new-family-teamwork" style="font-weight: 100;">Agregar Descuento al Complemento</h3>
                <div class="container__agregar__concepto">
                    ${renderFormularioDescuentos(codigo)}
                </div>
                
                <h4 class="new-family-teamwork" style="font-weight: 100;">Listado de Descuentos Incorporados en Complemento</h4>

                <div>
                    <table class="new-family-teamwork ml-auto mr-auto content__descuento__complemento" style="font-weight: 100; font-size: 17px;">
                        
                    </table>
                </div>

                <div class="mt-30" style="text-align: center;">
                    <button type="button" class="btn btn-anulado color-fx3 mb-10 new-family-teamwork button__acetar__modal__event" data-event="AnteriorEtapaHaberesComplemento" data-value="${codigo}"
                        style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                        <img src="${__SERVERHOST}/Resources/svgnuevos/arrowleft.svg" width="20" height="20" style="position: relative; right: 1%; margin-left: 3px;" data-event="AnteriorEtapaHaberesComplemento" data-value="${codigo}" />
                        <span style="position: relative; float: right;" data-event="AnteriorEtapaHaberesComplemento" data-value="${ codigo}">Anterior</span> 
                    </button>
                    <button type="button" class="btn btn-teamwork color-fx3 mb-10 new-family-teamwork button__acetar__modal__event" data-event="ContinuarEtapaFechaComplemento" data-value="${codigo}"
                        style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                        <span style="position: relative; float: right;" data-event="ContinuarEtapaFechaComplemento" data-value="${ codigo}">Continuar</span> 
                        <img src="${__SERVERHOST}/Resources/svgnuevos/arrowright.svg" width="20" height="20" style="position: relative; right: 1%; margin-left: 5px;" data-event="ContinuarEtapaFechaComplemento" data-value="${codigo}" />
                    </button>
                </div>
            </div>
        `
    );
};

const renderCrearComplementoStageFechaDocumento = (codigo) => {
    return renderDOM(
        `
            <div class="validar__finiquito m-auto" style="width: 80%;">
                <div class="ta-center new-family-teamwork pdt-30" style="font-weight: 100;">
                    <div style="width: 15%; display: inline-block; vertical-align: top;">
                        <div style="display: flex-inline; align-items: center; vertical-align: middle; width: 50px; height: 30px; border-radius: 50px;" class="m-auto success">
                            <img src="${__SERVERHOST}/Resources/svgnuevos/aprobar.svg" width="20" height="20" class="mt-3" />
                        </div>
                        <span style="font-size: 15px;">Haberes</span>
                    </div>

                    <div style="width: 20%; display: inline-block; vertical-align: middle;">
                        <span style="width: 100%; display: block; height: 5px; border-radius: 5px;" class="success"></span>
                    </div>

                    <div style="width: 15%; display: inline-block; vertical-align: top;">
                        <div style="display: flex-inline; align-items: center; vertical-align: middle; width: 50px; height: 30px; border-radius: 50px;" class="m-auto success">
                            <img src="${__SERVERHOST}/Resources/svgnuevos/aprobar.svg" width="20" height="20" class="mt-3" />
                        </div>
                        <span style="font-size: 15px;" class="color-150x3">Descuentos</span>
                    </div>

                    <div style="width: 20%; display: inline-block; vertical-align: middle;">
                        <span style="width: 100%; display: block; height: 5px; border-radius: 5px;" class="warning"></span>
                    </div>

                    <div style="width: 15%; display: inline-block;  vertical-align: top;">
                        <div style="display: flex-inline; align-items: center; vertical-align: middle; width: 50px; height: 30px; border-radius: 50px;" class="m-auto warning">
                            <img src="${__SERVERHOST}/Resources/svgnuevos/reloj.svg" width="20" height="20" class="mt-3" />
                        </div>
                        <span style="font-size: 15px;" class="color-150x3">Fecha Documento</span>
                    </div>
                </div>
                <h3 class="new-family-teamwork" style="font-weight: 100;">Agregar Fecha de Creación de Documento</h3>
                <div class="ta-center">
                    <input type="date" class="new-family-teamwork form-control" id="fecha__documento" style="vertical-align: middle; display: inline-block; font-wight: 100; width: 48%;" />
                </div>
                
                <div class="mt-30" style="text-align: center;">
                    <button type="button" class="btn btn-anulado color-fx3 mb-10 new-family-teamwork button__acetar__modal__event" data-event="AnteriorEtapaDescuentoComplemento" data-value="${codigo}"
                        style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                        <img src="${__SERVERHOST}/Resources/svgnuevos/arrowleft.svg" width="20" height="20" style="position: relative; right: 1%; margin-left: 3px;" data-event="AnteriorEtapaDescuentoComplemento" data-value="${codigo}" />
                        <span style="position: relative; float: right;" data-event="AnteriorEtapaDescuentoComplemento" data-value="${ codigo}">Anterior</span> 
                    </button>
                    <button type="button" class="btn btn-teamwork color-fx3 mb-10 new-family-teamwork button__acetar__modal__event" data-event="FinalizarCreacionDocumento" data-value="${codigo}"
                        style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                        <span style="position: relative; float: right;" data-event="FinalizarCreacionDocumento" data-value="${ codigo}">Crear Documento</span> 
                        <img src="${__SERVERHOST}/Resources/svgnuevos/arrowright.svg" width="20" height="20" style="position: relative; right: 1%; margin-left: 5px;" data-event="FinalizarCreacionDocumento" data-value="${codigo}" />
                    </button>
                </div>
            </div>
        `
    );
};

const renderActualizarGastoAdministrativo = (codigo) => {
    return renderDOM(
        `
            <div class="validar__finiquito m-auto" style="width: 80%;">
                <h3 class="new-family-teamwork pdt-20" style="font-weight: 100; text-align: center;">Actualizar Gasto Administrativo</h3>
                <p class="new-family-teamwork pdt-20 pdb-40" style="font-weight: 100; text-align: center;">
                   Al indicar el monto y aceptar, automaticamente se actualizara el monto total a pago
                </p>
                <h4 class="new-family-teamwork pdt-20 color-150x3" style="font-weight: 100; text-align: center;">Gasto Administrativo</h4>
                <input type="text" id="gastoadm__tef" class="form-control new-family-teamwork ta-center" style="font-weight: 100;" placeholder="0" />

                <textarea id="observacion__tef" class="form-control new-family-teamwork mt-10 mb-20" style="font-weight: 100; min-height: 100px;" placeholder="Observaciones, anotaciones y/o comentarios"></textarea>
                <div style="text-align: center;">
                    <button type="button" class="btn btn-success mb-10 new-family-teamwork button__acetar__modal__event" data-event="ActualizarGastoAdministrativo" data-value="${codigo}"
                        style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                        <img src="../Resources/svgnuevos/comprobar.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-event="ActualizarGastoAdministrativo" data-value="${ codigo}" />
                        <span style="position: relative; float: right;" data-event="ActualizarGastoAdministrativo" data-value="${ codigo}">Aceptar</span> 
                    </button>
                </div>
            </div>
        `
    );
};

const renderRevertirValidacion = (codigo) => {
    return renderDOM(
        `
            <div class="validar__finiquito m-auto" style="width: 80%;">
                <h3 class="new-family-teamwork pdt-20" style="font-weight: 100; text-align: center;">Revertir Validación de Finiquito</h3>
                <p class="new-family-teamwork pdt-20 pdb-40" style="font-weight: 100; text-align: center;">
                   Al revertir la validación del finiquito, este quedará automaticamente con estado calculado (debera indicarnos el motivo por el cual revierte la validación), declara entender la información entregada.
                </p>
                <textarea id="observacion__rev" class="form-control new-family-teamwork mt-10 mb-20" style="font-weight: 100; min-height: 100px;" placeholder="Observaciones y/o Motivo (Obligatorio)"></textarea>
                <div style="text-align: center;">
                    <button type="button" class="btn btn-success mb-10 new-family-teamwork button__acetar__modal__event" data-event="RevertirValidacion" data-value="${codigo}"
                        style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                        <img src="../Resources/svgnuevos/comprobar.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-event="RevertirValidacion" data-value="${ codigo}" />
                        <span style="position: relative; float: right;" data-event="RevertirValidacion" data-value="${ codigo}">Aceptar</span> 
                    </button>
                </div>
            </div>
        `
    );
};

const renderRevertirValidacionFinanzas = (codigo) => {
    return renderDOM(
        `
            <div class="validar__finiquito m-auto" style="width: 80%;">
                <h3 class="new-family-teamwork pdt-20" style="font-weight: 100; text-align: center;">Revertir V°B de Finanzas</h3>
                <p class="new-family-teamwork pdt-20 pdb-40" style="font-weight: 100; text-align: center;">
                   Al revertir el V°B de finanzas, el finiquito quedará automaticamente con estado calculado (debera indicarnos el motivo por el cual revierte el V°B de finanzas), declara entender la información entregada.
                </p>
                <textarea id="observacion__rev" class="form-control new-family-teamwork mt-10 mb-20" style="font-weight: 100; min-height: 100px;" placeholder="Observaciones y/o Motivo (Obligatorio)"></textarea>
                <div style="text-align: center;">
                    <button type="button" class="btn btn-success mb-10 new-family-teamwork button__acetar__modal__event" data-event="RevertirValidacionFinanzas" data-value="${codigo}"
                        style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                        <img src="../Resources/svgnuevos/comprobar.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-event="RevertirValidacionFinanzas" data-value="${ codigo}" />
                        <span style="position: relative; float: right;" data-event="RevertirValidacionFinanzas" data-value="${ codigo}">Aceptar</span> 
                    </button>
                </div>
            </div>
        `
    );
};

const renderRevertirGestionEnvio = (codigo) => {
    return renderDOM(
        `
            <div class="validar__finiquito m-auto" style="width: 80%;">
                <h3 class="new-family-teamwork pdt-20" style="font-weight: 100; text-align: center;">Revertir Gestion de Envio de Finiquito</h3>
                <p class="new-family-teamwork pdt-20 pdb-40" style="font-weight: 100; text-align: center;">
                   Al revertir la gestión de envio del finiquito, este quedará automaticamente con estado validado (debera indicarnos el motivo por el cual revierte la gestión de envio), declara entender la información entregada.
                </p>
                <textarea id="observacion__rev" class="form-control new-family-teamwork mt-10 mb-20" style="font-weight: 100; min-height: 100px;" placeholder="Observaciones y/o Motivo (Obligatorio)"></textarea>
                <div style="text-align: center;">
                    <button type="button" class="btn btn-success mb-10 new-family-teamwork button__acetar__modal__event" data-event="RevertirGestionEnvio" data-value="${codigo}"
                        style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                        <img src="../Resources/svgnuevos/comprobar.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-event="RevertirGestionEnvio" data-value="${ codigo}" />
                        <span style="position: relative; float: right;" data-event="RevertirGestionEnvio" data-value="${ codigo}">Aceptar</span> 
                    </button>
                </div>
            </div>
        `
    );
};

const renderRevertirLegalizacion = (codigo) => {
    return renderDOM(
        `
            <div class="validar__finiquito m-auto" style="width: 80%;">
                <h3 class="new-family-teamwork pdt-20" style="font-weight: 100; text-align: center;">Revertir Legalización de Finiquito</h3>
                <p class="new-family-teamwork pdt-20 pdb-40" style="font-weight: 100; text-align: center;">
                   Al revertir la legalización del finiquito, este quedará automaticamente con estado validado (debera indicarnos el motivo por el cual revierte la legalización), declara entender la información entregada.
                </p>
                <textarea id="observacion__rev" class="form-control new-family-teamwork mt-10 mb-20" style="font-weight: 100; min-height: 100px;" placeholder="Observaciones y/o Motivo (Obligatorio)"></textarea>
                <div style="text-align: center;">
                    <button type="button" class="btn btn-success mb-10 new-family-teamwork button__acetar__modal__event" data-event="RevertirLegalizacion" data-value="${codigo}"
                        style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                        <img src="../Resources/svgnuevos/comprobar.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-event="RevertirLegalizacion" data-value="${ codigo}" />
                        <span style="position: relative; float: right;" data-event="RevertirLegalizacion" data-value="${ codigo}">Aceptar</span> 
                    </button>
                </div>
            </div>
        `
    );
};

const renderRevertirSolicitudPago = (codigo) => {
    return renderDOM(
        `
            <div class="validar__finiquito m-auto" style="width: 80%;">
                <h3 class="new-family-teamwork pdt-20" style="font-weight: 100; text-align: center;">Revertir Solicitud de Pago de Finiquito</h3>
                <p class="new-family-teamwork pdt-20 pdb-40" style="font-weight: 100; text-align: center;">
                   Al revertir la Solicitud de pago del finiquito, este quedará automaticamente con el ultimo estado registrado (debera indicarnos el motivo por el cual revierte la solicitud de pago), declara entender la información entregada.
                </p>
                <textarea id="observacion__rev" class="form-control new-family-teamwork mt-10 mb-20" style="font-weight: 100; min-height: 100px;" placeholder="Observaciones y/o Motivo (Obligatorio)"></textarea>
                <div style="text-align: center;">
                    <button type="button" class="btn btn-success mb-10 new-family-teamwork button__acetar__modal__event" data-event="RevertirSolicitudPago" data-value="${codigo}"
                        style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                        <img src="../Resources/svgnuevos/comprobar.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-event="RevertirSolicitudPago" data-value="${ codigo}" />
                        <span style="position: relative; float: right;" data-event="RevertirSolicitudPago" data-value="${ codigo}">Aceptar</span> 
                    </button>
                </div>
            </div>
        `
    );
};

const renderRevertirConfirmacion = (codigo) => {
    return renderDOM(
        `
            <div class="validar__finiquito m-auto" style="width: 80%;">
                <h3 class="new-family-teamwork pdt-20" style="font-weight: 100; text-align: center;">Revertir Confirmación de Finiquito</h3>
                <p class="new-family-teamwork pdt-20 pdb-40" style="font-weight: 100; text-align: center;">
                   Al revertir la confirmación del finiquito, este quedará automaticamente en solicitud de pago (debera indicarnos el motivo por el cual revierte la confirmación), declara entender la información entregada.
                </p>
                <textarea id="observacion__rev" class="form-control new-family-teamwork mt-10 mb-20" style="font-weight: 100; min-height: 100px;" placeholder="Observaciones y/o Motivo (Obligatorio)"></textarea>
                <div style="text-align: center;">
                    <button type="button" class="btn btn-success mb-10 new-family-teamwork button__acetar__modal__event" data-event="RevertirConfirmacion" data-value="${codigo}"
                        style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                        <img src="../Resources/svgnuevos/comprobar.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-event="RevertirConfirmacion" data-value="${ codigo}" />
                        <span style="position: relative; float: right;" data-event="RevertirConfirmacion" data-value="${ codigo}">Aceptar</span> 
                    </button>
                </div>
            </div>
        `
    );
};

const renderRevertirEmisionPago = (codigo) => {
    return renderDOM(
        `
            <div class="validar__finiquito m-auto" style="width: 80%;">
                <h3 class="new-family-teamwork pdt-20" style="font-weight: 100; text-align: center;">Revertir Emisión de Finiquito</h3>
                <p class="new-family-teamwork pdt-20 pdb-40" style="font-weight: 100; text-align: center;">
                   Al revertir la emisión del pago,  este quedará automaticamente con el ultimo estado registrado antes de emitir el respectivo pago (debera indicarnos el motivo por el cual revierte la emisión de pago), declara entender la información entregada.
                </p>
                <textarea id="observacion__rev" class="form-control new-family-teamwork mt-10 mb-20" style="font-weight: 100; min-height: 100px;" placeholder="Observaciones y/o Motivo (Obligatorio)"></textarea>
                <div style="text-align: center;">
                    <button type="button" class="btn btn-success mb-10 new-family-teamwork button__acetar__modal__event" data-event="RevertirEmisionPago" data-value="${codigo}"
                        style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                        <img src="../Resources/svgnuevos/comprobar.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-event="RevertirEmisionPago" data-value="${ codigo}" />
                        <span style="position: relative; float: right;" data-event="RevertirEmisionPago" data-value="${ codigo}">Aceptar</span> 
                    </button>
                </div>
            </div>
        `
    );
};

const renderAgregarComentario = (codigo) => {
    return renderDOM(
        `
            
            <h3 class="new-family-teamwork pdt-20" style="font-weight: 100; text-align: center;">Administrar Comentarios</h3>
            <div style="width: 50%; display: inline-block; vertical-align: top;">
                <h4 class="new-family-teamwork ta-center pdt-20 pdb-20 ml-auto mr-auto" style="font-weight: 100; width: 90%;">Comentarios agregados a continuación</h4>
                <div class="container__comments__add">
                    
                </div>
            </div>
            <div class="validar__finiquito" style="width: calc(50% - 20px); display: inline-block; vertical-align: top;">
                <textarea id="observacion__comment" class="form-control new-family-teamwork mt-10 mb-20" style="font-weight: 100; min-height: 400px;" placeholder="Observaciones y/o Motivo (Obligatorio)"></textarea>
                <div style="text-align: center;">
                    <button type="button" class="btn btn-success mb-10 new-family-teamwork button__acetar__modal__event" data-event="AgregarComentario" data-value="${codigo}"
                        style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                        <img src="../Resources/svgnuevos/comprobar.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-event="AgregarComentario" data-value="${ codigo}" />
                        <span style="position: relative; float: right;" data-event="AgregarComentario" data-value="${ codigo}">Aceptar</span> 
                    </button>
                </div>
            </div>
        `
    );
};

const renderComentario = (comment) => {

    const { Html } = comment;

    return renderDOM(
        `
            <p class="new-family-teamwork ml-auto mr-auto" style="font-weight: 100; width: 90%;">
                ${Html}
            </p>
        `
    );
};

const renderButtonFiniquitos = (codigoFiniquito, optCaratulaFiniquito, optDocFiniquito, optHistorialFiniquito, optValidarFiniquito, optAnularFiniquito, optGestionEnvioFiniquito,
                                optRecepcionFiniquitoRegiones, optRecepcionFiniquitoNotaria, optRecepcionFiniquitoStgoFirma, optSolTef, optSolCheque, optSolValeVista, optEnvioLegalizacion, optRecepcionLegalizacion, optConfirmarFiniquito, optPagarFiniquito,
                                optLiquidacionesSueldo, optCrearComplemento, optActualizarMontoAdministrativo, optRevertirValidacion, optRevertirGestionEnvio, optRevertirLegalizacion, optRevertirSolicitudPago, optRevertirConfirmacion, optRevertirEmisionPago,
                                optComentarios, optTerminarFiniquito, optFirmarFiniquito, optReprocesarDocumentosFiniquito, optValidarFinanzas, optRevertirValidacionFinanzas) => {
    return renderDOM(
        `
            ${ optCaratulaFiniquito !== null
                ? optCaratulaFiniquito === 'S'
                    ? ` <a href="${__SERVERHOST}/Bajas/ViewPdf?pdf=Q2FyYXR1bGFGaW5pcXVpdG8=&data=${codigoFiniquito}" target="_blank" class="btn btn-danger new-family-teamwork mb-10" 
                            style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                            <img src="${__SERVERHOST}/Resources/pdf.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;">
                            <span style="position: relative; float: right;">Caratula Finiquito</span> 
                        </a>
                        `
                    : ``
                : ``
            }
            
            ${ optDocFiniquito !== null
                ? optDocFiniquito === 'S'
                    ? `<a href="${__SERVERHOST}/Bajas/ViewPdf?pdf=RG9jRmluaXF1aXRv&data=${codigoFiniquito}" target="_blank" class="btn btn-danger new-family-teamwork mb-10" 
                            style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                            <img src="${__SERVERHOST}/Resources/pdf.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;">
                            <span style="position: relative; float: right;">Documento Finiquito</span> 
                        </a>
                        `
                    : ``
                : ``
            }                       
                        
            ${ optHistorialFiniquito !== null
                ? optHistorialFiniquito === 'S'
                    ? `<button type="button" class="btn btn-create color-fx3 mb-10 new-family-teamwork button__historial__finiquito" data-value="${codigoFiniquito }" 
                            style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                            <img src="${__SERVERHOST}/Resources/svgnuevos/reloj.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-value="${ codigoFiniquito }" >
                            <span style="position: relative; float: right;" data-value="${ codigoFiniquito }">Historial Finiquito</span> 
                        </button>
                        `
                    : ``
                : ``
            }  
                            
            ${ optValidarFiniquito !== null
                ? optValidarFiniquito === 'S'
                    ? `<button type="button" class="btn btn-warning color-fx3 mb-10 new-family-teamwork button__validar__finiquito" data-value="${codigoFiniquito }" 
                            style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                            <img src="${__SERVERHOST}/Resources/svgnuevos/aprobar.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-value="${ codigoFiniquito }" >
                            <span style="position: relative; float: right;" data-value="${ codigoFiniquito }">Validar Finiquito</span> 
                        </button>
                        `
                    : ``
                : ``
            }  

            ${ optAnularFiniquito !== null
                ? optAnularFiniquito === 'S'
                    ? `<button type="button" class="btn btn-danger mb-10 new-family-teamwork button__anular__finiquito" data-value="${codigoFiniquito }" 
                            style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                            <img src="${__SERVERHOST}/Resources/svgnuevos/delete.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-value="${ codigoFiniquito }" >
                            <span style="position: relative; float: right;" data-value="${ codigoFiniquito }">Anular Finiquito</span> 
                        </button>
                        `
                    : ``
                : ``
            }  

            ${ optGestionEnvioFiniquito !== null
                ? optGestionEnvioFiniquito === 'S'
                    ?  `<button type="button" class="btn btn-info mb-10 new-family-teamwork button__gesenvio__finiquito" data-value="${codigoFiniquito}" 
                            style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                            <img src="${__SERVERHOST}/Resources/svgnuevos/enviar.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-value="${ codigoFiniquito}" >
                            <span style="position: relative; float: right;" data-value="${ codigoFiniquito }">Gestionar Envio Finiquito</span> 
                        </button>
                        `
                    : ``
                : ``
            }  

            ${ optRecepcionFiniquitoRegiones !== null
                ? optRecepcionFiniquitoRegiones === 'S'
                    ?  `<button type="button" class="btn btn-success mb-10 new-family-teamwork button__receplegreg__finiquito" data-value="${codigoFiniquito}" 
                            style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                            <img src="${__SERVERHOST}/Resources/svgnuevos/recibir.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-value="${ codigoFiniquito}" >
                            <span style="position: relative; float: right;" data-value="${ codigoFiniquito }">Recepcionar Finiquito Legalizado</span> 
                        </button>
                        `
                    : ``
                : ``
            } 

            ${ optRecepcionFiniquitoNotaria !== null
                ? optRecepcionFiniquitoNotaria === 'S'
                    ? `<button type="button" class="btn btn-success mb-10 new-family-teamwork button__receplegnot__finiquito" data-value="${codigoFiniquito}" 
                            style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                            <img src="${__SERVERHOST}/Resources/svgnuevos/recibir.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-value="${ codigoFiniquito}" >
                            <span style="position: relative; float: right;" data-value="${ codigoFiniquito}">Recepcionar Finiquito Legalizado</span> 
                        </button>
                        `
                    : ``
                : ``
            } 

            ${ optRecepcionFiniquitoStgoFirma !== null
                ? optRecepcionFiniquitoStgoFirma === 'S'
                    ? `<button type="button" class="btn btn-success mb-10 new-family-teamwork button__recepfirm__finiquito" data-value="${codigoFiniquito}" 
                            style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                            <img src="${__SERVERHOST}/Resources/svgnuevos/recibir.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-value="${ codigoFiniquito}" >
                            <span style="position: relative; float: right;" data-value="${ codigoFiniquito}">Recepcionar Firma Trabajador</span> 
                        </button>
                        `
                    : ``
                : ``
            } 

            ${ optEnvioLegalizacion !== null
                ? optEnvioLegalizacion === 'S'
                    ? `<button type="button" class="btn btn-teamwork color-fx3 mb-10 new-family-teamwork button__envioleg__finiquito" data-value="${codigoFiniquito}" 
                            style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                            <img src="${__SERVERHOST}/Resources/svgnuevos/enviar.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-value="${ codigoFiniquito}" >
                            <span style="position: relative; float: right;" data-value="${ codigoFiniquito}">Envio a Legalización</span> 
                        </button>
                        `
                    : ``
                : ``
            }

            ${ optRecepcionLegalizacion !== null
                ? optRecepcionLegalizacion === 'S'
                    ? `<button type="button" class="btn btn-success mb-10 new-family-teamwork button__recepleg__finiquito" data-value="${codigoFiniquito}" 
                            style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                            <img src="${__SERVERHOST}/Resources/svgnuevos/recibir.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-value="${ codigoFiniquito}" >
                            <span style="position: relative; float: right;" data-value="${ codigoFiniquito}">Recepcionar Finiquito Legalizado</span> 
                        </button>
                        `
                    : ``
                : ``
            }

            ${ optSolTef !== null
                ? optSolTef === 'S'
                    ? `<button type="button" class="btn btn-teamwork color-fx3 mb-10 new-family-teamwork button__soltef__finiquito" data-value="${codigoFiniquito}" 
                            style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                            <img src="${__SERVERHOST}/Resources/svgnuevos/tef.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-value="${ codigoFiniquito}" >
                            <span style="position: relative; float: right;" data-value="${ codigoFiniquito}">Solicitar Transferencia</span> 
                        </button>
                        `
                    : ``
                : ``
            } 

            ${ optSolCheque !== null
                ? optSolCheque === 'S'
                    ? `<button type="button" class="btn btn-teamwork color-fx3 mb-10 new-family-teamwork button__solchq__finiquito" data-value="${codigoFiniquito}" 
                            style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                            <img src="${__SERVERHOST}/Resources/svgnuevos/cheque.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-value="${ codigoFiniquito}" >
                            <span style="position: relative; float: right;" data-value="${ codigoFiniquito}">Solicitar Cheque</span> 
                        </button>
                        `
                    : ``
                : ``
            } 

            ${ optSolValeVista !== null
                ? optSolValeVista === 'S'
                    ? `<button type="button" class="btn btn-teamwork color-fx3 mb-10 new-family-teamwork button__solvv__finiquito" data-value="${codigoFiniquito}" 
                            style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                            <img src="${__SERVERHOST}/Resources/svgnuevos/valevista.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-value="${ codigoFiniquito}" >
                            <span style="position: relative; float: right;" data-value="${ codigoFiniquito}">Solicitar Vale Vista</span> 
                        </button>
                        `
                    : ``
                : ``
            } 

            ${ optConfirmarFiniquito !== null
                ? optConfirmarFiniquito === 'S'
                    ? `<button type="button" class="btn btn-warning color-fx3 mb-10 new-family-teamwork button__conffin__finiquito" data-value="${codigoFiniquito}" 
                            style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                            <img src="${__SERVERHOST}/Resources/svgnuevos/aprobar.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-value="${ codigoFiniquito}" >
                            <span style="position: relative; float: right;" data-value="${ codigoFiniquito}">Confirmar Finiquito</span> 
                        </button>
                        `
                    : ``
                : ``
            } 

            ${ optPagarFiniquito !== null
                ? optPagarFiniquito === 'S'
                    ? `<button type="button" class="btn btn-success  mb-10 new-family-teamwork button__pagfin__finiquito" data-value="${codigoFiniquito}" 
                            style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                            <img src="${__SERVERHOST}/Resources/svgnuevos/aprobar.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-value="${ codigoFiniquito}" >
                            <span style="position: relative; float: right;" data-value="${ codigoFiniquito}">Pagar Finiquito</span> 
                        </button>
                        `
                    : ``
                : ``
            } 

             ${ optLiquidacionesSueldo !== null
                ? optLiquidacionesSueldo === 'S'
                    ? `<button type="button" class="btn btn-danger mb-10 new-family-teamwork button__liquidacion__finiquito" data-value="${codigoFiniquito}" 
                            style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                            <img src="${__SERVERHOST}/Resources/pdf.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-value="${ codigoFiniquito}" >
                            <span style="position: relative; float: right;" data-value="${ codigoFiniquito}">Liquidaciones Sueldo</span> 
                        </button>
                        `
                    : ``
                : ``
            } 
            
            ${ optCrearComplemento !== null
                ? optCrearComplemento === 'S'
                    ? `<button type="button" class="btn btn-teamwork color-fx3 mb-10 new-family-teamwork button__crearcomplemento__finiquito" data-value="${codigoFiniquito}" 
                            style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                            <img src="${__SERVERHOST}/Resources/svgnuevos/rompecabezas.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-value="${ codigoFiniquito}" >
                            <span style="position: relative; float: right;" data-value="${ codigoFiniquito}">Crear Complemento</span> 
                        </button>
                        `
                    : ``
                : ``
            } 

            ${ optActualizarMontoAdministrativo !== null
                ? optActualizarMontoAdministrativo === 'S'
                    ? `<button type="button" class="btn btn-warning color-fx3 mb-10 new-family-teamwork button__actgastoadm__finiquito" data-value="${codigoFiniquito}" 
                            style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                            <img src="${__SERVERHOST}/Resources/svgnuevos/editar.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-value="${ codigoFiniquito}" >
                            <span style="position: relative; float: right;" data-value="${ codigoFiniquito}">Actualizar Gasto Administrativo</span> 
                        </button>
                        `
                    : ``
                : ``
            } 

            ${ optRevertirValidacion !== null
                ? optRevertirValidacion === 'S'
                    ? `<button type="button" class="btn btn-anulado color-fx3 mb-10 new-family-teamwork button__revval__finiquito" data-value="${codigoFiniquito}" 
                            style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                            <img src="${__SERVERHOST}/Resources/svgnuevos/revertir.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-value="${ codigoFiniquito}" >
                            <span style="position: relative; float: right;" data-value="${ codigoFiniquito}">Revertir Validación</span> 
                        </button>
                        `
                    : ``
                : ``
            } 

            ${ optRevertirGestionEnvio !== null
                ? optRevertirGestionEnvio === 'S'
                    ? `<button type="button" class="btn btn-anulado color-fx3 mb-10 new-family-teamwork button__revgesenv__finiquito" data-value="${codigoFiniquito}" 
                            style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                            <img src="${__SERVERHOST}/Resources/svgnuevos/revertir.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-value="${ codigoFiniquito}" >
                            <span style="position: relative; float: right;" data-value="${ codigoFiniquito}">Revertir Gestión de Envio</span> 
                        </button>
                        `
                    : ``
                : ``
            } 
    
            ${ optRevertirLegalizacion !== null
                ? optRevertirLegalizacion === 'S'
                    ? `<button type="button" class="btn btn-anulado color-fx3 mb-10 new-family-teamwork button__revleg__finiquito" data-value="${codigoFiniquito}" 
                            style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                            <img src="${__SERVERHOST}/Resources/svgnuevos/revertir.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-value="${ codigoFiniquito}" >
                            <span style="position: relative; float: right;" data-value="${ codigoFiniquito}">Revertir Legalización</span> 
                        </button>
                        `
                    : ``
                : ``
            } 

            
            ${ optRevertirSolicitudPago !== null
                ? optRevertirSolicitudPago === 'S'
                    ? `<button type="button" class="btn btn-anulado color-fx3 mb-10 new-family-teamwork button__revsolpag__finiquito" data-value="${codigoFiniquito}" 
                            style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                            <img src="${__SERVERHOST}/Resources/svgnuevos/revertir.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-value="${ codigoFiniquito}" >
                            <span style="position: relative; float: right;" data-value="${ codigoFiniquito}">Revertir Solicitud de Pago</span> 
                        </button>
                        `
                    : ``
                : ``
            } 


            ${ optRevertirConfirmacion !== null
                ? optRevertirConfirmacion === 'S'
                    ? `<button type="button" class="btn btn-anulado color-fx3 mb-10 new-family-teamwork button__revconf__finiquito" data-value="${codigoFiniquito}" 
                            style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                            <img src="${__SERVERHOST}/Resources/svgnuevos/revertir.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-value="${ codigoFiniquito}" >
                            <span style="position: relative; float: right;" data-value="${ codigoFiniquito}">Revertir Confirmación</span> 
                        </button>
                        `
                    : ``
                : ``
            } 
                
            ${ optRevertirEmisionPago !== null
                ? optRevertirEmisionPago === 'S'
                    ? `<button type="button" class="btn btn-anulado color-fx3 mb-10 new-family-teamwork button__revemipag__finiquito" data-value="${codigoFiniquito}" 
                            style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                            <img src="${__SERVERHOST}/Resources/svgnuevos/revertir.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-value="${ codigoFiniquito}" >
                            <span style="position: relative; float: right;" data-value="${ codigoFiniquito}">Revertir Emisión de Pago</span> 
                        </button>
                        `
                    : ``
                : ``
            } 

            ${ optComentarios !== null
                ? optComentarios === 'S'
                    ? `<button type="button" class="btn btn-create color-fx3 mb-10 new-family-teamwork button__addcomment__finiquito" data-value="${codigoFiniquito}" 
                            style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                            <img src="${__SERVERHOST}/Resources/svgnuevos/editar.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-value="${ codigoFiniquito}" >
                            <span style="position: relative; float: right;" data-value="${ codigoFiniquito}">Administrar Comentario</span> 
                        </button>
                        `
                    : ``
                : ``
            } 

            ${ optTerminarFiniquito !== null
                ? optTerminarFiniquito === 'S'
                    ? `<button type="button" class="btn btn-success mb-10 new-family-teamwork button__terfin__finiquito" data-value="${codigoFiniquito}" 
                            style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                            <img src="${__SERVERHOST}/Resources/svgnuevos/editar.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-value="${ codigoFiniquito}" >
                            <span style="position: relative; float: right;" data-value="${ codigoFiniquito}">Terminar Finiquito</span> 
                        </button>
                        `
                    : ``
                : ``
            } 

            ${ optFirmarFiniquito !== null
                ? optFirmarFiniquito === 'S'
                    ? `<button type="button" class="btn btn-warning color-fx3 mb-10 new-family-teamwork button__firmafin__finiquito" data-value="${codigoFiniquito}" 
                            style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                            <img src="${__SERVERHOST}/Resources/svgnuevos/firmar.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-value="${ codigoFiniquito}" >
                            <span style="position: relative; float: right;" data-value="${ codigoFiniquito}">Firmar Finiquito</span> 
                        </button>
                        `
                    : ``
                : ``
            }

            ${ optReprocesarDocumentosFiniquito !== null
                ? optReprocesarDocumentosFiniquito === 'S'
                    ? `<button optReprocesarDocumentosFiniquito="button" class="btn btn-warning color-fx3 mb-10 new-family-teamwork button__repdocfin__finiquito" data-value="${codigoFiniquito}" 
                            style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                            <img src="${__SERVERHOST}/Resources/pdf.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-value="${ codigoFiniquito}" >
                            <span style="position: relative; float: right;" data-value="${ codigoFiniquito}">Actualizar Docs. Finiquito</span> 
                        </button>
                        `
                    : ``
                : ``
            }

            ${ optValidarFinanzas !== null
                ? optValidarFinanzas === 'S'
                    ? `<button type="button" class="btn btn-warning color-fx3 mb-10 new-family-teamwork button__validarfinanzas__finiquito" data-value="${codigoFiniquito }" 
                            style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                            <img src="${__SERVERHOST}/Resources/svgnuevos/aprobar.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-value="${ codigoFiniquito }" >
                            <span style="position: relative; float: right;" data-value="${ codigoFiniquito }">V°B de Finanzas</span> 
                        </button>
                        `
                    : ``
                : ``
            }  

            ${ optRevertirValidacionFinanzas !== null
                ? optRevertirValidacionFinanzas === 'S'
                    ? `<button type="button" class="btn btn-anulado color-fx3 mb-10 new-family-teamwork button__revvalfinanzas__finiquito" data-value="${codigoFiniquito}" 
                            style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                            <img src="${__SERVERHOST}/Resources/svgnuevos/revertir.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;" data-value="${ codigoFiniquito}" >
                            <span style="position: relative; float: right;" data-value="${ codigoFiniquito}">Revertir V°B</span> 
                        </button>
                        `
                    : ``
                : ``
            } 

        `
    );
};

const renderButtonFiniquitosMasivo = (opciones) => {

    let html = "";
    const { CodigoOpcion, Valor } = opciones;

    const permission = permisos.button.filter(option => option.Key === CodigoOpcion);

    permission.forEach((priv) => {
        html = ButtonCenterBlock(
            {
                Width: '95%',
                Height: '50px',
                Class: `${priv.Clase} ${priv.EventMasive}`,
                Imagen: priv.Resource,
                Name: priv.Name,
                Enable: Valor === 'S' ?
                    priv.Masive
                        ? true
                        : false
                    : false
            }
        );
    });
    
    return renderDOM(html);
};

const renderButtonComplementos = (codigoComplemento, optDocumento) => {
    return renderDOM(
        `
            ${ optDocumento !== null
                ? optDocumento === 'S'
                    ? ` <a href="${__SERVERHOST}/Bajas/ViewPdf?pdf=Q29tcGxlbWVudG9GaW5pcXVpdG8=&data=${codigoComplemento}" target="_blank" class="btn btn-danger new-family-teamwork mb-10" 
                            style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                            <img src="${__SERVERHOST}/Resources/pdf.svg" width="20" height="20" style="position: relative; left: 1%; margin-right: 3px;">
                            <span style="position: relative; float: right;">Complemento</span> 
                        </a>
                        `
                    : ``
                : ``
            }
        `
    );
};

const renderFiniquito = (finiquitos, masive = false) => {

    const { Folio, NombreSolicitud, Creado, CreadoPor, CargoMod, Cargo, Causal, TotalFiniquito, Comentarios, Border, GlyphiconColor, CodigoFiniquito } = finiquitos;
    const {
        OptCaratulaFiniquito,
        OptDocFiniquito,
        OptHistorialFiniquito,
        OptValidarFiniquito,
        OptAnularFiniquito,
        OptGestionEnvioFiniquito,
        OptRecepcionFiniquitoRegiones,
        OptRecepcionFiniquitoNotaria,
        OptRecepcionFiniquitoStgoFirma,
        OptSolTef,
        OptSolCheque,
        OptSolValeVista,
        OptEnvioLegalizacion,
        OptRecepcionLegalizacion,
        OptConfirmarFiniquito,
        OptPagarFiniquito,
        OptLiquidacionesSueldo,
        OptCrearComplemento,
        OptActualizarMontoAdministrativo,
        OptRevertirValidacion,
        OptRevertirGestionEnvio,
        OptRevertirLegalizacion,
        OptRevertirSolicitudPago,
        OptRevertirConfirmacion,
        OptRevertirEmisionPago,
        OptComentarios,
        OptTerminarFiniquito,
        OptFirmarFiniquito,
        OptReprocesarDocumentosFiniquito,
        OptValidarFinanzas,
        OptRevertirValidacionFinanzas
    } = finiquitos;

    return renderDOM(
        `
            <tr class="${Border} br-1-solid-200x3 bb-1-solid-200x3 bt-1-solid-200x3 mb-5" style="${masive === true ? `box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.3);` : `` }" id="content__finiquito__${ Folio }">
                <td style="width: 120px; border-left: none; border-right: none;" class="ta-center">
                    <p class="${GlyphiconColor} new-family-teamwork color-fx3 fw-bold m-auto" style="font-weight: 100; font-size: 20px; width: 80px; height: 80px; display: inline-block; border-radius: 50%; vertical-align: middle; 
                              display: flex; align-items: center; text-align: center;">
                        <span style="width: 100%; text-align: center; display: block;">${ Folio }</span>
                    </p>
                </td>
                <td style="border-left: none; border-right: none;">
                    <a href="${__SERVERHOST}/Finiquitos/?f=${ CodigoFiniquito }" target="_blank" style="width: 100%; display: block;">
                        <h1 class="new-family-teamwork color-100x3" style="font-size: 18px; font-widght: 100;">${ NombreSolicitud }</h1>
                        <p class="new-family-teamwork color-100x3 mt-5neg" style="font-weight: 100;">Creado ${ Creado }</p>
                        <p class="new-family-teamwork color-100x3 mt-15neg" style="font-weight: 100;">Creado por <i class="fw-bold fst-normal">${ CreadoPor }</i></p>
                        <p class="new-family-teamwork color-100x3 mt-15neg" style="font-weight: 100;">CargoMod: ${ CargoMod } • Cargo: ${ Cargo }</p>
                        <p class="new-family-teamwork color-100x3 mt-15neg" style="font-weight: 100;">Causal <i class="fw-bold fst-normal">${ Causal }</i></p>
                        <p class="new-family-teamwork color-100x3 mt-15neg" style="font-weight: 100;">Monto Total Finiquito <i class="fw-bold fst-normal">$ ${ TotalFiniquito }</i></p>
                        <p class="new-family-teamwork color-100x3 mt-15neg" style="font-weight: 100;">Comentarios de Seguimiento: <i class="fw-bold fst-normal">${ Comentarios }</i></p>
                    </a>
                </td>
                <td style="width: 40%; border-left: none; border-right: none; position: relative;">
                    <div>
                        ${
                            renderButtonFiniquitos(
                                CodigoFiniquito,
                                OptCaratulaFiniquito,
                                OptDocFiniquito,
                                OptHistorialFiniquito,
                                OptValidarFiniquito,
                                OptAnularFiniquito,
                                OptGestionEnvioFiniquito,
                                OptRecepcionFiniquitoRegiones,
                                OptRecepcionFiniquitoNotaria,
                                OptRecepcionFiniquitoStgoFirma,
                                OptSolTef,
                                OptSolCheque,
                                OptSolValeVista,
                                OptEnvioLegalizacion,
                                OptRecepcionLegalizacion,
                                OptConfirmarFiniquito,
                                OptPagarFiniquito,
                                OptLiquidacionesSueldo,
                                OptCrearComplemento,
                                OptActualizarMontoAdministrativo,
                                OptRevertirValidacion,
                                OptRevertirGestionEnvio,
                                OptRevertirLegalizacion,
                                OptRevertirSolicitudPago,
                                OptRevertirConfirmacion,
                                OptRevertirEmisionPago,
                                OptComentarios,
                                OptTerminarFiniquito,
                                OptFirmarFiniquito,
                                OptReprocesarDocumentosFiniquito,
                                OptValidarFinanzas,
                                OptRevertirValidacionFinanzas
                           )  
                        }
                    </div>
                    <span style="position: absolute; top: 10px; right: 10px;">
                        <input type="checkbox" class="checkbox__selected__finiquito" style="width: 20px; height: 20px;" ${ masive === true ? `checked="checked"` : ``} id="checkbox__selected__${Folio}" value="${Folio }" />
                    </span>
                </td>
            </tr>
        `
    );
};

const renderFiniquitoDetalle = (finiquitos) => {

    const { Folio, NombreSolicitud, Creado, CreadoPor, CargoMod, Cargo, Causal, TotalFiniquito, Comentarios, Border, GlyphiconColor, CodigoFiniquito } = finiquitos;
    const {
        OptCaratulaFiniquito,
        OptDocFiniquito,
        OptHistorialFiniquito,
        OptValidarFiniquito,
        OptAnularFiniquito,
        OptGestionEnvioFiniquito,
        OptRecepcionFiniquitoRegiones,
        OptRecepcionFiniquitoNotaria,
        OptRecepcionFiniquitoStgoFirma,
        OptSolTef,
        OptSolCheque,
        OptSolValeVista,
        OptEnvioLegalizacion,
        OptRecepcionLegalizacion,
        OptConfirmarFiniquito,
        OptPagarFiniquito,
        OptLiquidacionesSueldo,
        OptCrearComplemento,
        OptActualizarMontoAdministrativo,
        OptRevertirValidacion,
        OptRevertirGestionEnvio,
        OptRevertirLegalizacion,
        OptRevertirSolicitudPago,
        OptRevertirConfirmacion,
        OptRevertirEmisionPago,
        OptComentarios,
        OptTerminarFiniquito,
        OptFirmarFiniquito,
        OptReprocesarDocumentosFiniquito,
        OptValidarFinanzas,
        OptRevertirValidacionFinanzas
    } = finiquitos;

    return renderDOM(
        `
            <h3 class="${GlyphiconColor} pdt-5 pdr-10 pdb-5 pdl-10 new-family-teamwork color-fx3 dspl-inline-block" style="vertical-align: top; font-weight: 100;">${Folio}</h3>
            <h1 class="new-family-teamwork color-100x3" style="font-weight: 100;">${NombreSolicitud}</h1>
            <p class="${GlyphiconColor} pdt-5 pdr-10 pdb-5 pdl-10 new-family-teamwork color-fx3 dspl-inline-block" style=" font-weight: 100;">Creado Por <b>${CreadoPor}</b></p>
            <p class="${GlyphiconColor} pdt-5 pdr-10 pdb-5 pdl-10 new-family-teamwork color-fx3 dspl-inline-block" style=" font-weight: 100;">Creado <b>${Creado}</b></p>
            <div class="row">
                <div class="col-12 col-sm-12 col-md-12 col-lg-6">
                    <p class="new-family-teamwork color-100x3" style="font-size: 18px; font-weight: 100;">CargoMod: ${CargoMod} &bull; Cargo: ${Cargo}</p>
                    <p class="new-family-teamwork color-100x3" style="font-size: 18px; font-weight: 100;">Causal de Desvinculación <i class="fw-bold fst-normal">${Causal}</i></p>
                </div>
                <div class="col-12 col-sm-12 col-md-12 col-lg-6 ta-center">
                    <p class="new-family-teamwork ta-center color-100x3" style="font-size: 18px; font-weight: 100;">Monto Total Finiquito</p>
                    <p class="new-family-teamwork ta-center mt-20neg color-100x3" style="font-size: 40px; font-weight: 100;"><b>$ ${TotalFiniquito}</b></p>
                </div>
            </div>
            <p class="new-family-teamwork ${Border} pdl-5 pdt-5 pdb-5 color-100x3" style="font-size: 18px; font-weight: 100;">Comentarios: ${Comentarios}</p>
            <div>
                ${
                    renderButtonFiniquitos(
                        CodigoFiniquito,
                        OptCaratulaFiniquito,
                        OptDocFiniquito,
                        OptHistorialFiniquito,
                        OptValidarFiniquito,
                        OptAnularFiniquito,
                        OptGestionEnvioFiniquito,
                        OptRecepcionFiniquitoRegiones,
                        OptRecepcionFiniquitoNotaria,
                        OptRecepcionFiniquitoStgoFirma,
                        OptSolTef,
                        OptSolCheque,
                        OptSolValeVista,
                        OptEnvioLegalizacion,
                        OptRecepcionLegalizacion,
                        OptConfirmarFiniquito,
                        OptPagarFiniquito,
                        OptLiquidacionesSueldo,
                        OptCrearComplemento,
                        OptActualizarMontoAdministrativo,
                        OptRevertirValidacion,
                        OptRevertirGestionEnvio,
                        OptRevertirLegalizacion,
                        OptRevertirSolicitudPago,
                        OptRevertirConfirmacion,
                        OptRevertirEmisionPago,
                        OptComentarios,
                        OptTerminarFiniquito,
                        OptFirmarFiniquito,
                        OptReprocesarDocumentosFiniquito,
                        OptValidarFinanzas,
                        OptRevertirValidacionFinanzas
                   )  
                }
            </div>
            <div class="row mt-30">
                <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                    <h1 class="new-family-teamwork color-100x3" style="font-size: 40px; font-weight: 100;">
                        <i class="glyphicon-tw-5 glyphicon-tw-complemento mb-10neg"></i> Complementos de Finiquito
                    </h1>
                </div>
                <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                    <table style="width: 100%;" class="content__complementos__finiquito">
                    </table>
                </div>
            </div>
            <div class="row mt-30">
                <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                    <h1 class="new-family-teamwork color-100x3" style="font-size: 40px; font-weight: 100;">
                        <i class="glyphicon-tw-5 glyphicon-tw-pdfReal mb-10neg"></i> Visualización de Caratula y Documento de finiquito
                    </h1>
                </div>
                <div class="col-12 col-sm-12 col-md-12 col-lg-12" style="height: 900px;">
                    <div class="row">
                        <div class="col-12 col-sm-12 col-md-12 col-lg-6">
                            ${ OptCaratulaFiniquito !== null
                                ? OptCaratulaFiniquito === 'S'
                                    ? `
                                        <embed src="${__SERVERHOST}/Bajas/ViewPdf?pdf=Q2FyYXR1bGFGaW5pcXVpdG8=&data=${CodigoFiniquito}" style="width: 100%; height: 700px;" />
                                        <div style="margin-top: -600px; width: 100%; height: 600px; text-align: center;">
                                            <img src="${__SERVERHOST}/Resources/pdfcolorreal-x2.png" style="width: 50px; display: block; margin: auto;" />
                                            <h1 class="family-teamwork color-100x3">Cargando PDF<br />Espere un momento...</h1>
                                        </div>
                                       `
                                    : ``
                                : ``
                            } 
                            
                        </div>
                        <div class="col-12 col-sm-12 col-md-12 col-lg-6">
                            ${ OptDocFiniquito !== null
                                ? OptDocFiniquito === 'S'
                                    ? `
                                        <embed src="${__SERVERHOST}/Bajas/ViewPdf?pdf=RG9jRmluaXF1aXRv&data=${CodigoFiniquito}" style="width: 100%; height: 700px;" />
                                        <div style="margin-top: -600px; width: 100%; height: 600px; text-align: center;">
                                            <img src="${__SERVERHOST}/Resources/pdfcolorreal-x2.png" style="width: 50px; display: block; margin: auto;" />
                                            <h1 class="family-teamwork color-100x3">Cargando PDF<br />Espere un momento...</h1>
                                        </div>
                                       `
                                    : ``
                                : ``
                            } 
                        </div>
                    </div>
                </div>
            </div>
        `
    );
};

const renderListarComplementos = (complementos, codigo) => {

    const { Border, GlyphiconColor, Comentarios, CreadoPor, Creado, NombreSolicitud, Folio, TotalComplemento, CodigoComplemento } = complementos;

    return renderDOM(
        `
            <tr class="${Border} br-1-solid-200x3 bb-1-solid-200x3 bt-1-solid-200x3">
                <td style="width: 120px; border-left: none; border-right: none;" class="ta-center">
                    <p class="${GlyphiconColor} family-teamwork color-fx3 fw-bold m-auto" style="font-size: 16px; width: 60px; height: 60px; display: inline-block; border-radius: 50%; vertical-align: middle; display: flex; align-items: center; text-align: center;">
                        <span style="width: 100%; text-align: center; display: block;">${Folio}</span>
                    </p>
                </td>
                <td style="border-left: none; border-right: none;">
                    <a href="${__SERVERHOST}/Bajas/Finiquitos/${codigo}/Complementos/${CodigoComplemento}" target="_blank" style="width: 100%; display: block;">
                        <h1 class="new-family-teamwork color-100x3 fw-bold" style="font-size: 18px; font-weight: 100;">${NombreSolicitud}</h1>
                        <p class="new-family-teamwork color-100x3 mt-5neg" style="font-weight: 100;">Liquido a Percibir: <b>$ ${TotalComplemento}</b></p>
                        <p class="new-family-teamwork color-100x3 mt-5neg" style="font-weight: 100;">Creado <b>${Creado}</b> • Creado por <b>${CreadoPor}</b></p>
                        <p class="new-family-teamwork color-100x3 mt-15neg" style="font-weight: 100;">Comentarios de Seguimiento: <i class="fw-bold fst-normal">${Comentarios}</i></p>
                    </a>
                </td>
                <td>
                    ${renderButtonComplementos(CodigoComplemento, 'S')}
                </td>
            </tr>
        `
    );
};

const renderFiniquitoConfigMasive = (finiquitos, confirm = false, process = false) => {
    const { Folio, NombreSolicitud } = finiquitos;

    return renderDOM(
        `
            <tr>
                <td class="pdl-10 pdr-10 pdt-10 pdb-10">${Folio}</td>
                <td class="pdl-10 pdr-10 pdt-10 pdb-10">${NombreSolicitud}</td>
                ${
                    confirm
                        ? `
                            <td class="pdl-10 pdr-10 pdt-10 pdb-10">
                                <button type="button" class="btn btn-danger mb-10 new-family-teamwork button__acetar__modal__event" data-event="" 
                                    style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle;">
                                    <img src="${__SERVERHOST}/Resources/svgnuevos/delete.svg" width="20" height="20" style="position: relative; display: block; left: 20%;"  data-event="" />
                                </button>
                            </td>
                          `
                        : ``
                }
                ${
                    process
                        ? `
                            <td class="pdl-20 pdr-20 pdt-10 pdb-10" id="result__process__resource__finiquito__${Folio}">
                                ${renderLoadingFiniquitoProcess()}
                            </td>
                            <td class="pdl-20 pdr-20 pdt-10 pdb-10">
                                <p class="new-family-teamwork" style="font-weight: 100;" id="result__process__comments__finiquito__${Folio}">Pendiente</p>
                            </div>
                          `
                        : ``
                }
            </tr>
            <tr id="result__process__obs__finiquito__${Folio}">
            </tr>
        `
    );
};

export {
    renderHeaderFiniquitos,
    renderFiniquitoConfigMasive,
    renderFiniquito,
    renderFiniquitoDetalle,
    renderLoadingFiniquito,
    renderHistorialFiniquito,
    renderValidacionFiniquito,
    renderAnulacionFiniquito,
    renderPaginationFiniquitos,
    renderGestionEnvioFiniquito,
    renderGestionEnvioRegion,
    renderGestionEnvioSantiagoNotaria,
    renderGestionEnvioSantiagoFirma,
    renderRecepcionarFiniquitoSantiagoNotaria,
    renderRecepcionarFiniquitoRegiones,
    renderRecepcionarFiniquitoSantiagoFirmaTrabajador,
    renderSolicitarValeVista,
    renderSolicitarCheque,
    renderConsultaDatosTEF,
    renderConsultaDatosTEFNo,
    renderBancosSelect,
    renderLoadingBancosSelect,
    renderBancoSelectHeader,
    renderGestionEnvioLegalizar,
    renderGestionRecepcionLegalizacion,
    renderConsultaSolicitudPago,
    renderPagarFiniquito,
    renderLiquidacionesSueldoYear,
    renderLiquidacionesSueldoMes,
    renderCrearComplemento,
    renderCrearComplementoStageHaberes,
    renderCrearComplementoStageDescuentos,
    renderCrearComplementoStageFechaDocumento,
    renderHaberesComplemento,
    renderDescuentosComplemento,
    renderFormularioHaberes,
    renderListarComplementos,
    renderFormularioDescuentos,
    renderActualizarGastoAdministrativo,
    renderRevertirValidacion,
    renderRevertirGestionEnvio,
    renderRevertirLegalizacion,
    renderRevertirSolicitudPago,
    renderRevertirConfirmacion,
    renderRevertirEmisionPago,
    renderBusquedaBarcodeFiniquitos,
    renderProcesosMasivo,
    renderProcesoMasivoBarcodeFiniquitosNone,
    renderProcesoMasivoBarcodeFiniquitos,
    renderConfirmacionProcesoMasivo,
    renderButtonFiniquitosMasivo,
    renderAgregarComentario,
    renderComentario,
    renderLoadingFiniquitoProcess,
    renderProcesingProcesoMasivo,
    renderProcesoMasivoCargaFile,
    renderButtonReporteProcesoMasivo,
    renderTerminarFiniquito,
    renderFirmarFiniquito,
    renderReprocesarDocumentosFiniquito,
    renderValidacionFinanzas,
    renderRevertirValidacionFinanzas
};