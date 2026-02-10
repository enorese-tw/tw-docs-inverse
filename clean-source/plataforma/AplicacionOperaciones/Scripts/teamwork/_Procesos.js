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

    if (sessionStorage.getItem("ApplicationNombreProceso") !== "" && sessionStorage.getItem("ApplicationMessageProceso") !== "" &&
        sessionStorage.getItem("ApplicationNombreProceso") !== undefined && sessionStorage.getItem("ApplicationMessageProceso") !== undefined &&
        sessionStorage.getItem("ApplicationNombreProceso") !== null && sessionStorage.getItem("ApplicationMessageProceso") !== null) {
        ajaxViewPartialLoaderComplete(prefixDomain + prefix, "#transaccionSuccess", sessionStorage.getItem("ApplicationMessageProceso"), sessionStorage.getItem("ApplicationNombreProceso"));
    }

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

});