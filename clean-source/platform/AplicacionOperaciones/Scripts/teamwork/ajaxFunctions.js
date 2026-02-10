function ajaxCargaMasiva(controller, data, codigoTransaccion, plantilla, lengthProcesamiento, procesoValidacion, templateColumn) {
    $.ajax({
        type: 'POST',
        url: controller + 'CargaMasiva',
        data: '{ data: "' + data + '", codigoTransaccion: "' + codigoTransaccion + '", plantilla: "' + plantilla + '", lengthProcesamiento: "' + lengthProcesamiento + '", templateColumn: "' + templateColumn + '" }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if(response.Code !== "600"){
                if (response.Code === "200") {
                    if (parseInt(response.TotalProcesado) < parseInt(lengthProcesamiento)) {
                        ajaxViewPartialLoaderProcesoMasivo(controller, response.Message);
                    }
                    else {
                        ajaxViewPartialLoaderProcesoMasivo(controller, response.Message);
                        setTimeout(function () {
                            for (var i = 0; i < procesoValidacion.length; i++) {
                                ajaxValidacionCargaMasiva(controller, codigoTransaccion, plantilla, procesoValidacion[i], lengthProcesamiento, procesoValidacion);
                            }
                        }, 500);
                    }
                } else {
                    ajaxViewPartialLoaderErrorGeneric(controller, response.Message);
                    sessionStorage.setItem("ApplicationError", "S");
                } 
            }
            else
            {
                $("#modelExpiracion").modal("show");
                sessionStorage.clear();
            }
        },
        error: function (xhr) {
            $("#loaderProceso").html('');
            ajaxViewPartialLoaderErrorGeneric(controller, "Ha ocurrido algo inesperado en la plataforma, favor comunicarse con el area de TI, para ser solucionado el problema. Favor refrescar la pagina para evitar problemas de encolamiento de información.");
            sessionStorage.clear();
        },
        complete: function () {
        }
    });
}

function ajaxValidacionCargaMasiva(controller, codigoTransaccion, plantilla, rutProcesado, lengthProcesamiento, procesoValidacion) {
    $.ajax({
        type: 'POST',
        url: controller + 'ValidacionCargaMasiva',
        data: '{ codigoTransaccion: "' + codigoTransaccion + '", plantilla: "' + plantilla + '", procesamiento: "' + rutProcesado + '" }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code !== "600") {
                if (response.Code === "200") {
                    var positionProcesado = 0;

                    if (parseInt(response.TotalProcesado) < parseInt(lengthProcesamiento)) {
                        if (response.ContieneErrores === 'N') {
                            positionProcesado = procesoValidacion.indexOf(response.RutProcesado);
                            procesoValidacion.splice(positionProcesado, 1);
                        }
                        ajaxViewPartialLoaderProcesoMasivo(controller, response.Message);
                    }
                    else {
                        if (parseInt(response.TotalErrores) < parseInt(lengthProcesamiento)) {
                            if (response.ContieneErrores === 'N') {
                                positionProcesado = procesoValidacion.indexOf(response.RutProcesado);
                                procesoValidacion.splice(positionProcesado, 1);
                            }
                            ajaxViewPartialLoaderProcesoMasivo(controller, response.Message);
                            if (parseInt(response.TotalErrores) > 0) {
                                ajaxViewPartialLoaderErrorGeneric(controller, response.MessageErrores);
                            }
                            setTimeout(function () {
                                for (var i = 0; i < procesoValidacion.length; i++) {
                                    switch (response.SigueProceso) {
                                        case "Q29udHJhdG8=":
                                            ajaxCreaOActualizaFichaPersonal(controller, codigoTransaccion, procesoValidacion[i], procesoValidacion.length, procesoValidacion, plantilla);
                                            break;
                                        case "UmVub3ZhY2lvbg==":
                                            ajaxCreaRenovacion(controller, codigoTransaccion, procesoValidacion[i], procesoValidacion.length, procesoValidacion, plantilla);
                                            break;
                                        case "QW5leG8=":
                                            ajaxCreaAnexos(controller, codigoTransaccion, procesoValidacion[i], procesoValidacion.length, procesoValidacion, plantilla);
                                            break;
                                        case "QmFqYXM=":
                                            ajaxCreaBaja(controller, codigoTransaccion, procesoValidacion[i], procesoValidacion.length, procesoValidacion, plantilla);
                                            break;
                                        case "R2FzdG9z":
                                            alert("validado");
                                            $("#loaderProceso").html('');
                                            ajaxCrearGastosFinanzas(controller, codigoTransaccion, procesoValidacion[i], procesoValidacion.length, procesoValidacion, plantilla)

                                            break;
                                    }
                                }
                            }, 500);
                        }
                        else {
                            setTimeout(function () {
                                ajaxViewPartialLoaderErrorGeneric(controller, response.MessageErrores);
                                $("#loaderProceso").html('');
                            }, 500);
                        }

                    }
                }
                else {
                    $("#loaderProceso").html('');
                    ajaxViewPartialLoaderErrorGeneric(controller, response.Message);
                }
            }
            else {
                $("#modelExpiracion").modal("show");
            }
        },
        error: function (xhr) {
            $("#loaderProceso").html('');
            ajaxViewPartialLoaderErrorGeneric(controller, "Ha ocurrido algo inesperado en la plataforma, favor comunicarse con el area de TI, para ser solucionado el problema. Favor refrescar la pagina para evitar problemas de encolamiento de información.");
        },
        complete: function () {
        }
    });
}

function ajaxCreaOActualizaFichaPersonal(controller, codigoTransaccion, procesamiento, lengthProcesamiento, procesoValidacion, plantilla) {
    $.ajax({
        type: 'POST',
        url: controller + 'CreaOActualizaFichaPersonal',
        data: '{ codigoTransaccion: "' + codigoTransaccion + '", procesamiento: "' + procesamiento + '" }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code !== "600") {
                if (response.Code === "200") {
                    if (parseInt(response.TotalProcesado) < parseInt(lengthProcesamiento)) {
                        ajaxViewPartialLoaderProcesoMasivo(controller, response.Message);
                    }
                    else {
                        ajaxViewPartialLoaderProcesoMasivo(controller, response.Message);
                        setTimeout(function () {
                            for (var i = 0; i < procesoValidacion.length; i++) {
                                ajaxCreaContratoDeTrabajo(controller, codigoTransaccion, procesoValidacion[i], procesoValidacion.length, procesoValidacion, plantilla);
                            }
                        }, 500);
                    }
                }
                else
                {
                    ajaxViewPartialLoaderErrorGeneric(controller, response.Message);
                }
            }
            else {
                $("#modelExpiracion").modal("show");
            }
        },
        error: function (xhr) {
            $("#loaderProceso").html('');
            ajaxViewPartialLoaderErrorGeneric(controller, "Ha ocurrido algo inesperado en la plataforma, favor comunicarse con el area de TI, para ser solucionado el problema. Favor refrescar la pagina para evitar problemas de encolamiento de información.");
        },
        complete: function () {
        }
    });
}

function ajaxCreaContratoDeTrabajo(controller, codigoTransaccion, procesamiento, lengthProcesamiento, procesoValidacion, plantilla) {
    $.ajax({
        type: 'POST',
        url: controller + 'CreaContratoDeTrabajo',
        data: '{ codigoTransaccion: "' + codigoTransaccion + '", procesamiento: "' + procesamiento + '" }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code !== "600") {
                if (response.Code === "200") {
                    if (parseInt(response.TotalProcesado) < parseInt(lengthProcesamiento)) {
                        ajaxViewPartialLoaderProcesoMasivo(controller, response.Message);
                    }
                    else {
                        ajaxViewPartialLoaderProcesoMasivo(controller, response.Message);
                        setTimeout(function () {
                            ajaxLoaderCrearSolicitud(controller, codigoTransaccion, plantilla, "Contrato");
                        }, 500);
                    }
                }
                else
                {
                    ajaxViewPartialLoaderErrorGeneric(controller, response.Message);
                }
            }
            else
            {
                $("#modelExpiracion").modal("show");
            }
        },
        error: function (xhr) {
            $("#loaderProceso").html('');
            ajaxViewPartialLoaderErrorGeneric(controller, "Ha ocurrido algo inesperado en la plataforma, favor comunicarse con el area de TI, para ser solucionado el problema. Favor refrescar la pagina para evitar problemas de encolamiento de información.");
        },
        complete: function () {
        }
    });
}

function ajaxCreaRenovacion(controller, codigoTransaccion, procesamiento, lengthProcesamiento, procesoValidacion, plantilla) {
    $.ajax({
        type: 'POST',
        url: controller + 'CreaRenovacion',
        data: '{ codigoTransaccion: "' + codigoTransaccion + '", procesamiento: "' + procesamiento + '" }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code !== "600") {
                if (response.Code === "200") {
                    if (parseInt(response.TotalProcesado) < parseInt(lengthProcesamiento)) {
                        ajaxViewPartialLoaderProcesoMasivo(controller, response.Message);
                    }
                    else {
                        ajaxViewPartialLoaderProcesoMasivo(controller, response.Message);
                        setTimeout(function () {
                            ajaxLoaderCrearSolicitud(controller, codigoTransaccion, plantilla, "Renovacion");
                        }, 500);
                    }
                }
                else
                {
                    ajaxViewPartialLoaderErrorGeneric(controller, response.Message);
                }
            }
            else
            {
                $("#modelExpiracion").modal("show");
            }
        },
        error: function (xhr) {
            $("#loaderProceso").html('');
            ajaxViewPartialLoaderErrorGeneric(controller, "Ha ocurrido algo inesperado en la plataforma, favor comunicarse con el area de TI, para ser solucionado el problema. Favor refrescar la pagina para evitar problemas de encolamiento de información.");
        },
        complete: function () {
        }
    });
}

function ajaxCreaAnexos(controller, codigoTransaccion, procesamiento, lengthProcesamiento, procesoValidacion, plantilla) {
    $.ajax({
        type: 'POST',
        url: controller + 'CreaAnexo',
        data: '{ codigoTransaccion: "' + codigoTransaccion + '", procesamiento: "' + procesamiento + '", template: "' + plantilla + '" }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code !== "600") {
                if (response.Code === "200") {
                    if (parseInt(response.TotalProcesado) < parseInt(lengthProcesamiento)) {
                        ajaxViewPartialLoaderProcesoMasivo(controller, response.Message);
                    }
                    else {
                        alert("ejecutado");
                        ajaxViewPartialLoaderProcesoMasivo(controller, response.Message);
                        setTimeout(function () {
                            ajaxLoaderCrearSolicitud(controller, codigoTransaccion, plantilla, "Anexo");
                        }, 500);
                    }
                }
                else {
                    ajaxViewPartialLoaderErrorGeneric(controller, response.Message);
                }
            }
            else {
                $("#modelExpiracion").modal("show");
            }
        },
        error: function (xhr) {
            $("#loaderProceso").html('');
            ajaxViewPartialLoaderErrorGeneric(controller, "Ha ocurrido algo inesperado en la plataforma, favor comunicarse con el area de TI, para ser solucionado el problema. Favor refrescar la pagina para evitar problemas de encolamiento de información.");
        },
        complete: function () {
        }
    });
}

function ajaxCreaBaja(controller, codigoTransaccion, procesamiento, lengthProcesamiento, procesoValidacion, plantilla) {
    $.ajax({
        type: 'POST',
        url: controller + 'CrearBaja',
        data: '{ codigoTransaccion: "' + codigoTransaccion + '", procesamiento: "' + procesamiento + '", plantilla: "' + plantilla + '" }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code !== "600") {
                if (response.Code === "200") {
                    if (parseInt(response.TotalProcesado) < parseInt(lengthProcesamiento)) {
                        ajaxViewPartialLoaderProcesoMasivo(controller, response.Message);
                    }
                    else {
                        alert("ejecutado");
                        ajaxViewPartialLoaderProcesoMasivo(controller, response.Message);
                        setTimeout(function () {
                            ajaxLoaderCrearSolicitud(controller, codigoTransaccion, plantilla, "Bajas");
                        }, 500);
                    }
                }
                else {
                    ajaxViewPartialLoaderErrorGeneric(controller, response.Message);
                }
            }
            else {
                $("#modelExpiracion").modal("show");
            }
        },
        error: function (xhr) {
            $("#loaderProceso").html('');
            ajaxViewPartialLoaderErrorGeneric(controller, "Ha ocurrido algo inesperado en la plataforma, favor comunicarse con el area de TI, para ser solucionado el problema. Favor refrescar la pagina para evitar problemas de encolamiento de información.");
        },
        complete: function () {
        }
    });
}

function ajaxLoaderCrearSolicitud(controller, codigoTransaccion, plantilla, event) {
    $.ajax({
        type: 'POST',
        url: controller + 'LoaderCrearSolicitud',
        data: '{ codigoTransaccion: "' + codigoTransaccion + '", plantilla: "' + plantilla + '" }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code !== "600") {
                if (response.Code === "200") {
                    ajaxViewPartialLoaderProcesoMasivo(controller, response.Message);
                    setTimeout(function () {
                        switch (event) {
                            case "Contrato":
                                ajaxCreaSolicitudContrato(controller, codigoTransaccion, plantilla, event);
                                break;
                            case "Renovacion":
                                ajaxCreaSolicitudRenovacion(controller, codigoTransaccion, plantilla, event);
                                break;
                            case "Bajas":
                                ajaxCreaSolicitudBaja(controller, codigoTransaccion, plantilla, event);
                                break;
                            case "Anexo":
                                $("#loaderProceso").html('');
                                //ajaxCreaSolicitudAnexos(controller, codigoTransaccion, plantilla, event);
                                break;
                        }
                    }, 500);
                }
                else
                {
                    ajaxViewPartialLoaderErrorGeneric(controller, response.Message);
                }
            }
            else
            {
                $("#modelExpiracion").modal("show");
            }
        },
        error: function (xhr) {
            $("#loaderProceso").html('');
            ajaxViewPartialLoaderErrorGeneric(controller, "Ha ocurrido algo inesperado en la plataforma, favor comunicarse con el area de TI, para ser solucionado el problema. Favor refrescar la pagina para evitar problemas de encolamiento de información.");
        },
        complete: function () {
        }
    });
}

function ajaxCreaSolicitudBaja(controller, codigoTransaccion, plantilla, event) {
    $.ajax({
        type: 'POST',
        url: controller + 'CreaSolicitudBaja',
        data: '{ codigoTransaccion: "' + codigoTransaccion + '" }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code !== "600") {
                if (response.Code === "200") {
                    ajaxLoaderEnviarSolicitud(controller, codigoTransaccion, plantilla, response.PathFile, event);
                }
                else {
                    ajaxViewPartialLoaderErrorGeneric(controller, response.Message);
                }
            }
            else {
                $("#modelExpiracion").modal("show");
            }
        },
        error: function (xhr) {
            $("#loaderProceso").html('');
            ajaxViewPartialLoaderErrorGeneric(controller, "Ha ocurrido algo inesperado en la plataforma, favor comunicarse con el area de TI, para ser solucionado el problema. Favor refrescar la pagina para evitar problemas de encolamiento de información.");
        },
        complete: function () {
        }
    });
}

function ajaxCreaSolicitudRenovacion(controller, codigoTransaccion, plantilla, event) {
    $.ajax({
        type: 'POST',
        url: controller + 'CreaSolicitudRenovacion',
        data: '{ codigoTransaccion: "' + codigoTransaccion + '", plantilla: "' + plantilla + '" }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code !== "600") {
                if (response.Code === "200") {
                    ajaxLoaderEnviarSolicitud(controller, codigoTransaccion, plantilla, response.PathFile, event);
                }
                else
                {
                    ajaxViewPartialLoaderErrorGeneric(controller, response.Message);
                }
            }
            else
            {
                $("#modelExpiracion").modal("show");
            }
        },
        error: function (xhr) {
            $("#loaderProceso").html('');
            ajaxViewPartialLoaderErrorGeneric(controller, "Ha ocurrido algo inesperado en la plataforma, favor comunicarse con el area de TI, para ser solucionado el problema. Favor refrescar la pagina para evitar problemas de encolamiento de información.");
        },
        complete: function () {
        }
    });
}

function ajaxCreaSolicitudContrato(controller, codigoTransaccion, plantilla, event) {
    $.ajax({
        type: 'POST',
        url: controller + 'CreaSolicitudContrato',
        data: '{ codigoTransaccion: "' + codigoTransaccion + '", plantilla: "' + plantilla + '" }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code !== "600"){
                if (response.Code === "200") {
                    ajaxLoaderEnviarSolicitud(controller, codigoTransaccion, plantilla, response.PathFile, event);
                }
                else
                {
                    ajaxViewPartialLoaderErrorGeneric(controller, response.Message);
                }
            }
            else {
                $("#modelExpiracion").modal("show");
            }
        },
        error: function (xhr) {
            $("#loaderProceso").html('');
            ajaxViewPartialLoaderErrorGeneric(controller, "Ha ocurrido algo inesperado en la plataforma, favor comunicarse con el area de TI, para ser solucionado el problema. Favor refrescar la pagina para evitar problemas de encolamiento de información.");
        },
        complete: function () {
        }
    });
}

function ajaxLoaderEnviarSolicitud(controller, codigoTransaccion, plantilla, fileCreado, event) {
    $.ajax({
        type: 'POST',
        url: controller + 'LoaderEnviarSolicitud',
        data: '{ codigoTransaccion: "' + codigoTransaccion + '", plantilla: "' + plantilla + '" }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code !== "600") {
                if (response.Code === "200") {
                    ajaxViewPartialLoaderProcesoMasivo(controller, response.Message);
                    setTimeout(function () {
                        switch (event) {
                            case "Contrato":
                                ajaxEnviaSolicitudContrato(controller, codigoTransaccion, fileCreado);
                                break;
                            case "Renovacion":
                                ajaxEnviaSolicitudRenovacion(controller, codigoTransaccion, fileCreado);
                                break;
                            case "Bajas":
                                ajaxEnviaSolicitudBajas(controller, codigoTransaccion, fileCreado);
                                break;
                        }
                    }, 500);
                }
                else {
                    ajaxViewPartialLoaderErrorGeneric(controller, response.Message);
                }
            }
            else {
                $("#modelExpiracion").modal("show");
            }
        },
        error: function (xhr) {
            $("#loaderProceso").html('');
            ajaxViewPartialLoaderErrorGeneric(controller, "Ha ocurrido algo inesperado en la plataforma, favor comunicarse con el area de TI, para ser solucionado el problema. Favor refrescar la pagina para evitar problemas de encolamiento de información.");
        },
        complete: function () {
        }
    });
}

function ajaxEnviaSolicitudRenovacion(controller, codigoTransaccion, fileCreado) {
    $.ajax({
        type: 'POST',
        url: controller + 'EnviaSolicitudRenovacion',
        data: '{ codigoTransaccion: "' + codigoTransaccion + '", pathFile: "' + fileCreado + '" }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code !== "600") {
                if (response.Code === "200") {
                    setTimeout(function () {
                        $("#loaderProceso").html('');
                        sessionStorage.clear();
                    }, 500);
                }
                else {
                    ajaxViewPartialLoaderErrorGeneric(controller, response.Message);
                }
            }
            else {
                $("#modelExpiracion").modal("show");
            }
        },
        error: function (xhr) {
            $("#loaderProceso").html('');
            ajaxViewPartialLoaderErrorGeneric(controller, "Ha ocurrido algo inesperado en la plataforma, favor comunicarse con el area de TI, para ser solucionado el problema. Favor refrescar la pagina para evitar problemas de encolamiento de información.");
        },
        complete: function () {
        }
    });
}

function ajaxEnviaSolicitudContrato(controller, codigoTransaccion, fileCreado) {
    $.ajax({
        type: 'POST',
        url: controller + 'EnviaSolicitudContrato',
        data: '{ codigoTransaccion: "' + codigoTransaccion + '", pathFile: "' + fileCreado + '" }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code !== "600") {
                if (response.Code === "200") {
                    setTimeout(function () {
                        $("#loaderProceso").html('');
                        sessionStorage.clear();
                    }, 500);
                }
                else {
                    ajaxViewPartialLoaderErrorGeneric(controller, response.Message);
                }
            }
            else {
                $("#modelExpiracion").modal("show");
            }
        },
        error: function (xhr) {
            $("#loaderProceso").html('');
            ajaxViewPartialLoaderErrorGeneric(controller, "Ha ocurrido algo inesperado en la plataforma, favor comunicarse con el area de TI, para ser solucionado el problema. Favor refrescar la pagina para evitar problemas de encolamiento de información.");
        },
        complete: function () {
        }
    });
}

function ajaxEnviaSolicitudBajas(controller, codigoTransaccion, fileCreado) {
    $.ajax({
        type: 'POST',
        url: controller + 'EnviaSolicitudBajas',
        data: '{ codigoTransaccion: "' + codigoTransaccion + '", pathFile: "' + fileCreado + '" }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code !== "600") {
                if (response.Code === "200") {
                    setTimeout(function () {
                        $("#loaderProceso").html('');
                        sessionStorage.clear();
                    }, 500);
                }
                else {
                    ajaxViewPartialLoaderErrorGeneric(controller, response.Message);
                }
            }
            else {
                $("#modelExpiracion").modal("show");
            }
        },
        error: function (xhr) {
            $("#loaderProceso").html('');
            ajaxViewPartialLoaderErrorGeneric(controller, "Ha ocurrido algo inesperado en la plataforma, favor comunicarse con el area de TI, para ser solucionado el problema. Favor refrescar la pagina para evitar problemas de encolamiento de información.");
        },
        complete: function () {
        }
    });
}

function ajaxRenderizaProcesos(controller, pagination, tipoSolicitud, htmlHeader, htmlRenderizado, htmlPagination, htmlRenderizadoSearch, typeFilter, dataFilter, typeDashboard,
                day, month, year, analista, tipoProceso, estado) {
    $.ajax({
        type: 'POST',
        url: controller + 'RenderizaProcesos',
        data: '{ pagination: "' + pagination + '", tipoSolicitud: "' + tipoSolicitud + '", typeFilter: "' + typeFilter + '", dataFilter: "' + dataFilter + '", typeDashboard: "' + typeDashboard + '", ' +
            'day: "' + day + '", month: "' + month + '", year: "' + year + '",  analista: "' + analista + '", tipoProceso: "' + tipoProceso + '", estado: "' + estado + '" }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code !== "600") {
                if (response.Code === "200") {
                    $("#interactionplataforma").hide();
                    $(htmlHeader).html('');
                    $(htmlRenderizado).html('');
                    $(htmlPagination).html('');
                    for (var k = 0; k < response.Headers.length; k++) {
                        ajaxViewPartialHeaderProcesos(controller, htmlHeader, response.Headers[k].HtmlSearchElement, response.Headers[k].PostTitulo, response.Headers[k].TipoSolicitud, 
                            response.Headers[k].HtmlRenderizado, response.Headers[k].HtmlPagination, response.Headers[k].ResultSearch);
                    }

                    for (var i = 0; i < response.Procesos.length; i++) {
                        ajaxViewPartialProcesos(controller, htmlRenderizado, tipoProceso, response.Procesos[i].CodificarCod, response.Procesos[i].BorderColor, response.Procesos[i].GlyphiconColor, 
                            response.Procesos[i].Glyphicon, response.Procesos[i].Prioridad, response.Procesos[i].NombreProceso, response.Procesos[i].Creado, 
                            response.Procesos[i].EjecutivoCreador, response.Procesos[i].TotalSolicitudes, response.Procesos[i].ColorComentarios, response.Procesos[i].Comentarios,
                            response.Procesos[i].CodigoTransaccion, response.Procesos[i].TipoEvento, response.Procesos[i].OptDescargarDatosCargados, response.Procesos[i].OptAsignarProceso,
                            response.Procesos[i].OptDescargarSolicitudContrato, response.Procesos[i].OptHistorialProceso, response.Procesos[i].OptRevertirAnulacion,
                            response.Procesos[i].OptDescargarErrorDatosCargados, response.Procesos[i].OptAnularProceso, response.Procesos[i].OptTerminarProceso, response.Procesos[i].OptRevertirTermino);
                    }

                    for (var j = 0; j < response.Pagination.length; j++) {
                        ajaxViewPartialPagination(controller, htmlPagination, response.Pagination[j].FirstItem, response.Pagination[j].PreviousItem,
                            response.Pagination[j].Codificar, response.Pagination[j].NumeroPagina, response.Pagination[j].NextItem, response.Pagination[j].LastItem,
                            response.Pagination[j].TotalItems, response.Pagination[j].Active, response.Pagination[j].TipoSolicitud, response.Pagination[j].HtmlRenderizado,
                            response.Pagination[j].HtmlPagination, response.Pagination[j].HtmlSearchElement, response.Pagination[j].HtmlEventAction);

                    }

                    ajaxRenderizaHeaderProcesos(controller, "#" + htmlRenderizadoSearch, tipoSolicitud);
                }
                else {
                    $(htmlHeader).html('');
                    $(htmlRenderizado).html('');
                    $(htmlPagination).html('');
                    for (var l = 0; l < response.Headers.length; l++) {
                        ajaxViewPartialHeaderProcesos(controller, htmlHeader, response.Headers[l].HtmlSearchElement, response.Headers[l].PostTitulo, response.Headers[l].TipoSolicitud,
                            response.Headers[l].HtmlRenderizado, response.Headers[l].HtmlPagination, response.Headers[l].ResultSearch);
                    }

                    for (var h = 0; h < response.Procesos.length; h++) {
                        ajaxViewPartialNoEncontrado(controller, htmlRenderizado, response.Procesos[h].Glyphicon, response.Procesos[h].Message);
                    }
                }
            }
            else {
                $("#modelExpiracion").modal("show");
            }
        },
        error: function (xhr) {
            ajaxViewPartialLoaderErrorGeneric(controller, "Ha ocurrido algo inesperado en la plataforma, favor comunicarse con el area de TI, para ser solucionado el problema. Favor refrescar la pagina para evitar problemas de encolamiento de información.");
        },
        complete: function () {
            $("#loaderSearch").html('');
            $('html, body').animate({
                scrollTop: $(htmlHeader).offset().top - 70
            }, 1000);
        }
    });
}

function ajaxRenderizaSolicitudesPro(controller, pagination, tipoSolicitud, htmlRenderizado, htmlPagination, codigoTransaccion, typeFilter, dataFilter) {
    $.ajax({
        type: 'POST',
        url: controller + 'RenderizaSolicitudes',
        data: '{ pagination: "' + pagination + '", tipoSolicitud: "' + tipoSolicitud + '", codigoTransaccion: "' + codigoTransaccion + '", typeFilter: "' + typeFilter + '", dataFilter: "' + dataFilter + '" }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code !== "600") {
                if (response.Code === "200") {
                    $(htmlRenderizado).html('');
                    $(htmlPagination).html('');
                    for (var i = 0; i < response.Solicitudes.length; i++) {
                        ajaxViewPartialSolicitudesPro(controller, htmlRenderizado,
                            response.Solicitudes[i].NombreSolicitud,
                            response.Solicitudes[i].NombreProceso,
                            response.Solicitudes[i].TipoEvento,
                            response.Solicitudes[i].Creado,
                            response.Solicitudes[i].FechasCompromiso,
                            response.Solicitudes[i].Comentarios,
                            response.Solicitudes[i].CodificarCod,
                            response.Solicitudes[i].CodigoSolicitud,
                            response.Solicitudes[i].Prioridad,
                            response.Solicitudes[i].Glyphicon,
                            response.Solicitudes[i].GlyphiconColor,
                            response.Solicitudes[i].BorderColor,
                            response.Solicitudes[i].ColorFont,
                            response.Solicitudes[i].RenderizadoOpciones,
                            response.Solicitudes[i].OptDescargarDatosCargados,
                            response.Solicitudes[i].OptAsignarSolicitud,
                            response.Solicitudes[i].OptDescargarSolicitudContratoIndividual,
                            response.Solicitudes[i].OptHistorialSolicitud,
                            response.Solicitudes[i].OptRevertirAnulacion,
                            response.Solicitudes[i].OptDescargarErrorDatosCargados,
                            response.Solicitudes[i].OptAnularSolicitud,
                            response.Solicitudes[i].OptRevertirTermino);
                    }

                    for (var j = 0; j < response.Pagination.length; j++) {
                        ajaxViewPartialPagination(controller, htmlPagination, response.Pagination[j].FirstItem, response.Pagination[j].PreviousItem,
                            response.Pagination[j].Codificar, response.Pagination[j].NumeroPagina, response.Pagination[j].NextItem, response.Pagination[j].LastItem,
                            response.Pagination[j].TotalItems, response.Pagination[j].Active, response.Pagination[j].TipoSolicitud, response.Pagination[j].HtmlRenderizado,
                            response.Pagination[j].HtmlPagination, response.Pagination[j].HtmlSearchElement, response.Pagination[j].HtmlEventAction);
                    }

                }
                else {
                    ajaxViewPartialLoaderErrorGeneric(controller, response.Message);
                }
            }
            else {
                $("#modelExpiracion").modal("show");
            }
        },
        error: function (xhr) {
            ajaxViewPartialLoaderErrorGeneric(controller, "Ha ocurrido algo inesperado en la plataforma, favor comunicarse con el area de TI, para ser solucionado el problema. Favor refrescar la pagina para evitar problemas de encolamiento de información.");
        },
        complete: function () {
            $("#loaderSearch").html('');
        }
    });
}

function ajaxRenderizaHeaderProcesos(controller, elementHtml, tipoSolicitud) {
    $.ajax({
        type: 'POST',
        url: controller + 'RenderizaHeaderProcesos',
        data: '{ tipoSolicitud: "' + tipoSolicitud + '" }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code !== "600") {
                if (response.Code === "200") {
                    $(elementHtml).html(response.Result);
                }
                else {
                    ajaxViewPartialLoaderErrorGeneric(controller, response.Message);
                }
            }
            else {
                $("#modelExpiracion").modal("show");
            }
        },
        error: function (xhr) {
            ajaxViewPartialLoaderErrorGeneric(controller, "Ha ocurrido algo inesperado en la plataforma, favor comunicarse con el area de TI, para ser solucionado el problema. Favor refrescar la pagina para evitar problemas de encolamiento de información.");
        },
        complete: function () {
        }
    });
}

function ajaxViewPartialProcesos(controller, elementHtml, tipoProceso, codificarCod, borderColor, glyphiconColor, glyphicon, prioridad, nombre, fechaCreacion,
    ejecutivoSeguimiento, totalSolicitudes, colorComentarios, comentarios, codigoTransaccion, tipoEvento, optDescargarDatosCargados, optAsignarProceso,
    optDescargarSolicitudContrato, optHistorialProceso, optRevertirAnulacion, optDescargarErrorDatosCargados, optAnularProceso, optTerminarProceso, optRevertirTermino) {
    $.ajax({
        type: 'POST',
        url: controller + 'ViewPartialProcesos',
        data: '{ TipoProceso: "' + tipoProceso + '", codificarCod: "' + codificarCod + '", borderColor: "' + borderColor + '", glyphiconColor: "' + glyphiconColor + '", ' +
            'glyphicon: "' + glyphicon + '", prioridad: "' + prioridad + '", nombre: "' + nombre + '", fechaCreacion: "' + fechaCreacion + '", ' +
            'ejecutivoSeguimiento: "' + ejecutivoSeguimiento + '", totalSolicitudes: "' + totalSolicitudes + '",  colorComentarios: "' + colorComentarios + '", ' +
            'comentarios: "' + comentarios + '", codigoTransaccion: "' + codigoTransaccion + '", tipoEvento: "' + tipoEvento + '", optDescargarDatosCargados: "' + optDescargarDatosCargados + '", optAsignarProceso: "' + optAsignarProceso + '", ' +
            'optDescargarSolicitudContrato: "' + optDescargarSolicitudContrato + '", optHistorialProceso: "' + optHistorialProceso + '", optRevertirAnulacion: "' + optRevertirAnulacion + '", optDescargarErrorDatosCargados: "' + optDescargarErrorDatosCargados + '", ' +
            'optAnularProceso: "' + optAnularProceso + '", optTerminarProceso: "' + optTerminarProceso + '", optRevertirTermino: "' + optRevertirTermino + '" }', 
        dataType: 'json',
        contentType: 'application/json',
        async: false,
        error: function (xhr) {
            $(elementHtml).append(xhr.responseText);
        },
        complete: function () {
        }
    });
}

function ajaxViewPartialSolicitudesPro(controller, elementHtml, nombreSolicitud, nombreProceso, tipoEvento, creado, fechasCompromiso, comentarios, codificarCod, codigoSolicitud,
                            prioridad, glyphicon, glyphiconColor, borderColor, colorFont, renderizadoOpciones, 
                            optDescargarDatosCargados, optAsignarSolicitud, optDescargarSolicitudContratoIndividual,
    optHistorialSolicitud, optRevertirAnulacion, optDescargarErrorDatosCargados, optAnularSolicitud, optRevertirTermino) {
    $.ajax({
        type: 'POST',
        url: controller + 'ViewPartialSolicitudes',
        data: '{ nombreSolicitud: "' + nombreSolicitud + '", nombreProceso: "' + nombreProceso + '", tipoEvento: "' + tipoEvento + '", creado: "' + creado + '", fechasCompromiso: "' + fechasCompromiso + '", ' +
            ' comentarios: "' + comentarios + '", codificarCod: "' + codificarCod + '", codigoSolicitud: "' + codigoSolicitud + '", prioridad: "' + prioridad + '", glyphicon: "' + glyphicon + '", glyphiconColor: "' + glyphiconColor + '", ' +
            ' borderColor: "' + borderColor + '", colorFont: "' + colorFont + '", renderizadoOpciones: "' + renderizadoOpciones + '", optDescargarDatosCargados: "' + optDescargarDatosCargados + '", ' +
            ' optAsignarSolicitud: "' + optAsignarSolicitud + '", optDescargarSolicitudContratoIndividual: "' + optDescargarSolicitudContratoIndividual + '", ' +
            ' optHistorialSolicitud: "' + optHistorialSolicitud + '", optRevertirAnulacion: "' + optRevertirAnulacion + '", ' +
            ' optDescargarErrorDatosCargados: "' + optDescargarErrorDatosCargados + '", optAnularSolicitud: "' + optAnularSolicitud + '", optRevertirTermino: "' + optRevertirTermino + '" }', 
        dataType: 'json',
        contentType: 'application/json',
        async: false,
        error: function (xhr) {
            $(elementHtml).append(xhr.responseText);
        }
    });
}

function ajaxViewPartialHeaderProcesos(controller, elementHtml, htmlSearchElement, postTitulo, tipoSolicitud, htmlRenderizado, htmlPagination, resultSearch) {
    $.ajax({
        type: 'POST',
        url: controller + 'ViewPartialHeaderProcesos',
        data: '{ htmlSearchElement: "' + htmlSearchElement + '", postTitulo: "' + postTitulo + '", tipoSolicitud: "' + tipoSolicitud + '", htmlRenderizado: "' + htmlRenderizado + '", ' +
            'htmlPagination: "' + htmlPagination + '", resultSearch: "' + resultSearch + '" }',
        dataType: 'json',
        contentType: 'application/json',
        async: false,
        error: function (xhr) {
            $(elementHtml).append(xhr.responseText);
        }
    });
}

function ajaxViewPartialPagination(controller, elementHtml, firstItem, previousItem, codificar, numeroPagina, nextItem, lastItem, totalItems, active, tipoSolicitud, htmlRenderizado, htmlPagination, htmlSearchElement, htmlEventAction) {
    $.ajax({
        type: 'POST',
        url: controller + 'ViewPartialPagination',
        data: '{ firstItem: "' + firstItem + '", previousItem: "' + previousItem + '", codificar: "' + codificar + '", numeroPagina: "' + numeroPagina + '", ' +
            'nextItem: "' + nextItem + '", lastItem: "' + lastItem + '", totalItems: "' + totalItems + '", active: "' + active + '", tipoSolicitud: "' + tipoSolicitud + '", ' +
            'htmlRenderizado: "' + htmlRenderizado + '", htmlPagination: "' + htmlPagination + '", htmlSearchElement: "' + htmlSearchElement + '", htmlEventAction: "' + htmlEventAction + '" }',
        dataType: 'json',
        contentType: 'application/json',
        async: false,
        error: function (xhr) {
            $(elementHtml).append(xhr.responseText);
        }
    });
}

function ajaxViewPartialLoaderProcesoMasivo(controller, message) {
    $.ajax({
        type: 'POST',
        url: controller + 'ViewPartialLoaderProcesoMasivo',
        data: '{ message: "' + message.split('"').join("'") + '" }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        error: function (xhr) {
            $("#loaderProceso").html(xhr.responseText);
        }
    });
}

function ajaxViewPartialLoaderErrorGeneric(controller, message) {
    $.ajax({
        type: 'POST',
        url: controller + 'ViewPartialLoaderErrorGeneric',
        data: '{ message: "' + message.split('"').join("'") + '" }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        error: function (xhr) {
            $("#messagePlataforma").html(xhr.responseText);
        }
    });
}

function ajaxViewPartialLoaderComplete(controller, htmlElement, message, proceso) {
    $.ajax({
        type: 'POST',
        url: controller + 'ViewPartialLoaderComplete',
        data: '{ message: "' + message + '", proceso: "' + proceso + '" }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        error: function (xhr) {
            $(htmlElement).html(xhr.responseText);
            sessionStorage.setItem("ApplicationNombreProceso", "");
            sessionStorage.setItem("ApplicationMessageProceso", "");
        }
    });
}

function ajaxViewPartialLoaderTransaccion(controller, htmlElement) {
    $.ajax({
        type: 'POST',
        url: controller + 'ViewPartialLoaderTransaccion',
        data: '{ }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        error: function (xhr) {
            $(htmlElement).html(xhr.responseText);
        }
    });
}

function ajaxViewPartialLoaderErrorGenericModal(controller, modalElement, message) {
    $.ajax({
        type: 'POST',
        url: controller + 'ViewPartialLoaderErrorGeneric',
        data: '{ message: "' + message.split('"').join("'") + '" }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        error: function (xhr) {
            $(modalElement).html(xhr.responseText);
        }
    });
}

function ajaxInformacionCuenta(controller, idCliente) {
    $.ajax({
        type: 'POST',
        url: controller + 'InformacionCuenta',
        data: '{ idCliente: "' + idCliente + '" }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code !== "600") {
                if (response.Code === "200") {
                    console.log(response.Clientes);
                    for (var i = 0; i < response.Clientes.length; i++) {

                        $("#Nombre").html(response.Clientes[i].Nombre + ' ' + response.Clientes[i].Apellido);
                        $("#Id").html(response.Clientes[i].Id);
                        $("#ClienteId").html(response.Clientes[i].IdCliente);
                        $("#NombreCliente").html(response.Clientes[i].NombreCliente);
                        $("#Rut").html(response.Clientes[i].Rut);
                        $("#codigoCli").html(response.Clientes[i].Codigo);
                        $("#Empresa").html(response.Clientes[i].Empresa);
                        

                    }
                }
                else {
                    //ajaxViewPartialLoaderErrorGenericModal(controller, response.Message);
                }
            }
            else {
                $("#modelExpiracion").modal("show");
            }
        },
        error: function (xhr) {
            ajaxViewPartialLoaderErrorGenericModal(controller, "Ha ocurrido algo inesperado en la plataforma, favor comunicarse con el area de TI, para ser solucionado el problema. Favor refrescar la pagina para evitar problemas de encolamiento de información.");
        },
        complete: function () {
        }
    });
}

function ajaxAnularProceso(controller, codigoTransaccion, tipoProceso, motivoAnulacion, procesoAnulado) {
    $.ajax({
        type: 'POST',
        url: controller + 'AnularProceso',
        data: '{ codigoTransaccion: "' + codigoTransaccion + '", tipoProceso: "' + tipoProceso + '", motivoAnulacion: "' + motivoAnulacion + '" }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code !== "600") {
                if (response.Code === "200") {
                    switch (tipoProceso) {
                        case "Q29udHJhdG8=":
                            ajaxCreaSolicitudContratoAnulada(controller, codigoTransaccion, response.Path, response.Message, procesoAnulado);
                            break;
                        case "UmVub3ZhY2lvbg==":
                            ajaxCreaSolicitudRenovacionAnulada(controller, codigoTransaccion, response.Path, response.Message, procesoAnulado);
                            break;
                    }
                }
                else {
                    ajaxViewPartialLoaderErrorGenericModal(controller, response.Message);
                }
            }
            else {
                $("#modelExpiracion").modal("show");
            }
        },
        error: function (xhr) {
            $("#loaderProceso").html('');
            ajaxViewPartialLoaderErrorGenericModal(controller, "Ha ocurrido algo inesperado en la plataforma, favor comunicarse con el area de TI, para ser solucionado el problema. Favor refrescar la pagina para evitar problemas de encolamiento de información.");
        },
        complete: function () {
        }
    });
}

function ajaxTerminarProceso(controller, codigoTransaccion, tipoProceso, procesoTerminado) {
    $.ajax({
        type: 'POST',
        url: controller + 'TerminarProceso',
        data: '{ codigoTransaccion: "' + codigoTransaccion + '", tipoProceso: "' + tipoProceso + '" }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code !== "600") {
                if (response.Code === "200") {
                    ajaxEnviaInformacionProcesoTerminado(controller, codigoTransaccion, "", response.Proceso, tipoProceso, response.Message, procesoTerminado,
                        response.Glyphicon, response.GlyphiconColor, response.Estado, response.Border);
                }
                else {
                    ajaxViewPartialLoaderErrorGenericModal(controller, response.Message);
                }
            }
            else {
                $("#modelExpiracion").modal("show");
            }
        },
        error: function (xhr) {
            $("#loaderProceso").html('');
            ajaxViewPartialLoaderErrorGenericModal(controller, "Ha ocurrido algo inesperado en la plataforma, favor comunicarse con el area de TI, para ser solucionado el problema. Favor refrescar la pagina para evitar problemas de encolamiento de información.");
        },
        complete: function () {
        }
    });
}

function ajaxEnviaInformacionProcesoTerminado(controller, codigoTransaccion, pathFile, proceso, tipoProceso, message, nombreproceso, glyphicon, glyphiconColor, estado, border) {
    $.ajax({
        type: 'POST',
        url: controller + 'EnviaInformacionProcesoTerminado',
        data: '{ codigoTransaccion: "' + codigoTransaccion + '", pathFile: "' + pathFile + '", proceso: "' + proceso + '" }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code !== "600") {
                if (response.Code === "200") {
                    ajaxViewPartialLoaderComplete(controller, "#processtx_" + atob(tipoProceso).toLowerCase() + "_" + codigoTransaccion.split('=').join('_'),
                        message, nombreproceso);
                    $("#process_" + atob(tipoProceso).toLowerCase() + "_" + codigoTransaccion.split('=').join('_') + " > span").html('<i class="dspl-block pdt-5 pdl-10 pdr-10 pdb-5 family-teamwork fst-normal fw-bold ta-center" style="border: 1px solid rgb(92, 184, 92); color: rgb(92, 184, 92); font-size: 14px; border-radius: 30px;">Proceso Actualizado</i>');
                    $("#process_" + atob(tipoProceso).toLowerCase() + "_" + codigoTransaccion.split('=').join('_') + " .eventAnularSolicitud").hide();
                    $("#process_" + atob(tipoProceso).toLowerCase() + "_" + codigoTransaccion.split('=').join('_') + " .eventTerminarSolicitud").hide();
                    $("#process_" + atob(tipoProceso).toLowerCase() + "_" + codigoTransaccion.split('=').join('_') + " .estadoProceso")
                        .attr("class", "btn " + glyphiconColor + " dspl-inline-block mt-80 estadoProceso")
                        .html('<i class="glyphicon-tw-7 ' + glyphicon + '" style="display: inline-block;"></i><span class="dspl-inline-block mt-15neg"><i class="family-teamwork" style="font-size: 16px; font-style: normal; display: block; margin-bottom: -8px;">Proceso ' + estado + '</i></span>');
                    $("#process_" + atob(tipoProceso).toLowerCase() + "_" + codigoTransaccion.split('=').join('_')).attr("class", "row width-90per m-auto " + border + " bt-1-solid-200x3 br-1-solid-200x3 bb-1-solid-200x3 mheight-180 mt-2");
                    $("#process_" + atob(tipoProceso).toLowerCase() + "_" + codigoTransaccion.split('=').join('_') + " .estadoProcesoGlosaDet").html("Este proceso se encuentra con prioridad " + estado);
                    setTimeout(function () {
                        $("#processtx_" + atob(tipoProceso).toLowerCase() + "_" + codigoTransaccion.split('=').join('_')).html('');
                        $("#process_" + atob(tipoProceso).toLowerCase() + "_" + codigoTransaccion.split('=').join('_') + " > span").html('');
                    }, 8000);
                    $('html, body').animate({
                        scrollTop: $("#processtx_" + atob(tipoProceso).toLowerCase() + "_" + codigoTransaccion.split('=').join('_')).offset().top - 70
                    }, 1000);
                }
                else {
                    ajaxViewPartialLoaderErrorGenericModal(controller, response.Message);
                }
            }
            else {
                $("#modelExpiracion").modal("show");
            }
        },
        error: function (xhr) {
            $("#loaderProceso").html('');
            ajaxViewPartialLoaderErrorGenericModal(controller, "Ha ocurrido algo inesperado en la plataforma, favor comunicarse con el area de TI, para ser solucionado el problema. Favor refrescar la pagina para evitar problemas de encolamiento de información.");
        },
        complete: function () {
            $("#loaderSearch").html('');
        }
    });
}

function ajaxAnularSolicitud(controller, solicitud, tipoSolicitud, motivoAnulacion) {
    $.ajax({
        type: 'POST',
        url: controller + 'AnularSolicitud',
        data: '{ solicitud: "' + solicitud + '", tipoSolicitud: "' + tipoSolicitud + '", motivoAnulacion: "' + motivoAnulacion + '" }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code !== "600") {
                if (response.Code === "200") {
                    switch (tipoSolicitud) {
                        case "Q29udHJhdG8=":
                            ajaxEnviaSolicitudContratoAnuladoInd(controller, solicitud, "", response.Path);
                            break;
                        case "UmVub3ZhY2lvbg==":
                            ajaxEnviaSolicitudRenovacionAnuladoInd(controller, solicitud, "", response.Path);
                            break;
                    }
                }
                else {
                    ajaxViewPartialLoaderErrorGenericModal(controller, response.Message);
                }
            }
            else {
                $("#modelExpiracion").modal("show");
            }
        },
        error: function (xhr) {
            $("#loaderProceso").html('');
            ajaxViewPartialLoaderErrorGenericModal(controller, "Ha ocurrido algo inesperado en la plataforma, favor comunicarse con el area de TI, para ser solucionado el problema. Favor refrescar la pagina para evitar problemas de encolamiento de información.");
        },
        complete: function () {
        }
    });
}

function ajaxCreaSolicitudContratoAnulada(controller, codigoTransaccion, path, messageProcesoAnulado, procesoAnulado) {
    $.ajax({
        type: 'POST',
        url: controller + 'CreaSolicitudContratoAnulada',
        data: '{ codigoTransaccion: "' + codigoTransaccion + '" }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code !== "600") {
                if (response.Code === "200") {
                    ajaxEnviaSolicitudContratoAnulado(controller, codigoTransaccion, response.PathFile, path, messageProcesoAnulado, procesoAnulado);
                }
                else {
                    ajaxViewPartialLoaderErrorGenericModal(controller, response.Message);
                }
            }
            else {
                $("#modelExpiracion").modal("show");
            }
        },
        error: function (xhr) {
            $("#loaderProceso").html('');
            ajaxViewPartialLoaderErrorGeneric(controller, "Ha ocurrido algo inesperado en la plataforma, favor comunicarse con el area de TI, para ser solucionado el problema. Favor refrescar la pagina para evitar problemas de encolamiento de información.");
        },
        complete: function () {
        }
    });
}

function ajaxEnviaSolicitudContratoAnulado(controller, codigoTransaccion, fileCreado, path, messageProcesoAnulado, procesoAnulado) {
    $.ajax({
        type: 'POST',
        url: controller + 'EnviaSolicitudContratoAnulado',
        data: '{ codigoTransaccion: "' + codigoTransaccion + '", pathFile: "' + fileCreado + '" }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code !== "600") {
                if (response.Code === "200") {
                    sessionStorage.setItem("ApplicationProceso", procesoAnulado);
                    sessionStorage.setItem("ApplicationProceso", messageProcesoAnulado);
                    window.location.href = controller + path;
                }
                else {
                    ajaxViewPartialLoaderErrorGeneric(controller, response.Message);
                }
            }
            else {
                $("#modelExpiracion").modal("show");
            }
        },
        error: function (xhr) {
            $("#loaderProceso").html('');
            ajaxViewPartialLoaderErrorGeneric(controller, "Ha ocurrido algo inesperado en la plataforma, favor comunicarse con el area de TI, para ser solucionado el problema. Favor refrescar la pagina para evitar problemas de encolamiento de información.");
        },
        complete: function () {
        }
    });
}

function ajaxCreaSolicitudRenovacionAnulada(controller, codigoTransaccion, path, messageProcesoAnulado, procesoAnulado) {
    $.ajax({
        type: 'POST',
        url: controller + 'CreaSolicitudRenovacionAnulada',
        data: '{ codigoTransaccion: "' + codigoTransaccion + '" }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code !== "600") {
                if (response.Code === "200") {
                    ajaxEnviaSolicitudRenovacionAnulado(controller, codigoTransaccion, response.PathFile, path, messageProcesoAnulado, procesoAnulado);
                }
                else {
                    ajaxViewPartialLoaderErrorGenericModal(controller, response.Message);
                }
            }
            else {
                $("#modelExpiracion").modal("show");
            }
        },
        error: function (xhr) {
            $("#loaderProceso").html('');
            ajaxViewPartialLoaderErrorGeneric(controller, "Ha ocurrido algo inesperado en la plataforma, favor comunicarse con el area de TI, para ser solucionado el problema. Favor refrescar la pagina para evitar problemas de encolamiento de información.");
        },
        complete: function () {
        }
    });
}

function ajaxEnviaSolicitudRenovacionAnulado(controller, codigoTransaccion, fileCreado, path, messageProcesoAnulado, procesoAnulado) {
    $.ajax({
        type: 'POST',
        url: controller + 'EnviaSolicitudRenovacionAnulado',
        data: '{ codigoTransaccion: "' + codigoTransaccion + '", pathFile: "' + fileCreado + '" }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code !== "600") {
                if (response.Code === "200") {
                    sessionStorage.setItem("ApplicationNombreProceso", procesoAnulado);
                    sessionStorage.setItem("ApplicationMessageProceso", messageProcesoAnulado);
                    window.location.href = controller + path;
                }
                else {
                    ajaxViewPartialLoaderErrorGeneric(controller, response.Message);
                }
            }
            else {
                $("#modelExpiracion").modal("show");
            }
        },
        error: function (xhr) {
            $("#loaderProceso").html('');
            ajaxViewPartialLoaderErrorGeneric(controller, "Ha ocurrido algo inesperado en la plataforma, favor comunicarse con el area de TI, para ser solucionado el problema. Favor refrescar la pagina para evitar problemas de encolamiento de información.");
        },
        complete: function () {
        }
    });
}

function ajaxEnviaSolicitudContratoAnuladoInd(controller, codigoTransaccion, fileCreado, path) {
    $.ajax({
        type: 'POST',
        url: controller + 'EnviaSolicitudContratoAnuladoInd',
        data: '{ codigoTransaccion: "' + codigoTransaccion + '", pathFile: "' + fileCreado + '" }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code !== "600") {
                if (response.Code === "200") {
                    window.location.href = controller + path;
                }
                else {
                    ajaxViewPartialLoaderErrorGeneric(controller, response.Message);
                }
            }
            else {
                $("#modelExpiracion").modal("show");
            }
        },
        error: function (xhr) {
            $("#loaderProceso").html('');
            ajaxViewPartialLoaderErrorGeneric(controller, "Ha ocurrido algo inesperado en la plataforma, favor comunicarse con el area de TI, para ser solucionado el problema. Favor refrescar la pagina para evitar problemas de encolamiento de información.");
        },
        complete: function () {
        }
    });
}

function ajaxEnviaSolicitudRenovacionAnuladoInd(controller, codigoTransaccion, fileCreado, path) {
    $.ajax({
        type: 'POST',
        url: controller + 'EnviaSolicitudRenovacionAnuladoInd',
        data: '{ codigoTransaccion: "' + codigoTransaccion + '", pathFile: "' + fileCreado + '" }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code !== "600") {
                if (response.Code === "200") {
                    window.location.href = controller + path;
                }
                else {
                    ajaxViewPartialLoaderErrorGeneric(controller, response.Message);
                }
            }
            else {
                $("#modelExpiracion").modal("show");
            }
        },
        error: function (xhr) {
            $("#loaderProceso").html('');
            ajaxViewPartialLoaderErrorGeneric(controller, "Ha ocurrido algo inesperado en la plataforma, favor comunicarse con el area de TI, para ser solucionado el problema. Favor refrescar la pagina para evitar problemas de encolamiento de información.");
        },
        complete: function () {
        }
    });
}

function ajaxRevertirTerminoProceso(controller, codigoTransaccion, tipoProceso, procesoTerminado) {
    $.ajax({
        type: 'POST',
        url: controller + 'RevertirTerminoProceso',
        data: '{ codigoTransaccion: "' + codigoTransaccion + '", tipoProceso: "' + tipoProceso + '", proceso: "' + procesoTerminado + '" }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code !== "600") {
                if (response.Code === "200") {
                    ajaxEnviaInformacionProcesoReversionTermino(controller, codigoTransaccion, "", response.Proceso, tipoProceso, "UHJvY2Vzbw", response.Message, procesoTerminado,
                        response.Glyphicon, response.GlyphiconColor, response.Estado, response.Border);
                }
                else if (response.Code === "400") {
                    $("#loaderSearch").html('');
                    $("#errorText").html(response.Message);
                    $("#modalErrorRevertirTerminado").modal("show");
                }
                else {
                    ajaxViewPartialLoaderErrorGenericModal(controller, response.Message);
                }
            }
            else {
                $("#modelExpiracion").modal("show");
            }
        },
        error: function (xhr) {
            $("#loaderProceso").html('');
            ajaxViewPartialLoaderErrorGenericModal(controller, "Ha ocurrido algo inesperado en la plataforma, favor comunicarse con el area de TI, para ser solucionado el problema. Favor refrescar la pagina para evitar problemas de encolamiento de información.");
        },
        complete: function () {
        }
    });
}

function ajaxRevertirTerminoSolicitud(controller, codigoTransaccion, tipoProceso, procesoTerminado) {
    $.ajax({
        type: 'POST',
        url: controller + 'RevertirTerminoSolicitud',
        data: '{ codigoTransaccion: "' + codigoTransaccion + '", tipoProceso: "' + tipoProceso + '", proceso: "' + procesoTerminado + '" }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code !== "600") {
                if (response.Code === "200") {
                    ajaxEnviaInformacionProcesoReversionTermino(controller, codigoTransaccion, "", response.Proceso, tipoProceso, "U29saWNpdHVk", response.Message, procesoTerminado,
                        response.Glyphicon, response.GlyphiconColor, response.Estado, response.Border);
                    location.reload();
                }
                else if (response.Code === "400") {
                    $("#loaderSearch").html('');
                    $("#errorText").html(response.Message);
                    $("#modalErrorRevertirTerminado").modal("show");
                }
                else {
                    ajaxViewPartialLoaderErrorGenericModal(controller, response.Message);
                }
            }
            else {
                $("#modelExpiracion").modal("show");
            }
        },
        error: function (xhr) {
            $("#loaderProceso").html('');
            ajaxViewPartialLoaderErrorGenericModal(controller, "Ha ocurrido algo inesperado en la plataforma, favor comunicarse con el area de TI, para ser solucionado el problema. Favor refrescar la pagina para evitar problemas de encolamiento de información.");
        },
        complete: function () {
        }
    });
}

function ajaxEnviaInformacionProcesoReversionTermino(controller, codigoTransaccion, pathFile, proceso, tipoProceso, tipo, message, nombreproceso, glyphicon, glyphiconColor, estado, border) {
    $.ajax({
        type: 'POST',
        url: controller + 'EnviaInformacionProcesoReversionTermino',
        data: '{ codigoTransaccion: "' + codigoTransaccion + '", pathFile: "' + pathFile + '", proceso: "' + proceso + '", tipoProceso: "' + tipoProceso + '", tipo: "' + tipo + '" }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code !== "600") {
                if (response.Code === "200") {
                    ajaxViewPartialLoaderComplete(controller, "#processtx_" + atob(tipoProceso).toLowerCase() + "_" + codigoTransaccion.split('=').join('_'),
                        message, nombreproceso);
                    $("#process_" + atob(tipoProceso).toLowerCase() + "_" + codigoTransaccion.split('=').join('_') + " > span").html('<i class="dspl-block pdt-5 pdl-10 pdr-10 pdb-5 family-teamwork fst-normal fw-bold ta-center" style="border: 1px solid rgb(92, 184, 92); color: rgb(92, 184, 92); font-size: 14px; border-radius: 30px;">Proceso Actualizado</i>');
                    $("#process_" + atob(tipoProceso).toLowerCase() + "_" + codigoTransaccion.split('=').join('_') + " .eventAnularSolicitud").hide();
                    $("#process_" + atob(tipoProceso).toLowerCase() + "_" + codigoTransaccion.split('=').join('_') + " .eventTerminarSolicitud").hide();
                    $("#process_" + atob(tipoProceso).toLowerCase() + "_" + codigoTransaccion.split('=').join('_') + " .estadoProceso")
                        .attr("class", "btn " + glyphiconColor + " dspl-inline-block mt-80 estadoProceso")
                        .html('<i class="glyphicon-tw-7 ' + glyphicon + '" style="display: inline-block;"></i><span class="dspl-inline-block mt-15neg"><i class="family-teamwork" style="font-size: 16px; font-style: normal; display: block; margin-bottom: -8px;">Proceso ' + estado + '</i></span>');
                    $("#process_" + atob(tipoProceso).toLowerCase() + "_" + codigoTransaccion.split('=').join('_')).attr("class", "row width-90per m-auto " + border + " bt-1-solid-200x3 br-1-solid-200x3 bb-1-solid-200x3 mheight-180 mt-2");
                    $("#process_" + atob(tipoProceso).toLowerCase() + "_" + codigoTransaccion.split('=').join('_') + " .estadoProcesoGlosaDet").html("Este proceso se encuentra con prioridad " + estado);
                    //setTimeout(function () {
                    //    $("#processtx_" + atob(tipoProceso).toLowerCase() + "_" + codigoTransaccion.split('=').join('_')).html('');
                    //    $("#process_" + atob(tipoProceso).toLowerCase() + "_" + codigoTransaccion.split('=').join('_') + " > span").html('');
                    //}, 8000);
                    //$('html, body').animate({
                    //    scrollTop: $("#processtx_" + atob(tipoProceso).toLowerCase() + "_" + codigoTransaccion.split('=').join('_')).offset().top - 70
                    //}, 1000);
                }
                else {
                    ajaxViewPartialLoaderErrorGenericModal(controller, response.Message);
                }
            }
            else {
                $("#modelExpiracion").modal("show");
            }
        },
        error: function (xhr) {
            $("#loaderProceso").html('');
            ajaxViewPartialLoaderErrorGenericModal(controller, "Ha ocurrido algo inesperado en la plataforma, favor comunicarse con el area de TI, para ser solucionado el problema. Favor refrescar la pagina para evitar problemas de encolamiento de información.");
        },
        complete: function () {
            $("#loaderSearch").html('');
        }
    });
}



function ajaxRenderizaAnexos(controller, htmlRenderizado, dataFilter) {
    $.ajax({
        type: 'POST',
        url: controller + 'RenderizaAnexos',
        data: '{ dataFilter: "' + dataFilter + '"  }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code !== "600") {
                if (response.Code === "200") {
                    $(htmlRenderizado).html('');

                    for (var i = 0; i < response.Anexos.length; i++) {
                        ajaxViewPartialAnexos(controller, htmlRenderizado, response.Anexos[i].RenderizadoTituloOne, response.Anexos[i].RenderizadoTituloTwo, response.Anexos[i].RenderizadoDescripcion, 
                            response.Anexos[i].Glyphicon, response.Anexos[i].GlyphiconColor, response.Anexos[i].BorderColor, 
                            response.Anexos[i].ColorFont, response.Anexos[i].ButtonClass,
                            response.Anexos[i].RenderizadoButtonOne, response.Anexos[i].RenderizadoButtonTwo, response.Anexos[i].Path);
                    }
                    
                }
                else {
                    /** error */
                }
            }
            else {
                $("#modelExpiracion").modal("show");
            }
        },
        error: function (xhr) {
            ajaxViewPartialLoaderErrorGeneric(controller, "Ha ocurrido algo inesperado en la plataforma, favor comunicarse con el area de TI, para ser solucionado el problema. Favor refrescar la pagina para evitar problemas de encolamiento de información.");
        },
        complete: function () {
        }
    });
}


function ajaxViewPartialAnexos(controller, elementHtml, renderizadoTituloOne, renderizadoTituloTwo, renderizadoDescripcion, glyphicon, glyphiconColor, borderColor, colorFont, buttonClass,
                               renderizadoButtonOne, renderizadoButtonTwo, path) {
    $.ajax({
        type: 'POST',
        url: controller + 'ViewPartialAnexos',
        data: '{ renderizadoTituloOne: "' + renderizadoTituloOne + '", renderizadoTituloTwo: "' + renderizadoTituloTwo + '", renderizadoDescripcion: "' + renderizadoDescripcion + '", ' +
            'glyphicon: "' + glyphicon + '", glyphiconColor: "' + glyphiconColor + '", borderColor: "' + borderColor + '", colorFont: "' + colorFont + '", ' +
            'buttonClass: "' + buttonClass + '", renderizadoButtonOne: "' + renderizadoButtonOne + '",  renderizadoButtonTwo: "' + renderizadoButtonTwo + '", path: "' + path + '" }',
        dataType: 'json',
        contentType: 'application/json',
        async: false,
        error: function (xhr) {
            $(elementHtml).append(xhr.responseText);
        }
    });
}



function ajaxRenderizaDashboardTotal(controller, elementHtml, tipoDashboard, day, month, year) {
    $.ajax({
        type: 'POST',
        url: controller + 'RenderizaDashboardTotal',
        data: '{ tipoDashboard: "' + tipoDashboard + '", day: "' + day + '", month: "' + month + '", year: "' + year + '"  }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code !== "600") {
                if (response.Code === "200") {
                    $(elementHtml).html('');

                    for (var i = 0; i < response.Estadisticas.length; i++) {
                        ajaxViewPartialDashboardSolicitudes(controller, elementHtml, response.Estadisticas[i].Total, response.Estadisticas[i].Concepto, response.Estadisticas[i].TipoProceso,
                            response.Estadisticas[i].Twest, response.Estadisticas[i].Twrrhh, response.Estadisticas[i].Twc);
                    }

                }
                else {
                    /** error */
                }
            }
            else {
                $("#modelExpiracion").modal("show");
            }
        },
        error: function (xhr) {
            ajaxViewPartialLoaderErrorGeneric(controller, "Ha ocurrido algo inesperado en la plataforma, favor comunicarse con el area de TI, para ser solucionado el problema. Favor refrescar la pagina para evitar problemas de encolamiento de información.");
        },
        complete: function () {
            $("#loaderSearch").html('');
        }
    });
}

function ajaxRenderizaDashboardAnalistaContratos(controller, elementHtml, tipoDashboard, day, month, year) {
    $.ajax({
        type: 'POST',
        url: controller + 'RenderizaDashboardAnalistaContratos',
        data: '{ tipoDashboard: "' + tipoDashboard + '", day: "' + day + '", month: "' + month + '", year: "' + year + '"  }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code !== "600") {
                if (response.Code === "200") {
                    $(elementHtml).html('');
                    var code = "";
                    for (var i = 0; i < response.EstadisticasXContrato.length; i++) {
                        if (response.EstadisticasXContrato[i].Code === "200") {
                            code = "200";
                        }

                        ajaxViewPartialDashboardAnalistaContartos(controller, elementHtml, response.EstadisticasXContrato[i].Analista, 
                            response.EstadisticasXContrato[i].CTotal, response.EstadisticasXContrato[i].ConceptoC, response.EstadisticasXContrato[i].CSolicitado,
                            response.EstadisticasXContrato[i].CTerminado,
                            response.EstadisticasXContrato[i].CTwest, response.EstadisticasXContrato[i].CTwrrhh, response.EstadisticasXContrato[i].CTwc,
                            response.EstadisticasXContrato[i].RTotal, response.EstadisticasXContrato[i].ConceptoR, response.EstadisticasXContrato[i].RSolicitado,
                            response.EstadisticasXContrato[i].RTerminado,
                            response.EstadisticasXContrato[i].RTwest,
                            response.EstadisticasXContrato[i].RTwrrhh, response.EstadisticasXContrato[i].RTwc);
                    }

                    if (code === "200") {
                        $("#contentDashboardXContratoDespliegue").show();
                    }

                }
                else {
                    /** error */
                }
            }
            else {
                $("#modelExpiracion").modal("show");
            }
        },
        error: function (xhr) {
            ajaxViewPartialLoaderErrorGeneric(controller, "Ha ocurrido algo inesperado en la plataforma, favor comunicarse con el area de TI, para ser solucionado el problema. Favor refrescar la pagina para evitar problemas de encolamiento de información.");
        },
        complete: function () {
            //$("#loaderSearch").html('');
        }
    });
}

function ajaxViewPartialDashboardSolicitudes(controller, elementHtml, total, concepto, tipoProceso, twest, twrrhh, twc) {
    $.ajax({
        type: 'POST',
        url: controller + 'ViewPartialDashboardSolicitudes',
        data: '{ total: "' + total + '", concepto: "' + concepto + '", tipoProceso: "' + tipoProceso + '", twest: "' + twest + '", twrrhh: "' + twrrhh + '", twc: "' + twc + '" }',
        dataType: 'json',
        contentType: 'application/json',
        async: false,
        error: function (xhr) {
            $(elementHtml).append(xhr.responseText);
        }
    });
}

function ajaxViewPartialDashboardAnalistaContartos(controller, elementHtml, analista, totalC, conceptoC, cSolicitado, cTerminado, Ctwest, Ctwrrhh, Ctwc, totalR,
                                  conceptoR, rSolicitado, rTerminado, Rtwest, Rtwrrhh, Rtwc) {
    $.ajax({
        type: 'POST',
        url: controller + 'ViewPartialDashboardAnalistaContartos',
        data: '{ analista: "' + analista + '", totalC: "' + totalC + '", conceptoC: "' + conceptoC + '", cSolicitado: "' + cSolicitado + '", cTerminado: "' + cTerminado + '", Ctwest: "' + Ctwest + '", Ctwrrhh: "' + Ctwrrhh + '", Ctwc: "' + Ctwc + '", totalR: "' + totalR + '", ' +
            'conceptoR: "' + conceptoR + '", rSolicitado: "' + rSolicitado + '", rTerminado: "' + rTerminado + '", Rtwest: "' + Rtwest + '", Rtwrrhh: "' + Rtwrrhh + '", Rtwc: "' + Rtwc + '" }',
        dataType: 'json',
        contentType: 'application/json',
        async: false,
        error: function (xhr) {
            $(elementHtml).append(xhr.responseText);
        }
    });
}


function ajaxCrearGastosFinanzas(controller, codigoTransaccion, procesamiento, lengthProcesamiento, procesoValidacion, plantilla) {
    $.ajax({
        type: 'POST',
        url: controller + 'CrearGastosFinanzas',
        data: '{ codigoTransaccion: "' + codigoTransaccion + '", procesamiento: "' + procesamiento + '" }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code !== "600") {
                if (response.Code === "200") {
                    alert('Done');
                }
                else {
                    ajaxViewPartialLoaderErrorGeneric(controller, response.Message);
                }
            }
            else {
                $("#modelExpiracion").modal("show");
            }
        },
        error: function (xhr) {
            $("#loaderProceso").html('');
            ajaxViewPartialLoaderErrorGeneric(controller, "Ha ocurrido algo inesperado en la plataforma, favor comunicarse con el area de TI, para ser solucionado el problema. Favor refrescar la pagina para evitar problemas de encolamiento de información.");
        },
        complete: function () {
        }
    });
}

function ajaxViewPartialNoEncontrado(controller, elementHtml, glyphicon, message) {
    $.ajax({
        type: 'POST',
        url: controller + 'ViewPartialNoEncontrado',
        data: '{ glyphicon: "' + glyphicon + '", message: "' + message + '" }',
        dataType: 'json',
        contentType: 'application/json',
        async: false,
        error: function (xhr) {
            $(elementHtml).append(xhr.responseText);
        }
    });
}