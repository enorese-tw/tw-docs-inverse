$(document).ready(function () {

    /** OBTENCION DINAMICA DE URL DE APLICACION PARA ACCESO A METODOS INTERNOS */

    var prefixDomain = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2];
    var prefix = "";
    var codigoTransaccion = window.location.href.split('/')[window.location.href.split('/').length - 1];

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

    $(document).on("click", ".eventAnularSolicitudInd", function (e) {
        e.preventDefault();
        $("#modalAnularSolicitud #tituloAnulacion").html($(this).attr("data-solicitud"));
        $("#modalAnularSolicitud .tipoAnulacion").html(atob($(this).attr("data-tipoevento")));
        $(".eventDeclararAnulacion").attr("data-tipoevento", $(this).attr("data-tipoevento"));
        $(".eventDeclararAnulacion").attr("data-cifrado", $(this).attr("data-cifrado"));
        $(".eventDeclararAnulacion").attr("data-tipoanulacion", $(this).attr("data-tipoanulacion"));
        $("#modalAnularSolicitud").modal("show");
    });

    $(document).on("click", ".eventRevertirTerminoSolicitudInd", function (e) {
        e.preventDefault();
        $(".eventConfirmarRevertirTerminoInd").attr("data-tipoevento", $(this).attr("data-tipoevento"));
        $(".eventConfirmarRevertirTerminoInd").attr("data-proceso", $(this).attr("data-solicitud"));
        $(".eventConfirmarRevertirTerminoInd").attr("data-codigotransaccion", $(this).attr("data-cifrado"));
        $(".eventConfirmarRevertirTerminoInd").attr("data-tipoanulacion", $(this).attr("data-tipoanulacion"));

        $("#modalConfirmarEventoRevertirTerminadoInd").modal("show");
    });


    $(".eventConfirmarRevertirTerminoInd").on("click", function (e) {
        e.preventDefault();
        switch ($(this).attr("data-tipoanulacion")) {
            case "Solicitud":

                var codigoTransaccion = $(this).attr("data-codigotransaccion");
                var tipoEvento = $(this).attr("data-tipoevento");
                var proceso = $(this).attr("data-proceso");

                $("#modalConfirmarEventoRevertirTerminadoInd").modal("hide");
                ajaxViewPartialLoaderTransaccion(prefixDomain + prefix, "#loaderSearch");
                setTimeout(function () {
                    sessionStorage.setItem("ApplicationNombreProceso", proceso);
                    ajaxRevertirTerminoSolicitud(prefixDomain + prefix, codigoTransaccion, tipoEvento, proceso);
                }, 500);
                break;
        }
    });



    $(".eventDescartarRevertirTerminadoInd").on("click", function (e) {
        e.preventDefault();
        $("#modalConfirmarEventoRevertirTerminadoInd").modal("hide");
    });



    $(".eventDeclararAnulacion").on("click", function (e) {
        e.preventDefault();
        var validado = true;
        if ($("#modalAnularSolicitud #selectMotivo").val() !== "0") {
            if ($("#modalAnularSolicitud #selectMotivo").val() === "otro") {
                if ($("#modalAnularSolicitud #OtroMotivoAnulacion").val() === "") {
                    validado = false;
                }
            }

            if (validado) {
                $("#modalConfirmarEvento").modal("show");
                $("#modalAnularSolicitud").modal("hide");
                $("#modalConfirmarEvento .tipoAnulacion").html($(this).attr("data-tipoevento"));
                $("#modalConfirmarEvento .eventConfirmarAnulacion").attr("data-codigotransaccion", $(this).attr("data-cifrado"));
                $("#modalConfirmarEvento .eventConfirmarAnulacion").attr("data-tipoevento", $(this).attr("data-tipoevento"));
                $("#modalConfirmarEvento .eventConfirmarAnulacion").attr("data-tipoanulacion", $(this).attr("data-tipoanulacion"));
                $("#modalConfirmarEvento").modal("show");
                if ($("#modalAnularSolicitud #selectMotivo").val() !== "otro") {
                    $("#modalConfirmarEvento #glosaMotivoAnulacion").html($("#modalAnularSolicitud #selectMotivo").val());
                    $("#modalConfirmarEvento .eventConfirmarAnulacion").attr("data-motivoanulacion", $("#modalAnularSolicitud #selectMotivo").val());
                } else {
                    $("#modalConfirmarEvento #glosaMotivoAnulacion").html($("#modalAnularSolicitud #OtroMotivoAnulacion").val());
                    $("#modalConfirmarEvento .eventConfirmarAnulacion").attr("data-motivoanulacion", $("#modalAnularSolicitud #OtroMotivoAnulacion").val());
                }
            } else {
                ajaxViewPartialLoaderErrorGenericModal(prefixDomain + prefix, "#modalAnularSolicitud .errorModal", "Favor indicar un motivo de anulación para generar el proceso correspondiente.");
            }
        }
        else {
            ajaxViewPartialLoaderErrorGenericModal(prefixDomain + prefix, "#modalAnularSolicitud .errorModal", "Favor indicar un motivo de anulación para generar el proceso correspondiente.");
        }
    });

    $(".eventConfirmarAnulacion").on("click", function (e) {
        e.preventDefault();
        switch ($(this).attr("data-tipoanulacion")) {
            case "Solicitud":
                ajaxAnularSolicitud(prefixDomain + prefix, $(this).attr("data-codigotransaccion"), $(this).attr("data-tipoevento"), $(this).attr("data-motivoanulacion"));
                break;
        }
    });

    $(".eventDescartarAnulacion").on("click", function (e) {
        e.preventDefault();
        $("#modalConfirmarEvento").modal("hide");
    });

    $(document).on("click", ".eventRenderPaginationSol", function (e) {
        e.preventDefault();
        
        var typeFilter = "";
        var dataFilter = "";
        var codigoTransaccionSearch = "";
        var codificado = $(this).attr("data-codificado");
        var tipoSolicitud = $(this).attr("data-tiposolicitud");
        var htmlRenderizado = $(this).attr("data-htmlrenderizado");
        var htmlPagination = $(this).attr("data-htmlpaginador");
        var htmlRenderizadoSearch = $(this).attr("data-htmlrenderizadosearch");

        if (codigoTransaccion !== "Solicitudes" && codigoTransaccion !== "Contrato" && codigoTransaccion !== "Renovacion") {
            codigoTransaccionSearch = codigoTransaccion;
        }

        switch ($("#" + $(this).attr("data-eventsearchget") + "_" + $(this).attr("data-eventclick")).val()) {
            case "RmljaGE=":
                typeFilter = $("#" + $(this).attr("data-eventsearchget") + "_" + $(this).attr("data-eventclick")).val();
                dataFilter = $("#" + $("option:selected", "#" + $(this).attr("data-eventsearchget") + "_" + $(this).attr("data-eventclick")).attr("data-input") + "_" + $(this).attr("data-eventclick")).val();
                break;
        }   

        ajaxViewPartialLoaderTransaccion(prefixDomain + prefix, "#loaderSearch");
        setTimeout(function () {
            ajaxRenderizaSolicitudesPro(prefixDomain + prefix, codificado, tipoSolicitud, htmlRenderizado, htmlPagination, codigoTransaccionSearch, typeFilter, dataFilter);
        }, 500);
    });

    $(".btnEventSearchSolicitudes").on("click", function (e) {
        e.preventDefault();

        var typeFilter = "";
        var dataFilter = "";
        var codigoTransaccionSearch = "";

        var codificado = "MS01";
        var tipoSolicitud = $(this).attr("data-tiposolicitud");
        var htmlRenderizado = $(this).attr("data-htmlrenderizado");
        var htmlPagination = $(this).attr("data-htmlpagination");
        var htmlRenderizadoSearch = $(this).attr("data-htmlrenderizadosearch");

        if (codigoTransaccion !== "Solicitudes" && codigoTransaccion !== "Contrato" && codigoTransaccion !== "Renovacion") {
            codigoTransaccionSearch = codigoTransaccion;
        }

        switch ($("#" + $(this).attr("data-eventelementget") + "_" + $(this).attr("data-eventdataget")).val()) {
            case "RmljaGE=":
                typeFilter = $("#" + $(this).attr("data-eventelementget") + "_" + $(this).attr("data-eventdataget")).val();
                dataFilter = $("#" + $("option:selected", "#" + $(this).attr("data-eventelementget") + "_" + $(this).attr("data-eventdataget")).attr("data-input") + "_" + $(this).attr("data-eventdataget")).val();
                break;
        }

        if (typeFilter !== "" && dataFilter !== "") {
            ajaxViewPartialLoaderTransaccion(prefixDomain + prefix, "#loaderSearch");
            setTimeout(function () {
                ajaxRenderizaSolicitudesPro(prefixDomain + prefix, codificado, tipoSolicitud,
                    htmlRenderizado, htmlPagination, codigoTransaccionSearch, typeFilter, dataFilter);
            }, 500);
        }
    });

    $(".eventSearchSolicitudes").on("change", function () {
        switch ($(this).val()) {
            case "TW9zdHJhciBUb2Rv":
                $("#" + $("option:selected", this).attr("data-input") + "_" + $(this).attr("data-eventClick")).addClass("dspl-none").removeClass("dspl-inline-block");
                $("#btnEvent_" + $(this).attr("data-eventClick")).addClass("dspl-none").removeClass("dspl-inline-block");
                break;
            case "RmljaGE=":
                $("#" + $("option:selected", this).attr("data-input") + "_" + $(this).attr("data-eventClick")).addClass("dspl-inline-block").removeClass("dspl-none");
                $("#btnEvent_" + $(this).attr("data-eventClick")).addClass("dspl-inline-block").removeClass("dspl-none");
                break;
        }
    });

    $(".eventRestore").on("click", function (e) {
        e.preventDefault();
        location.reload();
    });

});