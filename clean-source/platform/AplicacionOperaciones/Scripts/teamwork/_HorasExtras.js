//const __APP = "/AplicacionOperaciones/Asistencia/";
const __APP = "";
const __SERVERHOST = `${window.location.origin}${__APP}`;
const dias = ["Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sabado", "Domingo"];
const meses = ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"];

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

handleChangeMonth = (e) => {
    let periodo = document.getElementById('periodo').value;
    let date = new Date(periodo.substr(0, 4), periodo.substr(5, 2), '01');
    let month = date.getMonth();
    let year = date.getFullYear();
    let fechaInicio = e.dataset.fechainicio;
    let fechaTermino = e.dataset.fechatermino;
    let action = e.dataset.action;

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

    document.getElementById('periodo').value = year + '-' + (month < 10 ? '0' + month : month);

    let domain = handleGetDomain();
    let empresa = document.getElementById('empresa').value;
    let ficha = document.getElementById('ficha').value;
    periodo = document.getElementById('periodo').value;

    ajaxViewPartialLoaderTransaccion(domain, "#loaderSearchAsistencia", empresa, ficha, periodo);
};

handleHideMessage = () => {
    document.getElementById("div__modalMessage").innerHTML = '';
};

handleValidateData = (empresa, ficha, periodo) => {
    let validate = true;

    if (empresa === "" || empresa === null || empresa === undefined || empresa === '0') {
        validate = false;
    }

    if (ficha === "" || ficha === null || ficha === undefined) {
        validate = false;
    }

    if (periodo === "" || periodo === null || periodo === undefined) {
        validate = false;
    }
    return validate;
};

handleValidateDataHoraExtra = (codigoJornada, horaExtra) => {
    let validate = true;

    if (codigoJornada === '' || codigoJornada === null || codigoJornada === undefined) {
        validate = false;
    }

    if (horaExtra === "" || horaExtra === null || horaExtra === undefined) {
        validate = false;
    }

    return validate;
};

handleModalAcept = () => {
    let empresa = document.getElementById('empresa').value;
    let ficha = document.getElementById('ficha').value;
    let periodo = document.getElementById('periodo').value;
    let domain = handleGetDomain();

    ajaxViewPartialLoaderTransaccion(domain, "#loaderSearchAsistencia", empresa, ficha, periodo);
    $("#modalHorasExtras").modal("hide");
    document.querySelector('#div__HorasExtras').innerHTML = '';
};

handleShowModalHorasExtras = (e) => {
    let domain = handleGetDomain();
    let empresa = document.getElementById('empresa').value;
    let ficha = document.getElementById('ficha').value;
    let rut = e.dataset.rut;
    let areaNegocio = e.dataset.areanegocio;
    let horaextra = e.dataset.horaextra;
    let fecha = (Number(e.dataset.day) < 10 ? '0' + e.dataset.day : e.dataset.day) + '-' + e.dataset.month + '-' + e.dataset.year;
    let fechaInvertida = e.dataset.year + '-' + e.dataset.month + '-' + (Number(e.dataset.day) < 10 ? '0' + e.dataset.day : e.dataset.day);

    let htmlConfirmaEvento = '';

    htmlConfirmaEvento +=
        `
        <div class="col-12 col-sm-12 col-md-12 col-lg-12" style="text-align: center; margin-bottom: 10px; font-weight: 100;">
            <h2>Asignación de Horas Extras</h2>
        </div>

        <div class="col-12 col-sm-12 col-md-12 col-lg-12" style="text-align: left;  margin-bottom: 20px; font-weight: 100;">
            <h3>Nombre: ${empresa}</h3>
            <h3>Ficha: ${ficha}</h3>
            <h3>Rut: ${rut}</h3>
            <h3>Fecha: ${fecha}</h3>
        </div>

        <div class="col-12 col-sm-12 col-md-12 col-lg-12" style="text-align: left;  margin-bottom: 20px; font-weight: 100;">
            <table>
                <tr style="height: 50px;">
                    <td>
                        <label>Jornada Laboral: </label>
                    </td>
                    <td>
                        <select id="select__JornadaLaboral" class="form-control" style="width: 350px;"></select>
                    </td>
                </tr>
                <tr style="height: 50px;">
                    <td>
                        <label>Horas Extras: </label>
                    </td>
                    <td>
                        <input type="number" id="input__HorasExtras" class="form-control" style="width: 250px;" value="${horaextra}" />
                    </td>
                </tr>
            </table>
        </div>

        <div id="div__modalMessage" class="col-12 col-sm-12 col-md-12 col-lg-12" style="text-align: left; margin-bottom: 20px; font-weight: 100; text-align:center;"></div>

        <div id="div__modalAcept" class="col-12 col-sm-12 col-md-12 col-lg-12" style="text-align: left; margin-bottom: 20px; font-weight: 100; text-align:center;">
            <button id="searchHorasExtras" onclick="handleConfirmaHoraExtra(this)" 
                    class="btn btn-${horaextra === '' ? 'success' : 'warning'}" style="border-radius: 25px; height: 40px; text-align: left; color: #fff; font-weight:100;"
                    data-action="${horaextra === '' ? 'Guardar' : 'Editar'}">
                ${horaextra === '' ? 'Guardar' : 'Editar'}
                <img src="../Resources/check.svg" width="20" height="20" style="left: -17%;" />
            </button>

            <button id="searchHorasExtras" onclick="handleHideModalHorasExtras()" 
                    class="btn btn-danger" style="border-radius: 25px; height: 40px; text-align: left; color: #fff; font-weight:100;">
                Cancelar
                <img src="../Resources/cancel.svg" width="20" height="20" style="left: -17%;" />
            </button>
        </div>


        <div id="div__modalConfirm" class="col-12 col-sm-12 col-md-12 col-lg-12" style="text-align: left;  margin-bottom: 20px; font-weight: 100; text-align:center; display:none;">
            <div style="text-align: center;  margin-bottom: 20px;">
                <b>
                    <span class="new-font-family" style="font-size: 20px;">
                        ${horaextra === '' ? '¿Está seguro de querer asignar horas extras?' : '¿Está seguro de actualizar las horas extras asignadas?'}
                    </span>
                </b>
            </div>

            <button id="searchHorasExtras" onclick="handleAceptaHoraExtra(this)" 
                    class="btn btn-success" style="border-radius: 25px; height: 40px; text-align: left; color: #fff; font-weight:100;"
                    data-empresa="${empresa}" data-ficha="${ficha}" data-fecha="${fechaInvertida}" data-areanegocio="${areaNegocio}" data-action="${horaextra === '' ? 'Guardar' : 'Editar'}">
                Aceptar
                <img src="../Resources/check.svg" width="20" height="20" style="left: -17%;" />
            </button>


            <button id="searchHorasExtras" onclick="handleCancelaHorasExtras()" 
                    class="btn btn-danger" style="border-radius: 25px; height: 40px; text-align: left; color: #fff; font-weight:100;">
                Cancelar
                <img src="../Resources/cancel.svg" width="20" height="20" style="left: -17%;" />
            </button>
        </div>
        `;

    document.getElementById("div__HorasExtras").innerHTML = htmlConfirmaEvento;
    
    ajaxGetJornadaLaboral(domain, empresa, areaNegocio, ficha);
    ajaxGetHorasExtrasJornada(domain, empresa, ficha, fecha);
    
    $("#modalHorasExtras").modal("show");
};

handleHideModalHorasExtras = () => {
    document.getElementById("div__HorasExtras").innerHTML = '';


    $("#modalHorasExtras").modal("hide");
};

handleChangeEmpresa = () => {
    let empresa = document.getElementById('empresa').value;

    handleGetAreaNegocio(empresa, areaNegocio);
};

handleGetAreaNegocio = (empresa, areaNegocio) => {
    let domain = handleGetDomain();
    let area = '';

    if (empresa !== null && empresa !== "" && empresa !== undefined) {
        if (empresa !== '0') {
            ajaxGetAreaNegocio(domain, empresa, areaNegocio);
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

handleSearchAsistencia = () => {
    let empresa = document.getElementById('empresa').value;
    let ficha = document.getElementById('ficha').value;
    let periodo = document.getElementById('periodo').value;
    let domain = handleGetDomain();

    if (handleValidateData(empresa, ficha, periodo)) {
        ajaxViewPartialLoaderTransaccion(domain, "#loaderSearchAsistencia", empresa, ficha, periodo);
    }
    else {
        document.getElementById('calendarioAsistencia').innerHTML = '';
        document.getElementById('dataTrabajador').innerHTML = '';
    }
};

handleConfirmaHoraExtra = (e) => {
    let action = e.dataset.action;

    document.getElementById("div__modalAcept").style.display = "none";
    document.getElementById("div__modalConfirm").style.display = "block";
};

handleAceptaHoraExtra = (e) => {
    let domain = handleGetDomain();
    let codigoJornada = document.getElementById("select__JornadaLaboral").value;
    let horaExtra = document.getElementById("input__HorasExtras").value;
    let empresa = e.dataset.empresa;
    let areaNegocio = e.dataset.areanegocio;
    let fecha = e.dataset.fecha;
    let ficha = e.dataset.ficha;
    let action = e.dataset.action;
    let htmlMessage = '';

    if (handleValidateDataHoraExtra(codigoJornada, horaExtra)) {
        if (Number(horaExtra) < 1) {
            htmlMessage =
                `
                <p>
                    <span style="background-color: #fe5858; border-radius: 10px; padding: 10px 20px 10px 20px; color: #fff;"> 
                        Las hoars extras no pueden ser ${Number(horaExtra) === 0 ? 'igual a cero.' : 'menor a cero'}
                        <a class="btn-link" onclick="handleHideMessage()">
                            <img src="/Resources/delete.png" width="15" height="15" style="margin-bottom: 5px; margin-left: 10px;">
                        </a>
                    </span>
                </p>
                `;
        }
        else {
            ajaxSetHoraExtra(domain, codigoJornada, horaExtra, empresa, areaNegocio, fecha, ficha, action);
        }
    }
    else {
        htmlMessage =
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
    document.getElementById("div__modalMessage").innerHTML = htmlMessage;
};

handleCancelaHorasExtras = () => {
    document.getElementById("div__modalAcept").style.display = "block";
    document.getElementById("div__modalConfirm").style.display = "none";
};

handleShowLeyend = () => {
    let domain = handleGetDomain();

    ajaxGetLegendV2(domain);
};


ajaxViewPartialLoaderTransaccion = (prefix, htmlElement, empresa, ficha, periodo) => {
    $.ajax({
        type: 'POST',
        url: prefix + 'ViewPartialLoaderTransaccion',
        data: '{ }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        error: function (xhr) {
            $(htmlElement).html(xhr.responseText);
            document.getElementById('div__message').innerHTML = '';
            ajaxSearchDataPersonal(prefix, empresa, ficha, periodo);
        }
    });
};

ajaxSearchDataPersonal = (prefix, empresa, ficha, periodo) => {
    try {
        let method = 'GetDataPersonal';
        let xhr = new XMLHttpRequest();
        let __form = new FormData();

        __form.append("empresa", empresa);
        __form.append("ficha", ficha);
        __form.append("fecha", periodo + '-01');

        xhr.open('POST', `${prefix}${method}`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                const request = JSON.parse(xhr.responseText);
                let areaNegocio = '';

                let personalData = '';
                if (request.Personal.length > 0) {
                    let firstDay = new Date(periodo.substr(0, 4), periodo.substr(5, 2) - 1, 1);
                    let month = firstDay.getMonth();
                    if (request.Personal[0].Code === '200') {
                        areaNegocio = request.Personal[0].AreaNegocio;

                        let cambioMonth =
                            `
                        <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                            <div class="row">
                                <div class="col-12 col-sm-12 col-md-12 col-lg-8">
                                    <div class="row">
                                        <button class="btn btn-warning new-family-teamwork"
                                                style="border-radius: 25px; height: 40px; text-align: left; color: #fff; font-weight:100; font-size: 14px;"
                                                id="prevMonth" onclick="handleChangeMonth(this);"
                                                data-fechainicio="${request.Personal[0].FechaInicio}" data-fechatermino="${request.Personal[0].FechaTermino}"  data-action="prev">
                                                Anterior
                                        </button>

                                        <div id="div__month" style="font-size: 18px; margin: auto 10px; font-size: 18px;"><b>${meses[month]}</b></div>
                                
                                        <button class="btn btn-warning new-family-teamwork"  
                                                style="border-radius: 25px; height: 40px; text-align: left; color: #fff; font-weight:100; margin-right: 10px; font-size: 14px;"
                                                id="nextMonth" onclick="handleChangeMonth(this);"
                                                data-fechainicio="${request.Personal[0].FechaInicio}" data-fechatermino="${request.Personal[0].FechaTermino}" data-action="next">
                                            Siguiente
                                        </button>

                                        <button class="btn btn-teamwork new-family-teamwork"  
                                                style="border-radius: 25px; height: 40px; text-align: left; color: #fff; font-weight:100; display:none; font-size: 14px;"
                                                id="btnSetAsistencia" onclick="handleSetAsistencia()">
                                            Guardar Cambios
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
                        document.getElementById('divCambioMonth').innerHTML = cambioMonth;
                        ajaxGetCierrePeriodo(prefix, empresa, periodo, areaNegocio, "div__mesCerrado");

                        personalData = `<table class="table">
                                        <thead style="background-color: rgb(220, 220, 220);">
                                            <tr>
                                                <th><b><i class="new-family-teamwork" style="font-size: 14px; font-style: normal; display: block; margin-bottom: -8px;">Rut</i></b></th>
                                                <th><b><i class="new-family-teamwork" style="font-size: 14px; font-style: normal; display: block; margin-bottom: -8px;">Ficha</i></b></th>
                                                <th><b><i class="new-family-teamwork" style="font-size: 14px; font-style: normal; display: block; margin-bottom: -8px;">Nombre</i></b></th>
                                                <th><b><i class="new-family-teamwork" style="font-size: 14px; font-style: normal; display: block; margin-bottom: -8px;">Cargo</i></b></th>
                                                <th><b><i class="new-family-teamwork" style="font-size: 14px; font-style: normal; display: block; margin-bottom: -8px;">Inicio Contrato</i></b></th>
                                                <th><b><i class="new-family-teamwork" style="font-size: 14px; font-style: normal; display: block; margin-bottom: -8px;">Termino Contrato</i></b></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td><i class="new-family-teamwork" style="font-size: 12px; font-style: normal; display: block;">${request.Personal[0].Rut}</i></td>
                                                <td><i class="new-family-teamwork" style="font-size: 12px; font-style: normal; display: block;">${request.Personal[0].Ficha}</i></td>
                                                <td><i class="new-family-teamwork" style="font-size: 12px; font-style: normal; display: block;">${request.Personal[0].Nombres}</i></td>
                                                <td><i class="new-family-teamwork" style="font-size: 12px; font-style: normal; display: block;">${request.Personal[0].CargoMod}</i></td>
                                                <td><i class="new-family-teamwork" style="font-size: 12px; font-style: normal; display: block;">${request.Personal[0].FechaInicio}</i></td>
                                                <td><i class="new-family-teamwork" style="font-size: 12px; font-style: normal; display: block;">${request.Personal[0].FechaTermino}</i></td>
                                            </tr>
                                        </tbody>

                                    </table>`;

                        document.getElementById('dataTrabajador').innerHTML = personalData;

                        setTimeout(() => {
                            ajaxSearchAsistencia(prefix, empresa, ficha, periodo, request.Personal[0].FechaInicio, request.Personal[0].FechaTermino, areaNegocio, request.Personal[0].Rut);
                            //ajaxGetLegend(prefix);
                        }, 1000);

                    }
                    else {

                        document.getElementById('dataTrabajador').innerHTML = '';
                        document.getElementById('div__message').innerHTML = `<label style="margin-top: 50px;"><span class="new-teamwork-family" style="background-color: #FFDADA; padding: 40px 10px 40px 10px; font-size: 20px; margin-top: 50px;">${request.Personal[0].Message}</span></label>`;

                        document.getElementById('calendarioAsistencia').innerHTML = '';
                        document.getElementById('dataTrabajador').innerHTML = '';
                        document.getElementById(`dataLeyenda`).innerHTML = '';
                        $('#loaderSearchAsistencia').html('');

                        document.getElementById('div__month').innerHTML = `<b>${meses[month]}</b>`;
                    }
                }

            }
        };

    }
    catch{
        //
    }
};

ajaxSearchAsistencia = (prefix, empresa, ficha, periodo, fechaInicioC, fechaTerminoC, areaNegocio, rut) => {
    try {
        let method = 'ObtenerAsistencia';
        let xhr = new XMLHttpRequest();
        let __form = new FormData();

        __form.append("empresa", empresa);
        __form.append("ficha", ficha);
        __form.append("fecha", periodo + '-01');
        __form.append("areaNegocio", areaNegocio);

        xhr.open('POST', `${prefix}${method}`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                const request = JSON.parse(xhr.responseText);

                let calendario = '';
                let tipoAsistencia = '';
                let bajaConfirmadaClass = '';
                let bajaConfirmadaColor = '';

                let firstDay = new Date(periodo.substr(0, 4), periodo.substr(5, 2) - 1, 1);
                let lastDay = new Date(periodo.substr(0, 4), periodo.substr(5, 2), 0);
                let day1 = firstDay.getDay();
                let month = firstDay.getMonth();

                let dayMonth = '';
                let falCount = 0;
                let licCount = 0;
                let vacCount = 0;
                let hheeCount = 0;

                let diaFalta = 0;
                let diaLic = 0;
                let diaVac = 0;
                let diaHHEE = 0;
                let diaBajaConfirmada = 0;

                if (fechaTerminoC === '') {
                    fechaTerminoC = '31-12-9999';
                }

                let fechaInicio = new Date(Number(fechaInicioC.substr(6, 4)), Number(fechaInicioC.substr(3, 2)) - 1, Number(fechaInicioC.substr(0, 2)));
                let fechaTermino = new Date(Number(fechaTerminoC.substr(6, 4)), Number(fechaTerminoC.substr(3, 2)) - 1, Number(fechaTerminoC.substr(0, 2)));
                let fechaMonth = new Date(periodo.substr(0, 4), periodo.substr(5, 2) - 1, 1);

                let markFiniquito = false;
                let markBajaConfirmada = false;
                let markBajaKAM = false;
                let fechaBajaKAM = '';
                let fechaBajaConfirmada = '';

                if (request.BajasConfirmadas.length > 0) {
                    for (i = 0; i < request.BajasConfirmadas.length; i++) {
                        diaBajaConfirmada = Number(request.BajasConfirmadas[i].DiaBaja);
                        bajaConfirmadaClass = request.BajasConfirmadas[i].Class;
                        bajaConfirmadaColor = request.BajasConfirmadas[i].Color;
                        fechaBajaConfirmada = new Date(Number(request.BajasConfirmadas[i].FechaTermino.substr(0, 4)), Number(request.BajasConfirmadas[i].FechaTermino.substr(5, 2)) - 1, Number(request.BajasConfirmadas[i].FechaTermino.substr(8, 2)));
                    }
                }

                for (i = 0; i < lastDay.getDate(); i++) {
                    let markCell = false;
                    dayMonth += `<tr style="height: 60px;">`;
                    for (j = 0; j < 7; j++) {
                        //OBTIENE DIA DE FALTA O LIBRE
                        if (falCount < request.Asistencia.length) {
                            diaFalta = parseInt(request.Asistencia[falCount].Fecha.substr(0, 2));
                        }
                        //OBTIENE DIA DE LICENCIA MEDICA
                        if (licCount < request.Licencia.length) {
                            diaLic = parseInt(request.Licencia[licCount].DiaLic);
                        }
                        //OBTIENE DIA DE VACACION
                        if (vacCount < request.Vacacion.length) {
                            diaVac = parseInt(request.Vacacion[vacCount].DiaVac);
                        }
                        //HORAS EXTRAS
                        if (hheeCount < request.HorasExtras.length) {
                            diaHHEE = parseInt(request.HorasExtras[hheeCount].DiasHHEE);
                        }

                        markCell = false;
                        if (i === 0) {
                            //MARCA PRIMER DIA DEL MES
                            switch (day1) {
                                case 1:
                                    dayMonth += ``;
                                    break;
                                case 2:
                                    dayMonth += `<td></td>`;
                                    j = j + 1;
                                    break;
                                case 3:
                                    dayMonth += `<td></td><td></td>`;
                                    j = j + 2;
                                    break;
                                case 4:
                                    dayMonth += `<td></td><td></td><td></td>`;
                                    j = j + 3;
                                    break;
                                case 5:
                                    dayMonth += `<td></td><td></td><td></td><td></td>`;
                                    j = j + 4;
                                    break;
                                case 6:
                                    dayMonth += `<td></td><td></td><td></td><td></td><td></td>`;
                                    j = j + 5;
                                    break;
                                case 0:
                                    dayMonth += `<td></td><td></td><td></td><td></td><td></td><td></td>`;
                                    j = j + 6;
                                    break;
                            }

                            if (diaFalta === i + 1 || diaLic === i + 1 || diaVac === i + 1) {
                                //MARCA DIAS LICENCIA MEDICA
                                if (diaLic === i + 1) {
                                    dayMonth += `<td class="${request.Licencia[licCount].Class}">${i + 1}</td>`;
                                    licCount++;
                                    markCell = true;
                                }
                                //VALIDA BAJA CONFIRMADA
                                if (!markBajaConfirmada) {
                                    if (fechaTermino < fechaMonth) {
                                        dayMonth += `<td class="finiquitado" style="color: #fff;">${i + 1}</td>`;
                                        markCell = true;
                                    }
                                    else {
                                        if (diaBajaConfirmada > 0 && diaBajaConfirmada === i + 1) {
                                            dayMonth +=
                                                `
                                                <td class="${bajaConfirmadaClass}" style="color: ${bajaConfirmadaColor};">
                                                    ${i + 1}
                                                </td>
                                                `;
                                            markBajaConfirmada = true;
                                        }
                                        else {
                                            //MARCA DIAS VACACIONES
                                            if (!markCell) {
                                                if (fechaTermino < fechaMonth) {
                                                    dayMonth += `<td class="finiquitado" style="color: #fff;">${i + 1}</td>`;
                                                    markCell = true;
                                                }
                                            }
                                            else {
                                                if (diaVac === i + 1) {
                                                    if (diaHHEE === i + 1) {
                                                        dayMonth +=
                                                            `
                                                    <td class="${request.Vacacion[vacCount].Class}" ${periodoCerrado === 'S' ? '' : 'onclick="handleShowModalHorasExtras(this);"'}
                                                        data-year="${periodo.substr(0, 4)}" data-month="${periodo.substr(5, 2)}" data-day="${i + 1}" data-rut="${rut}" data-areanegocio="${areaNegocio}"
                                                        data-horaextra="${request.HorasExtras[hheeCount].HoraExtra.replace(",", ".")}"
                                                        style="${periodoCerrado === 'S' ? '' : 'cursor:pointer;'} color: #fff;">
                                                        <div style="text-align: left;">${i + 1}</div>
                                                        <div style="text-align: right;">${request.HorasExtras[hheeCount].HoraExtra} HHEE</div>
                                                    </td>
                                                    `;
                                                        hheeCount++;
                                                    }
                                                    else {
                                                        dayMonth +=
                                                            `
                                                    <td class="${request.Vacacion[vacCount].Class}" ${periodoCerrado === 'S' ? '' : 'onclick="handleShowModalHorasExtras(this);"'}
                                                        data-year="${periodo.substr(0, 4)}" data-month="${periodo.substr(5, 2)}" data-day="${i + 1}" data-rut="${rut}" data-areanegocio="${areaNegocio}"
                                                        data-horaextra=""
                                                        style="${periodoCerrado === 'S' ? '' : 'cursor:pointer;'} color: #fff;">
                                                        <div style="text-align: left;">${i + 1}</div>
                                                    </td>
                                                    `;
                                                    }
                                                    vacCount++;
                                                    markCell = true;
                                                }
                                            }
                                        }
                                    }
                                }
                                else {
                                    dayMonth +=
                                        `
                                            <td class="${bajaConfirmadaClass}" style="color: ${bajaConfirmadaColor};">
                                                ${i + 1}
                                            </td>
                                            `;
                                }
                            }
                            //SIN MARCAR DIAS CON ASISTENCIA
                            else {
                                if (Number(fechaTermino.getFullYear() + '' + fechaTermino.getMonth() + '' + fechaTermino.getDate()) === Number(fechaMonth.getFullYear() + '' + fechaMonth.getMonth() + '' + fechaMonth.getDate())) {
                                    if (Number(fechaTermino.getFullYear() + '' + fechaTermino.getMonth() + '' + fechaTermino.getDate()) === Number(fechaBajaConfirmada.getFullYear() + '' + fechaBajaConfirmada.getMonth() + '' + fechaBajaConfirmada.getDate())) {
                                        dayMonth += `<td class="finiquitado" style="color: #fff;">${i + 1}</td>`;
                                    }
                                    else {
                                        dayMonth += `<td class="pendiente" style="color: #fff;">${i + 1}</td>`;
                                        markFiniquito = true;
                                    }
                                    markCell = true;
                                }
                                else if (Number(fechaTermino.getFullYear() + '' + fechaTermino.getMonth() + '' + fechaTermino.getDate()) < Number(fechaMonth.getFullYear() + '' + fechaMonth.getMonth() + '' + fechaMonth.getDate())) {
                                    if (markFiniquito) {
                                        dayMonth += `<td class="pendiente" style="color: #fff;">${i + 1}</td>`;
                                    }
                                    else {
                                        dayMonth += `<td class="finiquitado" style="color: #fff;">${i + 1}</td>`;
                                    }
                                    markCell = true;
                                }
                                else {
                                    if (!markBajaConfirmada) {
                                        if (diaBajaConfirmada > 0 && diaBajaConfirmada === i + 1) {
                                            dayMonth +=
                                                `
                                                <td ${periodoCerrado === 'S' ? '' : 'onclick="handleShowModalHorasExtras(this);"'}
                                                        data-year="${periodo.substr(0, 4)}" data-month="${periodo.substr(5, 2)}" data-day="${i + 1}" data-tipo=""
                                                        style="${periodoCerrado === 'S' ? '' : 'cursor:pointer;'}">
                                                    ${i + 1}
                                                </td>
                                                `;
                                            markBajaConfirmada = true;
                                        }
                                        else {
                                            //SIN CONTRATO
                                            if (fechaInicio > fechaMonth) {
                                                dayMonth += `<td class="sincontrato">${i + 1}</td>`;
                                                markCell = true;
                                            }
                                            //FINIQUITADO
                                            if (fechaTermino < fechaMonth) {
                                                if (!markCell) {
                                                    dayMonth += `<td class="finiquitado" style="color: #fff;">${i + 1}</td>`;
                                                }
                                                markCell = true;
                                            }
                                            if (!markCell) {
                                                if (markBajaKAM) {
                                                    if (fechaTermino > fechaBajaKAM) {
                                                        dayMonth +=
                                                            `
                                                            <td class="create" ${periodoCerrado === 'S' ? '' : 'onclick=""'}
                                                                    data-year="${periodo.substr(0, 4)}" data-month="${periodo.substr(5, 2)}" data-day="${i + 1}" data-tipo="BK" 
                                                                    data-fechaBaja="${fechaBajaKAM.getFullYear()}-${fechaBajaKAM.getMonth()}-${fechaBajaKAM.getDate()}" color: #fff;">
                                                                ${i + 1}
                                                            </td>
                                                            `;
                                                    }
                                                }
                                                else {
                                                    dayMonth +=
                                                        `
                                                        <td ${periodoCerrado === 'S' ? '' : 'onclick="handleShowModalHorasExtras(this);"'}
                                                                data-year="${periodo.substr(0, 4)}" data-month="${periodo.substr(5, 2)}" data-day="${i + 1}" data-tipo=""
                                                                style="${periodoCerrado === 'S' ? '' : 'cursor:pointer;'}">
                                                            ${i + 1}
                                                        </td>
                                                        `;
                                                }
                                            }
                                        }
                                    }
                                    else {
                                        dayMonth +=
                                            `
                                                <td class="${bajaConfirmadaClass}" style="color: ${bajaConfirmadaColor};">
                                                    ${i + 1}
                                                </td>
                                                `;
                                    }
                                }
                            }
                            i++;
                        }
                        else {
                            if (i < lastDay.getDate()) {
                                //MARCA DIAS DE INASISTENCIA
                                if (diaFalta === i + 1 || diaLic === i + 1 || diaVac === i + 1) {
                                    //MARCA DIAS LICENCIA MEDICA
                                    if (licCount < request.Licencia.length) {
                                        if (diaLic === i + 1) {
                                            dayMonth += `<td class="${request.Licencia[licCount].Class}">${i + 1}</td>`;
                                            licCount++;
                                            markCell = true;
                                        }
                                    }

                                    if (!markBajaConfirmada) {
                                        if (fechaTermino < fechaMonth) {
                                            dayMonth += `<td class="finiquitado" style="color: #fff;">${i + 1}</td>`;
                                            markCell = true;
                                        }
                                        else {
                                            if (diaBajaConfirmada > 0 && diaBajaConfirmada === i + 1) {
                                                dayMonth +=
                                                    `
                                                    <td class="${bajaConfirmadaClass}" style="color: ${bajaConfirmadaColor};">
                                                        ${i + 1}
                                                    </td>
                                                    `;
                                                markBajaConfirmada = true;
                                            }
                                            else {
                                                //MARCA DIAS VACACIONES
                                                if (!markCell) {
                                                    if (fechaTermino < fechaMonth) {
                                                        dayMonth += `<td class="finiquitado" style="color: #fff;">${i + 1}</td>`;
                                                        markCell = true;
                                                    }
                                                    else {
                                                        if (vacCount < request.Vacacion.length) {
                                                            if (diaVac === i + 1) {
                                                                if (diaHHEE === i + 1) {
                                                                    dayMonth +=
                                                                        `
                                                                    <td class="${request.Vacacion[vacCount].Class}" ${periodoCerrado === 'S' ? '' : 'onclick="handleShowModalHorasExtras(this);"'}
                                                                        data-year="${periodo.substr(0, 4)}" data-month="${periodo.substr(5, 2)}" data-day="${i + 1}" data-rut="${rut}" data-areanegocio="${areaNegocio}"
                                                                        data-horaextra="${request.HorasExtras[hheeCount].HoraExtra.replace(",", ".")}"
                                                                        style="${periodoCerrado === 'S' ? '' : 'cursor:pointer;'} color: #fff;">
                                                                        <div style="text-align: left;">${i + 1}</div>
                                                                        <div style="text-align: right;">${request.HorasExtras[hheeCount].HoraExtra} HHEE</div>
                                                                    </td>
                                                                    `;
                                                                    hheeCount++;
                                                                }
                                                                else {
                                                                    dayMonth +=
                                                                        `
                                                                        <td class="${request.Vacacion[vacCount].Class}" ${periodoCerrado === 'S' ? '' : 'onclick="handleShowModalHorasExtras(this);"'}
                                                                            data-year="${periodo.substr(0, 4)}" data-month="${periodo.substr(5, 2)}" data-day="${i + 1}" data-rut="${rut}" data-areanegocio="${areaNegocio}"
                                                                            data-horaextra=""
                                                                            style="${periodoCerrado === 'S' ? '' : 'cursor:pointer;'} color: #fff;">
                                                                            <div style="text-align: left;">${i + 1}</div>
                                                                        </td>
                                                                        `;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                //MARCA DIAS DE INASISTENCIA
                                                if (!markCell) {
                                                    if (fechaTermino < fechaMonth) {
                                                        dayMonth += `<td class="finiquitado" style="color: #fff;">${i + 1}</td>`;
                                                        markCell = true;
                                                    }
                                                    else {
                                                        if (falCount < request.Asistencia.length) {
                                                            if (diaFalta === i + 1) {
                                                                if (request.Asistencia[falCount].CodigoAsistencia === 'F') {
                                                                    dayMonth +=
                                                                        `
                                                                        <td class="${request.Asistencia[falCount].Class}" style="color:${request.Asistencia[falCount].Color}">${i + 1}</td>
                                                                        `;
                                                                }
                                                                else {
                                                                    if (diaHHEE === i + 1) {
                                                                        dayMonth +=
                                                                            `
                                                                            <td class="${request.Asistencia[falCount].Class}" ${periodoCerrado === 'S' ? '' : 'onclick="handleShowModalHorasExtras(this);"'}
                                                                                data-year="${periodo.substr(0, 4)}" data-month="${periodo.substr(5, 2)}" data-day="${i + 1}"  data-rut="${rut}" data-areanegocio="${areaNegocio}"
                                                                                data-horaextra="${request.HorasExtras[hheeCount].HoraExtra.replace(",", ".")}"
                                                                                style="${periodoCerrado === 'S' ? '' : 'cursor:pointer;'} color:${request.Asistencia[falCount].Color}">
                                                                                <div style="text-align: left;">${i + 1}</div>
                                                                                <div style="text-align: right;">${request.HorasExtras[hheeCount].HoraExtra} HHEE</div>
                                                                            </td>
                                                                            `;
                                                                        hheeCount++;
                                                                    }
                                                                    else {
                                                                        dayMonth +=
                                                                            `
                                                                            <td class="${request.Asistencia[falCount].Class}" ${periodoCerrado === 'S' ? '' : 'onclick="handleShowModalHorasExtras(this);"'}
                                                                                data-year="${periodo.substr(0, 4)}" data-month="${periodo.substr(5, 2)}" data-day="${i + 1}"  data-rut="${rut}" data-areanegocio="${areaNegocio}"
                                                                                data-horaextra=""
                                                                                style="${periodoCerrado === 'S' ? '' : 'cursor:pointer;'}">
                                                                                ${i + 1}
                                                                            </td>
                                                                            `;
                                                                    }

                                                                }
                                                                falCount++;
                                                                markCell = true;
                                                            }
                                                            else {
                                                                if (diaHHEE === i + 1) {
                                                                    dayMonth +=
                                                                        `
                                                                        <td class="${request.Asistencia[falCount].Class}" ${periodoCerrado === 'S' ? '' : 'onclick="handleShowModalHorasExtras(this);"'}
                                                                            data-year="${periodo.substr(0, 4)}" data-month="${periodo.substr(5, 2)}" data-day="${i + 1}"  data-rut="${rut}" data-areanegocio="${areaNegocio}"
                                                                            data-horaextra="${request.HorasExtras[hheeCount].HoraExtra.replace(",", ".")}"
                                                                            style="${periodoCerrado === 'S' ? '' : 'cursor:pointer;'}">
                                                                            <div style="text-align: left;">${i + 1}</div>
                                                                            <div style="text-align: right;">${request.HorasExtras[hheeCount].HoraExtra} HHEE</div>
                                                                        </td>
                                                                        `;
                                                                    hheeCount++;
                                                                }
                                                                else {
                                                                    dayMonth +=
                                                                        `
                                                                        <td class="${request.Asistencia[falCount].Class}" ${periodoCerrado === 'S' ? '' : 'onclick="handleShowModalHorasExtras(this);"'}
                                                                            data-year="${periodo.substr(0, 4)}" data-month="${periodo.substr(5, 2)}" data-day="${i + 1}"  data-rut="${rut}" data-areanegocio="${areaNegocio}"
                                                                            data-horaextra=""
                                                                            style="${periodoCerrado === 'S' ? '' : 'cursor:pointer;'}">
                                                                            ${i + 1}
                                                                        </td>
                                                                        `;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else {
                                        dayMonth +=
                                            `
                                            <td class="${bajaConfirmadaClass}" style="color: ${bajaConfirmadaColor};">
                                                ${i + 1}
                                            </td>
                                            `;
                                    }
                                }
                                //SIN MARCAR DIAS CON ASISTENCIA
                                else {
                                    if (Number(fechaTermino.getFullYear() + '' + fechaTermino.getMonth() + '' + fechaTermino.getDate()) === Number(fechaMonth.getFullYear() + '' + fechaMonth.getMonth() + '' + fechaMonth.getDate())) {
                                        if (diaBajaConfirmada > 0 && diaBajaConfirmada === i + 1) {
                                            if (Number(fechaTermino.getFullYear() + '' + fechaTermino.getMonth() + '' + fechaTermino.getDate()) === Number(fechaBajaConfirmada.getFullYear() + '' + fechaBajaConfirmada.getMonth() + '' + fechaBajaConfirmada.getDate())) {

                                                markBajaConfirmada = true;
                                            }
                                            else {
                                                dayMonth += `<td class="pendiente" style="color: #fff;">${i + 1}</td>`;
                                                markFiniquito = true;
                                            }
                                        }
                                        else {
                                            markPendienteKAM = true;
                                        }
                                    }
                                    else if (Number(fechaTermino.getFullYear() + '' + fechaTermino.getMonth() + '' + fechaTermino.getDate()) < Number(fechaMonth.getFullYear() + '' + fechaMonth.getMonth() + '' + fechaMonth.getDate())) {
                                        if (markFiniquito) {
                                            dayMonth += `<td class="pendiente" style="color: #fff;">${i + 1}</td>`;
                                        }
                                        else {
                                            dayMonth += `<td class="finiquitado" style="color: #fff;">${i + 1}</td>`;
                                        }
                                        markCell = true;
                                    }
                                    else {
                                        if (!markBajaConfirmada) {
                                            if (diaBajaConfirmada > 0 && diaBajaConfirmada === i + 1) {
                                                if (diaHHEE === i + 1) {
                                                    dayMonth +=
                                                        `
                                                        <td ${periodoCerrado === 'S' ? '' : 'onclick="handleShowModalHorasExtras(this);"'}
                                                            data-year="${periodo.substr(0, 4)}" data-month="${periodo.substr(5, 2)}" data-day="${i + 1}"  data-rut="${rut}" data-areanegocio="${areaNegocio}"
                                                            data-horaextra="${request.HorasExtras[hheeCount].HoraExtra.replace(",", ".")}"
                                                            style="${periodoCerrado === 'S' ? '' : 'cursor:pointer;'}">
                                                            <div style="text-align: left;">${i + 1}</div>
                                                            <div style="text-align: right;">${request.HorasExtras[hheeCount].HoraExtra} HHEE</div>
                                                        </td>
                                                        `;
                                                    hheeCount++;
                                                }
                                                else {
                                                    dayMonth +=
                                                        `
                                                        <td ${periodoCerrado === 'S' ? '' : 'onclick="handleShowModalHorasExtras(this);"'}
                                                                data-year="${periodo.substr(0, 4)}" data-month="${periodo.substr(5, 2)}" data-day="${i + 1}" data-tipo=""
                                                                style="${periodoCerrado === 'S' ? '' : 'cursor:pointer;'}">
                                                            ${i + 1}
                                                        </td>
                                                        `;
                                                }
                                                markBajaConfirmada = true;
                                            }
                                            else {
                                                //SIN CONTRATO
                                                if (fechaInicio > fechaMonth) {
                                                    dayMonth += `<td class="sincontrato">${i + 1}</td>`;
                                                    markCell = true;
                                                }
                                                //FINIQUITADO
                                                if (fechaTermino < fechaMonth) {
                                                    if (!markCell) {
                                                        dayMonth += `<td class="finiquitado" style="color: #fff;">${i + 1}</td>`;
                                                    }
                                                    markCell = true;
                                                }
                                                if (!markCell) {
                                                    if (markBajaKAM) {
                                                        if (fechaTermino > fechaBajaKAM) {
                                                            dayMonth +=
                                                                `
                                                                <td class="create" ${periodoCerrado === 'S' ? '' : 'onclick=""'}
                                                                        data-year="${periodo.substr(0, 4)}" data-month="${periodo.substr(5, 2)}" data-day="${i + 1}" data-tipo="BK" 
                                                                        data-fechaBaja="${fechaBajaKAM.getFullYear()}-${fechaBajaKAM.getMonth()}-${fechaBajaKAM.getDate()}" color: #fff;">
                                                                    ${i + 1}
                                                                </td>
                                                                `;
                                                        }
                                                    }
                                                    else {
                                                        if (diaHHEE === i + 1) {
                                                            dayMonth +=
                                                                `
                                                                    <td ${periodoCerrado === 'S' ? '' : 'onclick="handleShowModalHorasExtras(this);"'}
                                                                        data-year="${periodo.substr(0, 4)}" data-month="${periodo.substr(5, 2)}" data-day="${i + 1}" data-rut="${rut}" data-areanegocio="${areaNegocio}"
                                                                        data-horaextra="${request.HorasExtras[hheeCount].HoraExtra.replace(",", ".")}"
                                                                        style="${periodoCerrado === 'S' ? '' : 'cursor:pointer;'}">
                                                                        <div style="text-align: left;">${i + 1}</div>
                                                                        <div style="text-align: right;">${request.HorasExtras[hheeCount].HoraExtra} HHEE</div>
                                                                    </td>
                                                                    `;
                                                            hheeCount++;
                                                        }
                                                        else {
                                                            dayMonth +=
                                                                `
                                                            <td ${periodoCerrado === 'S' ? '' : 'onclick="handleShowModalHorasExtras(this);"'}
                                                                    data-year="${periodo.substr(0, 4)}" data-month="${periodo.substr(5, 2)}" data-day="${i + 1}" data-rut="${rut}" data-areanegocio="${areaNegocio}"
                                                                    data-horaextra=""
                                                                    style="${periodoCerrado === 'S' ? '' : 'cursor:pointer;'}">
                                                                ${i + 1}
                                                            </td>
                                                            `;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else {
                                            dayMonth +=
                                                `
                                                <td class="${bajaConfirmadaClass}" style="color: ${bajaConfirmadaColor};">
                                                    ${i + 1}
                                                </td>
                                                `;
                                        }
                                    } 
                                }
                            }
                            if (j < 6) {
                                i++;
                            }
                        }
                        fechaMonth = new Date(fechaMonth.getFullYear(), fechaMonth.getMonth(), fechaMonth.getDate() + 1);
                    }
                    dayMonth += `</tr>`;
                }

                //ARMA CALENDARIO
                calendario =
                    `<div id="calendarioAsistencia" class="col-12 col-sm-12 col-md-12 col-lg-12" style="text-align: right;">
                            <div class="row">
                                
                            </div>
                        </div>

                        <table class="table">
                            <thead style="background-color: rgb(220, 220, 220);">
                                <tr>
                                <th class="new-family-teamwork" style="width: calc(100% / 7);">LUN</th>
                                <th class="new-family-teamwork" style="width: calc(100% / 7);">MAR</th>
                                <th class="new-family-teamwork" style="width: calc(100% / 7);">MIE</th>
                                <th class="new-family-teamwork" style="width: calc(100% / 7);">JUE</th>
                                <th class="new-family-teamwork" style="width: calc(100% / 7);">VIE</th>
                                <th class="new-family-teamwork" style="width: calc(100% / 7);">SAB</th>
                                <th class="new-family-teamwork" style="width: calc(100% / 7);">DOM</th>
                                </tr>
                            </thead>
                        <tbody>
                            ${dayMonth}
                        </tbody>
                </table>`;
                document.getElementById('calendarioAsistencia').innerHTML = calendario;
            }
        };
        $('#loaderSearchAsistencia').html('');
    }
    catch{
        $('#loaderSearchAsistencia').html('');
    }
};

ajaxGetJornadaLaboral = (prefix, empresa, areaNegocio, ficha) => {
    try {
        let method = 'ObtenerFichaJornadaLaboral';
        let xhr = new XMLHttpRequest();
        let __form = new FormData();

        __form.append("empresa", empresa);
        __form.append("areaNegocio", areaNegocio);
        __form.append("ficha", ficha);

        xhr.open('POST', `${prefix}${method}`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                const request = JSON.parse(xhr.responseText);
                let htmlFichaJornada = `<option value="" ${request.FichaJornadaLaboral.length === 0 ? 'selected' : ''}>Seleccione...</option>`;

                if (request.FichaJornadaLaboral.length > 0) {
                    for (i = 0; i < request.FichaJornadaLaboral.length; i++) {
                        htmlFichaJornada +=
                            `
                            <option value="${btoa(request.FichaJornadaLaboral[i].CodigoJornada)}" ${request.FichaJornadaLaboral[i].JornadaActiva === 'S' ? 'selected' : ''}>${request.FichaJornadaLaboral[i].NombreJornada}</option>
                            `;
                    }
                }

                document.getElementById("select__JornadaLaboral").innerHTML = htmlFichaJornada;
            }
        };
    }
    catch {
        //
    }
};

ajaxGetHorasExtrasJornada = (prefix, empresa, ficha, fecha) => {
    try {
        let method = 'ObtenerHorasExtras';
        let xhr = new XMLHttpRequest();
        let __form = new FormData();

        __form.append("empresa", empresa);
        __form.append("areaNegocio", areaNegocio);
        __form.append("ficha", ficha);

        xhr.open('POST', `${prefix}${method}`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                const request = JSON.parse(xhr.responseText);

                if (request.HorasExtras.length > 0) {
                    for (i = 0; i < request.HorasExtras.length; i++) {
                        //
                    }
                }
            }
        };
    }
    catch {
        //
    }
};

ajaxSetHoraExtra = (prefix, codigoJornada, horaExtra, empresa, areaNegocio, fecha, ficha, action) => {
    try {
        let method = 'IngresarHoraExtra';
        let xhr = new XMLHttpRequest();
        let __form = new FormData();

        __form.append("codigoJornada", codigoJornada);
        __form.append("horaExtra", horaExtra);
        __form.append("empresa", empresa);
        __form.append("areaNegocio", areaNegocio);
        __form.append("fecha", fecha);
        __form.append("ficha", ficha);
        __form.append("action", action);

        xhr.open('POST', `${prefix}${method}`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                const request = JSON.parse(xhr.responseText);
                document.getElementById("div__modalMessage").innerHTML = '';
                
                if (request.HorasExtras.length > 0) {
                    for (i = 0; i < request.HorasExtras.length; i++) {
                        if (request.HorasExtras[i].Code === "200") {
                            ajaxSetFichaJornadaLaboral(prefix, codigoJornada, empresa, areaNegocio, ficha);
                            $("#modalHorasExtras").modal("hide");
                            handleSearchAsistencia();
                        }
                        else {
                            document.getElementById("div__modalMessage").innerHTML +=
                                `
                                <p>
                                    <span style="background-color: #fe5858; border-radius: 10px; padding: 10px 20px 10px 20px; color: #fff;">
                                        ${request.HorasExtras[i].Message}
                                        <a class="btn-link" onclick="handleHideMessage()">
                                            <img src="/Resources/delete.png" width="15" height="15" style="margin-bottom: 5px; margin-left: 10px;">
                                        </a>
                                    </span>
                                </p>
                                `;
                        }
                    }
                }
            }
        };
    }
    catch {
        //
    }
};

ajaxSetFichaJornadaLaboral = (prefix, codigoJornada, empresa, areaNegocio, ficha) => {
    try {
        let method = 'IngresarFichaJornadaLaboral';
        let xhr = new XMLHttpRequest();
        let __form = new FormData();

        __form.append("codigoJornada", codigoJornada);
        __form.append("empresa", empresa);
        __form.append("areaNegocio", areaNegocio);
        __form.append("ficha", ficha);

        xhr.open('POST', `${prefix}${method}`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                const request = JSON.parse(xhr.responseText);
                document.getElementById("div__modalMessage").innerHTML = '';

                if (request.FichaJornadaLaboral.length > 0) {
                    for (i = 0; i < request.FichaJornadaLaboral.length; i++) {
                        //
                    }
                }
            }
        };
    }
    catch {
        //
    }
};


ajaxGetCierrePeriodo = (prefix, empresa, periodo, areaNegocio, target) => {
    try {
        let method = 'ObtenerCierrePeriodo';
        let xhr = new XMLHttpRequest();
        let __form = new FormData();

        __form.append("empresa", empresa);
        __form.append("fecha", periodo + '-01');
        __form.append("areaNegocio", areaNegocio);

        xhr.open('POST', `${prefix}${method}`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                const request = JSON.parse(xhr.responseText);
                let htmlCierrePeriodo = '';

                if (request.CierrePeriodo.length > 0) {
                    periodoCerrado = request.CierrePeriodo[0].Cerrado;
                    let profile = request.Profile;
                    if (request.CierrePeriodo[0].Cerrado === "S") {
                        htmlCierrePeriodo +=
                            `
                            <span class="btn danger dspl-inline-block new-family-teamwork mr-20 fs-20" 
                                style="margin-top: -4px; border-radius: 25px; height: 40px; text-align: left; color: rgb(217, 83, 79); background-color: #fff; margin: 10px;">
                                Periodo Cerrado
                            </span>
                            `;
                    }
                    else {
                        if (profile === 'Administrador' || profile === 'Jefe de Operaciones' || profile === 'Administrador Operaciones') {
                            htmlCierrePeriodo +=
                                `
                                <button class="btn btn-teamwork new-family-teamwork fs-18"  
                                        style="border-radius: 25px; height: 40px; text-align: left; color: #fff; font-weight:100;"
                                        id="button__CierraProceso" onclick="handleConfirmaEvento(this)"
                                        data-empresa="${empresa}" data-fecha="${periodo}-01" data-areanegocio="${areaNegocio}" data-action="CierrePeriodo">
                                    Cerrar Periodo
                                </button>
                                `;
                        }
                    }
                }
                document.getElementById(target).innerHTML = htmlCierrePeriodo;
            }
        };
    }
    catch {
        //
    }
};

ajaxGetLegend = (prefix) => {
    try {
        let method = 'GetLegend';
        let xhr = new XMLHttpRequest();
        let __form = new FormData();

        xhr.open('POST', `${prefix}${method}`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                const request = JSON.parse(xhr.responseText);
                let leyend = '';

                if (request.Leyenda.length > 0) {
                    leyend += `<ol style="list-style-type:none;">`;
                    for (i = 0; i < request.Leyenda.length; i++) {
                        leyend += `<div style="display: inline-block;">
                                    <input disabled style="width:40px; height:40px; text-align:center; border-radius: 25px; margin: 5px 0 5px 5px; color:${request.Leyenda[i].Color};"
                                           class="${request.Leyenda[i].Class}" value="${request.Leyenda[i].CodigoAsistencia}" />
                                    <span>${request.Leyenda[i].Nombre}</span>
                                   </div>`;
                    }
                    leyend += `</ol>`;
                }

                document.querySelector(`#dataLeyenda`).innerHTML = leyend;
            }
        };
    }
    catch{
        //
    }
};

ajaxGetLegendV2 = (prefix) => {
    try {
        let method = 'GetLegend';
        let xhr = new XMLHttpRequest();
        let __form = new FormData();

        xhr.open('POST', `${prefix}${method}`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                const request = JSON.parse(xhr.responseText);
                let leyendHTML = '';
                let leyendDiv = '';
                let mitadLegend = 0;

                if (request.Leyenda.length > 0) {
                    mitadLegend = request.Leyenda.length / 2;


                    for (i = 0; i < request.Leyenda.length; i++) {
                        leyendHTML +=
                            `<tr>
                                <td>
                                    <div style="display: inline-block;">
                                        <input disabled style="width:40px; height:40px; text-align:center; border-radius: 25px; margin: 5px 0 5px 5px; color:${request.Leyenda[i].Color};"
                                               class="${request.Leyenda[i].Class}" value="${request.Leyenda[i].CodigoAsistencia}" />
                                        <span>${request.Leyenda[i].Nombre}</span>
                                   </div>
                                </td>
                            </tr>`;

                        if (mitadLegend === (i + 1) || request.Leyenda.length === (i + 1)) {
                            leyendDiv +=
                                `
                                <div class="col-12 col-sm-12 col-md-12 col-lg-6">
                                    <table>
                                        ${leyendHTML}
                                    </table>
                                </div>
                                `;
                            leyendHTML = '';
                        }

                    }

                }

                document.getElementById('div__modalLeyenda').innerHTML = leyendDiv;
                $("#modalLeyenda").modal("show");
            }
        };
    }
    catch{
        //
    }
};