//const __APP = "/AplicacionOperaciones/Asistencia/";
const __APP = "";
const __SERVERHOST = `${window.location.origin}${__APP}`;
let errores = [];

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

handleChangeEmpresa = () => {
    let empresa = document.getElementById('empresa').value;

    handleGetAreaNegocio(empresa, '');

    document.getElementById('file__Upload').style.display = 'none';
};

handleChangeFilter = () => {
    let check = document.getElementById("check__tipoAccion");
    let empresa = document.getElementById('empresa').value;
    let areaNegocio = document.getElementById('areaNegocio').value;
    let periodo = document.getElementById('periodo').value;

    if (check.checked) {
       document.getElementById("div__listaArchivos").style.display = "none";
        document.getElementById('search__archivos').style.display = 'none';
        document.getElementById('file__Upload').style.display = 'none';

        handleValidateData(empresa, areaNegocio, periodo) ?
            document.getElementById('file__Upload').style.display = 'inline-block' :
            document.getElementById('file__Upload').style.display = 'none';
    }
    else {
        document.getElementById("div__listaArchivos").style.display = "block";
        document.getElementById('search__archivos').style.display = 'none';
        document.getElementById('file__Upload').style.display = 'none';

        if (handleValidateData(empresa, areaNegocio, periodo)) {
            document.getElementById('search__archivos').style.display = 'inline-block';
        }
        else {
            document.getElementById('search__archivos').style.display = 'none';
        }
    }
};

handleValidateData = (empresa, areaNegocio, periodo) => {
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

handleFileChange = (e) => {
    const inputFile = document.getElementById('fileAsistencia');
    const selectRuta = document.getElementById('select__rutaFichero');

    let domain = handleGetDomain();
    let rutaFichero = selectRuta.value;
    let empresa = document.getElementById('empresa').value;
    let areaNegocio = document.getElementById('areaNegocio').value;
    let periodo = document.getElementById('periodo').value;
    let ficha = document.getElementById('ficha').value;
    let codigoRuta = selectRuta.options[selectRuta.options.selectedIndex].dataset.codigoruta;

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

handleChangeAction = () => {
    let check = document.getElementById("check__tipoAccion");
    let empresa = document.getElementById('empresa').value;
    let areaNegocio = document.getElementById('areaNegocio').value;
    let periodo = document.getElementById('periodo').value;

    document.getElementById("div__message").innerHTML = ``;
    document.getElementById("div__message").style.display = "none";

    if (check.checked) {
        document.getElementById("div__listaArchivos").style.display = "none";
        document.getElementById('search__archivos').style.display = 'none';
        document.getElementById('file__Upload').style.display = 'none';

        handleValidateData(empresa, areaNegocio, periodo) ?
            document.getElementById('file__Upload').style.display = 'inline-block' :
            document.getElementById('file__Upload').style.display = 'none';
    }
    else {
        document.getElementById("div__listaArchivos").style.display = "block";
        document.getElementById('search__archivos').style.display = 'none';
        document.getElementById('file__Upload').style.display = 'none';

        if (handleValidateData(empresa, areaNegocio, periodo)) {
            document.getElementById('search__archivos').style.display = 'inline-block';
            handleSearchArchivos();
        }
        else {
            document.getElementById('search__archivos').style.display = 'none';
        }
    }
};

handleSearchArchivos = () => {
    let domain = handleGetDomain();
    let empresa = document.getElementById('empresa').value;
    let areaNegocio = document.getElementById('areaNegocio').value;
    let periodo = document.getElementById('periodo').value;
    let ficha = document.getElementById('ficha').value;

    if (handleValidateData(empresa, areaNegocio, periodo)) {
        document.getElementById('search__archivos').style.display = 'inline-block';
        ajaxGetArchivosAsistencia(domain, empresa, areaNegocio, periodo, ficha);
    }
    else {
        document.getElementById('search__archivos').style.display = 'none';
    }

};

handleDownloadArchivo = (e) => {
    let domain = handleGetDomain();
    let nombre = e.dataset.nombre;
    let nombreReal = e.dataset.nombrereal;
    let ruta = e.dataset.ruta;

    window.location.href = `${domain}DownloadArchivo?ref1=${nombre}&ref2=${nombreReal}&ref3=${ruta}`;
};

handleDeleteArchivo = (e) => {
    let domain = handleGetDomain();
    let ruta = e.dataset.ruta;
    let rutaCodigo = e.dataset.rutacodigo;
    let nombre = e.dataset.nombre;
    let nombreReal = e.dataset.nombrereal;
    let codigoArchivo = e.dataset.archivo;

    ajaxDeleteArchivo(domain, ruta, rutaCodigo, nombre, nombreReal, codigoArchivo);
};



ajaxGetAreaNegocio = (prefix, empresa, areaNegocio) => {
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
            }
        };
    }
    catch{
        //
    }
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

ajaxGetArchivosAsistencia = (prefix, empresa, areaNegocio, periodo, ficha) => {
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

                if (request.ArchivoAsistencia.length) {
                    htmlArchivos +=
                        `
                        <div class="row ml-auto mr-auto" style="width:85%; text-align: left;">
                            <table style="width:100%">
                        `;
                    for (i = 0; i < request.ArchivoAsistencia.length; i++) {
                        htmlArchivos +=
                            `
                            <tr>
                                <td style="width:80%; padding: 10px; border-bottom: solid 1px #939393;">
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
                        </div>
                        `;

                    document.getElementById("div__message").innerHTML = ``;
                    document.getElementById("div__message").style.display = "none";
                }
                else {
                    document.getElementById("div__message").innerHTML =
                        `<span style="background-color: #fe5858; border-radius: 10px; padding: 10px 20px 10px 20px; color: #fff;">
                            No se encontraron archivos relacionados a la busqueda.
                        </span>
                        `;
                    document.getElementById("div__message").style.display = "block";
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
            }
        };
    }
    catch {
        //
    }
};

ajaxDeleteArchivo = (prefix, rutaFichero, codigoRuta, nombreArchivo, nombreReal, codigoArchivo) => {
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
                    document.getElementById("div__message").innerHTML = '';
                    document.getElementById("div__message").style.display = "none";

                    handleSearchArchivos();
                }, 2000);
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

setTimeout(() => {
    let domain = handleGetDomain();

    ajaxGetRutaFichero(domain);
}, 1000);