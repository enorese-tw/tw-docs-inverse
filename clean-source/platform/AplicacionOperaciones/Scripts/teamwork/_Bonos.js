//const __APP = "/AplicacionOperaciones/Asistencia/";
const __APP = "";
const __SERVERHOST = `${window.location.origin}${__APP}`;

const dias = ["Domingo", "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sabado"];
const meses = ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"];
let diasCargaMasiva = [];
let arrayAreaNegocio = [];
let arrayTipoBono = [];

//FUNCTION
handleConvertLetterExcel = (columna) => {
    let a;
    let b;
    a = columna;
    let convertToLetter = "";

    while (columna > 0) {
        a = parseInt((columna - 1) / 26);
        b = (columna - 1) % 26;

        convertToLetter = String.fromCharCode(b + 65) + convertToLetter;
        columna = a;
    }

    return convertToLetter;
};

handleGetDomain = () => {
    var prefixDomain = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2];
    var prefix = "";

    if (window.location.href.split('/')[2].indexOf(atob("bG9jYWxob3N0")) !== -1) {
        if ((window.location.href.split('/').length - 1) === 3) {
            prefix = prefix + "/" + window.location.href.split('/')[window.location.href.split('/').length - 1] + "/";
        } else {
            prefix = prefix + "/" + window.location.href.split('/')[3] + "/";
        }
    }

    return prefixDomain + __APP + prefix;
};

handleHideMessage = () => {
    document.getElementById("div__message").innerHTML = '';
};

handleReturnMenuAsistencia = () => {
    window.location.href = `${__SERVERHOST}/Asistencia`;
    //window.location.href = `${__SERVERHOST}`;
};

handleReturnMenuCargaMasiva = () => {
    window.location.href = `${__SERVERHOST}/Asistencia/CargasMasivas`;
};

handleChangeMonth = (e) => {
    let domain = handleGetDomain();
    let empresa = document.getElementById('empresa').value;
    let periodo = document.getElementById('periodo').value;
    let date = new Date(periodo.substr(0, 4), periodo.substr(5, 2), '01');
    let month = date.getMonth();
    let year = date.getFullYear();
    let tipo = e.dataset.tipo;
    let action = e.dataset.action;
    let ficha = '';
    let areaNegocio = '';

    switch (tipo) {
        case 'Ficha':
            ficha = document.getElementById('ficha').value;
            areaNegocio = '';
            break;
        case 'AreaNegocio':
            ficha = '';
            areaNegocio = document.getElementById('areaNegocio').value;
            break;
    }

    switch (action) {
        case 'prev':
            month = month - 1;
            month === 0 || month === -1 ? year = year - 1 : year;
            month === 0 ? month = 12 : month;
            month === -1 ? month = 11 : month;
            break;

        case 'next':
            month = month + 1;
            month === 0 ? month = 12 : month;
            break;
    }

    periodo = year + '-' + (month < 10 ? '0' + month : month);
    document.getElementById('periodo').value = periodo;

    ajaxViewPartialLoaderTransaccion(domain, "loaderSearch", empresa, areaNegocio, ficha, periodo, tipo);
};

handleChangeFilterCarga = () => {
    let empresa = document.getElementById('select__empresaCarga').value;
    let periodo = document.getElementById('input__periodoCarga').value;
    let button = document.getElementById("button__downloadCargaMasiva");
    let fecha = new Date(Number(periodo.substr(0, 4)), Number(periodo.substr(5, 2)) - 1, 1);
    let fechaFin = new Date(Number(periodo.substr(0, 4)), Number(periodo.substr(5, 2)), 0);
    let areaNegocio = '';

    document.getElementById('fileUpload').dataset.fecha = periodo !== null && periodo !== undefined ? periodo : '';

    for (i = 0; i < arrayAreaNegocio.length; i++) {
        areaNegocio += (i === arrayAreaNegocio.length - 1) ?
            arrayAreaNegocio[i] :
            arrayAreaNegocio[i] + ';';
    }

    while (diasCargaMasiva.length) {
        diasCargaMasiva.pop();
    }

    for (j = 0; j < fechaFin.getDate(); j++) {
        diasCargaMasiva.push(j + 1);
    }

    button.href = button.href.split('&')[0] + `&empresa=${empresa === '' ? '' : empresa}&areanegocio=${areaNegocio}&fecha=${periodo === '' ? '' : periodo + '-01'}`;
    console.log(button.href);
};

handleChangeAction = (e) => {
    let action = e.dataset.action;
    let empresa = '';
    let areaNegocio = '';

    switch (action) {
        case 'TiposBonos':
            empresa = document.getElementById('empresa').value;
            areaNegocio = document.getElementById('areaNegocio').value;
            htmlBonos = '';

            document.getElementById("div__message").innerHTML = ``;
            document.getElementById("div__bonos").innerHTML = ``;

            if (e.checked) {
                htmlBonos +=
                    `
                <div class="row ml-auto mr-auto" style="width:50%; margin-top:10px; text-align: left;">
                    <table style="width:100%;">
                        <tr style="height: 50px;">
                            <td colspan="2">
                                <b><label style="font-size: 18px;">Nuevo Bono</label></b>
                            </td>
                            <td></td>
                        </tr>
                        <tr style="height: 50px;">
                            <td style="width: 25%;">
                                <label>Nombre Bono: </label>
                            </td>
                            <td>
                                <input type="text" id="input__Bono" class="form-control" style="width: 400px; padding-top: 10px;" />
                            </td>
                        </tr>
                        <tr style="height: 50px;">
                            <td style="width: 25%;">
                                <label>Descripción Bono: </label>
                            </td>
                            <td>
                                <input type="text" id="input__Descripcion" class="form-control" style="width: 400px;" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <button id="btn__IngresarJornada" 
                                        onclick="handleIngresaBono(this)" 
                                        class="btn btn-teamwork"  
                                        style="border-radius: 25px; height: 40px; text-align: left; color: #fff; font-weight:100;" 
                                        data-empresa="${empresa}" data-areanegocio="${areaNegocio}">
                                    Guardar
                                    <img src="../Resources/mas.svg" width="20" height="20" style="left: -17%;" />
                                </button>
                            </td>
                        </tr>
                    </table>
                </div>
                `;
                document.getElementById('div__bonos').innerHTML = htmlBonos;
            }
            else {
                handleObtenerBonos(empresa, areaNegocio);
            }
            break;
        case 'FichaBono':
            if (e.checked) {
                document.getElementById('ficha').style.display = 'none';
                document.getElementById('areaNegocio').style.display = 'inline-block';
                document.getElementById('searchBonos').dataset.action = 'AreaNegocio';
            }
            else {
                document.getElementById('ficha').style.display = 'inline-block';
                document.getElementById('areaNegocio').style.display = 'none';
                document.getElementById('searchBonos').dataset.action = 'Ficha';
            }
            document.getElementById('div__fichaBonos').innerHTML = '';
            break;
        case 'BonoCliente':
            handleObtenerBonosCliente();
            break;
    }
};

handleChangeEmpresa = () => {
    let empresa = document.getElementById('empresa').value;

    handleGetAreaNegocio(empresa, areaNegocio);
};

handleChangeEmpresaCarga = () => {
    let empresa = document.getElementById('select__empresaCarga').value;

    while (arrayAreaNegocio.length) {
        arrayAreaNegocio.pop();
    }

    handleGetAreaNegocioCarga(empresa, '', 'select__areaNegocioCarga');
};

handleGetAreaNegocio = (empresa, areaNegocio) => {
    let domain = handleGetDomain();
    let check = document.getElementById("check__tipoAccion");
    let area = '';

    if (empresa !== null && empresa !== undefined) {
        if (empresa !== '') {
            ajaxGetAreaNegocio(domain, empresa, areaNegocio, check);
        }
        else {
            area = `<option value="" data-input="input">Seleccione Area de Negocio</option>`;
            document.getElementById('areaNegocio').innerHTML = area;
        }
    }
    else {
        area = `<option value="" data-input="input">Seleccione Area de Negocio</option>`;
        document.getElementById('areaNegocio').innerHTML = area;
    }
};

handleGetAreaNegocioCarga = (empresa, areaNegocio, target) => {
    let domain = handleGetDomain();
    let area = '';

    if (empresa !== null && empresa !== "" && empresa !== undefined) {
        if (empresa !== '0') {
            ajaxGetAreaNegocioCarga(domain, empresa, areaNegocio, target);
        }
        else {
            document.getElementById(target).innerHTML = '';
        }
    }
    else {
        document.getElementById(target).innerHTML = '';
    }


};

handleAddAreaNegocioCarga = () => {
    let empresa = document.getElementById('select__empresaCarga').value;
    let areaNegocio = document.getElementById('input__areaNegocio').value;
    let datalist = document.getElementById('select__areaNegocioCarga');
    let htmlAreaNegocio = '';
    let existeAreaNegocio = false;


    for (i = 0; i < datalist.options.length; i++) {
        if (areaNegocio === datalist.options[i].value) {
            existeAreaNegocio = true;
            if (!arrayAreaNegocio.find(item => item === areaNegocio)) {
                arrayAreaNegocio.push(areaNegocio);
            }
            break;
        }
    }

    if (!existeAreaNegocio) {
        document.getElementById('div__errorReporte').innerHTML =
            `
            <p>
                <span style="background-color: #fe5858; border-radius: 10px; padding: 10px 20px 10px 20px; color: #fff;">
                    Area de negocio no existe para esta empresa
                    <a class="btn-link" onclick="handleHideMessage('div__errorReporte')">
                        <img src="/Resources/delete.png" width="15" height="15" style="margin-bottom: 5px; margin-left: 10px;">
                    </a>
                </span>
            </p>
            `;
    }

    if (arrayAreaNegocio.length > 0) {
        htmlAreaNegocio = ``;
        for (j = 0; j < arrayAreaNegocio.length; j++) {
            htmlAreaNegocio +=
                `
                <button class="btn btn-teamwork btn-sm" onclick="handleRemoveAreaNegocioCarga(this)" data-areanegocio="${arrayAreaNegocio[j]}" style="padding: 10px; margin-top: 5px; border-radius: 25px; color:#fff;">
                    <span>${arrayAreaNegocio[j]}</span>
                    <img src="../Resources/eliminar.svg" width="18" height="18" style="position: relative;">
                </button>
                `;
        }
    }

    document.getElementById('div__areasNegocios').innerHTML = htmlAreaNegocio;
    handleChangeFilterCarga();
};

handleObtenerBonos = () => {
    let domain = handleGetDomain();
    let check = document.getElementById("check__tipoAccion");
    let empresa = document.getElementById('empresa').value;
    let areaNegocio = document.getElementById('areaNegocio').value;

    if (!check.checked) {
        if (handleValidateDataBonos(empresa, areaNegocio)) {
            document.getElementById("div__message").innerHTML = '';
            ajaxObtenerBonos(domain, empresa, areaNegocio, 'Mantenedor', 'div__bonos', '', '');
        }
        else {
            document.getElementById("div__bonos").innerHTML = '';
        }
    }
};

handleObtenerBonosCliente = () => {
    let domain = handleGetDomain();
    let empresa = document.getElementById('empresa').value;
    let areaNegocio = document.getElementById('areaNegocio').value;
    let check = document.getElementById("check__tipoAccion");

    
    if (handleValidateDataBonos(empresa, areaNegocio)) {
        document.getElementById("div__message").innerHTML = '';
        ajaxObtenerBonosCliente(domain, empresa, areaNegocio, 'Tabla', 'div__bonos');
    }
    else {
        document.getElementById("div__bonos").innerHTML = '';
    }
    
    if (!check.checked) {
        document.getElementById('div__nuevoBono').style.display = 'none';
    }
    else {
        ajaxObtenerBonos(domain, empresa, areaNegocio, 'SelectOption', 'select__bonos', '', '');

        document.getElementById('div__nuevoBono').style.display = 'inline-block';
    }
};

handleValidateDataBonos = (empresa, areaNegocio) => {
    let validate = true;

    if (empresa === '' || empresa === null || empresa === undefined) {
        validate = false;
    }

    if (areaNegocio === '' || areaNegocio === null || areaNegocio === undefined) {
        validate = false;
    }
    return validate;
};

handleValidateIngresarBono = (empresa, areaNegocio, bono, descripcion) => {
    let validate = true;

    if (empresa === '' || empresa === null || empresa === undefined) {
        validate = false;
    }

    if (areaNegocio === '' || areaNegocio === null || areaNegocio === undefined) {
        validate = false;
    }

    if (bono === "" || bono === null || bono === undefined) {
        validate = false;
    }

    if (descripcion === "" || descripcion === null || descripcion === undefined) {
        validate = false;
    }
    return validate;
};

handleValidateIngresarBonoCliente = (empresa, areaNegocio, bono) => {
    let message = '';

    if (empresa === '' || empresa === null || empresa === undefined) {
        message = 'Favor seleccione una empresa.';
    }

    if (areaNegocio === '' || areaNegocio === null || areaNegocio === undefined) {
        message = 'Favor seleccione un area de negocio.';
    }

    if (bono === "" || bono === null || bono === undefined) {
        message = 'Favor seleccione un bono.';
    }
    return message;
};

handleValidateData = (empresa, areaNegocio, periodo, ficha, tipo) => {
    let validate = true;

    if (empresa === "" || empresa === null || empresa === undefined || empresa === '0') {
        validate = false;
    }

    if (periodo === "" || periodo === null || periodo === undefined) {
        validate = false;
    }

    switch (tipo) {
        case 'Ficha':
            if (ficha === "" || ficha === null || ficha === undefined) {
                validate = false;
            }
            break;
        case 'AreaNegocio':
            if (areaNegocio === "" || areaNegocio === null || areaNegocio === undefined || areaNegocio === '0') {
                validate = false;
            }
            break;
    }
    return validate;
};

handleIngresaBono = (e) => {
    let domain = handleGetDomain();
    let empresa = e.dataset.empresa;
    let areaNegocio = e.dataset.areanegocio;
    let bono = document.getElementById("input__Bono").value;
    let descripcion = document.getElementById("input__Descripcion").value;

    if (handleValidateIngresarBono(empresa, areaNegocio, bono, descripcion)) {
        document.getElementById("div__message").innerHTML = '';
        ajaxIngresarTipoBono(domain, empresa, areaNegocio, bono, descripcion);
    }
    else {
        document.getElementById("div__message").innerHTML =
            `
            <p>
                <span style="background-color: #fe5858; border-radius: 10px; padding: 10px 20px 10px 20px; color: #fff;"> 
                    Favor completar todos los campos.
                    <a class="btn-link" onclick="handleHideMessage()">
                        <img src="/Resources/delete.png" width="15" height="15" style="margin-bottom: 5px; margin-left: 10px;">
                    </a>
                </span>
            </p>
            `;
    }
};

handleIngresarBonoCliente = (e) => {
    let domain = handleGetDomain();
    let empresa = document.getElementById('empresa').value;
    let areaNegocio = document.getElementById('areaNegocio').value;
    let codigo = document.getElementById('select__bonos').value;
    let message = handleValidateIngresarBonoCliente(empresa, areaNegocio, codigo);

    if (message === '') {
        document.getElementById("div__message").innerHTML = '';
        ajaxIngresarBonoCliente(domain, empresa, areaNegocio, codigo);
    }
    else {
        document.getElementById("div__message").innerHTML =
            `
            <p>
                <span style="background-color: #fe5858; border-radius: 10px; padding: 10px 20px 10px 20px; color: #fff;"> 
                    ${message}
                    <a class="btn-link" onclick="handleHideMessage()">
                        <img src="/Resources/delete.png" width="15" height="15" style="margin-bottom: 5px; margin-left: 10px;">
                    </a>
                </span>
            </p>
            `;
    }
};

handleUpdateBono = (e) => {
    let empresa = e.dataset.empresa;
    let areaNegocio = e.dataset.areanegocio;

    let codigo = e.dataset.codigo;
    let bono = e.dataset.bono;
    let descripcion = e.dataset.descripcion;
    let vigente = e.dataset.vigente;

    document.getElementById('div__message').innerHTML = '';

    document.getElementById('div__bonos').innerHTML =
        `
            <div class="row ml-auto mr-auto" style="width:50%; margin-top:10px; text-align: left;">
                <table style="width:100%;">
                    <tr style="height: 50px;">
                        <td colspan="2">
                            <b><label style="font-size: 18px;">Editar Bono</label></b>
                        </td>
                        <td></td>
                    </tr>

                    <tr style="height: 50px;">
                        <td style="width: 25%;">
                            <label>Nombre Bono: </label>
                        </td>
                        <td>
                            <input type="text" id="input__Bono" class="form-control" style="width: 400px; padding-top: 10px;" value="${atob(bono)}" />
                        </td>
                    </tr>
                    <tr style="height: 50px;">
                        <td style="width: 25%;">
                            <label>Descripción Bono: </label>
                        </td>
                        <td>
                            <input type="text" id="input__Descripcion" class="form-control" style="width: 400px;" value="${atob(descripcion)}" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <button id="btn__IngresarJornada" 
                                    onclick="handleEditarBono(this)" 
                                    class="btn btn-success"  
                                    style="border-radius: 25px; height: 40px; text-align: left; color: #fff; font-weight:100;" 
                                    data-codigo="${codigo}" data-empresa="${empresa}" data-areanegocio="${areaNegocio}" data-vigente="${vigente}">
                                Actualizar
                                <img src="../Resources/check.svg" width="20" height="20" style="left: -17%;" />
                            </button>

                            <button id="btn__Volver"
                                    onclick="handleObtenerBonos()"
                                    class="btn btn-danger"
                                    style="border-radius: 25px; height: 40px; text-align: left; color: #fff; font-weight:100;">
                                Volver
                                <img src="../Resources/flecha-izquierda.svg" width="20" height="20" style="left: -17%;" />
                            </button>
                        </td>
                    </tr>
                </table>
            </div>
            `;
};

handleEditarBono = (e) => {
    let domain = handleGetDomain();
    let empresa = e.dataset.empresa;
    let areaNegocio = e.dataset.areanegocio;
    let codigo = e.dataset.codigo;
    let vigente = e.dataset.vigente;
    let bono = document.getElementById("input__Bono").value;
    let descripcion = document.getElementById("input__Descripcion").value;

    if (handleValidateIngresarBono(empresa, areaNegocio, bono, descripcion)) {
        document.getElementById("div__message").innerHTML = '';
        ajaxActualizaTipoBono(domain, codigo, vigente, empresa, areaNegocio, bono, descripcion, "Editar");
    }
    else {
        document.getElementById("div__message").innerHTML =
            `
            <p>
                <span style="background-color: #fe5858; border-radius: 10px; padding: 10px 20px 10px 20px; color: #fff;"> 
                    Favor completar todos los campos.
                    <a class="btn-link" onclick="handleHideMessage()">
                        <img src="/Resources/delete.png" width="15" height="15" style="margin-bottom: 5px; margin-left: 10px;">
                    </a>
                </span>
            </p>
            `;
    }
};

handleActualizaEstadoBono = (e) => {
    let domain = handleGetDomain();
    let empresa = e.dataset.empresa;
    let areaNegocio = e.dataset.areanegocio;
    let codigo = e.dataset.codigo;
    let vigente = e.dataset.vigente;

    ajaxActualizaTipoBono(domain, codigo, vigente === 'S' ? 'N' : 'S', empresa, areaNegocio, "", "", "Estado");
};

handleActualizaEstadoBonoCliente = (e) => {
    let domain = handleGetDomain();
    let empresa = e.dataset.empresa;
    let areaNegocio = e.dataset.areanegocio;
    let codigo = e.dataset.codigo;
    let vigente = e.dataset.vigente;

    ajaxActualizaBonoCliente(domain, empresa, areaNegocio, codigo, vigente === 'S' ? 'N' : 'S', "Estado");
};

handleDeleteBono = (e) => {
    let domain = handleGetDomain();
    let codigo = e.dataset.codigo;
    let bono = e.dataset.bono;
    let empresa = e.dataset.empresa;
    let areaNegocio = e.dataset.areanegocio;

    ajaxEliminarTipoBono(domain, codigo, empresa, areaNegocio);
};

handleDeleteBonoCliente = (e) => {
    let domain = handleGetDomain();
    let codigo = e.dataset.codigo;
    let empresa = e.dataset.empresa;
    let areaNegocio = e.dataset.areanegocio;

    ajaxEliminarBonoCliente(domain, codigo, empresa, areaNegocio);
};

handleShowModalConfirmaEvento = (e) => {
    let action = e.dataset.action;
    let htmlConfirmaEvento = '';
    let codigo = '';
    let bono = '';
    let descripcion = '';
    let empresa = '';
    let areaNegocio = '';
    let valor = '';

    switch (action) {
        case "EliminarBono":
            codigo = e.dataset.codigo;
            bono = e.dataset.bono;
            descripcion = e.dataset.descripcion;
            empresa = e.dataset.empresa;

            htmlConfirmaEvento =
                `
                <div class="new-teamwork-family" style="display:block; font-weight: 100;">
                    <h2>Eliminación Bono</h2>
            
                    <div style="text-align: left; margin-left: 60px; margin-top: 20px; margin-bottom: 20px;font-weight: 100;">
                        <h4><b>Nombre:</b> ${atob(bono)}</h4>
                        <h5><b>Descripción:</b> ${atob(descripcion)}</h5>
                    </div>
                </div>

                <div id="div__action__confirmaEvento" class="new-teamwork-family" style="margin-top: 10px; margin-bottom: 20px; font-weight: 100;">
                    <button class="btn btn-danger new-teamwork-family" style="color: #fff; border-radius: 25px; margin-right: 10px; font-weight: 100; font-size: 18px;"
                            onclick="handleConfirmaEvento(this)" data-codigo="${codigo}" data-bono="${bono}"  data-empresa="${empresa}" data-areanegocio="${areaNegocio}" data-action="EliminarBono">
                        Eliminar
                    </button>

                    <button class="btn btn-anulado new-teamwork-family" style="color: #fff; border-radius: 25px; margin-right: 10px; font-weight: 100; font-size: 18px;"
                            data-dismiss="modal">
                        Cancelar
                    </button>
                </div>

                <div id="div__confirmaEvento" class="new-teamwork-family" style="display:none; margin-top: 10px; margin-bottom: 20px; font-weight: 100; border-top: 1px solid;"></div>
                `;

            document.getElementById('div__modalTitle').innerHTML = `Administración Bonos`;
            break;
        case "EliminarBonoCliente":
            codigo = e.dataset.codigo;
            bono = e.dataset.bono;
            empresa = e.dataset.empresa;
            areaNegocio = e.dataset.areanegocio;

            htmlConfirmaEvento =
                `
                <div class="new-teamwork-family" style="display:block; font-weight: 100;">
                    <h2></h2>
            
                    <div style="text-align: left; margin-left: 60px; margin-top: 20px; margin-bottom: 20px;font-weight: 100;">
                        <h4><b>Nombre:</b> ${atob(bono)}</h4>
                        <h5><b>Area Negocio:</b> ${areaNegocio}</h5>
                    </div>
                </div>

                <div id="div__action__confirmaEvento" class="new-teamwork-family" style="margin-top: 10px; margin-bottom: 20px; font-weight: 100;">
                    <button class="btn btn-danger new-teamwork-family" style="color: #fff; border-radius: 25px; margin-right: 10px; font-weight: 100; font-size: 18px;"
                            onclick="handleConfirmaEvento(this)" data-codigo="${codigo}" data-bono="${bono}"  data-empresa="${empresa}" data-areanegocio="${areaNegocio}" data-action="${action}">
                        Eliminar
                    </button>

                    <button class="btn btn-anulado new-teamwork-family" style="color: #fff; border-radius: 25px; margin-right: 10px; font-weight: 100; font-size: 18px;"
                            data-dismiss="modal">
                        Cancelar
                    </button>
                </div>

                <div id="div__confirmaEvento" class="new-teamwork-family" style="display:none; margin-top: 10px; margin-bottom: 20px; font-weight: 100; border-top: 1px solid;"></div>
                `;


            document.getElementById('div__modalTitle').innerHTML = `Eliminar Asignación Bono`;
            break;
    }

    if (htmlConfirmaEvento !== '') {
        document.getElementById('div__modalConfirmaEvento').innerHTML = htmlConfirmaEvento;
        $("#modalConfirmaEvento").modal("show");
    }
};

handleShowModalEditBono = (e) => {
    let domain = handleGetDomain();
    let action = e.dataset.action;
    let codigo = '';
    let empresa = '';
    let areaNegocio = '';
    let bono = '';
    let descripcion = '';
    let periodo = '';
    let ficha = '';
    let valor = '';
    let htmlModal = '';

    switch (action) {
        case 'InsertaFichaBono':
            codigo = e.dataset.codigo;
            empresa = e.dataset.empresa;
            areaNegocio = e.dataset.areanegocio;
            bono = e.dataset.bono;
            descripcion = e.dataset.descripcion;
            periodo = e.dataset.periodo;
            ficha = e.dataset.ficha;
            tipo = e.dataset.tipo;

            htmlModal +=
                `
                <div id="div__updBono" class="new-teamwork-family" style="display:block; font-weight: 100;">
                    <h2>Asignación de Bono</h3>

                    <div style="text-align: left; margin-left: 60px; margin-bottom: 20px;font-weight: 100;">
                        <table style="width:100%">
                            <tr>
                                <td style="width: 130px;"><h3><b>Bono:</b></h3></td>
                                <td><h3>${atob(bono)}</h3></td>
                            </tr>
                            <tr>
                                <td style="width: 130px;"><h4><b>Descripcion:</b></h4></td>
                                <td><h4>${atob(descripcion)}</h4></td>
                            </tr>
                        </table>
                    </div>
                </div>

                <div id="div__tipoBonos" class="new-teamwork-family" style="margin-top: 40px; margin-bottom: 20px; font-weight: 100; margin-left: 60px;">
                    <table style="width:100%">
                        <tr>
                            <td>
                                <label class="new-family-teamwork dspl-block lh-10 fs-18 mt-10"><b>Bono :</b></label>
                            </td>
                            <td>
                                <input id="input__valorBono" type="number" class="form-control new-teamwork-family" style="width: 300px; font-weight: 100;" />
                            </td>
                        </tr>
                    </table>
                </div>
        
                <div id="div__action__FichaBono" class="new-teamwork-family" style="margin-top: 10px; margin-bottom: 20px; font-weight: 100;">
                    <button class="btn btn-success new-teamwork-family" style="color: #fff; border-radius: 25px; margin-right: 10px; font-weight: 100; font-size: 18px;"
                            onclick="handleConfirmaEvento(this)"
                            data-empresa="${empresa}" data-areanegocio="${areaNegocio}" data-ficha="${ficha}" data-periodo="${periodo}" data-codigo="${codigo}" data-action="IngresarFichaBono" data-tipo="${tipo}">
                        Ingresar
                    </button>

                    <button class="btn btn-anulado new-teamwork-family" style="color: #fff; border-radius: 25px; margin-right: 10px; font-weight: 100; font-size: 18px;"
                            data-dismiss="modal">
                        Cancelar
                    </button>
                </div>

                <div id="div__confirmaEvento" class="new-teamwork-family" style="display:none; margin-top: 10px; margin-bottom: 20px; font-weight: 100; border-top: 1px solid;"></div>

                <div id="div__message" style="display: none; margin-top: 10px; margin-bottom: 20px;"></div>
                `;

            document.getElementById('div__modalAdministraBonos').innerHTML = htmlModal;

            break;

        case 'ActualizaFichaBono':
            codigo = e.dataset.codigo;
            empresa = e.dataset.empresa;
            areaNegocio = e.dataset.areanegocio;
            bono = e.dataset.bono;
            descripcion = e.dataset.descripcion;
            periodo = e.dataset.periodo;
            ficha = e.dataset.ficha;
            valor = e.dataset.valor;

            htmlModal +=
                `
                <div id="div__updBono" class="new-teamwork-family" style="display:block; font-weight: 100;">
                    <h2>Actualización de Bono</h3>
            
                    <div style="text-align: left; margin-left: 60px; margin-bottom: 20px;font-weight: 100;">
                        <table style="width:100%">
                            <tr>
                                <td style="width: 130px;"><h3><b>Bono:</b></h3></td>
                                <td><h3>${atob(bono)}</h3></td>
                            </tr>
                            <tr>
                                <td style="width: 130px;"><h4><b>Descripcion:</b></h4></td>
                                <td><h4>${atob(descripcion)}</h4></td>
                            </tr>
                            <tr>
                                <td style="width: 130px;"><h4><b>Valor:</b></h4></td>
                                <td><h4>${atob(valor)}</h4></td>
                            </tr>
                        </table>
                    </div>
                </div>

                <div id="div__tipoBonos" class="new-teamwork-family" style="margin-top: 10px; margin-bottom: 20px; font-weight: 100; margin-left: 60px;">
                    <table style="width:100%">
                        <tr>
                            <td>
                                <label class="new-family-teamwork dspl-block lh-10 fs-18 mt-10"><b>Bono :</b></label>
                            </td>
                            <td>
                                <input id="input__valorBono" type="number" class="form-control new-teamwork-family" style="width: 300px; font-weight: 100;" value="${atob(valor).replace('.', '')}" />
                            </td>
                        </tr>
                    </table>
                </div>
        
                <div id="div__action__FichaBono" class="new-teamwork-family" style="margin-top: 10px; margin-bottom: 20px; font-weight: 100;">
                    <button class="btn btn-warning new-teamwork-family" style="color: #fff; border-radius: 25px; margin-right: 10px; font-weight: 100; font-size: 18px;"
                            onclick="handleConfirmaEvento(this)"
                            data-empresa="${empresa}" data-areanegocio="${areaNegocio}" data-ficha="${ficha}" data-periodo="${periodo}" data-codigo="${codigo}" data-action="ActualizarFichaBono" data-tipo="${tipo}">
                        Actualizar
                    </button>

                    <button class="btn btn-danger new-teamwork-family" style="color: #fff; border-radius: 25px; margin-right: 10px; font-weight: 100; font-size: 18px;"
                            onclick="handleConfirmaEvento(this)"
                            data-empresa="${empresa}" data-areanegocio="${areaNegocio}" data-ficha="${ficha}" data-periodo="${periodo}" data-codigo="${codigo}" data-action="EliminarFichaBono" data-tipo="${tipo}">
                        Eliminar
                    </button>

                    <button class="btn btn-anulado new-teamwork-family" style="color: #fff; border-radius: 25px; margin-right: 10px; font-weight: 100; font-size: 18px;"
                            data-dismiss="modal">
                        Cancelar
                    </button>
                </div>

                <div id="div__confirmaEvento" class="new-teamwork-family" style="display:none; margin-top: 10px; margin-bottom: 20px; font-weight: 100; border-top: 1px solid;"></div>

                <div id="div__message" style="display: none; margin-top: 10px; margin-bottom: 20px;"></div>
                `;

            document.getElementById('div__modalAdministraBonos').innerHTML = htmlModal;
            break;
    }

    if (htmlModal !== '') {
        $("#modalAdministraBonos").modal("show");
    }
};

handleConfirmaEvento = (e) => {
    let action = e.dataset.action;
    let codigo = '';
    let empresa = '';
    let areaNegocio = '';
    let valor = '';
    let periodo = '';
    let tipo = '';
    let htmlConfirmaEvento = '';
    let htmlPregunta = '';
    let htmlData = '';
    let htmlEvento = '';

    switch (action) {
        case 'EliminarBono':
            empresa = e.dataset.empresa;
            areaNegocio = e.dataset.areanegocio;
            codigo = e.dataset.codigo;
            valor = e.dataset.valor;
            htmlPregunta = '¿Está seguro de querer eliminar este Bono?';
            htmlEvento = 'handleDeleteBono(this)';
            htmlData = `data-codigo="${codigo}" data-empresa="${empresa}" data-areanegocio="${areaNegocio}" data-valor="${valor}"`;

            targetModal = 'div__confirmaEvento';
            targetConfirmaEvento = 'div__action__confirmaEvento';
            break;

        case 'IngresarFichaBono':
            tipo = e.dataset.tipo;
            empresa = e.dataset.empresa;
            areaNegocio = e.dataset.areanegocio;
            periodo = e.dataset.periodo;
            ficha = e.dataset.ficha;
            codigo = e.dataset.codigo;
            valor = document.getElementById('input__valorBono').value;
            htmlPregunta = '¡Favor confirmar asignación del Bono!';
            htmlEvento = 'handleIngresarFichaBono(this)';
            htmlData = `data-empresa="${empresa}" data-areanegocio="${areaNegocio}" data-periodo="${periodo}" data-ficha="${ficha}" data-codigo="${codigo}" data-valor="${valor}" data-tipo="${tipo}"`;

            targetModal = 'div__confirmaEvento';
            targetConfirmaEvento = 'div__action__FichaBono';
            break;

        case 'ActualizarFichaBono':
            tipo = e.dataset.tipo;
            empresa = e.dataset.empresa;
            areaNegocio = e.dataset.areanegocio;
            periodo = e.dataset.periodo;
            ficha = e.dataset.ficha;
            codigo = e.dataset.codigo;
            valor = document.getElementById('input__valorBono').value;
            htmlPregunta = '¡Favor confirmar actualización del Bono!';
            htmlEvento = 'handleActualizarFichaBono(this)';
            htmlData = `data-empresa="${empresa}" data-areanegocio="${areaNegocio}" data-periodo="${periodo}" data-ficha="${ficha}" data-codigo="${codigo}" data-valor="${valor}" data-tipo="${tipo}"`;

            targetModal = 'div__confirmaEvento';
            targetConfirmaEvento = 'div__action__FichaBono';
            break;

        case 'EliminarFichaBono':
            tipo = e.dataset.tipo;
            empresa = e.dataset.empresa;
            areaNegocio = e.dataset.areanegocio;
            periodo = e.dataset.periodo;
            ficha = e.dataset.ficha;
            codigo = e.dataset.codigo;
            htmlPregunta = '¡Favor confirmar eliminación del Bono!';
            htmlEvento = 'handleEliminarFichaBono(this)';
            htmlData = `data-empresa="${empresa}" data-areanegocio="${areaNegocio}" data-periodo="${periodo}" data-ficha="${ficha}" data-codigo="${codigo}" data-tipo="${tipo}"`;

            targetModal = 'div__confirmaEvento';
            targetConfirmaEvento = 'div__action__FichaBono';
            break;

        case 'EliminarBonoCliente':
            empresa = e.dataset.empresa;
            areaNegocio = e.dataset.areanegocio;
            codigo = e.dataset.codigo;
            htmlPregunta = '¿Está seguro de eliminar esta asignación de Bono?';
            htmlEvento = 'handleDeleteBonoCliente(this)';
            htmlData = `data-codigo="${codigo}" data-empresa="${empresa}" data-areanegocio="${areaNegocio}"`;

            targetModal = 'div__confirmaEvento';
            targetConfirmaEvento = 'div__action__confirmaEvento';
            break;
    }

    htmlConfirmaEvento = 
        `
        <div style="text-align: center; margin-top: 20px; margin-bottom: 20px;">
            <div style="text-align: center;  margin-bottom: 20px;">
                <b><span class="new-font-family" style="font-size: 20px;">${htmlPregunta}</span></b>
            </div>

            <button class="btn btn-success new-teamwork-family" style="color: #fff; border-radius: 25px; margin-right: 10px; font-weight: 100; font-size: 18px;"
                    ${htmlData} onclick="${htmlEvento}">
                Aceptar
            </button>
                
            <button class="btn btn-danger new-teamwork-family" style="color: #fff; border-radius: 25px; margin-right: 10px; font-weight: 100; font-size: 18px;"
                    data-action="${action}"
                    onclick="handleCancelEvento(this)">
                Cancelar
            </button>
        </div>
        `;

    document.getElementById(targetModal).innerHTML = htmlConfirmaEvento;
    document.getElementById(targetModal).style.display = "block";
    document.getElementById(targetConfirmaEvento).style.display = "none";
};

handleCancelEvento = (e) => {
    let action = e.dataset.action;

    switch (action) {
        case "EliminarBono":
            document.getElementById("div__action__confirmaEvento").style.display = "block";
            document.getElementById("div__confirmaEvento").style.display = "none";
            document.getElementById("div__confirmaEvento").innerHTML = "";
            document.getElementById("div__message").innerHTML = "";
            break;

        case "IngresarFichaBono":
            document.getElementById("div__action__FichaBono").style.display = "block";
            document.getElementById("div__confirmaEvento").style.display = "none";
            document.getElementById("div__confirmaEvento").innerHTML = "";
            document.getElementById("div__message").innerHTML = "";
            break;

        case "ActualizarFichaBono":
            document.getElementById("div__action__FichaBono").style.display = "block";
            document.getElementById("div__confirmaEvento").style.display = "none";
            document.getElementById("div__confirmaEvento").innerHTML = "";
            document.getElementById("div__message").innerHTML = "";
            break;

        case "EliminarFichaBono":
            document.getElementById("div__action__FichaBono").style.display = "block";
            document.getElementById("div__confirmaEvento").style.display = "none";
            document.getElementById("div__confirmaEvento").innerHTML = "";
            document.getElementById("div__message").innerHTML = "";
            break;

        case 'EliminarBonoCliente':
            document.getElementById("div__action__confirmaEvento").style.display = "block";
            document.getElementById("div__confirmaEvento").style.display = "none";
            document.getElementById("div__confirmaEvento").innerHTML = "";
            document.getElementById("div__message").innerHTML = "";
            break;
    }
};

handleObtenerFichaBonos = (e) => {
    let domain = handleGetDomain();
    let action = e.dataset.action;
    let empresa = document.getElementById('empresa').value;
    let periodo = document.getElementById('periodo').value;
    let ficha = '';
    let areaNegocio = '';

    switch (action) {
        case 'Ficha':
            ficha = document.getElementById('ficha').value;
            areaNegocio = '';
            break;
        case 'AreaNegocio':
            ficha = '';
            areaNegocio = document.getElementById('areaNegocio').value;
            break;
    }

    if (handleValidateData(empresa, areaNegocio, periodo, ficha, action)) {
        ajaxViewPartialLoaderTransaccion(domain, "loaderSearch", empresa, areaNegocio, ficha, periodo, action);
    }

    document.getElementById('loaderSearch').innerHTML = '';
};

handleIngresarFichaBono = (e) => {
    let domain = handleGetDomain();
    let tipo = e.dataset.tipo;
    let empresa = e.dataset.empresa;
    let areaNegocio = e.dataset.areanegocio;
    let periodo = e.dataset.periodo;
    let ficha = e.dataset.ficha;
    let codigo = e.dataset.codigo;
    let valor = document.getElementById('input__valorBono').value;
    let validate = true;
    let htmlMessage = '';

    if (codigo === '' || codigo === null || codigo === undefined) {
        htmlMessage = 
            `
            <p>
                <span style="background-color: #fe5858; border-radius: 10px; padding: 10px 20px 10px 20px; color: #fff;">
                    ¡¡Favor seleccionar Bono!!
                    <a class="btn-link" onclick="handleHideMessage()">
                        <img src="/Resources/delete.png" width="15" height="15" style="margin-bottom: 5px; margin-left: 10px; cursor: pointer;">
                    </a>
                </span>
            </p>
            `;
        validate = false;
    }
    else {
        if (valor === '' || valor === null || valor === undefined) {
            htmlMessage =
                `
                <p>
                    <span style="background-color: #fe5858; border-radius: 10px; padding: 10px 20px 10px 20px; color: #fff;">
                       ¡¡Favor indicar valor del Bono!!
                        <a class="btn-link" onclick="handleHideMessage()">
                            <img src="/Resources/delete.png" width="15" height="15" style="margin-bottom: 5px; margin-left: 10px; cursor: pointer;">
                        </a>
                    </span>
                </p>
                `;
            validate = false;
        }
    }

    if (!validate) {
        document.getElementById("div__message").style.display = "block";
        document.getElementById("div__message").innerHTML = htmlMessage;
    }
    else {
        ajaxIngresarFichaBono(domain, empresa, areaNegocio, ficha, periodo, codigo, valor, tipo);
    }
};

handleActualizarFichaBono = (e) => {
    let domain = handleGetDomain();
    let tipo = e.dataset.tipo;
    let empresa = e.dataset.empresa;
    let areaNegocio = e.dataset.areanegocio;
    let periodo = e.dataset.periodo;
    let ficha = e.dataset.ficha;
    let codigo = e.dataset.codigo;
    let valor = document.getElementById('input__valorBono').value;
    let validate = true;
    let htmlMessage = '';

    if (codigo === '' || codigo === null || codigo === undefined) {
        htmlMessage =
            `
            <p>
                <span style="background-color: #fe5858; border-radius: 10px; padding: 10px 20px 10px 20px; color: #fff;">
                    ¡¡Favor seleccionar Bono!!
                    <a class="btn-link" onclick="handleHideMessage()">
                        <img src="/Resources/delete.png" width="15" height="15" style="margin-bottom: 5px; margin-left: 10px; cursor: pointer;">
                    </a>
                </span>
            </p>
            `;
        validate = false;
    }
    else {
        if (valor === '' || valor === null || valor === undefined) {
            htmlMessage =
                `
                <p>
                    <span style="background-color: #fe5858; border-radius: 10px; padding: 10px 20px 10px 20px; color: #fff;">
                       ¡¡Favor indicar valor del Bono!!
                        <a class="btn-link" onclick="handleHideMessage()">
                            <img src="/Resources/delete.png" width="15" height="15" style="margin-bottom: 5px; margin-left: 10px; cursor: pointer;">
                        </a>
                    </span>
                </p>
                `;
            validate = false;
        }
    }

    if (!validate) {
        document.getElementById("div__message").style.display = "block";
        document.getElementById("div__message").innerHTML = htmlMessage;
    }
    else {
        ajaxActualizarFichaBono(domain, empresa, areaNegocio, ficha, periodo, codigo, valor, tipo);
    }
};

handleEliminarFichaBono = (e) => {
    let domain = handleGetDomain();
    let tipo = e.dataset.tipo;
    let empresa = e.dataset.empresa;
    let areaNegocio = e.dataset.areanegocio;
    let periodo = e.dataset.periodo;
    let ficha = e.dataset.ficha;
    let codigo = e.dataset.codigo;
    let validate = true;
    let htmlMessage = '';

    if (codigo === '' || codigo === null || codigo === undefined) {
        htmlMessage =
            `
            <p>
                <span style="background-color: #fe5858; border-radius: 10px; padding: 10px 20px 10px 20px; color: #fff;">
                    ¡¡Favor seleccionar Bono!!
                    <a class="btn-link" onclick="handleHideMessage()">
                        <img src="/Resources/delete.png" width="15" height="15" style="margin-bottom: 5px; margin-left: 10px; cursor: pointer;">
                    </a>
                </span>
            </p>
            `;
        validate = false;
    }

    if (!validate) {
        document.getElementById("div__message").style.display = "block";
        document.getElementById("div__message").innerHTML = htmlMessage;
    }
    else {
        ajaxEliminarFichaBono(domain, empresa, areaNegocio, ficha, periodo, codigo, tipo);
    }
};


//AJAX
ajaxGetAreaNegocio = (prefix, empresa, areaNegocio, check) => {
    try {
        let method = 'GetAreaNegocio';
        let xhr = new XMLHttpRequest();
        let __form = new FormData();

        __form.append("empresa", empresa);

        xhr.open('POST', `${prefix}${method}`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                const request = JSON.parse(xhr.responseText);
                let area = `<option value="" data-input="input">Seleccione Area de Negocio</option>`;

                if (request.AreaNegocio.length > 0) {
                    for (i = 0; i < request.AreaNegocio.length; i++) {
                        area +=
                            `<option value="${request.AreaNegocio[i].Codigo}" data-input="input" ${areaNegocio === request.AreaNegocio[i].Codigo ? 'selected': ''}>
                                ${request.AreaNegocio[i].Codigo} - ${request.AreaNegocio[i].Nombre}
                            </option>`;

                        //if (areaNegocio === request.AreaNegocio[i].Codigo) {
                        //    area += `<option value="${request.AreaNegocio[i].Codigo}" data-input="input" selected>${request.AreaNegocio[i].Codigo} - ${request.AreaNegocio[i].Nombre}</option>`;
                        //}
                        //else {
                        //    area += `<option value="${request.AreaNegocio[i].Codigo}" data-input="input">${request.AreaNegocio[i].Codigo} - ${request.AreaNegocio[i].Nombre}</option>`;
                        //}
                    }

                }

                document.getElementById('areaNegocio').innerHTML = area;

                //if (check !== '' && check !== null && check !== undefined) {
                //    setTimeout(() => {
                //        if (check.checked) {
                //            handleObtenerBonos();
                //        }
                //    }, 100);
                //}

                handleObtenerBonosCliente();
            }
        };
    }
    catch{
        //
    }
};

ajaxGetAreaNegocioCarga = (prefix, empresa, areaNegocio, target) => {
    try {
        let method = 'GetAreaNegocio';
        let xhr = new XMLHttpRequest();
        let __form = new FormData();

        __form.append("empresa", empresa);

        xhr.open('POST', `${prefix}${method}`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                const request = JSON.parse(xhr.responseText);
                let area = '';

                if (request.AreaNegocio.length > 0) {
                    for (i = 0; i < request.AreaNegocio.length; i++) {
                        if (areaNegocio === request.AreaNegocio[i].Codigo) {
                            area += `<option value="${request.AreaNegocio[i].Codigo}" data-input="input" selected>${request.AreaNegocio[i].Nombre}</option>`;
                        }
                        else {
                            area += `<option value="${request.AreaNegocio[i].Codigo}" data-input="input">${request.AreaNegocio[i].Nombre}</option>`;
                        }
                    }

                }
                document.getElementById(target).innerHTML = area;
            }
        };
    }
    catch{
        //
    }
};

ajaxObtenerBonos = (prefix, empresa, areaNegocio, action, target, tipoBono, valorBono) => {
    try {
        let method = 'ObtenerBonos';
        let xhr = new XMLHttpRequest();
        let __form = new FormData();

        __form.append("empresa", empresa);
        __form.append("areaNegocio", areaNegocio);

        xhr.open('POST', `${prefix}${method}`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                const request = JSON.parse(xhr.responseText);
                let htmlError = '';
                let htmlBonos = '';
                let bono = '';

                if (request.Bonos.length > 0) {
                    switch (action) {
                        case 'Mantenedor':
                            htmlBonos =
                                `
                                <div class="row ml-auto mr-auto" style="width:85%; margin-top:10px; text-align: left;">
                                    <table style="width:100%">
                                `;

                            for (i = 0; i < request.Bonos.length; i++) {
                                if (request.Bonos[i].Code === '200') {
                                    htmlBonos +=
                                        `
                                        <tr>
                                            <td style="width:70%; padding: 10px; border-bottom: solid 1px #939393;">
                                                <div style="color: rgb(100, 100, 100)">
                                                    <label class="new-family-teamwork dspl-block lh-10 fs-18 mt-10">
                                                        <b>${request.Bonos[i].Bono}</b>
                                                    </label>
                                                </div>

                                                <div style="color: rgb(100, 100, 100)">
                                                    <label class="new-family-teamwork dspl-block lh-10 fs-14 mt-10">
                                                        Descripción: ${request.Bonos[i].Descripcion === '' ? 'Sin Descripcion' : request.Bonos[i].Descripcion}
                                                    </label>
                                                </div>
                                            </td>

                                            <td style="padding: 10px; border-bottom: solid 1px #939393;">
                                                <button class="btn new-family-teamwork fs-18 dspl-inline-block"
                                                        style="min-width: 50px; border: none; text-align: center; font-weight: 100; border: 0; margin: none; background-color: transparent!important; font-weight: 100"
                                                        id="button__editBono" onclick="handleUpdateBono(this)"
                                                        data-codigo="${btoa(request.Bonos[i].CodigoBono)}" data-bono="${btoa(request.Bonos[i].Bono)}" data-descripcion="${btoa(request.Bonos[i].Descripcion)}" 
                                                        data-vigente="${request.Bonos[i].Vigente}" data-empresa="${empresa}" data-areanegocio="${areaNegocio}">
                                                    <span style="width: 50px; height: 50px; border-radius: 50%; display: flex; align-items: center; text-align: center;"
                                                          class="btn btn-warning pdt-5 pdr-5 pdb-5 pdl-5">
                                                        <img src="../Resources/editar.svg" width="30" height="30" style="position: relative; left: 10%;" />
                                                    </span>
                                                </button>

                                                <button class="btn new-family-teamwork fs-18 dspl-inline-block"
                                                        style="min-width: 50px; border: none; text-align: center; font-weight: 100; border: 0; margin: none; background-color: transparent!important; font-weight: 100"
                                                        onclick="handleActualizaEstadoBono(this)"
                                                        data-codigo="${btoa(request.Bonos[i].CodigoBono)}" data-vigente="${request.Bonos[i].Vigente}" data-empresa="${empresa}" data-areanegocio="${areaNegocio}">
                                                    <span style="width: 50px; height: 50px; border-radius: 50%; display: flex; align-items: center; text-align: center;"
                                                          class="btn btn-${request.Bonos[i].Vigente === 'S' ? 'anulado' : 'success'} pdt-5 pdr-5 pdb-5 pdl-5">
                                                        <img src="/Resources/${request.Bonos[i].Vigente === 'S' ? 'off' : 'on'}.svg" width="30" height="30" style="position: relative; left: 10%;" />
                                                    </span>
                                                </button>

                                                <button class="btn new-family-teamwork fs-18 dspl-inline-block"
                                                        style="min-width: 50px; border: none; text-align: center; font-weight: 100; border: 0; margin: none; background-color: transparent!important; font-weight: 100; 
                                                               display: ${request.Bonos[i].OptEliminar === 'N' ? 'none' : 'inline-block'}"
                                                        id="button__editBono" onclick="handleShowModalConfirmaEvento(this)"
                                                        data-codigo="${btoa(request.Bonos[i].CodigoBono)}" data-bono="${btoa(request.Bonos[i].Bono)}" data-descripcion="${btoa(request.Bonos[i].Descripcion)}"
                                                        data-empresa="${empresa}" data-areanegocio="${areaNegocio}" data-action="EliminarBono">
                                                    <span style="width: 50px; height: 50px; border-radius: 50%; display: flex; align-items: center; text-align: center;" 
                                                        class="btn btn-danger pdt-5 pdr-5 pdb-5 pdl-5">
                                                        <img src="../Resources/eliminar.svg" width="30" height="30" style="position: relative; left: 10%;" />
                                                    </span>
                                                </button>
                                            </td>
                                        </tr>
                                        `;
                                }
                                else {
                                    htmlError +=
                                        `
                                        <p>
                                            <span style="background-color: #fe5858; border-radius: 10px; padding: 10px 20px 10px 20px; color: #fff;">
                                                ${request.Bonos[i].Message}
                                                <a class="btn-link" onclick="handleHideMessage()">
                                                    <img src="/Resources/delete.png" width="15" height="15" style="margin-bottom: 5px; margin-left: 10px;">
                                                </a>
                                            </span>
                                        </p>
                                        `;

                                    document.getElementById("div__message").innerHTML = htmlError;
                                }
                            }

                            htmlBonos +=
                                `
                                    </table>
                                </div>
                                `;
                            break;

                        case 'AdministraFichaBono':
                            htmlBonos = 
                                `
                                <table style="width:100%">
                                    <tr>
                                        <td>
                                            <label class="new-family-teamwork dspl-block lh-10 fs-18 mt-10"><b>Bono :</b></label>
                                        </td>
                                        <td>
                                            <select id="select__tipoBonos" class="form-control new-family-teamwork" style="width: 500px;">
                                                <option value="" data-input="input">Seleccione Bono</option>
                                `;

                            for (i = 0; i < request.Bonos.length; i++) {
                                htmlBonos += `<option value="${request.Bonos[i].CodigoBono}" data-input="input" ${tipoBono === request.Bonos[i].CodigoBono ? 'selected' : ''}>${request.Bonos[i].Bono}</option>`;
                            }

                            htmlBonos +=
                                `
                                            </select>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label class="new-family-teamwork dspl-block lh-10 fs-18 mt-10"><b>Valor :</b></label>
                                        </td>
                                        <td>
                                            <input id="input__valorBono" type="number" class="form-control new-teamwork-family" style="width: 300px; margin-top: 10px; margin-bottom: 20px; font-weight: 100;" value="${valorBono.replace('.', '')}" />
                                        </td>
                                    </tr>

                                </table>
                                `;
                            break;
                        case 'SelectOption':
                            bono = document.getElementById('select__bonos').value;
                            htmlBonos = `<option value="" data-input="input" ${bono === '' ? 'selected' : ''}>Seleccione Bono</option>`;

                            if (request.Bonos.length > 0) {
                                for (i = 0; i < request.Bonos.length; i++) {
                                    htmlBonos +=
                                        `
                                        <option value="${btoa(request.Bonos[i].CodigoBono)}" data-input="input" ${bono === btoa(request.Bonos[i].CodigoBono) ? 'selected': ''} >
                                            ${request.Bonos[i].Bono}
                                        </option>`;
                                }
                            }
                    }
                }

                document.getElementById(target).innerHTML = htmlBonos;
            }
        };
    }
    catch {
        //
    }
};

ajaxObtenerBonosCliente = (prefix, empresa, areaNegocio, action, target) => {
    try {
        let method = 'ObtenerBonoCliente';
        let xhr = new XMLHttpRequest();
        let __form = new FormData();

        __form.append("empresa", empresa);
        __form.append("areaNegocio", areaNegocio);

        xhr.open('POST', `${prefix}${method}`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                const request = JSON.parse(xhr.responseText);
                let htmlError = '';
                let htmlBonos = '';

                if (request.Bonos.length > 0) {
                    switch (action) {
                        case 'Tabla':
                            htmlBonos =
                                `
                                <div class="row ml-auto mr-auto" style="width:85%; margin-top:10px; text-align: left;">
                                    <table style="width:100%">
                                `;

                            for (i = 0; i < request.Bonos.length; i++) {
                                if (request.Bonos[i].Code === '200') {
                                    htmlBonos +=
                                        `
                                        <tr>
                                            <td style="width:70%; padding: 10px; border-bottom: solid 1px #939393;">
                                                <div style="color: rgb(100, 100, 100)">
                                                    <label class="new-family-teamwork dspl-block lh-10 fs-18 mt-10">
                                                        <b>${request.Bonos[i].Bono}</b>
                                                    </label>
                                                </div>
                                            </td>

                                            <td style="padding: 10px; border-bottom: solid 1px #939393;">
                                                <button class="btn new-family-teamwork fs-18 dspl-inline-block"
                                                        style="min-width: 50px; border: none; text-align: center; font-weight: 100; border: 0; margin: none; background-color: transparent!important; font-weight: 100"
                                                        onclick="handleActualizaEstadoBonoCliente(this)"
                                                        data-codigo="${btoa(request.Bonos[i].CodigoBono)}" data-vigente="${request.Bonos[i].Vigente}" data-empresa="${empresa}" data-areanegocio="${areaNegocio}">
                                                    <span style="width: 50px; height: 50px; border-radius: 50%; display: flex; align-items: center; text-align: center;"
                                                            class="btn btn-${request.Bonos[i].Vigente === 'S' ? 'anulado' : 'success'} pdt-5 pdr-5 pdb-5 pdl-5">
                                                        <img src="/Resources/${request.Bonos[i].Vigente === 'S' ? 'off' : 'on'}.svg" width="30" height="30" style="position: relative; left: 10%;" />
                                                    </span>
                                                </button>

                                                <button class="btn new-family-teamwork fs-18 dspl-inline-block"
                                                        style="min-width: 50px; border: none; text-align: center; font-weight: 100; border: 0; margin: none; background-color: transparent!important; font-weight: 100; 
                                                               display: ${request.Bonos[i].OptEliminar === 'N' ? 'none' : 'inline-block'}"
                                                        id="button__delBono" onclick="handleShowModalConfirmaEvento(this)"
                                                        data-codigo="${btoa(request.Bonos[i].CodigoBono)}" data-bono="${btoa(request.Bonos[i].Bono)}" data-empresa="${empresa}" data-areanegocio="${areaNegocio}" data-action="EliminarBonoCliente">
                                                    <span style="width: 50px; height: 50px; border-radius: 50%; display: flex; align-items: center; text-align: center;" 
                                                        class="btn btn-danger pdt-5 pdr-5 pdb-5 pdl-5">
                                                        <img src="../Resources/eliminar.svg" width="30" height="30" style="position: relative; left: 10%;" />
                                                    </span>
                                                </button>
                                            </td>
                                        </tr>
                                        `;
                                }
                                else {
                                    htmlError +=
                                        `
                                        <p>
                                            <span style="background-color: #fe5858; border-radius: 10px; padding: 10px 20px 10px 20px; color: #fff;">
                                                ${request.Bonos[i].Message}
                                                <a class="btn-link" onclick="handleHideMessage()">
                                                    <img src="/Resources/delete.png" width="15" height="15" style="margin-bottom: 5px; margin-left: 10px;">
                                                </a>
                                            </span>
                                        </p>
                                        `;

                                    document.getElementById("div__message").innerHTML = htmlError;
                                }
                            }

                            htmlBonos +=
                                `
                                    </table>
                                </div>
                                `;
                            break;
                    }
                }

                document.getElementById(target).innerHTML = htmlBonos;
            }
        };
    }
    catch {
        //
    }
};

ajaxIngresarTipoBono = (prefix, empresa, areaNegocio, bono, descripcion) => {
    try {
        let method = 'IngresarBonos';
        let xhr = new XMLHttpRequest();
        let __form = new FormData();

        __form.append("empresa", empresa);
        __form.append("areaNegocio", areaNegocio);
        __form.append("bono", bono);
        __form.append("descripcion", descripcion);

        xhr.open('POST', `${prefix}${method}`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                const request = JSON.parse(xhr.responseText);
                let htmlMessage = '';

                if (request.Bonos.length > 0) {
                    for (i = 0; i < request.Bonos.length; i++) {
                        if (request.Bonos[i].Code === '200') {
                            document.getElementById("input__Bono").value = '';
                            document.getElementById("input__Descripcion").value = '';
                        }
                        htmlMessage +=
                            `
                            <p>
                                <span style="background-color: #${request.Bonos[i].Code === '200' ? '007a00' : 'fe5858'}; border-radius: 10px; padding: 10px 20px 10px 20px; color: #fff;">
                                    ${request.Bonos[i].Message}
                                    <a class="btn-link" onclick="handleHideMessage()">
                                        <img src="/Resources/delete.png" width="15" height="15" style="margin-bottom: 5px; margin-left: 10px;">
                                    </a>
                                </span>
                            </p>
                            `;
                    }
                }
                else {
                    htmlMessage +=
                        `
                        <p>
                            <span style="background-color: #fe5858; border-radius: 10px; padding: 10px 20px 10px 20px; color: #fff;">
                                Hubo un problema con la accion solicitada.
                                <a class="btn-link" onclick="handleHideMessage()">
                                    <img src="/Resources/delete.png" width="15" height="15" style="margin-bottom: 5px; margin-left: 10px;">
                                </a>
                            </span>
                        </p>
                        `;
                }
                document.getElementById("div__message").innerHTML = htmlMessage;
            }
        };
    }
    catch {
        //
    }
};

ajaxIngresarBonoCliente = (prefix, empresa, areaNegocio, codigo) => {
    try {
        let method = 'IngresarBonoCliente';
        let xhr = new XMLHttpRequest();
        let __form = new FormData();

        __form.append("empresa", empresa);
        __form.append("areaNegocio", areaNegocio);
        __form.append("codigo", codigo);

        xhr.open('POST', `${prefix}${method}`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                const request = JSON.parse(xhr.responseText);
                let htmlMessage = '';

                if (request.Bonos.length > 0) {
                    for (i = 0; i < request.Bonos.length; i++) {
                        if (request.Bonos[i].Code === '200') {
                            ajaxObtenerBonosCliente(prefix, empresa, areaNegocio, 'Tabla', 'div__bonos');
                        }
                        htmlMessage +=
                            `
                            <p>
                                <span style="background-color: #${request.Bonos[i].Code === '200' ? '007a00' : 'fe5858'}; border-radius: 10px; padding: 10px 20px 10px 20px; color: #fff;">
                                    ${request.Bonos[i].Message}
                                    <a class="btn-link" onclick="handleHideMessage()">
                                        <img src="/Resources/delete.png" width="15" height="15" style="margin-bottom: 5px; margin-left: 10px;">
                                    </a>
                                </span>
                            </p>
                            `;
                    }
                }
                else {
                    htmlMessage +=
                        `
                        <p>
                            <span style="background-color: #fe5858; border-radius: 10px; padding: 10px 20px 10px 20px; color: #fff;">
                                Hubo un problema con la accion solicitada.
                                <a class="btn-link" onclick="handleHideMessage()">
                                    <img src="/Resources/delete.png" width="15" height="15" style="margin-bottom: 5px; margin-left: 10px;">
                                </a>
                            </span>
                        </p>
                        `;
                }
                document.getElementById("div__message").innerHTML = htmlMessage;
            }
        };
    }
    catch {
        //
    }
};

ajaxActualizaTipoBono = (prefix, codigo, vigente, empresa, areaNegocio, bono, descripcion, action) => {
    try {
        let method = 'ActualizarBono';
        let xhr = new XMLHttpRequest();
        let __form = new FormData();

        __form.append("codigo", codigo);
        __form.append("vigente", vigente);
        __form.append("bono", bono);
        __form.append("descripcion", descripcion);
        __form.append("action", action);

        xhr.open('POST', `${prefix}${method}`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                const request = JSON.parse(xhr.responseText);
                let htmlMessage = '';

                if (request.Bonos.length > 0) {
                    for (i = 0; i < request.Bonos.length; i++) {
                        if (request.Bonos[i].Code === '200') {
                            ajaxObtenerBonos(prefix, empresa, areaNegocio, 'Mantenedor', 'div__bonos', '', '');
                        }

                        htmlMessage +=
                            `
                            <p>
                                <span style="background-color: #${request.Bonos[i].Code === '200' ? '007a00' : 'fe5858'}; border-radius: 10px; padding: 10px 20px 10px 20px; color: #fff;">
                                    ${request.Bonos[i].Message}
                                    <a class="btn-link" onclick="handleHideMessage()">
                                        <img src="/Resources/delete.png" width="15" height="15" style="margin-bottom: 5px; margin-left: 10px;">
                                    </a>
                                </span>
                            </p>
                            `;
                    }
                }
                else {
                    htmlMessage +=
                        `
                        <p>
                            <span style="background-color: #fe5858; border-radius: 10px; padding: 10px 20px 10px 20px; color: #fff;">Hubo un problema con la accion solicitada.</span>
                            <a class="btn-link" onclick="handleHideMessage()">
                                <img src="/Resources/delete.png" width="15" height="15" style="margin-bottom: 5px; margin-left: 10px;">
                            </a>
                        </p>
                        `;
                }

                document.getElementById("div__message").innerHTML = htmlMessage;
            }
        };
    }
    catch {
        //
    }
};

ajaxActualizaBonoCliente = (prefix, empresa, areaNegocio, codigo, vigente, action) => {
    try {
        let method = 'ActualizarBonoCliente';
        let xhr = new XMLHttpRequest();
        let __form = new FormData();

        __form.append("empresa", empresa);
        __form.append("areaNegocio", areaNegocio);
        __form.append("codigo", codigo);
        __form.append("vigente", vigente);
        __form.append("action", action);

        xhr.open('POST', `${prefix}${method}`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                const request = JSON.parse(xhr.responseText);
                let htmlMessage = '';

                if (request.Bonos.length > 0) {
                    for (i = 0; i < request.Bonos.length; i++) {
                        if (request.Bonos[i].Code === '200') {
                            ajaxObtenerBonosCliente(prefix, empresa, areaNegocio, 'Tabla', 'div__bonos');
                        }

                        htmlMessage +=
                            `
                            <p>
                                <span style="background-color: #${request.Bonos[i].Code === '200' ? '007a00' : 'fe5858'}; border-radius: 10px; padding: 10px 20px 10px 20px; color: #fff;">
                                    ${request.Bonos[i].Message}
                                    <a class="btn-link" onclick="handleHideMessage()">
                                        <img src="/Resources/delete.png" width="15" height="15" style="margin-bottom: 5px; margin-left: 10px;">
                                    </a>
                                </span>
                            </p>
                            `;
                    }
                }
                else {
                    htmlMessage +=
                        `
                        <p>
                            <span style="background-color: #fe5858; border-radius: 10px; padding: 10px 20px 10px 20px; color: #fff;">Hubo un problema con la accion solicitada.</span>
                            <a class="btn-link" onclick="handleHideMessage()">
                                <img src="/Resources/delete.png" width="15" height="15" style="margin-bottom: 5px; margin-left: 10px;">
                            </a>
                        </p>
                        `;
                }

                document.getElementById("div__message").innerHTML = htmlMessage;
            }
        };
    }
    catch {
        //
    }
};

ajaxEliminarTipoBono = (prefix, codigo, empresa, areaNegocio) => {
    try {
        let method = 'EliminarBono';
        let xhr = new XMLHttpRequest();
        let __form = new FormData();

        __form.append("codigo", codigo);

        xhr.open('POST', `${prefix}${method}`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                const request = JSON.parse(xhr.responseText);
                let htmlMessage = '';

                if (request.Bonos.length > 0) {
                    for (i = 0; i < request.Bonos.length; i++) {
                        if (request.Bonos[i].Code === '200') {
                            ajaxObtenerBonos(prefix, empresa, areaNegocio, 'Mantenedor', 'div__bonos', '', '');
                        }

                        htmlMessage +=
                            `
                            <p>
                                <span style="background-color: #${request.Bonos[i].Code === '200' ? '007a00' : 'fe5858'}; border-radius: 10px; padding: 10px 20px 10px 20px; color: #fff;">
                                    ${request.Bonos[i].Message}
                                    <a class="btn-link" onclick="handleHideMessage()">
                                        <img src="/Resources/delete.png" width="15" height="15" style="margin-bottom: 5px; margin-left: 10px;">
                                    </a>
                                </span>
                            </p>
                            `;
                    }
                }
                else {
                    htmlMessage +=
                        `
                        <p>
                            <span style="background-color: #fe5858; border-radius: 10px; padding: 10px 20px 10px 20px; color: #fff;">Hubo un problema con la accion solicitada.</span>
                            <a class="btn-link" onclick="handleHideMessage()">
                                <img src="/Resources/delete.png" width="15" height="15" style="margin-bottom: 5px; margin-left: 10px;">
                            </a>
                        </p>
                        `;
                }

                $("#modalConfirmaEvento").modal("hide");
                document.getElementById("div__message").innerHTML = htmlMessage;
            }
        };
    }
    catch {
        //
    }
};

ajaxEliminarBonoCliente = (prefix, codigo, empresa, areaNegocio) => {
    try {
        let method = 'EliminarBonoCliente';
        let xhr = new XMLHttpRequest();
        let __form = new FormData();

        __form.append("codigo", codigo);
        __form.append("empresa", empresa);
        __form.append("areaNegocio", areaNegocio);

        xhr.open('POST', `${prefix}${method}`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                const request = JSON.parse(xhr.responseText);
                let htmlMessage = '';

                if (request.Bonos.length > 0) {
                    for (i = 0; i < request.Bonos.length; i++) {
                        if (request.Bonos[i].Code === '200') {
                            ajaxObtenerBonosCliente(prefix, empresa, areaNegocio, 'Tabla', 'div__bonos');
                        }

                        htmlMessage +=
                            `
                            <p>
                                <span style="background-color: #${request.Bonos[i].Code === '200' ? '007a00' : 'fe5858'}; border-radius: 10px; padding: 10px 20px 10px 20px; color: #fff;">
                                    ${request.Bonos[i].Message}
                                    <a class="btn-link" onclick="handleHideMessage()">
                                        <img src="/Resources/delete.png" width="15" height="15" style="margin-bottom: 5px; margin-left: 10px;">
                                    </a>
                                </span>
                            </p>
                            `;
                    }
                }
                else {
                    htmlMessage +=
                        `
                        <p>
                            <span style="background-color: #fe5858; border-radius: 10px; padding: 10px 20px 10px 20px; color: #fff;">Hubo un problema con la accion solicitada.</span>
                            <a class="btn-link" onclick="handleHideMessage()">
                                <img src="/Resources/delete.png" width="15" height="15" style="margin-bottom: 5px; margin-left: 10px;">
                            </a>
                        </p>
                        `;
                }

                $("#modalConfirmaEvento").modal("hide");
                document.getElementById("div__message").innerHTML = htmlMessage;
            }
        };
    }
    catch {
        //
    }
};

ajaxSearchFichaBonos = (prefix, empresa, areaNegocio, ficha, periodo, pagination, tipo) => {
    try {
        let method = 'ObtenerFichaBonos';
        let xhr = new XMLHttpRequest();
        let __form = new FormData();

        __form.append("empresa", empresa);
        __form.append("areaNegocio", areaNegocio);
        __form.append("ficha", ficha);
        __form.append("fecha", periodo + '-01');
        __form.append("pagination", pagination);
        __form.append("action", tipo);

        xhr.open('POST', `${prefix}${method}`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                const request = JSON.parse(xhr.responseText);
                let htmlTabla = '';
                let htmlPersonalData = '';
                let htmlTipoBonos = '';
                let htmlBonos = '';
                let htmlRow = '';
                let htmlMessage = '';
                let valorBono = '';
                let hasBono = false;
                let count = 0;
                let countLimit = 6;

                let firstDay = new Date(periodo.substr(0, 4), periodo.substr(5, 2) - 1, 1);
                let month = firstDay.getMonth();

                switch (tipo) {
                    //BONOS POR FICHA
                    case 'Ficha':
                        document.getElementById('div__content').style.zoom = '100%';
                        if (request.Personal.length > 0) {
                            if (request.Bonos.length > 0) {
                                for (i = 0; i < request.Bonos.length; i++) {
                                    htmlTipoBonos += `<th class="new-family-teamwork" style="border: 1px solid;">${request.Bonos[i].Bono}</th>`;
                                }
                            }

                            for (i = 0; i < request.Personal.length; i++) {
                                document.getElementById('div__cambioMonth').innerHTML =
                                    `
                                    <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                                        <div class="row">
                                            <div class="col-12 col-sm-12 col-md-12 col-lg-8">
                                                <div class="row">
                                                    <button class="btn btn-warning new-family-teamwork"
                                                            style="border-radius: 25px; height: 40px; text-align: left; color: #fff; font-weight:100; font-size: 14px;"
                                                            id="prevMonth" onclick="handleChangeMonth(this);"
                                                            data-fechainicio="${request.Personal[i].FechaInicio}" data-fechatermino="${request.Personal[i].FechaTermino}"  data-action="prev" data-tipo="${tipo}">
                                                            Anterior
                                                    </button>

                                                    <div id="div__month" style="font-size: 18px; margin: auto 10px; font-size: 18px;"><b>${meses[month]}</b></div>
                                
                                                    <button class="btn btn-warning new-family-teamwork"  
                                                            style="border-radius: 25px; height: 40px; text-align: left; color: #fff; font-weight:100; margin-right: 10px; font-size: 14px;"
                                                            id="nextMonth" onclick="handleChangeMonth(this);"
                                                            data-fechainicio="${request.Personal[i].FechaInicio}" data-fechatermino="${request.Personal[i].FechaTermino}" data-action="next" data-tipo="${tipo}">
                                                        Siguiente
                                                    </button>
                                                </div>
                                            </div>

                                            <div  class="col-12 col-sm-12 col-md-12 col-lg-4">
                                                <div class="row">
                                                    <div id="div__mesCerrado"></div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    `;



                                if (request.Personal[i].Code === '200') {
                                    for (j = 0; j < request.Bonos.length; j++) {
                                        hasBono = false;
                                        for (k = 0; k < request.FichaBonos.length; k++) {
                                            if (request.Bonos[j].CodigoBono === request.FichaBonos[k].CodigoBono && request.Personal[i].Ficha === request.FichaBonos[k].Ficha) {
                                                valorBono = request.FichaBonos[k].ValorBono;
                                                hasBono = true;
                                            }
                                        }

                                        if (hasBono) {
                                            htmlBonos +=
                                                `
                                                <td style="background-color: rgb(238, 238, 238); white-space: nowrap; border-left: solid 1px; border-bottom: solid 1px; border-right: solid 1px rgb(220, 220, 220);">
                                                    <b><i class="new-family-teamwork" style="font-size: 14px; font-style: normal; display: block; margin-bottom: -8px;">${request.Bonos[j].Bono}</i></b>
                                                </td>
                                                <td onclick="handleShowModalEditBono(this)"
                                                    data-codigo="${btoa(request.Bonos[j].CodigoBono)}" data-ficha="${request.Personal[i].Ficha}" data-empresa="${empresa}" data-areanegocio="${request.Personal[i].AreaNegocio}" data-periodo="${periodo}"
                                                    data-bono="${btoa(request.Bonos[j].Bono)}" data-descripcion="${btoa(request.Bonos[j].Descripcion)}" data-valor=${btoa(valorBono)} data-action="ActualizaFichaBono" data-tipo="${tipo}"
                                                    style="width: 120px; white-space: nowrap; font-weight: bolder; cursor: pointer; border-right: solid 1px; border-bottom: solid 1px;">
                                                    <b>${valorBono}</b>
                                                </td>
                                                `;
                                        }
                                        else {
                                            htmlBonos +=
                                                `
                                                <td style="background-color: rgb(238, 238, 238); border-left: solid 1px; border-bottom: solid 1px; border-right: solid 1px rgb(220, 220, 220);">
                                                    <b><i class="new-family-teamwork" style="font-size: 14px; font-style: normal; display: block; margin-bottom: -8px;">${request.Bonos[j].Bono}</i></b>
                                                </td>
                                                <td onclick="handleShowModalEditBono(this)"
                                                    data-codigo="${btoa(request.Bonos[j].CodigoBono)}" data-ficha="${request.Personal[i].Ficha}" data-empresa="${empresa}" data-areanegocio="${request.Personal[i].AreaNegocio}" data-periodo="${periodo}"
                                                    data-bono="${btoa(request.Bonos[j].Bono)}" data-descripcion="${btoa(request.Bonos[j].Descripcion)}" data-action="InsertaFichaBono"  data-tipo="${tipo}"
                                                    style="width: 120px; white-space: nowrap; font-weight: bolder; cursor: pointer; border-right: solid 1px; border-bottom: solid 1px;">
                                                </td>
                                                `;
                                        }
                                        count++;

                                        if (count === countLimit) {
                                            htmlRow +=`<tr> ${htmlBonos}</tr>`;
                                            htmlBonos = '';
                                            count = 0;
                                        }
                                        if (count < countLimit && j === (request.Bonos.length - 1)) {
                                            htmlRow += `<tr>${htmlBonos}</tr>`;
                                        }
                                    }
                                    htmlTabla =
                                        `
                                        <table class="table new-family-teamwork" style="font-size:14px; font-weight: 100;">
                                            <thead style="background-color: rgb(220, 220, 220);">
                                                <tr>
                                                    <th class="new-family-teamwork" style="white-space: nowrap; border: 1px solid;">Area Negocio</th>
                                                    <th class="new-family-teamwork" style="white-space: nowrap; border: 1px solid;">Nombre</th>
                                                    <th class="new-family-teamwork" style="white-space: nowrap; border: 1px solid;">Rut</th>
                                                    <th class="new-family-teamwork" style="white-space: nowrap; border: 1px solid;">Ficha</th>
                                                    <th class="new-family-teamwork" style="white-space: nowrap; border: 1px solid;">Cargo Mod</th>
                                                    <th class="new-family-teamwork" style="white-space: nowrap; border: 1px solid;">Inicio Contrato</th>
                                                    <th class="new-family-teamwork" style="white-space: nowrap; border: 1px solid;">Termino Contrato</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <td style="white-space: nowrap; border: 1px solid; font-weight: 100;"><b>${request.Personal[i].AreaNegocio}</b></td>
                                                <td style="white-space: nowrap; border: 1px solid; font-weight: 100;"><b>${request.Personal[i].Nombres}</b></td>
                                                <td style="white-space: nowrap; border: 1px solid; font-weight: 100;"><b>${request.Personal[i].Rut}</b></td>
                                                <td style="white-space: nowrap; border: 1px solid; font-weight: 100;"><b>${request.Personal[i].Ficha}</b></td>
                                                <td style="white-space: nowrap; border: 1px solid; font-weight: 100;"><b>${request.Personal[i].CargoMod}</b></td>
                                                <td style="white-space: nowrap; border: 1px solid; font-weight: 100;"><b>${request.Personal[i].FechaInicio}</b></td>
                                                <td style="white-space: nowrap; border: 1px solid; font-weight: 100;;"><b>${request.Personal[i].FechaTermino === '' ? 'Indefinido' : request.Personal[i].FechaTermino}</b></td>
                                            </tbody>
                                        </table>

                                        <table class="table new-family-teamwork" style="font-size:14px; font-weight: 100;">
                                            <thead style="background-color: rgb(220, 220, 220);">
                                                <tr>
                                                    <th style="border: solid 1px;" colspan="${(countLimit * 2)}">BONOS</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                ${htmlRow}
                                            </tbody>
                                        </table>
                                        `;
                                }
                                else {
                                    htmlMessage =
                                        `
                                        <p style="margin: 40px 20px;">
                                            <span style="background-color: #fe5858; border-radius: 10px; color: #fff; padding: 40px 20px; font-size: 20px;">
                                                ${request.Personal[i].Message}
                                            </span>
                                        </p>
                                        `;
                                }
                            }
                        }
                        break;
                    //BONOS POR AREA DE NEGOCIO
                    case 'AreaNegocio':
                        document.getElementById('div__content').style.zoom = '100%';
                        if (request.Personal.length > 0) {
                            if (request.Bonos.length > 0) {
                                for (i = 0; i < request.Bonos.length; i++) {
                                    if (request.Bonos[i].Code === '200') {
                                        htmlTipoBonos += `<th class="new-family-teamwork" style="border-bottom: 1px solid rgb(135, 135, 135); width: 200px;">${request.Bonos[i].Bono}</th>`;
                                    }
                                    else {
                                        htmlMessage =
                                            `<p>
                                                <span style="background-color: #fe5858; border-radius: 10px; color: #fff; padding: 10px 20px 10px 20px; font-size: 16px;">
                                                    ${request.Bonos[i].Message}
                                                </span>
                                            </p>
                                            `;
                                    }
                                }
                            }

                            for (i = 0; i < request.Personal.length; i++) {
                                htmlBonos = '';
                                document.getElementById('div__cambioMonth').innerHTML =
                                    `
                                    <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                                        <div class="row">
                                            <div class="col-12 col-sm-12 col-md-12 col-lg-8">
                                                <div class="row">
                                                    <button id="prevMonth" onclick="handleChangeMonth(this);"
                                                            class="btn btn-warning new-family-teamwork"
                                                            style="border-radius: 25px; height: 40px; text-align: left; color: #fff; font-weight:100; font-size: 14px;"
                                                            data-fechainicio="${request.Personal[i].FechaInicio}" data-fechatermino="${request.Personal[i].FechaTermino}"  data-action="prev" data-tipo="${tipo}">
                                                            Anterior
                                                    </button>

                                                    <div id="div__month" style="font-size: 18px; margin: auto 10px; font-size: 18px;"><b>${meses[month]}</b></div>
                                
                                                    <button id="nextMonth" onclick="handleChangeMonth(this);"
                                                            class="btn btn-warning new-family-teamwork"  
                                                            style="border-radius: 25px; height: 40px; text-align: left; color: #fff; font-weight:100; margin-right: 10px; font-size: 14px;"
                                                            data-fechainicio="${request.Personal[i].FechaInicio}" data-fechatermino="${request.Personal[i].FechaTermino}" data-action="next" data-tipo="${tipo}">
                                                        Siguiente
                                                    </button>
                                                </div>
                                            </div>

                                            <div  class="col-12 col-sm-12 col-md-12 col-lg-4">
                                                <div class="row">
                                                    <div id="div__mesCerrado"></div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    `;

                                for (j = 0; j < request.Bonos.length; j++) {
                                    if (request.Bonos[j].Code === '200') {
                                        hasBono = false;
                                        for (k = 0; k < request.FichaBonos.length; k++) {
                                            if (request.Bonos[j].CodigoBono === request.FichaBonos[k].CodigoBono && request.Personal[i].Ficha === request.FichaBonos[k].Ficha) {
                                                valorBono = request.FichaBonos[k].ValorBono;
                                                hasBono = true;
                                            }
                                        }

                                        if (hasBono) {
                                            htmlBonos +=
                                                `
                                            <td onclick="handleShowModalEditBono(this)"
                                                data-codigo="${btoa(request.Bonos[j].CodigoBono)}" data-ficha="${request.Personal[i].Ficha}" data-empresa="${empresa}" data-areanegocio="${areaNegocio}" data-periodo="${periodo}"
                                                data-bono="${btoa(request.Bonos[j].Bono)}" data-descripcion="${btoa(request.Bonos[j].Descripcion)}" data-valor=${btoa(valorBono)} data-action="ActualizaFichaBono"
                                                style="white-space: nowrap; border-bottom: 1px solid rgb(135, 135, 135); font-weight: bolder; cursor: pointer; width: 200px;">
                                                <b>${valorBono}</b>
                                            </td>
                                            `;
                                        }
                                        else {
                                            htmlBonos +=
                                                `
                                            <td onclick="handleShowModalEditBono(this)"
                                                data-codigo="${btoa(request.Bonos[j].CodigoBono)}" data-ficha="${request.Personal[i].Ficha}" data-empresa="${empresa}" data-areanegocio="${areaNegocio}" data-periodo="${periodo}"
                                                data-bono="${btoa(request.Bonos[j].Bono)}" data-descripcion="${btoa(request.Bonos[j].Descripcion)}" data-action="InsertaFichaBono"
                                                style="white-space: nowrap; border-bottom: 1px solid rgb(135, 135, 135); font-weight: bolder; cursor: pointer; width: 200px;">
                                            </td>
                                            `;
                                        }
                                    }
                                }


                                htmlPersonalData +=
                                    `<tr>
                                        <td style="white-space: nowrap; border-bottom: 1px solid rgb(135, 135, 135); font-weight: bolder; ${request.Personal[i].Plataforma === 'Softland' ? '' : 'background-color: #ffe1b4;'}"><b>${request.Personal[i].Nombres}</b></td>
                                        <td style="white-space: nowrap; border-bottom: 1px solid rgb(135, 135, 135); font-weight: bolder; ${request.Personal[i].Plataforma === 'Softland' ? '' : 'background-color: #ffe1b4;'}"><b>${request.Personal[i].Rut}</b></td>
                                        <td style="white-space: nowrap; border-bottom: 1px solid rgb(135, 135, 135); font-weight: bolder; ${request.Personal[i].Plataforma === 'Softland' ? '' : 'background-color: #ffe1b4;'}"><b>${request.Personal[i].Ficha}</b></td>
                                        <td style="white-space: nowrap; border-bottom: 1px solid rgb(135, 135, 135); font-weight: bolder; ${request.Personal[i].Plataforma === 'Softland' ? '' : 'background-color: #ffe1b4;'}"><b>${request.Personal[i].CargoMod}</b></td>
                                        <td style="white-space: nowrap; border-bottom: 1px solid rgb(135, 135, 135); font-weight: bolder; ${request.Personal[i].Plataforma === 'Softland' ? '' : 'background-color: #ffe1b4;'}"><b>${request.Personal[i].FechaInicio}</b></td>
                                        <td style="white-space: nowrap; border-bottom: 1px solid rgb(135, 135, 135); font-weight: bolder; ${request.Personal[i].Plataforma === 'Softland' ? '' : 'background-color: #ffe1b4;'}"><b>${request.Personal[i].FechaTermino === '' ? 'Indefinido' : request.Personal[i].FechaTermino}</b></td>
                                        ${htmlBonos}
                                    </tr>
                                    `;
                            }

                            htmlTabla =
                                `
                                <table class="table new-family-teamwork header-fixed" style="font-size:11px; font-weight: 100; margin-right: 10px;">
                                    <thead>
                                        <tr>
                                            <th class="new-family-teamwork" style="border-bottom: 1px solid rgb(135, 135, 135); width: 200px;">Nombre</th>
                                            <th class="new-family-teamwork" style="border-bottom: 1px solid rgb(135, 135, 135); width: 200px;">Rut</th>
                                            <th class="new-family-teamwork" style="border-bottom: 1px solid rgb(135, 135, 135); width: 200px;">Ficha</th>
                                            <th class="new-family-teamwork" style="border-bottom: 1px solid rgb(135, 135, 135); width: 200px;">Cargo Mod</th>
                                            <th class="new-family-teamwork" style="border-bottom: 1px solid rgb(135, 135, 135); width: 200px;">Inicio Contrato</th>
                                            <th class="new-family-teamwork" style="border-bottom: 1px solid rgb(135, 135, 135); width: 200px;">Termino Contrato</th>
                                            ${htmlTipoBonos}
                                        </tr>
                                    </thead>
                                    <tbody>
                                        ${htmlPersonalData}
                                    </tbody>
                                </table>
                                `;
                        }
                        else {
                            htmlMessage =
                                `
                                <p>
                                    <span style="background-color: #fe5858; border-radius: 10px; color: #fff; padding: 40px 20px 40px 20px; font-size: 20px;">
                                        ¡No hay personal vigente para este periodo y area de negocio!
                                    </span>
                                </p>
                                `;
                        }
                        break;
                }

                document.getElementById('div__errorMessage').innerHTML = htmlMessage;
                document.getElementById('div__fichaBonos').innerHTML = htmlTabla;
                $('#loaderSearch').html('');
            }
        };
    }
    catch{
        //
    }
};

ajaxIngresarFichaBono = (prefix, empresa, areaNegocio, ficha, periodo, codigo, valor, tipo) => {
    try {
        let method = 'IngresarFichaBono';
        let xhr = new XMLHttpRequest();
        let __form = new FormData();

        __form.append("empresa", empresa);
        __form.append("areaNegocio", areaNegocio);
        __form.append("ficha", ficha);
        __form.append("fecha", periodo + '-01');
        __form.append("codigo", codigo);
        __form.append("valor", valor);

        xhr.open('POST', `${prefix}${method}`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                const request = JSON.parse(xhr.responseText);
                let htmlMessage = '';

                if (request.FichaBonos.length > 0) {
                    for (i = 0; i < request.FichaBonos.length; i++) {
                        if (request.FichaBonos[i].Code === "200") {
                            $("#modalAdministraBonos").modal("hide");
                            document.getElementById('div__modalAdministraBonos').innerHTML = '';

                            ajaxViewPartialLoaderTransaccion(prefix, "loaderSearch", empresa, areaNegocio, ficha, periodo, tipo);
                        }
                        else {
                            htmlMessage =
                                `
                                <p>
                                    <span style="background-color: #fe5858; border-radius: 10px; padding: 10px 20px 10px 20px; color: #fff;">
                                       ${request.FichaBonos[i].Message}
                                        <a class="btn-link" onclick="handleHideMessage()">
                                            <img src="/Resources/delete.png" width="15" height="15" style="margin-bottom: 5px; margin-left: 10px; cursor: pointer;">
                                        </a>
                                    </span>
                                </p>
                                `;
                            document.getElementById("div__message").style.display = "block";
                            document.getElementById("div__message").innerHTML = htmlMessage;
                        }
                    }
                }
            }
        };
    }
    catch{
        //
    }
};

ajaxActualizarFichaBono = (prefix, empresa, areaNegocio, ficha, periodo, codigo, valor, tipo) => {
    try {
        let method = 'ActualizarFichaBono';
        let xhr = new XMLHttpRequest();
        let __form = new FormData();

        __form.append("empresa", empresa);
        __form.append("areaNegocio", areaNegocio);
        __form.append("ficha", ficha);
        __form.append("fecha", periodo + '-01');
        __form.append("codigo", codigo);
        __form.append("valor", valor);

        xhr.open('POST', `${prefix}${method}`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                const request = JSON.parse(xhr.responseText);
                let htmlMessage = '';

                if (request.FichaBonos.length > 0) {
                    for (i = 0; i < request.FichaBonos.length; i++) {
                        if (request.FichaBonos[i].Code === "200") {
                            $("#modalAdministraBonos").modal("hide");
                            document.getElementById('div__modalAdministraBonos').innerHTML = '';

                            ajaxViewPartialLoaderTransaccion(prefix, "loaderSearch", empresa, areaNegocio, ficha, periodo, tipo);
                        }
                        else {
                            htmlMessage =
                                `
                                <p>
                                    <span style="background-color: #fe5858; border-radius: 10px; padding: 10px 20px 10px 20px; color: #fff;">
                                       ${request.FichaBonos[i].Message}
                                        <a class="btn-link" onclick="handleHideMessage()">
                                            <img src="/Resources/delete.png" width="15" height="15" style="margin-bottom: 5px; margin-left: 10px; cursor: pointer;">
                                        </a>
                                    </span>
                                </p>
                                `;
                            document.getElementById("div__message").style.display = "block";
                            document.getElementById("div__message").innerHTML = htmlMessage;
                        }
                    }
                }
            }
        };
    }
    catch{
        //
    }
};

ajaxEliminarFichaBono = (prefix, empresa, areaNegocio, ficha, periodo, codigo, tipo) => {
    try {
        let method = 'EliminarFichaBono';
        let xhr = new XMLHttpRequest();
        let __form = new FormData();

        __form.append("empresa", empresa);
        __form.append("areaNegocio", areaNegocio);
        __form.append("ficha", ficha);
        __form.append("fecha", periodo + '-01');
        __form.append("codigo", codigo);

        xhr.open('POST', `${prefix}${method}`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                const request = JSON.parse(xhr.responseText);
                let htmlMessage = '';

                if (request.FichaBonos.length > 0) {
                    for (i = 0; i < request.FichaBonos.length; i++) {
                        if (request.FichaBonos[i].Code === "200") {
                            $("#modalAdministraBonos").modal("hide");
                            document.getElementById('div__modalAdministraBonos').innerHTML = '';

                            ajaxViewPartialLoaderTransaccion(prefix, "loaderSearch", empresa, areaNegocio, ficha, periodo, tipo);
                        }
                        else {
                            htmlMessage =
                                `
                                <p>
                                    <span style="background-color: #fe5858; border-radius: 10px; padding: 10px 20px 10px 20px; color: #fff;">
                                       ${request.FichaBonos[i].Message}
                                        <a class="btn-link" onclick="handleHideMessage()">
                                            <img src="/Resources/delete.png" width="15" height="15" style="margin-bottom: 5px; margin-left: 10px; cursor: pointer;">
                                        </a>
                                    </span>
                                </p>
                                `;
                            document.getElementById("div__message").style.display = "block";
                            document.getElementById("div__message").innerHTML = htmlMessage;
                        }
                    }
                }
            }
        };
    }
    catch{
        //
    }
};


ajaxViewPartialLoaderTransaccion = (prefix, htmlElement, empresa, areaNegocio, ficha, periodo, action) => {
    $.ajax({
        type: 'POST',
        url: prefix + 'ViewPartialLoaderTransaccion',
        data: '{ }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        error: function (xhr) {
            $(htmlElement).html(xhr.responseText);
            document.getElementById(htmlElement).innerHTML = xhr.responseText;

            switch (action) {
                case 'Ficha':
                    ajaxSearchFichaBonos(prefix, empresa, '', ficha, periodo, '', action);
                    break;
                case 'AreaNegocio':
                    ajaxSearchFichaBonos(prefix, empresa, areaNegocio, '', periodo, '', action);
                    break;
            }
        }
    });
};