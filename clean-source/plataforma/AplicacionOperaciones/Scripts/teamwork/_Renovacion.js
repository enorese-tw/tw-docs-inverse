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

    $(document).on("click", "#fileUpload", function (e) {
        e.preventDefault();
        $("#file").val(null);
        $("#messagePlataforma").html('');
        sessionStorage.setItem("ApplicationError", "N");
        $("#file").trigger("click");
    });

    $("#file").on("change", function () {
        $("#eventFileUpload").trigger("click");
    });

    $("#downloadUpload").on("click", function (e) {
        e.preventDefault();
        ajaxCreatePlantillaExcelDocManual(prefixDomain + prefix, $(this).attr("data-cifrado"));
    });

    $(document).on("click", "#closeErrorGeneric", function () {
        $("#errorGeneric").hide();
    });

});