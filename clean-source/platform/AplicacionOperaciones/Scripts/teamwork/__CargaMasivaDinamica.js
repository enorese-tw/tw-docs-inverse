(function () {
    var app = angular.module('myApp', []);
    app.controller('MyController', ['$scope', myController]);

    /** OBTENCION DINAMICA DE URL DE APLICACION PARA ACCESO A METODOS INTERNOS */

    var prefixDomain = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2];
    var prefix = "";

    if (window.location.href.split('/')[2].indexOf(atob("bG9jYWxob3N0")) !== -1) {
        if ((window.location.href.split('/').length - 1) === 3) {
            prefix = prefix + "/" + window.location.href.split('/')[window.location.href.split('/').length - 1] + "/";
        } else {
            prefix = prefix + "/" + window.location.href.split('/')[3] + "/";
        }
    }

    if (window.location.href.split('/')[2].indexOf(atob('bG9jYWxob3N0')) === -1) {
        prefix = prefix + "/" + window.location.href.split('/')[3] + "/";

        if ((window.location.href.split('/').length - 1) === 4) {
            prefix = prefix + "/" + window.location.href.split('/')[window.location.href.split('/').length - 1] + "/";
        } else {
            prefix = prefix + "/" + window.location.href.split('/')[4] + "/";
        }
    }

    /** END OBTENCION DINAMICA DE URL DE APLICACION PARA ACCESO A METODOS INTERNOS */

    let excelJsonObj = [];
    function myController($scope) {
        $scope.uploadExcel = function () {
            const myFile = document.getElementById('file');
            const input = myFile;
            const reader = new FileReader();
            reader.onload = function () {
                const fileData = reader.result;
                const workbook = XLSX.read(fileData, { type: 'binary' });
                const fileUpload = document.getElementById("fileUpload");
                const sheetIncorporada = atob(fileUpload.dataset.namesheet);
                let sheetExits = false;
                let excelJsonObjLength = "0";

                [...workbook.SheetNames].map(x => {
                    if (sheetIncorporada === x) {
                        sheetExits = true;
                    }
                });

                if (sheetExits) {
                    let procesoAValidar = [];
                    let xml = "";
                    let xmlDinamic = "";
                    let columna = "";
                    let templateColumn = "";
                    let date = new Date();
                    let transaccion = btoa(`${String(date.getDate())}${String(date.getMonth())}${String(date.getFullYear())}${String(date.getHours())}${String(date.getMinutes())}${String(date.getSeconds())}${String(date.getMilliseconds())}_TransaccionProcesoMasivo`);
                    console.log(atob(transaccion), date);
                    let rowObject = XLSX.utils.sheet_to_row_object_array(workbook.Sheets["Carga Masiva"]);
                    excelJsonObj = rowObject;
                    let error = false;
                    let periodo = fileUpload.dataset.fecha;
                    let countColumn = 0;
                    let rowHeader = 0;
                    let rowInit = 0;
                    let countRegistros = 0;
                    let validate = false;

                    switch (atob(fileUpload.dataset.cifrado)) {
                        case "49":
                            countColumn = atob(fileUpload.dataset.columnas).split(':').length - 1;
                            rowInit = 3;
                            break;
                        default:
                            countColumn = atob(fileUpload.dataset.columnas).split(':').length;
                            rowInit = 2;
                    }

                    if (atob(fileUpload.dataset.cifrado) === '49') {
                        if (periodo !== null && periodo !== '' && periodo !== undefined) {
                            validate = true;
                        }
                    }
                    else {
                        validate = true;
                    }


                    rowHeader = rowInit - 1;

                    if (validate) {
                        if (excelJsonObj.length > 0) {
                            switch (atob(fileUpload.dataset.cifrado)) {
                                case "49":
                                    countRegistros = excelJsonObj.length - 1;
                                    break;
                                default:
                                    countRegistros = excelJsonObj.length;
                                    break;
                            }



                            document.getElementById('content__cargamasiva').innerHTML =
                                `
                                <img src="../Resources/cargamasivasvg/subida.svg" width="70" class="m-auto dspl-block" />
                                <h2 class="new-family-teamwork" style="font-weight: 500; text-align: center; width: 100%; color: rgb(70, 70, 70);">ETAPA 1</h2>
                                <h1 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; color: rgb(70, 70, 70); margin-bottom: -30px;">Subiendo Información al Sistema</h1>
                                <div class="container__loaderv2">
                                    <div class="loader__v2" id="loader"></div>
                                </div>
                                <h1 class="new-family-teamwork" style="font-weight: 600; text-align: center; width: 100%; font-size: 60px; margin-bottom: -10px; color: #428bca;">${countRegistros} </h1>
                                <h1 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; color: rgb(70, 70, 70);">${(countRegistros > 1) ? "Trabajadores" : "Trabajador"}</h1>
                                `;

                            setTimeout(() => {
                                for (let h = rowInit; h <= excelJsonObj.length + 1; h++) {
                                    templateColumn = "";

                                    xml = "<" + atob(fileUpload.dataset.nodopadre) + "><" + atob(fileUpload.dataset.nodohijo) + " ";

                                    for (let j = 0; j < atob(fileUpload.dataset.columnas).split(':').length; j++) {
                                        columna = (Number(atob(fileUpload.dataset.columnas).split(':')[j])) ? handleConvertLetterExcel(Number(atob(fileUpload.dataset.columnas).split(':')[j])) : atob(fileUpload.dataset.columnas).split(':')[j];

                                        if (j === atob(fileUpload.dataset.columnas).split(':').length - 1 && atob(fileUpload.dataset.cifrado) === '49') {
                                            xml = xml + "periodo='" + periodo.substr(0, 7) + "' ";

                                            if (j === 0) {
                                                templateColumn = templateColumn + "periodo";
                                            }
                                            else {
                                                templateColumn = templateColumn + "-periodo";
                                            }

                                        }
                                        else {
                                            if (workbook.Sheets[sheetIncorporada][columna + rowHeader] !== undefined) {
                                                if (workbook.Sheets[sheetIncorporada][columna + '' + String(h)] !== undefined) {
                                                    xml = xml + workbook.Sheets[sheetIncorporada][columna + rowHeader].v + "='" + workbook.Sheets[sheetIncorporada][columna + '' + String(h)].v + "' ";
                                                    if (j === 0) {
                                                        templateColumn = templateColumn + "" + workbook.Sheets[sheetIncorporada][columna + rowHeader].v + "";
                                                    } else {
                                                        templateColumn = templateColumn + "-" + workbook.Sheets[sheetIncorporada][columna + rowHeader].v + "";
                                                    }

                                                }
                                                else {
                                                    xml = xml + workbook.Sheets[sheetIncorporada][columna + rowHeader].v +
                                                        "='' ";

                                                    if (j === 0) {
                                                        templateColumn = templateColumn + "" + workbook.Sheets[sheetIncorporada][columna + rowHeader].v + "";
                                                    } else {
                                                        templateColumn = templateColumn + "-" + workbook.Sheets[sheetIncorporada][columna + rowHeader].v + "";
                                                    }
                                                }
                                            }
                                        }

                                    }

                                    xml = xml + " fila='" + (h - 1) + "' /></" + atob(fileUpload.dataset.nodopadre) + ">";


                                    /**COLUMNAS DINAMICAS*/
                                    if (diasCargaMasiva !== "") {
                                        for (let z = 0; z < diasCargaMasiva.length; z++) {
                                            xmlDinamic = "<" + atob(fileUpload.dataset.nodopadre) + "><" + atob(fileUpload.dataset.nodohijo) + " ";

                                            columna = (Number(diasCargaMasiva[z])) ? handleConvertLetterExcel(Number(diasCargaMasiva[z]) + countColumn) : diasCargaMasiva[z];

                                            if (workbook.Sheets[sheetIncorporada][columna + rowHeader] !== undefined) {
                                                if (workbook.Sheets[sheetIncorporada][columna + '' + String(h)] !== undefined) {
                                                    xmlDinamic = xmlDinamic +
                                                        "header='" + (workbook.Sheets[sheetIncorporada][columna + rowHeader].v < 10 ? "0" + workbook.Sheets[sheetIncorporada][columna + rowHeader].v : workbook.Sheets[sheetIncorporada][columna + rowHeader].v) + "-" + periodo.substr(5, 2) + "-" + periodo.substr(0, 4) + "' " +
                                                        "body='" + workbook.Sheets[sheetIncorporada][columna + '' + String(h)].v + "' " +
                                                        "type='Asistencia' ";

                                                    xmlDinamic = xmlDinamic + " fila='" + (h - 1) + "' /></" + atob(fileUpload.dataset.nodopadre) + ">";

                                                    try {
                                                        let xhrDinamico = new XMLHttpRequest();
                                                        let __form = new FormData();
                                                        __form.append("data", xmlDinamic);
                                                        __form.append("codigoTransaction", transaccion);
                                                        __form.append("templateColumn", "");
                                                        __form.append("plantillaMasiva", fileUpload.dataset.cifrado);

                                                        xhrDinamico.open('POST', `${prefix}PlantillaSubirDinamica`, false);
                                                        xhrDinamico.overrideMimeType('text/xml; charset=UTF-8');
                                                        xhrDinamico.setRequestHeader('X-Requested-With', 'XMLHttpRequest');

                                                        xhrDinamico.send(__form);

                                                        xhrDinamico.onload = () => {
                                                            if (xhrDinamico.status >= 200 && xhrDinamico.status < 300) {
                                                                const request = JSON.parse(xhrDinamico.responseText);

                                                                if (request[0].Code !== "200") {
                                                                    document.getElementById('messagePlataforma').innerHTML =
                                                                        `
                                                                Notificación de la plataforma:
                                                                <br/>
                                                                ${request[0].Message}
                                                                `;
                                                                    error = false;
                                                                }
                                                                __form = null;
                                                            }
                                                        };
                                                    }
                                                    catch {
                                                        //
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    /**DINAMICA BONOS */
                                    if (arrayBonos.length > 0) {
                                        for (let z = 0; z < arrayBonos.length; z++) {
                                            xmlDinamic = "<" + atob(fileUpload.dataset.nodopadre) + "><" + atob(fileUpload.dataset.nodohijo) + " ";

                                            columna = (Number(z + 1)) ? handleConvertLetterExcel(Number(z + 1) + countColumn + diasCargaMasiva.length) : z;

                                            if (workbook.Sheets[sheetIncorporada][columna + rowHeader] !== undefined) {
                                                if (workbook.Sheets[sheetIncorporada][columna + '' + String(h)] !== undefined) {
                                                    xmlDinamic = xmlDinamic +
                                                        "header='" + arrayBonos[z] + "' " +
                                                        "body='" + workbook.Sheets[sheetIncorporada][columna + '' + String(h)].v + "' " +
                                                        "type='Bono' ";

                                                    xmlDinamic = xmlDinamic + " fila='" + (h - 1) + "' /></" + atob(fileUpload.dataset.nodopadre) + ">";

                                                    try {
                                                        let xhrDinamico = new XMLHttpRequest();
                                                        let __form = new FormData();
                                                        __form.append("data", xmlDinamic);
                                                        __form.append("codigoTransaction", transaccion);
                                                        __form.append("templateColumn", "");
                                                        __form.append("plantillaMasiva", fileUpload.dataset.cifrado);

                                                        xhrDinamico.open('POST', `${prefix}PlantillaSubirDinamica`, false);
                                                        xhrDinamico.overrideMimeType('text/xml; charset=UTF-8');
                                                        xhrDinamico.setRequestHeader('X-Requested-With', 'XMLHttpRequest');

                                                        xhrDinamico.send(__form);

                                                        xhrDinamico.onload = () => {
                                                            if (xhrDinamico.status >= 200 && xhrDinamico.status < 300) {
                                                                const request = JSON.parse(xhrDinamico.responseText);

                                                                if (request[0].Code !== "200") {
                                                                    document.getElementById('messagePlataforma').innerHTML =
                                                                        `
                                                            Notificación de la plataforma:
                                                            <br/>
                                                            ${request[0].Message}
                                                            `;
                                                                    error = false;
                                                                }
                                                                __form = null;
                                                            }
                                                        };
                                                    }
                                                    catch {
                                                        //
                                                    }
                                                }
                                            }
                                        }
                                    }


                                    try {
                                        let xhr = new XMLHttpRequest();
                                        let __form = new FormData();
                                        __form.append("data", xml);
                                        __form.append("codigoTransaction", transaccion);
                                        __form.append("templateColumn", templateColumn);
                                        __form.append("plantillaMasiva", fileUpload.dataset.cifrado);

                                        xhr.open('POST', `${prefix}PlantillaSubir`, false);
                                        xhr.overrideMimeType('text/xml; charset=UTF-8');
                                        xhr.setRequestHeader('X-Requested-With', 'XMLHttpRequest');

                                        xhr.send(__form);

                                        xhr.onload = () => {
                                            if (xhr.status >= 200 && xhr.status < 300) {
                                                const request = JSON.parse(xhr.responseText);

                                                if (request[0].Code !== "200") {
                                                    document.getElementById('messagePlataforma').innerHTML =
                                                        `
                                                    Notificación de la plataforma:
                                                    <br/>
                                                    ${request[0].Message}
                                                    `;
                                                    error = false;
                                                }

                                                __form = null;
                                            }
                                            else {
                                                document.getElementById('content__cargamasiva').innerHTML =
                                                    `
                                                <img src="../Resources/cargamasivasvg/triste.svg" width="70" class="m-auto dspl-block" />
                                                <h1 class="new-family-teamwork" style="font-weight: 400; text-align: center; width: 100%; color: rgb(70, 70, 70);">UPS! Ha ocurrido algo inesperado</h1>
                                                <h2 class="new-family-teamwork m-auto" style="font-weight: 100; text-align: center; width: 80%;">
                                                    La carga no pudo ser procesada, ni validada, ni subida al sisteama, intentelo nuevamente, si el problema persiste favor levantar ticket mediante mesa de ayuda para que el area de sistemas pueda resolver el problema.
                                                </h2>
                                                <h1 class="new-family-teamwork ml-auto mr-auto mt-30" style="font-weight: 100; text-align: center; width: 80%;">Gracias por su comprensión!!!!</h1>
                                                <div style="width: 100%;">
                                                    <div onclick="handleResetCargaMasiva()" class="btn btn-teamwork ml-auto mr-auto mt-30 new-family-teamwork pdt-10 pdb-10 cursor-pointer" style="font-weight: 100; color: #fff; font-size: 20px; display: block;">Entiendo! Volver a Solicitar</div>
                                                </div>
                                                `;
                                            }
                                        };
                                    }
                                    catch {
                                        document.getElementById('content__cargamasiva').innerHTML =
                                            `
                                        <img src="../Resources/cargamasivasvg/triste.svg" width="70" class="m-auto dspl-block" />
                                        <h1 class="new-family-teamwork" style="font-weight: 400; text-align: center; width: 100%; color: rgb(70, 70, 70);">UPS! Ha ocurrido algo inesperado</h1>
                                        <h2 class="new-family-teamwork m-auto" style="font-weight: 100; text-align: center; width: 80%;">
                                            La carga no pudo ser procesada, ni validada, ni subida al sisteama, intentelo nuevamente, si el problema persiste favor levantar ticket mediante mesa de ayuda para que el area de sistemas pueda resolver el problema.
                                        </h2>
                                        <h1 class="new-family-teamwork ml-auto mr-auto mt-30" style="font-weight: 100; text-align: center; width: 80%;">Gracias por su comprensión!!!!</h1>
                                        <div style="width: 100%;">
                                            <div onclick="handleResetCargaMasiva()" class="btn btn-teamwork ml-auto mr-auto mt-30 new-family-teamwork pdt-10 pdb-10 cursor-pointer" style="font-weight: 100; color: #fff; font-size: 20px; display: block;">Entiendo! Volver a Solicitar</div>
                                        </div>
                                        `;
                                    }


                                    /** AQUI EVENTO DE GUARDADO DE INFORMACION MASIVA */

                                    if (sessionStorage.getItem("ApplicationError") === "N") {
                                        if (atob(fileUpload.dataset.cifrado) === "1" || atob(fileUpload.dataset.cifrado) === "2" || atob(fileUpload.dataset.cifrado) === "41" || atob(fileUpload.dataset.cifrado) === "45" || atob(fileUpload.dataset.cifrado) === "47") {
                                            try {
                                                let xhr = new XMLHttpRequest();
                                                let __form = new FormData();
                                                __form.append("data", xml);
                                                __form.append("codigoTransaction", transaccion);
                                                __form.append("templateColumn", templateColumn);
                                                __form.append("plantillaMasiva", fileUpload.dataset.cifrado);

                                                xhr.open('POST', `${prefix}PlantillaSubir`, false);
                                                xhr.overrideMimeType('text/xml; charset=UTF-8');
                                                xhr.setRequestHeader('X-Requested-With', 'XMLHttpRequest');

                                                xhr.send(__form);

                                                xhr.onload = () => {
                                                    if (xhr.status >= 200 && xhr.status < 300) {
                                                        const request = JSON.parse(xhr.responseText);

                                                        if (request[0].Code !== "200") {
                                                            document.getElementById('messagePlataforma').innerHTML =
                                                                `
                                                                Notificación de la plataforma:
                                                                <br/>
                                                                ${request[0].Message}
                                                                `;
                                                            error = false;
                                                        }

                                                        __form = null;
                                                    }
                                                    else {
                                                        document.getElementById('content__cargamasiva').innerHTML =
                                                            `
                                                            <img src="../Resources/cargamasivasvg/triste.svg" width="70" class="m-auto dspl-block" />
                                                            <h1 class="new-family-teamwork" style="font-weight: 400; text-align: center; width: 100%; color: rgb(70, 70, 70);">UPS! Ha ocurrido algo inesperado</h1>
                                                            <h2 class="new-family-teamwork m-auto" style="font-weight: 100; text-align: center; width: 80%;">
                                                                La carga no pudo ser procesada, ni validada, ni subida al sisteama, intentelo nuevamente, si el problema persiste favor levantar ticket mediante mesa de ayuda para que el area de sistemas pueda resolver el problema.
                                                            </h2>
                                                            <h1 class="new-family-teamwork ml-auto mr-auto mt-30" style="font-weight: 100; text-align: center; width: 80%;">Gracias por su comprensión!!!!</h1>
                                                            <div style="width: 100%;">
                                                                <div onclick="handleResetCargaMasiva()" class="btn btn-teamwork ml-auto mr-auto mt-30 new-family-teamwork pdt-10 pdb-10 cursor-pointer" style="font-weight: 100; color: #fff; font-size: 20px; display: block;">Entiendo! Volver a Solicitar</div>
                                                            </div>
                                                            `;
                                                    }
                                                };
                                            }
                                            catch {
                                                document.getElementById('content__cargamasiva').innerHTML =
                                                    `
                                                    <img src="../Resources/cargamasivasvg/triste.svg" width="70" class="m-auto dspl-block" />
                                                    <h1 class="new-family-teamwork" style="font-weight: 400; text-align: center; width: 100%; color: rgb(70, 70, 70);">UPS! Ha ocurrido algo inesperado</h1>
                                                    <h2 class="new-family-teamwork m-auto" style="font-weight: 100; text-align: center; width: 80%;">
                                                        La carga no pudo ser procesada, ni validada, ni subida al sisteama, intentelo nuevamente, si el problema persiste favor levantar ticket mediante mesa de ayuda para que el area de sistemas pueda resolver el problema.
                                                    </h2>
                                                    <h1 class="new-family-teamwork ml-auto mr-auto mt-30" style="font-weight: 100; text-align: center; width: 80%;">Gracias por su comprensión!!!!</h1>
                                                    <div style="width: 100%;">
                                                       <div onclick="handleResetCargaMasiva()" class="btn btn-teamwork ml-auto mr-auto mt-30 new-family-teamwork pdt-10 pdb-10 cursor-pointer" style="font-weight: 100; color: #fff; font-size: 20px; display: block;">Entiendo! Volver a Solicitar</div>
                                                    </div>
                                                    `;
                                            }
                                        }
                                        else {
                                            ajaxCargaMasiva(prefixDomain + prefix, xml, transaccion, $("#fileUpload").attr("data-cifrado"), parseInt(excelJsonObj.length).toString(), procesoAValidar, templateColumn);
                                        }

                                    }
                                }
                            
                                if (!error) {
                                    __ajaxPlantillaValidationData(prefix, transaccion, fileUpload.dataset.cifrado, excelJsonObj, fileUpload.dataset.templatedownload);
                                }
                            }, 500);

                        }
                        else {
                            if (parseInt(excelJsonObjLength) === 0) {
                                document.getElementById('messagePlataforma').innerHTML =
                                    `
                                    Notificación de la plataforma:
                                    <br/>
                                    La plantilla que esta tratando de subir, no contiene datos de trabajadores en la hoja Carga Masiva.
                                    `;
                            }
                        }
                    }
                    else {
                        document.getElementById('messagePlataforma').innerHTML =
                            `
                            Notificación de la plataforma:
                            <br/>
                            Debe seleccionar una periodo para realizar la Carga Masiva de inasistencia.
                            `;
                    }
                }
                else {
                    document.getElementById('messagePlataforma').innerHTML =
                        `
                        Notificación de la plataforma:
                        <br/>
                        La plantilla que esta tratando de subir, no contiene la hoja llamada 'Carga Masiva', favor verificar que exista dicha hoja dentro del excel de carga masiva.
                        `;
                }
            };
            reader.readAsBinaryString(input.files[0]);
        };
    }

})();