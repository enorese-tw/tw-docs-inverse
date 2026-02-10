function ajaxRenderizaSolicitudes(controller, pagination, tipoSolicitud, htmlRenderizado, htmlPagination, codigoTransaccion, typeFilter, dataFilter) {
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
                        ajaxViewPartialSolicitudes(controller, htmlRenderizado,
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
                            response.Solicitudes[i].Calcular);
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

function ajaxViewPartialSolicitudes(controller, elementHtml, nombreSolicitud, nombreProceso, tipoEvento, creado, fechasCompromiso, comentarios, codificarCod, codigoSolicitud,
    prioridad, glyphicon, glyphiconColor, borderColor, colorFont, renderizadoOpciones,
    optDescargarDatosCargados, optAsignarSolicitud, optDescargarSolicitudContratoIndividual,
    optHistorialSolicitud, optRevertirAnulacion, optDescargarErrorDatosCargados, optAnularSolicitud) {
    $.ajax({
        type: 'POST',
        url: controller + 'ViewPartialSolicitudes',
        data: '{ nombreSolicitud: "' + nombreSolicitud + '", nombreProceso: "' + nombreProceso + '", tipoEvento: "' + tipoEvento + '", creado: "' + creado + '", fechasCompromiso: "' + fechasCompromiso + '", ' +
            ' comentarios: "' + comentarios + '", codificarCod: "' + codificarCod + '", codigoSolicitud: "' + codigoSolicitud + '", prioridad: "' + prioridad + '", glyphicon: "' + glyphicon + '", glyphiconColor: "' + glyphiconColor + '", ' +
            ' borderColor: "' + borderColor + '", colorFont: "' + colorFont + '", renderizadoOpciones: "' + renderizadoOpciones + '", optDescargarDatosCargados: "' + optDescargarDatosCargados + '", ' +
            ' optAsignarSolicitud: "' + optAsignarSolicitud + '", optDescargarSolicitudContratoIndividual: "' + optDescargarSolicitudContratoIndividual + '", ' +
            ' optHistorialSolicitud: "' + optHistorialSolicitud + '", optRevertirAnulacion: "' + optRevertirAnulacion + '", ' +
            ' optDescargarErrorDatosCargados: "' + optDescargarErrorDatosCargados + '", optAnularSolicitud: "' + optAnularSolicitud + '"}',
        dataType: 'json',
        contentType: 'application/json',
        async: false,
        error: function (xhr) {
            $(elementHtml).append(xhr.responseText);
        }
    });
}