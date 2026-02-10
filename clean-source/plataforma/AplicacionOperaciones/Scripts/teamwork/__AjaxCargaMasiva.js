
let method = "";
let downloadError = "";

__ajaxPlantillaValidationData = (prefix, transaccion, file, objectsExcel, templateError) => {
    try
    {

        let xhr = new XMLHttpRequest();
        let __form = new FormData();
        __form.append("codigoTransaction", transaccion);

        switch (atob(file)) {
            case "1":
                method = "PlantillaValidarCargaContrato";
                break;
            case "2":
                method = "PlantillaValidarCargaRenovacion";
                break;
            case "41":
                method = "PlantillaValidarCargaAsistencia";
                break;
            case "45":
                method = "PlantillaValidarCargaAsistencia";
                break;
            case "47":
                method = "PlantillaValidarCargaHorasExtras";
                break;
            case "49":
                method = "PlantillaValidarCargaAsistenciaRetail";
                break;
            case "51":
                method = "PlantillaValidarCargaAsistenciaRelojControlGeoVictoria";
                break;
            case "53":
                method = "PlantillaValidarCargaAsistenciaRelojControl";
                break;
            case "55":
                method = "PlantillaValidarCargaBono";
                break;
            case "37":
                method = "PlantillaValidarCargaBajas";
                break;
        }

        document.getElementById('content__cargamasiva').innerHTML =
            `
            <img src="../Resources/cargamasivasvg/validacion.svg" width="70" class="m-auto dspl-block" />
            <h2 class="new-family-teamwork" style="font-weight: 500; text-align: center; width: 100%; color: rgb(70, 70, 70);">ETAPA 2</h2>
            <h1 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; color: rgb(70, 70, 70); margin-bottom: -30px;">Estamos Validando la Información de la Carga</h1>
            <div class="container__loaderv2">
                <div class="loader__v2" id="loader"></div>
            </div>
            <h1 class="new-family-teamwork" style="font-weight: 600; text-align: center; width: 100%; font-size: 60px; margin-bottom: -10px; color: #428bca;">${atob(file) === '49' ? objectsExcel.length - 1 : objectsExcel.length}</h1>
            <h1 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; color: rgb(70, 70, 70);">
                ${(atob(file) === '41' || atob(file) === '45') ?
                    ((objectsExcel.length > 1) ? "Registros" : "Registro") :
                    ((objectsExcel.length > 1) ? "Trabajadores" : "Trabajador")}
            </h1>
        `;

        xhr.open('POST', `${prefix}${method}`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                const request = JSON.parse(xhr.responseText);

                if (request[0] !== undefined) {
                    if (Number(request[0].Procesados) > 0) {

                        switch (atob(file)) {
                            case "1":
                                __ajaxPlantillaCrearActualizar(prefix, transaccion, file, objectsExcel, request[0].Errores, request[0].Procesados, templateError);
                                break;
                            case "2":
                                __ajaxPlantillaCrearRenovacion(prefix, transaccion, file, objectsExcel, request[0].Errores, request[0].Procesados, templateError);
                                break;
                            case "41":
                                __ajaxPlantillaCrearAsistencia(prefix, transaccion, file, objectsExcel, request[0].Errores, request[0].Procesados, templateError);
                                break;
                            case "45":
                                __ajaxPlantillaCrearAsistencia(prefix, transaccion, file, objectsExcel, request[0].Errores, request[0].Procesados, templateError);
                                break;
                            case "47":
                                __ajaxPlantillaCrearHorasExtras(prefix, transaccion, file, objectsExcel, request[0].Errores, request[0].Procesados, templateError);
                                break;
                            case "49":
                                __ajaxPlantillaCrearAsistenciaRetail(prefix, transaccion, file, objectsExcel, request[0].Errores, request[0].Procesados, templateError);
                                break;
                            case "51":
                                __ajaxPlantillaCrearAsistenciaRelojControlGeoVictoria(prefix, transaccion, file, objectsExcel, request[0].Errores, request[0].Procesados, templateError);
                                break;
                            case "53":
                                __ajaxPlantillaCrearAsistenciaRelojControl(prefix, transaccion, file, objectsExcel, request[0].Errores, request[0].Procesados, templateError);
                                break;
                            case "55":
                                __ajaxPlantillaCrearBonos(prefix, transaccion, file, objectsExcel, request[0].Errores, request[0].Procesados, templateError);
                                break;
                            case "37":
                                __ajaxPlantillaCrearBajas(prefix, transaccion, file, objectsExcel, request[0].Errores, request[0].Procesados, templateError);
                                break;
                        }

                    }
                    else {
                        switch (atob(file)) {
                            case "51":
                                downloadError = "DownloadFileCargaMasivaRelojControl";
                                break;
                            case "53":
                                downloadError = "DownloadFileCargaMasivaRelojControl";
                                break;
                            default:
                                downloadError = "DownloadFileCargaMasiva";
                                break;
                        }

                        document.getElementById('content__cargamasiva').innerHTML =
                            `
                        <img src="../Resources/cargamasivasvg/rechazado.svg" width="70" class="m-auto dspl-block" />
                        <h1 class="new-family-teamwork" style="font-weight: 400; text-align: center; width: 100%; color: #f00;">PROCESO RECHAZADO</h1>
                        <h2 class="new-family-teamwork m-auto" style="font-weight: 100; text-align: center; width: 80%;">
                            La carga que esta subiendo ha sido rechazada de manera integra, debido a esto es que se le invita a corregir la solicitud y volver a subirla al sistema.
                        </h2>
                        <h1 class="new-family-teamwork ml-auto mr-auto mt-30" style="font-weight: 100; text-align: center; width: 80%;">La puede descargar a continuación</h1>  
                        <div class="mt-20" style="width: 100%;">
                        <a href="${prefix}/${downloadError}?template=${templateError}&codigotransaccion=${transaccion}" class="btn btn-danger new-family-teamwork dspl-block m-auto" style="font-weight: 100; font-size: 20px;">
                            Descargar Solicitud Con Errores!
                        </a>
                        </div>
                        <h3 class="new-family-teamwork ml-auto mr-auto mt-30" style="font-weight: 100; text-align: center; width: 80%;">
                            Recuerde que una vez que haya corregido los datos con errores, debe eliminar la columna donde se muestra la observacion.
                        </h3>
                        <div style="width: 100%;">
                        <div onclick="handleResetCargaMasiva()" class="btn btn-teamwork ml-auto mr-auto mt-30 new-family-teamwork pdt-10 pdb-10 cursor-pointer" style="font-weight: 100; color: #fff; font-size: 20px; display: block;">Entiendo! Volver a Solicitar</div>
                        </div>
                        `;
                    }
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
                
            }
            else
            {
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
    } catch (e) {
        document.getElementById('content__cargamasiva').innerHTML =
            `
            <img src="../Resources/cargamasivasvg/triste.svg" width="70" class="m-auto dspl-block" />
            <h1 class="new-family-teamwork" style="font-weight: 400; text-align: center; width: 100%; color: #f00;">UPS! Ha ocurrido algo inesperado</h1>
            <h2 class="new-family-teamwork m-auto" style="font-weight: 100; text-align: center; width: 80%;">
                La carga no pudo ser procesada, ni validada, ni subida al sisteama, intentelo nuevamente, si el problema persiste favor levantar ticket mediante mesa de ayuda para que el area de sistemas pueda resolver el problema.
            </h2>
            <h1 class="new-family-teamwork ml-auto mr-auto mt-30" style="font-weight: 100; text-align: center; width: 80%;">Gracias por su comprensión!!!!</h1>
            `;
    }
    
};

__ajaxPlantillaCrearActualizar = (prefix, transaccion, file, objectsExcel, errores, procesados, templateError) => {
    try {
        let xhr = new XMLHttpRequest();
        let __form = new FormData();
        __form.append("codigoTransaction", transaccion);
        

        document.getElementById('content__cargamasiva').innerHTML =
            `
            <img src="../Resources/cargamasivasvg/lista.svg" width="70" class="m-auto dspl-block" />
            <h2 class="new-family-teamwork" style="font-weight: 500; text-align: center; width: 100%; color: rgb(70, 70, 70);">ETAPA 3</h2>
            <h1 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; margin-bottom: -30px;">Estamos Actualizando/Creando la Ficha Personal ${(procesados > 1) ? "de los Trabajadores" : "del Trabajador"}</h1>
                <div class="container__loaderv2">
                    <div class="loader__v2" id="loader"></div>
                </div>
            <h1 class="new-family-teamwork" style="font-weight: 600; text-align: center; width: 100%; font-size: 60px; margin-bottom: -10px; color: darkgreen;">${procesados} </h1>
            <h1 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; color: darkgreen;">${(procesados > 1) ? "Trabajadores correctos" : "Trabajador correcto"}</h1>
            <h3 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; margin-bottom: -10px; color: #f00;">${(errores > 0) ? errores : "" }</h3>
            <h4 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; color: #f00;">${(errores > 0) ? (errores > 1) ? "Trabajadores con problemas en sus datos" : "Trabajador con problema en sus datos" : ""}</h4>
        `;

        xhr.open('POST', `${prefix}PlantillaActualizaCreaFicha`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300)
            {

                __ajaxPlantillaCrearContrato(prefix, transaccion, file, objectsExcel, errores, procesados, templateError);
            }
            else
            {
                document.getElementById('content__cargamasiva').innerHTML =
                    `
                    <img src="../Resources/cargamasivasvg/triste.svg" width="70" class="m-auto dspl-block" />
                    <h1 class="new-family-teamwork" style="font-weight: 400; text-align: center; width: 100%; color: #f00;">UPS! Ha ocurrido algo inesperado</h1>
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
    } catch {
        document.getElementById('content__cargamasiva').innerHTML =
            `
            <img src="../Resources/cargamasivasvg/triste.svg" width="70" class="m-auto dspl-block" />
            <h1 class="new-family-teamwork" style="font-weight: 400; text-align: center; width: 100%; color: #f00;">UPS! Ha ocurrido algo inesperado</h1>
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

__ajaxPlantillaCrearRenovacion = (prefix, transaccion, file, objectsExcel, errores, procesados, templateError) => {
    try {
        let xhr = new XMLHttpRequest();
        let __form = new FormData();
        __form.append("codigoTransaction", transaccion);
        
        document.getElementById('content__cargamasiva').innerHTML =
            `
            <img src="../Resources/cargamasivasvg/lista.svg" width="70" class="m-auto dspl-block" />
            <h2 class="new-family-teamwork" style="font-weight: 500; text-align: center; width: 100%; color: rgb(70, 70, 70);">ETAPA 3</h2>
            <h1 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; margin-bottom: -30px;">Estamos Creando ${(procesados > 1) ? "las Renovaciones de los Trabajadores" : "la renovación del Trabajador"}</h1>
                <div class="container__loaderv2">
                    <div class="loader__v2" id="loader"></div>
                </div>
            <h1 class="new-family-teamwork" style="font-weight: 600; text-align: center; width: 100%; font-size: 60px; margin-bottom: -10px; color: darkgreen;">${procesados} </h1>
            <h1 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; color: darkgreen;">${(procesados > 1) ? "Trabajadores correctos" : "Trabajador correcto"}</h1>
            <h3 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; margin-bottom: -10px; color: #f00;">${(errores > 0) ? errores : ""}</h3>
            <h4 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; color: #f00;">${(errores > 0) ? (errores > 1) ? "Trabajadores con problemas en sus datos" : "Trabajador con problema en sus datos" : ""}</h4>
        `;

        xhr.open('POST', `${prefix}PlantillaCrearRenovacion`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                __ajaxPlantillaEmitir(prefix, transaccion, file, objectsExcel, errores, procesados, "Renovacion", templateError);
            }
            else {
                document.getElementById('content__cargamasiva').innerHTML =
                    `
                    <img src="../Resources/cargamasivasvg/triste.svg" width="70" class="m-auto dspl-block" />
                    <h1 class="new-family-teamwork" style="font-weight: 400; text-align: center; width: 100%; color: #f00;">UPS! Ha ocurrido algo inesperado</h1>
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
    } catch {
        document.getElementById('content__cargamasiva').innerHTML =
            `
            <img src="../Resources/cargamasivasvg/triste.svg" width="70" class="m-auto dspl-block" />
            <h1 class="new-family-teamwork" style="font-weight: 400; text-align: center; width: 100%; color: #f00;">UPS! Ha ocurrido algo inesperado</h1>
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

__ajaxPlantillaCrearBajas = (prefix, transaccion, file, objectsExcel, errores, procesados, templateError) => {
    try {
        let xhr = new XMLHttpRequest();
        let __form = new FormData();
        __form.append("codigoTransaction", transaccion);
        
        document.getElementById('content__cargamasiva').innerHTML =
            `
            <img src="../Resources/cargamasivasvg/lista.svg" width="70" class="m-auto dspl-block" />
            <h2 class="new-family-teamwork" style="font-weight: 500; text-align: center; width: 100%; color: rgb(70, 70, 70);">ETAPA 3</h2>
            <h1 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; margin-bottom: -30px;">Estamos Creando ${(procesados > 1) ? "las Bajas de los Trabajadores" : "la Baja del Trabajador"}</h1>
                <div class="container__loaderv2">
                    <div class="loader__v2" id="loader"></div>
                </div>
            <h1 class="new-family-teamwork" style="font-weight: 600; text-align: center; width: 100%; font-size: 60px; margin-bottom: -10px; color: darkgreen;">${procesados} </h1>
            <h1 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; color: darkgreen;">${(procesados > 1) ? "Trabajadores correctos" : "Trabajador correcto"}</h1>
            <h3 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; margin-bottom: -10px; color: #f00;">${(errores > 0) ? errores : ""}</h3>
            <h4 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; color: #f00;">${(errores > 0) ? (errores > 1) ? "Trabajadores con problemas en sus datos" : "Trabajador con problema en sus datos" : ""}</h4>
        `;

        xhr.open('POST', `${prefix}PlantillaCrearBaja`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                __ajaxPlantillaEmitir(prefix, transaccion, file, objectsExcel, errores, procesados, "Bajas", templateError);
            }
            else {
                document.getElementById('content__cargamasiva').innerHTML =
                    `
                    <img src="../Resources/cargamasivasvg/triste.svg" width="70" class="m-auto dspl-block" />
                    <h1 class="new-family-teamwork" style="font-weight: 400; text-align: center; width: 100%; color: #f00;">UPS! Ha ocurrido algo inesperado</h1>
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
    } catch {
        document.getElementById('content__cargamasiva').innerHTML =
            `
            <img src="../Resources/cargamasivasvg/triste.svg" width="70" class="m-auto dspl-block" />
            <h1 class="new-family-teamwork" style="font-weight: 400; text-align: center; width: 100%; color: #f00;">UPS! Ha ocurrido algo inesperado</h1>
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

__ajaxPlantillaCrearContrato = (prefix, transaccion, file, objectsExcel, errores, procesados, templateError) => {
    try {
        let xhr = new XMLHttpRequest();
        let __form = new FormData();
        __form.append("codigoTransaction", transaccion);
        
        document.getElementById('content__cargamasiva').innerHTML =
            `
            <img src="../Resources/cargamasivasvg/contratorenov.svg" width="70" class="m-auto dspl-block" />
            <h2 class="new-family-teamwork" style="font-weight: 500; text-align: center; width: 100%; color: rgb(70, 70, 70);">ETAPA 4</h2>
            <h1 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; margin-bottom: -30px;">Estamos Creando ${(procesados > 1) ? "los Contratos de los Trabajadores" : "el Contrato del Trabajador"}</h1>
            <div class="container__loaderv2">
                <div class="loader__v2" id="loader"></div>
            </div>
            <h1 class="new-family-teamwork" style="font-weight: 600; text-align: center; width: 100%; font-size: 60px; margin-bottom: -10px; color: darkgreen;">${procesados} </h1>
            <h1 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; color: darkgreen;">${(procesados > 1) ? "Trabajadores correctos" : "Trabajador correcto"}</h1>
            <h3 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; margin-bottom: -10px; color: #f00;">${(errores > 0) ? errores : ""}</h3>
            <h4 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; color: #f00;">${(errores > 0) ? (errores > 1) ? "Trabajadores con problemas en sus datos" : "Trabajador con problema en sus datos" : ""}</h4>
        `;

        xhr.open('POST', `${prefix}PlantillaCrearContrato`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300)
            {
                __ajaxPlantillaEmitir(prefix, transaccion, file, objectsExcel, errores, procesados, "Contrato", templateError);
            }
            else
            {
                document.getElementById('content__cargamasiva').innerHTML =
                    `
                    <img src="../Resources/cargamasivasvg/triste.svg" width="70" class="m-auto dspl-block" />
                    <h1 class="new-family-teamwork" style="font-weight: 400; text-align: center; width: 100%; color: #f00;">UPS! Ha ocurrido algo inesperado</h1>
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
    } catch {
        document.getElementById('content__cargamasiva').innerHTML =
            `
            <img src="../Resources/cargamasivasvg/triste.svg" width="70" class="m-auto dspl-block" />
            <h1 class="new-family-teamwork" style="font-weight: 400; text-align: center; width: 100%; color: #f00;">UPS! Ha ocurrido algo inesperado</h1>
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

__ajaxPlantillaEmitir = (prefix, transaccion, file, objectsExcel, errores, procesados, type, templateError) => {
    try {
        let xhr = new XMLHttpRequest();
        let __form = new FormData();
        __form.append("codigoTransaction", transaccion);
        __form.append("plantillaMasiva", file);

        document.getElementById('content__cargamasiva').innerHTML =
            `
            <img src="../Resources/cargamasivasvg/email.svg" width="70" class="m-auto dspl-block" />
            <h2 class="new-family-teamwork" style="font-weight: 500; text-align: center; width: 100%; color: rgb(70, 70, 70);">ETAPA 5</h2>
            <h1 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; margin-bottom: -30px;">Estamos Emitiendo y Enviando la Solicitud</h1>
                <div class="container__loaderv2">
                    <div class="loader__v2" id="loader"></div>
                </div>
            <h1 class="new-family-teamwork" style="font-weight: 600; text-align: center; width: 100%; font-size: 60px; margin-bottom: -10px; color: darkgreen;">${procesados} </h1>
            <h1 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; color: darkgreen;">${(procesados > 1) ? "Trabajadores correctos" : "Trabajador correcto"}</h1>
            <h3 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; margin-bottom: -10px; color: #f00;">${(errores > 0) ? errores : ""}</h3>
            <h4 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; color: #f00;">${(errores > 0) ? (errores > 1) ? "Trabajadores con problemas en sus datos" : "Trabajador con problema en sus datos" : ""}</h4>
            `;

        xhr.open('POST', `${prefix}PlantillaEmitirSolicitud${type}`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300)
            {
                document.getElementById('content__cargamasiva').innerHTML =
                    `
                    
                    <img src="../Resources/cargamasivasvg/cheque.svg" width="70" class="m-auto dspl-block" />
                    <h1 class="new-family-teamwork" style="font-weight: 400; text-align: center; width: 100%; color: darkgreen;">PROCESO COMPLETADO</h1>
                    <h3 class="new-family-teamwork m-auto" style="font-weight: 100; text-align: center; width: 80%;">
                        La solicitud fue notificada al area correspondiente, a usted se le envio un comprobante con la solicitud para concepto de validación.
                    </h3>
                    <h1 class="new-family-teamwork" style="font-weight: 600; text-align: center; width: 100%; font-size: 60px; margin-bottom: -10px; color: darkgreen;">${procesados} </h1>
                    <h1 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; color: darkgreen;">${(procesados > 1) ? "Trabajadores" : "Trabajador"}  en la Solicitud</h1>
                    <h3 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; margin-bottom: -10px; color: #f00;">${(errores > 0) ? errores : ""}</h3>
                    <h4 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; color: #f00;">${(errores > 0) ? (errores > 1) ? "Trabajadores excluidos de la Solicitud por problema en esus datos" : "Trabajador excluido de la Solicitud por problema en esus datos" : ""}  </h4>
                    <a href="${prefix}/DownloadFileCargaMasiva?template=${templateError}&codigotransaccion=${transaccion}" class="btn btn-danger new-family-teamwork m-auto ${(errores > 0) ? "dspl-block": "dspl-none"}" style="font-weight: 100; font-size: 20px;">
                        Descargar Solicitud Con Errores!
                    </a>
                    </div>
                    <h3 class="new-family-teamwork ml-auto mr-auto mt-30 ${(errores > 0) ? "dspl-block" : "dspl-none"}" style="font-weight: 100; text-align: center; width: 80%;">
                        Recuerde que una vez que haya corregido los datos con errores, debe eliminar la columna donde se muestra la observacion.
                    </h3>
                    <div style="width: 100%;">
                       <div onclick="handleResetCargaMasiva()" class="btn btn-success ml-auto mr-auto mt-30 new-family-teamwork pdt-10 pdb-10 cursor-pointer" style="font-weight: 100; color: #fff; font-size: 20px; display: block;">Excelente! Gracias, Volver a Solicitar</div>
                    </div>
                    `;
            } else {
                document.getElementById('content__cargamasiva').innerHTML =
                    `
                    <img src="../Resources/cargamasivasvg/triste.svg" width="70" class="m-auto dspl-block" />
                    <h1 class="new-family-teamwork" style="font-weight: 400; text-align: center; width: 100%; color: #f00;">UPS! Ha ocurrido algo inesperado</h1>
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
    } catch {
        document.getElementById('content__cargamasiva').innerHTML =
            `
            <img src="../Resources/cargamasivasvg/triste.svg" width="70" class="m-auto dspl-block" />
            <h1 class="new-family-teamwork" style="font-weight: 400; text-align: center; width: 100%; color: #f00;">UPS! Ha ocurrido algo inesperado</h1>
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

__ajaxPlantillaCrearAsistencia = (prefix, transaccion, file, objectsExcel, errores, procesados, templateError) => {
    try {
        let xhr = new XMLHttpRequest();
        let __form = new FormData();
        __form.append("codigoTransaction", transaccion);

        document.getElementById('content__cargamasiva').innerHTML =
            `
            <img src="../Resources/cargamasivasvg/lista.svg" width="70" class="m-auto dspl-block" />
            <h2 class="new-family-teamwork" style="font-weight: 500; text-align: center; width: 100%; color: rgb(70, 70, 70);">ETAPA 3</h2>
            <h1 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; margin-bottom: -30px;">Estamos Ingresando ${(procesados > 1) ? "las Asistencias de los Trabajadores" : "la Asistencia del Trabajador"}</h1>
                <div class="container__loaderv2">
                    <div class="loader__v2" id="loader"></div>
                </div>
            <h1 class="new-family-teamwork" style="font-weight: 600; text-align: center; width: 100%; font-size: 60px; margin-bottom: -10px; color: darkgreen;">${procesados} </h1>
            <h1 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; color: darkgreen;">${(procesados > 1) ? "Asistencias correctas" : "Asistencia correcta"}</h1>
            <h3 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; margin-bottom: -10px; color: #f00;">${(errores > 0) ? errores : ""}</h3>
            <h4 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; color: #f00;">${(errores > 0) ? (errores > 1) ? "Asistencias con problemas en sus datos" : "Asistencia con problema en sus datos" : ""}</h4>
        `;

        xhr.open('POST', `${prefix}PlantillaCrearAsistencia`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                //__ajaxPlantillaEmitir(prefix, transaccion, file, objectsExcel, errores, procesados, "Renovacion", templateError);

                document.getElementById('content__cargamasiva').innerHTML =
                    `
                    <img src="../Resources/cargamasivasvg/cheque.svg" width="70" class="m-auto dspl-block" />
                    <h1 class="new-family-teamwork" style="font-weight: 400; text-align: center; width: 100%; color: darkgreen;">PROCESO COMPLETADO</h1>
                    <h1 class="new-family-teamwork" style="font-weight: 600; text-align: center; width: 100%; font-size: 60px; margin-bottom: -10px; color: darkgreen;">${procesados} </h1>
                    <h1 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; color: darkgreen;">${(procesados > 1) ? "Trabajadores" : "Trabajador"}  en la Solicitud</h1>
                    <h3 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; margin-bottom: -10px; color: #f00;">${(errores > 0) ? errores : ""}</h3>
                    <h4 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; color: #f00;">${(errores > 0) ? (errores > 1) ? "Asistencias excluidas de la Solicitud por problema en sus datos" : "Asistencia excluida de la Solicitud por problema en sus datos" : ""}  </h4>
                    <a href="${prefix}/DownloadFileCargaMasiva?template=${templateError}&codigotransaccion=${transaccion}" class="btn btn-danger new-family-teamwork m-auto ${(errores > 0) ? "dspl-block" : "dspl-none"}" style="font-weight: 100; font-size: 20px;">
                        Descargar Solicitud Con Errores!
                    </a>
                    </div>
                    <h3 class="new-family-teamwork ml-auto mr-auto mt-30 ${(errores > 0) ? "dspl-block" : "dspl-none"}" style="font-weight: 100; text-align: center; width: 80%;">
                        Recuerde que una vez que haya corregido los datos con errores, debe eliminar la columna donde se muestra la observacion.
                    </h3>
                    <div style="width: 100%;">
                       <div onclick="handleResetCargaMasiva()" class="btn btn-success ml-auto mr-auto mt-30 new-family-teamwork pdt-10 pdb-10 cursor-pointer" style="font-weight: 100; color: #fff; font-size: 20px; display: block;">Excelente! Gracias, Volver a Solicitar</div>
                    </div>
                    `;
            }
            else {
                document.getElementById('content__cargamasiva').innerHTML =
                    `
                    <img src="../Resources/cargamasivasvg/triste.svg" width="70" class="m-auto dspl-block" />
                    <h1 class="new-family-teamwork" style="font-weight: 400; text-align: center; width: 100%; color: #f00;">UPS! Ha ocurrido algo inesperado</h1>
                    <h2 class="new-family-teamwork m-auto" style="font-weight: 100; text-align: center; width: 80%;">
                        La carga no pudo ser procesada, ni validada, ni subida al sistema, intentelo nuevamente, si el problema persiste favor levantar ticket mediante mesa de ayuda para que el area de sistemas pueda resolver el problema.
                    </h2>
                    <h1 class="new-family-teamwork ml-auto mr-auto mt-30" style="font-weight: 100; text-align: center; width: 80%;">Gracias por su comprensión!!!!</h1>
                    <div style="width: 100%;">
                       <div onclick="handleResetCargaMasiva()" class="btn btn-teamwork ml-auto mr-auto mt-30 new-family-teamwork pdt-10 pdb-10 cursor-pointer" style="font-weight: 100; color: #fff; font-size: 20px; display: block;">Entiendo! Volver a Solicitar</div>
                    </div>
                    `;
            }
        };
    } catch {
        document.getElementById('content__cargamasiva').innerHTML =
            `
            <img src="../Resources/cargamasivasvg/triste.svg" width="70" class="m-auto dspl-block" />
            <h1 class="new-family-teamwork" style="font-weight: 400; text-align: center; width: 100%; color: #f00;">UPS! Ha ocurrido algo inesperado</h1>
            <h2 class="new-family-teamwork m-auto" style="font-weight: 100; text-align: center; width: 80%;">
                La carga no pudo ser procesada, ni validada, ni subida al sistema, intentelo nuevamente, si el problema persiste favor levantar ticket mediante mesa de ayuda para que el area de sistemas pueda resolver el problema.
            </h2>
            <h1 class="new-family-teamwork ml-auto mr-auto mt-30" style="font-weight: 100; text-align: center; width: 80%;">Gracias por su comprensión!!!!</h1>
            <div style="width: 100%;">
                <div onclick="handleResetCargaMasiva()" class="btn btn-teamwork ml-auto mr-auto mt-30 new-family-teamwork pdt-10 pdb-10 cursor-pointer" style="font-weight: 100; color: #fff; font-size: 20px; display: block;">Entiendo! Volver a Solicitar</div>
            </div>
            `;
    }
};

__ajaxPlantillaCrearHorasExtras = (prefix, transaccion, file, objectsExcel, errores, procesados, templateError) => {
    try {
        let xhr = new XMLHttpRequest();
        let __form = new FormData();
        __form.append("codigoTransaction", transaccion);

        document.getElementById('content__cargamasiva').innerHTML =
            `
            <img src="../Resources/cargamasivasvg/lista.svg" width="70" class="m-auto dspl-block" />
            <h2 class="new-family-teamwork" style="font-weight: 500; text-align: center; width: 100%; color: rgb(70, 70, 70);">ETAPA 3</h2>
            <h1 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; margin-bottom: -30px;">Estamos Ingresando ${(procesados > 1) ? "las Asistencias de los Trabajadores" : "la Asistencia del Trabajador"}</h1>
                <div class="container__loaderv2">
                    <div class="loader__v2" id="loader"></div>
                </div>
            <h1 class="new-family-teamwork" style="font-weight: 600; text-align: center; width: 100%; font-size: 60px; margin-bottom: -10px; color: darkgreen;">${procesados} </h1>
            <h1 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; color: darkgreen;">${(procesados > 1) ? "Asistencias correctas" : "Asistencia correcta"}</h1>
            <h3 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; margin-bottom: -10px; color: #f00;">${(errores > 0) ? errores : ""}</h3>
            <h4 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; color: #f00;">${(errores > 0) ? (errores > 1) ? "Asistencias con problemas en sus datos" : "Asistencia con problema en sus datos" : ""}</h4>
        `;

        xhr.open('POST', `${prefix}PlantillaCrearHorasExtras`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                //__ajaxPlantillaEmitir(prefix, transaccion, file, objectsExcel, errores, procesados, "Renovacion", templateError);

                document.getElementById('content__cargamasiva').innerHTML =
                    `
                    <img src="../Resources/cargamasivasvg/cheque.svg" width="70" class="m-auto dspl-block" />
                    <h1 class="new-family-teamwork" style="font-weight: 400; text-align: center; width: 100%; color: darkgreen;">PROCESO COMPLETADO</h1>
                    <h1 class="new-family-teamwork" style="font-weight: 600; text-align: center; width: 100%; font-size: 60px; margin-bottom: -10px; color: darkgreen;">${procesados} </h1>
                    <h1 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; color: darkgreen;">${(procesados > 1) ? "Trabajadores" : "Trabajador"}  en la Solicitud</h1>
                    <h3 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; margin-bottom: -10px; color: #f00;">${(errores > 0) ? errores : ""}</h3>
                    <h4 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; color: #f00;">${(errores > 0) ? (errores > 1) ? "Asistencias excluidas de la Solicitud por problema en sus datos" : "Asistencia excluida de la Solicitud por problema en sus datos" : ""}  </h4>
                    <a href="${prefix}/DownloadFileCargaMasiva?template=${templateError}&codigotransaccion=${transaccion}" class="btn btn-danger new-family-teamwork m-auto ${(errores > 0) ? "dspl-block" : "dspl-none"}" style="font-weight: 100; font-size: 20px;">
                        Descargar Solicitud Con Errores!
                    </a>
                    </div>
                    <h3 class="new-family-teamwork ml-auto mr-auto mt-30 ${(errores > 0) ? "dspl-block" : "dspl-none"}" style="font-weight: 100; text-align: center; width: 80%;">
                        Recuerde que una vez que haya corregido los datos con errores, debe eliminar la columna donde se muestra la observacion.
                    </h3>
                    <div style="width: 100%;">
                       <div onclick="handleResetCargaMasiva()" class="btn btn-success ml-auto mr-auto mt-30 new-family-teamwork pdt-10 pdb-10 cursor-pointer" style="font-weight: 100; color: #fff; font-size: 20px; display: block;">Excelente! Gracias, Volver a Solicitar</div>
                    </div>
                    `;
            }
            else {
                document.getElementById('content__cargamasiva').innerHTML =
                    `
                    <img src="../Resources/cargamasivasvg/triste.svg" width="70" class="m-auto dspl-block" />
                    <h1 class="new-family-teamwork" style="font-weight: 400; text-align: center; width: 100%; color: #f00;">UPS! Ha ocurrido algo inesperado</h1>
                    <h2 class="new-family-teamwork m-auto" style="font-weight: 100; text-align: center; width: 80%;">
                        La carga no pudo ser procesada, ni validada, ni subida al sistema, intentelo nuevamente, si el problema persiste favor levantar ticket mediante mesa de ayuda para que el area de sistemas pueda resolver el problema.
                    </h2>
                    <h1 class="new-family-teamwork ml-auto mr-auto mt-30" style="font-weight: 100; text-align: center; width: 80%;">Gracias por su comprensión!!!!</h1>
                    <div style="width: 100%;">
                       <div onclick="handleResetCargaMasiva()" class="btn btn-teamwork ml-auto mr-auto mt-30 new-family-teamwork pdt-10 pdb-10 cursor-pointer" style="font-weight: 100; color: #fff; font-size: 20px; display: block;">Entiendo! Volver a Solicitar</div>
                    </div>
                    `;
            }
        };
    } catch {
        document.getElementById('content__cargamasiva').innerHTML =
            `
            <img src="../Resources/cargamasivasvg/triste.svg" width="70" class="m-auto dspl-block" />
            <h1 class="new-family-teamwork" style="font-weight: 400; text-align: center; width: 100%; color: #f00;">UPS! Ha ocurrido algo inesperado</h1>
            <h2 class="new-family-teamwork m-auto" style="font-weight: 100; text-align: center; width: 80%;">
                La carga no pudo ser procesada, ni validada, ni subida al sistema, intentelo nuevamente, si el problema persiste favor levantar ticket mediante mesa de ayuda para que el area de sistemas pueda resolver el problema.
            </h2>
            <h1 class="new-family-teamwork ml-auto mr-auto mt-30" style="font-weight: 100; text-align: center; width: 80%;">Gracias por su comprensión!!!!</h1>
            <div style="width: 100%;">
                <div onclick="handleResetCargaMasiva()" class="btn btn-teamwork ml-auto mr-auto mt-30 new-family-teamwork pdt-10 pdb-10 cursor-pointer" style="font-weight: 100; color: #fff; font-size: 20px; display: block;">Entiendo! Volver a Solicitar</div>
            </div>
            `;
    }
};

__ajaxPlantillaCrearAsistenciaRetail = (prefix, transaccion, file, objectsExcel, errores, procesados, templateError) => {
    try {
        let xhr = new XMLHttpRequest();
        let __form = new FormData();
        __form.append("codigoTransaction", transaccion);

        document.getElementById('content__cargamasiva').innerHTML =
            `
            <img src="../Resources/cargamasivasvg/lista.svg" width="70" class="m-auto dspl-block" />
            <h2 class="new-family-teamwork" style="font-weight: 500; text-align: center; width: 100%; color: rgb(70, 70, 70);">ETAPA 3</h2>
            <h1 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; margin-bottom: -30px;">Estamos Ingresando ${(procesados > 1) ? "las Asistencias de los Trabajadores" : "la Asistencia del Trabajador"}</h1>
                <div class="container__loaderv2">
                    <div class="loader__v2" id="loader"></div>
                </div>
            <h1 class="new-family-teamwork" style="font-weight: 600; text-align: center; width: 100%; font-size: 60px; margin-bottom: -10px; color: darkgreen;">${procesados} </h1>
            <h1 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; color: darkgreen;">${(procesados > 1) ? "Asistencias correctas" : "Asistencia correcta"}</h1>
            <h3 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; margin-bottom: -10px; color: #f00;">${(errores > 0) ? errores : ""}</h3>
            <h4 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; color: #f00;">${(errores > 0) ? (errores > 1) ? "Asistencias con problemas en sus datos" : "Asistencia con problema en sus datos" : ""}</h4>
        `;

        xhr.open('POST', `${prefix}PlantillaCrearAsistenciaRetail`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                //__ajaxPlantillaEmitir(prefix, transaccion, file, objectsExcel, errores, procesados, "Renovacion", templateError);

                document.getElementById('content__cargamasiva').innerHTML =
                    `
                    <img src="../Resources/cargamasivasvg/cheque.svg" width="70" class="m-auto dspl-block" />
                    <h1 class="new-family-teamwork" style="font-weight: 400; text-align: center; width: 100%; color: darkgreen;">PROCESO COMPLETADO</h1>
                    <h1 class="new-family-teamwork" style="font-weight: 600; text-align: center; width: 100%; font-size: 60px; margin-bottom: -10px; color: darkgreen;">${procesados} </h1>
                    <h1 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; color: darkgreen;">${(procesados > 1) ? "Trabajadores" : "Trabajador"}  en la Solicitud</h1>
                    <h3 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; margin-bottom: -10px; color: #f00;">${(errores > 0) ? errores : ""}</h3>
                    <h4 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; color: #f00;">${(errores > 0) ? (errores > 1) ? "Asistencias excluidas de la Solicitud por problema en sus datos" : "Asistencia excluida de la Solicitud por problema en sus datos" : ""}  </h4>
                    <a href="${prefix}/DownloadFileCargaMasiva?template=${templateError}&codigotransaccion=${transaccion}" class="btn btn-danger new-family-teamwork m-auto ${(errores > 0) ? "dspl-block" : "dspl-none"}" style="font-weight: 100; font-size: 20px;">
                        Descargar Solicitud Con Errores!
                    </a>
                    </div>
                    <h3 class="new-family-teamwork ml-auto mr-auto mt-30 ${(errores > 0) ? "dspl-block" : "dspl-none"}" style="font-weight: 100; text-align: center; width: 80%;">
                        Recuerde que una vez que haya corregido los datos con errores, debe eliminar la columna donde se muestra la observacion.
                    </h3>
                    <div style="width: 100%;">
                       <div onclick="handleResetCargaMasiva()" class="btn btn-success ml-auto mr-auto mt-30 new-family-teamwork pdt-10 pdb-10 cursor-pointer" style="font-weight: 100; color: #fff; font-size: 20px; display: block;">Excelente! Gracias, Volver a Solicitar</div>
                    </div>
                    `;
            }
            else {
                document.getElementById('content__cargamasiva').innerHTML =
                    `
                    <img src="../Resources/cargamasivasvg/triste.svg" width="70" class="m-auto dspl-block" />
                    <h1 class="new-family-teamwork" style="font-weight: 400; text-align: center; width: 100%; color: #f00;">UPS! Ha ocurrido algo inesperado</h1>
                    <h2 class="new-family-teamwork m-auto" style="font-weight: 100; text-align: center; width: 80%;">
                        La carga no pudo ser procesada, ni validada, ni subida al sistema, intentelo nuevamente, si el problema persiste favor levantar ticket mediante mesa de ayuda para que el area de sistemas pueda resolver el problema.
                    </h2>
                    <h1 class="new-family-teamwork ml-auto mr-auto mt-30" style="font-weight: 100; text-align: center; width: 80%;">Gracias por su comprensión!!!!</h1>
                    <div style="width: 100%;">
                       <div onclick="handleResetCargaMasiva()" class="btn btn-teamwork ml-auto mr-auto mt-30 new-family-teamwork pdt-10 pdb-10 cursor-pointer" style="font-weight: 100; color: #fff; font-size: 20px; display: block;">Entiendo! Volver a Solicitar</div>
                    </div>
                    `;
            }
        };
    } catch {
        document.getElementById('content__cargamasiva').innerHTML =
            `
            <img src="../Resources/cargamasivasvg/triste.svg" width="70" class="m-auto dspl-block" />
            <h1 class="new-family-teamwork" style="font-weight: 400; text-align: center; width: 100%; color: #f00;">UPS! Ha ocurrido algo inesperado</h1>
            <h2 class="new-family-teamwork m-auto" style="font-weight: 100; text-align: center; width: 80%;">
                La carga no pudo ser procesada, ni validada, ni subida al sistema, intentelo nuevamente, si el problema persiste favor levantar ticket mediante mesa de ayuda para que el area de sistemas pueda resolver el problema.
            </h2>
            <h1 class="new-family-teamwork ml-auto mr-auto mt-30" style="font-weight: 100; text-align: center; width: 80%;">Gracias por su comprensión!!!!</h1>
            <div style="width: 100%;">
                <div onclick="handleResetCargaMasiva()" class="btn btn-teamwork ml-auto mr-auto mt-30 new-family-teamwork pdt-10 pdb-10 cursor-pointer" style="font-weight: 100; color: #fff; font-size: 20px; display: block;">Entiendo! Volver a Solicitar</div>
            </div>
            `;
    }
};

__ajaxPlantillaCrearAsistenciaRelojControlGeoVictoria = (prefix, transaccion, file, objectsExcel, errores, procesados, templateError) => {
    try {
        let xhr = new XMLHttpRequest();
        let __form = new FormData();
        __form.append("codigoTransaction", transaccion);

        document.getElementById('content__cargamasiva').innerHTML =
            `
            <img src="../Resources/cargamasivasvg/lista.svg" width="70" class="m-auto dspl-block" />
            <h2 class="new-family-teamwork" style="font-weight: 500; text-align: center; width: 100%; color: rgb(70, 70, 70);">ETAPA 3</h2>
            <h1 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; margin-bottom: -30px;">Estamos Ingresando ${(procesados > 1) ? "las Asistencias de los Trabajadores" : "la Asistencia del Trabajador"}</h1>
                <div class="container__loaderv2">
                    <div class="loader__v2" id="loader"></div>
                </div>
            <h1 class="new-family-teamwork" style="font-weight: 600; text-align: center; width: 100%; font-size: 60px; margin-bottom: -10px; color: darkgreen;">${procesados} </h1>
            <h1 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; color: darkgreen;">${(procesados > 1) ? "Asistencias correctas" : "Asistencia correcta"}</h1>
            <h3 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; margin-bottom: -10px; color: #f00;">${(errores > 0) ? errores : ""}</h3>
            <h4 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; color: #f00;">${(errores > 0) ? (errores > 1) ? "Asistencias con problemas en sus datos" : "Asistencia con problema en sus datos" : ""}</h4>
        `;

        xhr.open('POST', `${prefix}PlantillaCrearAsistenciaRelojControlGeoVictoria`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                document.getElementById('content__cargamasiva').innerHTML =
                    `
                    <img src="../Resources/cargamasivasvg/cheque.svg" width="70" class="m-auto dspl-block" />
                    <h1 class="new-family-teamwork" style="font-weight: 400; text-align: center; width: 100%; color: darkgreen;">PROCESO COMPLETADO</h1>
                    <h1 class="new-family-teamwork" style="font-weight: 600; text-align: center; width: 100%; font-size: 60px; margin-bottom: -10px; color: darkgreen;">${procesados} </h1>
                    <h1 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; color: darkgreen;">${(procesados > 1) ? "Registros" : "Registros"}  en la Solicitud</h1>
                    <h3 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; margin-bottom: -10px; color: #f00;">${(errores > 0) ? errores : ""}</h3>
                    <h4 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; color: #f00;">${(errores > 0) ? (errores > 1) ? "Asistencias excluidas de la Solicitud por problema en sus datos" : "Asistencia excluida de la Solicitud por problema en sus datos" : ""}  </h4>
                    <a href="${prefix}/DownloadFileCargaMasivaRelojControl?template=${templateError}&codigotransaccion=${transaccion}" class="btn btn-danger new-family-teamwork m-auto ${(errores > 0) ? "dspl-block" : "dspl-none"}" style="font-weight: 100; font-size: 20px;">
                        Descargar Solicitud Con Errores!
                    </a>
                    </div>
                    <h3 class="new-family-teamwork ml-auto mr-auto mt-30 ${(errores > 0) ? "dspl-block" : "dspl-none"}" style="font-weight: 100; text-align: center; width: 80%;">
                        Recuerde que una vez que haya corregido los datos con errores, debe eliminar la columna donde se muestra la observacion.
                    </h3>
                    <div style="width: 100%;">
                       <div onclick="handleResetCargaMasiva()" class="btn btn-success ml-auto mr-auto mt-30 new-family-teamwork pdt-10 pdb-10 cursor-pointer" style="font-weight: 100; color: #fff; font-size: 20px; display: block;">Excelente! Gracias, Volver a Solicitar</div>
                    </div>
                    `;
            }
            else {
                document.getElementById('content__cargamasiva').innerHTML =
                    `
                    <img src="../Resources/cargamasivasvg/triste.svg" width="70" class="m-auto dspl-block" />
                    <h1 class="new-family-teamwork" style="font-weight: 400; text-align: center; width: 100%; color: #f00;">UPS! Ha ocurrido algo inesperado</h1>
                    <h2 class="new-family-teamwork m-auto" style="font-weight: 100; text-align: center; width: 80%;">
                        La carga no pudo ser procesada, ni validada, ni subida al sistema, intentelo nuevamente, si el problema persiste favor levantar ticket mediante mesa de ayuda para que el area de sistemas pueda resolver el problema.
                    </h2>
                    <h1 class="new-family-teamwork ml-auto mr-auto mt-30" style="font-weight: 100; text-align: center; width: 80%;">Gracias por su comprensión!!!!</h1>
                    <div style="width: 100%;">
                       <div onclick="handleResetCargaMasiva()" class="btn btn-teamwork ml-auto mr-auto mt-30 new-family-teamwork pdt-10 pdb-10 cursor-pointer" style="font-weight: 100; color: #fff; font-size: 20px; display: block;">Entiendo! Volver a Solicitar</div>
                    </div>
                    `;
            }
        };
    } catch {
        document.getElementById('content__cargamasiva').innerHTML =
            `
            <img src="../Resources/cargamasivasvg/triste.svg" width="70" class="m-auto dspl-block" />
            <h1 class="new-family-teamwork" style="font-weight: 400; text-align: center; width: 100%; color: #f00;">UPS! Ha ocurrido algo inesperado</h1>
            <h2 class="new-family-teamwork m-auto" style="font-weight: 100; text-align: center; width: 80%;">
                La carga no pudo ser procesada, ni validada, ni subida al sistema, intentelo nuevamente, si el problema persiste favor levantar ticket mediante mesa de ayuda para que el area de sistemas pueda resolver el problema.
            </h2>
            <h1 class="new-family-teamwork ml-auto mr-auto mt-30" style="font-weight: 100; text-align: center; width: 80%;">Gracias por su comprensión!!!!</h1>
            <div style="width: 100%;">
                <div onclick="handleResetCargaMasiva()" class="btn btn-teamwork ml-auto mr-auto mt-30 new-family-teamwork pdt-10 pdb-10 cursor-pointer" style="font-weight: 100; color: #fff; font-size: 20px; display: block;">Entiendo! Volver a Solicitar</div>
            </div>
            `;
    }
};

__ajaxPlantillaCrearAsistenciaRelojControl = (prefix, transaccion, file, objectsExcel, errores, procesados, templateError) => {
    try {
        let xhr = new XMLHttpRequest();
        let __form = new FormData();
        __form.append("codigoTransaction", transaccion);

        document.getElementById('content__cargamasiva').innerHTML =
            `
            <img src="../Resources/cargamasivasvg/lista.svg" width="70" class="m-auto dspl-block" />
            <h2 class="new-family-teamwork" style="font-weight: 500; text-align: center; width: 100%; color: rgb(70, 70, 70);">ETAPA 3</h2>
            <h1 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; margin-bottom: -30px;">Estamos Ingresando ${(procesados > 1) ? "las Asistencias de los Trabajadores" : "la Asistencia del Trabajador"}</h1>
                <div class="container__loaderv2">
                    <div class="loader__v2" id="loader"></div>
                </div>
            <h1 class="new-family-teamwork" style="font-weight: 600; text-align: center; width: 100%; font-size: 60px; margin-bottom: -10px; color: darkgreen;">${procesados} </h1>
            <h1 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; color: darkgreen;">${(procesados > 1) ? "Asistencias correctas" : "Asistencia correcta"}</h1>
            <h3 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; margin-bottom: -10px; color: #f00;">${(errores > 0) ? errores : ""}</h3>
            <h4 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; color: #f00;">${(errores > 0) ? (errores > 1) ? "Asistencias con problemas en sus datos" : "Asistencia con problema en sus datos" : ""}</h4>
        `;

        xhr.open('POST', `${prefix}PlantillaCrearAsistenciaRelojControl`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                document.getElementById('content__cargamasiva').innerHTML =
                    `
                    <img src="../Resources/cargamasivasvg/cheque.svg" width="70" class="m-auto dspl-block" />
                    <h1 class="new-family-teamwork" style="font-weight: 400; text-align: center; width: 100%; color: darkgreen;">PROCESO COMPLETADO</h1>
                    <h1 class="new-family-teamwork" style="font-weight: 600; text-align: center; width: 100%; font-size: 60px; margin-bottom: -10px; color: darkgreen;">${procesados} </h1>
                    <h1 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; color: darkgreen;">${(procesados > 1) ? "Registros" : "Registros"}  en la Solicitud</h1>
                    <h3 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; margin-bottom: -10px; color: #f00;">${(errores > 0) ? errores : ""}</h3>
                    <h4 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; color: #f00;">${(errores > 0) ? (errores > 1) ? "Asistencias excluidas de la Solicitud por problema en sus datos" : "Asistencia excluida de la Solicitud por problema en sus datos" : ""}  </h4>
                    <a href="${prefix}/DownloadFileCargaMasivaRelojControl?template=${templateError}&codigotransaccion=${transaccion}" class="btn btn-danger new-family-teamwork m-auto ${(errores > 0) ? "dspl-block" : "dspl-none"}" style="font-weight: 100; font-size: 20px;">
                        Descargar Solicitud Con Errores!
                    </a>
                    </div>
                    <h3 class="new-family-teamwork ml-auto mr-auto mt-30 ${(errores > 0) ? "dspl-block" : "dspl-none"}" style="font-weight: 100; text-align: center; width: 80%;">
                        Recuerde que una vez que haya corregido los datos con errores, debe eliminar la columna donde se muestra la observacion.
                    </h3>
                    <div style="width: 100%;">
                       <div onclick="handleResetCargaMasiva()" class="btn btn-success ml-auto mr-auto mt-30 new-family-teamwork pdt-10 pdb-10 cursor-pointer" style="font-weight: 100; color: #fff; font-size: 20px; display: block;">Excelente! Gracias, Volver a Solicitar</div>
                    </div>
                    `;
            }
            else {
                document.getElementById('content__cargamasiva').innerHTML =
                    `
                    <img src="../Resources/cargamasivasvg/triste.svg" width="70" class="m-auto dspl-block" />
                    <h1 class="new-family-teamwork" style="font-weight: 400; text-align: center; width: 100%; color: #f00;">UPS! Ha ocurrido algo inesperado</h1>
                    <h2 class="new-family-teamwork m-auto" style="font-weight: 100; text-align: center; width: 80%;">
                        La carga no pudo ser procesada, ni validada, ni subida al sistema, intentelo nuevamente, si el problema persiste favor levantar ticket mediante mesa de ayuda para que el area de sistemas pueda resolver el problema.
                    </h2>
                    <h1 class="new-family-teamwork ml-auto mr-auto mt-30" style="font-weight: 100; text-align: center; width: 80%;">Gracias por su comprensión!!!!</h1>
                    <div style="width: 100%;">
                       <div onclick="handleResetCargaMasiva()" class="btn btn-teamwork ml-auto mr-auto mt-30 new-family-teamwork pdt-10 pdb-10 cursor-pointer" style="font-weight: 100; color: #fff; font-size: 20px; display: block;">Entiendo! Volver a Solicitar</div>
                    </div>
                    `;
            }
        };
    } catch {
        document.getElementById('content__cargamasiva').innerHTML =
            `
            <img src="../Resources/cargamasivasvg/triste.svg" width="70" class="m-auto dspl-block" />
            <h1 class="new-family-teamwork" style="font-weight: 400; text-align: center; width: 100%; color: #f00;">UPS! Ha ocurrido algo inesperado</h1>
            <h2 class="new-family-teamwork m-auto" style="font-weight: 100; text-align: center; width: 80%;">
                La carga no pudo ser procesada, ni validada, ni subida al sistema, intentelo nuevamente, si el problema persiste favor levantar ticket mediante mesa de ayuda para que el area de sistemas pueda resolver el problema.
            </h2>
            <h1 class="new-family-teamwork ml-auto mr-auto mt-30" style="font-weight: 100; text-align: center; width: 80%;">Gracias por su comprensión!!!!</h1>
            <div style="width: 100%;">
                <div onclick="handleResetCargaMasiva()" class="btn btn-teamwork ml-auto mr-auto mt-30 new-family-teamwork pdt-10 pdb-10 cursor-pointer" style="font-weight: 100; color: #fff; font-size: 20px; display: block;">Entiendo! Volver a Solicitar</div>
            </div>
            `;
    }
};

__ajaxPlantillaCrearBonos = (prefix, transaccion, file, objectsExcel, errores, procesados, templateError) => {
    try {
        let xhr = new XMLHttpRequest();
        let __form = new FormData();
        __form.append("codigoTransaction", transaccion);

        document.getElementById('content__cargamasiva').innerHTML =
            `
            <img src="../Resources/cargamasivasvg/lista.svg" width="70" class="m-auto dspl-block" />
            <h2 class="new-family-teamwork" style="font-weight: 500; text-align: center; width: 100%; color: rgb(70, 70, 70);">ETAPA 3</h2>
            <h1 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; margin-bottom: -30px;">Estamos Ingresando ${(procesados > 1) ? "las Asignaciones de bonos de los Trabajadores" : "la Asignación de bonos del Trabajador"}</h1>
                <div class="container__loaderv2">
                    <div class="loader__v2" id="loader"></div>
                </div>
            <h1 class="new-family-teamwork" style="font-weight: 600; text-align: center; width: 100%; font-size: 60px; margin-bottom: -10px; color: darkgreen;">${procesados} </h1>
            <h1 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; color: darkgreen;">${(procesados > 1) ? "Asignación Bonos correctos" : "Asignación Bono correcto"}</h1>
            <h3 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; margin-bottom: -10px; color: #f00;">${(errores > 0) ? errores : ""}</h3>
            <h4 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; color: #f00;">${(errores > 0) ? (errores > 1) ? "Asignación bonos con problemas en sus datos" : "Asignación bono con problema en sus datos" : ""}</h4>
        `;

        xhr.open('POST', `${prefix}PlantillaCrearBono`);

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                document.getElementById('content__cargamasiva').innerHTML =
                    `
                    <img src="../Resources/cargamasivasvg/cheque.svg" width="70" class="m-auto dspl-block" />
                    <h1 class="new-family-teamwork" style="font-weight: 400; text-align: center; width: 100%; color: darkgreen;">PROCESO COMPLETADO</h1>
                    <h1 class="new-family-teamwork" style="font-weight: 600; text-align: center; width: 100%; font-size: 60px; margin-bottom: -10px; color: darkgreen;">${procesados} </h1>
                    <h1 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; color: darkgreen;">${(procesados > 1) ? "Registros" : "Registros"}  en la Solicitud</h1>
                    <h3 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; margin-bottom: -10px; color: #f00;">${(errores > 0) ? errores : ""}</h3>
                    <h4 class="new-family-teamwork" style="font-weight: 100; text-align: center; width: 100%; color: #f00;">${(errores > 0) ? (errores > 1) ? "Asignación de bonos excluidas de la Solicitud por problema en sus datos" : "Asignación de bono excluida de la Solicitud por problema en sus datos" : ""}  </h4>
                    <a href="${prefix}/DownloadFileCargaMasiva?template=${templateError}&codigotransaccion=${transaccion}" class="btn btn-danger new-family-teamwork m-auto ${(errores > 0) ? "dspl-block" : "dspl-none"}" style="font-weight: 100; font-size: 20px;">
                        Descargar Solicitud Con Errores!
                    </a>
                    </div>
                    <h3 class="new-family-teamwork ml-auto mr-auto mt-30 ${(errores > 0) ? "dspl-block" : "dspl-none"}" style="font-weight: 100; text-align: center; width: 80%;">
                        Recuerde que una vez que haya corregido los datos con errores, debe eliminar la columna donde se muestra la observacion.
                    </h3>
                    <div style="width: 100%;">
                       <div onclick="handleResetCargaMasiva()" class="btn btn-success ml-auto mr-auto mt-30 new-family-teamwork pdt-10 pdb-10 cursor-pointer" style="font-weight: 100; color: #fff; font-size: 20px; display: block;">Excelente! Gracias, Volver a Solicitar</div>
                    </div>
                    `;
            }
            else {
                document.getElementById('content__cargamasiva').innerHTML =
                    `
                    <img src="../Resources/cargamasivasvg/triste.svg" width="70" class="m-auto dspl-block" />
                    <h1 class="new-family-teamwork" style="font-weight: 400; text-align: center; width: 100%; color: #f00;">UPS! Ha ocurrido algo inesperado</h1>
                    <h2 class="new-family-teamwork m-auto" style="font-weight: 100; text-align: center; width: 80%;">
                        La carga no pudo ser procesada, ni validada, ni subida al sistema, intentelo nuevamente, si el problema persiste favor levantar ticket mediante mesa de ayuda para que el area de sistemas pueda resolver el problema.
                    </h2>
                    <h1 class="new-family-teamwork ml-auto mr-auto mt-30" style="font-weight: 100; text-align: center; width: 80%;">Gracias por su comprensión!!!!</h1>
                    <div style="width: 100%;">
                       <div onclick="handleResetCargaMasiva()" class="btn btn-teamwork ml-auto mr-auto mt-30 new-family-teamwork pdt-10 pdb-10 cursor-pointer" style="font-weight: 100; color: #fff; font-size: 20px; display: block;">Entiendo! Volver a Solicitar</div>
                    </div>
                    `;
            }
        };
    } catch {
        document.getElementById('content__cargamasiva').innerHTML =
            `
            <img src="../Resources/cargamasivasvg/triste.svg" width="70" class="m-auto dspl-block" />
            <h1 class="new-family-teamwork" style="font-weight: 400; text-align: center; width: 100%; color: #f00;">UPS! Ha ocurrido algo inesperado</h1>
            <h2 class="new-family-teamwork m-auto" style="font-weight: 100; text-align: center; width: 80%;">
                La carga no pudo ser procesada, ni validada, ni subida al sistema, intentelo nuevamente, si el problema persiste favor levantar ticket mediante mesa de ayuda para que el area de sistemas pueda resolver el problema.
            </h2>
            <h1 class="new-family-teamwork ml-auto mr-auto mt-30" style="font-weight: 100; text-align: center; width: 80%;">Gracias por su comprensión!!!!</h1>
            <div style="width: 100%;">
                <div onclick="handleResetCargaMasiva()" class="btn btn-teamwork ml-auto mr-auto mt-30 new-family-teamwork pdt-10 pdb-10 cursor-pointer" style="font-weight: 100; color: #fff; font-size: 20px; display: block;">Entiendo! Volver a Solicitar</div>
            </div>
            `;
    }
};

handleResetCargaMasiva = () => {
    window.location.reload();
};