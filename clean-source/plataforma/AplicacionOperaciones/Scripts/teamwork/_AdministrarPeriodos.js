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

$(document).ready(function () {


    //$(".eventAbrirPeriodo").on("click", function (e) {
    //    e.preventDefault();
    //    $("#nombreTituloA").text($(this).attr("data-nombreEmpresa"));
    //    $("#nombreEmpresa").val(btoa($(this).attr("data-nombreEmpresa")));
    //    $('#btnconfirm').hide(); //oculto mediante id
    //    $("#modalAgregarPerido").modal("show");
    //});

    $(document).on("click", ".eventCerarPeriodo", function (e) {
        e.preventDefault();
        $("#idacerrar").val(btoa($(this).attr("data-nombreEmpresa")));
        $("#periodovig").text($(this).attr("data-vigente"));
        $("#nombretitulo").text($(this).attr("data-nombreEmpresa"));
        $("#modalCerrarP").modal("show");
    });

    $(".eventDescartarAbrir").on("click", function (e) {
        $("#modalCerrarP").modal("hide");
    });

    $('#message').bind("DOMSubtreeModified", function () {
        $("#modalCerrarP").modal("hide");
    });
});

function modalAbrirPeriodo() {
    $("#modalAgregarPerido").modal("show");
}

function modalCerrarVentanaAbrir() {
    $("#modalAgregarPerido").modal("hide");
}

function modalCerrarPeriodo() {
    
    setTimeout(function () {
        $("#modalAgregarPerido").modal("hide");
     
    }, 500);
    setTimeout(function () {
        $("#modalAgregarPerido").modal("hide");
        $("#messageApplication").html('');
    }, 500);
}

function returnSection() {
    window.location.href = prefixDomain + prefix + "AdministrarPeriodos";
}

