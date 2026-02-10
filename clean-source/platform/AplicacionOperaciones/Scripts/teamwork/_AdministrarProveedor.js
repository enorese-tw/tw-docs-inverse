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

    $(".eventAgregarProvee").on("click", function (e) {
        e.preventDefault();
        $("#modalAgregarProveedor").modal("show");
    });

    $("#AbrirNuevaActualizar").on("click", function (e) {
        e.preventDefault();
        $("#modalActualizarProvee").modal("show");
    });

    $(".eventActualizar").on("click", function (e) {
        e.preventDefault();
        $("#nombremodal").val($("#newNombreModal").val());
        $("#rutModal").val($("#nuevoRut").val());
        $("#modalActualizarProvee").modal("show");
    });

   

    $(".eventDescartarActualizacion").on("click", function (e) {
        $("#modalActualizarProvee").modal("hide");
    });

    function eventoActualizacionTerminado() {
        $("#modalActualizarProvee").modal("hide");
    }


    $(".eventEliminar").on("click", function (e) {
        e.preventDefault();
        $("#rutModal").val($(this).attr("data-rut"));
        $("#modalEliminarProvee").modal("show");
    });

    $(".eventDescartarEliminacion").on("click", function (e) {
        $("#modalEliminarProvee").modal("hide");
    });

    $(".eventEliminarProveedor").on("click", function (e) {
        e.preventDefault();
        $("#rutModalEliminar").val(btoa($(this).attr("data-rut")));
        $("#nombreModalToEliminar").text($(this).attr("data-nombre"));
        $("#modalEliminarProvee").modal("show");
    });

    $(".eventNuevoActProveedor").on("click", function (e) {
        e.preventDefault();
        $("#rutModal").val(btoa($(this).attr("data-rut")));
        $("#nombreProvedor").text($(this).attr("data-nombre"));
        $("#modalActualizarProvee").modal("show");
    });

});

function returnSection() {
    window.location.href = prefixDomain + prefix + "AdministrarProveedor";
}

