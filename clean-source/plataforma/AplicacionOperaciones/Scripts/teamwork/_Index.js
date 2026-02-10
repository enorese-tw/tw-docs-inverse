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
            prefix = prefix + "/" + window.location.href.split('/')[window.location.href.split('/').length - 1] + "/";
        } else {
            prefix = prefix + "/" + window.location.href.split('/')[4] + "/";
        }
    }

    /** END OBTENCION DINAMICA DE URL DE APLICACION PARA ACCESO A METODOS INTERNOS */

    $("#selectMotivo").on("change", function () {
        if ($(this).val() === "otro") {
            $("#OtroMotivoAnulacion").show();
        }
        else {
            $("#OtroMotivoAnulacion").hide();
        }
    });

    $(document).on("click", "#closeErrorGeneric", function (e) {
        e.preventDefault();
        $("#errorGeneric").hide();
    });


    $(".dashboardBtnKAM").on("click", function (e) {
        $($(this).attr("data-attrpadre")).removeClass("btn-success").addClass("btn-teamwork");
        $(".dashboardBtnKAM").attr("data-attrpadre", "#" + $(this).attr("id"));

        $(this).addClass("btn-success").removeClass("btn-teamwork");
    });

    $(".dashboardEvent").on("click", function (e) {
        e.preventDefault();

        var tipoDashboard = $(this).attr("data-typedashboard");
        var day = $(this).attr("data-day");
        var month = $(this).attr("data-month");
        var year = $(this).attr("data-year");
        var analista = "";
        var tipoEvento = "";
        var evento = "";

        $("#renderizadoheader_contratos").html('');
        $("#procesosContratos").html('');
        $("#procesosContratosPagination").html('');
        $("#renderizadoheader_renovaciones").html('');
        $("#procesosRenovacion").html('');
        $("#procesosRenovacionPagination").html('');

        $("#interactionplataforma").show();

        $($(this).attr("data-attrpadre")).removeClass("btn-success").addClass("btn-teamwork");
        $(".dashboardEvent").attr("data-attrpadre", "#" + $(this).attr("id"));

        $(this).addClass("btn-success").removeClass("btn-teamwork");

        ajaxViewPartialLoaderTransaccion(prefixDomain + prefix, "#loaderSearch");
        setTimeout(function () {
            ajaxRenderizaDashboardTotal(prefixDomain + prefix, "#contentDashboardTotales", tipoDashboard, day, month, year);
            ajaxRenderizaDashboardAnalistaContratos(prefixDomain + prefix, "#contentDashboardXContrato", tipoDashboard, day, month, year);
            //ajaxRenderizaProcesos(prefixDomain + prefix, "MS01", "U29saWNpdHVkQ29udHJhdG8=", "#renderizadoheader_contratos", "#procesosContratos", "#procesosContratosPagination", "renderizadoresult_contratos",
            //    "", "", tipoDashboard, day, month, year, analista, tipoEvento, evento);
            //ajaxRenderizaProcesos(prefixDomain + prefix, "MS01", "U29saWNpdHVkUmVub3ZhY2lvbg==", "#renderizadoheader_renovaciones",  "#procesosRenovacion", "#procesosRenovacionPagination", "renderizadoresult_renovaciones",
            //    "", "", tipoDashboard, day, month, year, analista, tipoEvento, evento);
        }, 500);
        
    });

    $(document).on("click", ".eventFiltrarEspecifico", function (e) {
        e.preventDefault();

        var tipoDashboard ='';
        var day = '';
        var month = '';
        var year = '';

        if ($($(".dashboardEvent").attr("data-attrpadre")).attr("data-typedashboard")) {
            tipoDashboard = $($(".dashboardEvent").attr("data-attrpadre")).attr("data-typedashboard");
            day = $($(".dashboardEvent").attr("data-attrpadre")).attr("data-day");
            month = $($(".dashboardEvent").attr("data-attrpadre")).attr("data-month");
            year = $($(".dashboardEvent").attr("data-attrpadre")).attr("data-year");
        }
        else {
            tipoDashboard = $($(".dashboardBtnKAM").attr("data-attrpadre")).attr("data-typedashboard");
            day = $($(".dashboardBtnKAM").attr("data-attrpadre")).attr("data-day");
            month = $($(".dashboardBtnKAM").attr("data-attrpadre")).attr("data-month");
            year = $($(".dashboardBtnKAM").attr("data-attrpadre")).attr("data-year");
        }


        //var tipoDashboard = $($(".dashboardEvent").attr("data-attrpadre")).attr("data-typedashboard");
        //var day = $($(".dashboardEvent").attr("data-attrpadre")).attr("data-day");
        //var month = $($(".dashboardEvent").attr("data-attrpadre")).attr("data-month");
        //var year = $($(".dashboardEvent").attr("data-attrpadre")).attr("data-year");
        var analista = $(this).attr("data-analista");
        var tipoProceso = $(this).attr("data-tipo");
        var estado = $(this).attr("data-estado");
        $("#interactionplataforma").hide();

        switch (tipoProceso) {
            case "contrato":
                ajaxViewPartialLoaderTransaccion(prefixDomain + prefix, "#loaderSearch");
                setTimeout(function () {
                    $("#procesosRenovacion").html('');
                    $("#procesosRenovacionPagination").html('');
                    $("#renderizadoheader_renovaciones").html('');
                    ajaxRenderizaProcesos(prefixDomain + prefix, "MS01", "U29saWNpdHVkQ29udHJhdG8=", "#renderizadoheader_contratos", "#procesosContratos", "#procesosContratosPagination", "renderizadoresult_contratos",
                        "", "", tipoDashboard, day, month, year, analista, tipoProceso, estado);
                }, 500);
                break;
            case "renovacion":
                ajaxViewPartialLoaderTransaccion(prefixDomain + prefix, "#loaderSearch");
                setTimeout(function () {
                    $("#procesosContratos").html('');
                    $("#procesosContratosPagination").html('');
                    $("#renderizadoheader_contratos").html('');
                    ajaxRenderizaProcesos(prefixDomain + prefix, "MS01", "U29saWNpdHVkUmVub3ZhY2lvbg==", "#renderizadoheader_renovaciones", "#procesosRenovacion", "#procesosRenovacionPagination", "renderizadoresult_renovaciones",
                        "", "", tipoDashboard, day, month, year, analista, tipoProceso, estado);
                }, 500);
                break;
        }
        
    });
    
});