//const __APP = "/AplicacionOperaciones/Asistencia/";
const __APP = "";
const __SERVERHOST = `${window.location.origin}${__APP}`;

handleHideMessage = () => {
    document.getElementById("div__message").innerHTML = '';
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

handleReturnMenuAsistencia = () => {
    window.location.href = `${__SERVERHOST}/Asistencia`;
    //window.location.href = `${__SERVERHOST}`;
};

handleChangeAction = () => {
    let check = document.getElementById("check__tipoAccion");
    let empresa = document.getElementById('empresa').value;
    let areaNegocio = document.getElementById('areaNegocio').value;
    let htmlJornadaLaboral = '';

    document.getElementById("div__message").innerHTML = ``;
    document.getElementById("div__jornadaLaboral").innerHTML = ``;

    if (check.checked) {
        htmlJornadaLaboral +=
            `
            <div class="row ml-auto mr-auto" style="width:50%; margin-top:10px; text-align: left;">
                <table style="width:100%;">
                    <tr style="height: 50px;">
                        <td colspan="2">
                            <label style="font-size: 18px;">Nueva Jornada Laboral</label>
                        </td>
                        <td></td>
                    </tr>
                    <tr style="height: 50px;">
                        <td style="width: 35%;">
                            <label>Nombre Jornada Laboral: </label>
                        </td>
                        <td>
                            <input type="text" id="input__JornadaLaboral" class="form-control" style="width: 300px; padding-top: 10px;" />
                        </td>
                    </tr>
                    <tr style="height: 50px;">
                        <td style="width: 35%;">
                            <label>Descripción Jornada Laboral: </label>
                        </td>
                        <td>
                            <input type="text" id="input__Descripcion" class="form-control" style="width: 300px;" />
                        </td>
                    </tr>
                    <tr style="height: 50px;">
                        <td style="width: 35%;">    
                            <label>Cantidad Horas Semanales: </label>
                        </td>
                        <td>
                            <input type="number" id="input__HorasSemanales" class="form-control" style="width: 300px;" />
                        </td>
                    </tr>
                    <tr style="height: 50px;">
                        <td style="width: 35%;">    
                            <label>Porcentaje Pago Hora Extra: </label>
                        </td>
                        <td>
                            <input type="number" id="input__PorcentajePago" class="form-control" style="width: 300px;" />
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <button id="btn__IngresarJornada" 
                                    onclick="handleIngresaJornada(this)" 
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
        document.getElementById('div__jornadaLaboral').innerHTML = htmlJornadaLaboral;
    }
    else {
        handleObtenerJornadaLaboral(empresa, areaNegocio);
    }

    
};

handleChangeEmpresa = () => {
    let empresa = document.getElementById('empresa').value;

    handleGetAreaNegocio(empresa, areaNegocio);
};

handleGetAreaNegocio = (empresa, areaNegocio) => {
    let domain = handleGetDomain();
    let check = document.getElementById("check__tipoAccion");
    let area = '';

    if (empresa !== null && empresa !== "" && empresa !== undefined) {
        if (empresa !== '0') {
            ajaxGetAreaNegocio(domain, empresa, areaNegocio, check);
        }
        else {
            area = `<option value="0" data-input="input">Seleccione Area de Negocio</option>`;
            document.getElementById('areaNegocio').innerHTML = area;
        }
    }
    else {
        area = `<option value="0" data-input="input">Seleccione Area de Negocio</option>`;
        document.getElementById('areaNegocio').innerHTML = area;
    }
};

handleValidateDataJornada = (empresa, areaNegocio) => {
    let validate = true;

    if (empresa === "" || empresa === null || empresa === undefined || empresa === '0') {
        validate = false;
    }

    if (areaNegocio === "" || areaNegocio === null || areaNegocio === undefined || areaNegocio === '0') {
        validate = false;
    }
    return validate;
};

handleValidateIngresarJornada = (empresa, areaNegocio, nombreJornada, descripcionJornada, horasSemanales, porcentaje) => {
    let validate = true;

    if (empresa === "" || empresa === null || empresa === undefined || empresa === '0') {
        validate = false;
    }

    if (areaNegocio === "" || areaNegocio === null || areaNegocio === undefined || areaNegocio === '0') {
        validate = false;
    }

    if (nombreJornada === "" || nombreJornada === null || nombreJornada === undefined) {
        validate = false;
    }

    if (descripcionJornada === "" || descripcionJornada === null || descripcionJornada === undefined) {
        validate = false;
    }

    if (horasSemanales === "" || horasSemanales === null || horasSemanales === undefined) {
        validate = false;
    }

    if (porcentaje === "" || porcentaje === null || porcentaje === undefined) {
        validate = false;
    }
    return validate;
};

handleObtenerJornadaLaboral = () => {
    let domain = handleGetDomain();
    let check = document.getElementById("check__tipoAccion");
    let empresa = document.getElementById('empresa').value;
    let areaNegocio = document.getElementById('areaNegocio').value;

    if (!check.checked) {
        if (handleValidateDataJornada(empresa, areaNegocio)) {
            document.getElementById("div__message").innerHTML = '';
            ajaxObtenerJornadaLaboral(domain, empresa, areaNegocio);
        }
        else {
            document.getElementById("div__jornadaLaboral").innerHTML = '';
        }
    }
};

handleActualizaEstadoJornada = (e) => {
    let domain = handleGetDomain();
    let empresa = e.dataset.empresa;
    let areaNegocio = e.dataset.areanegocio;
    let codigoJornada = e.dataset.jornada;
    let vigente = e.dataset.vigente;

    ajaxActualizaJornadaLaboral(domain, codigoJornada, vigente === 'S' ? 'N' : 'S', empresa, areaNegocio, "", "", "", "Estado");
};

handleIngresaJornada = (e) => {
    let domain = handleGetDomain();
    let empresa = e.dataset.empresa;
    let areaNegocio = e.dataset.areanegocio;
    let nombreJornada = document.getElementById("input__JornadaLaboral").value;
    let descripcionJornada = document.getElementById("input__Descripcion").value;
    let horasSemanales = document.getElementById("input__HorasSemanales").value;
    let porcentaje = document.getElementById("input__PorcentajePago").value;

    if (handleValidateIngresarJornada(empresa, areaNegocio, nombreJornada, descripcionJornada, horasSemanales, porcentaje)) {
        document.getElementById("div__message").innerHTML = '';
        ajaxIngresarJornadaLaboral(domain, empresa, areaNegocio, nombreJornada, descripcionJornada, horasSemanales, porcentaje);
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

handleEditJornadaLaboral = (e) => {
    let empresa = e.dataset.empresa;
    let areaNegocio = e.dataset.areanegocio;

    let codigoJornada = e.dataset.jornada;
    let nombreJornada = e.dataset.nombre;
    let descripcionJornada = e.dataset.descripcion;
    let horasSemanales = e.dataset.horas;
    let vigente = e.dataset.vigente;
    let porcentaje = e.dataset.porcentaje;

    document.getElementById('div__message').innerHTML = '';

    document.getElementById('div__jornadaLaboral').innerHTML =
        `
            <div class="row ml-auto mr-auto" style="width:50%; margin-top:10px; text-align: left;">
                <table style="width:100%;">
                    <tr style="height: 50px;">
                        <td colspan="2">
                            <b><label style="font-size: 18px;">Editar Jornada Laboral</label></b>
                        </td>
                        <td></td>
                    </tr>

                     <tr style="height: 30px;">
                        <td style="width: 35%;">
                            <b><label class="new-family-teamwork dspl-block lh-10 fs-16 mt-10" style="color: rgb(100, 100, 100)">Código: </label></b>
                        </td>
                        <td>
                            <b><label class="new-family-teamwork dspl-block lh-10 fs-16 mt-10" style="color: rgb(100, 100, 100)">${atob(codigoJornada)} </label></b>
                        </td>
                    </tr>

                    <tr style="height: 50px;">
                        <td style="width: 35%;">
                            <label>Jornada Laboral: </label>
                        </td>
                        <td>
                            <input type="text" id="input__JornadaLaboral" class="form-control" style="width: 400px; padding-top: 10px;" value="${atob(nombreJornada)}" />
                        </td>
                    </tr>
                    <tr style="height: 50px;">
                        <td style="width: 35%;">
                            <label>Descripción: </label>
                        </td>
                        <td>
                            <input type="text" id="input__Descripcion" class="form-control" style="width: 400px;" value="${atob(descripcionJornada)}" />
                        </td>
                    </tr>
                    <tr style="height: 50px;">
                        <td style="width: 35%;">    
                            <label>Horas Semanales: </label>
                        </td>
                        <td>
                            <input type="number" id="input__HorasSemanales" class="form-control" style="width: 400px;" value="${atob(horasSemanales)}" />
                        </td>
                    </tr>
                    <tr style="height: 50px;">
                        <td style="width: 35%;">
                            <label>Porcentaje Pago Hora Extra: </label>
                        </td>
                        <td>
                            <input type="number" id="input__PorcentajePago" class="form-control" style="width: 400px;" value="${porcentaje}" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <button id="btn__IngresarJornada" 
                                    onclick="handleEditarJornada(this)" 
                                    class="btn btn-success"  
                                    style="border-radius: 25px; height: 40px; text-align: left; color: #fff; font-weight:100;" 
                                    data-jornada="${codigoJornada}" data-empresa="${empresa}" data-areanegocio="${areaNegocio}" data-vigente="${vigente}">
                                Actualizar
                                <img src="../Resources/check.svg" width="20" height="20" style="left: -17%;" />
                            </button>

                            <button id="btn__Volver"
                                    onclick="handleObtenerJornadaLaboral()"
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

handleEditarJornada = (e) => {
    let domain = handleGetDomain();
    let empresa = e.dataset.empresa;
    let areaNegocio = e.dataset.areanegocio;
    let codigoJornada = e.dataset.jornada;
    let vigente = e.dataset.vigente;
    let nombreJornada = document.getElementById("input__JornadaLaboral").value;
    let descripcionJornada = document.getElementById("input__Descripcion").value;
    let horasSemanales = document.getElementById("input__HorasSemanales").value;
    let porcentaje = document.getElementById("input__PorcentajePago").value;

    if (handleValidateIngresarJornada(empresa, areaNegocio, nombreJornada, descripcionJornada, horasSemanales, porcentaje)) {
        document.getElementById("div__message").innerHTML = '';
        ajaxActualizaJornadaLaboral(domain, codigoJornada, vigente, empresa, areaNegocio, nombreJornada, descripcionJornada, horasSemanales, porcentaje, "Jornada");
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
                let area = `<option value="0" data-input="input">Seleccione Area de Negocio</option>`;

                if (request.AreaNegocio.length > 0) {
                    for (i = 0; i < request.AreaNegocio.length; i++) {
                        if (areaNegocio === request.AreaNegocio[i].Codigo) {
                            area += `<option value="${request.AreaNegocio[i].Codigo}" data-input="input" selected>${request.AreaNegocio[i].Codigo} - ${request.AreaNegocio[i].Nombre}</option>`;
                        }
                        else {
                            area += `<option value="${request.AreaNegocio[i].Codigo}" data-input="input">${request.AreaNegocio[i].Codigo} - ${request.AreaNegocio[i].Nombre}</option>`;
                        }
                    }

                }

                document.getElementById('areaNegocio').innerHTML = area;

                setTimeout(() => {
                    if (check.checked) {
                        handleObtenerJornadaLaboral();
                    }
                }, 100);
            }
        };
    }
    catch{
        //
    }
};

ajaxObtenerJornadaLaboral = (prefix, empresa, areaNegocio) => {
    try {
        let method = 'ObtenerJornadaLaboral';
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
                let htmlJornadaLaboral = '';

                if (request.JornadaLaboral.length > 0) {
                    htmlJornadaLaboral =
                        `
                        <div class="row ml-auto mr-auto" style="width:85%; margin-top:10px; text-align: left;">
                            <table style="width:100%">
                        `;

                    for (i = 0; i < request.JornadaLaboral.length; i++) {
                        if (request.JornadaLaboral[i].Code === '200') {
                            htmlJornadaLaboral +=
                                `
                                <tr>
                                    <td style="width:75%; padding: 10px; border-bottom: solid 1px #939393;">
                                        <div style="color: rgb(100, 100, 100)">
                                            <label class="new-family-teamwork dspl-block lh-10 fs-18 mt-10">
                                                <b>${request.JornadaLaboral[i].NombreJornada}</b>
                                            </label>
                                        </div>

                                        <div style="color: rgb(100, 100, 100)">
                                            <label class="new-family-teamwork dspl-block lh-10 fs-16 mt-10">
                                                <b>Código: ${request.JornadaLaboral[i].CodigoJornada}</b>
                                            </label>
                                        </div>

                                        <div style="color: rgb(100, 100, 100)">
                                            <label class="new-family-teamwork dspl-block lh-10 fs-14 mt-10">
                                                Descripción de la Jornada: ${request.JornadaLaboral[i].DescripcionJornada === '' ? 'Sin Descripcion' : request.JornadaLaboral[i].DescripcionJornada}
                                            </label>
                                        </div>

                                        <div style="color: rgb(100, 100, 100)">
                                            <label class="new-family-teamwork dspl-block lh-10 fs-12 mt-10">
                                                Horas Semanal: ${request.JornadaLaboral[i].HorasSemanal}
                                            </label>
                                        </div>

                                        <div style="color: rgb(100, 100, 100)">
                                            <label class="new-family-teamwork dspl-block lh-10 fs-12 mt-10">
                                                Porcentaje Pago Hora Extra: ${request.JornadaLaboral[i].PorcentajePago}%
                                            </label>
                                        </div>
                                    </td>

                                    <td style="padding: 10px; border-bottom: solid 1px #939393;">
                                        <button class="btn new-family-teamwork fs-18 dspl-inline-block"
                                                style="min-width: 50px; border: none; text-align: center; font-weight: 100; border: 0; margin: none; background-color: transparent!important; font-weight: 100"
                                                id="button__editJornadaLaboral" onclick="handleEditJornadaLaboral(this)"
                                                data-jornada="${btoa(request.JornadaLaboral[i].CodigoJornada)}" data-nombre="${btoa(request.JornadaLaboral[i].NombreJornada)}" 
                                                data-descripcion="${btoa(request.JornadaLaboral[i].DescripcionJornada)}" data-horas="${btoa(request.JornadaLaboral[i].HorasSemanal)}"
                                                data-empresa="${empresa}" data-areanegocio="${areaNegocio}" data-vigente="${request.JornadaLaboral[i].Vigente}"
                                                data-porcentaje="${request.JornadaLaboral[i].PorcentajePago}">
                                            <span style='width: 50px; height: 50px; border-radius: 50%; display: flex; align-items: center; text-align: center;'
                                                  class='btn btn-warning pdt-5 pdr-5 pdb-5 pdl-5'>
                                                <img src='../Resources/editar.svg' width='30' height='30' style='position: relative; left: 10%;' />
                                            </span>
                                        </button>

                                        <button class="btn btn-${request.JornadaLaboral[i].Vigente === 'S' ? 'anulado' : 'success'}"
                                                style="border-radius: 25px; text-align: left; color: #fff; font-weight:100;"
                                                onclick="handleActualizaEstadoJornada(this)"
                                                data-jornada="${btoa(request.JornadaLaboral[i].CodigoJornada)}" data-vigente="${request.JornadaLaboral[i].Vigente}" data-empresa="${empresa}" data-areanegocio="${areaNegocio}">
                                            <span>${request.JornadaLaboral[i].Vigente === 'S' ? 'Deshabilitar' : 'Habilitar'} </span>
                                            <img src="/Resources/${request.JornadaLaboral[i].Vigente === 'S' ? 'off' : 'on'}.svg" width="28" height="28" style="position: relative;">
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
                                        ${request.JornadaLaboral[i].Message}
                                        <a class="btn-link" onclick="handleHideMessage()">
                                            <img src="/Resources/delete.png" width="15" height="15" style="margin-bottom: 5px; margin-left: 10px;">
                                        </a>
                                    </span>
                                </p>
                                `;

                            document.getElementById("div__message").innerHTML = htmlError;
                        }
                    }

                    htmlJornadaLaboral +=
                        `
                            </table>
                        </div>
                        `;
                }

                document.getElementById("div__jornadaLaboral").innerHTML = htmlJornadaLaboral;
            }
        };
    }
    catch {
        //
    }
};

ajaxActualizaJornadaLaboral = (prefix, codigoJornada, vigente, empresa, areaNegocio, nombreJornada, descripcionJornada, horasSemanales, porcentaje, action) => {
    try {
        let method = 'ActualizarJornadaLaboral';
        let xhr = new XMLHttpRequest();
        let __form = new FormData();

        __form.append("codigoJornada", codigoJornada);
        __form.append("vigente", vigente);
        __form.append("nombreJornada", nombreJornada);
        __form.append("descripcionJornada", descripcionJornada);
        __form.append("horasSemanales", horasSemanales);
        __form.append("action", action);
        __form.append("porcentaje", porcentaje);

        xhr.open('POST', `${prefix}${method}`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                const request = JSON.parse(xhr.responseText);
                let htmlMessage = '';

                if (request.JornadaLaboral.length > 0) {
                    for (i = 0; i < request.JornadaLaboral.length; i++) {
                        if (request.JornadaLaboral[i].Code === '200') {
                            ajaxObtenerJornadaLaboral(prefix, empresa, areaNegocio);
                        }

                        htmlMessage +=
                            `
                            <p>
                                <span style="background-color: #${request.JornadaLaboral[i].Code === '200' ? '007a00' : 'fe5858'}; border-radius: 10px; padding: 10px 20px 10px 20px; color: #fff;">
                                    ${request.JornadaLaboral[i].Message}
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

ajaxIngresarJornadaLaboral = (prefix, empresa, areaNegocio, nombreJornada, descripcionJornada, horasSemanales, porcentaje) => {
    try {
        let method = 'IngresarJornadaLaboral';
        let xhr = new XMLHttpRequest();
        let __form = new FormData();

        __form.append("empresa", empresa);
        __form.append("areaNegocio", areaNegocio);
        __form.append("nombreJornada", nombreJornada);
        __form.append("descripcionJornada", descripcionJornada);
        __form.append("horasSemanales", horasSemanales);
        __form.append("porcentaje", porcentaje);

        xhr.open('POST', `${prefix}${method}`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                const request = JSON.parse(xhr.responseText);
                let htmlMessage = '';

                if (request.JornadaLaboral.length > 0) {
                    for (i = 0; i < request.JornadaLaboral.length; i++) {
                        if (request.JornadaLaboral[i].Code === '200') {
                            document.getElementById("input__JornadaLaboral").value = '';
                            document.getElementById("input__Descripcion").value = '';
                            document.getElementById("input__HorasSemanales").value = '';
                            document.getElementById("input__PorcentajePago").value = '';
                        }

                        htmlMessage +=
                            `
                            <p>
                                <span style="background-color: #${request.JornadaLaboral[i].Code === '200' ? '007a00' : 'fe5858'}; border-radius: 10px; padding: 10px 20px 10px 20px; color: #fff;">
                                    ${request.JornadaLaboral[i].Message}
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
