$(document).ready(function () {

    /** GENERIC */

    var dateNotConcur = { fechas: [] };
    var comSimple = "'";

    var path = window.location.href.split('/')[window.location.href.split('/').length - 1];

    $("#rut").on("blur", function () {
        //if ($(this).val().length >= 8 && $(this).val().length <= 9) {
        //    $(this).val(formatRutTypeSoftland($(this).val()));
        //}

        //if ($(this).val().length >= 8 && $(this).val().length <= 9) {
        //    $("#btnBuscarTrabajador").removeAttr("disabled");
        //} else {
        //    $("#btnBuscarTrabajador").attr("disabled", "disabled");
        //}

        //$(this).val(maskRutTypeSoftland($(this).val()));
        $(this).val(formatRutTypeSoftland($(this).val()));
        $("#btnBuscarTrabajador").removeAttr("disabled");

    });
    
    ajaxObtenerCausalesDespido(path);

    $("#btnVisualizarFechas").on("click", function () {
        $("#modalFechasAgregadas").modal("show");
    });

    $(document).on("click", ".notEvent", function (e) {
        e.preventDefault();
    });

    $("#fechaDesvinculacion").on("change", function () {
        var html = '';

        if ($(this).val() !== "" && $(this).val().length === 10 && $(this).val().split('-').length - 1 === 2) {
            if (diffDays($(this).val()) > 0) {
                if (diffDays($(this).val()) === 1) {
                    html = '<i class="glyphicon-tw-1x glyphicon-tw-checksuccess"></i>Fecha informada es para mañana.';
                    $("#eventFechaDesvinculacion").html(html).show().css("color", "rgb(92, 184, 92)");
                } else {
                    html = '<i class="glyphicon-tw-1x glyphicon-tw-checksuccess"></i>Fecha informada es para ' + diffDays($(this).val()) + ' días más.';
                    $("#eventFechaDesvinculacion").html(html).show().css("color", "rgb(92, 184, 92)");
                }
            } else {
                if (diffDays($(this).val()) === 0) {
                    html = '<i class="glyphicon-tw-1x glyphicon-tw-atencionwarning"></i>Fecha informada es hoy.';
                    $("#eventFechaDesvinculacion").html(html).show().css("color", "rgb(240, 173, 78)");
                } else {
                    html = '<i class="glyphicon-tw-1x glyphicon-tw-atencionwarning"></i>Fecha informada esta atrasada por ' + String(diffDays($(this).val())).split('-').join('') + ' días.';
                    $("#eventFechaDesvinculacion").html(html).show().css("color", "rgb(240, 173, 78)");
                }
            }
            
        }
    });

    $("#fechaDesvinculacion").on("keyup", function () {
        var html = "";
        if ($(this).val() !== "" && $(this).val().length === 10 && $(this).val().split('-').length - 1 === 2) {
            if (diffDays($(this).val()) > 0) {
                if (diffDays($(this).val()) === 1) {
                    html = '<i class="glyphicon-tw-1x glyphicon-tw-checksuccess"></i>Fecha informada es para mañana.';
                    $("#eventFechaDesvinculacion").html(html).show().css("color", "rgb(92, 184, 92)");
                } else {
                    html = '<i class="glyphicon-tw-1x glyphicon-tw-checksuccess"></i>Fecha informada es para ' + diffDays($(this).val()) + ' días más.';
                    $("#eventFechaDesvinculacion").html(html).show().css("color", "rgb(92, 184, 92)");
                }
            } else {
                html = '<i class="glyphicon-tw-1x glyphicon-tw-atencionwarning"></i>Fecha informada esta atrasada por ' + String(diffDays($(this).val())).split('-').join('') + ' días.';
                $("#eventFechaDesvinculacion").html(html).show().css("color", "rgb(240, 173, 78)");
            }
        } else  if ($(this).val() !== "") {
            if ($(this).val().length !== 10 || $(this).val().split('-').length - 1 !== 2) {
                html = '<i class="glyphicon-tw-1x glyphicon-tw-errordanger"></i>El formato de la fecha no es el correcto DD-MM-AAAA';

                $("#eventFechaDesvinculacion").html(html).show().css("color", "rgb(217, 83, 79)");
            }
        }else if ($(this).val() === "") {
            $("#eventFechaDesvinculacion").html(html).hide();
        }
    });

    /** METODOS ESPECIALES */

    $("#btnBuscarTrabajador").on("click", function () {
        ajaxValidarPermitidoSolBj4(path, $("#rut").val());
        ajaxListarContratosFiniquitados(path, $("#rut").val());
    });

    $("#ddlContratos").on("change", function () {
        ajaxObtenerCargo(path, $(this).val());
        ajaxObtenerAreaNegocio(path, $(this).val());
        ajaxContratoActivoBaja(path, $("#rut").val(), $(this).val());
        ajaxCentroCosto(path, $(this).val());
    });

    $("#ddlCausalDespido").on("change", function () {
        if ($(this).val() === "12") {
            $("#boxAdicInformation").show();
        } else {
            $("#boxAdicInformation").hide();
        }
    });

    $("#btnGeneraSolB4j").on("click", function () {
        var data = new FormData();
        var files = $("#fileUpload").get(0).files;

        if (files.length > 0) {
            data.append("lengthFiles", files.length);
            for (var i = 0; i < files.length; i++) {
                data.append("uploadFiles" + (i + 1), files[i]);
            }
        }

        var filePath = "";
        var datesString = "";
        var extensions = "";

        /** BLOCK PROCESS FILE NAME */

        if (files.length > 0) {
            for (var j = 0; j < files.length; j++) {
                if (j === 0) {
                    filePath = standarFileName(files[j].name);
                    extensions = files[j].name.split('.')[files[j].name.split('.').length - 1];
                } else {
                    filePath = filePath + ";" + standarFileName(files[j].name);
                    extensions = extensions + ";" + files[j].name.split('.')[files[j].name.split('.').length - 1];
                }
            }
        }

        /** BLOCK PROCESS DATES */

        if (dateNotConcur.fechas.length > 0) {
            for (var h = 0; h < dateNotConcur.fechas.length; h++) {
                if (h === 0) {
                    datesString = dateNotConcur.fechas[h];
                } else {
                    datesString = datesString + ";" + dateNotConcur.fechas[h];
                }
            }
        }

        var existe = true;
        var adcexiste = true;

        var rut = $("#rut").val();
        var fechaDesvinculcacion = $("#fechaDesvinculacion").val();
        var nombreTrabajador = $("#nombre").val();
        var ficha = $("#ddlContratos").val();
        var cargo = $("#cargo").val();
        var cargoMod = $("#cargomod").val();
        var cliente = $("#unidadNegocio").val();
        var areaNegocio = $("#areaNegocio").val();
        var fechaInicio = $("#fechaInicioContrato").val();
        var fechaTermino = $("#fechaFinContrato").val();
        var estado = $("#estado").val();
        var causal = $("#ddlCausalDespido").val();
        var observacion = $("#observacion").val();
        var agencia = $("#agencia").val();
        var adcFiles = filePath;
        var adcDates = datesString;
        var adcExtensions = extensions;
        var adccfiles = files.length;
        var adccdates = dateNotConcur.fechas.length;
        var filesForm = data;
        var adccExtensions = files.length;

        if (rut === "" || fechaDesvinculcacion === "" || nombreTrabajador === "" || ficha === "0" || cargo === "" || cargoMod === "" || cliente === "" || fechaInicio === "" ||
            estado === "" || causal === "1" || observacion === "" || areaNegocio === "" || agencia === "") {
            existe = false;
        }

        if (causal === "Art.160N3 | No concurrencia a sus labores sin causa justificada") {
            if (adccfiles === 0 || adccdates === 0 || adccExtensions === 0) {
                adcexiste = false;
            }
        }

        if (existe) {
            if (adcexiste) {
                ajaxCrearSolicitudB4J(path, rut, fechaDesvinculcacion, nombreTrabajador, ficha, cargo, cargoMod, areaNegocio, cliente, fechaInicio, fechaTermino, estado, causal, observacion,
                    agencia, adcFiles, adcDates, adcExtensions, adccfiles, adccdates, adccExtensions, filesForm);
            } else {
                swal({
                    type: 'warning',
                    title: 'Atención!',
                    text: 'Ha seleccionado causal de desvinculacion, no concurrencia a sus labores sin causa justificada, favor indicar fechas de no concurrencia y además adjuntar archivos necesarios.'
                });
            }
        } else {
            swal({
                type: 'warning',
                title: 'Atención!',
                text: 'Alguno de los datos no se especifico, favor revisar e intentar nuevamente.'
            });
        }
        
        
    });

    $("#btnCargarArchivos").on("click", function () {
        $("#fileUpload").val(null);
        $("#fileUpload").trigger("click");
    });

    $("#fileUpload").on("change", function () {

        var files = $("#fileUpload").get(0).files;
        

        var html = '';
        var boxLength = '';
        var nombCompress = '';

        if (files.length > 0) {
            for (var i = 0; i < files.length; i++) {
                if (files[i].name.length > 65) {
                    nombCompress = files[i].name.substr(0, 65) + '....' + files[i].name.split('.')[files[i].name.split('.').length - 1];
                }
                else {
                    nombCompress = files[i].name;
                }
                html = html + '<div style="width: 95%; margin: 10px auto 5px auto;">' +
                    '<button type="button" class="' + extensionFileType(files[i].name.split('.')[files[i].name.split('.').length - 1]) + '" title="' + files[i].name + '" style="border-radius: 50%;  margin-top: -25px; width: 45px; height: 45px; position: relative; ">' +
                    '<i class="' + extensionFileIcon(files[i].name.split('.')[files[i].name.split('.').length - 1]) + '" style="display: inline-block;"></i>' +
                    '</span>' +
                    '</button>' +
                    '<span style="display: inline-block; width: 80%; margin-left: 5px;">' +
                    '<p style="font-size: 13px; font-style: normal; display: inline-block; width: 100%; line-height: 15px;">Nombre Archivo<br/>' + nombCompress + '<br/>' +
                    extensionFile(files[i].name.split('.')[files[i].name.split('.').length - 1]) + '</p>' +
                    '</span></div>';
                if (files.length > 1) {
                    boxLength = 'Se han seleccionado ' + files.length + ' archivos.';
                } else {
                    boxLength = 'Se ha seleccionado ' + files.length + ' archivo.';
                }
            }
        }
        
        $("#boxLengthFiles").html(boxLength);
        $("#boxFiles").html(html);
    });

    $("#fechaNoConcurridas").on("focus", function () {
        $("#eventosuccessfecha").hide();
    });

    $("#btnAgregarFechas").on("click", function () {
        var fechaAgregar = $("#fechaNoConcurridas").val();
        var lengthFechas = dateNotConcur.fechas.length;
        var eventMessage = "";
        var fechaExistente = false;

        if (fechaAgregar !== "") {

            for (var i = 0; i < dateNotConcur.fechas.length; i++) {
                if (dateNotConcur.fechas[i] === fechaAgregar) {
                    fechaExistente = true;
                    break;
                }
            }

            if (!fechaExistente) {
                var html = '<div id="boxContentFecha' + lengthFechas + '" class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="margin: 10px auto 10px auto; border-bottom: 1px dashed rgb(200, 200, 200);">' +
                    '<button type="button" class="btn btn-primary" style="border-radius: 50%;  margin-top: -25px; width: 55px; height: 55px; position: relative;">' +
                    '<i class="glyphicon-tw-8 glyphicon-tw-date" style="display: inline-block;"></i>' +
                    '</button> ' +
                    '<p id="boxFechas' + lengthFechas + '" style="font-family: ' + comSimple + 'Open Sans Condensed' + comSimple + ', sans-serif; width: 45%; display: inline-block; line-height: 20px; margin-bottom: 20px; position: relative; text-align: left;">' +
                    fechaAgregar + '<br />' + convertFechaPalabra(fechaAgregar) +
                    '</p> ' +
                    '<button type="button" id="btnEventMod' + lengthFechas + '" class="btn btn-info btnEventModificarFecha" style="border-radius: 25px; margin-top: -20px;" data-post="' + lengthFechas + '">' +
                    '<i class="glyphicon-tw-6 glyphicon-tw-edit eventBtnIconMod" style="display: inline-block;"></i>' +
                    '<span style="display: inline-block; text-align: left;">' +
                    '<i class="titleBtnEventMod" style="font-family: ' + comSimple + 'Open Sans Condensed' + comSimple + ', sans-serif; font-size: 14px; font-style: normal; display: block; margin-bottom: -8px;">Modificar</i>' +
                    '<i class="title2BtnEventMod" style="font-family: ' + comSimple + 'Open Sans Condensed' + comSimple + ', sans-serif; font-size: 14px; font-style: normal; display: block;">Fecha</i>' +
                    '</span>' +
                    '</button> ' +
                    '<button type="button" id="btnEventDel' + lengthFechas + '" class="btn btn-danger btnEventEliminarFecha" style="border-radius: 25px; margin-top: -20px;" data-post="' + lengthFechas + '">' +
                    '<i class="glyphicon-tw-6 glyphicon-tw-trash eventBtnIconDel" style="display: inline-block;"></i>' +
                    '<span style="display: inline-block; text-align: left;">' +
                    '<i class="titleBtnEventDel" style="font-family: ' + comSimple + 'Open Sans Condensed' + comSimple + ', sans-serif; font-size: 14px; font-style: normal; display: block; margin-bottom: -8px;">Eliminar</i>' +
                    '<i class="title2BtnEventDel" style="font-family: ' + comSimple + 'Open Sans Condensed' + comSimple + ', sans-serif; font-size: 14px; font-style: normal; display: block;">Fecha</i>' +
                    '</span>' +
                    '</button>' +
                    '</div>';

                if (dateNotConcur.fechas.length > 0) {
                    $("#boxFechasNoConcurrencia").append(html);
                } else {
                    $("#boxFechasNoConcurrencia").html(html);
                }

                eventMessage = '<i class="glyphicon-tw-1x glyphicon-tw-checksuccess"></i>Fecha Agregada exitosamente';

                dateNotConcur.fechas.push(fechaAgregar);
                $("#eventosuccessfecha").html(eventMessage).show().css("color", "rgb(92, 184, 92)");
                $("#fechaNoConcurridas").val("");
            } else {
                eventMessage = '<i class="glyphicon-tw-1x glyphicon-tw-atencionwarning"></i>La fecha de no concurrencia ya la agrego';
                $("#eventosuccessfecha").html(eventMessage).show().css("color", "rgb(240, 173, 78)");
            }
        } else {
            eventMessage = '<i class="glyphicon-tw-1x glyphicon-tw-errordanger"></i>Especifique fecha de no concurrencia';
            $("#eventosuccessfecha").html(eventMessage).show().css("color", "rgb(217, 83, 79)");
        }
        
    });

    $(document).on("click", ".btnEventEliminarFecha", function () {
        dateNotConcur.fechas.splice($(this).attr("data-post"), 1);
        var positionRemove = parseInt($(this).attr("data-post"));
        if (dateNotConcur.fechas.length > 0) {
            $("#boxContentFecha" + positionRemove).remove();
            for (var i = positionRemove + 1; i <= dateNotConcur.fechas.length; i++) {

                $("#boxFechas" + i).attr("id", "boxFechas" + String(i - 1));
                $("#boxContentFecha" + i).attr("id", "boxContentFecha" + String(i - 1));

                if ($("#btnEventMod" + String(i)).length > 0) {
                    $("#btnEventMod" + String(i)).attr("data-post", String(i - 1));
                    $("#btnEventMod" + String(i)).attr("id", "btnEventMod" + String(i - 1));
                } else if ($("#btnEventAdd" + String(i)).length > 0) {
                    $("#btnEventAdd" + String(i)).attr("data-post", String(i - 1));
                    $("#btnEventAdd" + String(i)).attr("id", "btnEventAdd" + String(i - 1));
                }

                if ($("#btnEventDel" + String(i)).length > 0) {
                    $("#btnEventDel" + String(i)).attr("data-post", String(i - 1));
                    $("#btnEventDel" + String(i)).attr("id", "btnEventDel" + String(i - 1));
                } else if ($("#btnEventCan" + String(i)).length > 0) {
                    $("#btnEventCan" + String(i)).attr("data-post", String(i - 1));
                    $("#btnEventCan" + String(i)).attr("id", "btnEventCan" + String(i - 1));
                }

                if ($("#btnEventCan" + String(i)).length > 0 && $("#btnEventAdd" + String(i)).length > 0) {
                    $("#eventModDay" + String(i)).attr("id", "eventModDay" + String(i - 1));
                    $("#eventModMonth" + String(i)).attr("id", "eventModMonth" + String(i - 1));
                    $("#eventModYear" + String(i)).attr("id", "eventModYear" + String(i - 1));
                }
                
            }
        } else {
            $("#boxContentFecha" + $(this).attr("data-post")).remove();

            html = '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">'+
                   '<div class="glyphicon-tw-7 glyphicon-tw-datedisabled" style="display: inline-block; width: 100px; height: 100px;"></div>'+
                   '<p style="font-family: ' + comSimple + 'Open Sans Condensed' + comSimple + ', sans-serif;">No se han agregado fechas de no concurrencia para la causal<br />Artículo 160 N° 3 | No concurrencia a sus labores sin causa justificada</p>'+
                   '</div>';
            $("#boxFechasNoConcurrencia").html(html);
        }

        console.log(dateNotConcur);
    });

    $(document).on("click", ".btnEventModificarFecha", function () {
        var html = '<span style="display: block; text-align: left;">Nueva Fecha</span>' +
            '<select id="eventModDay' + $(this).attr("data-post") + '" class="form-control" style="width: 32%; display: inline-block;">' + daysList() + '</select>' +
            '<select id="eventModMonth' + $(this).attr("data-post") + '" class="form-control" style="width: 32%; display: inline-block;">' + MonthList() + '</select>' +
            '<select id="eventModYear' + $(this).attr("data-post") + '" class="form-control" style="width: 32%; display: inline-block;">' + YearList() + '</select>' +
            '<span id="eventError' + $(this).attr("data-post") + '" style="position: absolute; display: block; padding-left: 25px;"></span>';

        $("#boxFechas" + $(this).attr("data-post")).html(html).css("width", "38%");

        /** modificacion de boton evento eliminar por cancelar */

        $("#btnEventDel" + $(this).attr("data-post") + " .titleBtnEventDel").html("Cancelar");
        $("#btnEventDel" + $(this).attr("data-post") + " .title2BtnEventDel").html("Modificación");
        $("#btnEventDel" + $(this).attr("data-post") + " .eventBtnIconDel").removeClass("glyphicon-tw-trash").addClass("glyphicon-tw-cancel");
        $("#btnEventDel" + $(this).attr("data-post")).removeClass("btnEventEliminarFecha").addClass("btnEventCancelarMod");
        $("#btnEventDel" + $(this).attr("data-post")).attr("id", "btnEventCan" + $(this).attr("data-post"));

        /** modificacion de boton evento modificar por guardar */

        $("#btnEventMod" + $(this).attr("data-post") + " .titleBtnEventMod").html("Guardar");
        $("#btnEventMod" + $(this).attr("data-post") + " .title2BtnEventMod").html("Modificación");
        $("#btnEventMod" + $(this).attr("data-post") + " .eventBtnIconMod").removeClass("glyphicon-tw-edit").addClass("glyphicon-tw-check");
        $("#btnEventMod" + $(this).attr("data-post")).removeClass("btn-info").addClass("btn-success");
        $("#btnEventMod" + $(this).attr("data-post")).removeClass("btnEventModificarFecha").addClass("btnEventGuardarMod");
        $("#btnEventMod" + $(this).attr("data-post")).attr("id", "btnEventAdd" + $(this).attr("data-post"));

    });

    $(document).on("click", ".btnEventGuardarMod", function () {
        var htmlError = '';

        if ($("#eventModDay" + $(this).attr("data-post")).val() !== "0" && $("#eventModMonth" + $(this).attr("data-post")).val() !== "0" && $("#eventModYear" + $(this).attr("data-post")).val() !== "0") {
            if (dateNotConcur.fechas[$(this).attr("data-post")] !== $("#eventModDay" + $(this).attr("data-post")).val() + "-" +
                                                                    $("#eventModMonth" + $(this).attr("data-post")).val() + "-" +
                                                                    $("#eventModYear" + $(this).attr("data-post")).val()) {
                dateNotConcur.fechas[$(this).attr("data-post")] =
                    $("#eventModDay" + $(this).attr("data-post")).val() + "-" +
                    $("#eventModMonth" + $(this).attr("data-post")).val() + "-" +
                    $("#eventModYear" + $(this).attr("data-post")).val();

                /** modificacion de boton evento eliminar por cancelar */

                $("#btnEventCan" + $(this).attr("data-post") + " .titleBtnEventDel").html("Eliminar");
                $("#btnEventCan" + $(this).attr("data-post") + " .title2BtnEventDel").html("Fecha");
                $("#btnEventCan" + $(this).attr("data-post") + " .eventBtnIconDel").removeClass("glyphicon-tw-cancel").addClass("glyphicon-tw-trash");
                $("#btnEventCan" + $(this).attr("data-post")).removeClass("btnEventCancelarMod").addClass("btnEventEliminarFecha");
                $("#btnEventCan" + $(this).attr("data-post")).attr("id", "btnEventDel" + $(this).attr("data-post"));

                /** modificacion de boton evento modificar por guardar */

                $("#btnEventAdd" + $(this).attr("data-post") + " .titleBtnEventMod").html("Modificar");
                $("#btnEventAdd" + $(this).attr("data-post") + " .title2BtnEventMod").html("Fecha");
                $("#btnEventAdd" + $(this).attr("data-post") + " .eventBtnIconMod").removeClass("glyphicon-tw-check").addClass("glyphicon-tw-edit");
                $("#btnEventAdd" + $(this).attr("data-post")).removeClass("btn-success").addClass("btn-info");
                $("#btnEventAdd" + $(this).attr("data-post")).removeClass("btnEventGuardarMod").addClass("btnEventModificarFecha");
                $("#btnEventAdd" + $(this).attr("data-post")).attr("id", "btnEventMod" + $(this).attr("data-post"));

                var html = dateNotConcur.fechas[$(this).attr("data-post")] + "<br/>" + convertFechaPalabra(dateNotConcur.fechas[$(this).attr("data-post")]);

                $("#boxFechas" + $(this).attr("data-post")).html(html).css("width", "45%");
            } else {
                htmlError = '<i class="glyphicon-tw-1x glyphicon-tw-atencionwarning" style="margin-left: -20px; margin-top: -1px;"></i>Es la misma fecha que existe';
                //sadasda
                $("#eventError" + $(this).attr("data-post")).html(htmlError).show().css("color", "rgb(240, 173, 78)");
            }
        } else {
            htmlError = '<i class="glyphicon-tw-1x glyphicon-tw-errordanger" style="margin-left: -20px; margin-top: -1px;"></i>Favor especificar fecha';
            $("#eventError" + $(this).attr("data-post")).html(htmlError).show().css("color", "rgb(217, 83, 79)");
        }
    });

    $(document).on("click", ".btnEventCancelarMod", function () {
        /** modificacion de boton evento eliminar por cancelar */

        $("#btnEventCan" + $(this).attr("data-post") + " .titleBtnEventDel").html("Eliminar");
        $("#btnEventCan" + $(this).attr("data-post") + " .title2BtnEventDel").html("Fecha");
        $("#btnEventCan" + $(this).attr("data-post") + " .eventBtnIconDel").removeClass("glyphicon-tw-cancel").addClass("glyphicon-tw-trash");
        $("#btnEventCan" + $(this).attr("data-post")).removeClass("btnEventCancelarMod").addClass("btnEventEliminarFecha");
        $("#btnEventCan" + $(this).attr("data-post")).attr("id", "btnEventDel" + $(this).attr("data-post"));

        /** modificacion de boton evento modificar por guardar */

        $("#btnEventAdd" + $(this).attr("data-post") + " .titleBtnEventMod").html("Modificar");
        $("#btnEventAdd" + $(this).attr("data-post") + " .title2BtnEventMod").html("Fecha");
        $("#btnEventAdd" + $(this).attr("data-post") + " .eventBtnIconMod").removeClass("glyphicon-tw-check").addClass("glyphicon-tw-edit");
        $("#btnEventAdd" + $(this).attr("data-post")).removeClass("btn-success").addClass("btn-info");
        $("#btnEventAdd" + $(this).attr("data-post")).removeClass("btnEventGuardarMod").addClass("btnEventModificarFecha");
        $("#btnEventAdd" + $(this).attr("data-post")).attr("id", "btnEventMod" + $(this).attr("data-post"));
        
        var html = dateNotConcur.fechas[$(this).attr("data-post")] + "<br/>" + convertFechaPalabra(dateNotConcur.fechas[$(this).attr("data-post")]);

        $("#boxFechas" + $(this).attr("data-post")).html(html).css("width", "45%");
    });

    /**
     * METODOS FUNCIONES
     */

    function ajaxValidarPermitidoSolBj4(page, rut) {
        $.ajax({
            type: 'POST',
            url: page + '/GetValidarPermitidoSolBj4',
            data: '{ rut: "' + rut + '" }',
            dataType: 'json',
            contentType: 'application/json',
            async: true,
            success: function (response) {
                var data = JSON.parse(response.d);
                if (data.resultado.length > 0) {
                    for (var i = 0; i < data.resultado.length; i++) {
                        if (data.resultado[i].ESBAJA === "N") {
                            ajaxRutTrabajador(page, rut);
                        } else {
                            swal({
                                type: 'warning',
                                title: 'Atención!',
                                text: 'El trabajador que esta buscando se encuentra en estado de baja.'
                            });
                        }
                    }
                }
            },
            error: function (xhr) {
                swal({
                    type: 'warning',
                    title: "Atención!",
                    text: "Servicio no disponible en este momento, intentelo más tarde.",
                    showConfirmButton: false,
                    timer: 2000
                });
            },
            complete: function () {

            }
        });
    }

    function ajaxRutTrabajador(page, rut) {
        $.ajax({
            type: 'POST',
            url: page + '/GetRutTrabajador',
            data: '{ rut: "' + rut + '" }',
            dataType: 'json',
            contentType: 'application/json',
            async: true,
            success: function (response) {
                var data = JSON.parse(response.d);
                if (data.resultado.length > 0) {
                    for (var i = 0; i < data.resultado.length; i++) {
                        $("#nombre").val(data.resultado[i].nombres);
                    }
                }
            },
            error: function (xhr) {
                swal({
                    type: 'warning',
                    title: "Atención!",
                    text: "Servicio no disponible en este momento, intentelo más tarde.",
                    showConfirmButton: false,
                    timer: 2000
                });
            },
            complete: function () {

            }
        });
    }

    function ajaxListarContratosFiniquitados(page, rut) {
        $.ajax({
            type: 'POST',
            url: page + '/GetListarContratosFiniquitados',
            data: '{ rut: "' + rut + '" }',
            dataType: 'json',
            contentType: 'application/json',
            async: true,
            success: function (response) {
                var data = JSON.parse(response.d);
                if (data.resultado.length > 0) {
                    $("#ddlContratos").html('<option value="0">Seleccione ficha o contrato</option>');
                    for (var i = 0; i < data.resultado.length; i++) {
                        $("#ddlContratos").append('<option value="' + data.resultado[i].ficha + '">' + data.resultado[i].ficha + '</option>');

                    }
                }
            },
            error: function (xhr) {
                swal({
                    type: 'warning',
                    title: "Atención!",
                    text: "Servicio no disponible en este momento, intentelo más tarde.",
                    showConfirmButton: false,
                    timer: 2000
                });
            },
            complete: function () {

            }
        });
    }

    function ajaxContratoActivoBaja(page, rut, ficha) {
        $.ajax({
            type: 'POST',
            url: page + '/GetContratoActivoBaja',
            data: '{ rut: "' + rut + '", ficha: "' + ficha + '" }',
            dataType: 'json',
            contentType: 'application/json',
            async: true,
            success: function (response) {
                var data = JSON.parse(response.d);
                if (data.resultado.length > 0) {
                    for (var i = 0; i < data.resultado.length; i++) {
                        $("#fechaInicioContrato").val(data.resultado[i].fechaIngreso);
                        $("#fechaFinContrato").val(data.resultado[i].FecTermContrato);
                        $("#estado").val(data.resultado[i].codEstudios);
                    }
                }
            },
            error: function (xhr) {
                swal({
                    type: 'warning',
                    title: "Atención!",
                    text: "Servicio no disponible en este momento, intentelo más tarde.",
                    showConfirmButton: false,
                    timer: 2000
                });
            },
            complete: function () {

            }
        });
    }

    function ajaxObtenerAreaNegocio(page, ficha) {
        $.ajax({
            type: 'POST',
            url: page + '/GetObtenerAreaNegocio',
            data: '{ ficha: "' + ficha + '" }',
            dataType: 'json',
            contentType: 'application/json',
            async: true,
            success: function (response) {
                var data = JSON.parse(response.d);
                if (data.resultado.length > 0) {
                    for (var i = 0; i < data.resultado.length; i++) {
                        $("#unidadNegocio").val(data.resultado[i].DesArn);
                        $("#areaNegocio").val(data.resultado[i].codArn);
                    }
                }
            },
            error: function (xhr) {
                swal({
                    type: 'warning',
                    title: "Atención!",
                    text: "Servicio no disponible en este momento, intentelo más tarde.",
                    showConfirmButton: false,
                    timer: 2000
                });
            },
            complete: function () {

            }
        });
    }

    function ajaxObtenerCargo(page, ficha) {
        $.ajax({
            type: 'POST',
            url: page + '/GetObtenerCargo',
            data: '{ ficha: "' + ficha + '" }',
            dataType: 'json',
            contentType: 'application/json',
            async: true,
            success: function (response) {
                var data = JSON.parse(response.d);
                if (data.resultado.length > 0) {
                    for (var i = 0; i < data.resultado.length; i++) {
                        $("#cargomod").val(data.resultado[i].glosa);
                        $("#cargo").val(data.resultado[i].CarNom);
                    }
                }
            },
            error: function (xhr) {
                swal({
                    type: 'warning',
                    title: "Atención!",
                    text: "Servicio no disponible en este momento, intentelo más tarde.",
                    showConfirmButton: false,
                    timer: 2000
                });
            },
            complete: function () {

            }
        });
    }

    function ajaxObtenerCausalesDespido(page) {
        $.ajax({
            type: 'POST',
            url: page + '/GetObtenerCausalesDespido',
            data: '{ }',
            dataType: 'json',
            contentType: 'application/json',
            async: true,
            success: function (response) {
                var data = JSON.parse(response.d);
                if (data.resultado.length > 0) {
                    for (var i = 0; i < data.resultado.length; i++) {
                        $("#ddlCausalDespido").append('<option value="' + data.resultado[i].ID + '">' + data.resultado[i].UNIFICADO + '</option>');

                    }
                }
            },
            error: function (xhr) {
                swal({
                    type: 'warning',
                    title: "Atención!",
                    text: "Servicio no disponible en este momento, intentelo más tarde.",
                    showConfirmButton: false,
                    timer: 2000
                });
            },
            complete: function () {

            }
        });
    }

    function ajaxCentroCosto(page, ficha) {
        $.ajax({
            type: 'POST',
            url: page + '/GetCentroCosto',
            data: '{ ficha: "' + ficha + '" }',
            dataType: 'json',
            contentType: 'application/json',
            async: true,
            success: function (response) {
                var data = JSON.parse(response.d);
                if (data.resultado.length > 0) {
                    for (var i = 0; i < data.resultado.length; i++) {
                        $("#agencia").val(data.resultado[i].DescCC);
                    }
                }
            },
            error: function (xhr) {
                swal({
                    type: 'warning',
                    title: "Atención!",
                    text: "Servicio no disponible en este momento, intentelo más tarde.",
                    showConfirmButton: false,
                    timer: 2000
                });
            },
            complete: function () {

            }
        });
    }

    function ajaxUploadFile(data) {
        $.ajax({
            type: 'POST',
            url: 'UploadFiles.ashx',
            data: data,
            processData: false,
            contentType: false
        });
    }

    function ajaxCrearSolicitudB4J(page, rut, fechaDesvinculcacion, nombreTrabajador, ficha, cargo, cargoMod, areaNegocio, cliente, fechaInicio, fechaTermino, estado, causal,
                                   observacion, agencia, adcFiles, adcDates, adcExtensions, adccfiles, adccdates, adccExntesions, files) {
        $.ajax({
            type: 'POST',
            url: page + '/SetCrearSolicitudB4J',
            data: '{ rut: "' + rut + '", fechaDesvinculcacion: "' + fechaDesvinculcacion + '", nombreTrabajador: "' + nombreTrabajador + '", ficha: "' + ficha +
                  '", cargo: "' + cargo + '", cargoMod: "' + cargoMod + '", areaNegocio: "' + areaNegocio + '", cliente: "' + cliente + '", fechaInicio: "' + fechaInicio + '", fechaTermino: "' + fechaTermino +
                  '", estado: "' + estado + '", causal: "' + causal + '", observacion: "' + observacion + '", agencia: "' + agencia + '", adcFiles: "' + adcFiles + '", adcDates: "' + adcDates +
                  '", adcExtensions: "' + adcExtensions + '", adccfiles: "' + adccfiles + '", adccdates: "' + adccdates + '", adccExtensions: "' + adccExntesions + '" }',
            dataType: 'json',
            contentType: 'application/json',
            async: true,
            success: function (response) {
                var data = JSON.parse(response.d);
                if (data.resultado.length > 0) {
                    for (var i = 0; i < data.resultado.length; i++) {
                        if (data.resultado[i].VALIDACION === "0") {
                            //ajaxCorreoElectronicoTeamwork(page, data.resultado[i].TRANSACCION);
                            files.append("transaccion", data.resultado[i].TRANSACCION);
                            ajaxUploadFile(files);
                        } else {
                            swal({
                                type: 'error',
                                title: 'Ha ocurrido algo!',
                                text: data.resultado[i].RESULTADO
                            });
                        }
                    }
                }
            },
            error: function (xhr) {
                swal({
                    type: 'warning',
                    title: "Atención!",
                    text: "Servicio no disponible en este momento, intentelo más tarde.",
                    showConfirmButton: false,
                    timer: 2000
                });
            },
            complete: function () {

            }
        });
    }

    function ajaxCorreoElectronicoTeamwork(page, transaccion) {
        $.ajax({
            type: 'POST',
            url: page + '/GetCorreoElectronicoTeamwork',
            data: '{ transaccion: "' + transaccion + '" }',
            dataType: 'json',
            contentType: 'application/json',
            async: true,
            success: function (response) {
                swal({
                    type: 'success',
                    title: 'Solicitud de Baja',
                    text: 'Se ha informado la baja, sera emitida durante los proximos dias'
                });
            },
            error: function (xhr) {
                swal({
                    type: 'warning',
                    title: "Atención!",
                    text: "Servicio no disponible en este momento, intentelo más tarde.",
                    showConfirmButton: false,
                    timer: 2000
                });
            },
            complete: function () {

            }
        });
    }

});