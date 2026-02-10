import { __post } from '../../modules/ajax.js';
import { json } from '../../modules/config.js';
import { handleModalShow, handleModalHidden } from '../../modules/modal.js';
import { handleRenderNotification } from '../../modules/notificacion.js';

import {
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
    renderDescuentosComplemento,
    renderHaberesComplemento,
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
    renderProcesingProcesoMasivo,
    renderProcesoMasivoCargaFile,
    renderButtonReporteProcesoMasivo,
    renderTerminarFiniquito,
    renderFirmarFiniquito,
    renderReprocesarDocumentosFiniquito,
    renderValidacionFinanzas,
    renderRevertirValidacionFinanzas
} from '../finiquitos/render/finiquito.js';

const config = [json][0].config;

const params = new URLSearchParams(config.search);

const __APP = config.app;
const __SERVERHOST = `${config.origin}${__APP}`;

const eventListenerHiddenSelectConfigOpcionFiniquito = () => {
    document.querySelector('html').addEventListener("click", (e) => {
        let hidden = true;
        const classList = [...e.target.classList];
        classList.forEach(properties => {
            if (properties === "selected__opcion__masiva__finiquito") {
                hidden = false;
            }
        });

        if (hidden) {
            if (document.querySelector('.content__options__admin') !== null) {
                document.querySelector('.content__options__admin').style.display = "none";
            }
        }
    });
};

const handleEventMasivoFiniquitosModal = () => {
    let filter = "SeleccionMultiple";
    let dataFilter = "";

    const buttonProcesoMasivos = document.querySelectorAll('.button__acetar__modal__event');

    buttonProcesoMasivos.forEach((event) => {
        event.addEventListener("click", (e) => {
            switch (e.target.dataset.event) {
                case "SeleccionarOpcionMasiva":
                    document.querySelector(".content__options__admin").style.display = "block";
                    break;
            }
        });
    });

    const buttonValidarFiniquitos = document.querySelectorAll('.button__val__finiquito');

    buttonValidarFiniquitos.forEach((event) => {
        event.addEventListener("click", (e) => {
            const masiveFiniquitos = localStorage.getItem("application__masive__finiquitos__temp") !== null
                ? JSON.parse(localStorage.getItem("application__masive__finiquitos__temp"))
                : [];

            masiveFiniquitos.forEach(finiquito => {
                dataFilter += `${(dataFilter !== "" ? `, ` : ``)}${finiquito}`;
            });

            document.querySelector('.content__proceso__masivo__finiquito').innerHTML = renderProcesingProcesoMasivo(masiveFiniquitos.length);

            if (masiveFiniquitos.length > 0) {
                handleFiniquitosConfigMasiveProcess('.content__finiquito__masive', filter, dataFilter, false, true);
            }

            masiveFiniquitos.forEach((finiquito) => {

                handleValidarFiniquito(null, btoa(finiquito), true);

            });
        });
    });

    const buttonRevertirValidacionFiniquito = document.querySelectorAll('.button__revval__finiquito');

    buttonRevertirValidacionFiniquito.forEach((event) => {
        event.addEventListener("click", (e) => {
            const masiveFiniquitos = localStorage.getItem("application__masive__finiquitos__temp") !== null
                ? JSON.parse(localStorage.getItem("application__masive__finiquitos__temp"))
                : [];

            document.querySelector('.content__proceso__masivo__finiquito').innerHTML = renderProcesingProcesoMasivo(masiveFiniquitos.length);

            masiveFiniquitos.forEach(finiquito => {
                dataFilter += `${(dataFilter !== "" ? `, ` : ``)}${finiquito}`;
            });

            if (masiveFiniquitos.length > 0) {
                handleFiniquitosConfigMasiveProcess('.content__finiquito__masive', filter, dataFilter, false, true);
            }

            masiveFiniquitos.forEach((finiquito) => {

                handleValidarFiniquito(null, btoa(finiquito), true);

            });
        });
    });

    const buttonSolicitudTefFiniquito = document.querySelectorAll('.button__soltef__finiquitos');

    buttonSolicitudTefFiniquito.forEach((event) => {
        event.addEventListener("click", (e) => {
            const masiveFiniquitos = localStorage.getItem("application__masive__finiquitos__temp") !== null
                ? JSON.parse(localStorage.getItem("application__masive__finiquitos__temp"))
                : [];

            const masiveGastosAdmFiniquito = localStorage.getItem("application__masive__finiquitos__gastoadm__temp") !== null
                ? JSON.parse(localStorage.getItem("application__masive__finiquitos__gastoadm__temp"))
                : [];

            masiveFiniquitos.forEach(finiquito => {
                dataFilter += `${(dataFilter !== "" ? `, ` : ``)}${finiquito}`;
            });

            document.querySelector('.content__proceso__masivo__finiquito').innerHTML = renderProcesingProcesoMasivo(masiveFiniquitos.length);

            if (masiveFiniquitos.length > 0) {
                handleFiniquitosConfigMasiveProcess('.content__finiquito__masive', filter, dataFilter, false, true);
            }

            masiveFiniquitos.forEach((finiquito) => {

                const resultMasive = masiveGastosAdmFiniquito.filter(finiquitos => finiquitos.Folio === finiquito);
                let gastoAdm = "";

                resultMasive.forEach((result) => {
                    gastoAdm = result.GastoAdm;
                });
                

                handleSolicitudTEF(null, btoa(finiquito), "", "", gastoAdm, "", true);

            });
        });
    });

    const buttonSolicitudChequeFiniquito = document.querySelectorAll('.button__solchq__finiquitos');

    buttonSolicitudChequeFiniquito.forEach((event) => {
        event.addEventListener("click", (e) => {
            const masiveFiniquitos = localStorage.getItem("application__masive__finiquitos__temp") !== null
                ? JSON.parse(localStorage.getItem("application__masive__finiquitos__temp"))
                : [];

            masiveFiniquitos.forEach(finiquito => {
                dataFilter += `${(dataFilter !== "" ? `, ` : ``)}${finiquito}`;
            });

            document.querySelector('.content__proceso__masivo__finiquito').innerHTML = renderProcesingProcesoMasivo(masiveFiniquitos.length);

            if (masiveFiniquitos.length > 0) {
                handleFiniquitosConfigMasiveProcess('.content__finiquito__masive', filter, dataFilter, false, true);
            }

            masiveFiniquitos.forEach((finiquito) => {

                handleSolicitudCheque(null, btoa(finiquito), true);

            });
        });
    });

    const buttonSolicitudValeVistaFiniquito = document.querySelectorAll('.button__solvv__finiquitos');

    buttonSolicitudValeVistaFiniquito.forEach((event) => {
        event.addEventListener("click", (e) => {
            const masiveFiniquitos = localStorage.getItem("application__masive__finiquitos__temp") !== null
                ? JSON.parse(localStorage.getItem("application__masive__finiquitos__temp"))
                : [];

            masiveFiniquitos.forEach(finiquito => {
                dataFilter += `${(dataFilter !== "" ? `, ` : ``)}${finiquito}`;
            });

            document.querySelector('.content__proceso__masivo__finiquito').innerHTML = renderProcesingProcesoMasivo(masiveFiniquitos.length);

            if (masiveFiniquitos.length > 0) {
                handleFiniquitosConfigMasiveProcess('.content__finiquito__masive', filter, dataFilter, false, true);
            }

            masiveFiniquitos.forEach((finiquito) => {

                handleSolicitarValeVista(null, btoa(finiquito), true);

            });
        });
    });

    const buttonRecepcionLegalizacionRegionesFiniquito = document.querySelectorAll('.button__reclegreg__finiquitos');

    buttonRecepcionLegalizacionRegionesFiniquito.forEach((event) => {
        event.addEventListener("click", (e) => {
            const masiveFiniquitos = localStorage.getItem("application__masive__finiquitos__temp") !== null
                ? JSON.parse(localStorage.getItem("application__masive__finiquitos__temp"))
                : [];

            masiveFiniquitos.forEach(finiquito => {
                dataFilter += `${(dataFilter !== "" ? `, ` : ``)}${finiquito}`;
            });

            document.querySelector('.content__proceso__masivo__finiquito').innerHTML = renderProcesingProcesoMasivo(masiveFiniquitos.length);

            if (masiveFiniquitos.length > 0) {
                handleFiniquitosConfigMasiveProcess('.content__finiquito__masive', filter, dataFilter, false, true);
            }

            masiveFiniquitos.forEach((finiquito) => {

                handleGestionRecepcionRegiones(null, btoa(finiquito), true);

            });
        });
    });

    const buttonRecepcionNotaiaFiniquito = document.querySelectorAll('.button__reclegnot__finiquitos');

    buttonRecepcionNotaiaFiniquito.forEach((event) => {
        event.addEventListener("click", (e) => {
            const masiveFiniquitos = localStorage.getItem("application__masive__finiquitos__temp") !== null
                ? JSON.parse(localStorage.getItem("application__masive__finiquitos__temp"))
                : [];

            masiveFiniquitos.forEach(finiquito => {
                dataFilter += `${(dataFilter !== "" ? `, ` : ``)}${finiquito}`;
            });

            document.querySelector('.content__proceso__masivo__finiquito').innerHTML = renderProcesingProcesoMasivo(masiveFiniquitos.length);

            if (masiveFiniquitos.length > 0) {
                handleFiniquitosConfigMasiveProcess('.content__finiquito__masive', filter, dataFilter, false, true);
            }

            masiveFiniquitos.forEach((finiquito) => {

                handleGestionRecepcionSantiagoNotaria(null, btoa(finiquito), true);

            });
        });
    });

    const buttonRecepcionStgoFirmaFiniquito = document.querySelectorAll('.button__recstgofirma__finiquitos');

    buttonRecepcionStgoFirmaFiniquito.forEach((event) => {
        event.addEventListener("click", (e) => {
            const masiveFiniquitos = localStorage.getItem("application__masive__finiquitos__temp") !== null
                ? JSON.parse(localStorage.getItem("application__masive__finiquitos__temp"))
                : [];

            masiveFiniquitos.forEach(finiquito => {
                dataFilter += `${(dataFilter !== "" ? `, ` : ``)}${finiquito}`;
            });

            document.querySelector('.content__proceso__masivo__finiquito').innerHTML = renderProcesingProcesoMasivo(masiveFiniquitos.length);

            if (masiveFiniquitos.length > 0) {
                handleFiniquitosConfigMasiveProcess('.content__finiquito__masive', filter, dataFilter, false, true);
            }

            masiveFiniquitos.forEach((finiquito) => {

                handleGestionRecepcionSantiagoParaFirma(null, btoa(finiquito), true);

            });
        });
    });

    const buttonRecepcionLegalizacionFiniquito = document.querySelectorAll('.button__recleg__finiquitos');

    buttonRecepcionLegalizacionFiniquito.forEach((event) => {
        event.addEventListener("click", (e) => {
            const masiveFiniquitos = localStorage.getItem("application__masive__finiquitos__temp") !== null
                ? JSON.parse(localStorage.getItem("application__masive__finiquitos__temp"))
                : [];

            masiveFiniquitos.forEach(finiquito => {
                dataFilter += `${(dataFilter !== "" ? `, ` : ``)}${finiquito}`;
            });

            document.querySelector('.content__proceso__masivo__finiquito').innerHTML = renderProcesingProcesoMasivo(masiveFiniquitos.length);

            if (masiveFiniquitos.length > 0) {
                handleFiniquitosConfigMasiveProcess('.content__finiquito__masive', filter, dataFilter, false, true);
            }

            masiveFiniquitos.forEach((finiquito) => {

                handleGestionRecepcionLegalizacion(null, btoa(finiquito), true);

            });
        });
    });
};

const handleHeaderFiniquitos = (container) => {

    document.querySelector(container).innerHTML = renderHeaderFiniquitos();
    handleEventFiniquitosHeader();

};

const handleEventModalHeader = () => {
    let filter = "SeleccionMultiple";
    let dataFilter = "";
    let excelFilter = "";

    const inputBarcodeBusqueda = document.querySelectorAll('#barcode__read');

    inputBarcodeBusqueda.forEach((event) => {
        event.addEventListener("change", (e) => {

            document.querySelector('#selected__filter > option[value="Folio"]').setAttribute('selected', 'selected');
            document.querySelector('#data__filter').value = e.target.value.split('FNQ-').join('');

            document.querySelector('.button__buscar__finiquitos').click();

            handleModalHidden(
                document.querySelector(".modal-tw"),
                document.querySelector(".modal-tw .modal-tw-body")
            );

            handleRenderNotification("info", "El Codigo de Barra ha sido leido y buscado en sistema", ".alert__notification");

        });
    });

    const inputBarcodeProcesoMasivo = document.querySelectorAll('#barcode__read__masive');

    inputBarcodeProcesoMasivo.forEach((event) => {
        event.addEventListener("change", (e) => {
            let position = 0;
            let event = "Delete";
            let folio = e.target.value.split('FNQ-').join('');
            
            const masiveFiniquitos = localStorage.getItem("application__masive__finiquitos__temp") !== null
                ? JSON.parse(localStorage.getItem("application__masive__finiquitos__temp"))
                : [];
            
            const resultMasive = masiveFiniquitos.filter(finiquito => finiquito === folio);
            if (resultMasive.length === 0) {
                event = "Add";
            }

            switch (event) {
                case "Add":
                    masiveFiniquitos.push(folio);
                    handleRenderNotification("info", `Ha agregado a la selección masiva el finiquito folio ${folio}`, ".alert__notification");

                    break;
                case "Delete":
                    position = masiveFiniquitos.map(x => x.id).indexOf(folio);

                    masiveFiniquitos.splice(position, 1);

                    handleRenderNotification("warning", `Ha quitado de la selección masiva el finiquito folio ${folio}`, ".alert__notification");

                    break;
            }
            
            localStorage.setItem("application__masive__finiquitos__temp", JSON.stringify(masiveFiniquitos));
            e.target.value = "";

            masiveFiniquitos.forEach(finiquito => {
                dataFilter += `${(dataFilter !== "" ? `, ` : ``)}${finiquito}`;
            });

            if (masiveFiniquitos.length > 0)
            {
                document.querySelector('.content__proceso__masivo__finiquito').innerHTML =
                    masiveFiniquitos.length > 0
                        ? renderProcesoMasivoBarcodeFiniquitos(masiveFiniquitos.length)
                        : renderProcesoMasivoBarcodeFiniquitosNone();

                masiveFiniquitos.forEach(finiquito => {
                    dataFilter += `${(dataFilter !== "" ? `, ` : ``)}${finiquito}`;
                });

                handleFiniquitosConfigMasive('.content__finiquito__masive', filter, dataFilter);
            }
            else
            {
                document.querySelector('.content__proceso__masivo__finiquito').innerHTML = renderProcesoMasivoBarcodeFiniquitosNone();
            }

            handleEventModalHeader();
            document.querySelector('#barcode__read__masive').focus();
        });
    });

    const buttonProcesoMasivos = document.querySelectorAll('.button__acetar__modal__event');

    buttonProcesoMasivos.forEach((event) => {
        event.addEventListener("click", (e) => {
            const masiveFiniquitos = localStorage.getItem("application__masive__finiquitos__temp") !== null
                                        ? JSON.parse(localStorage.getItem("application__masive__finiquitos__temp"))
                                        : [];

            switch (e.target.dataset.event) {
                case "MasivoBarcode":
                    document.querySelector('.content__proceso__masivo__finiquito').innerHTML =
                        masiveFiniquitos.length > 0
                            ? renderProcesoMasivoBarcodeFiniquitos(masiveFiniquitos.length)
                            : renderProcesoMasivoBarcodeFiniquitosNone();

                    masiveFiniquitos.forEach(finiquito => {
                        dataFilter += `${(dataFilter !== "" ? `, ` : ``)}${finiquito}`;
                    });

                    if (masiveFiniquitos.length > 0) {
                        handleFiniquitosConfigMasive('.content__finiquito__masive', filter, dataFilter);
                    }

                    handleEventModalHeader();
                    document.querySelector('#barcode__read__masive').focus();
                    break;
                case "ConfirmarProcesoMasivo":
                    if (masiveFiniquitos.length > 0) {
                        masiveFiniquitos.forEach(finiquito => {
                            dataFilter += `${(dataFilter !== "" ? `, ` : ``)}${finiquito}`;
                            excelFilter += `${(excelFilter !== "" ? `+` : ``)}!${finiquito}!`;
                        });

                        document.querySelector('.content__proceso__masivo__finiquito').innerHTML = renderConfirmacionProcesoMasivo(masiveFiniquitos.length, excelFilter);

                        if (masiveFiniquitos.length > 0) {
                            handleFiniquitosConfigMasive('.content__finiquito__masive', filter, dataFilter, false, true);
                        }

                        handleConfigOpcionesMovimientoMasivos('.content__options__admin', dataFilter);

                        localStorage.removeItem("application__masive__finiquitos__temp__200");
                        localStorage.removeItem("application__masive__finiquitos__temp__500");

                    }
                    else
                    {
                        handleRenderNotification("warning", `No puede continuar porque no tiene ningun finiquito dentro del proceso masivo`, ".alert__notification");
                    }
                    
                    break;
                case "CargaMasiva":
                    document.querySelector('.content__proceso__masivo__finiquito').innerHTML = renderProcesoMasivoCargaFile();
                    handleEventModalHeader();
                    break;
                case "UploadFile":
                    document.querySelector('#uploadFile').click();
                    break;
            }
        });
    });

    const fileUploadChange = document.querySelectorAll('#uploadFile');

    fileUploadChange.forEach((event) => {
        event.addEventListener("change", (e) => {
            const reader = new FileReader();

            reader.onload = function () {

                document.querySelector('.content__proceso__masivo__finiquito').innerHTML = renderLoadingFiniquito('Procesando archivo subido, espere un momento');

                let excelJsonObj = [];
                const fileData = reader.result;
                const workbook = XLSX.read(fileData, { type: 'binary' });

                const sheetIncorporada = "Carga Masiva";
                let sheetExits = false;

                [...workbook.SheetNames].map(x => {
                    if (sheetIncorporada === x) {
                        sheetExits = true;
                    }
                });

                if (sheetExits) {

                    let rowObject = XLSX.utils.sheet_to_row_object_array(workbook.Sheets[sheetIncorporada]);
                    excelJsonObj = rowObject;

                    console.log(excelJsonObj);

                    excelJsonObj.forEach((dato) => {
                        console.log(dato);
                        const { ficha, empresa, folio, gasto_administrativo } = dato;

                        handleFiniquitosConfigMasiveFile(folio, ficha, empresa, gasto_administrativo, excelJsonObj.length);
                    });
                    
                }
                
            };

            reader.readAsBinaryString(e.target.files[0]);
            
        });
    });
};

const handleEventFiniquitosHeader = () => {

    const buttonBuscarFiniquitos = document.querySelectorAll('.button__buscar__finiquitos');

    buttonBuscarFiniquitos.forEach((event) => {
        event.addEventListener("click", (e) => {
            const filter = document.querySelector('#selected__filter');
            const data = document.querySelector('#data__filter');

            handleFiniquitos('.finiquitos', "1-5", filter.value, data.value);
            handlePaginationFiniquitos(".paginationFiniquitos", "1-5", filter.value, data.value);
        });
    });

    const buttonRefreshFiniquitos = document.querySelectorAll('.button__refresh__finiquitos');

    buttonRefreshFiniquitos.forEach((event) => {
        event.addEventListener("click", (e) => {
            handleFiniquitos('.finiquitos', "1-5");
            handlePaginationFiniquitos(".paginationFiniquitos", "1-5");
        });
    });

    const buttonSeleccionMasivaFiniquitos = document.querySelectorAll('.button__filter__masive__finiquitos');

    buttonSeleccionMasivaFiniquitos.forEach((event) => {
        event.addEventListener("click", (e) => {
            let filter = "SeleccionMultiple";
            let dataFilter = "";

            let masive = JSON.parse(localStorage.getItem("application__masive__finiquitos__temp"));

            masive.forEach(finiquito => {
                dataFilter += `${(dataFilter !== "" ? `, ` : ``)}${finiquito}`; 
            });

            handleFiniquitos('.finiquitos', "1-5", dataFilter !== "" ? filter : "", dataFilter);
            handlePaginationFiniquitos(".paginationFiniquitos", "1-5", dataFilter !== "" ? filter : "", dataFilter);
        });
    });

    const buttonSeleccionMasiveDeleted = document.querySelectorAll('.button__deleted__masive__finiquitos');

    buttonSeleccionMasiveDeleted.forEach((event) => {
        event.addEventListener("click", (e) => {
            handleRenderNotification("info", "Se han quitado los finiquitos seleccionados.", ".alert__notification");
            localStorage.removeItem("application__masive__finiquitos__temp");
            localStorage.removeItem("application__masive__finiquitos__gastoadm__temp");
            localStorage.removeItem("application__masive__finiquitos__temp__200");
            localStorage.removeItem("application__masive__finiquitos__temp__500");
            handleFiniquitos('.finiquitos', "1-5");
            handlePaginationFiniquitos(".paginationFiniquitos", "1-5");
        });
    });

    const buttonAdministrarSeleccionMasiva = document.querySelectorAll('.button__filter__masive_admin');

    buttonAdministrarSeleccionMasiva.forEach((event) => {
        event.addEventListener("click", (e) => {
            handleModalShow(
                document.querySelector(".modal-tw"),
                document.querySelector(".modal-tw .modal-tw-body"),
                `
                    <div style="width: 80%;" class="m-auto">
                        <h3 class="pdt-30 new-family-teamwork ta-center" style="font-weight: 100;">Administración Masiva de Finiquitos</h3>
                        <div class="container__admin__masive__finiquitos" style="position: relative;">
                        </div>
                    </div>
                
                `
            );
            let filter = "SeleccionMultiple";
            let dataFilter = "";

            let masive = JSON.parse(localStorage.getItem("application__masive__finiquitos__temp"));

            masive.forEach(finiquito => {
                dataFilter += `${(dataFilter !== "" ? `, ` : ``)}${finiquito}`;
            });

            handleFiniquitosConfigMasive('.container__admin__masive__finiquitos', filter, dataFilter);

        });
    });

    const buttonBusquedaBarcode = document.querySelectorAll('.button__barcodebusqueda__finiquitos');

    buttonBusquedaBarcode.forEach((event) => {
        event.addEventListener("click", e => {
            handleModalShow(
                document.querySelector(".modal-tw"),
                document.querySelector(".modal-tw .modal-tw-body"),
                `
                <div class="barcode__busqueda__finiquito">
                    ${renderBusquedaBarcodeFiniquitos()}
                </div>
                
                `
            );
            document.querySelector('#barcode__read').focus();
            handleEventModalHeader();
        });
    });

    const buttonAdminMasivo = document.querySelectorAll('.button__adminmasivo__finiquitos');

    buttonAdminMasivo.forEach((event) => {
        event.addEventListener("click", (e) => {
            handleModalShow(
                document.querySelector(".modal-tw"),
                document.querySelector(".modal-tw .modal-tw-body"),
                `
                    <div class="content__proceso__masivo__finiquito">
                        ${renderProcesosMasivo()}
                    </div>
                
                `
            );
            handleEventModalHeader();
        });
    });

};

const handleEventModalModal = () => {

    const buttonAceptar = document.querySelectorAll('.button__acetar__modal__event');

    buttonAceptar.forEach((event) => {
        event.addEventListener("click", (e) => {
            switch (e.target.dataset.event) {
                case "GestionEnvioRegionAceptar":
                    const mecanismoEnvio = document.querySelector("#select__metodo__envio");
                    if (mecanismoEnvio.value !== "") {
                        handleGestionEnvioRegiones('.container__gesenv__finiquito', e.target.dataset.value, mecanismoEnvio.value);
                    }
                    else {
                        handleRenderNotification("danger", "Debe seleccionar el metodo de envio a regiones.", ".alert__notification");
                    }
                    break;
                case "EnvioSantiagoNotariaAceptar":
                    handleGestionEnvioSantiagoNotaria('.container__gesenv__finiquito', e.target.dataset.value);
                    break;
                case "EnvioSantiagoFirmaAceptar":
                    const rolCoordinador = document.querySelector("#select__rol__coordinador");
                    const coordinador = document.querySelector("#name__coordinador");
                    if (rolCoordinador.value !== "" && coordinador.value !== "") {
                        handleGestionEnvioSantiagoParaFirma('.container__gesenv__finiquito', e.target.dataset.value, rolCoordinador, coordinador);
                    }
                    else {
                        handleRenderNotification("danger", "Debe Completar los datos con quien se coordino.", ".alert__notification");
                    }
                    break;
                case "SolicitarTef":
                    const banco = document.querySelector('#banco__selected__beneficario');
                    const cuenta = document.querySelector('#cuenta__beneficiario');
                    const gastoAdm = document.querySelector('#gastoadm__tef');
                    const observacion = document.querySelector('#observacion__tef');
                    
                    if (banco.value !== "" && cuenta.value !== "")
                    {
                        handleSolicitudTEF('.container__soltef__finiquito', e.target.dataset.value, cuenta.value, banco.value, gastoAdm.value, observacion.value);
                    }
                    else{
                        handleRenderNotification("danger", "Debe Completar los datos para poder solicitar transferencia.", ".alert__notification");
                    }

                    break;
                case "ConfirmarPago":
                    handleConfirmarProcesoPago('.container__conffin__finiquito', e.target.dataset.value);
                    break;
            }
        });
    });

};

const handleEventModal = () => {

    const buttonAceptar = document.querySelectorAll('.button__acetar__modal__event');

    buttonAceptar.forEach((event) => {
        event.addEventListener("click", (e) => {
            switch (e.target.dataset.event) {
                case "ValidarFiniquito":
                    handleValidarFiniquito('.container__validar__finiquito', e.target.dataset.value);
                    break;
                case "ValidarFinanzas":
                    handleValidarFinanzas('.container__validar__finiquito', e.target.dataset.value);
                    break;
                case "AnularFiniquito":
                    const observacion = document.querySelector("#observacion__anulacion");
                    if (observacion.value !== "") {
                        handleAnularFiniquito('.container__anular__finiquito', e.target.dataset.value, observacion.value);
                    }
                    else
                    {
                        handleRenderNotification("danger", "Debe indicar el motivo de la anulación del finiquito.", ".alert__notification");
                    }
                    break;
                case "GestionEnvioRegion":
                    document.querySelector('.container__gesenv__finiquito').innerHTML = renderGestionEnvioRegion(e.target.dataset.value);
                    handleEventModalModal();
                    break;
                case "EnvioSantiagoNotaria":
                    document.querySelector('.container__gesenv__finiquito').innerHTML = renderGestionEnvioSantiagoNotaria(e.target.dataset.value);
                    handleEventModalModal();
                    break;
                case "EnvioSantiagoFirma":
                    document.querySelector('.container__gesenv__finiquito').innerHTML = renderGestionEnvioSantiagoFirma(e.target.dataset.value);
                    handleEventModalModal();
                    break;
                case "RecepcionarFiniquitoSantiagoNotaria":
                    handleGestionRecepcionSantiagoNotaria('.container__receplegnot__finiquito', e.target.dataset.value);
                    break;
                case "RecepcionarFiniquitoRegiones":
                    handleGestionRecepcionRegiones('.container__recplegreg__finiquito', e.target.dataset.value);
                    break;
                case "RecepcionarFiniquitoSantiagoFirmaTrabajador":
                    handleGestionRecepcionSantiagoParaFirma('.container__recepfirm__finiquito', e.target.dataset.value);
                    break;
                case "SolicitarValeVista":
                    handleSolicitarValeVista('.container__soltef__finiquito', e.target.dataset.value);
                    break;
                case "SolicitarCheque":
                    handleSolicitudCheque('.container__solchq__finiquito', e.target.dataset.value);
                    break;
                case "EditarBeneficiario":
                    document.querySelector('#rut__beneficiario').removeAttribute('disabled');
                    document.querySelector('#name__beneficiario').removeAttribute('disabled');
                    break;
                case "EditarDatosBancariosBeneficiario":
                    document.querySelector('#banco__selected__beneficario').removeAttribute('disabled');
                    document.querySelector('#cuenta__beneficiario').removeAttribute('disabled');
                    break;
                case "GestionEnvioLegalizar":
                    handleGestionEnvioLegalizacion('.container__envioleg__finiquito', e.target.dataset.value);
                    break;
                case "GestionRecepcionLegalizacion":
                    handleGestionRecepcionLegalizacion('.container__recepleg__finiquito', e.target.dataset.value);
                    break;
                case "PagarFiniquito":
                    handlePagarFiniquito('.container__pagfin__finiquito', e.target.dataset.value);
                    break;
                case "TerminarFiniquito":
                    handleTerminarFiniquito('.container__terfin__finiquito', e.target.dataset.value);
                    break;
                case "FirmarFiniquito":
                    handleFirmarFiniquito('.container__firmarfin__finiquito', e.target.dataset.value);
                    break;
                case "ReprocesarDocumentosFiniquito":
                    handleReprocesarDocumentosFiniquito('.container__repdocfin__finiquito', e.target.dataset.value);
                    break;
                case "CrearDocumentoComplemento":
                    handleComplementoCrear('.container__crearcomplemento__finiquito', e.target.dataset.value);
                    break;
                case "AgregarHaberComplemento":
                    const descripcion = document.querySelector('#concepto__descripcion');
                    const monto = document.querySelector('#concepto__monto');
                    if (descripcion.value !== "" && monto.value !== "")
                    {
                        handleComplementoAgregarHaber('.container__agregar__concepto', e.target.dataset.value, monto.value, descripcion.value);
                    }
                    else
                    {
                        handleRenderNotification("danger", "Debe indicar el monto y el haber que incorporara.", ".alert__notification");
                    }
                    break;
                case "EliminarComplementoHaber":
                    handleComplementoEliminarHaber('.content__haberes__complemento', e.target.dataset.complemento, e.target.dataset.value);
                    break;
                case "ContinuarEtapaDescuentosComplemento":
                    handleContainerDescuentos('.container__crearcomplemento__finiquito', e.target.dataset.value);
                    break;
                case "AgregarDescuentoComplemento":
                    const descripcionD = document.querySelector('#concepto__descripcion');
                    const montoD = document.querySelector('#concepto__monto');
                    if (descripcionD.value !== "" && montoD.value !== "") {
                        handleComplementoAgregarDescuento('.container__agregar__concepto', e.target.dataset.value, montoD.value, descripcionD.value);
                    }
                    else {
                        handleRenderNotification("danger", "Debe indicar el monto y el descuento que incorporara.", ".alert__notification");
                    }
                    break;
                case "AnteriorEtapaHaberesComplemento":
                    handleContainerHaberes('.container__crearcomplemento__finiquito', e.target.dataset.value);
                    break;
                case "EliminarComplementoDescuento":
                    handleComplementoEliminarDescuento('.content__descuento__complemento', e.target.dataset.complemento, e.target.dataset.value);
                    break;
                case "ContinuarEtapaFechaComplemento":
                    handleContainerFecha('.container__crearcomplemento__finiquito', e.target.dataset.value);
                    break;
                case "AnteriorEtapaDescuentoComplemento":
                    handleContainerDescuentos('.container__crearcomplemento__finiquito', e.target.dataset.value);
                    break;
                case "FinalizarCreacionDocumento":
                    const fecha = document.querySelector('#fecha__documento');

                    if (fecha.value !== "") {
                        handleComplementoDejarActivoCreado('.container__crearcomplemento__finiquito', e.target.dataset.value, fecha.value);
                    }
                    else
                    {
                        handleRenderNotification("danger", "Debe indicar la fecha del documento", ".alert__notification");
                    }
                    break;
                case "ActualizarGastoAdministrativo":
                    const gastoAdm = document.querySelector('#gastoadm__tef');
                    const observacionAGA = document.querySelector('#observacion__tef');

                    if (gastoAdm.value !== "") {
                        handleActualizarMontoAdministrativo('.container__actgastoadm__finiquito', e.target.dataset.value, observacionAGA.value, gastoAdm.value);
                    }
                    else
                    {
                        handleRenderNotification("danger", "Debe indicar el gasto administrativo", ".alert__notification");
                    }
                    break;
                case "RevertirValidacion":
                    const obsValidacion = document.querySelector('#observacion__rev');
                    if (obsValidacion.value !== "")
                    {
                        handleRevertirValidacion('.container__revval__finiquito', e.target.dataset.value, obsValidacion.value);
                    }
                    else
                    {
                        handleRenderNotification("danger", "Debe indicar el motivo para revertir el finiquito", ".alert__notification");
                    }
                    break;
                case "RevertirValidacionFinanzas":
                    const obsValidacionFinanzas = document.querySelector('#observacion__rev');
                    if (obsValidacionFinanzas.value !== "") {
                        handleRevertirValidacionFinanzas('.container__revval__finiquito', e.target.dataset.value, obsValidacionFinanzas.value);
                    }
                    else {
                        handleRenderNotification("danger", "Debe indicar el motivo para revertir V°B de finanzas", ".alert__notification");
                    }
                    break;
                case "RevertirGestionEnvio":
                    const obsGestionEnvio = document.querySelector('#observacion__rev');
                    if (obsGestionEnvio.value !== "")
                    {
                        handleRevertirGestionEnvio('.container__revgesenv__finiquito', e.target.dataset.value, obsGestionEnvio.value);
                    }
                    else {
                        handleRenderNotification("danger", "Debe indicar el motivo para revertir el finiquito", ".alert__notification");
                    }
                    break;
                case "RevertirLegalizacion":
                    const obsLegalizacion = document.querySelector('#observacion__rev');
                    if (obsLegalizacion.value !== "") {
                        handleRevertirLegalizacion('.container__revleg__finiquito', e.target.dataset.value, obsLegalizacion.value);
                    }
                    else {
                        handleRenderNotification("danger", "Debe indicar el motivo para revertir el finiquito", ".alert__notification");
                    }
                    break;
                case "RevertirSolicitudPago":
                    const obsSolicitudPago = document.querySelector('#observacion__rev');
                    if (obsSolicitudPago.value !== "") {
                        handleRevertirSolicitudPago('.container__revsolpag__finiquito', e.target.dataset.value, obsSolicitudPago.value);
                    }
                    else {
                        handleRenderNotification("danger", "Debe indicar el motivo para revertir el finiquito", ".alert__notification");
                    }
                    break;
                case "RevertirConfirmacion":
                    const obsConfirmacion = document.querySelector('#observacion__rev');
                    if (obsConfirmacion.value !== "") {
                        handleRevertirConfirmacion('.container__revconf__finiquito', e.target.dataset.value, obsConfirmacion.value);
                    }
                    else {
                        handleRenderNotification("danger", "Debe indicar el motivo para revertir el finiquito", ".alert__notification");
                    }
                    break;
                case "RevertirEmisionPago":
                    const obsEmisionpago = document.querySelector('#observacion__rev');
                    if (obsEmisionpago.value !== "") {
                        handleRevertirEmisionPago('.container__revemipag__finiquito', e.target.dataset.value, obsEmisionpago.value);
                    }
                    else {
                        handleRenderNotification("danger", "Debe indicar el motivo para revertir el finiquito", ".alert__notification");
                    }
                    break;
                case "AgregarComentario":
                    const obsComentario = document.querySelector('#observacion__comment');
                    const htmlComentario = obsComentario.value.replace(/\n/g, "<br />");

                    handleActualizarComentariosAnotaciones('.container__addcomment__finiquito', e.target.dataset.value, htmlComentario);
                    
                    break;
            }
        });
    });

};

const handleEventFiniquitos = () => {
    const selectedMasive = document.querySelectorAll('.checkbox__selected__finiquito');

    selectedMasive.forEach((event) => {
        event.addEventListener("click", (e) => {
            let position = 0;
            let event = "";
            const masiveFiniquitos = localStorage.getItem("application__masive__finiquitos__temp") !== null
                ? JSON.parse(localStorage.getItem("application__masive__finiquitos__temp"))
                : [];

            if (e.target.checked) {
                const resultMasive = masiveFiniquitos.filter(finiquito => finiquito === e.target.value);
                if (resultMasive.length === 0) {
                    event = "Add";
                }
            }
            else {
                event = "Delete";
            }

            switch (event) {
                case "Add":
                    document.querySelector(`#content__finiquito__${e.target.value}`).style.boxShadow = "0px 0px 10px rgba(0, 0, 0, 0.3)";
                    masiveFiniquitos.push(e.target.value);

                    handleRenderNotification("info", `Ha agregado a la selección masiva el finiquito folio ${e.target.value}`, ".alert__notification");

                    break;
                case "Delete":
                    document.querySelector(`#content__finiquito__${e.target.value}`).style.boxShadow = "none";
                    position = masiveFiniquitos.map(x => x.id).indexOf(e.target.value);

                    masiveFiniquitos.splice(position, 1);

                    handleRenderNotification("warning", `Ha quitado de la selección masiva el finiquito folio ${e.target.value}`, ".alert__notification");

                    break;
                default:
                    document.querySelector(`#content__finiquito__${e.target.value}`).style.boxShadow = "0px 0px 10px rgba(0, 0, 0, 0.3)";
                    break;
            }


            localStorage.setItem("application__masive__finiquitos__temp", JSON.stringify(masiveFiniquitos));
        });
    });

    const buttonHistorial = document.querySelectorAll('.button__historial__finiquito');

    buttonHistorial.forEach((event) => {
        event.addEventListener("click", (e) => {
            handleModalShow(
                document.querySelector(".modal-tw"),
                document.querySelector(".modal-tw .modal-tw-body"),
                `
                <div class="historial__finiquito">
                </div>
                
                `
            );
            handleHistorialFiniquito('.historial__finiquito', e.target.dataset.value);
        });
    });

    const buttonValidarFiniquito = document.querySelectorAll('.button__validar__finiquito');

    buttonValidarFiniquito.forEach((event) => {
        event.addEventListener("click", (e) => {
            handleModalShow(
                document.querySelector(".modal-tw"),
                document.querySelector(".modal-tw .modal-tw-body"),
                `
                    <div class="container__validar__finiquito">
                        ${renderValidacionFiniquito(e.target.dataset.value)}
                    </div>
                `
            );
            handleEventModal();
        });
    });

    const buttonValidarFinanzas = document.querySelectorAll('.button__validarfinanzas__finiquito');

    buttonValidarFinanzas.forEach((event) => {
        event.addEventListener("click", (e) => {
            handleModalShow(
                document.querySelector(".modal-tw"),
                document.querySelector(".modal-tw .modal-tw-body"),
                `
                    <div class="container__validar__finiquito">
                        ${renderValidacionFinanzas(e.target.dataset.value)}
                    </div>
                `
            );
            handleEventModal();
        });
    });

    const buttonAnularFiniquito = document.querySelectorAll('.button__anular__finiquito');

    buttonAnularFiniquito.forEach((event) => {
        event.addEventListener("click", (e) => {
            handleModalShow(
                document.querySelector(".modal-tw"),
                document.querySelector(".modal-tw .modal-tw-body"),
                `
                    <div class="container__anular__finiquito">
                        ${renderAnulacionFiniquito(e.target.dataset.value)}
                    </div>
                `
            );
            handleEventModal();
        });
    });

    const buttonGestionarFiniquito = document.querySelectorAll('.button__gesenvio__finiquito');

    buttonGestionarFiniquito.forEach((event) => {
        event.addEventListener("click", (e) => {
            handleModalShow(
                document.querySelector(".modal-tw"),
                document.querySelector(".modal-tw .modal-tw-body"),
                `
                    <div class="container__gesenv__finiquito">
                        ${renderGestionEnvioFiniquito(e.target.dataset.value)}
                    </div>
                
                `
            );
            handleEventModal();
        });
    });

    const buttonRecepcionarLegalizarRegiones = document.querySelectorAll('.button__receplegreg__finiquito');

    buttonRecepcionarLegalizarRegiones.forEach((event) => {
        event.addEventListener("click", (e) => {
            handleModalShow(
                document.querySelector(".modal-tw"),
                document.querySelector(".modal-tw .modal-tw-body"),
                `
                    <div class="container__recplegreg__finiquito">
                        ${renderRecepcionarFiniquitoRegiones(e.target.dataset.value)}
                    </div>
                
                `
            );
            handleEventModal();
        });
    });

    const buttonRecepcionarLegalizarNotaria = document.querySelectorAll('.button__receplegnot__finiquito');

    buttonRecepcionarLegalizarNotaria.forEach((event) => {
        event.addEventListener("click", (e) => {
            handleModalShow(
                document.querySelector(".modal-tw"),
                document.querySelector(".modal-tw .modal-tw-body"),
                `
                    <div class="container__receplegnot__finiquito">
                        ${renderRecepcionarFiniquitoSantiagoNotaria(e.target.dataset.value)}
                    </div>
                
                `
            );
            handleEventModal();
        });
    });

    const buttonRecepcionarLegalizarStgoFirma = document.querySelectorAll('.button__recepfirm__finiquito');

    buttonRecepcionarLegalizarStgoFirma.forEach((event) => {
        event.addEventListener("click", (e) => {
            handleModalShow(
                document.querySelector(".modal-tw"),
                document.querySelector(".modal-tw .modal-tw-body"),
                `
                    <div class="container__recepfirm__finiquito">
                        ${renderRecepcionarFiniquitoSantiagoFirmaTrabajador(e.target.dataset.value)}
                    </div>
                
                `
            );
            handleEventModal();
        });
    });

    const buttonSolicitarTEF = document.querySelectorAll('.button__soltef__finiquito');

    buttonSolicitarTEF.forEach((event) => {
        event.addEventListener("click", (e) => {
            handleModalShow(
                document.querySelector(".modal-tw"),
                document.querySelector(".modal-tw .modal-tw-body"),
                `
                    <div class="container__soltef__finiquito">
                    </div>
                
                `
            );
            handleConsultaDatosTEF('.container__soltef__finiquito', e.target.dataset.value);
            handleEventModal();
        });
    });
    
    const buttonSolicitarVV = document.querySelectorAll('.button__solvv__finiquito');

    buttonSolicitarVV.forEach((event) => {
        event.addEventListener("click", (e) => {
            handleModalShow(
                document.querySelector(".modal-tw"),
                document.querySelector(".modal-tw .modal-tw-body"),
                `
                    <div class="container__soltef__finiquito">
                        ${renderSolicitarValeVista(e.target.dataset.value)}
                    </div>
                
                `
            );
            handleEventModal();
        });
    });

    const buttonSolicitarCHQ = document.querySelectorAll('.button__solchq__finiquito');

    buttonSolicitarCHQ.forEach((event) => {
        event.addEventListener("click", (e) => {
            handleModalShow(
                document.querySelector(".modal-tw"),
                document.querySelector(".modal-tw .modal-tw-body"),
                `
                    <div class="container__solchq__finiquito">
                        ${renderSolicitarCheque(e.target.dataset.value)}
                    </div>
                
                `
            );
            handleEventModal();
        });
    });

    const buttonEnvioLegalizacion = document.querySelectorAll('.button__envioleg__finiquito');

    buttonEnvioLegalizacion.forEach((event) => {
        event.addEventListener("click", (e) => {
            handleModalShow(
                document.querySelector(".modal-tw"),
                document.querySelector(".modal-tw .modal-tw-body"),
                `
                    <div class="container__envioleg__finiquito">
                        ${renderGestionEnvioLegalizar(e.target.dataset.value)}
                    </div>
                
                `
            );
            handleEventModal();
        });
    });

    const buttonRecepcionLegalizacion = document.querySelectorAll('.button__recepleg__finiquito');

    buttonRecepcionLegalizacion.forEach((event) => {
        event.addEventListener("click", (e) => {
            handleModalShow(
                document.querySelector(".modal-tw"),
                document.querySelector(".modal-tw .modal-tw-body"),
                `
                    <div class="container__recepleg__finiquito">
                        ${renderGestionRecepcionLegalizacion(e.target.dataset.value)}
                    </div>
                
                `
            );
            handleEventModal();
        });
    });

    const buttonConfirmarFiniquito = document.querySelectorAll('.button__conffin__finiquito');

    buttonConfirmarFiniquito.forEach((event) => {
        event.addEventListener("click", (e) => {
            handleModalShow(
                document.querySelector(".modal-tw"),
                document.querySelector(".modal-tw .modal-tw-body"),
                `
                    <div class="container__conffin__finiquito">
                    </div>
                
                `,
                false
            );
            handleConsultaSolicitudPago('.container__conffin__finiquito', e.target.dataset.value);
            handleEventModal();
        });
    });

    const buttonPagarFiniquito = document.querySelectorAll('.button__pagfin__finiquito');

    buttonPagarFiniquito.forEach((event) => {
        event.addEventListener("click", (e) => {
            handleModalShow(
                document.querySelector(".modal-tw"),
                document.querySelector(".modal-tw .modal-tw-body"),
                `
                    <div class="container__pagfin__finiquito">
                        ${renderPagarFiniquito(e.target.dataset.value)}
                    </div>
                
                `
            );
            handleEventModal();
        });
    });

    const buttonTerminarFiniquito = document.querySelectorAll('.button__terfin__finiquito');

    buttonTerminarFiniquito.forEach((event) => {
        event.addEventListener("click", (e) => {
            handleModalShow(
                document.querySelector(".modal-tw"),
                document.querySelector(".modal-tw .modal-tw-body"),
                `
                    <div class="container__terfin__finiquito">
                        ${renderTerminarFiniquito(e.target.dataset.value)}
                    </div>
                
                `
            );
            handleEventModal();
        });
    });

    const buttonFirmarFiniquito = document.querySelectorAll('.button__firmafin__finiquito');

    buttonFirmarFiniquito.forEach((event) => {
        event.addEventListener("click", (e) => {
            handleModalShow(
                document.querySelector(".modal-tw"),
                document.querySelector(".modal-tw .modal-tw-body"),
                `
                    <div class="container__firmarfin__finiquito">
                        ${renderFirmarFiniquito(e.target.dataset.value)}
                    </div>
                
                `
            );
            handleEventModal();
        });
    });

    const buttonReprocesarDocumentosFiniquito = document.querySelectorAll('.button__repdocfin__finiquito');

    buttonReprocesarDocumentosFiniquito.forEach((event) => {
        event.addEventListener("click", (e) => {
            handleModalShow(
                document.querySelector(".modal-tw"),
                document.querySelector(".modal-tw .modal-tw-body"),
                `
                    <div class="container__repdocfin__finiquito">
                        ${renderReprocesarDocumentosFiniquito(e.target.dataset.value)}
                    </div>
                
                `
            );
            handleEventModal();
        });
    });

    const buttonLiquidacionesSueldo = document.querySelectorAll('.button__liquidacion__finiquito');

    buttonLiquidacionesSueldo.forEach((event) => {
        event.addEventListener("click", (e) => {
            handleModalShow(
                document.querySelector(".modal-tw"),
                document.querySelector(".modal-tw .modal-tw-body"),
                `
                    <div class="container__liquidaciones__finiquito">
                    </div>
                
                `
            );
            handleLiquidacionesSueldoYear('.container__liquidaciones__finiquito', e.target.dataset.value);
        });
    });

    const buttonCrearComplemento = document.querySelectorAll('.button__crearcomplemento__finiquito');

    buttonCrearComplemento.forEach((event) => {
        event.addEventListener("click", (e) => {
            handleModalShow(
                document.querySelector(".modal-tw"),
                document.querySelector(".modal-tw .modal-tw-body"),
                `
                    <div class="container__crearcomplemento__finiquito">
                        ${renderCrearComplemento(e.target.dataset.value)}
                    </div>
                
                `
            );
            handleEventModal();
        });
    });

    const buttonActualizarGastoAdm = document.querySelectorAll('.button__actgastoadm__finiquito');

    buttonActualizarGastoAdm.forEach((event) => {
        event.addEventListener("click", (e) => {
            handleModalShow(
                document.querySelector(".modal-tw"),
                document.querySelector(".modal-tw .modal-tw-body"),
                `
                    <div class="container__actgastoadm__finiquito">
                        ${renderActualizarGastoAdministrativo(e.target.dataset.value)}
                    </div>
                
                `
            );
            handleEventModal();
        });
    });

    const buttonRevertirValidacion = document.querySelectorAll('.button__revval__finiquito');

    buttonRevertirValidacion.forEach((event) => {
        event.addEventListener("click", (e) => {
            handleModalShow(
                document.querySelector(".modal-tw"),
                document.querySelector(".modal-tw .modal-tw-body"),
                `
                    <div class="container__revval__finiquito">
                        ${renderRevertirValidacion(e.target.dataset.value)}
                    </div>
                
                `
            );
            handleEventModal();
        });
    });

    const buttonRevertirValidacionFinanzas = document.querySelectorAll('.button__revvalfinanzas__finiquito');

    buttonRevertirValidacionFinanzas.forEach((event) => {
        event.addEventListener("click", (e) => {
            handleModalShow(
                document.querySelector(".modal-tw"),
                document.querySelector(".modal-tw .modal-tw-body"),
                `
                    <div class="container__revval__finiquito">
                        ${renderRevertirValidacionFinanzas(e.target.dataset.value)}
                    </div>
                
                `
            );
            handleEventModal();
        });
    });

    const buttonRevertirGestionEnvio = document.querySelectorAll('.button__revgesenv__finiquito');

    buttonRevertirGestionEnvio.forEach((event) => {
        event.addEventListener("click", (e) => {
            handleModalShow(
                document.querySelector(".modal-tw"),
                document.querySelector(".modal-tw .modal-tw-body"),
                `
                    <div class="container__revgesenv__finiquito">
                        ${renderRevertirGestionEnvio(e.target.dataset.value)}
                    </div>
                
                `
            );
            handleEventModal();
        });
    });

    const buttonRevertirLegalizacion = document.querySelectorAll('.button__revleg__finiquito');

    buttonRevertirLegalizacion.forEach((event) => {
        event.addEventListener("click", (e) => {
            handleModalShow(
                document.querySelector(".modal-tw"),
                document.querySelector(".modal-tw .modal-tw-body"),
                `
                    <div class="container__revleg__finiquito">
                        ${renderRevertirLegalizacion(e.target.dataset.value)}
                    </div>
                
                `
            );
            handleEventModal();
        });
    });

    const buttonRevertirSolicitudPago = document.querySelectorAll('.button__revsolpag__finiquito');

    buttonRevertirSolicitudPago.forEach((event) => {
        event.addEventListener("click", (e) => {
            handleModalShow(
                document.querySelector(".modal-tw"),
                document.querySelector(".modal-tw .modal-tw-body"),
                `
                    <div class="container__revsolpag__finiquito">
                        ${renderRevertirSolicitudPago(e.target.dataset.value)}
                    </div>
                
                `
            );
            handleEventModal();
        });
    });

    const buttonRevertirConfirmacion = document.querySelectorAll('.button__revconf__finiquito');

    buttonRevertirConfirmacion.forEach((event) => {
        event.addEventListener("click", (e) => {
            handleModalShow(
                document.querySelector(".modal-tw"),
                document.querySelector(".modal-tw .modal-tw-body"),
                `
                    <div class="container__revconf__finiquito">
                        ${renderRevertirConfirmacion(e.target.dataset.value)}
                    </div>
                
                `
            );
            handleEventModal();
        });
    });

    const buttonRevertirEmisionPago = document.querySelectorAll('.button__revemipag__finiquito');

    buttonRevertirEmisionPago.forEach((event) => {
        event.addEventListener("click", (e) => {
            handleModalShow(
                document.querySelector(".modal-tw"),
                document.querySelector(".modal-tw .modal-tw-body"),
                `
                    <div class="container__revemipag__finiquito">
                        ${renderRevertirEmisionPago(e.target.dataset.value)}
                    </div>
                
                `
            );
            handleEventModal();
        });
    });

    const buttonAgregarComentario = document.querySelectorAll('.button__addcomment__finiquito');

    buttonAgregarComentario.forEach((event) => {
        event.addEventListener("click", (e) => {
            handleModalShow(
                document.querySelector(".modal-tw"),
                document.querySelector(".modal-tw .modal-tw-body"),
                `
                    <div class="container__addcomment__finiquito">
                        ${renderAgregarComentario(e.target.dataset.value)}
                    </div>
                
                `,
                false
            );
            handleConsultaComentariosAnotaciones('.container__comments__add', e.target.dataset.value);
            handleEventModal();
        });
    });


};

const handleEventPaginationFiniquitos = () => {

    const buttonPagination = document.querySelectorAll('.button__pagination__finiquitos');

    buttonPagination.forEach((event) => {
        event.addEventListener("click", e => {
            handleFiniquitos(".finiquitos", e.target.dataset.page, e.target.dataset.filter, e.target.dataset.dfilter);
            handlePaginationFiniquitos(".paginationFiniquitos", e.target.dataset.page, e.target.dataset.filter, e.target.dataset.dfilter);
        });
    });

};

const handleFiniquitos = (container, pagination = '', filter = '', dataFilter = '') => {

    /* Loading finiquitos */
    document.querySelector(container).innerHTML = renderLoadingFiniquito("Cargando Finiquitos, espere un momento");

    __post(`${__SERVERHOST}/Finiquitos/FiniquitosConsultaFiniquitos`, {
        pagination: pagination,
        filter: filter,
        dataFilter: dataFilter
    })
    .then(resolve => {  

        let html = "";
        const masiveFiniquitos = localStorage.getItem("application__masive__finiquitos__temp") !== null
            ? JSON.parse(localStorage.getItem("application__masive__finiquitos__temp"))
            : [];

        if (resolve.length > 0) {
            resolve.forEach((finiquitos) => {
                let masive = false;

                if (masiveFiniquitos.length > 0) {

                    const resultMasive = masiveFiniquitos.filter(finiquito => finiquito === finiquitos.Folio);

                    if (resultMasive.length > 0) {
                        masive = true;
                    }
                }
                
                html += filter !== 'CodigoSolicitud' ? renderFiniquito(finiquitos, masive) : renderFiniquitoDetalle(finiquitos);
            });
        }
        
        document.querySelector(container).innerHTML = filter !== 'CodigoSolicitud' ?
            `
            <table border="0">
                <tbody>
                    ${html}
                </tbody>
            </table>
            ` : html;

        if (filter === 'CodigoSolicitud') {
            handleConsultarComplementos(
                '.content__complementos__finiquito',
                '1-999',
                'Folio',
                params.get('f')
            );
        }
        

        handleEventFiniquitos();
    })
    .catch(reject => {
        document.querySelector(container).innerHTML = '';
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });
};

const handlePaginationFiniquitos = (container, pagination = null, filter = null, dataFilter = null) => {
    document.querySelector(container).innerHTML = renderLoadingFiniquito("Cargando Paginación Finiquitos, espere un momento");

    __post(`${__SERVERHOST}/Finiquitos/FiniquitosPaginationFiniquitos`, {
        pagination: pagination,
        filter: filter,
        dataFilter: dataFilter
    })
    .then(resolve => {  

        let html = "";

        if (resolve.length > 0) {
            resolve.forEach((page) => {
                html += renderPaginationFiniquitos(page, "finiquitos");
            });
        }
        
        document.querySelector(container).innerHTML = html;
        handleEventPaginationFiniquitos();
        
    })
    .catch(reject => {
        document.querySelector(container).innerHTML = '';
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });
};

const handleFiniquitosConfigMasive = (container, filter, dataFilter, confirm = false) => {
    /* Loading finiquitos */
    document.querySelector(container).innerHTML = renderLoadingFiniquito("Cargando Finiquitos Seleccionados, espere un momento");

    __post(`${__SERVERHOST}/Finiquitos/FiniquitosConsultaFiniquitos`, {
        pagination: "1-999999999",
        filter: filter,
        dataFilter: dataFilter
    })
    .then(resolve => {  

        let html = "";

        if (resolve.length > 0) {
            resolve.forEach((finiquitos) => {
                html += renderFiniquitoConfigMasive(finiquitos, confirm);
            });
        }
        
        document.querySelector(container).innerHTML = html;

    })
    .catch(reject => {
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });
};

const handleFiniquitosConfigMasiveProcess = (container, filter, dataFilter, confirm = false, process = false) => {
    /* Loading finiquitos */
    document.querySelector(container).innerHTML = renderLoadingFiniquito("Cargando Finiquitos Seleccionados, espere un momento");

    __post(`${__SERVERHOST}/Finiquitos/FiniquitosConsultaFiniquitos`, {
        pagination: "1-999999999",
        filter: filter,
        dataFilter: dataFilter
    })
    .then(resolve => {  

        let html = "";

        if (resolve.length > 0) {
            resolve.forEach((finiquitos) => {
                html += renderFiniquitoConfigMasive(finiquitos, confirm, process);
            });
        }
        
        document.querySelector(container).innerHTML = html;

    })
    .catch(reject => {
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });
};

const handleFiniquitosConfigMasiveFile = (folio = "", ficha = "", empresa = "", gastoAdm = "", length = 0) => {

    let filter = "";
    let dataFilter = "";
    let excelFilter = "";

    if (folio !== "") {
        filter = "Folio";
        dataFilter = folio;
    }
    else
    {
        dataFilter = ficha + '-' + empresa;
        filter = 'FichaEmpresa';
    }
    
    __post(`${__SERVERHOST}/Finiquitos/FiniquitosConsultaFiniquitos`, {
        pagination: '1-99999999',
        filter: filter,
        dataFilter: dataFilter
    })
    .then(resolve => {  
        let filter = "SeleccionMultiple";
        let dataFilter = "";

        const masiveGastosAdmFiniquito = localStorage.getItem("application__masive__finiquitos__gastoadm__temp") !== null
            ? JSON.parse(localStorage.getItem("application__masive__finiquitos__gastoadm__temp"))
            : [];

        const masiveFiniquitos = localStorage.getItem("application__masive__finiquitos__temp") !== null
            ? JSON.parse(localStorage.getItem("application__masive__finiquitos__temp"))
            : [];

        if (resolve.length > 0) {
            resolve.forEach((finiquitos) => {
                const { Folio } = finiquitos;

                let event = "";

                const resultMasive = masiveFiniquitos.filter(finiquito => finiquito === Folio);
                if (resultMasive.length === 0) {
                    event = "Add";
                }

                switch (event) {
                    case "Add":
                        masiveFiniquitos.push(Folio);

                        if (gastoAdm !== "") {

                            masiveGastosAdmFiniquito.push({ Folio: Folio, GastoAdm: gastoAdm });

                            localStorage.setItem("application__masive__finiquitos__gastoadm__temp", JSON.stringify(masiveGastosAdmFiniquito));
                        }

                        break;
                }
                
                localStorage.setItem("application__masive__finiquitos__temp", JSON.stringify(masiveFiniquitos));
                
            });
        }
        
        if (masiveFiniquitos.length === length) {
            if (masiveFiniquitos.length > 0) {
                masiveFiniquitos.forEach(finiquito => {
                    dataFilter += `${(dataFilter !== "" ? `, ` : ``)}${finiquito}`;
                    excelFilter += `${(excelFilter !== "" ? `+` : ``)}!${finiquito}!`;
                });

                document.querySelector('.content__proceso__masivo__finiquito').innerHTML = renderConfirmacionProcesoMasivo(length, excelFilter);
                handleEventMasivoFiniquitosModal();
                
                if (masiveFiniquitos.length > 0) {
                    handleFiniquitosConfigMasive('.content__finiquito__masive', filter, dataFilter, true);
                }

                handleConfigOpcionesMovimientoMasivos('.content__options__admin', dataFilter);

            }
            else {
                handleRenderNotification("warning", `No puede continuar porque no tiene ningun finiquito dentro del proceso masivo`, ".alert__notification");
                document.querySelector('.content__proceso__masivo__finiquito').innerHTML = renderProcesoMasivoCargaFile();
                handleEventModalHeader();
            }
        }
    })
    .catch(reject => {
        console.log(reject);
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });

};

/** Modulos de eventos de finiquito */

const handleHistorialFiniquito = (container, codigo = "") => {

    document.querySelector(container).innerHTML = renderLoadingFiniquito("Cargando Historial, espere un momento");

    __post(`${__SERVERHOST}/Finiquitos/FiniquitosHistorialFiniquitos`, {
        codigo: codigo
    })
    .then(resolve => {  

        let html = "";

        if (resolve.length > 0) {
            resolve.forEach((historial) => {
                html += renderHistorialFiniquito(historial);
            });
        }
        
        document.querySelector(container).innerHTML =
            `
                <h3 class="new-family-teamwork pdt-20" style="font-weight: 100; text-align: center;">Historial Finiquito</h3>
                <h1 class="new-family-teamwork pdb-20" style="font-weight: 100; text-align: center;">${ atob(codigo) }</h1>
                <table border="1" class="new-family-teamwork m-auto br-1-solid-200x3 bb-1-solid-200x3 bt-1-solid-200x3 bl-1-solid-200x3" style="width: 95%; font-weight: 100; text-align: center;">
                    <thead>
                        <tr>
                            <td>Fecha</td>
                            <td>Usuario</td>
                            <td>Comentarios</td>
                        </tr>    
                    </thead>
                    <tbody>
                        ${ html }
                    </tbody>
                </table>
            `;
    })
    .catch(reject => {
        document.querySelector(container).innerHTML = '';
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });
};

const handleValidarFiniquito = (container, codigo = "", masive = false) => {

    if (!masive) {
        document.querySelector(container).innerHTML = renderLoadingFiniquito("Validando finiquito, espere un momento");
    }

    __post(`${__SERVERHOST}/Finiquitos/FiniquitosValidarFiniquito`, {
        codigo: codigo
    })
    .then(resolve => {  
        
        if (resolve.length > 0) {
            resolve.forEach((validacion) => {
                if (!masive) {

                    const { Code, Message } = validacion;

                    handleRenderNotification(Code === "200" ? "success" : "danger", Message, ".alert__notification");

                    if (Code === "200") {
                        const filter = document.querySelector('#selected__filter');
                        const dataFilter = document.querySelector('#data__filter');

                        handleFiniquitos(
                            '.finiquitos',
                            '1-5',
                            params.get('f') === null
                                ? filter.value
                                : "CodigoSolicitud"
                            ,
                            params.get('f') === null
                                ? dataFilter.value
                                : params.get('f')
                        );

                        if (params.get('f') === null) {
                            handlePaginationFiniquitos(
                                '.paginationFiniquitos',
                                '1-5',
                                filter.value,
                                dataFilter.value
                            );
                        }

                        handleModalHidden(
                            document.querySelector(".modal-tw"),
                            document.querySelector(".modal-tw .modal-tw-body")
                        );

                    }
                    else {
                        document.querySelector(container).innerHTML =
                            `${renderValidacionFiniquito(codigo)}`;

                        handleEventModal();
                    }
                }
                else
                {
                    handleResultMasive(codigo, validacion);
                }
            });
        }
        
    })
    .catch(reject => {
        document.querySelector(container).innerHTML = renderValidacionFiniquito(codigo);
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });

};

const handleValidarFinanzas = (container, codigo = "", masive = false) => {

    if (!masive) {
        document.querySelector(container).innerHTML = renderLoadingFiniquito("Dando V°B al finiquito, espere un momento");
    }

    __post(`${__SERVERHOST}/Finiquitos/FiniquitosValidarFinanzas`, {
        codigo: codigo
    })
    .then(resolve => {  
        
        if (resolve.length > 0) {
            resolve.forEach((validacion) => {
                if (!masive) {

                    const { Code, Message } = validacion;

                    handleRenderNotification(Code === "200" ? "success" : "danger", Message, ".alert__notification");

                    if (Code === "200") {
                        const filter = document.querySelector('#selected__filter');
                        const dataFilter = document.querySelector('#data__filter');

                        handleFiniquitos(
                            '.finiquitos',
                            '1-5',
                            params.get('f') === null
                                ? filter.value
                                : "CodigoSolicitud"
                            ,
                            params.get('f') === null
                                ? dataFilter.value
                                : params.get('f')
                        );

                        if (params.get('f') === null) {
                            handlePaginationFiniquitos(
                                '.paginationFiniquitos',
                                '1-5',
                                filter.value,
                                dataFilter.value
                            );
                        }

                        handleModalHidden(
                            document.querySelector(".modal-tw"),
                            document.querySelector(".modal-tw .modal-tw-body")
                        );

                    }
                    else {
                        document.querySelector(container).innerHTML =
                            `${renderValidacionFinanzas(codigo)}`;

                        handleEventModal();
                    }
                }
                else
                {
                    handleResultMasive(codigo, validacion);
                }
            });
        }
        
    })
    .catch(reject => {
        document.querySelector(container).innerHTML = renderValidacionFinanzas(codigo);
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });

};

const handleAnularFiniquito = (container, codigo = "", observacion = "", masive = false) => {

    if (!masive) {
        document.querySelector(container).innerHTML = renderLoadingFiniquito("Anulando finiquito, espere un momento");
    }

    __post(`${__SERVERHOST}/Finiquitos/FiniquitosAnularFiniquito`, {
        codigo: codigo,
        observacion: observacion
    })
    .then(resolve => {  
        
        if (resolve.length > 0) {
            resolve.forEach((anular) => {

                if (!masive) {
                    const { Code, Message } = anular;

                    handleRenderNotification(Code === "200" ? "success" : "danger", Message, ".alert__notification");

                    if (Code === "200") {
                        handleFiniquitos('.finiquitos', '1-5');
                        handlePaginationFiniquitos('.paginationFiniquitos', '1-5');
                        handleModalHidden(
                            document.querySelector(".modal-tw"),
                            document.querySelector(".modal-tw .modal-tw-body")
                        );

                    }
                    else {
                        document.querySelector(container).innerHTML =
                            `${renderAnulacionFiniquito(codigo, observacion)}`;

                        handleEventModal();
                    }
                }
                else {
                    handleResultMasive(codigo, anular);
                }

            });
        }
        
    })
    .catch(reject => {
        document.querySelector(container).innerHTML = renderAnulacionFiniquito(codigo, observacion);
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });

};

const handleGestionEnvioRegiones = (container, codigo = "", envio = "", observacion = "", masive = false) => {

    if (!masive) {
        document.querySelector(container).innerHTML = renderLoadingFiniquito("Registrando Gestion de Envio, espere un momento");
    }
    
    __post(`${__SERVERHOST}/Finiquitos/FiniquitosGestionEnvioRegiones`, {
        codigo: codigo,
        observacion: observacion,
        envio: envio
    })
    .then(resolve => {

        if (resolve.length > 0) {
            resolve.forEach((gestion) => {
                
                if (!masive)
                {
                    const { Code, Message } = gestion;

                    handleRenderNotification(Code === "200" ? "success" : "danger", Message, ".alert__notification");

                    if (Code === "200") {

                        const filter = document.querySelector('#selected__filter');
                        const dataFilter = document.querySelector('#data__filter');

                        handleFiniquitos(
                            '.finiquitos',
                            '1-5',
                            params.get('f') === null
                                ? filter.value
                                : "CodigoSolicitud"
                            ,
                            params.get('f') === null
                                ? dataFilter.value
                                : params.get('f')
                        );

                        if (params.get('f') === null) {
                            handlePaginationFiniquitos(
                                '.paginationFiniquitos',
                                '1-5',
                                filter.value,
                                dataFilter.value
                            );
                        }

                        handleModalHidden(
                            document.querySelector(".modal-tw"),
                            document.querySelector(".modal-tw .modal-tw-body")
                        );
                    }
                    else {
                        document.querySelector(container).innerHTML =
                            `${renderGestionEnvioRegion(codigo)}`;

                        handleEventModalModal();
                    }
                }
                else {
                    handleResultMasive(codigo, gestion);
                }
                
            });
        }

    })
    .catch(reject => {
        document.querySelector(container).innerHTML = renderGestionEnvioRegion(codigo);
        handleEventModalModal();
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });

};

const handleGestionEnvioSantiagoNotaria = (container, codigo = "", observacion = "", masive = false) => {

    if (!masive) {
        document.querySelector(container).innerHTML = renderLoadingFiniquito("Registrando Gestion de Envio, espere un momento");
    }
    
    __post(`${__SERVERHOST}/Finiquitos/FiniquitosGestionEnvioSantiagoNotaria`, {
        codigo: codigo,
        observacion: observacion
    })
    .then(resolve => {

        if (resolve.length > 0) {
            resolve.forEach((gestion) => {

                if (!masive) {
                    const { Code, Message } = gestion;

                    handleRenderNotification(Code === "200" ? "success" : "danger", Message, ".alert__notification");

                    if (Code === "200") {
                        const filter = document.querySelector('#selected__filter');
                        const dataFilter = document.querySelector('#data__filter');

                        handleFiniquitos(
                            '.finiquitos',
                            '1-5',
                            params.get('f') === null
                                ? filter.value
                                : "CodigoSolicitud"
                            ,
                            params.get('f') === null
                                ? dataFilter.value
                                : params.get('f')
                        );

                        if (params.get('f') === null) {
                            handlePaginationFiniquitos(
                                '.paginationFiniquitos',
                                '1-5',
                                filter.value,
                                dataFilter.value
                            );
                        }

                        handleModalHidden(
                            document.querySelector(".modal-tw"),
                            document.querySelector(".modal-tw .modal-tw-body")
                        );
                    }
                    else {
                        document.querySelector(container).innerHTML =
                            `${renderGestionEnvioSantiagoNotaria(codigo)}`;

                        handleEventModalModal();
                    }
                }
                else {

                    handleResultMasive(codigo, gestion);
                }

            });
        }

    })
    .catch(reject => {
        document.querySelector(container).innerHTML = renderGestionEnvioSantiagoNotaria(codigo);
        handleEventModalModal();
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });

};

const handleGestionEnvioSantiagoParaFirma = (container, codigo = "", rolCoordinador = "", coordinador = "", observacion = "", masive = false) => {

    if (!masive) {
        document.querySelector(container).innerHTML = renderLoadingFiniquito("Registrando Gestion de Envio, espere un momento");
    }
    
    __post(`${__SERVERHOST}/Finiquitos/FiniquitosGestionEnvioSantiagoParaFirma`, {
        codigo: codigo,
        observacion: observacion,
        rolCoordinador: rolCoordinador,
        coordinador: coordinador
    })
    .then(resolve => {

        if (resolve.length > 0) {
            resolve.forEach((gestion) => {

                if (!masive) {
                    const { Code, Message } = gestion;

                    handleRenderNotification(Code === "200" ? "success" : "danger", Message, ".alert__notification");

                    if (Code === "200") {
                        const filter = document.querySelector('#selected__filter');
                        const dataFilter = document.querySelector('#data__filter');

                        handleFiniquitos(
                            '.finiquitos',
                            '1-5',
                            params.get('f') === null
                                ? filter.value
                                : "CodigoSolicitud"
                            ,
                            params.get('f') === null
                                ? dataFilter.value
                                : params.get('f')
                        );

                        if (params.get('f') === null) {
                            handlePaginationFiniquitos(
                                '.paginationFiniquitos',
                                '1-5',
                                filter.value,
                                dataFilter.value
                            );
                        }

                        handleModalHidden(
                            document.querySelector(".modal-tw"),
                            document.querySelector(".modal-tw .modal-tw-body")
                        );
                    }
                    else {
                        document.querySelector(container).innerHTML =
                            `${renderGestionEnvioSantiagoFirma(codigo)}`;

                        handleEventModalModal();
                    }
                }
                else {
                    handleResultMasive(codigo, gestion);
                }
                
            });
        }

    })
    .catch(reject => {
        document.querySelector(container).innerHTML = renderGestionEnvioSantiagoFirma(codigo);
        handleEventModalModal();
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });

};

const handleGestionRecepcionRegiones = (container, codigo = "", masive = false) => {

    if (!masive) {
        document.querySelector(container).innerHTML = renderLoadingFiniquito("Registrando Recepcion Finiquito Legalizado, espere un momento");
    }
    
    __post(`${__SERVERHOST}/Finiquitos/FiniquitosGestionRecepcionRegiones`, {
        codigo: codigo
    })
    .then(resolve => {

        if (resolve.length > 0) {
            resolve.forEach((gestion) => {

                if (!masive)
                {
                    const { Code, Message } = gestion;
                    
                    handleRenderNotification(Code === "200" ? "success" : "danger", Message, ".alert__notification");

                    if (Code === "200") {
                        const filter = document.querySelector('#selected__filter');
                        const dataFilter = document.querySelector('#data__filter');

                        handleFiniquitos(
                            '.finiquitos',
                            '1-5',
                            params.get('f') === null
                                ? filter.value
                                : "CodigoSolicitud"
                            ,
                            params.get('f') === null
                                ? dataFilter.value
                                : params.get('f')
                        );

                        if (params.get('f') === null) {
                            handlePaginationFiniquitos(
                                '.paginationFiniquitos',
                                '1-5',
                                filter.value,
                                dataFilter.value
                            );
                        }

                        handleModalHidden(
                            document.querySelector(".modal-tw"),
                            document.querySelector(".modal-tw .modal-tw-body")
                        );
                    }
                    else {
                        document.querySelector(container).innerHTML =
                            `${renderRecepcionarFiniquitoRegiones(codigo)}`;

                        handleEventModal();
                    }
                } else {
                    handleResultMasive(codigo, gestion);
                }
                
            });
        }

    })
    .catch(reject => {
        document.querySelector(container).innerHTML = renderRecepcionarFiniquitoRegiones(codigo);
        handleEventModal();
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });

};

const handleGestionRecepcionSantiagoNotaria = (container, codigo = "", masive = false) => {

    if (!masive) {
        document.querySelector(container).innerHTML = renderLoadingFiniquito("Registrando Recepcion Finiquito Legalizado, espere un momento");
    }
    
    __post(`${__SERVERHOST}/Finiquitos/FiniquitosGestionRecepcionSantiagoNotaria`, {
        codigo: codigo
    })
    .then(resolve => {

        if (resolve.length > 0) {
            resolve.forEach((gestion) => {

                if (!masive) {

                    const { Code, Message } = gestion;

                    handleRenderNotification(Code === "200" ? "success" : "danger", Message, ".alert__notification");

                    if (Code === "200") {
                        const filter = document.querySelector('#selected__filter');
                        const dataFilter = document.querySelector('#data__filter');

                        handleFiniquitos(
                            '.finiquitos',
                            '1-5',
                            params.get('f') === null
                                ? filter.value
                                : "CodigoSolicitud"
                            ,
                            params.get('f') === null
                                ? dataFilter.value
                                : params.get('f')
                        );

                        if (params.get('f') === null) {
                            handlePaginationFiniquitos(
                                '.paginationFiniquitos',
                                '1-5',
                                filter.value,
                                dataFilter.value
                            );
                        }

                        handleModalHidden(
                            document.querySelector(".modal-tw"),
                            document.querySelector(".modal-tw .modal-tw-body")
                        );
                    }
                    else {
                        document.querySelector(container).innerHTML =
                            `${renderRecepcionarFiniquitoSantiagoNotaria(codigo)}`;

                        handleEventModal();
                    }
                }
                else {
                    handleResultMasive(codigo, gestion);
                }
                
            });
        }

    })
    .catch(reject => {
        document.querySelector(container).innerHTML = renderRecepcionarFiniquitoSantiagoNotaria(codigo);
        handleEventModal();
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });

};

const handleGestionRecepcionSantiagoParaFirma = (container, codigo = "", masive = false) => {

    if (!masive) {
        document.querySelector(container).innerHTML = renderLoadingFiniquito("Registrando Recepcion Finiquito Legalizado, espere un momento");
    }
    
    __post(`${__SERVERHOST}/Finiquitos/FiniquitosGestionRecepcionSantiagoParaFirma`, {
        codigo: codigo
    })
    .then(resolve => {

        if (resolve.length > 0) {
            resolve.forEach((gestion) => {

                if (!masive) {
                    const { Code, Message } = gestion;

                    handleRenderNotification(Code === "200" ? "success" : "danger", Message, ".alert__notification");

                    if (Code === "200") {
                        const filter = document.querySelector('#selected__filter');
                        const dataFilter = document.querySelector('#data__filter');

                        handleFiniquitos(
                            '.finiquitos',
                            '1-5',
                            params.get('f') === null
                                ? filter.value
                                : "CodigoSolicitud"
                            ,
                            params.get('f') === null
                                ? dataFilter.value
                                : params.get('f')
                        );

                        if (params.get('f') === null) {
                            handlePaginationFiniquitos(
                                '.paginationFiniquitos',
                                '1-5',
                                filter.value,
                                dataFilter.value
                            );
                        }

                        handleModalHidden(
                            document.querySelector(".modal-tw"),
                            document.querySelector(".modal-tw .modal-tw-body")
                        );
                    }
                    else {
                        document.querySelector(container).innerHTML =
                            `${renderRecepcionarFiniquitoSantiagoFirmaTrabajador(codigo)}`;

                        handleEventModal();
                    }

                }
                else {
                    handleResultMasive(codigo, gestion);
                }
                
            });
        }

    })
    .catch(reject => {
        document.querySelector(container).innerHTML = renderRecepcionarFiniquitoSantiagoFirmaTrabajador(codigo);
        handleEventModal();
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });

};

const handleSolicitarValeVista = (container, codigo = "", masive = false) => {

    if (!masive) {
        document.querySelector(container).innerHTML = renderLoadingFiniquito("Solicitando Vale Vista, espere un momento");
    }

    __post(`${__SERVERHOST}/Finiquitos/FiniquitosSolicitudValeVista`, {
        codigo: codigo
    })
    .then(resolve => {

        if (resolve.length > 0) {
            resolve.forEach((solicitar) => {

                if (!masive) {
                    const { Code, Message } = solicitar;

                    handleRenderNotification(Code === "200" ? "success" : "danger", Message, ".alert__notification");

                    if (Code === "200") {
                        const filter = document.querySelector('#selected__filter');
                        const dataFilter = document.querySelector('#data__filter');

                        handleFiniquitos(
                            '.finiquitos',
                            '1-5',
                            params.get('f') === null
                                ? filter.value
                                : "CodigoSolicitud"
                            ,
                            params.get('f') === null
                                ? dataFilter.value
                                : params.get('f')
                        );

                        if (params.get('f') === null) {
                            handlePaginationFiniquitos(
                                '.paginationFiniquitos',
                                '1-5',
                                filter.value,
                                dataFilter.value
                            );
                        }

                        handleModalHidden(
                            document.querySelector(".modal-tw"),
                            document.querySelector(".modal-tw .modal-tw-body")
                        );
                    }
                    else {
                        document.querySelector(container).innerHTML =
                            `${renderSolicitarValeVista(codigo)}`;

                        handleEventModal();
                    }
                }
                else {
                    handleResultMasive(codigo, solicitar);
                }
                
            });
        }

    })
    .catch(reject => {
        document.querySelector(container).innerHTML = renderSolicitarValeVista(codigo);
        handleEventModal();
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });

};

const handleSolicitudCheque = (container, codigo = "", masive = false) => {

    if (!masive) {
        document.querySelector(container).innerHTML = renderLoadingFiniquito("Solicitando Cheque, espere un momento");
    }
    
    __post(`${__SERVERHOST}/Finiquitos/FiniquitosSolicitudCheque`, {
        codigo: codigo
    })
    .then(resolve => {

        if (resolve.length > 0) {
            resolve.forEach((solicitar) => {

                if (!masive) {
                    const { Code, Message } = solicitar;

                    handleRenderNotification(Code === "200" ? "success" : "danger", Message, ".alert__notification");

                    if (Code === "200") {

                        const filter = document.querySelector('#selected__filter');
                        const dataFilter = document.querySelector('#data__filter');

                        handleFiniquitos(
                            '.finiquitos',
                            '1-5',
                            params.get('f') === null
                                ? filter.value
                                : "CodigoSolicitud"
                            ,
                            params.get('f') === null
                                ? dataFilter.value
                                : params.get('f')
                        );

                        if (params.get('f') === null) {
                            handlePaginationFiniquitos(
                                '.paginationFiniquitos',
                                '1-5',
                                filter.value,
                                dataFilter.value
                            );
                        }

                        handleModalHidden(
                            document.querySelector(".modal-tw"),
                            document.querySelector(".modal-tw .modal-tw-body")
                        );
                    }
                    else {
                        document.querySelector(container).innerHTML =
                            `${renderSolicitarCheque(codigo)}`;

                        handleEventModal();
                    }
                }
                else {
                    handleResultMasive(codigo, solicitar);
                }

            });
        }

    })
    .catch(reject => {
        document.querySelector(container).innerHTML = renderSolicitarCheque(codigo);
        handleEventModal();
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });

};

const handleConsultaDatosTEF = (container, codigo = "") => {

    document.querySelector(container).innerHTML = renderLoadingFiniquito("Consultando Datos para Transferencia, espere un momento");

    __post(`${__SERVERHOST}/Finiquitos/FiniquitosConsultaDatosTEF`, {
        codigo: codigo
    })
    .then(resolve => {

        if (resolve.length > 0) {
            let html = "";
            let selected = "";

            resolve.forEach((tef) => {
                const { Banco } = tef;

                html += tef.Code === "200" ? renderConsultaDatosTEF(tef, codigo) : renderConsultaDatosTEFNo(tef);

                selected = Banco;
            });

            document.querySelector(container).innerHTML = html;
            handleBancos('#banco__selected__beneficario', selected);
            handleEventModal();
            handleEventModalModal();
        }

    })
    .catch(reject => {
        //document.querySelector(container).innerHTML = renderSolicitarValeVista(codigo);
        //handleEventModal();
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });

};

const handleBancos = (container, selected) => {

    document.querySelector(container).innerHTML = renderLoadingBancosSelect("Consultando Datos para Transferencia, espere un momento");

    __post(`${__SERVERHOST}/Finiquitos/FiniquitosBancos`)
    .then(resolve => {

        if (resolve.length > 0) {
            let html = "";

            html = html + renderBancoSelectHeader("Seleccione Banco");

            resolve.forEach((bancos) => {
                html += renderBancosSelect(bancos, selected);
            });

            document.querySelector(container).innerHTML = html;
        }

    })
    .catch(reject => {
        document.querySelector(container).innerHTML = '';
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });

};

const handleSolicitudTEF = (container, codigo = "", cuenta = "", banco = "", gastoadm = "", observacion = "", masive = false) => {

    if (!masive) {
        document.querySelector(container).innerHTML = renderLoadingFiniquito("Solicitando Transferencia, espere un momento");
    }

    __post(`${__SERVERHOST}/Finiquitos/FiniquitosSolicitudTEF`, {
        codigo: codigo,
        observacion: observacion,
        banco: banco,
        numeroCta: cuenta,
        gastoAdm: gastoadm !== "" ? gastoadm : "0"
    })
    .then(resolve => {

        if (resolve.length > 0) {
            resolve.forEach((solicitar) => {

                if (!masive) {

                    const { Code, Message } = solicitar;
                    
                    handleRenderNotification(Code === "200" ? "success" : "danger", Message, ".alert__notification");

                    if (Code === "200") {

                        const filter = document.querySelector('#selected__filter');
                        const dataFilter = document.querySelector('#data__filter');

                        handleFiniquitos(
                            '.finiquitos',
                            '1-5',
                            params.get('f') === null
                                ? filter.value
                                : "CodigoSolicitud"
                            ,
                            params.get('f') === null
                                ? dataFilter.value
                                : params.get('f')
                        );

                        if (params.get('f') === null) {
                            handlePaginationFiniquitos(
                                '.paginationFiniquitos',
                                '1-5',
                                filter.value,
                                dataFilter.value
                            );
                        }

                        handleModalHidden(
                            document.querySelector(".modal-tw"),
                            document.querySelector(".modal-tw .modal-tw-body")
                        );
                    }
                    else {

                        handleConsultaDatosTEF('.container__soltef__finiquito', codigo);
                    }
                }
                else {
                    handleResultMasive(codigo, solicitar);
                }

                

            });
        }

    })
    .catch(reject => {
        handleConsultaDatosTEF('.container__soltef__finiquito', codigo);
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });

};

const handleGestionEnvioLegalizacion = (container, codigo = "", masive = false) => {

    if (!masive) {
        document.querySelector(container).innerHTML = renderLoadingFiniquito("Registrando Gestion de Envio, espere un momento");
    }
    
    __post(`${__SERVERHOST}/Finiquitos/FiniquitosGestionEnvioLegalizacion`, {
        codigo: codigo
    })
    .then(resolve => {

        if (resolve.length > 0) {
            resolve.forEach((solicitar) => {

                if (!masive) {
                    const { Code, Message } = solicitar;

                    handleRenderNotification(Code === "200" ? "success" : "danger", Message, ".alert__notification");

                    if (Code === "200") {
                        const filter = document.querySelector('#selected__filter');
                        const dataFilter = document.querySelector('#data__filter');

                        handleFiniquitos(
                            '.finiquitos',
                            '1-5',
                            params.get('f') === null
                                ? filter.value
                                : "CodigoSolicitud"
                            ,
                            params.get('f') === null
                                ? dataFilter.value
                                : params.get('f')
                        );

                        if (params.get('f') === null) {
                            handlePaginationFiniquitos(
                                '.paginationFiniquitos',
                                '1-5',
                                filter.value,
                                dataFilter.value
                            );
                        }

                        handleModalHidden(
                            document.querySelector(".modal-tw"),
                            document.querySelector(".modal-tw .modal-tw-body")
                        );
                    }
                    else {
                        document.querySelector(container).innerHTML =
                            `${renderGestionEnvioLegalizar(codigo)}`;

                        handleEventModal();
                    }
                }
                else {
                    handleResultMasive(codigo, solicitar);
                }

            });
        }

    })
    .catch(reject => {
        document.querySelector(container).innerHTML = renderGestionEnvioLegalizar(codigo);
        handleEventModal();
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });

};

const handleGestionRecepcionLegalizacion = (container, codigo = "", masive = false) => {

    if (!masive) {
        document.querySelector(container).innerHTML = renderLoadingFiniquito("Registrando Gestion de Recepción Finiquito Legalizado, espere un momento");
    }
    
    __post(`${__SERVERHOST}/Finiquitos/FiniquitosGestionRecepcionLegalizacion`, {
        codigo: codigo
    })
    .then(resolve => {

        if (resolve.length > 0) {
            resolve.forEach((gestion) => {

                if (!masive) {
                    const { Code, Message } = gestion;

                    handleRenderNotification(Code === "200" ? "success" : "danger", Message, ".alert__notification");

                    if (Code === "200") {

                        const filter = document.querySelector('#selected__filter');
                        const dataFilter = document.querySelector('#data__filter');

                        handleFiniquitos(
                            '.finiquitos',
                            '1-5',
                            params.get('f') === null
                                ? filter.value
                                : "CodigoSolicitud"
                            ,
                            params.get('f') === null
                                ? dataFilter.value
                                : params.get('f')
                        );

                        if (params.get('f') === null) {
                            handlePaginationFiniquitos(
                                '.paginationFiniquitos',
                                '1-5',
                                filter.value,
                                dataFilter.value
                            );
                        }

                        handleModalHidden(
                            document.querySelector(".modal-tw"),
                            document.querySelector(".modal-tw .modal-tw-body")
                        );
                    }
                    else {
                        document.querySelector(container).innerHTML =
                            `${renderGestionRecepcionLegalizacion(codigo)}`;

                        handleEventModal();
                    }
                }
                else {
                    handleResultMasive(codigo, gestion);
                }
                
            });
        }

    })
    .catch(reject => {
        document.querySelector(container).innerHTML = renderGestionRecepcionLegalizacion(codigo);
        handleEventModal();
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });

};

const handleConsultaSolicitudPago = (container, codigo = "") => {

    document.querySelector(container).innerHTML = renderLoadingFiniquito("Consultando Datos de Solicitud de Pago, espere un momento");

    __post(`${__SERVERHOST}/Finiquitos/FiniquitosConsultaSolicitudPago`, {
        codigo: codigo
    })
    .then(resolve => {

        if (resolve.length > 0) {
            let html = "";

            resolve.forEach((solicitud) => {
                html += renderConsultaSolicitudPago(solicitud, codigo);
            });

            document.querySelector(container).innerHTML = html;
            handleEventModalModal();
        }

    })
    .catch(reject => {
        //document.querySelector(container).innerHTML = renderSolicitarValeVista(codigo);
        //handleEventModal();
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });

};

const handleConfirmarProcesoPago = (container, codigo = "", masive = false) => {

    if (!masive) {
        document.querySelector(container).innerHTML = renderLoadingFiniquito("Confirmando Proceso de Pago, espere un momento");
    }
    
    __post(`${__SERVERHOST}/Finiquitos/FiniquitosConfirmarProcesoPago`, {
        codigo: codigo
    })
    .then(resolve => {

        if (resolve.length > 0) {
            resolve.forEach((gestion) => {

                if (!masive) {
                    const { Code, Message } = gestion;

                    handleRenderNotification(Code === "200" ? "success" : "danger", Message, ".alert__notification");

                    if (Code === "200") {
                        handleFiniquitos('.finiquitos', '1-5', 'Estado', 'SENDPP');
                        handlePaginationFiniquitos('.paginationFiniquitos', '1-5', 'Estado', 'SENDPP');
                        handleModalHidden(
                            document.querySelector(".modal-tw"),
                            document.querySelector(".modal-tw .modal-tw-body")
                        );
                    }
                    else {

                        handleConsultaSolicitudPago(container, codigo);
                    }
                }
                else {
                    handleResultMasive(codigo, gestion);
                }

            });
        }

    })
    .catch(reject => {
        handleConsultaSolicitudPago(container, codigo);
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });

};

const handlePagarFiniquito = (container, codigo = "", masive = false) => {

    if (!masive) {
        document.querySelector(container).innerHTML = renderLoadingFiniquito("Pagando y Terminando el Finiquito, espere un momento");
    }
    
    __post(`${__SERVERHOST}/Finiquitos/FiniquitosPagarFiniquito`, {
        codigo: codigo
    })
    .then(resolve => {

        if (resolve.length > 0) {
            resolve.forEach((pagar) => {

                if (!masive) {

                    const { Code, Message } = pagar;

                    handleRenderNotification(Code === "200" ? "success" : "danger", Message, ".alert__notification");

                    if (Code === "200") {
                        const filter = document.querySelector('#selected__filter');
                        const dataFilter = document.querySelector('#data__filter');

                        handleFiniquitos(
                            '.finiquitos',
                            '1-5',
                            params.get('f') === null
                                ? filter.value
                                : "CodigoSolicitud"
                            ,
                            params.get('f') === null
                                ? dataFilter.value
                                : params.get('f')
                        );

                        if (params.get('f') === null) {
                            handlePaginationFiniquitos(
                                '.paginationFiniquitos',
                                '1-5',
                                filter.value,
                                dataFilter.value
                            );
                        }

                        handleModalHidden(
                            document.querySelector(".modal-tw"),
                            document.querySelector(".modal-tw .modal-tw-body")
                        );
                    }
                    else {
                        document.querySelector(container).innerHTML =
                            `${renderPagarFiniquito(codigo)}`;

                        handleEventModal();
                    }
                }
                else {
                    handleResultMasive(codigo, pagar);
                }
            });
        }

    })
    .catch(reject => {
        document.querySelector(container).innerHTML = renderPagarFiniquito(codigo);
        handleEventModal();
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });

};

const handleTerminarFiniquito = (container, codigo = "", masive = false) => {

    if (!masive) {
        document.querySelector(container).innerHTML = renderLoadingFiniquito("Terminando el Finiquito, espere un momento");
    }
    
    __post(`${__SERVERHOST}/Finiquitos/FiniquitosTerminarFiniquito`, {
        codigo: codigo
    })
    .then(resolve => {

        if (resolve.length > 0) {
            resolve.forEach((terminar) => {

                if (!masive) {

                    const { Code, Message } = terminar;

                    handleRenderNotification(Code === "200" ? "success" : "danger", Message, ".alert__notification");

                    if (Code === "200") {
                        const filter = document.querySelector('#selected__filter');
                        const dataFilter = document.querySelector('#data__filter');

                        handleFiniquitos(
                            '.finiquitos',
                            '1-5',
                            params.get('f') === null
                                ? filter.value
                                : "CodigoSolicitud"
                            ,
                            params.get('f') === null
                                ? dataFilter.value
                                : params.get('f')
                        );

                        if (params.get('f') === null) {
                            handlePaginationFiniquitos(
                                '.paginationFiniquitos',
                                '1-5',
                                filter.value,
                                dataFilter.value
                            );
                        }

                        handleModalHidden(
                            document.querySelector(".modal-tw"),
                            document.querySelector(".modal-tw .modal-tw-body")
                        );
                    }
                    else {
                        document.querySelector(container).innerHTML =
                            `${renderTerminarFiniquito(codigo)}`;

                        handleEventModal();
                    }
                }
                else {
                    handleResultMasive(codigo, terminar);
                }
            });
        }

    })
    .catch(reject => {
        document.querySelector(container).innerHTML = renderTerminarFiniquito(codigo);
        handleEventModal();
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });

};

const handleFirmarFiniquito = (container, codigo = "", masive = false) => {

    if (!masive) {
        document.querySelector(container).innerHTML = renderLoadingFiniquito("Firmando el Finiquito, espere un momento");
    }
    
    __post(`${__SERVERHOST}/Finiquitos/FiniquitosFirmarFiniquito`, {
        codigo: codigo
    })
    .then(resolve => {

        if (resolve.length > 0) {
            resolve.forEach((terminar) => {

                if (!masive) {

                    const { Code, Message } = terminar;

                    handleRenderNotification(Code === "200" ? "success" : "danger", Message, ".alert__notification");

                    if (Code === "200") {
                        const filter = document.querySelector('#selected__filter');
                        const dataFilter = document.querySelector('#data__filter');

                        handleFiniquitos(
                            '.finiquitos',
                            '1-5',
                            params.get('f') === null
                                ? filter.value
                                : "CodigoSolicitud"
                            ,
                            params.get('f') === null
                                ? dataFilter.value
                                : params.get('f')
                        );

                        if (params.get('f') === null) {
                            handlePaginationFiniquitos(
                                '.paginationFiniquitos',
                                '1-5',
                                filter.value,
                                dataFilter.value
                            );
                        }

                        handleModalHidden(
                            document.querySelector(".modal-tw"),
                            document.querySelector(".modal-tw .modal-tw-body")
                        );
                    }
                    else {
                        document.querySelector(container).innerHTML =
                            `${renderFirmarFiniquito(codigo)}`;

                        handleEventModal();
                    }
                }
                else {
                    handleResultMasive(codigo, terminar);
                }
            });
        }

    })
    .catch(reject => {
        document.querySelector(container).innerHTML = renderFirmarFiniquito(codigo);
        handleEventModal();
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });

};

const handleReprocesarDocumentosFiniquito = (container, codigo = "", masive = false) => {

    if (!masive) {
        document.querySelector(container).innerHTML = renderLoadingFiniquito("Actualizando documentos del Finiquito, espere un momento");
    }
    
    __post(`${__SERVERHOST}/Finiquitos/FiniquitosReprocesarDocumentosFiniquito`, {
        codigo: codigo
    })
    .then(resolve => {

        if (resolve.length > 0) {
            resolve.forEach((terminar) => {

                if (!masive) {

                    const { Code, Message } = terminar;

                    handleRenderNotification(Code === "200" ? "success" : "danger", Message, ".alert__notification");

                    if (Code === "200") {
                        const filter = document.querySelector('#selected__filter');
                        const dataFilter = document.querySelector('#data__filter');

                        handleFiniquitos(
                            '.finiquitos',
                            '1-5',
                            params.get('f') === null
                                ? filter.value
                                : "CodigoSolicitud"
                            ,
                            params.get('f') === null
                                ? dataFilter.value
                                : params.get('f')
                        );

                        if (params.get('f') === null) {
                            handlePaginationFiniquitos(
                                '.paginationFiniquitos',
                                '1-5',
                                filter.value,
                                dataFilter.value
                            );
                        }

                        handleModalHidden(
                            document.querySelector(".modal-tw"),
                            document.querySelector(".modal-tw .modal-tw-body")
                        );
                    }
                    else {
                        document.querySelector(container).innerHTML =
                            `${renderReprocesarDocumentosFiniquito(codigo)}`;

                        handleEventModal();
                    }
                }
                else {
                    handleResultMasive(codigo, terminar);
                }
            });
        }

    })
    .catch(reject => {
        document.querySelector(container).innerHTML = renderReprocesarDocumentosFiniquito(codigo);
        handleEventModal();
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });

};

const handleLiquidacionesSueldoYear = (container, codigo = "") => {
    document.querySelector(container).innerHTML = renderLoadingFiniquito("Cargando liquidaciones de sueldo, espere un momento");

    __post(`${__SERVERHOST}/Finiquitos/FiniquitosListarLiquidacionesSueldoYear`, {
        codigo: codigo
    })
    .then(resolve => {

        let html = "";

        if (resolve.length > 0) {
            resolve.forEach((liquidaciones) => {

                const { FechaMes, Year } = liquidaciones;

                html += renderLiquidacionesSueldoYear(liquidaciones, codigo);

                handleLiquidacionesSueldoMes(`.content__liquidaciones__${Year}`, codigo, Year);

            });
        }

        document.querySelector(container).innerHTML = html;

    })
    .catch(reject => {
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });
};

const handleLiquidacionesSueldoMes = (container, codigo = "", mes = "") => {
    
    __post(`${__SERVERHOST}/Finiquitos/FiniquitosListarLiquidacionesSueldoMes`, {
        codigo: codigo,
        filter: "",
        dataFilter: mes
    })
    .then(resolve => {

        let html = "";

        if (resolve.length > 0) {
            resolve.forEach((liquidaciones) => {

                html += renderLiquidacionesSueldoMes(liquidaciones, codigo);

            });
        }

        document.querySelector(container).innerHTML = html;

    })
    .catch(reject => {
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });
};

const handleComplementoCrear = (container, codigo = "") => {

    document.querySelector(container).innerHTML = renderLoadingFiniquito("Creando Instancia de documento complemento, espere un momento");

    __post(`${__SERVERHOST}/Finiquitos/FiniquitosComplementoCrear`, {
        codigo: codigo
    })
    .then(resolve => {

        let html = "";
        let complement = "";

        if (resolve.length > 0) {

            resolve.forEach((complemento) => {  

                const { Code, Message, Codigo } = complemento;

                handleRenderNotification(Code === "200" ? "success" : "danger", Message, ".alert__notification");

                if (Code === "200") {
                    html += renderCrearComplementoStageHaberes(Codigo);
                }
                else
                {
                    html += renderCrearComplemento(codigo);
                }

                complement = Codigo;
            });

            document.querySelector(container).innerHTML = html;
            handleComplementoListarHaberes('.content__haberes__complemento', complement);
            handleEventModal();
        }

    })
    .catch(reject => {
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });

};

const handleComplementoListarHaberes = (container, codigo = "") => {

    document.querySelector(container).innerHTML = renderLoadingFiniquito("Cargando los haberes incluidos en complemento, espere un momento");

    __post(`${__SERVERHOST}/Finiquitos/FiniquitosComplementoListarHaberes`, {
        codigo: codigo
    })
    .then(resolve => {

        let html = "";

        if (resolve.length > 0) {

            resolve.forEach((haberes) => {

                html += renderHaberesComplemento(haberes);

            });

            document.querySelector(container).innerHTML = html;
            handleEventModal();
        }
        else
        {
            document.querySelector(container).innerHTML = '';
        }

    })
    .catch(reject => {
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });

};

const handleComplementoListarDescuento = (container, codigo = "") => {

    document.querySelector(container).innerHTML = renderLoadingFiniquito("Cargando los descuentos incluidos en complemento, espere un momento");

    __post(`${__SERVERHOST}/Finiquitos/FiniquitosComplementoListarDescuento`, {
        codigo: codigo
    })
    .then(resolve => {

        let html = "";

        if (resolve.length > 0) {

            resolve.forEach((descuento) => {

                html += renderDescuentosComplemento(descuento);

            });

            document.querySelector(container).innerHTML = html;
            handleEventModal();
        }
        else {
            document.querySelector(container).innerHTML = '';
        }

    })
    .catch(reject => {
        document.querySelector(container).innerHTML = html;
        handleEventModal();
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });

};

const handleComplementoAgregarHaber = (container, codigo = "", monto = "", descripcion = "") => {

    document.querySelector(container).innerHTML = renderLoadingFiniquito("Agregando Haber al complemento, espere un momento");

    __post(`${__SERVERHOST}/Finiquitos/FiniquitosComplementoAgregarHaber`, {
        codigo: codigo,
        monto: monto,
        descripcion: descripcion
    })
    .then(resolve => {
        resolve.forEach((haber) => {

            const { Code, Message } = haber;

            handleRenderNotification(Code === "200" ? "success" : "danger", Message, ".alert__notification");

            if (Code === "200") {
                handleComplementoListarHaberes('.content__haberes__complemento', codigo);
            }

            document.querySelector('.container__agregar__concepto').innerHTML = renderFormularioHaberes(codigo);
        });

    })
    .catch(reject => {
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });

};

const handleComplementoAgregarDescuento = (container, codigo = "", monto = "", descripcion = "") => {

    document.querySelector(container).innerHTML = renderLoadingFiniquito("Agregando Descuento al complemento, espere un momento");

    __post(`${__SERVERHOST}/Finiquitos/FiniquitosComplementoAgregarDescuento`, {
        codigo: codigo,
        monto: monto,
        descripcion: descripcion
    })
    .then(resolve => {
        resolve.forEach((haber) => {

            const { Code, Message } = haber;

            handleRenderNotification(Code === "200" ? "success" : "danger", Message, ".alert__notification");

            if (Code === "200") {
                handleComplementoListarDescuento('.content__descuento__complemento', codigo);
            }

            document.querySelector('.container__agregar__concepto').innerHTML = renderFormularioDescuentos(codigo);
        });

    })
    .catch(reject => {
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });

};

const handleComplementoEliminarHaber = (container, codigo = "", variable = "") => {

    document.querySelector(container).innerHTML = renderLoadingFiniquito("Eliminando Haber del complemento, espere un momento");

    __post(`${__SERVERHOST}/Finiquitos/FiniquitosComplementoEliminarHaber`, {
        codigo: codigo,
        variable: variable
    })
    .then(resolve => {
        resolve.forEach((haber) => {

            const { Code, Message } = haber;

            handleRenderNotification(Code === "200" ? "success" : "danger", Message, ".alert__notification");

            if (Code === "200") {
                handleComplementoListarHaberes('.content__haberes__complemento', codigo);
            }
            
        });

    })
    .catch(reject => {
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });

};

const handleComplementoEliminarDescuento = (container, codigo = "", variable = "") => {

    document.querySelector(container).innerHTML = renderLoadingFiniquito("Eliminando Descuento del complemento, espere un momento");

    __post(`${__SERVERHOST}/Finiquitos/FiniquitosComplementoEliminarDescuento`, {
        codigo: codigo,
        variable: variable
    })
    .then(resolve => {
        resolve.forEach((haber) => {

            const { Code, Message } = haber;

            handleRenderNotification(Code === "200" ? "success" : "danger", Message, ".alert__notification");

            if (Code === "200") {
                handleComplementoListarDescuento('.content__descuento__complemento', codigo);
            }
            
        });

    })
    .catch(reject => {
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });

};

const handleContainerDescuentos = (container, codigo = "") => {
    document.querySelector(container).innerHTML = renderCrearComplementoStageDescuentos(codigo);
    handleComplementoListarDescuento('.content__descuento__complemento', codigo);
    handleEventModal();
};

const handleContainerHaberes = (container, codigo = "") => {
    document.querySelector(container).innerHTML = renderCrearComplementoStageHaberes(codigo);
    handleComplementoListarHaberes('.content__haberes__complemento', codigo);
    handleEventModal();
};

const handleContainerFecha = (container, codigo = "") => {
    document.querySelector(container).innerHTML = renderCrearComplementoStageFechaDocumento(codigo);
    handleEventModal();
};

const handleConsultarComplementos = (container, pagination = "", filter = "", dataFilter = "") => {
    /* Loading finiquitos */
    document.querySelector(container).innerHTML = renderLoadingFiniquito("Cargando Complementos, espere un momento");

    __post(`${__SERVERHOST}/Finiquitos/FiniquitosConsultaComplementos`, {
        pagination: pagination,
        filter: filter,
        dataFilter: dataFilter
    })
    .then(resolve => {  

        let html = "";

        if (resolve.length > 0) {
            resolve.forEach((complemento) => {
                html += renderListarComplementos(complemento);
            });
        }
        
        document.querySelector(container).innerHTML = 
            `
            <table border="0">
                <tbody>
                    ${html}
                </tbody>
            </table>
            `;

        handleEventFiniquitos();

    })
    .catch(reject => {
        document.querySelector(container).innerHTML = '';
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });
};

const handleComplementoDejarActivoCreado = (container, codigo = "", fecha = "") => {

    document.querySelector(container).innerHTML = renderLoadingFiniquito("Creando Documento Complemento, espere un momento");

    __post(`${__SERVERHOST}/Finiquitos/FiniquitosComplementoDejarActivoCreado`, {
        codigo: codigo,
        fecha: fecha
    })
    .then(resolve => {

        if (resolve.length > 0) {
            resolve.forEach((complemento) => {
                const { Code, Message } = complemento;

                handleRenderNotification(Code === "200" ? "success" : "danger", Message, ".alert__notification");

                if (Code === "200") {

                    handleModalHidden(
                        document.querySelector(".modal-tw"),
                        document.querySelector(".modal-tw .modal-tw-body")
                    );

                    if (params.get('f') !== null) {
                        handleConsultarComplementos(
                            '.content__complementos__finiquito',
                            '1-999',
                            'Folio',
                            params.get('f')
                        );
                    }

                }
                else {
                    handleContainerFecha('.container__crearcomplemento__finiquito', codigo);
                }

            });
        }

    })
    .catch(reject => {
        handleContainerFecha('.container__crearcomplemento__finiquito', codigo);
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });

};

const handleActualizarMontoAdministrativo = (container, codigo = "", observacion = "", gastoAdm = "", masive = false) => {

    if (!masive) {
        document.querySelector(container).innerHTML = renderLoadingFiniquito("Actualizando Gasto Administrativo, espere un momento");
    }
    
    __post(`${__SERVERHOST}/Finiquitos/FiniquitosActualizarMontoAdministrativo`, {
        codigo: codigo,
        observacion: observacion,
        gastoAdm: gastoAdm
    })
    .then(resolve => {

        if (resolve.length > 0) {
            resolve.forEach((finiquito) => {

                if (!masive) {
                    const { Code, Message } = finiquito;

                    handleRenderNotification(Code === "200" ? "success" : "danger", Message, ".alert__notification");

                    if (Code === "200") {
                        const filter = document.querySelector('#selected__filter');
                        const dataFilter = document.querySelector('#data__filter');

                        handleFiniquitos(
                            '.finiquitos',
                            '1-5',
                            params.get('f') === null
                                ? filter.value
                                : "CodigoSolicitud"
                            ,
                            params.get('f') === null
                                ? dataFilter.value
                                : params.get('f')
                        );

                        if (params.get('f') === null) {
                            handlePaginationFiniquitos(
                                '.paginationFiniquitos',
                                '1-5',
                                filter.value,
                                dataFilter.value
                            );
                        }

                        handleModalHidden(
                            document.querySelector(".modal-tw"),
                            document.querySelector(".modal-tw .modal-tw-body")
                        );
                    }
                    else {

                        document.querySelector(container).innerHTML = renderActualizarGastoAdministrativo(codigo);
                        handleEventModal();
                    }
                }
                else {
                    handleResultMasive(codigo, finiquito);
                }
                
            });
        }

    })
    .catch(reject => {
        document.querySelector(container).innerHTML = renderActualizarGastoAdministrativo(codigo);
        handleEventModal();
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });

};

const handleRevertirValidacion = (container, codigo = "", observacion = "", masive = false) => {

    if (!masive) {
        document.querySelector(container).innerHTML = renderLoadingFiniquito("Revirtiendo Validacion de Finiquito, espere un momento");
    }

    __post(`${__SERVERHOST}/Finiquitos/FiniquitosRevertirValidacion`, {
        codigo: codigo,
        observacion: observacion
    })
    .then(resolve => {

        if (resolve.length > 0) {
            resolve.forEach((finiquito) => {

                if (!masive) {
                    const { Code, Message } = finiquito;

                    handleRenderNotification(Code === "200" ? "success" : "danger", Message, ".alert__notification");

                    if (Code === "200") {
                        const filter = document.querySelector('#selected__filter');
                        const dataFilter = document.querySelector('#data__filter');

                        handleFiniquitos(
                            '.finiquitos',
                            '1-5',
                            params.get('f') === null
                                ? filter.value
                                : "CodigoSolicitud"
                            ,
                            params.get('f') === null
                                ? dataFilter.value
                                : params.get('f')
                        );

                        if (params.get('f') === null) {
                            handlePaginationFiniquitos(
                                '.paginationFiniquitos',
                                '1-5',
                                filter.value,
                                dataFilter.value
                            );
                        }

                        handleModalHidden(
                            document.querySelector(".modal-tw"),
                            document.querySelector(".modal-tw .modal-tw-body")
                        );
                    }
                    else {

                        document.querySelector(container).innerHTML = renderRevertirValidacion(codigo);
                        handleEventModal();
                    }
                } else {
                    handleResultMasive(codigo, finiquito);
                }
                
            });
        }

    })
    .catch(reject => {
        document.querySelector(container).innerHTML = renderRevertirValidacion(codigo);
        handleEventModal();
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });

};

const handleRevertirValidacionFinanzas = (container, codigo = "", observacion = "", masive = false) => {

    if (!masive) {
        document.querySelector(container).innerHTML = renderLoadingFiniquito("Revirtiendo V°B de Finanzas, espere un momento");
    }

    __post(`${__SERVERHOST}/Finiquitos/FiniquitosRevertirValidacionFinanzas`, {
        codigo: codigo,
        observacion: observacion
    })
    .then(resolve => {

        if (resolve.length > 0) {
            resolve.forEach((finiquito) => {

                if (!masive) {
                    const { Code, Message } = finiquito;

                    handleRenderNotification(Code === "200" ? "success" : "danger", Message, ".alert__notification");

                    if (Code === "200") {
                        const filter = document.querySelector('#selected__filter');
                        const dataFilter = document.querySelector('#data__filter');

                        handleFiniquitos(
                            '.finiquitos',
                            '1-5',
                            params.get('f') === null
                                ? filter.value
                                : "CodigoSolicitud"
                            ,
                            params.get('f') === null
                                ? dataFilter.value
                                : params.get('f')
                        );

                        if (params.get('f') === null) {
                            handlePaginationFiniquitos(
                                '.paginationFiniquitos',
                                '1-5',
                                filter.value,
                                dataFilter.value
                            );
                        }

                        handleModalHidden(
                            document.querySelector(".modal-tw"),
                            document.querySelector(".modal-tw .modal-tw-body")
                        );
                    }
                    else {

                        document.querySelector(container).innerHTML = renderRevertirValidacionFinanzas(codigo);
                        handleEventModal();
                    }
                } else {
                    handleResultMasive(codigo, finiquito);
                }
                
            });
        }

    })
    .catch(reject => {
        document.querySelector(container).innerHTML = renderRevertirValidacionFinanzas(codigo);
        handleEventModal();
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });

};

const handleRevertirGestionEnvio = (container, codigo = "", observacion = "", masive = false) => {

    if (!masive) {
        document.querySelector(container).innerHTML = renderLoadingFiniquito("Revirtiendo Gestion de Envio de Finiquito, espere un momento");
    }

    __post(`${__SERVERHOST}/Finiquitos/FiniquitosRevertirGestionEnvio`, {
        codigo: codigo,
        observacion: observacion
    })
    .then(resolve => {

        if (resolve.length > 0) {
            resolve.forEach((finiquito) => {

                if (!masive) {
                    const { Code, Message } = finiquito;

                    handleRenderNotification(Code === "200" ? "success" : "danger", Message, ".alert__notification");

                    if (Code === "200") {
                        const filter = document.querySelector('#selected__filter');
                        const dataFilter = document.querySelector('#data__filter');

                        handleFiniquitos(
                            '.finiquitos',
                            '1-5',
                            params.get('f') === null
                                ? filter.value
                                : "CodigoSolicitud"
                            ,
                            params.get('f') === null
                                ? dataFilter.value
                                : params.get('f')
                        );

                        if (params.get('f') === null) {
                            handlePaginationFiniquitos(
                                '.paginationFiniquitos',
                                '1-5',
                                filter.value,
                                dataFilter.value
                            );
                        }

                        handleModalHidden(
                            document.querySelector(".modal-tw"),
                            document.querySelector(".modal-tw .modal-tw-body")
                        );
                    }
                    else {

                        document.querySelector(container).innerHTML = renderRevertirGestionEnvio(codigo);
                        handleEventModal();
                    }
                }
                else {
                    handleResultMasive(codigo, finiquito);
                }
                
            });
        }

    })
    .catch(reject => {
        document.querySelector(container).innerHTML = renderRevertirGestionEnvio(codigo);
        handleEventModal();
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });

};

const handleRevertirLegalizacion = (container, codigo = "", observacion = "", masive = false) => {

    if (!masive) {
        document.querySelector(container).innerHTML = renderLoadingFiniquito("Revirtiendo Legalización de Finiquito, espere un momento");
    }
    
    __post(`${__SERVERHOST}/Finiquitos/FiniquitosRevertirLegalizacion`, {
        codigo: codigo,
        observacion: observacion
    })
    .then(resolve => {

        if (resolve.length > 0) {
            resolve.forEach((finiquito) => {

                if (!masive) {
                    const { Code, Message } = finiquito;

                    handleRenderNotification(Code === "200" ? "success" : "danger", Message, ".alert__notification");

                    if (Code === "200") {
                        const filter = document.querySelector('#selected__filter');
                        const dataFilter = document.querySelector('#data__filter');

                        handleFiniquitos(
                            '.finiquitos',
                            '1-5',
                            params.get('f') === null
                                ? filter.value
                                : "CodigoSolicitud"
                            ,
                            params.get('f') === null
                                ? dataFilter.value
                                : params.get('f')
                        );

                        if (params.get('f') === null) {
                            handlePaginationFiniquitos(
                                '.paginationFiniquitos',
                                '1-5',
                                filter.value,
                                dataFilter.value
                            );
                        }

                        handleModalHidden(
                            document.querySelector(".modal-tw"),
                            document.querySelector(".modal-tw .modal-tw-body")
                        );
                    }
                    else {

                        document.querySelector(container).innerHTML = renderRevertirLegalizacion(codigo);
                        handleEventModal();
                    }
                }
                else {
                    handleResultMasive(codigo, finiquito);
                }
                
            });
        }

    })
    .catch(reject => {
        document.querySelector(container).innerHTML = renderRevertirLegalizacion(codigo);
        handleEventModal();
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });

};

const handleRevertirSolicitudPago = (container, codigo = "", observacion = "", masive = false) => {

    if (!masive) {
        document.querySelector(container).innerHTML = renderLoadingFiniquito("Revirtiendo Solicitud de Pago de Finiquito, espere un momento");
    }
    
    __post(`${__SERVERHOST}/Finiquitos/FiniquitosRevertirSolicitudPago`, {
        codigo: codigo,
        observacion: observacion
    })
    .then(resolve => {

        if (resolve.length > 0) {
            resolve.forEach((finiquito) => {

                if (!masive) {
                    const { Code, Message } = finiquito;

                    handleRenderNotification(Code === "200" ? "success" : "danger", Message, ".alert__notification");

                    if (Code === "200") {
                        const filter = document.querySelector('#selected__filter');
                        const dataFilter = document.querySelector('#data__filter');

                        handleFiniquitos(
                            '.finiquitos',
                            '1-5',
                            params.get('f') === null
                                ? filter.value
                                : "CodigoSolicitud"
                            ,
                            params.get('f') === null
                                ? dataFilter.value
                                : params.get('f')
                        );

                        if (params.get('f') === null) {
                            handlePaginationFiniquitos(
                                '.paginationFiniquitos',
                                '1-5',
                                filter.value,
                                dataFilter.value
                            );
                        }

                        handleModalHidden(
                            document.querySelector(".modal-tw"),
                            document.querySelector(".modal-tw .modal-tw-body")
                        );
                    }
                    else {

                        document.querySelector(container).innerHTML = renderRevertirSolicitudPago(codigo);
                        handleEventModal();
                    }
                }
                else {
                    handleResultMasive(codigo, finiquito);
                }

            });
        }

    })
    .catch(reject => {
        document.querySelector(container).innerHTML = renderRevertirSolicitudPago(codigo);
        handleEventModal();
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });

};

const handleRevertirConfirmacion = (container, codigo = "", observacion = "", masive = false) => {

    if (!masive) {
        document.querySelector(container).innerHTML = renderLoadingFiniquito("Revirtiendo Confirmación de Finiquito, espere un momento");
    }
    
    __post(`${__SERVERHOST}/Finiquitos/FiniquitosRevertirConfirmacion`, {
        codigo: codigo,
        observacion: observacion
    })
    .then(resolve => {

        if (resolve.length > 0) {
            resolve.forEach((finiquito) => {

                if (!masive) {
                    const { Code, Message } = finiquito;

                    handleRenderNotification(Code === "200" ? "success" : "danger", Message, ".alert__notification");

                    if (Code === "200") {
                        const filter = document.querySelector('#selected__filter');
                        const dataFilter = document.querySelector('#data__filter');

                        handleFiniquitos(
                            '.finiquitos',
                            '1-5',
                            params.get('f') === null
                                ? filter.value
                                : "CodigoSolicitud"
                            ,
                            params.get('f') === null
                                ? dataFilter.value
                                : params.get('f')
                        );

                        if (params.get('f') === null) {
                            handlePaginationFiniquitos(
                                '.paginationFiniquitos',
                                '1-5',
                                filter.value,
                                dataFilter.value
                            );
                        }

                        handleModalHidden(
                            document.querySelector(".modal-tw"),
                            document.querySelector(".modal-tw .modal-tw-body")
                        );
                    }
                    else {

                        document.querySelector(container).innerHTML = renderRevertirConfirmacion(codigo);
                        handleEventModal();
                    }
                }
                else {
                    handleResultMasive(codigo, finiquito);
                }
                
            });
        }

    })
    .catch(reject => {
        document.querySelector(container).innerHTML = renderRevertirConfirmacion(codigo);
        handleEventModal();
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });

};

const handleRevertirEmisionPago = (container, codigo = "", observacion = "", masive = false) => {

    if (!masive) {
        document.querySelector(container).innerHTML = renderLoadingFiniquito("Revirtiendo Emisión de Pago de Finiquito, espere un momento");
    }

    __post(`${__SERVERHOST}/Finiquitos/FiniquitosRevertirEmisionPago`, {
        codigo: codigo,
        observacion: observacion
    })
    .then(resolve => {

        if (resolve.length > 0) {
            resolve.forEach((finiquito) => {

                if (!masive) {
                    const { Code, Message } = finiquito;

                    handleRenderNotification(Code === "200" ? "success" : "danger", Message, ".alert__notification");

                    if (Code === "200") {
                        const filter = document.querySelector('#selected__filter');
                        const dataFilter = document.querySelector('#data__filter');

                        handleFiniquitos(
                            '.finiquitos',
                            '1-5',
                            params.get('f') === null
                                ? filter.value
                                : "CodigoSolicitud"
                            ,
                            params.get('f') === null
                                ? dataFilter.value
                                : params.get('f')
                        );

                        if (params.get('f') === null) {
                            handlePaginationFiniquitos(
                                '.paginationFiniquitos',
                                '1-5',
                                filter.value,
                                dataFilter.value
                            );
                        }

                        handleModalHidden(
                            document.querySelector(".modal-tw"),
                            document.querySelector(".modal-tw .modal-tw-body")
                        );
                    }
                    else {

                        document.querySelector(container).innerHTML = renderRevertirEmisionPago(codigo);
                        handleEventModal();
                    }

                }
                else {
                    handleResultMasive(codigo, finiquito);
                }
                
            });
        }

    })
    .catch(reject => {
        document.querySelector(container).innerHTML = renderRevertirEmisionPago(codigo);
        handleEventModal();
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });

};

const handleConfigOpcionesMovimientoMasivos = (container, codigo = "") => {

    document.querySelector(container).innerHTML = renderLoadingFiniquito("Cargando opciones, espere un momento");

    __post(`${__SERVERHOST}/Finiquitos/FiniquitosConfigOpcionesMovimientoMasivos`, {
        codigo: codigo
    })
    .then(resolve => {

        let html = "";

        if (resolve.length > 0) {
            resolve.forEach((opciones) => {
                html += renderButtonFiniquitosMasivo(opciones);
            });
            console.log(resolve);
        }
        else {
            html = "<h1>No se han encontrado acciones disponibles</h1>";
        }

        document.querySelector(container).innerHTML = html;
        handleEventMasivoFiniquitosModal();

    })
    .catch(reject => {
        document.querySelector(container).innerHTML = '';
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });

};

const handleActualizarComentariosAnotaciones = (container, codigo = "", html = "") => {

    document.querySelector(container).innerHTML = renderLoadingFiniquito("Agregando comentario, espere un momento");

    __post(`${__SERVERHOST}/Finiquitos/FiniquitosActualizarComentariosAnotaciones`, {
        codigo: codigo,
        html: html
    })
    .then(resolve => {

        if (resolve.length > 0) {
            resolve.forEach((finiquito) => {
                const { Code, Message } = finiquito;

                handleRenderNotification(Code === "200" ? "success" : "danger", Message, ".alert__notification");
                
                document.querySelector(container).innerHTML = renderAgregarComentario(codigo);
                handleConsultaComentariosAnotaciones('.container__comments__add', codigo);
                handleEventModal();

            });
        }

    })
    .catch(reject => {
        document.querySelector(container).innerHTML = renderAgregarComentario(codigo);
        handleEventModal();
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
    });

};

const handleConsultaComentariosAnotaciones = (container, codigo = "") => {

    document.querySelector(container).innerHTML = renderLoadingFiniquito("Consultando comentario, espere un momento");

    __post(`${__SERVERHOST}/Finiquitos/FiniquitosConsultaComentariosAnotaciones`, {
        codigo: codigo
    })
    .then(resolve => {

        let comentario = "";
        let html = "";
        if (resolve.length > 0) {
            resolve.forEach((comments) => {

                const { Html } = comments; 

                html += renderComentario(comments);

                comentario = Html;

            });
        }

        document.querySelector(container).innerHTML = html;
        document.querySelector('#observacion__comment').value = comentario.split("<br />").join('\n');

    })
    .catch(reject => {
        document.querySelector(container).innerHTML = '';
        handleRenderNotification("danger", "Ha ocurrido un problema inesperado, intentelo nuevamente mas tarde.", ".alert__notification");
        console.log(reject);
    });

};

const handleResultMasive = (codigo, result) => {
    let excelFilter = "";
    const { Code, Message } = result;

    const htmlCorrecto = document.querySelector('.content__correct__finiquitos');
    const htmlError = document.querySelector('.content__error__finiquitos');

    document.querySelector(`#result__process__resource__finiquito__${atob(codigo)}`).innerHTML = Code === "200"
        ? `<img src="${__SERVERHOST}/Resources/svgnuevos/exitoIL.svg" width="40" height="40" />`
        : `<img src="${__SERVERHOST}/Resources/svgnuevos/errorIL.svg" width="40" height="40" />`;
    document.querySelector(`#result__process__comments__finiquito__${atob(codigo)}`).innerHTML = Code === "200" ? `Completado` : `Problema`;
    document.querySelector(`#result__process__obs__finiquito__${atob(codigo)}`).innerHTML =
        Code !== "200"
            ? `<td colspan="4" class="pdl-30 pdr-20 pdt-10 pdb-10 bl-5-solid-2178379" style="font-size: 13px; color: rgb(217, 83, 79);">${Message}</td>`
            : ``;

    const masiveFiniquitos = localStorage.getItem("application__masive__finiquitos__temp") !== null
        ? JSON.parse(localStorage.getItem("application__masive__finiquitos__temp"))
        : [];

    const masiveResult200 = localStorage.getItem("application__masive__finiquitos__temp__200") !== null
        ? JSON.parse(localStorage.getItem("application__masive__finiquitos__temp__200"))
        : [];
    const masiveResult500 = localStorage.getItem("application__masive__finiquitos__temp__500") !== null
        ? JSON.parse(localStorage.getItem("application__masive__finiquitos__temp__500"))
        : [];

    switch (Code) {
        case "200":
            htmlCorrecto.innerHTML = `Finiquitos Exitosos ${Number(htmlCorrecto.innerHTML.split('Finiquitos Exitosos ').join('')) + 1}`;
            masiveResult200.push(atob(codigo));
            break;
        default:
            htmlError.innerHTML = `Finiquitos con Problemas ${Number(htmlError.innerHTML.split('Finiquitos con Problemas ').join('')) + 1}`;
            masiveResult500.push(atob(codigo));
            break;
    }
    
    if (masiveResult200.length + masiveResult500.length === masiveFiniquitos.length) {
        if (masiveResult200.length > 0) {
            masiveResult200.forEach(finiquito => {
                excelFilter += `${(excelFilter !== "" ? `+` : ``)}!${finiquito}!`;
            });

            document.querySelector('.container__button__report__result').innerHTML = renderButtonReporteProcesoMasivo(excelFilter);
        }
    }

    localStorage.setItem("application__masive__finiquitos__temp__200", JSON.stringify(masiveResult200));
    localStorage.setItem("application__masive__finiquitos__temp__500", JSON.stringify(masiveResult500));

};

/** end modulos de eventos de finiquito */

export { handleFiniquitos, handlePaginationFiniquitos, handleHeaderFiniquitos, handleConsultarComplementos, eventListenerHiddenSelectConfigOpcionFiniquito };