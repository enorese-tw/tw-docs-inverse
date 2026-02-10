//const __APP = "/AplicacionOperaciones/Asistencia/";
const __APP = "";
const __SERVERHOST = `${window.location.origin}${__APP}`;

const dias = [ "Domingo", "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sabado"];
const meses = ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"];
let diasCargaMasiva = [];
let arrayAsistencia = [];
let arrayAreaNegocio = [];
let arrayBonos = [];
let periodoCerrado = '';


(function () {
    //SELECCIONA CARGA MASIVA RELOJ CONTROL
    if (window.location.pathname.split('/')[2] === 'SolicitudAsistenciaRelojControl') {
        const params = new URLSearchParams(window.location.search)
        let select = document.getElementById('select__plantillaCarga');
        let plantilla = '';

        for (const param of params) {
            plantilla = param[1];
        }

        if (plantilla !== '') {
            for (i = 0; i < select.options.length; i++) {
                if (select.options[i].value === plantilla) {
                    select.selectedIndex = i;
                }
            }
        }
    }
})();



document.querySelector('html').addEventListener("click", (e) => {
    //
});

if (document.querySelector('#modalMsjAsistencia') !== null) {
    document.querySelector('#modalMsjAsistencia').addEventListener("click", (e) => {
        if (!document.querySelector('.modal-content').contains(e.target)) {
            //handleModalAcept();
        }
    });
}




//FUNCIONES
handleSetAsistencia = () => {
    let empresa = document.getElementById('empresa').value;
    let ficha = document.getElementById('ficha').value;
    let periodo = document.getElementById('periodo').value;
    let domain = handleGetDomain();
    let dayAsistencia = '';

    for (i = 0; i < arrayAsistencia.length; i++) {
        dayAsistencia += arrayAsistencia[i] + ';';
    }

    ajaxInsertAsistencia(domain, empresa, ficha, periodo, dayAsistencia);
};

handleChangeMonth = (e) => {
    let periodo = document.getElementById('periodo').value;
    let date = new Date(periodo.substr(0, 4), periodo.substr(5, 2), '01');
    let month = date.getMonth();
    let year = date.getFullYear();
    let fechaInicio = e.dataset.fechainicio;
    let fechaTermino = e.dataset.fechatermino;
    let action = e.dataset.action;

    while (arrayAsistencia.length) {
        arrayAsistencia.pop();
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

    document.getElementById('periodo').value = year + '-' + (month < 10 ? '0' + month : month);

    let domain = handleGetDomain();
    let empresa = document.getElementById('empresa').value;
    let ficha = document.getElementById('ficha').value;
    periodo = document.getElementById('periodo').value;

    ajaxViewPartialLoaderTransaccion(domain, "#loaderSearchAsistencia", empresa, ficha, periodo);
};

handleSearchAsistencia = () => {
    let empresa = document.getElementById('empresa').value;
    let ficha = document.getElementById('ficha').value;
    let periodo = document.getElementById('periodo').value;
    let domain = handleGetDomain();

    if (!handleValidateReportData(empresa, periodo)) {
        document.getElementById('reportAsist').style.display = 'none';
    }
    else {
        //document.getElementById('reportAsist').style.display = 'inline-block';
    }

    if (handleValidateData(empresa, ficha, periodo)) {
        while (arrayAsistencia.length) {
            arrayAsistencia.pop();
        }


        ajaxViewPartialLoaderTransaccion(domain, "#loaderSearchAsistencia", empresa, ficha, periodo);
    }
    else {
        document.getElementById('calendarioAsistencia').innerHTML = '';
        document.getElementById('dataTrabajador').innerHTML = '';
    }
    $('#loaderSearchAsistencia').html('');
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

handleChangeFalta = (e) => {
    let day = e.dataset.day;
    let month = e.dataset.month;
    let year = e.dataset.year;
    let tipoAsistencia = document.querySelector('input[name="tipoAsistencia"]:checked').value;
    let clase = document.querySelector('input[name="tipoAsistencia"]:checked').dataset.clase;
    let color = document.querySelector('input[name="tipoAsistencia"]:checked').dataset.color;
    let tipo = e.dataset.tipo;

    if (!arrayAsistencia.find(item => item === year + month + (day < 10 ? '0' + day : day) + ('-' + tipoAsistencia))) {
        //AGREGA NUEVA INASISTENCIA
        arrayAsistencia.push(year + month + (day < 10 ? '0' + day : day) + ('-' + tipoAsistencia));
        //QUITA INASISTENCIA ANTERIOR
        arrayAsistencia = arrayAsistencia.filter(item => !(year + month + (day < 10 ? '0' + day : day) + '-' + tipo).includes(item));
        e.dataset.tipo = tipoAsistencia;

        e.classList.value = '';
        e.classList.add(clase);
        e.style.color = color;
    }
    else {
        //DESMARCA INASISTENCIA
        arrayAsistencia = arrayAsistencia.filter(item => !(year + month + (day < 10 ? '0' + day : day) + ('-' + tipoAsistencia)).includes(item));
        e.dataset.tipo = '';
        e.classList.remove(clase);
        e.style.color = '';
    }

    if (arrayAsistencia.length > 0) {
        document.getElementById('btnSetAsistencia').style.display = 'inline-block';
    }
    else {
        document.getElementById('btnSetAsistencia').style.display = 'none';
    }
};

handleDelAsistencia = (e) => {
    let empresa = document.getElementById('empresa').value;
    let ficha = document.getElementById('ficha').value;
    let periodo = document.getElementById('periodo').value;
    let fecha = e.dataset.year + e.dataset.month + (e.dataset.day < 10 ? '0' + e.dataset.day : e.dataset.day);
    let domain = handleGetDomain();

    ajaxDeleteAsistencia(domain, empresa, ficha, fecha, periodo);
};

handleUpdAsistencia = (e) => {
    let empresa = document.getElementById('empresa').value;
    let ficha = document.getElementById('ficha').value;
    let periodo = document.getElementById('periodo').value;
    let fecha = e.dataset.fecha;
    let fechaInicio = e.dataset.fechainicio;
    let fechaTermino = e.dataset.fechatermino;
    
    let domain = handleGetDomain();

    let tipoAsistencia = e.dataset.action === 'ActualizarV1' ? document.querySelector('input[name="tipoUpdAsistencia"]:checked').value :
                                                      e.dataset.tipo;

    e.dataset.action === 'ActualizarV1' ?
        ajaxUpdateAsistencia(domain, empresa, ficha, fecha, periodo, tipoAsistencia, fechaInicio, fechaTermino) :
        ajaxDeleteAsistencia(domain, empresa, ficha, fecha, periodo, tipoAsistencia, fechaInicio, fechaTermino);

};

handleShowUpdate = (e) => {
    let click = true;

    [e.classList].map(x => {
        if (x === "change__options__tipo__asistencia") {
            click = false;
        }
    });

    if (click) {
        let domain = handleGetDomain();
        ajaxGetTipoAsistencia(domain, e.id, e.dataset.tipo, e.dataset.year, e.dataset.month, e.dataset.day, e.dataset.action);
    }
};

handleModalAcept = () => {
    let empresa = document.getElementById('empresa').value;
    let ficha = document.getElementById('ficha').value;
    let periodo = document.getElementById('periodo').value;
    let domain = handleGetDomain();

    ajaxViewPartialLoaderTransaccion(domain, "#loaderSearchAsistencia", empresa, ficha, periodo);
    $("#modalMsjAsistencia").modal("hide");
    document.querySelector('#divMessageAsistencia').innerHTML = '';
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

handleValidateReportData = (empresa, periodo) => {
    let validate = true;

    if (empresa === "" || empresa === null || empresa === undefined || empresa === '0') {
        validate = false;
    }

    if (periodo === "" || periodo === null || periodo === undefined) {
        validate = false;
    }
    return validate;
};

handleAsistenciaReport = () => {
    let empresa = document.getElementById('empresa').value;
    let periodo = document.getElementById('periodo').value;
    let domain = handleGetDomain();

    window.location.href = domain + 'GenerateExcel?excel=YXNpc3RlbmNpYQ==&empresa=' + empresa + '&periodo=' + periodo + '-01';
};

handleShowLeyend = () => {
    let domain = handleGetDomain();

    ajaxGetLegendV2(domain);
};

handleAddAreaNegocio = () => {
    let empresa = document.getElementById('empresa').value;
    let areaNegocio = document.getElementById('input__areaNegocio').value;
    let datalist = document.getElementById('select__areaNegocio');
    let htmlAreaNegocio = '';
    let existeAreaNegocio = false;

    if (empresa === null || empresa === '' || empresa === undefined || empresa === '0') {
        document.getElementById('div__errorReporte').innerHTML =
            `
            <p>
                <span style="background-color: #fe5858; border-radius: 10px; padding: 10px 20px 10px 20px; color: #fff;">
                    Favor seleccionar Empresa.
                    <a class="btn-link" onclick="handleHideMessage('div__errorReporte')">
                        <img src="/Resources/delete.png" width="15" height="15" style="margin-bottom: 5px; margin-left: 10px; cursor: pointer;">
                    </a>
                </span>
            </p>
            `;
    }
    else {
        for (i = 0; i < datalist.options.length; i++) {
            if (areaNegocio === datalist.options[i].value) {
                if (!arrayAreaNegocio.find(item => item === areaNegocio)) {
                    existeAreaNegocio = true;
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
    }

    if (arrayAreaNegocio.length > 0) {
        htmlAreaNegocio = ``;
        for (j = 0; j < arrayAreaNegocio.length; j++) {
            htmlAreaNegocio +=
                `
                <button class="btn btn-teamwork btn-sm" onclick="handleRemoveAreaNegocio(this)" data-areanegocio="${arrayAreaNegocio[j]}" style="padding: 10px; margin-top: 5px; border-radius: 25px; color:#fff;">
                    <span>${arrayAreaNegocio[j]}</span>
                    <img src="../Resources/eliminar.svg" width="18" height="18" style="position: relative;">
                </button>
                `;
        }
    }

    document.getElementById('div__areasNegocios').innerHTML = htmlAreaNegocio;
};

handleRemoveAreaNegocio = (e) => {
    let areaNegocio = e.dataset.areanegocio;
    let htmlAreaNegocio = '';

    if (arrayAreaNegocio.find(item => item === areaNegocio)) {
        const index = arrayAreaNegocio.indexOf(areaNegocio);
        if (index > -1) {
            //QUITAR AREA NEGOCIO
            arrayAreaNegocio.splice(index, 1);
        }
    }

    if (arrayAreaNegocio.length > 0) {
        htmlAreaNegocio = ``;
        for (j = 0; j < arrayAreaNegocio.length; j++) {
            htmlAreaNegocio +=
                `
                <button class="btn btn-teamwork btn-sm" onclick="handleRemoveAreaNegocio(this)" data-areanegocio="${arrayAreaNegocio[j]}" style="padding: 10px; margin-top: 5px; border-radius: 25px; color:#fff;">
                    <span>${arrayAreaNegocio[j]}</span>
                    <img src="../Resources/eliminar.svg" width="18" height="18" style="position: relative;">
                </button>
                `;
        }
    }
    document.getElementById('div__areasNegocios').innerHTML = htmlAreaNegocio;
};

handleHideMessage = (target) => {
    document.getElementById(target).innerHTML = '';
};

//FUNCIONES AJAX
ajaxSearchAsistencia = (prefix, empresa, ficha, periodo, fechaInicioC, fechaTerminoC, areaNegocio) => {
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

                let diaFalta = 0;
                let diaLic = 0;
                let diaVac = 0;
                let diaBajaConfirmada = 0;

                if (fechaTerminoC === '') {
                    fechaTerminoC = '31-12-9999';
                }

                let fechaInicio = new Date(Number(fechaInicioC.substr(6, 4)), Number(fechaInicioC.substr(3, 2)) - 1, Number(fechaInicioC.substr(0, 2)));
                let fechaTermino = new Date(Number(fechaTerminoC.substr(6, 4)), Number(fechaTerminoC.substr(3, 2)) - 1, Number(fechaTerminoC.substr(0, 2)));
                let fechaMonth = new Date(periodo.substr(0, 4), periodo.substr(5, 2) - 1, 1);

                let markFiniquito = false;
                let markBajaConfirmada = false;
                let markPendienteKAM = false;
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

                            if (request.Asistencia[falCount].CodigoAsistencia === 'BK') {
                                if (diaBajaConfirmada > diaFalta) {
                                    document.getElementById('errorBaja').innerHTML =
                                        `
                                        <p>
                                            <span class="warning" style="border-radius: 10px; padding: 10px 20px 10px 20px; font-size: 16px;">
                                                <img src="../Resources/advertencia.png" width="25" height="25"" />
                                                  Advertencia, hay una inconsistencia entre la Baja Confirmada y la la inasistencia Baja Informada KAM
                                            </span>
                                        </p>
                                        `;
                                }
                            }

                        }
                        //OBTIENE DIA DE LICENCIA MEDICA
                        if (licCount < request.Licencia.length) {
                            diaLic = parseInt(request.Licencia[licCount].DiaLic);
                        }
                        //OBTIENE DIA DE VACACION
                        if (vacCount < request.Vacacion.length) {
                            diaVac = parseInt(request.Vacacion[vacCount].DiaVac);
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
                                    if (Number(fechaTermino.getFullYear() + '' + fechaTermino.getMonth() + '' + fechaTermino.getDate()) === Number(fechaMonth.getFullYear() + '' + fechaMonth.getMonth() + '' + fechaMonth.getDate())) {
                                        if (diaBajaConfirmada > 0 && diaBajaConfirmada === i + 1) {
                                            if (diaFalta === i + 1) {
                                                dayMonth +=
                                                    `<td class="${request.Asistencia[falCount].Class}" style="color: ${request.Asistencia[falCount].Color};">${i + 1}
                                                        <button id="upd${i + 1}" onclick="handleShowModalConfirmaEvento(this)" class="dspl-inline-block btn btn-warning rounded-circle" 
                                                                style="${periodoCerrado === 'S' ? 'display: none;' : 'float: right; box-shadow: 0px 0px 5px rgba(0, 0, 0, .5);'}"
                                                                data-year="${periodo.substr(0, 4)}" data-month="${periodo.substr(5, 2)}" data-day="${i + 1}" 
                                                                data-tipo="${request.Asistencia[falCount].CodigoAsistencia}" data-ficha="${ficha}" data-action="ActualizarV1"
                                                                data-fechainicio="${fechaInicioC}" data-fechatermino="${fechaTerminoC}">
                                                            <img src="../Resources/editar.svg" width="10" height="10" style="left: -17%;" />
                                                        </button>
                                                        </td>
                                                    `;
                                                falCount++;
                                                markCell = true;
                                            }
                                            else {
                                                if (!markCell) {
                                                    dayMonth +=
                                                        `
                                                    <td ${periodoCerrado === 'S' ? '' : 'onclick="handleChangeFalta(this);"'}
                                                            data-year="${periodo.substr(0, 4)}" data-month="${periodo.substr(5, 2)}" data-day="${i + 1}" data-tipo=""
                                                            style="${periodoCerrado === 'S' ? '' : 'cursor:pointer;'}">
                                                        ${i + 1}
                                                    </td>
                                                    `;
                                                    markCell = true;
                                                }
                                            }
                                            markBajaConfirmada = true;
                                        }
                                        else {
                                            dayMonth += `<td class="pendienteconfirmacionKAM" style="color: #fff;">${i + 1}</td>`;
                                            markPendienteKAM = true;
                                        }
                                        markCell = true;
                                    }
                                    else if (Number(fechaTermino.getFullYear() + '' + fechaTermino.getMonth() + '' + fechaTermino.getDate()) < Number(fechaMonth.getFullYear() + '' + fechaMonth.getMonth() + '' + fechaMonth.getDate()) && fechaTermino.getMonth() === fechaMonth.getMonth()) {
                                        if (markPendienteKAM) {
                                            dayMonth += `<td class="pendienteconfirmacionKAM" style="color: #fff;">${i + 1}</td>`;
                                        }
                                        else if (markFiniquito) {
                                            dayMonth += `<td class="pendiente" style="color: #fff;">${i + 1}</td>`;
                                        }
                                        else {
                                            dayMonth += `<td class="finiquitado" style="color: #fff;">${i + 1}</td>`;
                                        }
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
                                                    if (diaVac === i + 1) {
                                                        dayMonth += `<td class="${request.Vacacion[vacCount].Class}" style="color: #fff;">${i + 1}</td>`;
                                                        vacCount++;
                                                        markCell = true;
                                                    }
                                                }
                                            }
                                            //MARCA DIAS DE INASISTENCIA
                                            if (!markCell) {
                                                if (fechaTermino < fechaMonth) {
                                                    dayMonth += `<td class="finiquitado">${i + 1}</td>`;
                                                    markCell = true;
                                                }
                                                else {
                                                    if (diaFalta === i + 1) {
                                                        dayMonth +=
                                                            `<td class="${request.Asistencia[falCount].Class}" style="color: ${request.Asistencia[falCount].Color};">${i + 1}
                                                                <button id="upd${i + 1}" onclick="handleShowModalConfirmaEvento(this)" class="dspl-inline-block btn btn-warning rounded-circle" 
                                                                        style="${periodoCerrado === 'S' ? 'display: none;' : 'float: right; box-shadow: 0px 0px 5px rgba(0, 0, 0, .5);'}"
                                                                        data-year="${periodo.substr(0, 4)}" data-month="${periodo.substr(5, 2)}" data-day="${i + 1}" 
                                                                        data-tipo="${request.Asistencia[falCount].CodigoAsistencia}" data-ficha="${ficha}" data-action="ActualizarV1"
                                                                        data-fechainicio="${fechaInicioC}" data-fechatermino="${fechaTerminoC}">
                                                                    <img src="../Resources/editar.svg" width="10" height="10" style="left: -17%;" />
                                                                </button>
                                                             </td>
                                                            `;
                                                        falCount++;
                                                        markCell = true;
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
                                        if (!markBajaConfirmada) {
                                            markBajaConfirmada = true;
                                        }
                                        else {
                                            if (Number(fechaTermino.getFullYear() + '' + fechaTermino.getMonth() + '' + fechaTermino.getDate()) === Number(fechaBajaConfirmada.getFullYear() + '' + fechaBajaConfirmada.getMonth() + '' + fechaBajaConfirmada.getDate())) {
                                                dayMonth += `<td class="bajaconfirmada" style="color: #fff;">${i + 1}</td>`;
                                                markBajaConfirmada = true;
                                            }
                                            else {
                                                dayMonth += `<td class="pendiente" style="color: #fff;">${i + 1}</td>`;
                                                markFiniquito = true;
                                            }
                                        }
                                    }
                                    else {
                                        dayMonth += `<td class="pendienteconfirmacionKAM" style="color: #fff;">${i + 1}</td>`;
                                        markPendienteKAM = true;
                                    }
                                }
                                else if (Number(fechaTermino.getFullYear() + '' + fechaTermino.getMonth() + '' + fechaTermino.getDate()) < Number(fechaMonth.getFullYear() + '' + fechaMonth.getMonth() + '' + fechaMonth.getDate()) && fechaTermino.getMonth() === fechaMonth.getMonth()) {
                                    if (markPendienteKAM) {
                                        dayMonth += `<td class="pendienteconfirmacionKAM" style="color: #fff;">${i + 1}</td>`;
                                    }
                                    else if (markFiniquito) {
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
                                                <td class="${bajaConfirmadaClass}" style="color: ${bajaConfirmadaColor};">
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
                                                    markCell = true;
                                                }
                                                else {
                                                    dayMonth +=
                                                        `
                                                        <td ${periodoCerrado === 'S' ? '' : 'onclick="handleChangeFalta(this);"'}
                                                                data-year="${periodo.substr(0, 4)}" data-month="${periodo.substr(5, 2)}" data-day="${i + 1}" data-tipo=""
                                                                style="${periodoCerrado === 'S' ? '' : 'cursor:pointer;'}">
                                                            ${i + 1}
                                                        </td>
                                                        `;
                                                    markCell = true;
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

                                        markCell = true;
                                    }
                                }


                                if (fechaTermino < fechaMonth) {
                                    dayMonth += `<td class="finiquitado" style="color: #fff;">${i + 1}</td>`;
                                    markCell = true;
                                }
                                else {
                                    if (!markBajaConfirmada) {
                                        if (diaBajaConfirmada > 0 && diaBajaConfirmada === i + 1) {
                                            if (!markCell) {
                                                dayMonth +=
                                                `
                                                <td class="${bajaConfirmadaClass}" style="color: ${bajaConfirmadaColor};">
                                                    ${i + 1}
                                                </td>
                                                `;
                                                markBajaConfirmada = true;
                                            }
                                        }
                                        else {
                                            //SIN CONTRATO
                                            if (!markCell) {
                                                if (fechaInicio > fechaMonth) {
                                                    dayMonth += `<td class="sincontrato">${i + 1}</td>`;
                                                    markCell = true;
                                                }
                                            }
                                            //FINIQUITADO
                                            if (!markCell) {
                                                if (fechaTermino < fechaMonth) {
                                                    if (!markCell) {
                                                        dayMonth += `<td class="finiquitado">${i + 1}</td>`;
                                                    }
                                                    markCell = true;
                                                }
                                            }

                                            if (!markCell) {
                                                dayMonth +=
                                                    `
                                                    <td ${periodoCerrado === 'S' ? '' : 'onclick="handleChangeFalta(this);"'}
                                                            data-year="${periodo.substr(0, 4)}" data-month="${periodo.substr(5, 2)}" data-day="${i + 1}" data-tipo=""
                                                            style="${periodoCerrado === 'S' ? '' : 'cursor:pointer;'}">
                                                        ${i + 1}
                                                    </td>
                                                    `;
                                            }
                                        }
                                    }
                                    else {
                                        if (!markCell) {
                                            dayMonth +=
                                                `
                                                <td ${periodoCerrado === 'S' ? '' : 'onclick="handleChangeFalta(this);"'}
                                                        data-year="${periodo.substr(0, 4)}" data-month="${periodo.substr(5, 2)}" data-day="${i + 1}" data-tipo=""
                                                        style="${periodoCerrado === 'S' ? '' : 'cursor:pointer;'}">
                                                    ${i + 1}
                                                </td>
                                                `;
                                            markCell = true;
                                        }
                                    }
                                }
                            }
                            if (j < 6) {
                                i++;
                            }
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
                                        //markBajaConfirmada = true;
                                        if (fechaTermino < fechaMonth) {
                                            dayMonth += `<td class="finiquitado" style="color: #fff;">${i + 1}</td>`;
                                            markCell = true;
                                        }
                                        else {
                                            if (diaBajaConfirmada > 0 && diaBajaConfirmada === i + 1) {
                                                if (diaFalta === i + 1) {
                                                    dayMonth +=
                                                        `
                                                        <td class="${request.Asistencia[falCount].Class}" style="color: ${request.Asistencia[falCount].Color};">${i + 1}
                                                            <button class="dspl-inline-block btn btn-warning rounded-circle" id="upd${i + 1}"
                                                                    style="${periodoCerrado === 'S' ? 'display: none;' : 'float: right; box-shadow: 0px 0px 5px rgba(0, 0, 0, .5);'}"
                                                                    onclick="handleShowModalConfirmaEvento(this)"
                                                                    data-year="${periodo.substr(0, 4)}" data-month="${periodo.substr(5, 2)}" data-day="${i + 1}" 
                                                                    data-tipo="${!markBajaKAM ? request.Asistencia[falCount].CodigoAsistencia : 'BK'}" data-ficha="${ficha}" data-action="ActualizarV1"
                                                                    data-fechainicio="${fechaInicioC}" data-fechatermino="${fechaTerminoC}">
                                                                <img src="../Resources/editar.svg" width="10" height="10" style="left: -17%;" />
                                                            </button>
                                                        </td>
                                                        `;
                                                }
                                                else {
                                                    dayMonth +=
                                                        `
                                                        <td class="${bajaConfirmadaClass}" style="color: ${bajaConfirmadaColor};">
                                                            ${i + 1}
                                                        </td>
                                                        `;
                                                }
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
                                                                dayMonth += `<td class="${request.Vacacion[vacCount].Class}" style="color: #fff;">${i + 1}</td>`;
                                                                vacCount++;
                                                                markCell = true;
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
                                                                dayMonth +=
                                                                    `
                                                                    <td class="${request.Asistencia[falCount].Class}" style="color: ${request.Asistencia[falCount].Color};">${i + 1}
                                                                        <button class="dspl-inline-block btn btn-warning rounded-circle" id="upd${i + 1}"
                                                                                style="${periodoCerrado === 'S' ? 'display: none;' : 'float: right; box-shadow: 0px 0px 5px rgba(0, 0, 0, .5);'}"
                                                                                onclick="handleShowModalConfirmaEvento(this)"
                                                                                data-year="${periodo.substr(0, 4)}" data-month="${periodo.substr(5, 2)}" data-day="${i + 1}" 
                                                                                data-tipo="${!markBajaKAM ? request.Asistencia[falCount].CodigoAsistencia : 'BK'}" data-ficha="${ficha}" data-action="ActualizarV1"
                                                                                data-fechainicio="${fechaInicioC}" data-fechatermino="${fechaTerminoC}">
                                                                            <img src="../Resources/editar.svg" width="10" height="10" style="left: -17%;" />
                                                                        </button>
                                                                    </td>
                                                                    `;

                                                                if (!markBajaKAM) {
                                                                    if (Number(request.Asistencia[falCount].Fecha.substr(0, 2)) === (i + 1)) {
                                                                        if (request.Asistencia[falCount].CodigoAsistencia === 'BK') {
                                                                            fechaBajaKAM = new Date(Number(periodo.substr(0, 4)), Number(periodo.substr(5, 2)) - 1, Number(i + 1));
                                                                            markBajaKAM = true;
                                                                        }
                                                                    }
                                                                }
                                                                falCount++;
                                                                markCell = true;
                                                            }
                                                        }
                                                    }
                                                }
                                            }

                                            if (Number(fechaTermino.getFullYear() + '' + fechaTermino.getMonth() + '' + fechaTermino.getDate()) === Number(fechaMonth.getFullYear() + '' + fechaMonth.getMonth() + '' + fechaMonth.getDate()) && diaBajaConfirmada === 0) {
                                                markPendienteKAM = true;
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
                                                //dayMonth += `<td class="bajaconfirmada" style="color: #fff;">${i + 1}</td>`;
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

                                        if (diaFalta === i + 1) {
                                            dayMonth +=
                                                `
                                                <td class="${request.Asistencia[falCount].Class}" style="color: ${request.Asistencia[falCount].Color};">${i + 1}
                                                    <button class="dspl-inline-block btn btn-warning rounded-circle" id="upd${i + 1}"
                                                            style="${periodoCerrado === 'S' ? 'display: none;' : 'float: right; box-shadow: 0px 0px 5px rgba(0, 0, 0, .5);'}"
                                                            onclick="handleShowModalConfirmaEvento(this)"
                                                            data-year="${periodo.substr(0, 4)}" data-month="${periodo.substr(5, 2)}" data-day="${i + 1}" 
                                                            data-tipo="${!markBajaKAM ? request.Asistencia[falCount].CodigoAsistencia : 'BK'}" data-ficha="${ficha}" data-action="ActualizarV1"
                                                            data-fechainicio="${fechaInicioC}" data-fechatermino="${fechaTerminoC}">
                                                        <img src="../Resources/editar.svg" width="10" height="10" style="left: -17%;" />
                                                    </button>
                                                </td>
                                                `;
                                            falCount++;
                                            markCell = true;
                                        }
                                        else {
                                            if (!markCell) {
                                                dayMonth +=
                                                    `
                                                    <td ${periodoCerrado === 'S' ? '' : 'onclick="handleChangeFalta(this);"'}
                                                            data-year="${periodo.substr(0, 4)}" data-month="${periodo.substr(5, 2)}" data-day="${i + 1}" data-tipo=""
                                                            style="${periodoCerrado === 'S' ? '' : 'cursor:pointer;'}">
                                                        ${i + 1}
                                                    </td>
                                                    `;
                                                markCell = true;
                                            }
                                        }

                                        markCell = true;
                                    }
                                    else if (Number(fechaTermino.getFullYear() + '' + fechaTermino.getMonth() + '' + fechaTermino.getDate()) < Number(fechaMonth.getFullYear() + '' + fechaMonth.getMonth() + '' + fechaMonth.getDate()) && fechaTermino.getMonth() === fechaMonth.getMonth()) {
                                        if (markPendienteKAM) {
                                            dayMonth += `<td class="pendienteconfirmacionKAM" style="color: #fff;">${i + 1}</td>`;
                                        }
                                        else if (markFiniquito) {
                                            dayMonth += `<td class="pendiente" style="color: #fff;">${i + 1}</td>`;
                                        }
                                        else {
                                            dayMonth += `<td class="bajaconfirmada" style="color: #fff;">${i + 1}</td>`;
                                        }
                                        markCell = true;
                                    }
                                    else {
                                        if (!markBajaConfirmada) {
                                            if (diaBajaConfirmada > 0 && diaBajaConfirmada === i + 1) {
                                                dayMonth +=
                                                    `
                                                    <td ${periodoCerrado === 'S' ? '' : 'onclick="handleChangeFalta(this);"'}
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
                                                        dayMonth += `<td class="bajaconfirmada" style="color: #fff;">${i + 1}</td>`;
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
                                                            <td ${periodoCerrado === 'S' ? '' : 'onclick="handleChangeFalta(this);"'}
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
                        ajaxGetArchivosAsistencia(prefix, empresa, areaNegocio, periodo, ficha, 'Ficha');

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
                                                <td><i class="new-family-teamwork" style="font-size: 12px; font-style: normal; display: block;">${request.Personal[0].FechaTermino === '' ? 'Indefinido' : request.Personal[0].FechaTermino}</i></td>
                                            </tr>
                                        </tbody>

                                    </table>`;

                        document.getElementById('dataTrabajador').innerHTML = personalData;
                        document.querySelector('#errorPersonal').innerHTML = '';
                        document.getElementById('errorBaja').innerHTML = '';

                        setTimeout(() => {
                            ajaxSearchAsistencia(prefix, empresa, ficha, periodo, request.Personal[0].FechaInicio, request.Personal[0].FechaTermino, areaNegocio);
                            //ajaxGetLegend(prefix);
                        }, 1000);

                    }
                    else {

                        document.getElementById('dataTrabajador').innerHTML = '';
                        document.querySelector('#errorPersonal').innerHTML = `<label style="margin-top: 50px;"><span class="new-teamwork-family" style="background-color: #FFDADA; padding: 40px 10px 40px 10px; font-size: 20px; margin-top: 50px;">${request.Personal[0].Message}</span></label>`;

                        document.getElementById('calendarioAsistencia').innerHTML = '';
                        document.getElementById('dataTrabajador').innerHTML = '';
                        document.getElementById(`dataLeyenda`).innerHTML = '';
                        document.getElementById("div__listaArchivos").innerHTML = '';
                        document.getElementById('errorBaja').innerHTML = '';
                        $('#loaderSearchAsistencia').html('');
                    }
                }
            }
        };

    }
    catch{
        //
    }
};

ajaxInsertAsistencia = (prefix, empresa, ficha, periodo, asistencia) => {

    try {
        let method = 'InsertaAsistencia';
        let xhr = new XMLHttpRequest();
        let __form = new FormData();

        __form.append("empresa", empresa);
        __form.append("ficha", ficha);
        __form.append("asistencias", asistencia);

        xhr.open('POST', `${prefix}${method}`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                const request = JSON.parse(xhr.responseText);
                while (arrayAsistencia.length) {
                    arrayAsistencia.pop();
                }

                let msgAsistencia = '';
                let msgTitle = '';
                let countError = 0;
                if (request.Asistencia.length > 0) {
                    msgAsistencia = `<ol style="list-style-type:none;">`;
                    for (i = 0; i < request.Asistencia.length; i++) {
                        if (request.Asistencia[i].Code === '400') {
                            msgAsistencia += `<li><b>${request.Asistencia[i].Fecha}</b> - ${request.Asistencia[i].Message}</li>`;
                            countError++;
                        }
                    }
                    msgAsistencia += `</ol>`;

                    if (countError === 1) {
                        msgTitle = `<div class="ta-center">
                                        <img class="ta-center" src="../Resources/svg/046-error.svg" style="width:100px;" />
                                        <h5><b>La siguiente fecha no fue ingresada al sistema</b></h5>
                                    </div>`;
                    }
                    else if (countError > 1) {
                        msgTitle = `<div class="ta-center">
                                        <img src="../Resources/svg/046-error.svg" style="width:100px;" />
                                        <h5 class="ta-center"><b>Las siguientes fechas no fueron ingresadas al sistema</b></h5>
                                    </div>`;
                    }
                    else {
                        msgTitle = `<div class="ta-center">
                                        <img src="../Resources/svg/044-success.svg" style="width:100px;" />
                                        <h5 class="ta-center">Asistencia ingresada con éxito</h5>
                                    </div>`;
                    }

                    msgAsistencia += `<br><br>
                                      <div class="ta-center">
                                          <button class="btn btn-success" onclick="handleModalAcept()"
                                                  style="border-radius: 25px; height: 40px; text-align: left; color: #fff; font-weight:100;">
                                              Aceptar
                                          </button>
                                      </div>`;

                    document.querySelector('#divMessageAsistencia').innerHTML = msgTitle + msgAsistencia;
                    $("#modalMsjAsistencia").modal("show");
                }
                else {
                    msgTitle = `<div class="ta-center">
                                    <img src="../Resources/svg/046-error.svg" style="width:100px;" />
                                    <h5 class="ta-center">No fue posible ingresar asistencia, intentelo nuevamente.</h5>
                                </div>`;

                    msgAsistencia += `<br><br>
                                      <div class="ta-center">
                                          <button class="btn btn-success" onclick="handleModalAcept()"
                                                  style="border-radius: 25px; height: 40px; text-align: left; color: #fff; font-weight:100;">
                                              Aceptar
                                          </button>
                                      </div>`;

                    document.querySelector('#divMessageAsistencia').innerHTML = msgTitle + msgAsistencia;
                    $("#modalMsjAsistencia").modal("show");
                }
            }
        };
    }
    catch {
        //
    }
};

ajaxUpdateAsistencia = (prefix, empresa, ficha, fecha, periodo, tipo, fechaInicio, fechaTermino) => {
    try {
        let method = 'ActualizaAsistencia';
        let xhr = new XMLHttpRequest();
        let __form = new FormData();

        __form.append("empresa", empresa);
        __form.append("ficha", ficha);
        __form.append("fecha", fecha);
        __form.append("tipo", tipo);

        xhr.open('POST', `${prefix}${method}`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                const request = JSON.parse(xhr.responseText);

                if (request.Asistencia[0].Code === "200") {
                    document.getElementById("div__alert__message").style.display = "block";
                    document.getElementById("alert__modalSuccess").classList.add("show");
                    document.getElementById("alert__modalSuccess").innerHTML = request.Asistencia[0].Message;

                    
                    document.getElementById("div__alert__message").style.display = "none";
                    document.getElementById("alert__modalSuccess").classList.remove("show");
                    document.getElementById("alert__modalSuccess").innerHTML = "";

                    $("#modalEditAsistencia").modal("hide");

                    ajaxViewPartialLoaderTransaccion(prefix, "#loaderSearchAsistencia", empresa, ficha, periodo);
                    
                }
                else {
                    document.getElementById("div__alert__message").style.display = "block";
                    document.getElementById("alert__modalError").classList.add("show");
                    document.getElementById("alert__modalError").innerHTML = request.Asistencia[0].Message;

                    
                    document.getElementById("div__alert__message").style.display = "none";
                    document.getElementById("alert__modalError").classList.remove("show");
                    document.getElementById("alert__modalError").innerHTML = "";
                    
                }
            }
        };
    }
    catch{
        //
    }
};

ajaxDeleteAsistencia = (prefix, empresa, ficha, fecha, periodo, tipo, fechaInicio, fechaTermino) => {
    try {
        let method = 'EliminaAsistencia';
        let xhr = new XMLHttpRequest();
        let __form = new FormData();

        __form.append("empresa", empresa);
        __form.append("ficha", ficha);
        __form.append("fecha", fecha);
        __form.append("tipo", tipo);

        xhr.open('POST', `${prefix}${method}`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                const request = JSON.parse(xhr.responseText);

                if (request.Asistencia[0].Code === "200") {
                    document.getElementById("div__alert__message").style.display = "block";
                    document.getElementById("alert__modalSuccess").classList.add("show");
                    document.getElementById("alert__modalSuccess").innerHTML = request.Asistencia[0].Message;

                        document.getElementById("div__alert__message").style.display = "none";
                        document.getElementById("alert__modalSuccess").classList.remove("show");
                        document.getElementById("alert__modalSuccess").innerHTML = "";

                        $("#modalEditAsistencia").modal("hide");

                        ajaxViewPartialLoaderTransaccion(prefix, "#loaderSearchAsistencia", empresa, ficha, periodo);
                }
                else {
                    document.getElementById("div__alert__message").style.display = "block";
                    document.getElementById("alert__modalError").classList.add("show");
                    document.getElementById("alert__modalError").innerHTML = request.Asistencia[0].Message;

                        document.getElementById("div__alert__message").style.display = "none";
                        document.getElementById("alert__modalError").classList.remove("show");
                        document.getElementById("alert__modalError").innerHTML = "";
                }
            }
        };
    }
    catch{
        //
    }
};

ajaxGetTipoAsistencia = (prefix, target, tipo, year, month, day, action) => {
    try {
        let method = 'GetTipoAsistencia';
        let xhr = new XMLHttpRequest();
        let __form = new FormData();

        xhr.open('POST', `${prefix}${method}`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                const request = JSON.parse(xhr.responseText);

                let div = document.createElement('div');
                let tipoUpdAsistencia = '';

                tipoUpdAsistencia = `<span style="z-indez: 999;
                                                  width: 200px; 
                                                  display: block; 
                                                  border: 1px solid rgb(200, 200, 200); 
                                                  position: absolute; 
                                                  margin-top: 15px; 
                                                  min-height: ${ action === "upd" ? '250px' : '150px'}; 
                                                  background: #fff;
                                                  box-shadow: 0px 0px 5px rgba(0, 0, 0, .3);">
                                        <span style="margin-bottom: 5px;"><b>Confirmar ${ action === "upd" ? 'cambio' : 'eliminación'} asistencia</b></span>
                                  <br><br>`;

                if (action === "upd") {
                    for (i = 0; i < request.TipoAsistencia.length; i++) {
                        tipoUpdAsistencia += `<p>
                                                 <label class="switch">
                                                    <input type="radio" class="change__options__tipo__asistencia" id="tipo${request.TipoAsistencia[i].Nombre}" name="tipoUpdAsistencia" value="${request.TipoAsistencia[i].CodigoAsistencia}" ${request.TipoAsistencia[i].CodigoAsistencia === tipo ? 'checked' : ''} style="display: inline-block; font-weight:100;">
                                                       <span class="slider round"></span>
                                                   </label>
                                                 <span class="new-new-family-teamwork" style="font-weight:100; margin-right: 5px;">${request.TipoAsistencia[i].Nombre}</span>
                                              </p>`;
                    }
                }


                tipoUpdAsistencia += `<button onclick="handleUpdAsistencia(this)" class="btn ${action === "upd" ? 'btn-teamwork' : 'btn-danger'}" style="border-radius: 25px; height: 40px; text-align: left; color: #fff; font-weight:100;"
                                           style="float: right; box-shadow: 0px 0px 5px rgba(0, 0, 0, .5);" data-year="${year}" 
                                                  data-month="${month}" data-day="${day}" data-tipo="${tipo}" data-action="${action}">
                                        Confirmar
                                   </button>
                               </span>`;


                document.querySelector(`.cambio__edit__asistencia`).innerHTML = tipoUpdAsistencia;
            }
        };
    }
    catch{
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
            ajaxSearchDataPersonal(prefix, empresa, ficha, periodo);
        }
    });
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
                    mitadLegend = Math.round(request.Leyenda.length / 2);

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

//V2

handleGetAreaNegocioAsistencia = (empresa, areaNegocio, target) => {
    let domain = handleGetDomain();
    let area = '';

    if (empresa !== null && empresa !== "" && empresa !== undefined) {
        if (empresa !== '0') {
            ajaxGetAreaNegocioAsistencia(domain, empresa, areaNegocio, target);
        }
        else {
            document.getElementById(target).innerHTML = '';
            area = `<option value="0" data-input="input">Seleccione Area de Negocio</option>`;
            document.getElementById(target).innerHTML = area;
        }
    }
    else {
        document.getElementById(target).innerHTML = '';
        area = `<option value="0" data-input="input">Seleccione Area de Negocio</option>`;
        document.getElementById(target).innerHTML = area;
    }
};

handleGetAreaNegocio = (empresa, areaNegocio, target) => {
    let domain = handleGetDomain();
    let area = '';

    if (empresa !== null && empresa !== "" && empresa !== undefined) {
        if (empresa !== '0') {
            ajaxGetAreaNegocio(domain, empresa, areaNegocio, target);
        }
        else {
            document.getElementById(target).innerHTML = '';
            //area = `<option value="0" data-input="input">Seleccione Area de Negocio</option>`;
            //document.getElementById(target).innerHTML = area;
        }
    }
    else {
        document.getElementById(target).innerHTML = '';
        //area = `<option value="0" data-input="input">Seleccione Area de Negocio</option>`;
        //document.getElementById(target).innerHTML = area;
    }

    
};

handleChangeEmpresa = () => {
    let empresa = document.getElementById('empresa').value;

    while (arrayAreaNegocio.length) {
        arrayAreaNegocio.pop();
    }

    handleGetAreaNegocio(empresa, '', 'select__areaNegocio');
};

handleChangeEmpresaPeriodo = () => {
    let empresa = document.getElementById('empresa').value;

    while (arrayAreaNegocio.length) {
        arrayAreaNegocio.pop();
    }

    handleGetAreaNegocioAsistencia(empresa, '', 'areaNegocio');
};

handleSearchAsistenciaV2 = (e) => {
    let empresa = document.getElementById('empresa').value;
    let areaNegocio = document.getElementById('areaNegocio').value;
    let periodo = document.getElementById('periodo').value;
    let domain = handleGetDomain();

    if (e.id === 'empresa') {
        handleGetAreaNegocioAsistencia(empresa, areaNegocio, 'areaNegocio');
    }

    if (!handleValidateReportDataV2(empresa, periodo)) {
        document.getElementById('reportAsist').style.display = 'none';
    }
    else {
        //document.getElementById('reportAsist').style.display = 'inline-block';
    }

    if (handleValidateDataV2(empresa, areaNegocio, periodo)) {
        while (arrayAsistencia.length) {
            arrayAsistencia.pop();
        }

        ajaxViewPartialLoaderTransaccionV2(domain, "#loaderSearchAsistenciaV2", empresa, '', areaNegocio === '0' ? '' : areaNegocio, periodo, '1-15');
    }
    else {
        document.getElementById('calendarioAsistencia').innerHTML = '';
    }
};

handleValidateDataV2 = (empresa, areaNegocio, periodo) => {
    let validate = true;

    if (empresa === "" || empresa === null || empresa === undefined || empresa === '0') {
        validate = false;
    }

    if (areaNegocio === "" || areaNegocio === null || areaNegocio === undefined || areaNegocio === '0') {
        validate = false;
    }

    if (periodo === "" || periodo === null || periodo === undefined) {
        validate = false;
    }
    return validate;
};

handleValidateReportDataV2 = (empresa, periodo) => {
    let validate = true;

    if (empresa === "" || empresa === null || empresa === undefined || empresa === '0') {
        validate = false;
    }

    if (periodo === "" || periodo === null || periodo === undefined) {
        validate = false;
    }
    return validate;
};

handleChangeFaltaV2 = (e) => {
    let tipoAsistencia = document.querySelector('input[name="tipoAsistencia"]:checked').value;
    let clase = document.querySelector('input[name="tipoAsistencia"]:checked').dataset.clase;
    let color = document.querySelector('input[name="tipoAsistencia"]:checked').dataset.color;
    let fechaAsistencia = e.dataset.year + e.dataset.month + (e.dataset.day < 10 ? '0' + e.dataset.day : e.dataset.day);
    let tipo = e.dataset.tipo;

    if (!arrayAsistencia.find(item => item === fechaAsistencia + ('-' + tipoAsistencia) + ('_' + e.dataset.ficha))) {
        //AGREGA NUEVA INASISTENCIA
        arrayAsistencia.push(fechaAsistencia + ('-' + tipoAsistencia) + ('_' + e.dataset.ficha));
        //QUITA INASISTENCIA ANTERIOR
        arrayAsistencia = arrayAsistencia.filter(item => !(fechaAsistencia + '-' + tipo + ('_' + e.dataset.ficha)).includes(item));
        e.dataset.tipo = tipoAsistencia;

        e.classList.value = '';
        e.classList.add(clase);
        e.style.color = color;
    }
    else {
        //DESMARCA INASISTENCIA
        arrayAsistencia = arrayAsistencia.filter(item => !(fechaAsistencia + '-' + tipoAsistencia + ('_' + e.dataset.ficha)).includes(item));
        e.dataset.tipo = '';
        e.classList.remove(clase);
        e.style.color = '';
    }

    if (arrayAsistencia.length > 0) {
        document.getElementById('btnSetAsistencia').style.display = 'inline-block';
    }
    else {
        document.getElementById('btnSetAsistencia').style.display = 'none';
    }
};

handleShowModalConfirmaEvento = (e) => {
    let domain = handleGetDomain();
    let action = e.dataset.action;
    let fecha = '';
    let ficha = '';
    let nombre = '';
    let rut = '';
    let tipo = '';
    let areaNegocio = '';
    let pagination = '';
    let htmlConfirmaEvento = '';

    switch (action) {
        case "Actualizar":
            fecha = (Number(e.dataset.day) < 10 ? '0' + e.dataset.day : e.dataset.day) + '-' + e.dataset.month + '-' + e.dataset.year;
            ficha = e.dataset.ficha;
            nombre = e.dataset.nombre;
            rut = e.dataset.rut;
            tipo = e.dataset.asistencia;
            areaNegocio = e.dataset.areanegocio;
            pagination = e.dataset.pagination;

            htmlConfirmaEvento += 
                `
                <div id="div__updAsistencia" class="new-teamwork-family" style="display:block; font-weight: 100;">
                    <h2>${tipo === 'BK' ? 'Eliminación de Baja Informada por KAM' : 'Actualización de Insistencia'}</h3>
            
                    <div id="div__tipoAsistencia" style="display: inline-block; margin-bottom: 10px; font-weight: 100;"></div>
            
                    <div style="text-align: left; margin-left: 60px; margin-bottom: 20px;font-weight: 100;">
                        <h3>Nombre: ${nombre}</h3>
                        <h3>Ficha: ${ficha}</h3>
                        <h3>Rut: ${rut}</h3>
                        <h3>Fecha: ${fecha}</h3>
                    </div>
                </div>
        
                <div id="div__action__updAsistencia" class="new-teamwork-family" style="margin-top: 10px; margin-bottom: 20px; font-weight: 100;">
                    <button class="btn btn-warning new-teamwork-family" style="color: #fff; border-radius: 25px; margin-right: 10px; font-weight: 100; font-size: 18px; ${tipo === 'BK' ? 'display: none;' : ''}"
                            onclick="handleConfirmaEvento(this)"
                            data-fecha="${fecha}" data-ficha="${ficha}" data-tipo="${tipo}" data-action="Actualizar" data-areanegocio="${areaNegocio}" data-pagination="${pagination}">
                        Actualizar
                    </button>

                    <button class="btn btn-danger new-teamwork-family" style="color: #fff; border-radius: 25px; margin-right: 10px; font-weight: 100; font-size: 18px;"
                            onclick="handleConfirmaEvento(this)"
                            data-fecha="${fecha}" data-ficha="${ficha}" data-tipo="${tipo}" data-action="Eliminar" data-areanegocio="${areaNegocio}" data-pagination="${pagination}">
                        Eliminar
                    </button>

                    <button class="btn btn-anulado new-teamwork-family" style="color: #fff; border-radius: 25px; margin-right: 10px; font-weight: 100; font-size: 18px;"
                            data-dismiss="modal">
                        Cancelar
                    </button>
                </div>
         

                <div id="div__confirmaEvento" class="new-teamwork-family" style="display:none; margin-top: 10px; margin-bottom: 20px; font-weight: 100; border-top: 1px solid;"></div>

                <div id="div__alert__message" style="display: none; margin-top: 10px; margin-bottom: 20px;">
                    <span id="alert__modalSuccess" class="alert alert-success fade" role="alert" style="font-size: 20px;"></span>
                    <span id="alert__modalError" class="alert alert-danger fade" role="alert" style="font-size: 20px;"></span>
                </div>
                
                `;

            document.getElementById("div__modalCambioAsistencia").innerHTML = htmlConfirmaEvento;

            if (tipo !== 'BK') {
                ajaxGetTipoAsistenciaV2(domain, "div__tipoAsistencia", tipo, 'Actualizar');
            }

            $("#modalEditAsistencia").modal("show");
            break;
        case "ActualizarV1":
            let fechaInicio = e.dataset.fechainicio;
            let fechaTermino = e.dataset.fechatermino;
            fecha = (Number(e.dataset.day) < 10 ? '0' + e.dataset.day : e.dataset.day) + '-' + e.dataset.month + '-' + e.dataset.year;
            ficha = e.dataset.ficha;
            nombre = e.dataset.nombre;
            rut = e.dataset.rut;
            tipo = e.dataset.tipo;
            

            htmlConfirmaEvento +=
                `
                <div id="div__updAsistencia" class="new-teamwork-family" style="display:block; font-weight: 100;">
                    <h2>${tipo === 'BK' ? 'Eliminación de Baja Informada por KAM' : 'Actualización de Insistencia'}</h3>
            
                    <div id="div__tipoAsistencia" style="display: inline-block; margin-bottom: 10px; font-weight: 100;"></div>
            
                    <div style="text-align: left; margin-left: 60px; margin-bottom: 20px;font-weight: 100;">
                        <h3>Ficha: ${ficha}</h3>    
                        <h3>Fecha: ${fecha}</h3>
                    </div>
                </div>
        
                <div id="div__action__updAsistencia" class="new-teamwork-family" style="margin-top: 10px; margin-bottom: 20px; font-weight: 100;">
                    <button class="btn btn-warning new-teamwork-family" style="color: #fff; border-radius: 25px; margin-right: 10px; font-weight: 100; font-size: 18px; ${tipo === 'BK' ? 'display: none;' : ''}"
                            onclick="handleConfirmaEvento(this)"
                            data-fecha="${fecha}" data-ficha="${ficha}" data-tipo="${tipo}" data-action="ActualizarV1" data-fechainicio="${fechaInicio}" data-fechatermino="${fechaTermino}">
                        Actualizar
                    </button>

                    <button class="btn btn-danger new-teamwork-family" style="color: #fff; border-radius: 25px; margin-right: 10px; font-weight: 100; font-size: 18px;"
                            onclick="handleConfirmaEvento(this)"
                            data-fecha="${fecha}" data-ficha="${ficha}" data-tipo="${tipo}" data-action="EliminarV1" data-fechainicio="${fechaInicio}" data-fechatermino="${fechaTermino}">
                        Eliminar
                    </button>

                    <button class="btn btn-anulado new-teamwork-family" style="color: #fff; border-radius: 25px; margin-right: 10px; font-weight: 100; font-size: 18px;"
                            data-dismiss="modal">
                        Cancelar
                    </button>
                </div>
         

                <div id="div__confirmaEvento" class="new-teamwork-family" style="display:none; margin-top: 10px; margin-bottom: 20px; font-weight: 100; border-top: 1px solid;"></div>

                <div id="div__alert__message" style="display: none; margin-top: 10px; margin-bottom: 20px;">
                    <span id="alert__modalSuccess" class="alert alert-success fade" role="alert" style="font-size: 20px;"></span>
                    <span id="alert__modalError" class="alert alert-danger fade" role="alert" style="font-size: 20px;"></span>
                </div>
                
                `;

            document.getElementById("div__modalCambioAsistencia").innerHTML = htmlConfirmaEvento;

            if (tipo !== 'BK') {
                ajaxGetTipoAsistenciaV2(domain, "div__tipoAsistencia", tipo, 'Actualizar');
            }

            $("#modalEditAsistencia").modal("show");
            break;
    }
};

handleUpdAsistenciaV2 = (e) => {
    let domain = handleGetDomain();
    let fecha = e.dataset.fecha;
    let ficha = e.dataset.ficha;
    let areaNegocio = e.dataset.areanegocio;
    let pagination = e.dataset.pagination;
    let empresa = document.getElementById('empresa').value;
    let periodo = document.getElementById('periodo').value;
    let tipo = document.querySelector('input[name="tipoUpdAsistencia"]:checked') !== null ? document.querySelector('input[name="tipoUpdAsistencia"]:checked').value : 'BK';

    e.dataset.action === 'Actualizar' ?
        ajaxUpdateAsistenciaV2(domain, empresa, ficha, fecha, periodo, tipo, areaNegocio, pagination) :
        ajaxDeleteAsistenciaV2(domain, empresa, ficha, fecha, periodo, tipo, areaNegocio, pagination);
};

handleConfirmaEvento = (e) => {
    let action = e.dataset.action;
    let fecha = '';
    let ficha = '';
    let tipo = '';
    let areaNegocio = '';
    let pagination = '';
    let empresa = '';
    let htmlConfirmaEvento = '';
    let htmlPregunta = '';
    let htmlData = '';
    let htmlEvento = '';
    let fechaInicio = '';
    let fechaTermino = '';

    switch (action) {
        case "Actualizar":
            fecha = e.dataset.fecha;
            ficha = e.dataset.ficha;
            tipo = e.dataset.asistencia;
            areaNegocio = e.dataset.areanegocio;
            pagination = e.dataset.pagination;
            empresa = document.getElementById('empresa').value;
            htmlPregunta = '¿Está seguro de querer actualizar inasistencia?';
            htmlEvento = 'handleUpdAsistenciaV2(this)';
            htmlData = `data-empresa="${empresa}" data-ficha="${ficha}" data-fecha="${fecha}" data-tipo="${tipo}" data-action="${action}" data-areanegocio="${areaNegocio}" data-pagination="${pagination}"`;

            document.getElementById("div__action__updAsistencia").style.display = "none";
            break;

        case "Eliminar":
            fecha = e.dataset.fecha;
            ficha = e.dataset.ficha;
            tipo = e.dataset.asistencia;
            areaNegocio = e.dataset.areanegocio;
            pagination = e.dataset.pagination;
            empresa = document.getElementById('empresa').value;
            htmlPregunta = '¿Está seguro de querer eliminar inasistencia?';
            htmlEvento = 'handleUpdAsistenciaV2(this)';
            htmlData = `data-empresa="${empresa}" data-ficha="${ficha}" data-fecha="${fecha}" data-tipo="${tipo}" data-action="${action}" data-areanegocio="${areaNegocio}" data-pagination="${pagination}"`;

            document.getElementById("div__action__updAsistencia").style.display = "none";
            break;

        case "CierrePeriodo":
            fecha = e.dataset.fecha;
            areaNegocio = e.dataset.areanegocio;
            empresa = e.dataset.empresa;
            htmlPregunta = '¿Está seguro de querer cerrar este periodo?';
            htmlEvento = 'handleCerrarPeriodo(this)';
            htmlData = `data-empresa="${empresa}" data-fecha="${fecha}" data-areanegocio="${areaNegocio}"`;
            let fechaP = new Date(Number(fecha.substr(6, 4)), Number(fecha.substr(3, 2)) - 1, Number(fecha.substr(0, 2)));

            htmlConfirmaEvento =
                `
                 <div id="div__updAsistencia" class="new-teamwork-family" style="display:block; font-weight: 100;">
                    <h2>Confirmación Cierre Periodo ${meses[fechaP.getMonth()]} de ${fechaP.getFullYear()}</h3>
                </div>

                <div id="div__confirmaEvento" class="new-teamwork-family" style="display:none; margin-top: 10px; margin-bottom: 20px; font-weight: 100; border-top: 1px solid;"></div>

                <div id="div__alert__message" style="display: none; margin-top: 10px; margin-bottom: 20px;">
                    <span id="alert__modalSuccess" class="alert alert-success fade" role="alert" style="font-size: 20px;"></span>
                    <span id="alert__modalError" class="alert alert-danger fade" role="alert" style="font-size: 20px;"></span>
                </div>
                `;

            document.getElementById("div__modalCambioAsistencia").innerHTML = htmlConfirmaEvento;
            break;

        case "ExcepcionPeriodo":
            let exception = e.dataset.exception;
            fecha = e.dataset.fecha;
            areaNegocio = e.dataset.areanegocio;
            empresa = e.dataset.empresa;
            htmlPregunta = `¿Está seguro de querer ${exception === 'S' ? 'quitar' : 'agregar'} una excepción para este periodo?`;
            htmlEvento = 'handleExcepcionPeriodo(this)';
            htmlData = `data-empresa="${empresa}" data-fecha="${fecha}" data-areanegocio="${areaNegocio}" data-excepcion="${exception === 'S' ? 'N' : 'S'}"`;
            let fechaE = new Date(Number(fecha.substr(6, 4)), Number(fecha.substr(3, 2)) - 1, Number(fecha.substr(0, 2)));

            htmlConfirmaEvento =
                `
                 <div id="div__updAsistencia" class="new-teamwork-family" style="display:block; font-weight: 100;">
                    <h2>Confirmación Cierre Periodo ${meses[fechaE.getMonth()]} de ${fechaE.getFullYear()}</h3>
                </div>

                <div id="div__confirmaEvento" class="new-teamwork-family" style="display:none; margin-top: 10px; margin-bottom: 20px; font-weight: 100; border-top: 1px solid;"></div>

                <div id="div__alert__message" style="display: none; margin-top: 10px; margin-bottom: 20px;">
                    <span id="alert__modalSuccess" class="alert alert-success fade" role="alert" style="font-size: 20px;"></span>
                    <span id="alert__modalError" class="alert alert-danger fade" role="alert" style="font-size: 20px;"></span>
                </div>
                `;
            document.getElementById("div__modalCambioAsistencia").innerHTML = htmlConfirmaEvento;
            break;

        case "ActualizarV1":
            fechaInicio = e.dataset.fechainicio;
             fechaTermino = e.dataset.fechatermino;
            fecha = e.dataset.fecha;
            ficha = e.dataset.ficha;
            tipo = e.dataset.tipo;
            empresa = document.getElementById('empresa').value;
            htmlPregunta = '¿Está seguro de querer actualizar inasistencia?';
            htmlEvento = 'handleUpdAsistencia(this)';
            htmlData = `data-empresa="${empresa}" data-ficha="${ficha}" data-fecha="${fecha}" data-tipo="${tipo}" data-action="${action}" data-fechainicio="${fechaInicio}" data-fechatermino="${fechaTermino}"`;

            document.getElementById("div__action__updAsistencia").style.display = "none";
            break;

        case "EliminarV1":
            fechaInicio = e.dataset.fechainicio;
            fechaTermino = e.dataset.fechatermino;
            fecha = e.dataset.fecha;
            ficha = e.dataset.ficha;
            tipo = e.dataset.tipo;
            empresa = document.getElementById('empresa').value;
            htmlPregunta = '¿Está seguro de querer eliminar inasistencia?';
            htmlEvento = 'handleUpdAsistencia(this)';
            htmlData = `data-empresa="${empresa}" data-ficha="${ficha}" data-fecha="${fecha}" data-tipo="${tipo}" data-action="${action}" data-fechainicio="${fechaInicio}" data-fechatermino="${fechaTermino}"`;

            document.getElementById("div__action__updAsistencia").style.display = "none";
            break;

    }

    htmlConfirmaEvento = `
            <div style="text-align: center; margin-top: 20px; margin-bottom: 20px;">
                <div style="text-align: center;  margin-bottom: 20px;">
                    <b>
                        <span class="new-font-family" style="font-size: 20px;">${htmlPregunta}</span>
                    </b>
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


    $("#modalEditAsistencia").modal("show");
    document.getElementById("div__confirmaEvento").innerHTML = htmlConfirmaEvento;
    document.getElementById("div__confirmaEvento").style.display = "block";
};

handleCancelEvento = (e) => {
    let action = e.dataset.action;

    switch (action) {
        case "Actualizar":
            document.getElementById("div__action__updAsistencia").style.display = "block";
            document.getElementById("div__confirmaEvento").style.display = "none";
            document.getElementById("div__confirmaEvento").innerHTML = "";
            break; 

        case "CierrePeriodo":
            document.getElementById("div__confirmaEvento").style.display = "none";
            document.getElementById("div__confirmaEvento").innerHTML = "";
            break;
    }

    $("#modalEditAsistencia").modal("hide");
};

handleDelAsistenciaV2 = (e) => {
    let domain = handleGetDomain();
    let empresa = e.dataset.empresa;
    let ficha = e.dataset.ficha;
    let fecha = e.dataset.fecha;
    let tipo = e.dataset.tipo;
    let periodo = document.getElementById('periodo').value;

    ajaxDeleteAsistencia(domain, empresa, ficha, fecha, periodo, tipo);
};

handleSetAsistenciaV2 = () => {
    let empresa = document.getElementById('empresa').value;
    let periodo = document.getElementById('periodo').value;
    let domain = handleGetDomain();
    let dayAsistencia = '';

    for (i = 0; i < arrayAsistencia.length; i++) {
        dayAsistencia += arrayAsistencia[i] + ';';
    }

    ajaxInsertAsistenciaV2(domain, empresa, '', periodo, dayAsistencia);
};

handleChangeMonthV2 = (accion) => {
    let periodo = document.getElementById('periodo').value;
    let date = new Date(periodo.substr(0, 4), periodo.substr(5, 2), '01');
    let month = date.getMonth();
    let year = date.getFullYear();

    while (arrayAsistencia.length) {
        arrayAsistencia.pop();
    }

    switch (accion) {
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
    let areaNegocio = document.getElementById('areaNegocio').value;
    periodo = document.getElementById('periodo').value;

    ajaxViewPartialLoaderTransaccionV2(domain, "#loaderSearchAsistenciaV2", empresa, '', areaNegocio === '0' ? '' : areaNegocio, periodo, '1-15');
};

handleModalAceptV2 = () => {
    let empresa = document.getElementById('empresa').value;
    let areaNegocio = document.getElementById('areaNegocio').value;
    let periodo = document.getElementById('periodo').value;
    let domain = handleGetDomain();

    ajaxViewPartialLoaderTransaccionV2(domain, "#loaderSearchAsistenciaV2", empresa, '', areaNegocio === '0' ? '' : areaNegocio, periodo, '1-15');
    $("#modalMsjAsistencia").modal("hide");
    document.querySelector('#divMessageAsistencia').innerHTML = '';
};


handleAsistenciaReportV2 = () => {
    let domain = handleGetDomain();

    let empresa = document.getElementById('empresa').value;
    let periodo = document.getElementById('periodo').value;
    //let selectElement = document.getElementById('areaNegocio').selectedOptions;
    let msjError = '';
    let error = false;
    //let values = Array.from(selectElement).map(({ value }) => value);
    let areaNegocio = '';

    if (!error) {
        if (empresa === "" || empresa === null || empresa === undefined || empresa === '0') {
            error = true;
            msjError = '¡Favor seleccione una empresa!';
        }
    }

    if (!error) {
        if (periodo === "" || periodo === null || periodo === undefined) {
            error = true;
            msjError = '¡Favor seleccione un periodo!';
        }
    }

    if (error) {
        document.getElementById("div__errorReporte").innerHTML =
            `
            <p>
                <span style="background-color: #fe5858; border-radius: 10px; padding: 10px 20px 10px 20px; color: #fff;">
                   ${msjError}
                    <a class="btn-link" onclick="handleHideMessage('div__errorReporte')">
                        <img src="/Resources/delete.png" width="15" height="15" style="margin-bottom: 5px; margin-left: 10px; cursor: pointer;">
                    </a>
                </span>
            </p>
            `;
    }
    else {
        for (i = 0; i < arrayAreaNegocio.length; i++) {
            if (i === arrayAreaNegocio.length - 1) {
                areaNegocio += arrayAreaNegocio[i];
            }
            else {
                areaNegocio += arrayAreaNegocio[i] + ';';
            }
            
        }
        //window.location.href = `${domain}GenerateExcel?excel=YXNpc3RlbmNpYQ==&empresa=${empresa}&periodo=${periodo}-01&areanegocio=${areaNegocio === '0' ? '' : areaNegocio}`;
        window.open(`${domain}GenerateExcel?excel=YXNpc3RlbmNpYQ==&empresa=${empresa}&periodo=${periodo}-01&areanegocio=${areaNegocio === '0' ? '' : areaNegocio}`);
    }
};

handlePagination = (e) => {
    let domain = handleGetDomain();
    let empresa = e.dataset.empresa;
    let ficha = e.dataset.ficha;
    let periodo = e.dataset.fecha.substr(0, 4) + '-' + e.dataset.fecha.substr(5, 2);
    let areaNegocio = e.dataset.areanegocio;
    let pagination = e.dataset.pagination;

    ajaxViewPartialLoaderTransaccionV2(domain, "#loaderSearchAsistenciaV2", empresa, ficha, areaNegocio,  periodo, pagination);
};

handleCerrarPeriodo = (e) => {
    let domain = handleGetDomain();
    let empresa = e.dataset.empresa;
    let fecha = e.dataset.fecha;
    let areaNegocio = e.dataset.areanegocio;

    ajaxSetCierrePeriodo(domain, empresa, fecha, areaNegocio);
};


handleChangeEmpresaCarga = () => {
    let empresa = document.getElementById('select__empresaCarga').value;

    while (arrayAreaNegocio.length) {
        arrayAreaNegocio.pop();
    }

    document.getElementById('div__areasNegocios').innerHTML = '';

    handleGetAreaNegocio(empresa, '', 'select__areaNegocioCarga');
    handleChangeFilterCarga();
};


handleChangeFilterCarga = () => {
    let domain = handleGetDomain();
    let empresa = document.getElementById('select__empresaCarga').value;
    let periodo = document.getElementById('input__periodoCarga').value;
    let areaNegocio = '';
    let button = document.getElementById("button__downloadCargaMasiva");
    let fecha = new Date(Number(periodo.substr(0, 4)), Number(periodo.substr(5, 2)) - 1, 1);
    let fechaFin = new Date(Number(periodo.substr(0, 4)), Number(periodo.substr(5, 2)), 0);

    document.getElementById('fileUpload').dataset.fecha = periodo !== null && periodo !== undefined ? periodo : '';

    for (i = 0; i < arrayAreaNegocio.length; i++) {
        if (i === arrayAreaNegocio.length - 1) {
            areaNegocio += arrayAreaNegocio[i];
        }
        else {
            areaNegocio += arrayAreaNegocio[i] + ';';
        }
    }

    while (diasCargaMasiva.length) {
        diasCargaMasiva.pop();
    }

    while (arrayBonos.length) {
        arrayBonos.pop();
    }
    
    for (j = 0; j < fechaFin.getDate(); j++) {
        diasCargaMasiva.push(j + 1);
    }

    button.href = button.href.split('&')[0] + `&empresa=${empresa === '' ? '' : empresa}&areanegocio=${areaNegocio}&fecha=${periodo === '' ? '' : periodo + '-01'}`;

    ajaxObtenerBonosCliente(domain, empresa, areaNegocio, 'Array', '');
    console.log(arrayBonos);
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

handleRemoveAreaNegocioCarga = (e) => {
    let areaNegocio = e.dataset.areanegocio;
    let htmlAreaNegocio = '';

    if (arrayAreaNegocio.find(item => item === areaNegocio)) {
        const index = arrayAreaNegocio.indexOf(areaNegocio);
        if (index > -1) {
            arrayAreaNegocio.splice(index, 1);
        }
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

handleEliminarDiacriticos = (str) => {
    return str.normalize('NFD').replace(/[\u0300-\u036f]/g, "");
};


//FUNCIONES AJAX V2
ajaxGetAreaNegocio = (prefix, empresa, areaNegocio, target) => {
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
                //let area = `<option value="0" data-input="input">Seleccione Area de Negocio</option>`;
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


ajaxGetAreaNegocioAsistencia = (prefix, empresa, areaNegocio, target) => {
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
                document.getElementById(target).innerHTML = area;
            }
        };
    }
    catch{
        //
    }
};

ajaxViewPartialLoaderTransaccionV2 = (prefix, htmlElement, empresa, ficha, areaNegocio, periodo, pagination) => {
    $.ajax({
        type: 'POST',
        url: prefix + 'ViewPartialLoaderTransaccion',
        data: '{ }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        error: function (xhr) {
            $(htmlElement).html(xhr.responseText);

            let firstDay = new Date(periodo.substr(0, 4), periodo.substr(5, 2) - 1, 1);
            let month = firstDay.getMonth();

            let cambioMonth =
                `
                    <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                        <div class="row">
                            <div class="col-12 col-sm-12 col-md-12 col-lg-8">
                                <div class="row">
                                    <button class="btn btn-warning new-family-teamwork" 
                                            style="border-radius: 25px; height: 40px; text-align: left; color: #fff; font-weight:100; font-size: 14px;"
                                            id="prevMonth" onclick="handleChangeMonthV2('prev');">
                                        Anterior
                                    </button>

                                    <div id="div__month" style="font-size: 18px; margin: auto 10px; font-size: 18px;"><b>${meses[month]}</b></div>
                                
                                    <button class="btn btn-warning new-family-teamwork"  
                                            style="border-radius: 25px; height: 40px; text-align: left; color: #fff; font-weight:100; margin-right: 10px; font-size: 14px;"
                                            id="nextMonth" onclick="handleChangeMonthV2('next');">
                                        Siguiente
                                    </button>

                                    <button class="btn btn-teamwork new-family-teamwork"  
                                            style="border-radius: 25px; height: 40px; text-align: left; color: #fff; font-weight:100; display:none; font-size: 14px;"
                                            id="btnSetAsistencia" onclick="handleSetAsistenciaV2()">
                                        Guardar Cambios
                                    </button>
                                </div>
                            </div>

                            <div  class="col-12 col-sm-12 col-md-12 col-lg-4">
                                <div class="row">
                                    <div id="div__mesCerrado"></div>
                                    <div id="div__listaArchivos"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                    `;
            document.getElementById('divCambioMonth').innerHTML = cambioMonth;

            ajaxGetCierrePeriodo(prefix, empresa, periodo, areaNegocio, "div__mesCerrado");
            ajaxSearchAsistenciaV2(prefix, empresa, ficha, periodo, areaNegocio, pagination);
        }
    });
};

ajaxSearchAsistenciaV2 = (prefix, empresa, ficha, periodo, areaNegocio, pagination) => {
    try {
        let method = 'ObtenerAsistenciaV2';
        let xhr = new XMLHttpRequest();
        let __form = new FormData();

        __form.append("empresa", empresa);
        __form.append("ficha", ficha);
        __form.append("fecha", periodo + '-01');
        __form.append("areaNegocio", areaNegocio);
        __form.append("pagination", pagination);

        xhr.open('POST', `${prefix}${method}`);
        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                const request = JSON.parse(xhr.responseText);

                let tabla = '';
                let asistencia = '';
                let monthDays = '';
                let weekDays = '';
                let dayMonth = '';

                let firstDay = new Date(periodo.substr(0, 4), periodo.substr(5, 2) - 1, 1);
                let lastDay = new Date(periodo.substr(0, 4), periodo.substr(5, 2), 0);
                let month = firstDay.getMonth();

                let falCount = 0;
                let licCount = 0;
                let vacCount = 0;
                let diaFalta = 0;
                let diaLic = 0;
                let diaVac = 0;

                if (request.Personal.length > 0) {
                    for (i = 0; i < request.Personal.length; i++) {
                        let fechaTermino = '';

                        let fechaInicio = new Date(request.Personal[i].FechaInicio.substr(6, 4) + '-' + request.Personal[i].FechaInicio.substr(3, 2) + '-' + (parseInt(request.Personal[i].FechaInicio.substr(0, 2))));
                        let fechaMonth = new Date(periodo.substr(0, 4), periodo.substr(5, 2) - 1, 1);

                        if (request.Personal[i].FechaTermino === '') {
                            fechaTermino = new Date('9999-12-31');
                        }
                        else {
                            fechaTermino = new Date(request.Personal[i].FechaTermino.substr(6, 4) + '-' + request.Personal[i].FechaTermino.substr(3, 2) + '-' + (parseInt(request.Personal[i].FechaTermino.substr(0, 2)) + 1));
                        }

                        if ((parseInt(request.Personal[i].FechaTermino.substr(0, 2)) + 1) === fechaTermino.getDate()) {
                            fechaTermino = new Date(request.Personal[i].FechaTermino.substr(6, 4) + '-' + request.Personal[i].FechaTermino.substr(3, 2) + '-' + (parseInt(request.Personal[i].FechaTermino.substr(0, 2))));
                        }

                        let markFiniquito = false;
                        let markBajaConfirmada = false;
                        let markPendienteKAM = false;
                        let markBajaKAM = false;
                        let fechaBajaKAM = '';

                        let diaBajaConfirmada = 0;
                        let bajaConfirmadaClass = '';
                        let bajaConfirmadaColor = '';
                        let fechaBajaConfirmada = '';

                        dayMonth = '';

                        if (request.BajasConfirmadas.length > 0) {
                            for (k = 0; k < request.BajasConfirmadas.length; k++) {
                                if (request.Personal[i].Ficha === request.BajasConfirmadas[k].Ficha) {
                                    diaBajaConfirmada = Number(request.BajasConfirmadas[k].DiaBaja);
                                    bajaConfirmadaClass = request.BajasConfirmadas[k].Class;
                                    bajaConfirmadaColor = request.BajasConfirmadas[k].Color;
                                    fechaBajaConfirmada = new Date(Number(request.BajasConfirmadas[k].FechaTermino.substr(0, 4)), Number(request.BajasConfirmadas[k].FechaTermino.substr(5, 2)) - 1, Number(request.BajasConfirmadas[k].FechaTermino.substr(8, 2)));
                                }
                            }
                        }

                        for (j = 0; j < lastDay.getDate(); j++) {
                            let markCell = false;

                            //LICENCIAS
                            if (request.Licencia.length > 0) {
                                if (licCount < request.Licencia.length) {
                                    if (request.Personal[i].Ficha === request.Licencia[licCount].Ficha) {
                                        diaLic = parseInt(request.Licencia[licCount].DiaLic);
                                        if (diaLic === j + 1) {
                                            dayMonth += `<td class="${request.Licencia[licCount].Class}" style="border: 1px solid; text-align: center;">${request.Licencia[licCount].CodigoAsistencia}</td>`;
                                            licCount++;
                                            markCell = true;
                                        }
                                    }
                                }
                            }

                            //VALIDA BAJA CONFIRMADA
                            if (!markBajaConfirmada) {
                                if (fechaTermino < fechaMonth) {
                                    if (Number(fechaTermino.getFullYear() + '' + fechaTermino.getMonth() + '' + fechaTermino.getDate()) === Number(fechaMonth.getFullYear() + '' + fechaMonth.getMonth() + '' + fechaMonth.getDate())) {
                                        if (diaBajaConfirmada > 0) {
                                            if (Number(fechaTermino.getFullYear() + '' + fechaTermino.getMonth() + '' + fechaTermino.getDate()) === Number(fechaBajaConfirmada.getFullYear() + '' + fechaBajaConfirmada.getMonth() + '' + fechaBajaConfirmada.getDate())) {
                                                dayMonth +=
                                                    `<td class="bajaconfirmada" style="border: 1px solid;">
                                                        <span style="color: #fff;">BC</span>
                                                    </td>`;
                                                markBajaConfirmada = true;
                                            }
                                            else {
                                                dayMonth +=
                                                    `<td class="pendiente" style="border: 1px solid;">
                                                        <span style="color: #fff;">PEN</span>
                                                    </td>`;
                                                markFiniquito = true;
                                            }
                                        }
                                        else {
                                            dayMonth +=
                                                `<td class="pendienteconfirmacionKAM" style="border: 1px solid;">
                                                    <span style="color: #fff;">PCK</span>
                                                </td>`;
                                            markPendienteKAM = true;
                                        }
                                        markCell = true;
                                    }
                                    else if (Number(fechaTermino.getFullYear() + '' + fechaTermino.getMonth() + '' + fechaTermino.getDate()) < Number(fechaMonth.getFullYear() + '' + fechaMonth.getMonth() + '' + fechaMonth.getDate()) && fechaTermino.getMonth() === fechaMonth.getMonth()) {
                                        if (markPendienteKAM) {
                                            dayMonth +=
                                                `<td class="pendienteconfirmacionKAM" style="border: 1px solid;">
                                                    <span style="color: #fff;">PCK</span>
                                                </td>`;
                                        }
                                        else if (markFiniquito) {
                                            dayMonth +=
                                                `<td class="pendiente" style="border: 1px solid;">
                                                    <span style="color: #fff;">PEN</span>
                                                </td>`;
                                        }
                                        else {
                                            dayMonth +=
                                                `<td class="pendienteconfirmacionKAM" style="border: 1px solid;">
                                                    <span style="color: #fff;">PCK</span>
                                                </td>`;
                                        }
                                        markCell = true;
                                    }
                                    markCell = true;
                                }
                                else {
                                    if (diaBajaConfirmada > 0 && diaBajaConfirmada === j + 1) {
                                        if (Number(fechaTermino.getFullYear() + '' + fechaTermino.getMonth() + '' + fechaTermino.getDate()) === Number(fechaMonth.getFullYear() + '' + fechaMonth.getMonth() + '' + fechaMonth.getDate())) {
                                            if (diaBajaConfirmada > 0) {
                                                if (Number(fechaTermino.getFullYear() + '' + fechaTermino.getMonth() + '' + fechaTermino.getDate()) === Number(fechaBajaConfirmada.getFullYear() + '' + fechaBajaConfirmada.getMonth() + '' + fechaBajaConfirmada.getDate())) {
                                                    if (falCount < request.Asistencia.length) {
                                                        if (request.Personal[i].Ficha === request.Asistencia[falCount].Ficha) {
                                                            diaFalta = parseInt(request.Asistencia[falCount].Fecha.substr(0, 2));
                                                            if (diaFalta === j + 1) {
                                                                dayMonth += `<td class="${request.Asistencia[falCount].Class} ${`${request.Personal[i].Ficha}__${j + 1}`}" 
                                                                                ${periodoCerrado === 'S' ? '' : 'onclick="handleShowModalConfirmaEvento(this)"'}
                                                                                data-year="${periodo.substr(0, 4)}" data-month="${periodo.substr(5, 2)}" data-day="${j + 1}" data-ficha="${request.Personal[i].Ficha}"
                                                                                data-asistencia="${request.Asistencia[falCount].CodigoAsistencia}" data-nombre="${request.Personal[i].Nombres}" data-rut="${request.Personal[i].Rut}"
                                                                                data-areanegocio="${areaNegocio}" data-pagination="${pagination}" data-action="Actualizar"
                                                                                style="${periodoCerrado === 'S' ? '' : 'cursor:pointer;'} border: 1px solid; text-align: center;">
                                                                                    <span style="font-weight: bold; color:${request.Asistencia[falCount].Color};" data-day="${j + 1}" data-ficha="${request.Personal[i].Ficha}">${request.Asistencia[falCount].CodigoAsistencia}</span>
                                                                             </td>`;
                                                                falCount++;
                                                                markCell = true;
                                                            }
                                                            else {
                                                                dayMonth += `<td ${periodoCerrado === 'S' ? '' : 'onclick="handleChangeFaltaV2(this)"'}
                                                                             data-year="${periodo.substr(0, 4)}" data-month="${periodo.substr(5, 2)}" data-day="${j + 1}" 
                                                                             data-ficha="${request.Personal[i].Ficha}"  data-tipo=""
                                                                             style="${periodoCerrado === 'S' ? '' : 'cursor:pointer;'} border: 1px solid;"></td>`;
                                                            }

                                                        }
                                                        else {
                                                            dayMonth += `<td ${periodoCerrado === 'S' ? '' : 'onclick="handleChangeFaltaV2(this)"'}
                                                                             data-year="${periodo.substr(0, 4)}" data-month="${periodo.substr(5, 2)}" data-day="${j + 1}" 
                                                                             data-ficha="${request.Personal[i].Ficha}"  data-tipo=""
                                                                             style="${periodoCerrado === 'S' ? '' : 'cursor:pointer;'} border: 1px solid;"></td>`;
                                                        }
                                                    }
                                                    else {
                                                        dayMonth += `<td ${periodoCerrado === 'S' ? '' : 'onclick="handleChangeFaltaV2(this)"'}
                                                                             data-year="${periodo.substr(0, 4)}" data-month="${periodo.substr(5, 2)}" data-day="${j + 1}" 
                                                                             data-ficha="${request.Personal[i].Ficha}"  data-tipo=""
                                                                             style="${periodoCerrado === 'S' ? '' : 'cursor:pointer;'} border: 1px solid;"></td>`;
                                                    }
                                                    markBajaConfirmada = true;
                                                }
                                                else {
                                                    dayMonth +=
                                                        `<td class="pendiente" style="border: 1px solid;">
                                                    <span style="color: #fff;">PEN</span>
                                                </td>`;
                                                    markFiniquito = true;
                                                }
                                            }
                                            else {
                                                dayMonth +=
                                                    `<td class="pendienteconfirmacionKAM" style="border: 1px solid;">
                                                    <span style="color: #fff;">PCK</span>
                                                </td>`;
                                                markPendienteKAM = true;
                                            }
                                            markCell = true;
                                        }
                                        else if (Number(fechaTermino.getFullYear() + '' + fechaTermino.getMonth() + '' + fechaTermino.getDate()) < Number(fechaMonth.getFullYear() + '' + fechaMonth.getMonth() + '' + fechaMonth.getDate()) && fechaTermino.getMonth() === fechaMonth.getMonth()) {
                                            if (markPendienteKAM) {
                                                dayMonth +=
                                                    `<td class="pendienteconfirmacionKAM" style="border: 1px solid;">
                                                    <span style="color: #fff;">PCK</span>
                                                </td>`;
                                            }
                                            else if (markFiniquito) {
                                                dayMonth +=
                                                    `<td class="pendiente" style="border: 1px solid;">
                                                    <span style="color: #fff;">PEN</span>
                                                </td>`;
                                            }
                                            else {
                                                dayMonth +=
                                                    `<td class="bajaconfirmada" style="border: 1px solid;">
                                                    <span style="color: #fff;">BC</span>
                                                </td>`;
                                            }
                                            markCell = true;
                                        }
                                        else {
                                            if (Number(fechaTermino.getFullYear() + '' + fechaTermino.getMonth() + '' + fechaTermino.getDate()) === Number(fechaMonth.getFullYear() + '' + fechaMonth.getMonth() + '' + fechaMonth.getDate())) {
                                                if (diaBajaConfirmada > 0) {
                                                    if (Number(fechaTermino.getFullYear() + '' + fechaTermino.getMonth() + '' + fechaTermino.getDate()) === Number(fechaBajaConfirmada.getFullYear() + '' + fechaBajaConfirmada.getMonth() + '' + fechaBajaConfirmada.getDate())) {
                                                        dayMonth +=
                                                            `<td class="bajaconfirmada" style="border: 1px solid;">
                                                                <span style="color: #fff;">BC</span>
                                                            </td>`;
                                                        markBajaConfirmada = true;
                                                    }
                                                    else {
                                                        dayMonth +=
                                                            `<td class="pendiente" style="border: 1px solid;">
                                                                <span style="color: #fff;">PEN</span>
                                                            </td>`;
                                                        markFiniquito = true;
                                                    }
                                                }
                                                else {
                                                    dayMonth +=
                                                        `<td class="pendienteconfirmacionKAM" style="border: 1px solid;">
                                                        <span style="color: #fff;">PCK</span>
                                                    </td>`;
                                                    markPendienteKAM = true;
                                                }
                                                markCell = true;
                                            }
                                            else {
                                                dayMonth +=
                                                    `<td ${periodoCerrado === 'S' ? '' : 'onclick="handleChangeFaltaV2(this)"'}
                                                         data-year="${periodo.substr(0, 4)}" data-month="${periodo.substr(5, 2)}" data-day="${j + 1}" 
                                                         data-ficha="${request.Personal[i].Ficha}"  data-tipo=""
                                                         style="${periodoCerrado === 'S' ? '' : 'cursor:pointer;'} border: 1px solid;"></td>`;
                                                markBajaConfirmada = true;
                                            }
                                        }
                                    }
                                    else {
                                        //MARCA DIAS VACACIONES
                                        if (!markCell) {
                                            if (fechaTermino < fechaMonth) {
                                                if (diaBajaConfirmada > 0) {
                                                    if (Number(fechaTermino.getFullYear() + '' + fechaTermino.getMonth() + '' + fechaTermino.getDate()) === Number(fechaMonth.getFullYear() + '' + fechaMonth.getMonth() + '' + fechaMonth.getDate())) {
                                                        if (diaBajaConfirmada > 0) {
                                                            if (Number(fechaTermino.getFullYear() + '' + fechaTermino.getMonth() + '' + fechaTermino.getDate()) === Number(fechaBajaConfirmada.getFullYear() + '' + fechaBajaConfirmada.getMonth() + '' + fechaBajaConfirmada.getDate())) {
                                                                dayMonth +=
                                                                    `<td class="bajaconfirmada" style="border: 1px solid;">
                                                                        <span style="color: #fff;">BC</span>
                                                                    </td>`;
                                                                markBajaConfirmada = true;
                                                            }
                                                            else {
                                                                dayMonth +=
                                                                    `<td class="pendiente" style="border: 1px solid;">
                                                                        <span style="color: #fff;">PEN</span>
                                                                    </td>`;
                                                                markFiniquito = true;
                                                            }
                                                        }
                                                        else {
                                                            dayMonth +=
                                                                `<td class="pendienteconfirmacionKAM" style="border: 1px solid;">
                                                                        <span style="color: #fff;">PCK</span>
                                                                    </td>`;
                                                            markPendienteKAM = true;
                                                        }
                                                        markCell = true;
                                                    }
                                                    else if (Number(fechaTermino.getFullYear() + '' + fechaTermino.getMonth() + '' + fechaTermino.getDate()) < Number(fechaMonth.getFullYear() + '' + fechaMonth.getMonth() + '' + fechaMonth.getDate()) && fechaTermino.getMonth() === fechaMonth.getMonth()) {
                                                        if (markPendienteKAM) {
                                                            dayMonth +=
                                                                `<td class="pendienteconfirmacionKAM" style="border: 1px solid;">
                                                                    <span style="color: #fff;">PCK</span>
                                                                </td>`;
                                                        }
                                                        else if (markFiniquito) {
                                                            dayMonth +=
                                                                `<td class="pendiente" style="border: 1px solid;">
                                                                    <span style="color: #fff;">PEN</span>
                                                                </td>`;
                                                        }
                                                        else {
                                                            dayMonth +=
                                                                `<td class="bajaconfirmada" style="border: 1px solid;">
                                                                    <span style="color: #fff;">BC</span>
                                                                </td>`;
                                                        }
                                                        markCell = true;
                                                    }
                                                }
                                                else {
                                                    if (markFiniquito) {
                                                        dayMonth +=
                                                            `<td class="pendiente" style="border: 1px solid;">
                                                                    <span style="color: #fff;">PEN</span>
                                                                </td>`;
                                                    }
                                                    else {
                                                        dayMonth +=
                                                            `<td class="bajaconfirmada" style="border: 1px solid;">
                                                                    <span style="color: #fff;">BC</span>
                                                                </td>`;
                                                    }
                                                }
                                                markCell = true;
                                            }
                                            else {
                                                if (request.Vacacion.length > 0) {
                                                    if (vacCount < request.Vacacion.length) {
                                                        if (request.Personal[i].Ficha === request.Vacacion[vacCount].Ficha) {
                                                            diaVac = parseInt(request.Vacacion[vacCount].DiaLic);
                                                            if (diaVac === j + 1) {
                                                                dayMonth += `<td class="${request.Vacacion[vacCount].Class}" style="border: 1px solid #000; color: #fff; text-align: center;">${request.Vacacion[vacCount].CodigoAsistencia}</td>`;
                                                                vacCount++;
                                                                markCell = true;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        //MARCA DIAS DE INASISTENCIA
                                        if (!markCell) {
                                            if (fechaTermino < fechaMonth) {
                                                if (Number(fechaTermino.getFullYear() + '' + fechaTermino.getMonth() + '' + fechaTermino.getDate()) === Number(fechaMonth.getFullYear() + '' + fechaMonth.getMonth() + '' + fechaMonth.getDate())) {
                                                    if (diaBajaConfirmada > 0) {
                                                        if (Number(fechaTermino.getFullYear() + '' + fechaTermino.getMonth() + '' + fechaTermino.getDate()) === Number(fechaBajaConfirmada.getFullYear() + '' + fechaBajaConfirmada.getMonth() + '' + fechaBajaConfirmada.getDate())) {
                                                            dayMonth +=
                                                                `<td class="bajaconfirmada" style="border: 1px solid;">
                                                                    <span style="color: #fff;">BC</span>
                                                                </td>`;
                                                            markBajaConfirmada = true;
                                                        }
                                                        else {
                                                            dayMonth +=
                                                                `<td class="pendiente" style="border: 1px solid;">
                                                                    <span style="color: #fff;">PEN</span>
                                                                </td>`;
                                                            markFiniquito = true;
                                                        }
                                                    }
                                                    else {
                                                        dayMonth +=
                                                            `<td class="pendienteconfirmacionKAM" style="border: 1px solid;">
                                                                <span style="color: #fff;">PCK</span>
                                                            </td>`;
                                                        markPendienteKAM = true;
                                                    }
                                                    markCell = true;
                                                }
                                                else if (Number(fechaTermino.getFullYear() + '' + fechaTermino.getMonth() + '' + fechaTermino.getDate()) < Number(fechaMonth.getFullYear() + '' + fechaMonth.getMonth() + '' + fechaMonth.getDate()) && fechaTermino.getMonth() === fechaMonth.getMonth()) {
                                                    if (markPendienteKAM) {
                                                        dayMonth +=
                                                            `<td class="pendienteconfirmacionKAM" style="border: 1px solid;">
                                                                <span style="color: #fff;">PCK</span>
                                                            </td>`;
                                                    }
                                                    else if (markFiniquito) {
                                                        dayMonth +=
                                                            `<td class="pendiente" style="border: 1px solid;">
                                                                <span style="color: #fff;">PEN</span>
                                                            </td>`;
                                                    }
                                                    else {
                                                        dayMonth +=
                                                            `<td class="bajaconfirmada" style="border: 1px solid;">
                                                                <span style="color: #fff;">BC</span>
                                                            </td>`;
                                                    }
                                                    markCell = true;
                                                }
                                                markCell = true;
                                            }
                                            else {
                                                if (request.Asistencia.length > 0) {
                                                    if (falCount < request.Asistencia.length) {
                                                        if (request.Personal[i].Ficha === request.Asistencia[falCount].Ficha) {
                                                            diaFalta = parseInt(request.Asistencia[falCount].Fecha.substr(0, 2));
                                                            if (diaFalta === j + 1) {
                                                                dayMonth += `<td class="${request.Asistencia[falCount].Class} ${`${request.Personal[i].Ficha}__${j + 1}`}" 
                                                                                ${periodoCerrado === 'S' ? '' : 'onclick="handleShowModalConfirmaEvento(this)"'}
                                                                                data-year="${periodo.substr(0, 4)}" data-month="${periodo.substr(5, 2)}" data-day="${j + 1}" data-ficha="${request.Personal[i].Ficha}"
                                                                                data-asistencia="${request.Asistencia[falCount].CodigoAsistencia}" data-nombre="${request.Personal[i].Nombres}" data-rut="${request.Personal[i].Rut}"
                                                                                data-areanegocio="${areaNegocio}" data-pagination="${pagination}" data-action="Actualizar"
                                                                                style="${periodoCerrado === 'S' ? '' : 'cursor:pointer;'} border: 1px solid; text-align: center;">
                                                                                    <span style="font-weight: bold; color:${request.Asistencia[falCount].Color};" data-day="${j + 1}" data-ficha="${request.Personal[i].Ficha}">${request.Asistencia[falCount].CodigoAsistencia}</span>
                                                                             </td>`;

                                                                if (!markBajaKAM) {
                                                                    if (Number(request.Asistencia[falCount].Fecha.substr(0, 2)) === (j + 1)) {
                                                                        if (request.Asistencia[falCount].CodigoAsistencia === 'BK') {
                                                                            fechaBajaKAM = new Date(Number(periodo.substr(0, 4)), Number(periodo.substr(5, 2)) - 1, Number(j + 1));
                                                                            markBajaKAM = true;
                                                                        }
                                                                        else {
                                                                            if (Number(fechaTermino.getFullYear() + '' + fechaTermino.getMonth() + '' + fechaTermino.getDate()) === Number(fechaMonth.getFullYear() + '' + fechaMonth.getMonth() + '' + fechaMonth.getDate())) {
                                                                                if (diaBajaConfirmada > 0) {
                                                                                    if (Number(fechaTermino.getFullYear() + '' + fechaTermino.getMonth() + '' + fechaTermino.getDate()) === Number(fechaBajaConfirmada.getFullYear() + '' + fechaBajaConfirmada.getMonth() + '' + fechaBajaConfirmada.getDate())) {
                                                                                        markBajaConfirmada = true;
                                                                                    }
                                                                                    else {
                                                                                        markFiniquito = true;
                                                                                    }
                                                                                }
                                                                                else {
                                                                                    markPendienteKAM = true;
                                                                                }
                                                                                markCell = true;
                                                                            }
                                                                            else if (Number(fechaTermino.getFullYear() + '' + fechaTermino.getMonth() + '' + fechaTermino.getDate()) < Number(fechaMonth.getFullYear() + '' + fechaMonth.getMonth() + '' + fechaMonth.getDate()) && fechaTermino.getMonth() === fechaMonth.getMonth()) {
                                                                                if (markPendienteKAM) {
                                                                                    dayMonth +=
                                                                                        `<td class="pendienteconfirmacionKAM" style="border: 1px solid;">
                                                                                            <span style="color: #fff;">PCK</span>
                                                                                        </td>`;
                                                                                }
                                                                                else if (markFiniquito) {
                                                                                    dayMonth +=
                                                                                        `<td class="pendiente" style="border: 1px solid;">
                                                                                            <span style="color: #fff;">PEN</span>
                                                                                        </td>`;
                                                                                }
                                                                                else {
                                                                                    dayMonth +=
                                                                                        `<td class="bajaconfirmada" style="border: 1px solid;">
                                                                                            <span style="color: #fff;">BC</span>
                                                                                        </td>`;
                                                                                }
                                                                                markCell = true;
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                                falCount++;
                                                                markCell = true;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        //SIN CONTRATO
                                        if (fechaInicio > fechaMonth) {
                                            dayMonth +=
                                                `<td class="sincontrato" style="border: 1px solid;">
                                                    <span>SC</span>
                                                </td>`;
                                            markCell = true;
                                        }
                                        //FINIQUITADO
                                        if (fechaTermino < fechaMonth) {
                                            if (!markCell) {
                                                if (markBajaKAM) {
                                                    if (fechaTermino > fechaBajaKAM) {
                                                        dayMonth +=
                                                            `
                                                            <td class="create" ${periodoCerrado === 'S' ? '' : 'onclick=""'}
                                                                    data-year="${periodo.substr(0, 4)}" data-month="${periodo.substr(5, 2)}" data-day="${i + 1}" data-tipo="BK" 
                                                                    data-fechaBaja="${fechaBajaKAM.getFullYear()}-${fechaBajaKAM.getMonth()}-${fechaBajaKAM.getDate()}" style="border: 1px solid #000;">
                                                                <span style="color: #fff;">BK</span>
                                                            </td>
                                                            `;
                                                    }
                                                }
                                                else {
                                                    if (Number(fechaTermino.getFullYear() + '' + fechaTermino.getMonth() + '' + fechaTermino.getDate()) === Number(fechaMonth.getFullYear() + '' + fechaMonth.getMonth() + '' + fechaMonth.getDate())) {
                                                        if (diaBajaConfirmada > 0) {
                                                            if (Number(fechaTermino.getFullYear() + '' + fechaTermino.getMonth() + '' + fechaTermino.getDate()) === Number(fechaBajaConfirmada.getFullYear() + '' + fechaBajaConfirmada.getMonth() + '' + fechaBajaConfirmada.getDate())) {
                                                                dayMonth +=
                                                                    `<td class="bajaconfirmada" style="border: 1px solid;">
                                                                        <span style="color: #fff;">BC</span>
                                                                    </td>`;
                                                                markBajaConfirmada = true;
                                                            }
                                                            else {
                                                                dayMonth +=
                                                                    `<td class="pendiente" style="border: 1px solid;">
                                                                        <span style="color: #fff;">PEN</span>
                                                                    </td>`;
                                                                markFiniquito = true;
                                                            }
                                                        }
                                                        else {
                                                            dayMonth +=
                                                                `<td class="pendienteconfirmacionKAM" style="border: 1px solid;">
                                                                    <span style="color: #fff;">PCK</span>
                                                                </td>`;
                                                            markPendienteKAM = true;
                                                        }
                                                        markCell = true;
                                                    }
                                                    else if (Number(fechaTermino.getFullYear() + '' + fechaTermino.getMonth() + '' + fechaTermino.getDate()) < Number(fechaMonth.getFullYear() + '' + fechaMonth.getMonth() + '' + fechaMonth.getDate()) && fechaTermino.getMonth() === fechaMonth.getMonth()) {
                                                        if (markPendienteKAM) {
                                                            dayMonth +=
                                                                `<td class="pendienteconfirmacionKAM" style="border: 1px solid;">
                                                                    <span style="color: #fff;">PCK</span>
                                                                </td>`;
                                                        }
                                                        else if (markFiniquito) {
                                                            dayMonth +=
                                                                `<td class="pendiente" style="border: 1px solid;">
                                                                    <span style="color: #fff;">PEN</span>
                                                                </td>`;
                                                        }
                                                        else {
                                                            dayMonth +=
                                                                `<td class="bajaconfirmada" style="border: 1px solid;">
                                                                    <span style="color: #fff;">BC</span>
                                                                </td>`;
                                                        }
                                                        markCell = true;
                                                    }
                                                }
                                            }
                                            markCell = true;
                                        }
                                        //SIN MARCAR
                                        if (!markCell) {
                                            if (markBajaKAM) {
                                                if (fechaTermino > fechaBajaKAM) {
                                                    dayMonth +=
                                                        `
                                                        <td class="create" ${periodoCerrado === 'S' ? '' : 'onclick=""'}
                                                                data-year="${periodo.substr(0, 4)}" data-month="${periodo.substr(5, 2)}" data-day="${i + 1}" data-tipo="BK" 
                                                                data-fechaBaja="${fechaBajaKAM.getFullYear()}-${fechaBajaKAM.getMonth()}-${fechaBajaKAM.getDate()}" style="border: 1px solid #000">
                                                            <span style="color: #fff;">BK</span>
                                                        </td>
                                                        `;
                                                }
                                            }
                                            else {
                                                if (Number(fechaTermino.getFullYear() + '' + fechaTermino.getMonth() + '' + fechaTermino.getDate()) === Number(fechaMonth.getFullYear() + '' + fechaMonth.getMonth() + '' + fechaMonth.getDate())) {
                                                    if (diaBajaConfirmada > 0) {
                                                        if (Number(fechaTermino.getFullYear() + '' + fechaTermino.getMonth() + '' + fechaTermino.getDate()) === Number(fechaBajaConfirmada.getFullYear() + '' + fechaBajaConfirmada.getMonth() + '' + fechaBajaConfirmada.getDate())) {
                                                            dayMonth +=
                                                                `<td class="bajaconfirmada" style="border: 1px solid;">
                                                                    <span style="color: #fff;">BC</span>
                                                                </td>`;
                                                            markBajaConfirmada = true;
                                                        }
                                                        else {
                                                            dayMonth +=
                                                                `<td class="pendiente" style="border: 1px solid;">
                                                                    <span style="color: #fff;">PEN</span>
                                                                </td>`;
                                                            markFiniquito = true;
                                                        }
                                                    }
                                                    else {
                                                        if (falCount < request.Asistencia.length) {
                                                            if (request.Personal[i].Ficha === request.Asistencia[falCount].Ficha) {
                                                                diaFalta = parseInt(request.Asistencia[falCount].Fecha.substr(0, 2));
                                                                if (diaFalta === j + 1) {
                                                                    dayMonth += `<td class="${request.Asistencia[falCount].Class} ${`${request.Personal[i].Ficha}__${j + 1}`}" 
                                                                                ${periodoCerrado === 'S' ? '' : 'onclick="handleShowModalConfirmaEvento(this)"'}
                                                                                data-year="${periodo.substr(0, 4)}" data-month="${periodo.substr(5, 2)}" data-day="${j + 1}" data-ficha="${request.Personal[i].Ficha}"
                                                                                data-asistencia="${request.Asistencia[falCount].CodigoAsistencia}" data-nombre="${request.Personal[i].Nombres}" data-rut="${request.Personal[i].Rut}"
                                                                                data-areanegocio="${areaNegocio}" data-pagination="${pagination}" data-action="Actualizar"
                                                                                style="${periodoCerrado === 'S' ? '' : 'cursor:pointer;'} border: 1px solid; text-align: center;">
                                                                                    <span style="font-weight: bold; color:${request.Asistencia[falCount].Color};" data-day="${j + 1}" data-ficha="${request.Personal[i].Ficha}">${request.Asistencia[falCount].CodigoAsistencia}</span>
                                                                             </td>`;
                                                                    falCount++;
                                                                    markCell = true;
                                                                }
                                                            }
                                                            else {
                                                                dayMonth += `<td ${periodoCerrado === 'S' ? '' : 'onclick="handleChangeFaltaV2(this)"'}
                                                                             data-year="${periodo.substr(0, 4)}" data-month="${periodo.substr(5, 2)}" data-day="${j + 1}" 
                                                                             data-ficha="${request.Personal[i].Ficha}"  data-tipo=""
                                                                             style="${periodoCerrado === 'S' ? '' : 'cursor:pointer;'} border: 1px solid;"></td>`;
                                                            }
                                                        }
                                                        else {
                                                            dayMonth += `<td ${periodoCerrado === 'S' ? '' : 'onclick="handleChangeFaltaV2(this)"'}
                                                                             data-year="${periodo.substr(0, 4)}" data-month="${periodo.substr(5, 2)}" data-day="${j + 1}" 
                                                                             data-ficha="${request.Personal[i].Ficha}"  data-tipo=""
                                                                             style="${periodoCerrado === 'S' ? '' : 'cursor:pointer;'} border: 1px solid;"></td>`;
                                                        }
                                                        markPendienteKAM = true;
                                                    }
                                                    markCell = true;
                                                }
                                                else if (Number(fechaTermino.getFullYear() + '' + fechaTermino.getMonth() + '' + fechaTermino.getDate()) < Number(fechaMonth.getFullYear() + '' + fechaMonth.getMonth() + '' + fechaMonth.getDate()) && fechaTermino.getMonth() === fechaMonth.getMonth()) {
                                                    if (markPendienteKAM) {
                                                        dayMonth +=
                                                            `<td class="pendienteconfirmacionKAM" style="border: 1px solid;">
                                                                <span style="color: #fff;">PCK</span>
                                                            </td>`;
                                                    }
                                                    else if (markFiniquito) {
                                                        dayMonth +=
                                                            `<td class="pendiente" style="border: 1px solid;">
                                                                <span style="color: #fff;">PEN</span>
                                                            </td>`;
                                                    }
                                                    else {
                                                        dayMonth +=
                                                            `<td class="bajaconfirmada" style="border: 1px solid;">
                                                                <span style="color: #fff;">BC</span>
                                                            </td>`;
                                                    }
                                                    markCell = true;
                                                }
                                                else {
                                                    dayMonth += `<td ${periodoCerrado === 'S' ? '' : 'onclick="handleChangeFaltaV2(this)"'}
                                                                 data-year="${periodo.substr(0, 4)}" data-month="${periodo.substr(5, 2)}" data-day="${j + 1}" 
                                                                 data-ficha="${request.Personal[i].Ficha}"  data-tipo=""
                                                                 style="${periodoCerrado === 'S' ? '' : 'cursor:pointer;'} border: 1px solid;"></td>`;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else {
                                if (fechaTermino < fechaMonth) {
                                    if (Number(fechaTermino.getFullYear() + '' + fechaTermino.getMonth() + '' + fechaTermino.getDate()) === Number(fechaMonth.getFullYear() + '' + fechaMonth.getMonth() + '' + fechaMonth.getDate())) {
                                        if (diaBajaConfirmada > 0) {
                                            if (Number(fechaTermino.getFullYear() + '' + fechaTermino.getMonth() + '' + fechaTermino.getDate()) === Number(fechaBajaConfirmada.getFullYear() + '' + fechaBajaConfirmada.getMonth() + '' + fechaBajaConfirmada.getDate())) {
                                                dayMonth +=
                                                    `<td class="bajaconfirmada" style="border: 1px solid;">
                                                    <span style="color: #fff;">BC</span>
                                                </td>`;
                                                markBajaConfirmada = true;
                                            }
                                            else {
                                                dayMonth +=
                                                    `<td class="pendiente" style="border: 1px solid;">
                                                    <span style="color: #fff;">PEN</span>
                                                </td>`;
                                                markFiniquito = true;
                                            }
                                        }
                                        else {
                                            dayMonth +=
                                                `<td class="pendienteconfirmacionKAM" style="border: 1px solid;">
                                                    <span style="color: #fff;">PCK</span>
                                                </td>`;
                                            markPendienteKAM = true;
                                        }
                                        markCell = true;
                                    }
                                    else if (Number(fechaTermino.getFullYear() + '' + fechaTermino.getMonth() + '' + fechaTermino.getDate()) < Number(fechaMonth.getFullYear() + '' + fechaMonth.getMonth() + '' + fechaMonth.getDate()) && fechaTermino.getMonth() === fechaMonth.getMonth()) {
                                        if (markPendienteKAM) {
                                            dayMonth +=
                                                `<td class="pendienteconfirmacionKAM" style="border: 1px solid;">
                                                    <span style="color: #fff;">PCK</span>
                                                </td>`;
                                        }
                                        else if (markFiniquito) {
                                            dayMonth +=
                                                `<td class="pendiente" style="border: 1px solid;">
                                                    <span style="color: #fff;">PEN</span>
                                                </td>`;
                                        }
                                        else {
                                            dayMonth +=
                                                `<td class="bajaconfirmada" style="border: 1px solid;">
                                                    <span style="color: #fff;">BC</span>
                                                </td>`;
                                        }
                                        markCell = true;
                                    }
                                }
                                else {
                                    if (Number(fechaTermino.getFullYear() + '' + fechaTermino.getMonth() + '' + fechaTermino.getDate()) === Number(fechaMonth.getFullYear() + '' + fechaMonth.getMonth() + '' + fechaMonth.getDate())) {
                                        if (diaBajaConfirmada > 0) {
                                            if (Number(fechaTermino.getFullYear() + '' + fechaTermino.getMonth() + '' + fechaTermino.getDate()) === Number(fechaBajaConfirmada.getFullYear() + '' + fechaBajaConfirmada.getMonth() + '' + fechaBajaConfirmada.getDate())) {
                                                dayMonth +=
                                                    `<td class="bajaconfirmada" style="border: 1px solid;">
                                                    <span style="color: #fff;">BC</span>
                                                </td>`;
                                                markBajaConfirmada = true;
                                            }
                                            else {
                                                dayMonth +=
                                                    `<td class="pendiente" style="border: 1px solid;">
                                                    <span style="color: #fff;">PEN</span>
                                                </td>`;
                                                markFiniquito = true;
                                            }
                                        }
                                        else {
                                            dayMonth +=
                                                `<td class="pendienteconfirmacionKAM" style="border: 1px solid;">
                                                    <span style="color: #fff;">PCK</span>
                                                </td>`;
                                            markPendienteKAM = true;
                                        }
                                        markCell = true;
                                    }
                                    else if (Number(fechaTermino.getFullYear() + '' + fechaTermino.getMonth() + '' + fechaTermino.getDate()) < Number(fechaMonth.getFullYear() + '' + fechaMonth.getMonth() + '' + fechaMonth.getDate()) && fechaTermino.getMonth() === fechaMonth.getMonth()) {
                                        if (markPendienteKAM) {
                                            dayMonth +=
                                                `<td class="pendienteconfirmacionKAM" style="border: 1px solid;">
                                                    <span style="color: #fff;">PCK</span>
                                                </td>`;
                                        }
                                        else if (markFiniquito) {
                                            dayMonth +=
                                                `<td class="pendiente" style="border: 1px solid;">
                                                    <span style="color: #fff;">PEN</span>
                                                </td>`;
                                        }
                                        else {
                                            dayMonth +=
                                                `<td class="bajaconfirmada" style="border: 1px solid;">
                                                    <span style="color: #fff;">BC</span>
                                                </td>`;
                                        }
                                        markCell = true;
                                    }
                                    else {
                                        dayMonth +=
                                            `
                                            <td class="${bajaConfirmadaClass}" style=" border: 1px solid #000;">
                                                <span style="color: ${bajaConfirmadaColor};">BC</span>
                                            </td>
                                            `;
                                    }
                                }
                            }

                            fechaMonth = new Date(fechaMonth.getFullYear(), fechaMonth.getMonth(), fechaMonth.getDate() + 1);
                        }
                        //ROW ASISTENCIA
                        asistencia += `<tr>
                                           <td style="white-space: nowrap; border: 1px solid; font-weight: bolder; ${request.Personal[i].Plataforma === 'Softland' ? '' : 'background: #ffe1b4;'}"><b>${request.Personal[i].Nombres}</b></td>
                                           <td style="white-space: nowrap; border: 1px solid; font-weight: bolder; ${request.Personal[i].Plataforma === 'Softland' ? '' : 'background: #ffe1b4;'}"><b>${request.Personal[i].Rut}</b></td>
                                           <td style="white-space: nowrap; border: 1px solid; font-weight: bolder; ${request.Personal[i].Plataforma === 'Softland' ? '' : 'background: #ffe1b4;'}"><b>${request.Personal[i].Ficha}</b></td>
                                           <td style="white-space: nowrap; border: 1px solid; font-weight: bolder; ${request.Personal[i].Plataforma === 'Softland' ? '' : 'background: #ffe1b4;'}"><b>${request.Personal[i].CargoMod}</b></td>
                                           <td style="white-space: nowrap; border: 1px solid; font-weight: bolder; ${request.Personal[i].Plataforma === 'Softland' ? '' : 'background: #ffe1b4;'}"><b>${request.Personal[i].FechaInicio}</b></td>
                                           <td style="white-space: nowrap; border: 1px solid; font-weight: bolder; ${request.Personal[i].Plataforma === 'Softland' ? '' : 'background: #ffe1b4;'}"><b>${request.Personal[i].FechaTermino === '' ? 'Indefinido' : request.Personal[i].FechaTermino}</b></td>
                                           ${dayMonth}
                                       </tr>`;
                    }

                    for (i = 0; i < lastDay.getDate(); i++) {
                        let day = new Date(firstDay.getFullYear(), firstDay.getMonth(), firstDay.getDate() + i);
                        weekDays += `<th class="new-family-teamwork" style="border: 1px solid;">${dias[day.getDay()].substr(0, 2)}</th>`;
                        monthDays += `<th class="new-family-teamwork" style="border: 1px solid;">${i + 1}</th>`;
                    }


                    tabla = `<table class="table new-family-teamwork" style="font-size:11px; font-weight: 100;">
                                <thead style="background-color: rgb(220, 220, 220);">
                                    <tr>
                                        <th class="new-family-teamwork" style="border: 1px solid;" colspan="6"></th>
                                        ${weekDays}
                                    </tr>

                                    <tr>
                                        <th class="new-family-teamwork" style="border: 1px solid;">Nombre</th>
                                        <th class="new-family-teamwork" style="border: 1px solid;">Rut</th>
                                        <th class="new-family-teamwork" style="border: 1px solid;">Ficha</th>
                                        <th class="new-family-teamwork" style="border: 1px solid;">Cargo Mod</th>
                                        <th class="new-family-teamwork" style="border: 1px solid;">Inicio Contrato</th>
                                        <th class="new-family-teamwork" style="border: 1px solid;">Termino Contrato</th>
                                        ${monthDays}
                                    </tr>
                                </thead>
                                <tbody>
                                
                                    ${asistencia}
                                </tbody>
                            </table>`;

                    document.getElementById('calendarioAsistencia').innerHTML = tabla;
                    $('#loaderSearchAsistenciaV2').html('');
                }
                else {
                    document.getElementById('calendarioAsistencia').innerHTML =
                        `
                        <div class="new-family-teamwork" style="font-weight: 100; margin:30px;">
                            <h2>
                                <span>No se encontraron trabajadores con la busqueda realizada</span>
                            </h2>
                        </div>
                        `;
                    $('#loaderSearchAsistenciaV2').html('');
                    document.getElementById("div__Pagination").innerHTML = '';
                }
            }
        };
    }
    catch{
        //
    }
};

ajaxInsertAsistenciaV2 = (prefix, empresa, ficha, periodo, asistencia) => {
    try {
        let method = 'InsertaAsistenciaV2';
        let xhr = new XMLHttpRequest();
        let __form = new FormData();

        __form.append("empresa", empresa);
        __form.append("ficha", ficha);
        __form.append("asistencias", asistencia);

        xhr.open('POST', `${prefix}${method}`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                const request = JSON.parse(xhr.responseText);
                while (arrayAsistencia.length) {
                    arrayAsistencia.pop();
                }

                let msgAsistencia = '';
                let msgTitle = '';
                let countError = 0;
                if (request.Asistencia.length > 0) {
                    msgAsistencia = `<ol style="list-style-type:none;">`;
                    for (i = 0; i < request.Asistencia.length; i++) {
                        if (request.Asistencia[i].Code === '400') {
                            msgAsistencia += `<li><b>${request.Asistencia[i].Fecha}</b> - ${request.Asistencia[i].Message}</li>`;
                            countError++;
                        }
                    }
                    msgAsistencia += `</ol>`;

                    if (countError === 1) {
                        msgTitle = `<div class="ta-center">
                                        <img class="ta-center" src="../Resources/svg/046-error.svg" style="width:100px;" />
                                        <h5><b>La siguiente fecha no fue ingresada al sistema</b></h5>
                                    </div>`;
                    }
                    else if (countError > 1) {
                        msgTitle = `<div class="ta-center">
                                        <img src="../Resources/svg/046-error.svg" style="width:100px;" />
                                        <h5 class="ta-center"><b>Las siguientes fechas no fueron ingresadas al sistema</b></h5>
                                    </div>`;
                    }
                    else {
                        msgTitle = `<div class="ta-center">
                                        <img src="../Resources/svg/044-success.svg" style="width:100px;" />
                                        <h5 class="ta-center">Asistencia ingresada con éxito</h5>
                                    </div>`;
                    }

                    msgAsistencia += `<br><br>
                                      <div class="ta-center">
                                          <button class="btn btn-success" onclick="handleModalAceptV2()"
                                                  style="border-radius: 25px; height: 40px; text-align: left; color: #fff; font-weight:100;">
                                              Aceptar
                                          </button>
                                      </div>`;

                    document.querySelector('#divMessageAsistencia').innerHTML = msgTitle + msgAsistencia;
                    $("#modalMsjAsistencia").modal("show");
                }
                else {
                    msgTitle = `<div class="ta-center">
                                    <img src="../Resources/svg/046-error.svg" style="width:100px;" />
                                    <h5 class="ta-center">No fue posible ingresar asistencia, intentelo nuevamente.</h5>
                                </div>`;

                    msgAsistencia += `<br><br>
                                      <div class="ta-center">
                                          <button class="btn btn-success" onclick="handleModalAceptV2()"
                                                  style="border-radius: 25px; height: 40px; text-align: left; color: #fff; font-weight:100;">
                                              Aceptar
                                          </button>
                                      </div>`;

                    document.querySelector('#divMessageAsistencia').innerHTML = msgTitle + msgAsistencia;
                    $("#modalMsjAsistencia").modal("show");
                }
            }
        };
    }
    catch {
        //
    }
};

ajaxGetTipoAsistenciaV2 = (prefix, target, tipo, action) => {
    try {
        let method = 'GetTipoAsistencia';
        let xhr = new XMLHttpRequest();
        let __form = new FormData();

        xhr.open('POST', `${prefix}${method}`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                const request = JSON.parse(xhr.responseText);
                let tipoAsistenciaTable = '';
                let tipoAsistenciaHtml = '';
                let count = 0;
                let countLimit = 0;

                if (action === "Actualizar") {
                    countLimit = 2;
                }
                else {
                    countLimit = 6;
                }

                if (request.TipoAsistencia.length > 0) {
                    tipoAsistenciaTable = '<table style="text-align: left;">';

                    for (i = 0; i < request.TipoAsistencia.length; i++) {
                        tipoAsistenciaHtml +=
                            `
                            <td>
                                <div style="display: inline-block;">
                                    <label class="switch">
                                        <input type="radio" id="tipo${request.TipoAsistencia[i].Nombre}" 
                                                ${action === 'Actualizar' ? 'name="tipoUpdAsistencia"' : 'name="tipoAsistencia"'} name="tipoUpdAsistencia" 
                                                value="${request.TipoAsistencia[i].CodigoAsistencia}" ${request.TipoAsistencia[i].CodigoAsistencia === tipo ? 'checked' : ''} 
                                                data-clase="${request.TipoAsistencia[i].Class}" data-color="${request.TipoAsistencia[i].Color}"
                                                style="display: inline-block; font-weight:100;">
                                        <span class="slider round"></span>
                                    </label>
                                    <span class="new-new-family-teamwork" style="font-weight:100; margin-right: 5px;">${request.TipoAsistencia[i].Nombre}</span>
                                </div>
                            </td>
                            `;
                        count++;

                        if (count === countLimit) {
                            tipoAsistenciaTable +=
                                `
                                <tr>
                                    ${tipoAsistenciaHtml}
                                </tr>
                                `;
                            tipoAsistenciaHtml = '';
                            count = 0;
                        }

                    }
                    if (count < 6) {
                        tipoAsistenciaTable +=
                            `
                            <tr>
                                ${tipoAsistenciaHtml}
                            </tr>
                            `;
                    }
                    tipoAsistenciaTable += '</table>';
                }

                document.getElementById(target).innerHTML = tipoAsistenciaTable;
            }
        };
    }
    catch{
        //
    }
};

ajaxUpdateAsistenciaV2 = (prefix, empresa, ficha, fecha, periodo, tipo, areaNegocio, pagination) => {
    try {
        let method = 'ActualizaAsistencia';
        let xhr = new XMLHttpRequest();
        let __form = new FormData();

        __form.append("empresa", empresa);
        __form.append("ficha", ficha);
        __form.append("fecha", fecha);
        __form.append("tipo", tipo);

        xhr.open('POST', `${prefix}${method}`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                const request = JSON.parse(xhr.responseText);

                if (request.Asistencia[0].Code === "200") {
                    document.getElementById("div__alert__message").style.display = "block";
                    document.getElementById("alert__modalSuccess").classList.add("show");
                    document.getElementById("alert__modalSuccess").innerHTML = request.Asistencia[0].Message;

                    document.getElementById("div__alert__message").style.display = "none";
                    document.getElementById("alert__modalSuccess").classList.remove("show");
                    document.getElementById("alert__modalSuccess").innerHTML = "";

                    $("#modalEditAsistencia").modal("hide");
                    ajaxViewPartialLoaderTransaccionV2(prefix, "#loaderSearchAsistenciaV2", empresa, "", areaNegocio, periodo, pagination);
                }
                else {
                    document.getElementById("div__alert__message").style.display = "block";
                    document.getElementById("alert__modalError").classList.add("show");
                    document.getElementById("alert__modalError").innerHTML = request.Asistencia[0].Message;

                    document.getElementById("div__alert__message").style.display = "none";
                    document.getElementById("alert__modalError").classList.remove("show");
                    document.getElementById("alert__modalError").innerHTML = "";
                }
            }
        };
    }
    catch{
        //
    }
};

ajaxDeleteAsistenciaV2 = (prefix, empresa, ficha, fecha, periodo, tipo, areaNegocio, pagination) => {
    try {
        let method = 'EliminaAsistencia';
        let xhr = new XMLHttpRequest();
        let __form = new FormData();

        __form.append("empresa", empresa);
        __form.append("ficha", ficha);
        __form.append("fecha", fecha);
        __form.append("tipo", tipo);

        xhr.open('POST', `${prefix}${method}`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                const request = JSON.parse(xhr.responseText);

                if (request.Asistencia[0].Code === "200") {
                    document.getElementById("div__alert__message").style.display = "block";
                    document.getElementById("alert__modalSuccess").classList.add("show");
                    document.getElementById("alert__modalSuccess").innerHTML = request.Asistencia[0].Message;

                    document.getElementById("div__alert__message").style.display = "none";
                    document.getElementById("alert__modalSuccess").classList.remove("show");
                    document.getElementById("alert__modalSuccess").innerHTML = "";

                    $("#modalEditAsistencia").modal("hide");

                    document.querySelector(`.${ficha}__${Number(fecha.split('-')[0])}`).classList.remove("success");
                    document.querySelector(`.${ficha}__${Number(fecha.split('-')[0])}`).innerHTML = "";
                    document.querySelector(`.${ficha}__${Number(fecha.split('-')[0])}`).setAttribute("onclick", "handleChangeFaltaV2(this)");

                    //ajaxViewPartialLoaderTransaccionV2(prefix, "#loaderSearchAsistenciaV2", empresa, "", areaNegocio, periodo, pagination);
                }
                else {
                    document.getElementById("div__alert__message").style.display = "block";
                    document.getElementById("alert__modalError").classList.add("show");
                    document.getElementById("alert__modalError").innerHTML = request.Asistencia[0].Message;

                    document.getElementById("div__alert__message").style.display = "none";
                    document.getElementById("alert__modalError").classList.remove("show");
                    document.getElementById("alert__modalError").innerHTML = "";
                }
            }
        };
    }
    catch{
        //
    }
};

ajaxPagination = (prefix, empresa, ficha, periodo, areaNegocio, pagination, typePagination) => {
    try {
        let method = 'Pagination';
        let xhr = new XMLHttpRequest();
        let __form = new FormData();

        __form.append("empresa", empresa);
        __form.append("ficha", ficha);
        __form.append("fecha", periodo + '-01');
        __form.append("areaNegocio", areaNegocio);
        __form.append("pagination", pagination);
        __form.append("typePagination", typePagination);

        xhr.open('POST', `${prefix}${method}`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                const request = JSON.parse(xhr.responseText);

                let htmlPagination = '';
                if (request.Pagination.length > 0) {
                    for (i = 0; i < request.Pagination.length; i++) {
                        htmlPagination +=
                            `<button class="btn new-family-teamwork enlacePagina ${request.Pagination[i].Class}" ${request.Pagination[i].Properties}
                                     data-pagination="${request.Pagination[i].Rango}" data-empresa="${request.Pagination[i].Empresa}" data-ficha="${request.Pagination[i].Ficha}" 
                                     data-fecha="${request.Pagination[i].Fecha}" data-areanegocio="${request.Pagination[i].AreaNegocio}" 
                                     onclick="handlePagination(this)">
                                ${request.Pagination[i].NumeroPagina}
                             </button>
                            `;
                    }
                }

                document.getElementById("div__Pagination").innerHTML = htmlPagination;

                $('#loaderSearchAsistenciaV2').html('');
            }
        };
    }
    catch {
        //
    }
};

ajaxGetCierrePeriodo = (prefix, empresa, periodo, areaNegocio, target) =>{
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

ajaxSetCierrePeriodo = (prefix, empresa, fecha, areaNegocio) => {
    try {
        let method = 'InsertarCierrePeriodo';
        let xhr = new XMLHttpRequest();
        let __form = new FormData();

        __form.append("empresa", empresa);
        __form.append("fecha", fecha);
        __form.append("areaNegocio", areaNegocio);

        xhr.open('POST', `${prefix}${method}`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                const request = JSON.parse(xhr.responseText);
                let htmlCierrePeriodo = '';

                if (request.CierrePeriodo.length > 0) {
                    if (request.CierrePeriodo[0].Code === "200") {
                        document.getElementById("div__alert__message").style.display = "block";
                        document.getElementById("alert__modalSuccess").classList.add("show");
                        document.getElementById("alert__modalSuccess").innerHTML = request.CierrePeriodo[0].Message;

                        document.getElementById("div__alert__message").style.display = "none";
                        document.getElementById("alert__modalSuccess").classList.remove("show");
                        document.getElementById("alert__modalSuccess").innerHTML = "";

                        $("#modalEditAsistencia").modal("hide");

                        handleSearchPeriodos();
                    }
                    else {
                        document.getElementById("div__alert__message").style.display = "block";
                        document.getElementById("alert__modalError").classList.add("show");
                        document.getElementById("alert__modalError").innerHTML = request.CierrePeriodo[0].Message;

                        document.getElementById("div__alert__message").style.display = "none";
                        document.getElementById("alert__modalError").classList.remove("show");
                        document.getElementById("alert__modalError").innerHTML = "";
                    }
                }
            }
        };
    }
    catch {
        //
    }
};


setTimeout(() => {
    let domain = handleGetDomain();

    if (window.location.pathname.split('/')[2] === 'Asistencia' || window.location.pathname.split('/')[2] === 'Calendario') {
        //ajaxGetLegend(domain);
        ajaxGetTipoAsistenciaV2(domain, 'divTipoAsistencia', 'F', '');

        if (window.location.pathname.split('/')[3] === 'Calendario') {
            ajaxGetRutaFichero(domain);
        }
    }
}, 1000);


handleChangePeriodo = () => {
    let periodoDesde = document.getElementById("input__periodoDesde").value;
    let periodoHasta = document.getElementById("input__periodoHasta").value;
    let tipoPeriodo = document.querySelector('input[name="tipoPeriodo"]:checked').value;
    let empresa = document.getElementById("empresa").value;
    let areaNegocio = document.getElementById("areaNegocio").value;
    let msjError = '';


    ajaxGetPeriodos(periodoDesde, periodoHasta, tipoPeriodo, empresa, areaNegocio);
};

handleChangeTipoPeriodo = () => {
    let tipoPeriodo = document.querySelector('input[name="tipoPeriodo"]:checked').value;

    switch (tipoPeriodo) {
        case "AreaNegocio":
            document.getElementById("empresa").style.display = "inline-block";
            document.getElementById("areaNegocio").style.display = "inline-block";
            break;

        case "General":
            document.getElementById("empresa").style.display = 'none';
            document.getElementById("areaNegocio").style.display = 'none';
            break;
    }
};

handleSearchPeriodos = () => {
    let domain = handleGetDomain();
    let periodoDesde = document.getElementById("input__periodoDesde").value;
    let periodoHasta = document.getElementById("input__periodoHasta").value;
    let tipoPeriodo = document.querySelector('input[name="tipoPeriodo"]:checked').value;
    let empresa = document.getElementById("empresa").value;
    let areaNegocio = document.getElementById("areaNegocio").value;
    let msjError = '';
    let error = false;

    document.getElementById("div__errores").innerHTML = '';

    if (periodoDesde === '') {
        msjError = 'Debe seleccionar un periodo.';
        error = true;
    }

    switch (tipoPeriodo) {
        case "AreaNegocio":
            if (!error) {
                if (empresa === "" || empresa === null || empresa === undefined || empresa === '0') {
                    msjError = 'Debe seleccionar una empresa.';
                    error = true;
                }
            }

            if (!error) {
                if (areaNegocio === "" || areaNegocio === null || areaNegocio === undefined || areaNegocio === '0') {
                    msjError = 'Debe seleccionar un area de negocio.';
                    error = true;
                }
            }
            break;
        case "General":
            empresa = '';
            areaNegocio = '';
            break;
    }
    if (!error) {
        if (periodoDesde !== null && periodoDesde !== undefined && periodoDesde !== "" && periodoHasta !== null && periodoHasta !== undefined && periodoHasta !== "") {
            let fechaDesde = new Date(Number(periodoDesde.substr(0, 4)), Number(periodoDesde.substr(5, 2)) - 1, Number(periodoDesde.substr(8, 2)));
            let fechaHasta = new Date(Number(periodoHasta.substr(0, 4)), Number(periodoHasta.substr(5, 2)) - 1, Number(periodoHasta.substr(8, 2)));

            if (fechaDesde > fechaHasta) {
                msjError = 'Fecha desde no puede ser mayor a fecha hasta.';
                error = true;
            }
        }
    }

    if (!error) {
        ajaxGetPeriodos(domain, periodoDesde, periodoHasta, empresa, areaNegocio);
    }
    else {
        document.getElementById("div__errores").innerHTML =
            `<label style="margin-bottom: 30px;">
                <span class="new-teamwork-family" style="background-color: #FFDADA; padding: 10px 10px 10px 10px; margin-top: 10px;">
                    ${msjError}
                </span>
             </label>`;

        setTimeout(() => {
            document.getElementById("div__errores").innerHTML = '';
        }, 4000);
    }
};

handleExcepcionPeriodo = (e) => {
    let domain = handleGetDomain();
    let empresa = e.dataset.empresa;
    let fecha = e.dataset.fecha;
    let areaNegocio = e.dataset.areanegocio;
    let exception = e.dataset.excepcion;

    ajaxSetExcepcionPeriodo(domain, empresa, fecha, areaNegocio, exception);
};

ajaxGetPeriodos = (prefix, periodoDesde, periodoHasta, empresa, areaNegocio) => {
    try {
        let method = 'ObtenerCierrePeriodos';
        let xhr = new XMLHttpRequest();
        let __form = new FormData();

        __form.append("fechaDesde", periodoDesde + '-01');
        __form.append("fechaHasta", periodoHasta === '' ? '' : periodoHasta + '-01');
        __form.append("empresa", empresa);
        __form.append("areaNegocio", areaNegocio);

        xhr.open('POST', `${prefix}${method}`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                const request = JSON.parse(xhr.responseText);
                let htmlCierrePeriodos = '';
                let htmlPeriodoCerrado = '';
                let periodoDesde = '';

                if (request.CierrePeriodo.length > 0) {
                    htmlCierrePeriodos += `<table style="width:80%; text-align: center;">`;
                    for (i = 0; i < request.CierrePeriodo.length; i++) {
                        periodoDesde = new Date(Number(request.CierrePeriodo[i].FechaCierre.substr(0, 4)), Number(request.CierrePeriodo[i].FechaCierre.substr(5, 2)) - 1, Number(request.CierrePeriodo[i].FechaCierre.substr(8, 2)));

                        let htmlExcepcion =
                            `
                            <button class="btn btn-${request.CierrePeriodo[i].Excepcion === 'N' ? 'success' : 'warning'} new-family-teamwork fs-18" style="border-radius: 25px; height: 40px; text-align: left; color: #fff; font-weight:100; margin: 10px;" id="button__excepcion" onclick="handleConfirmaEvento(this)"
                                    data-fecha="${request.CierrePeriodo[i].FechaCierre.substr(8, 2) + '-' + request.CierrePeriodo[i].FechaCierre.substr(5, 2) + '-' + request.CierrePeriodo[i].FechaCierre.substr(0, 4)}"
                                    data-empresa="${empresa}" data-areanegocio="${areaNegocio}" data-action="ExcepcionPeriodo" data-exception="${request.CierrePeriodo[i].Excepcion}" >
                                ${request.CierrePeriodo[i].Excepcion === 'N' ? 'Agregar' : 'Quitar'} Excepción
                            </button>
                            `;

                        if (request.CierrePeriodo[i].Cerrado === "S") {
                            htmlPeriodoCerrado =
                                `
                                <span class="btn dspl-inline-block new-family-teamwork mr-20 fs-18" 
                                    style="margin-top: -4px; border-radius: 25px; height: 40px; text-align: left; color: rgb(217, 83, 79); background-color: #fff; margin: 10px;">
                                    Periodo Cerrado
                                </span>

                                ${request.Profile === 'Administrador' || request.Profile === 'Jefe de Operaciones' || request.Profile === 'Administrador Operaciones' ? htmlExcepcion : ''}
                                `;
                        }
                        else {
                            if (request.Profile === 'Administrador' || request.Profile === 'Jefe de Operaciones' || request.Profile === 'Administrador Operaciones') {
                                htmlPeriodoCerrado =
                                    `
                                    <button class="btn btn-teamwork new-family-teamwork fs-18"  
                                            style="border-radius: 25px; height: 40px; text-align: left; color: #fff; font-weight:100; margin: 10px;"
                                            id="button__CierraPeriodo" onclick="handleConfirmaEvento(this)"
                                            data-fecha="${request.CierrePeriodo[i].FechaCierre.substr(8, 2) + '-' + request.CierrePeriodo[i].FechaCierre.substr(5, 2) + '-' + request.CierrePeriodo[i].FechaCierre.substr(0, 4)}" 
                                            data-empresa="${empresa}" data-areanegocio="${areaNegocio}" data-action="CierrePeriodo">
                                        Cerrar Periodo
                                    </button>
                                    `;
                            }
                            else {
                                htmlPeriodoCerrado =
                                    `
                                    <span class="btn success dspl-inline-block new-family-teamwork mr-20 fs-18" 
                                        style="margin-top: -4px; border-radius: 25px; height: 40px; text-align: left; color: #fff; margin: 10px;">
                                        Periodo Abierto
                                    </span>
                                    `;
                            }
                        }



                        htmlCierrePeriodos +=
                            `
                            <tr class="mb-30 bt-1-solid-200x3 bb-1-solid-200x3" style="text-align: left;">
                                <td style="width: 40%;">
                                    <div style="color: rgb(100, 100, 100)">
                                        <label class="new-family-teamwork dspl-block lh-10 fs-14 mt-10">
                                            <h4><b>${meses[periodoDesde.getMonth()]} de ${periodoDesde.getFullYear()}</b></h4>
                                        </label>
                                    </div>
                                </td>

                                <td style="text-align: center;">
                                     ${htmlPeriodoCerrado}
                                </td>
                            </tr>
                            `;
                    }
                    htmlCierrePeriodos += `</table>`;
                    document.getElementById("div__periodos").innerHTML = htmlCierrePeriodos;
                }
            }
        };
    }
    catch {
        //
    }
};

ajaxSetExcepcionPeriodo = (prefix, empresa, fecha, areaNegocio, exception) => {
    try {
        let method = 'InsertarExcepcionPeriodo';
        let xhr = new XMLHttpRequest();
        let __form = new FormData();

        __form.append("empresa", empresa);
        __form.append("fecha", fecha);
        __form.append("areaNegocio", areaNegocio);
        __form.append("excepcion", exception);

        xhr.open('POST', `${prefix}${method}`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                const request = JSON.parse(xhr.responseText);
                let htmlCierrePeriodo = '';

                if (request.CierrePeriodo.length > 0) {
                    if (request.CierrePeriodo[0].Code === "200") {
                        document.getElementById("div__alert__message").style.display = "block";
                        document.getElementById("alert__modalSuccess").classList.add("show");
                        document.getElementById("alert__modalSuccess").innerHTML = request.CierrePeriodo[0].Message;

                        document.getElementById("div__alert__message").style.display = "none";
                        document.getElementById("alert__modalSuccess").classList.remove("show");
                        document.getElementById("alert__modalSuccess").innerHTML = "";

                        $("#modalEditAsistencia").modal("hide");

                        handleSearchPeriodos();
                    }
                    else {
                        document.getElementById("div__alert__message").style.display = "block";
                        document.getElementById("alert__modalError").classList.add("show");
                        document.getElementById("alert__modalError").innerHTML = request.CierrePeriodo[0].Message;

                        document.getElementById("div__alert__message").style.display = "none";
                        document.getElementById("alert__modalError").classList.remove("show");
                        document.getElementById("alert__modalError").innerHTML = "";
                    }
                }
            }
        };
    }
    catch {
        //
    }
};





handleGetPlantillaMasiva = () => {
    let domain = handleGetDomain();
    let plantillaMasiva = document.getElementById("sel__plantillaMasiva").value;

    if (plantillaMasiva === null || plantillaMasiva === undefined || plantillaMasiva === '') {
        document.getElementById('div__renderCaraMasiva').innerHTML = '';
    }
    else {
        window.location.href = window.location.origin + '/Asistencia/SolicitudAsistencia?ref=' + plantillaMasiva;
    }
};

ajaxGetPlantillaMasiva = (prefix, cargaMasiva, plantillaMasiva) => {
    try {
        let method = 'RenderizaCargaMasiva';
        let xhr = new XMLHttpRequest();
        let __form = new FormData();

        __form.append("cargaMasiva", cargaMasiva);
        __form.append("plantillaMasiva", plantillaMasiva);

        xhr.open('POST', `${prefix}${method}`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                const request = JSON.parse(xhr.responseText);
                let htmlCargaMasiva = '';

                if (request.CargaMasivas.length > 0) {
                    for (i = 0; i < request.CargaMasivas.length; i++) {
                        htmlCargaMasiva =
                            `
                                <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                                    <h1 class="new-family-teamwork" style="font-weight: 100; font-size: 40px;">
                                        <i class="glyphicon-tw-21 ${request.CargaMasivas[i].Glyphicon}"></i>
                                        <br />
                                        ${request.CargaMasivas[i].RenderizadoTituloOne} ${request.CargaMasivas[i].RenderizadoTituloTwo}
                                    </h1>
                                    <h2 class="new-family-teamwork fs-20 mt-5neg" style="font-weight: 100;">${request.CargaMasivas[i].RenderizadoDescripcion}</h2>
                                </div>
                                <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                                    <div class="new-family-teamwork m-auto ta-justify" style="font-weight: 100; color: #f00; width: 70%;" id="messagePlataforma"></div>
                                </div>
                                <div class="col-12 col-sm-12 col-md-12 col-lg-12">
                                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 ta-center">
                                        <input type="button" id="eventFileUpload" ng-click="uploadExcel()" style="display: none;" />
                                        <input type="file" id="file" style="display: none;" accept="application/excel" />
                                        <a href="#" id="fileUpload" class="ta-center btn ${request.CargaMasivas[i].UploadRenderizadoColor}" data-mameTemplate="${request.CargaMasivas[i].UploadNombrePlantilla}" data-nameSheet="${request.CargaMasivas[i].UploadNombreHojaCargaMasiva}"
                                           data-cifrado="${request.CargaMasivas[i].UploadCifrado}" data-columnas="${request.CargaMasivas[i].UploadColumnas}" data-typetemplate=""
                                           data-nodoPadre="${request.CargaMasivas[i].UploadNodoPadre}" data-nodoHijo="${request.CargaMasivas[i].UploadNodoHijo}" data-templatedownload="${request.CargaMasivas[i].DownloadCifrado}"
                                           style="border-radius: 100px; min-width: 400px; margin-top: 12px; text-align: left; color: #fff;">
                                            <i class="glyphicon-tw-15 ${request.CargaMasivas[i].UploadRenderizadoGlyphicon}" style="display: block; margin-bottom: -15px;"></i>
                                            <span class="new-family-teamwork ta-center dspl-block" style="font-weight: 100; margin-bottom: 10px;">
                                                <i style="font-size: 25px; font-style: normal; display: block; margin-bottom: -10px;">${request.CargaMasivas[i].UploadRenderizadoOneTexto}</i>
                                                <i style="font-size: 25px; font-style: normal; display: block;">${request.CargaMasivas[i].UploadRenderizadoSecTexto}</i>
                                            </span>
                                        </a>
                                        <p class="new-family-teamwork mt-5" style="font-weight: 100; font-size: 15px;">${request.CargaMasivas[i].UploadRenderizadoMensajeImpt}</p>
                                    </div>
                                </div>
                                <div class="col-12 col-sm-12 col-md-12 col-lg-12 ta-center mt-30">
                                    <h1 class="new-family-teamwork" style="font-weight: 100; font-size: 30px;">${request.CargaMasivas[i].RenderizadoTituloDownload} ${request.CargaMasivas[i].RenderizadoTituloTwo}</h1>
                                </div>
                                <div class="col-12 col-sm-12 col-md-12 col-lg-12 ta-center mt-10">
                                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 ta-center">
                                        <a href="${request.CargaMasivas[i].Path}" class="btn ${request.CargaMasivas[i].DownloadRenderizadoColor}"
                                           style="border-radius: 100px; min-width: 400px; margin-top: 12px; text-align: left; color: #fff;">
                                            <i class="glyphicon-tw-15 ${request.CargaMasivas[i].DownloadRenderizadoGlyphicon}" style="display: block; margin-bottom: -15px;"></i>
                                            <span class="new-family-teamwork ta-center dspl-block" style="font-weight: 100; margin-bottom: 10px;">
                                                <i style="font-size: 25px; font-style: normal; display: block; margin-bottom: -10px;">${request.CargaMasivas[i].DownloadRenderizadoOneTexto}</i>
                                                <i style="font-size: 25px; font-style: normal; display: block;">${request.CargaMasivas[i].DownloadRenderizadoSecTexto}</i>
                                            </span>
                                        </a>
                                        <p class="family-teamwork mt-5" style="font-size: 15px;">${request.CargaMasivas[i].DownloadRenderizadoMensajeImpt}</p>
                                    </div>
                                </div>
                            <span id="loaderProceso"></span>
                        `;
                    }
                }

                document.getElementById('div__renderCaraMasiva').innerHTML = htmlCargaMasiva;
            }
        };
    }
    catch {
        //
    }
};

handleReturnMenuAsistencia = () => {
    window.location.href = `${__SERVERHOST}/Asistencia`;
    //window.location.href = `${__SERVERHOST}`;
};

handleReturnMenuCargaMasiva = () => {
    window.location.href = `${__SERVERHOST}/Asistencia/CargasMasivas`;
};



handleDownloadArchivo = (e) => {
    let domain = handleGetDomain();
    let nombre = e.dataset.nombre;
    let nombreReal = e.dataset.nombrereal;
    let ruta = e.dataset.ruta;


    window.location.href = `${domain}DownloadArchivo?ref1=${nombre}&ref2=${nombreReal}&ref3=${ruta}`;
};

handleFileChange = (e) => {
    const inputFile = document.getElementById('fileAsistencia');
    const selectRuta = document.getElementById('select__rutaFichero');

    let domain = handleGetDomain();
    let rutaFichero = selectRuta.value;
    let empresa = document.getElementById('empresa').value;
    let periodo = document.getElementById('periodo').value;
    let ficha = document.getElementById('ficha').value;
    let codigoRuta = selectRuta.options[selectRuta.options.selectedIndex].dataset.codigoruta;

    let areaNegocio = e.dataset.areanegocio;

    document.getElementById("div__message").innerHTML = '';
    document.getElementById("div__message").style.display = "none";

    if (inputFile.files.length > 0) {
        for (i = 0; i < inputFile.files.length; i++) {
            ajaxGetNombreArchivo(domain, inputFile.files[i], rutaFichero, empresa, areaNegocio, periodo, ficha, codigoRuta);
        }
    }
};

handleClickUpload = () => {
    document.getElementById("fileAsistencia").click();
};

handleDeleteArchivo = (e) => {
    let domain = handleGetDomain();
    let ruta = e.dataset.ruta;
    let rutaCodigo = e.dataset.rutacodigo;
    let nombre = e.dataset.nombre;
    let nombreReal = e.dataset.nombrereal;
    let codigoArchivo = e.dataset.archivo;
    let empresa = document.getElementById('empresa').value;
    let periodo = document.getElementById('periodo').value;
    let ficha = document.getElementById('ficha').value;

    ajaxDeleteArchivo(domain, ruta, rutaCodigo, nombre, nombreReal, codigoArchivo, empresa, periodo, ficha);
};


ajaxGetRutaFichero = (prefix) => {
    try {
        let method = 'ObtenerRutaFichero';
        let xhr = new XMLHttpRequest();
        let __form = new FormData();

        xhr.open('POST', `${prefix}${method}`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                const request = JSON.parse(xhr.responseText);
                let htmlrutaFichero = '';
                htmlrutaFichero = htmlrutaFichero += `<option value="" data-codigoruta="" selected>Selecciones Ruta de Archivos</option>`;
                if (request.RutaFicheros.length > 0) {

                    for (i = 0; i < request.RutaFicheros.length; i++) {
                        htmlrutaFichero +=
                            `
                            <option value="${request.RutaFicheros[i].RutaFichero}" data-codigoruta="${request.RutaFicheros[i].CodigoRuta}" ${i === 0 ? 'selected' : ''}>
                                ${request.RutaFicheros[i].NombreRuta}
                            </option>
                            `;
                    }
                }

                document.getElementById("select__rutaFichero").innerHTML = htmlrutaFichero;
            }
        };
    }
    catch {
        //
    }
};

ajaxGetNombreArchivo = (prefix, fileData, rutaFichero, empresa, areaNegocio, periodo, ficha, codigoRuta) => {
    try {
        let method = 'ObtenerNombreArchivo';
        let xhr = new XMLHttpRequest();
        let __form = new FormData();

        xhr.open('POST', `${prefix}${method}`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                const request = JSON.parse(xhr.responseText);

                if (request.ArchivoAsistencia.length > 0) {

                    for (i = 0; i < request.ArchivoAsistencia.length; i++) {
                        ajaxSetArchivoAsistencia(prefix, fileData, rutaFichero, empresa, areaNegocio, periodo, ficha, codigoRuta, request.ArchivoAsistencia[i].NombreArchivo);
                    }
                }
            }
        };
    }
    catch {
        //
    }
};

ajaxGetArchivosAsistencia = (prefix, empresa, areaNegocio, periodo, ficha, tipo) => {
    try {
        let method = 'ObtenerArchivoAsistencia';
        let xhr = new XMLHttpRequest();
        let __form = new FormData();

        __form.append("empresa", empresa);
        __form.append("areaNegocio", areaNegocio);
        __form.append("fecha", periodo + '-01');
        __form.append("ficha", ficha);

        xhr.open('POST', `${prefix}${method}`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                const request = JSON.parse(xhr.responseText);
                let htmlArchivos = '';
                    
                switch (tipo) {
                    case "Ficha":
                        htmlArchivos =
                            `
                            <input id="fileAsistencia" type="file" multiple onchange="handleFileChange(this)" data-areanegocio="${areaNegocio}" style="display: none;" />
                            <button id="file__Upload" onclick="handleClickUpload()" class="btn btn-success ta-center" style="border-radius: 25px;  text-align: left; color: #fff; font-weight:100;">
                                <span>Cargar Archivos </span>
                                <img src="../Resources/svg/upload-file.svg" width="28" height="28" style="position: relative;">
                            </button>

                            <div id="div__message" style="border-radius: 15px; margin-top: 20px; margin-bottom: 10px; display:none;"></div>
                            `;

                        if (request.ArchivoAsistencia.length) {
                            htmlArchivos +=
                                `
                            <table style="width:100%">
                            `;
                            for (i = 0; i < request.ArchivoAsistencia.length; i++) {
                                htmlArchivos +=
                                    `
                                <tr>
                                    <td style="width:70%; padding: 10px; border-bottom: solid 1px #939393;">
                                        ${request.ArchivoAsistencia[i].NombreRealArchivo}
                                    </td>
                                
                                    <td style="padding: 10px; border-bottom: solid 1px #939393;">
                                        <button class="btn btn-info"
                                                style="border-radius: 25px; text-align: left; color: #fff; font-weight:100;"
                                                onclick="handleDownloadArchivo(this)"
                                                data-nombre="${btoa(request.ArchivoAsistencia[i].NombreArchivo)}" data-nombrereal="${btoa(request.ArchivoAsistencia[i].NombreRealArchivo)}" 
                                                data-ruta="${btoa(request.ArchivoAsistencia[i].RutaFichero)}">
                                            <span>Descargar </span>
                                            <img src="../Resources/svg/download-file.svg" width="28" height="28" style="position: relative;">
                                        </button>
                                    </td>

                                    <td style="padding: 10px; border-bottom: solid 1px #939393;">
                                        <button class="btn btn-danger"
                                                style="border-radius: 25px; text-align: left; color: #fff; font-weight:100; height: 48px;"
                                                onclick="handleDeleteArchivo(this)"
                                                data-nombre="${btoa(request.ArchivoAsistencia[i].NombreArchivo)}" data-nombrereal="${btoa(request.ArchivoAsistencia[i].NombreRealArchivo)}" 
                                                data-ruta="${btoa(request.ArchivoAsistencia[i].RutaFichero)}" data-archivo="${btoa(request.ArchivoAsistencia[i].CodigoArchivo)}" 
                                                data-rutacodigo="${btoa(request.ArchivoAsistencia[i].CodigoRuta)}">
                                            <img src="../Resources/eliminar.svg" width="25" height="25" style="position: relative;">
                                        </button>
                                    </td>
                                </tr>
                                `;
                            }
                            htmlArchivos +=
                                `
                            </table>
                            `;

                        }
                        break;
                    case "AreaNegocio":

                        break;
                }

                document.getElementById("div__listaArchivos").innerHTML = htmlArchivos;
            }
        };
    }
    catch {
        //
    }
};

ajaxSetArchivoAsistencia = (prefix, fileData, rutaFichero, empresa, areaNegocio, periodo, ficha, codigoRuta, nombreArchivo) => {
    try {
        let method = 'InsertaArchivoAsistencia';
        let xhr = new XMLHttpRequest();
        let __form = new FormData();

        __form.append("fileData", fileData);
        __form.append("rutaFichero", rutaFichero);
        __form.append("empresa", empresa);
        __form.append("areaNegocio", areaNegocio);
        __form.append("fecha", periodo + '-01');
        __form.append("ficha", ficha);
        __form.append("nombreArchivo", nombreArchivo);
        __form.append("codigoRuta", codigoRuta);

        xhr.open('POST', `${prefix}${method}`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {

                const request = JSON.parse(xhr.responseText);
                htmlMessage = '';

                if (request.ArchivoAsistencia.length) {
                    for (i = 0; i < request.ArchivoAsistencia.length; i++) {
                        htmlMessage =
                            `
                            <p>
                                <span style="background-color: #${request.ArchivoAsistencia[i].Code === "200" ? '007a00' : 'fe5858'}; border-radius: 10px; padding: 10px 20px 10px 20px; color: #fff;">
                                    ${request.ArchivoAsistencia[i].Message}
                                </span>
                            </p>
                            `;
                    }
                }
                else {
                    htmlMessage =
                        `
                        <p>
                            <span style="background-color: #fe5858; border-radius: 10px; padding: 10px 20px 10px 20px; color: #fff;">
                                ${request.Error}
                            </span>
                        </p>
                        `;
                }

                document.getElementById("div__message").innerHTML += htmlMessage;
                document.getElementById("div__message").style.display = "block";


                setTimeout(() => {
                    ajaxSearchDataPersonal(prefix, empresa, ficha, periodo);
                }, 2000);
            }
        };
    }
    catch {
        //
    }
};

ajaxDeleteArchivo = (prefix, rutaFichero, codigoRuta, nombreArchivo, nombreReal, codigoArchivo, empresa, periodo, ficha) => {
    try {
        let method = 'EliminaArchivoAsistencia';
        let xhr = new XMLHttpRequest();
        let __form = new FormData();

        __form.append("rutaFichero", rutaFichero);
        __form.append("codigoRuta", codigoRuta);
        __form.append("nombreArchivo", nombreArchivo);
        __form.append("nombreReal", nombreReal);
        __form.append("codigoArchivo", codigoArchivo);

        xhr.open('POST', `${prefix}${method}`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {

                const request = JSON.parse(xhr.responseText);
                htmlMessage = '';

                if (request.ArchivoAsistencia.length) {
                    for (i = 0; i < request.ArchivoAsistencia.length; i++) {
                        htmlMessage =
                            `
                            <p>
                                <span style="background-color: #${request.ArchivoAsistencia[i].Code === "200" ? '007a00' : 'fe5858'}; border-radius: 10px; padding: 10px 20px 10px 20px; color: #fff;">
                                    ${request.ArchivoAsistencia[i].Message}
                                </span>
                            </p>
                            `;
                    }
                }
                else {
                    htmlMessage =
                        `
                        <p>
                            <span style="background-color: #fe5858; border-radius: 10px; padding: 10px 20px 10px 20px; color: #fff;">
                                ${request.Error}
                            </span>
                        </p>
                        `;
                }

                document.getElementById("div__message").innerHTML += htmlMessage;
                document.getElementById("div__message").style.display = "block";

                setTimeout(() => {
                    ajaxSearchDataPersonal(prefix, empresa, ficha, periodo);
                }, 2000);
            }
        };
    }
    catch {
        //
    }
};


//CARGA MASIVA
handleChangePlantillaRelojControl = () => {
    let plantilla = document.getElementById('select__plantillaCarga').value;

    if (plantilla !== null && plantilla !== '' && plantilla !== undefined) {
        window.location.href = window.location.href.split('?')[0] + '?ref=' + plantilla;
    }
    else {
        window.location.href = window.location.href.split('?')[0];
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

                if (request.Bonos.length > 0) {
                    switch (action) {
                        case 'Array':
                            for (i = 0; i < request.Bonos.length; i++) {
                                if (request.Bonos[i].Code === '200') {
                                    if (request.Bonos[i].Vigente === 'S') {
                                        if (!arrayBonos.find(item => item === request.Bonos[i].Bono)) {
                                            arrayBonos.push(request.Bonos[i].Bono);
                                        }
                                    }
                                }
                            }
                            break;
                    }
                }
            }
        };
    }
    catch {
        //
    }
};