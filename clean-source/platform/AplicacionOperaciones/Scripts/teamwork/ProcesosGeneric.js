$(document).ready(function () {

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
            prefix = prefix + window.location.href.split('/')[window.location.href.split('/').length - 1] + "/";
        } else {
            prefix = prefix + window.location.href.split('/')[4] + "/";
        }
    }

    /** END OBTENCION DINAMICA DE URL DE APLICACION PARA ACCESO A METODOS INTERNOS */

    $(document).on("click", ".eventAnularSolicitud", function (e) {
        e.preventDefault();
        $("#modalAnularSolicitud #tituloAnulacion").html($(this).attr("data-solicitud"));
        $("#modalAnularSolicitud .tipoAnulacion").html(atob($(this).attr("data-tipoevento")));
        $(".eventDeclararAnulacion").attr("data-tipoevento", $(this).attr("data-tipoevento"));
        $(".eventDeclararAnulacion").attr("data-proceso", $(this).attr("data-solicitud"));
        $(".eventDeclararAnulacion").attr("data-cifrado", $(this).attr("data-cifrado"));
        $(".eventDeclararAnulacion").attr("data-tipoanulacion", $(this).attr("data-tipoanulacion"));
        $("#modalAnularSolicitud").modal("show");
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
                $("#modalConfirmarEvento .tipoAnulacion").html(atob($(this).attr("data-tipoevento")));
                $("#modalConfirmarEvento .eventConfirmarAnulacion").attr("data-codigotransaccion", $(this).attr("data-cifrado"));
                $("#modalConfirmarEvento .eventConfirmarAnulacion").attr("data-tipoevento", $(this).attr("data-tipoevento"));
                $("#modalConfirmarEvento .eventConfirmarAnulacion").attr("data-tipoanulacion", $(this).attr("data-tipoanulacion"));
                $("#modalConfirmarEvento .eventConfirmarAnulacion").attr("data-proceso", $(this).attr("data-proceso"));
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
            case "Proceso":

                var codigoTransaccion = $(this).attr("data-codigotransaccion");
                var tipoEvento = $(this).attr("data-tipoevento");
                var motivoanulacion = $(this).attr("data-motivoanulacion");
                var proceso = $(this).attr("data-proceso");

                $("#modalConfirmarEvento").modal("hide");
                ajaxViewPartialLoaderTransaccion(prefixDomain + prefix, "#loaderSearch");
                setTimeout(function () {
                    sessionStorage.setItem("ApplicationNombreProceso", proceso);
                    ajaxAnularProceso(prefixDomain + prefix, codigoTransaccion, tipoEvento, motivoanulacion, proceso);
                }, 500);
                break;
        }
    });

    $(".eventDescartarAnulacion").on("click", function (e) {
        e.preventDefault();
        $("#modalConfirmarEvento").modal("hide");
    });

    $(document).on("click", ".eventTerminarSolicitud", function (e) {
        e.preventDefault();
        $(".eventConfirmarTerminado").attr("data-tipoevento", $(this).attr("data-tipoevento"));
        $(".eventConfirmarTerminado").attr("data-proceso", $(this).attr("data-solicitud"));
        $(".eventConfirmarTerminado").attr("data-codigotransaccion", $(this).attr("data-cifrado"));
        $(".eventConfirmarTerminado").attr("data-tipoanulacion", $(this).attr("data-tipoanulacion"));
        $("#modalConfirmarEventoTerminado").modal("show");
    });

    $(".eventConfirmarTerminado").on("click", function (e) {
        e.preventDefault();
        switch ($(this).attr("data-tipoanulacion")) {
            case "Proceso":

                var codigoTransaccion = $(this).attr("data-codigotransaccion");
                var tipoEvento = $(this).attr("data-tipoevento");
                var proceso = $(this).attr("data-proceso");

                $("#modalConfirmarEventoTerminado").modal("hide");
                ajaxViewPartialLoaderTransaccion(prefixDomain + prefix, "#loaderSearch");
                setTimeout(function () {
                    sessionStorage.setItem("ApplicationNombreProceso", proceso);
                    ajaxTerminarProceso(prefixDomain + prefix, codigoTransaccion, tipoEvento, proceso);
                }, 500);
                break;
        }
    });

    $(".eventDescartarTerminado").on("click", function (e) {
        e.preventDefault();
        $("#modalConfirmarEventoTerminado").modal("hide");
    });





    $(document).on("click", ".eventRevertirTerminoSolicitud", function (e) {
        e.preventDefault();
        $(".eventConfirmarRevertirTermino").attr("data-tipoevento", $(this).attr("data-tipoevento"));
        $(".eventConfirmarRevertirTermino").attr("data-proceso", $(this).attr("data-solicitud"));
        $(".eventConfirmarRevertirTermino").attr("data-codigotransaccion", $(this).attr("data-cifrado"));
        $(".eventConfirmarRevertirTermino").attr("data-tipoanulacion", $(this).attr("data-tipoanulacion"));
        $("#modalConfirmarEventoRevertirTerminado").modal("show");
    });

    $(".eventConfirmarRevertirTermino").on("click", function (e) {
        e.preventDefault();
        switch ($(this).attr("data-tipoanulacion")) {
            case "Proceso":

                var codigoTransaccion = $(this).attr("data-codigotransaccion");
                var tipoEvento = $(this).attr("data-tipoevento");
                var proceso = $(this).attr("data-proceso");

                $("#modalConfirmarEventoRevertirTerminado").modal("hide");
                ajaxViewPartialLoaderTransaccion(prefixDomain + prefix, "#loaderSearch");
                setTimeout(function () {
                    sessionStorage.setItem("ApplicationNombreProceso", proceso);
                    ajaxRevertirTerminoProceso(prefixDomain + prefix, codigoTransaccion, tipoEvento, proceso);
                }, 500);
                break;
        }
    });

    $(".eventDescartarRevertirTerminado").on("click", function (e) {
        e.preventDefault();
        $("#modalConfirmarEventoRevertirTerminado").modal("hide");
    });




    $(document).on("click", ".eventRenderPagination", function (e) {
        e.preventDefault();
        
        var date = new Date();

        var typeFilter = "";
        var dataFilter = "";
        var tipoDashboard = "Completo";
        var day = date.getDate();
        var month = date.getMonth();
        var year = date.getFullYear();
        var analista = "";
        var tipoEstado = "";
        var estado = "";

        if ($($(".dashboardEvent").attr("data-attrpadre")).attr("data-typedashboard") !== undefined) {
            tipoDashboard = $($(".dashboardEvent").attr("data-attrpadre")).attr("data-typedashboard");
        }

        if ($($(".dashboardEvent").attr("data-attrpadre")).attr("data-day") !== undefined) {
            day = $($(".dashboardEvent").attr("data-attrpadre")).attr("data-day");
        }

        if ($($(".dashboardEvent").attr("data-attrpadre")).attr("data-month") !== undefined) {
            month = $($(".dashboardEvent").attr("data-attrpadre")).attr("data-month");
        }

        if ($($(".dashboardEvent").attr("data-attrpadre")).attr("data-year") !== undefined) {
            year = $($(".dashboardEvent").attr("data-attrpadre")).attr("data-year");
        }

        if ($(".eventFiltrarEspecifico").attr("data-analista") !== undefined) {
            analista = $(".eventFiltrarEspecifico").attr("data-analista");
        }

        if ($(".eventFiltrarEspecifico").attr("data-tipo") !== undefined) {
            tipoEstado = $(".eventFiltrarEspecifico").attr("data-tipo");
        }

        if ($(".eventFiltrarEspecifico").attr("data-estado") !== undefined) {
            estado = $(".eventFiltrarEspecifico").attr("data-estado");
        }
        
        var codificado = $(this).attr("data-codificado");
        var tipoSolicitud = $(this).attr("data-tiposolicitud");
        var htmlRenderizado = $(this).attr("data-htmlrenderizado");
        var htmlPagination = $(this).attr("data-htmlpaginador");
        var htmlRenderizadoSearch = $(this).attr("data-htmlrenderizadosearch");
        var htmlHeader = "#renderizadoheader_" + $(this).attr("data-eventclick");
        
        switch ($("#" + $(this).attr("data-eventsearchget") + "_" + $(this).attr("data-eventclick")).val()) {
            case "RmljaGE=":
                typeFilter = $("#" + $(this).attr("data-eventsearchget") + "_" + $(this).attr("data-eventclick")).val();
                dataFilter = $("#" + $("option:selected", "#" + $(this).attr("data-eventsearchget") + "_" + $(this).attr("data-eventclick")).attr("data-input") + "_" + $(this).attr("data-eventclick")).val();
                break;
        }

        ajaxViewPartialLoaderTransaccion(prefixDomain + prefix, "#loaderSearch");
        setTimeout(function () {
            ajaxRenderizaProcesos(prefixDomain + prefix, codificado, tipoSolicitud, htmlHeader, htmlRenderizado, htmlPagination, htmlRenderizadoSearch, typeFilter, dataFilter, tipoDashboard, day,
                                  month, year, analista, tipoEstado, estado);
        }, 500);
    });

    $(".btnEventSearchProcess").on("click", function (e) {
        e.preventDefault();
        
        var date = new Date();

        var typeFilter = "";
        var dataFilter = "";
        var tipoDashboard = "Completo";
        var day = date.getDate();
        var month = date.getMonth();
        var year = date.getFullYear();
        var analista = "";
        var tipoEstado = "";
        var estado = "";
        
        if ($($(".dashboardEvent").attr("data-attrpadre")).attr("data-typedashboard") !== undefined) {
            tipoDashboard = $($(".dashboardEvent").attr("data-attrpadre")).attr("data-typedashboard");
        }

        if ($($(".dashboardEvent").attr("data-attrpadre")).attr("data-day") !== undefined) {
            day = $($(".dashboardEvent").attr("data-attrpadre")).attr("data-day");
        }

        if ($($(".dashboardEvent").attr("data-attrpadre")).attr("data-month") !== undefined) {
            month = $($(".dashboardEvent").attr("data-attrpadre")).attr("data-month");
        }

        if ($($(".dashboardEvent").attr("data-attrpadre")).attr("data-year") !== undefined) {
            year = $($(".dashboardEvent").attr("data-attrpadre")).attr("data-year");
        }

        if ($(".eventFiltrarEspecifico").attr("data-analista") !== undefined) {
            analista = $(".eventFiltrarEspecifico").attr("data-analista");
        }

        if ($(".eventFiltrarEspecifico").attr("data-tipo") !== undefined) {
            tipoEstado = $(".eventFiltrarEspecifico").attr("data-tipo");
        }

        if ($(".eventFiltrarEspecifico").attr("data-estado") !== undefined) {
            estado = $(".eventFiltrarEspecifico").attr("data-estado");
        }

        var codificado = "MS01";
        var tipoSolicitud = $(this).attr("data-tiposolicitud");
        var htmlRenderizado = $(this).attr("data-htmlrenderizado");
        var htmlPagination = $(this).attr("data-htmlpagination");
        var htmlRenderizadoSearch = $(this).attr("data-htmlrenderizadosearch");
        var htmlHeader = "#renderizadoheader_" + $("#" + $(this).attr("data-eventelementget") + "_" + $(this).attr("data-eventdataget")).attr("data-eventclick");

        switch ($("#" + $(this).attr("data-eventelementget") + "_" + $(this).attr("data-eventdataget")).val()) {
            case "RmljaGE=":
                typeFilter = $("#" + $(this).attr("data-eventelementget") + "_" + $(this).attr("data-eventdataget")).val();
                dataFilter = $("#" + $("option:selected", "#" + $(this).attr("data-eventelementget") + "_" + $(this).attr("data-eventdataget")).attr("data-input") + "_" + $(this).attr("data-eventdataget")).val();
                break;
        }

        if (typeFilter !== "" && dataFilter !== "") {
            ajaxViewPartialLoaderTransaccion(prefixDomain + prefix, "#loaderSearch");
            setTimeout(function () {
                ajaxRenderizaProcesos(prefixDomain + prefix, codificado, tipoSolicitud, htmlHeader, htmlRenderizado, htmlPagination, htmlRenderizadoSearch, typeFilter, dataFilter, tipoDashboard, day,
                    month, year, analista, tipoEstado, estado);
            }, 500);
        }
    });

    $(".eventSearchProcess").on("change", function () {
        switch ($(this).val())
        {
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