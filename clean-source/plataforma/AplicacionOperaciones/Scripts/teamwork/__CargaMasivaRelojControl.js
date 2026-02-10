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
                let sheetIncorporada = atob(fileUpload.dataset.namesheet);
                let sheetExits = false;
                let excelJsonObjLength = "0";
                let filaHeader = "";
                let totalRows = 0;


                [...workbook.SheetNames].map(x => {
                    if (sheetIncorporada === x) {
                        sheetExits = true;
                    }
                });

                if (sheetExits) {
                    let procesoAValidar = [];
                    let xml = "";
                    let templateColumn = "";
                    let date = new Date();
                    let transaccion = btoa(`${String(date.getDate())}${String(date.getMonth())}${String(date.getFullYear())}${String(date.getHours())}${String(date.getMinutes())}${String(date.getSeconds())}${String(date.getMilliseconds())}_TransaccionProcesoMasivo`);
                    console.log(atob(transaccion), date);
                    let rowObject = XLSX.utils.sheet_to_row_object_array(workbook.Sheets[sheetIncorporada]);
                    excelJsonObj = rowObject;
                    let error = false;

                    if (excelJsonObj.length > 0) {
                        document.getElementById('content__cargamasiva').innerHTML =
                            `
                            <img src="../Resources/cargamasivasvg/subida.svg" width="70" class="m-auto dspl-block" />
                            <h2 class="new-family-teamwork" style="font-weight: 500; text-align: center; width: 100%; color: rgb(70, 70, 70);">ETAPA 1</h2>
                            <h1 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; color: rgb(70, 70, 70); margin-bottom: -30px;">Subiendo Información al Sistema</h1>
                            <div class="container__loaderv2">
                                <div class="loader__v2" id="loader"></div>
                            </div>
                            <h1 class="new-family-teamwork" style="font-weight: 600; text-align: center; width: 100%; font-size: 60px; margin-bottom: -10px; color: #428bca;">${excelJsonObj.length} </h1>
                            <h1 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; color: rgb(70, 70, 70);">${(excelJsonObj.length > 1) ? "Registros" : "Registro"}</h1>
                            `;

                        switch (atob(fileUpload.dataset.cifrado)) {
                            case "51":
                                filaHeader = "2";
                                totalRows = excelJsonObj.length + Number(filaHeader);
                                break;
                            case "53":
                                filaHeader = "6";
                                totalRows = excelJsonObj.length + 1;
                                break;
                        }

                        setTimeout(() => {
                            for (let h = Number(filaHeader) + 1; h <= totalRows; h++) {
                                templateColumn = "";

                                xml = "<" + atob(fileUpload.dataset.nodopadre) + "><" + atob(fileUpload.dataset.nodohijo) + " ";

                                for (let j = 0; j < atob(fileUpload.dataset.columnas).split(':').length; j++) {
                                    columna = (Number(atob(fileUpload.dataset.columnas).split(':')[j])) ? handleConvertLetterExcel(Number(atob(fileUpload.dataset.columnas).split(':')[j])) : atob(fileUpload.dataset.columnas).split(':')[j];

                                    if (workbook.Sheets[sheetIncorporada][columna + filaHeader] !== undefined) {
                                        if (workbook.Sheets[sheetIncorporada][columna + '' + String(h)] !== undefined) {
                                            xml = xml + handleEliminarDiacriticos(workbook.Sheets[sheetIncorporada][columna + filaHeader].v).replace(/\s+/g, '')  + "='" + workbook.Sheets[sheetIncorporada][columna + '' + String(h)].v + "' ";
                                            if (j === 0) {
                                                templateColumn = templateColumn + "" + handleEliminarDiacriticos(workbook.Sheets[sheetIncorporada][columna + filaHeader].v).replace(/\s+/g, '') + "";
                                            }
                                            else {
                                                templateColumn = templateColumn + "-" + handleEliminarDiacriticos(workbook.Sheets[sheetIncorporada][columna + filaHeader].v).replace(/\s+/g, '') + "";
                                            }
                                        }
                                        else {
                                            xml = xml + handleEliminarDiacriticos(workbook.Sheets[sheetIncorporada][columna + "2"].v) + "='' ";

                                            if (j === 0) {
                                                templateColumn = templateColumn + "" + handleEliminarDiacriticos(workbook.Sheets[sheetIncorporada][columna + filaHeader].v).replace(/\s+/g, '') + "";
                                            }
                                            else {
                                                templateColumn = templateColumn + "-" + handleEliminarDiacriticos(workbook.Sheets[sheetIncorporada][columna + filaHeader].v).replace(/\s+/g, '') + "";
                                            }
                                        }
                                    }
                                }
                                xml = xml + " fila='" + (h) + "' /></" + atob(fileUpload.dataset.nodopadre) + ">";

                                /** AQUI EVENTO DE GUARDADO DE INFORMACION MASIVA */
                                if (sessionStorage.getItem("ApplicationError") === "N") {
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
                            }

                            if (!error) {
                                __ajaxPlantillaValidationData(prefix, transaccion, fileUpload.dataset.cifrado, excelJsonObj, fileUpload.dataset.templatedownload)
                            }
                        }, 1000);

                    }
                    else {
                        if (parseInt(excelJsonObjLength) === 0) {
                            document.getElementById('messagePlataforma').innerHTML =
                                `Notificación de la plataforma:
                                <br/>
                                La plantilla que esta tratando de subir, no contiene datos de trabajadores en la hoja Carga Masiva.
                                `;
                        }
                    }
                }
                else {
                    document.getElementById('messagePlataforma').innerHTML =
                        `Notificación de la plataforma:
                         <br/>
                         La plantilla que esta tratando de subir, no contiene la hoja llamada 'Carga Masiva', favor verificar que exista dicha hoja dentro del excel de carga masiva.`;
                }
            };
            reader.readAsBinaryString(input.files[0]);
        };
    }

})();