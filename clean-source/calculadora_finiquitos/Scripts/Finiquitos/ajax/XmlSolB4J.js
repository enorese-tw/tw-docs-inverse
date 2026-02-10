$(document).ready(function () {
    $(".datepicker").datepicker({
        dateFormat: "dd-mm-yy",
        showButtonPanel: false,
        changeMonth: false,
        changeYear: false,
        inline: true
    });
    $.datepicker.regional["es"] = {
        closeText: "Cerrar",
        prevText: "Anterior",
        nextText: "Siguiente",
        currentText: "Hoy",
        monthNames: [
            "Enero",
            "Febrero",
            "Marzo",
            "Abril",
            "Mayo",
            "Junio",
            "Julio",
            "Agosto",
            "Septiembre",
            "Octubre",
            "Noviembre",
            "Diciembre"
        ],
        monthNamesShort: [
            "Ene",
            "Feb",
            "Mar",
            "Abr",
            "May",
            "Jun",
            "Jul",
            "Ago",
            "Sep",
            "Oct",
            "Nov",
            "Dic"
        ],
        dayNames: [
            "Domingo",
            "Lunes",
            "Martes",
            "Miércoles",
            "Jueves",
            "Viernes",
            "Sábado"
        ],
        dayNamesShort: ["Dom", "Lun", "Mar", "Mié", "Juv", "Vie", "Sáb"],
        dayNamesMin: ["Do", "Lu", "Ma", "Mi", "Ju", "Vi", "Sá"],
        weekHeader: "Sm",
        dateFormat: "dd/mm/yy",
        firstDay: 1,
        isRTL: false,
        showMonthAfterYear: false,
        yearSuffix: ""
    };
    $.datepicker.setDefaults($.datepicker.regional["es"]);

    var comS = "'";

    ajaxObtenerSolicitudesB4J("xmlsolb4j.aspx", "N", "INICIO", "", "", "", "");

    $(document).on("click", ".btn-event-rev", function (e) {
        e.preventDefault();
        ajaxObtenerSolicitudesB4J("xmlsolb4j.aspx", "S", "DETAILS", "S", "PTSOLB4J", "", $(this).attr("data-event"));
        ajaxObtenerSolicitudesB4J("xmlsolb4j.aspx", "S", "DETAILS", "S", "DATAFILES", "", $(this).attr("data-event"));
        ajaxObtenerSolicitudesB4J("xmlsolb4j.aspx", "S", "DETAILS", "S", "DATADATES", "", $(this).attr("data-event"));
    });

    $(document).on("click", ".btn-event-aprov", function (e) {
        e.preventDefault();
        $("#aprobarSolicitud #eventSolicitudCodAprov").html($(this).attr("data-event"));
        $("#aprobarSolicitud #eventTrabajadorAprov").html($(this).attr("data-trabajador"));
        $("#aprobarSolicitud #eventRutTAprov").html($(this).attr("data-rutt"));
        $(".btn-event-aprob-int").attr("data-event", $(this).attr("data-event"));
        $("#aprobarSolicitud").modal("show");
    });

    $(document).on("click", ".btn-event-aprob-int", function (e) {
        e.preventDefault();
        ajaxAdministrarProcesosSolB4J("XmlSolB4j.aspx", "APROVAR", $(this).attr("data-event"));
    });

    $(document).on("click", ".btn-event-rechz", function (e) {
        e.preventDefault();

        var content =
            '<a href="#" class="btn btn-danger btn-event-rechz-int" data-event="' + $(this).attr("data-event") + '" style="border-radius: 25px; width: auto; height: 50px; margin-left: 5px;">' +
            '<i class="glyphicon-tw-cancel" style="width: 30px; height: 30px; display: inline-block; margin-left: -3px; margin-top: 3px; margin-bottom: -3px;"></i> ' +
            '<i style="font-family: ' + comS + 'Open Sans Condensed' + comS + ', sans-serif; font-size: 16px; font-style: normal;  line-height: 15px;  text-align: left;  display: inline-block;" class="btn-administrar-sol">Rechazar Solicitud<br />de Baja</i>' +
            '</a>';

        $("#otrosEventos #titleEvento").html("Rechazo de Solicitud de Baja");
        $("#otrosEventos #eventAccion").html("rechazar");
        $("#otrosEventos #eventAccionField").html("del rechazo");
        $("#otrosEventos #eventSolicitudCod").html($(this).attr("data-event"));
        $("#otrosEventos #eventTrabajador").html($(this).attr("data-trabajador"));
        $("#otrosEventos #eventRutT").html($(this).attr("data-rutt"));
        $("#otrosEventos #eventImportante").html('Importante: una vez realizado el rechazo, se le informará mediante correo al ejecutivo que corresponda. Si requiere volver la solicitud de baja a estado de validación, puede realizarlo mediante la opción de "Revertir Estado A En Validación", quedará registro de la acción para conceptos de auditoria.');
        $("#otrosEventos #eventBtnAccion").html(content);
        $("#otrosEventos").modal("show");
    });

    $(document).on("click", ".btn-event-rechz-int", function (e) {
        e.preventDefault();
        ajaxAdministrarProcesosSolB4J("XmlSolB4j.aspx", "RECHAZAR", $(this).attr("data-event"));
    });

    $(document).on("click", ".btn-event-anular", function (e) {
        e.preventDefault();

        var content =
            '<a href="#" class="btn btn-grey btn-event-anular-int" data-event="' + $(this).attr("data-event") + '" style="border-radius: 25px; width: auto; height: 50px; margin-left: 5px;">' +
            '<i class="glyphicon-tw-anular2" style="width: 30px; height: 30px; display: inline-block; margin-left: -3px; margin-top: 3px; margin-bottom: -3px;"></i> ' +
            '<i style="font-family: ' + comS + 'Open Sans Condensed' + comS + ', sans-serif; font-size: 16px; font-style: normal;  line-height: 15px;  text-align: left;  display: inline-block;" class="btn-administrar-sol">Anular Solicitud<br />de Baja</i>' +
            '</a>';

        $("#otrosEventos #titleEvento").html("Anulación de Solicitud de Baja");
        $("#otrosEventos #eventAccion").html("anular");
        $("#otrosEventos #eventAccionField").html("de la anulación");
        $("#otrosEventos #eventSolicitudCod").html($(this).attr("data-event"));
        $("#otrosEventos #eventTrabajador").html($(this).attr("data-trabajador"));
        $("#otrosEventos #eventRutT").html($(this).attr("data-rutt"));
        $("#otrosEventos #eventImportante").html('Importante: Si requiere volver la solicitud de baja a estado de validación, puede realizarlo mediante la opción de "Revertir Estado A En Validación", quedará registro de la acción para conceptos de auditoria.');
        $("#otrosEventos #eventBtnAccion").html(content);
        $("#otrosEventos").modal("show");
    });

    $(document).on("click", ".btn-event-anular-int", function (e) {
        e.preventDefault();
        ajaxAdministrarProcesosSolB4J("XmlSolB4j.aspx", "ANULAR", $(this).attr("data-event"));
    });

    $(document).on("click", ".btn-event-revert", function (e) {
        e.preventDefault();
        ajaxAdministrarProcesosSolB4J("XmlSolB4j.aspx", "REVERTIR", $(this).attr("data-event"));
    });

    $(document).on("click", ".btn-event-download", function (e) {
        e.preventDefault();
        var data = new FormData();
        data.append("pathOrigin", $(this).attr("data-path"));
        data.append("filename", $(this).attr("data-filename"));
        ajaxDownloadFile(data);
    });

    $(document).on("click", ".btn-event-view-temporary", function (e) {
        e.preventDefault();
        var data = new FormData();
        data.append("pathOrigin", $(this).attr("data-path"));
        data.append("codProceso", $("#revisarSolicitud #codigoProceso").html());
        data.append("filename", $(this).attr("data-filename"));
        ajaxTemporaryPDF(data);
    });

    $("#modalReporte").on("click", function () {
        $("#boxModalReporte").modal("show");
    });
    
    function ajaxObtenerSolicitudesB4J(page, hasFilter, etapa, xSection, section, filtrarPor, filtro) {
        $.ajax({
            type: 'POST',
            url: page + '/GetObtenerSolicitudesB4J',
            data: '{ hasFilter: "' + hasFilter + '", etapa: "' + etapa + '", xSection: "' + xSection + '", section: "' + section + '", filtrarPor: "' + filtrarPor + '", filtro: "' + filtro + '" }',
            dataType: 'json',
            contentType: 'application/json',
            async: true,
            success: function (response) {
                var data = JSON.parse(response.d);
                var content = "";
                switch (etapa) {
                    case "INICIO":
                        for (var i = 0; i < data.resultado.length; i++) {
                            content = content +
                                '<tr class="items_' + data.resultado[i].ID + '">' +
                                '<td>' +
                                '<div class="row" style="' + data.resultado[i].ESTADO_PROCESO + '">' +
                                '<div class="col-xs-12 col-sm-12 col-md-2 col-lg-1" style="min-height: 155px; display: flex; justify-content: center; align-items: center;">' +
                                '<span style="font-family: ' + comS + 'Open Sans Condensed' + comS + ', sans-serif;" class="btn-process-index btn ' + data.resultado[i].ESTADO_PROCESO_BTN + '" style="text-align: center;">N° SOL<br/>' +
                                + data.resultado[i].ID + '</span></div>' +
                                '<div class="col-xs-12 col-sm-12 col-md-2 col-lg-2" style="min-height: 155px; display: flex; justify-content: center; align-items: center;">' +
                                '<label><i class="etiq-title">USUARIO SOLICITANTE</i><br>' +
                                '<i class="etiq-data">' + data.resultado[i].CREADO_POR + '</i></label></div>' +
                                '<div class="col-xs-12 col-sm-12 col-md-3 col-lg-6">' +
                                '<div class="row" style="text-align: left;">' +
                                '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="margin-top: 10px; margin-bottom: -10px;">' +
                                '<label><i class="etiq-title">EMPRESA </i><i class="etiq-data">' + data.resultado[i].EMPRESA + '</i><i class="etiq-title"> • CLIENTE </i><i class="etiq-data">' + data.resultado[i].AREANEG + '</i></label>' +
                                '</div>' +
                                '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="margin-bottom: -10px;">' +
                                '<label><i class="etiq-title">EMPLEADO </i><i class="etiq-data">' + data.resultado[i].NOMBRE + '</i><i class="etiq-title"> • RUT </i><i class="etiq-data">' + data.resultado[i].RUT + '</i></label>' +
                                '</div>' +
                                '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="margin-bottom: -10px;">' +
                                '<label><i class="etiq-title">CODIGO PROCESO </i><i class="etiq-data">' + data.resultado[i].CODIGO_PROCESO + '</i><i class="etiq-title"> • NOMBRE PROCESO </i><i class="etiq-data">' + data.resultado[i].NOMBRE_PROCESO + '</i></label>' +
                                '</div>' +
                                '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="margin-bottom: -10px;">' +
                                '<label><i class="etiq-title">SOLICITUD DE BAJA CREADA </i><i class="etiq-data">' + data.resultado[i].FECHA_CREACION + '</i></label>' +
                                '</div>' +
                                '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="margin-bottom: 10px;">' +
                                '<label><i class="etiq-title">Observaciones del proceso: </i><i class="etiq-title event-anot-process">' + data.resultado[i].ANOTACION_DOCUMENTO + '</i></label>' +
                                '</div>' +
                                '</div>' +
                                '</div>' +
                                '<div class="col-xs-12 col-sm-12 col-md-5 col-lg-3" style="min-height: 155px; display: flex; justify-content: center; align-items: center;">';

                                if (data.resultado[i].BTN_REV_SOLB4J === "S") {
                                    content = content +
                                        '<a href="#" class="btn btn-primary btn-event-rev" data-event="' + data.resultado[i].ID + '" style="border-radius: 25px; width: auto; height: 50px; margin-left: 5px;">' +
                                        '<i class="glyphicon-tw-edit" style="width: 30px; height: 30px; display: inline-block; margin-left: -3px; margin-top: 3px; margin-bottom: -1px;"></i>' +
                                        '<i style="font-family: ' + comS + 'Open Sans Condensed' + comS + ', sans-serif; font-size: 16px; font-style: normal;  line-height: 15px;  text-align: left;  display: inline-block;" class="btn-administrar-sol">Revisar<br />Solicitud</i>' +
                                        '</a> ';
                                }
                                
                                content = content + ' <a href="#" class="btn btn-warning btn-event-revert" data-event="' + data.resultado[i].ID + '" style="';

                                if (data.resultado[i].BTN_REVERT_SOLB4J === "N") {
                                    content = content + 'display:none; ';
                                }

                                content = content +
                                    'border-radius: 25px; width: auto; height: 50px; margin - left: 5px; ">' +
                                    '<i class="glyphicon-tw-revertir" style="width: 30px; height: 30px; display: inline-block; margin-left: -3px; margin-top: 3px; margin-bottom: -1px;"></i>' +
                                    '<i style="font-family: ' + comS + 'Open Sans Condensed' + comS + ', sans-serif; font-size: 16px; font-style: normal;  line-height: 15px;  text-align: left;  display: inline-block;" class="btn-administrar-sol">Revertir Estado<br />A En Validación</i>' +
                                    '</a>';

                                content = content + '<a href="#" class="btn btn-success btn-event-aprov" title="Aprobar Solicitud de Baja" data-event="' + data.resultado[i].ID + '" ' +
                                    'data-rutT="' + data.resultado[i].RUT + '" data-trabajador="' + data.resultado[i].NOMBRE + '" style="';

                                if (data.resultado[i].BTN_APROV_SOLB4J === "N") {
                                    content = content + 'display:none; ';
                                }

                                content = content +
                                    'border-radius: 25px; width: 50px; height: 50px; margin-left: 5px;">' +
                                    '<i class="glyphicon-tw-check" style="width: 30px; height: 30px; display: inline-block; margin-left: -3px; margin-top: 3px;"></i>' +
                                    '</a>';

                                content = content + '<a href="#" class="btn btn-danger btn-event-rechz" title="Rechazar Solicitud de Baja" data-event="' + data.resultado[i].ID + '" ' +
                                    'data-rutT="' + data.resultado[i].RUT + '" data-trabajador="' + data.resultado[i].NOMBRE + '" style="';

                                if (data.resultado[i].BTN_RECHZ_SOLB4J === "N") {
                                    content = content + 'display: none; ';
                                }

                                content = content +
                                    'border-radius: 25px; width: 50px; height: 50px; margin-left: 5px;">' +
                                    '<i class="glyphicon-tw-cancel" style="width: 30px; height: 30px; display: inline-block; margin-left: -3px; margin-top: 3px;"></i>' +
                                    '</a>';

                                content = content + '<a href="#" class="btn btn-grey btn-event-anular" title="Anular Solicitud de Baja" data-event="' + data.resultado[i].ID + '" ' +
                                    'data-rutT="' + data.resultado[i].RUT + '" data-trabajador="' + data.resultado[i].NOMBRE + '" style="';

                                if (data.resultado[i].BTN_ANUL_SOLB4J === "N") {
                                    content = content + 'display: none; ';
                                }

                                content = content +
                                    'border-radius: 25px; width: 50px; height: 50px; margin-left: 5px;">' +
                                    '<i class="glyphicon-tw-anular2" style="width: 30px; height: 30px; display: inline-block; margin-left: -3px; margin-top: 3px;"></i>' +
                                    '</a>';

                                content = content +
                                    '</div>' +
                                    '</div>' +
                                    '</td>' +
                                    '</tr>';
                        }

                        $("#tablaSolicitudesBaja > tbody").html(content);

                        break;
                    case "DETAILS":
                        switch (section) {
                            case "PTSOLB4J":
                                for (var h = 0; h < data.resultado.length; h++) {
                                    $("#revisarSolicitud #codigoProceso").html(data.resultado[h].CODIGO_TX);
                                    $("#revisarSolicitud #nombreProceso").html(data.resultado[h].NOMBRE_PROCESO);
                                    $("#revisarSolicitud #glosaEstadoProceso").html(data.resultado[h].GLOSA_ESTADO);
                                    $("#revisarSolicitud #glosa-color-estado").removeClass().addClass("btn " + data.resultado[h].GLOSA_COLOR_ESTADO);
                                    $("#revisarSolicitud #numeroSolicitud").html(data.resultado[h].ID_SOL);
                                    $("#revisarSolicitud #ejecutivoSolicitud").html(data.resultado[h].CREADO_POR);
                                    $("#revisarSolicitud #tiempoCreacion").html(data.resultado[h].FECHA_CREACION);
                                    $("#revisarSolicitud #empresaTeamwork").html(data.resultado[h].EMPRESA);
                                    $("#revisarSolicitud #nombreTrabajador").html(data.resultado[h].NOMBRE);
                                    $("#revisarSolicitud #rutTrabajador").html(data.resultado[h].RUTTRABAJADOR);
                                    $("#revisarSolicitud #clienteAsociado").html(data.resultado[h].CLIENTE);

                                    $("#revisarSolicitud #fichaTrabajador").html(data.resultado[h].FICHA);
                                    $("#revisarSolicitud #cargoTrabajador").html(data.resultado[h].CARGO);
                                    $("#revisarSolicitud #cargoModTrabajador").html(data.resultado[h].CARGOMOD);
                                    $("#revisarSolicitud #clienteTrabajador").html(data.resultado[h].CLIENTE);

                                    $("#revisarSolicitud #estadoContratoTrabajador").html(data.resultado[h].ESTADO);
                                    $("#revisarSolicitud #fechaInicioContratoTrabajador").html(data.resultado[h].FECHAINICIO);
                                    $("#revisarSolicitud #fechaTerminoContratoTrabajador").html(data.resultado[h].FECHATERMINO);
                                    $("#revisarSolicitud #agenciaTrabajador").html(data.resultado[h].AGENCIA);
                                    $("#revisarSolicitud #fechaDesvinculacionTrabajador").html(data.resultado[h].ACONTARDE);
                                    $("#revisarSolicitud #causalDesvinculacionTrabajador").html(data.resultado[h].CAUSAL);
                                }
                                break;
                            case "DATAFILES":
                                if (data.resultado.length > 0) {
                                    for (var j = 0; j < data.resultado.length; j++) {
                                        if (data.resultado[j].CANTIDAD_FILES > 0) {
                                            if (data.resultado[j].CANTIDAD_FILES > 1) {
                                                $("#revisarSolicitud #glosa-files-download").html("Hay " + data.resultado[j].CANTIDAD_FILES + " archivos para descargar.");
                                            } else {
                                                $("#revisarSolicitud #glosa-files-download").html("Hay " + data.resultado[j].CANTIDAD_FILES + " archivo para descargar.");
                                            }

                                        }

                                        content = content +
                                            '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-4" style="height: 280px; border: 1px dashed rgb(210, 210, 210); text-align: center; padding-top: 30px; padding-bottom: 30px;">' +
                                            '<button type="button" class="' + extensionFileType(data.resultado[j].EXTENSION) + '" title="' + data.resultado[j].FILENAMES + '" style="border-radius: 50%;  margin-top: -25px; width: 45px; height: 45px; position: relative; ">' +
                                            '<i class="' + extensionFileIcon(data.resultado[j].EXTENSION) + '" style="display: inline-block;"></i>' +
                                            '</span>' +
                                            '</button>' +
                                            '<p style="font-family: ' + comS + 'Open Sans Condensed' + comS + ', sans-serif; height: 80px;">' +
                                            data.resultado[j].FILENAMES +
                                            '</p>' +
                                            '<a href="#" class="btn btn-primary btn-event-download" data-path="' + data.resultado[j].RUTA + '" data-filename="' + data.resultado[j].FILENAMES + '" style="border-radius: 20px; margin-bottom: 10px;">' +
                                            '<i class="glyphicon-tw-download" style="width: 30px; height: 30px; display: inline-block; margin-left: -3px; margin-top: -3px; margin-bottom: -10px;"></i>' +
                                            '<i style="font-family: ' + comS + 'Open Sans Condensed' + comS + ', sans-serif; font-size: 16px; font-style: normal;  line-height: 15px;  text-align: left;  display: inline-block;" class="btn-administrar-sol">Descargar</i>' +
                                            '</a><br/>';

                                        if (data.resultado[j].VISUALIZAR === "S") {
                                            content = content +
                                                '<a href="#" class="btn btn-danger btn-event-view-temporary" data-path="' + data.resultado[j].RUTA + '" data-filename="' + data.resultado[j].FILENAMES + '" style="border-radius: 20px;">' +
                                                '<i class="glyphicon-tw-docpdf" style="width: 30px; height: 30px; display: inline-block; margin-left: -3px; margin-top: -3px; margin-bottom: -10px;"></i>' +
                                                '<i style="font-family: ' + comS + 'Open Sans Condensed' + comS + ', sans-serif; font-size: 16px; font-style: normal;  line-height: 15px;  text-align: left;  display: inline-block;" class="btn-administrar-sol">Visualizar</i>' +
                                                '</a>';
                                        }

                                        content = content +
                                            '</div>';
                                    }
                                } else {
                                    $("#revisarSolicitud #glosa-files-download").html("No hay archivos para descargar.");
                                }

                                $("#revisarSolicitud #adcFiles").html(content);
                                $("#revisarSolicitud").modal("show");
                                break;
                            case "DATADATES":
                                
                                if (data.resultado.length > 0) {
                                    if (data.resultado.length > 0) {
                                        if (data.resultado.length > 1) {
                                            $("#revisarSolicitud #glosa-dates-download").html("Hay " + data.resultado.length + " fechas adjuntas.");
                                        } else {
                                            $("#revisarSolicitud #glosa-dates-download").html("Hay " + data.resultado.length + " fecha adjunta.");
                                        }
                                    }
                                    for (var k = 0; k < data.resultado.length; k++) {
                                        if (data.resultado[k].VALIDACION === "0") {
                                            content = content + '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-3" style="text-align: center;">';
                                            content = content + '<i style="width: 50px; height: 50px; border-radius: 50%; display: inline-block;" class="glyphicon-tw-date btn-primary"></i>';
                                            content = content + '<p style="font-family: ' + comS + 'Open Sans Condensed' + comS + ', sans-serif; ">';
                                            content = content + '23 de febrero de 2019';
                                            content = content + '</p>';
                                            content = content + '</div>';
                                        } else {
                                            content = content + '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-3" style="text-align: center;">';
                                            content = content + '<p style="font-family: ' + comS + 'Open Sans Condensed' + comS + ', sans-serif; ">';
                                            content = content + data.resultado[k].RESULTADO;
                                            content = content + '</p>';
                                            content = content + '</div>';
                                        }
                                    }
                                } else {
                                    content = content + '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-3" style="text-align: center;">';
                                    content = content + '<p style="font-family: ' + comS + 'Open Sans Condensed' + comS + ', sans-serif; ">';
                                    content = content + 'Información no disponible.'
                                    content = content + '</p>';
                                    content = content + '</div>';
                                }
                                
                                break;
                            case "GESTIONBAJAS":

                                break;
                            case "TRACKINGSOLICITUD":



                                $("#revisarSolicitud").modal("show");
                                break;
                        }
                        break;
                    }
                    
                    //for (var i = 0; i < data.resultado.length; i++) {
                    //    if (data.resultado[i].XSECTION === 'N') {
                    //        content = content + '<tr class="items_' + data.resultado[i].ID + '">' +
                    //            '<td>' +
                    //            '<div class="row" style="' + data.resultado[i].ESTADO_PROCESO + '">' +
                    //            '<div class="col-xs-12 col-sm-12 col-md-2 col-lg-1" style="min-height: 155px; display: flex; justify-content: center; align-items: center;">' +
                    //            '<span style="font-family: ' + comS + 'Open Sans Condensed' + comS + ', sans-serif;" class="btn-process-index btn ' + data.resultado[i].ESTADO_PROCESO_BTN + '" style="text-align: center;">N° SOL<br/>' +
                    //            + data.resultado[i].ID + '</span></div>' +
                    //            '<div class="col-xs-12 col-sm-12 col-md-2 col-lg-2" style="min-height: 155px; display: flex; justify-content: center; align-items: center;">' +
                    //            '<label><i class="etiq-title">USUARIO SOLICITANTE</i><br>' +
                    //            '<i class="etiq-data">' + data.resultado[i].CREADO_POR + '</i></label></div>' +
                    //            '<div class="col-xs-12 col-sm-12 col-md-3 col-lg-6">' +
                    //            '<div class="row" style="text-align: left;">' +
                    //            '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="margin-top: 10px; margin-bottom: -10px;">' +
                    //            '<label><i class="etiq-title">EMPRESA </i><i class="etiq-data">' + data.resultado[i].EMPRESA + '</i><i class="etiq-title"> • CLIENTE </i><i class="etiq-data">' + data.resultado[i].AREANEG + '</i></label>' +
                    //            '</div>' +
                    //            '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="margin-bottom: -10px;">' +
                    //            '<label><i class="etiq-title">EMPLEADO </i><i class="etiq-data">' + data.resultado[i].NOMBRE + '</i><i class="etiq-title"> • RUT </i><i class="etiq-data">' + data.resultado[i].RUT + '</i></label>' +
                    //            '</div>' +
                    //            '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="margin-bottom: -10px;">' +
                    //            '<label><i class="etiq-title">CODIGO PROCESO </i><i class="etiq-data">' + data.resultado[i].CODIGO_PROCESO + '</i><i class="etiq-title"> • NOMBRE PROCESO </i><i class="etiq-data">' + data.resultado[i].NOMBRE_PROCESO + '</i></label>' +
                    //            '</div>' +
                    //            '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="margin-bottom: -10px;">' +
                    //            '<label><i class="etiq-title">SOLICITUD DE BAJA CREADA </i><i class="etiq-data">' + data.resultado[i].FECHA_CREACION + '</i></label>' +
                    //            '</div>' +
                    //            '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="margin-bottom: 10px;">' +
                    //            '<label><i class="etiq-title">Observaciones del proceso: </i><i class="etiq-title event-anot-process">' + data.resultado[i].ANOTACION_DOCUMENTO + '</i></label>' +
                    //            '</div>' +
                    //            '</div>' +
                    //            '</div>' +
                    //            '<div class="col-xs-12 col-sm-12 col-md-5 col-lg-3" style="min-height: 155px; display: flex; justify-content: center; align-items: center;">';

                    //        if (data.resultado[i].BTN_REV_SOLB4J === "S") {
                    //            content = content +
                    //                '<a href="#" class="btn btn-primary btn-event-rev" data-event="' + data.resultado[i].ID + '" style="border-radius: 25px; width: auto; height: 50px; margin-left: 5px;">' +
                    //                '<i class="glyphicon-tw-edit" style="width: 30px; height: 30px; display: inline-block; margin-left: -3px; margin-top: 3px; margin-bottom: -1px;"></i>' +
                    //                '<i style="font-family: ' + comS + 'Open Sans Condensed' + comS + ', sans-serif; font-size: 16px; font-style: normal;  line-height: 15px;  text-align: left;  display: inline-block;" class="btn-administrar-sol">Revisar<br />Solicitud</i>' +
                    //                '</a> ';
                    //        }


                    //        content = content + ' <a href="#" class="btn btn-warning btn-event-revert" data-event="' + data.resultado[i].ID + '" style="';

                    //        if (data.resultado[i].BTN_REVERT_SOLB4J === "N") {
                    //            content = content + 'display:none; ';
                    //        }

                    //        content = content +
                    //            'border-radius: 25px; width: auto; height: 50px; margin - left: 5px; ">' +
                    //            '<i class="glyphicon-tw-revertir" style="width: 30px; height: 30px; display: inline-block; margin-left: -3px; margin-top: 3px; margin-bottom: -1px;"></i>' +
                    //            '<i style="font-family: ' + comS + 'Open Sans Condensed' + comS + ', sans-serif; font-size: 16px; font-style: normal;  line-height: 15px;  text-align: left;  display: inline-block;" class="btn-administrar-sol">Revertir Estado<br />A En Validación</i>' +
                    //            '</a>';

                    //        content = content + '<a href="#" class="btn btn-success btn-event-aprov" title="Aprobar Solicitud de Baja" data-event="' + data.resultado[i].ID + '" ' +
                    //            'data-rutT="' + data.resultado[i].RUT + '" data-trabajador="' + data.resultado[i].NOMBRE + '" style="';

                    //        if (data.resultado[i].BTN_APROV_SOLB4J === "N") {
                    //            content = content + 'display:none; ';
                    //        }

                    //        content = content +
                    //            'border-radius: 25px; width: 50px; height: 50px; margin-left: 5px;">' +
                    //            '<i class="glyphicon-tw-check" style="width: 30px; height: 30px; display: inline-block; margin-left: -3px; margin-top: 3px;"></i>' +
                    //            '</a>';

                    //        content = content + '<a href="#" class="btn btn-danger btn-event-rechz" title="Rechazar Solicitud de Baja" data-event="' + data.resultado[i].ID + '" ' +
                    //            'data-rutT="' + data.resultado[i].RUT + '" data-trabajador="' + data.resultado[i].NOMBRE + '" style="';

                    //        if (data.resultado[i].BTN_RECHZ_SOLB4J === "N") {
                    //            content = content + 'display: none; ';
                    //        }

                    //        content = content +
                    //            'border-radius: 25px; width: 50px; height: 50px; margin-left: 5px;">' +
                    //            '<i class="glyphicon-tw-cancel" style="width: 30px; height: 30px; display: inline-block; margin-left: -3px; margin-top: 3px;"></i>' +
                    //            '</a>';

                    //        content = content + '<a href="#" class="btn btn-grey btn-event-anular" title="Anular Solicitud de Baja" data-event="' + data.resultado[i].ID + '" ' +
                    //            'data-rutT="' + data.resultado[i].RUT + '" data-trabajador="' + data.resultado[i].NOMBRE + '" style="';

                    //        if (data.resultado[i].BTN_ANUL_SOLB4J === "N") {
                    //            content = content + 'display: none; ';
                    //        }

                    //        content = content +
                    //            'border-radius: 25px; width: 50px; height: 50px; margin-left: 5px;">' +
                    //            '<i class="glyphicon-tw-anular2" style="width: 30px; height: 30px; display: inline-block; margin-left: -3px; margin-top: 3px;"></i>' +
                    //            '</a>';

                    //        content = content +
                    //            '</div>' +
                    //            '</div>' +
                    //            '</td>' +
                    //            '</tr>';
                    //    }
                    //    else
                    //    {
                    //        if (data.resultado[i].SECTION === 'PTSOLB4J') {
                    //            $("#revisarSolicitud #codigoProceso").html(data.resultado[i].CODIGO_TX);
                    //            $("#revisarSolicitud #nombreProceso").html(data.resultado[i].NOMBRE_PROCESO);
                    //            $("#revisarSolicitud #glosaEstadoProceso").html(data.resultado[i].GLOSA_ESTADO);
                    //            $("#revisarSolicitud #glosa-color-estado").removeClass().addClass("btn " + data.resultado[i].GLOSA_COLOR_ESTADO);
                    //            $("#revisarSolicitud #numeroSolicitud").html(data.resultado[i].ID_SOL);
                    //            $("#revisarSolicitud #ejecutivoSolicitud").html(data.resultado[i].CREADO_POR);
                    //            $("#revisarSolicitud #tiempoCreacion").html(data.resultado[i].FECHA_CREACION);
                    //            $("#revisarSolicitud #empresaTeamwork").html(data.resultado[i].EMPRESA);
                    //            $("#revisarSolicitud #nombreTrabajador").html(data.resultado[i].NOMBRE);
                    //            $("#revisarSolicitud #rutTrabajador").html(data.resultado[i].RUTTRABAJADOR);
                    //            $("#revisarSolicitud #clienteAsociado").html(data.resultado[i].CLIENTE);

                    //            $("#revisarSolicitud #fichaTrabajador").html(data.resultado[i].FICHA);
                    //            $("#revisarSolicitud #cargoTrabajador").html(data.resultado[i].CARGO);
                    //            $("#revisarSolicitud #cargoModTrabajador").html(data.resultado[i].CARGOMOD);
                    //            $("#revisarSolicitud #clienteTrabajador").html(data.resultado[i].CLIENTE);

                    //            $("#revisarSolicitud #estadoContratoTrabajador").html(data.resultado[i].ESTADO);
                    //            $("#revisarSolicitud #fechaInicioContratoTrabajador").html(data.resultado[i].FECHAINICIO);
                    //            $("#revisarSolicitud #fechaTerminoContratoTrabajador").html(data.resultado[i].FECHATERMINO);
                    //            $("#revisarSolicitud #agenciaTrabajador").html(data.resultado[i].AGENCIA);
                    //            $("#revisarSolicitud #fechaDesvinculacionTrabajador").html(data.resultado[i].ACONTARDE);
                    //            $("#revisarSolicitud #causalDesvinculacionTrabajador").html(data.resultado[i].CAUSAL);
                    //        }
                    //        if (data.resultado[i].SECTION !== 'PTSOLB4J') {
                    //            if () {
                    //                for (var j = 0; j < data.resultado.length; j++) {

                    //                }
                    //            } else {

                    //            }
                                
                    //        }
                    //        //else if (data.resultado[i].SECTION === 'DATAFILE')
                    //        //{

                    //        //}
                    //        //else if (data.resultado[i].SECTION === 'DATADATES') {

                    //        //}
                    //    }
                        
                    //    if ((i + 1) === data.resultado.length) {
                    //        if (data.resultado[i].XSECTION === 'N') {
                    //            $("#tablaSolicitudesBaja > tbody").html(content);
                    //        } else {
                    //            $("#revisarSolicitud").modal("show");
                    //        }
                    //    }
                    //}
                
            },
            error: function (xhr) {
                console.log(xhr.responseText);
            },
            complete: function () {

            }
        });
    }

    function ajaxAdministrarProcesosSolB4J(page, evento, idSolB4j) {
        $.ajax({
            type: 'POST',
            url: page + '/SetAdministrarProcesosSolB4J',
            data: '{ evento: "' + evento + '", idSolB4j: "' + idSolB4j + '" }',
            dataType: 'json',
            contentType: 'application/json',
            async: true,
            success: function (response) {
                var data = JSON.parse(response.d);
                var content = "";
                if (data.resultado.length > 0) {
                    for (var i = 0; i < data.resultado.length; i++) {
                        if (data.resultado[i].VALIDACION === "0") {

                            $(".items_" + idSolB4j + " > td > .row .event-anot-process").html(data.resultado[i].ANOTACION_DOCUMENTO);
                            $(".items_" + idSolB4j + " > td > .row").attr("style", data.resultado[i].ESTADO_PROCESO);
                            $(".items_" + idSolB4j + " > td > .row .btn-process-index").removeClass(data.resultado[i].ESTADO_PROCESO_BTN_ANT).addClass(data.resultado[i].ESTADO_PROCESO_BTN);

                            if (data.resultado[i].BTN_REV_SOLB4J === "N") {
                                $(".items_" + idSolB4j + " > td > .row .btn-event-rev").hide();
                            } else {
                                $(".items_" + idSolB4j + " > td > .row .btn-event-rev").show();
                            }

                            if (data.resultado[i].BTN_REVERT_SOLB4J === "N") {
                                $(".items_" + idSolB4j + " > td > .row .btn-event-revert").hide();
                            } else {
                                $(".items_" + idSolB4j + " > td > .row .btn-event-revert").show();
                            }

                            if (data.resultado[i].BTN_APROV_SOLB4J === "N") {
                                $(".items_" + idSolB4j + " > td > .row .btn-event-aprov").hide();
                            } else {
                                $(".items_" + idSolB4j + " > td > .row .btn-event-aprov").show();
                            }

                            if (data.resultado[i].BTN_RECHZ_SOLB4J === "N") {
                                $(".items_" + idSolB4j + " > td > .row .btn-event-rechz").hide();
                            } else {
                                $(".items_" + idSolB4j + " > td > .row .btn-event-rechz").show();
                            }

                            if (data.resultado[i].BTN_ANUL_SOLB4J === "N") {
                                $(".items_" + idSolB4j + " > td > .row .btn-event-anular").hide();
                            } else {
                                $(".items_" + idSolB4j + " > td > .row .btn-event-anular").show();
                            }
                            

                        }
                    }
                }
            },
            error: function (xhr) {
                console.log(xhr.responseText);
            },
            complete: function () {

            }
        });
    }
    
    function ajaxDownloadFile(data) {
        $.ajax({
            type: 'POST',
            url: 'DownloadFiles.ashx',
            data: data,
            processData: false,
            contentType: false
        });
    }

    function ajaxTemporaryPDF(data) {
        $.ajax({
            type: 'POST',
            url: 'PDFTemporal.ashx',
            data: data,
            processData: false,
            contentType: false
        });
    }

});