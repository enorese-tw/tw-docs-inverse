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

    var nCliente = 1;
    var aGastos = new Array();

    $(document).on("click", ".btnCrearGastoFinanzas", function (e) {
        e.preventDefault();
        var fecha = new Date();
        var token = fecha.getFullYear().toString(10) + fecha.getMonth().toString(10) + fecha.getDay().toString(10) + fecha.getHours().toString(10) + fecha.getMinutes().toString(10) + fecha.getMilliseconds().toString(10);

        if ($("#dllCliente").val() !== null) {
            if ($("#ddlTipoDocumento").val() !== "-1" && $("#ddlTipoDocumento").val() !== null) {
                if ($("#txbRutProveedor").val() !== "" && $("#ddlProveedor").val() !== null) {
                    if ($("#ddlReembolso").val() !== "0" && $("#ddlReembolso").val() !== null) {
                        if ($("#monto").val() !== "0" && $("#monto").val() !== "") {
                            sessionStorage.setItem("CodigoGastosApplication", "");
                            ajaxCargaGastoFinanza(prefixDomain + prefix, $("#empresa").val(), $("#dllCliente").val(), $('#periodo').text(), $("#monto").val(),
                                $("#ddlReembolso").val(), $("#txtComentario").val(), $("#nFactura").val(), $("#ddlProveedor").val(), "unitaria", token,
                                $("#ddlTipoDocumento").val(), $("#" + $(".rdindividual:checked").attr("id")).val(), $("#monto").val(), 1, 1);

                            $("#btnCrearGastoFinanzas").prop("disabled", true);
                        }
                        else
                        {
                            swal({
                                title: 'Falta Completar Datos',
                                text: "Debe indicar un monto neto",
                                type: 'warning',
                                showCancelButton: false,
                                confirmButtonColor: '#3085d6',
                                cancelButtonColor: '#d33',
                                confirmButtonText: 'Entiendo!',
                                cancelButtonText: 'No, Cancelar',
                                closeOnConfirm: false,
                                closeOnCancel: false,
                                allowOutsideClick: false
                            }).then((result) => {
                            });
                        }
                    }
                    else {
                        swal({
                            title: 'Falta Completar Datos',
                            text: "Debe seleccionar el tipo de reembolso",
                            type: 'warning',
                            showCancelButton: false,
                            confirmButtonColor: '#3085d6',
                            cancelButtonColor: '#d33',
                            confirmButtonText: 'Entiendo!',
                            cancelButtonText: 'No, Cancelar',
                            closeOnConfirm: false,
                            closeOnCancel: false,
                            allowOutsideClick: false
                        }).then((result) => {
                        });
                    } 

                }
                else {
                    swal({
                        title: 'Falta Completar Datos',
                        text: "Debe seleccionar el proveedor para poder guardar el gasto.",
                        type: 'warning',
                        showCancelButton: false,
                        confirmButtonColor: '#3085d6',
                        cancelButtonColor: '#d33',
                        confirmButtonText: 'Entiendo!',
                        cancelButtonText: 'No, Cancelar',
                        closeOnConfirm: false,
                        closeOnCancel: false,
                        allowOutsideClick: false
                    }).then((result) => {
                    });
                }
            }
            else {
                swal({
                    title: 'Falta Completar Datos',
                    text: "Debe seleccionar el tipo de documento para poder guardar el gasto.",
                    type: 'warning',
                    showCancelButton: false,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Entiendo!',
                    cancelButtonText: 'No, Cancelar',
                    closeOnConfirm: false,
                    closeOnCancel: false,
                    allowOutsideClick: false
                }).then((result) => {
                });
            }
        } else {
            alert("Seleccione Cliente");
        }
    });

    $(document).on("click", ".btnCrearGastoFinanzasApertura", function (e) {
        e.preventDefault();

        var montoTotal = 0;
        var montoNeto = parseInt($("#monto").val());
        var fecha = new Date();
        var token = fecha.getFullYear().toString(10) + fecha.getMonth().toString(10) + fecha.getDay().toString(10) + fecha.getHours().toString(10) + fecha.getMinutes().toString(10) + fecha.getMilliseconds().toString(10);

        for (var j = 0; j < aGastos.length; j++) {
            montoTotal = montoTotal + parseInt(aGastos[j].monto, 10);
        }

        if ($("#dllCliente").val() !== null) {
            if ($("#ddlTipoDocumento").val() !== "-1" && $("#ddlTipoDocumento").val() !== null) {
                if ($("#txbRutProveedor").val() !== "" && $("#ddlProveedor").val() !== null) {
                    if (aGastos.length > 0) {
                        if (montoNeto === montoTotal) {
                            sessionStorage.setItem("CodigoGastosApplication", "");
                            for (var i = 0; i < aGastos.length; i++) {
                                ajaxCargaGastoFinanza(prefixDomain + prefix, $("#empresa").val(), aGastos[i].cliente, $('#periodo').text(), aGastos[i].monto,
                                    aGastos[i].tipoReembolso, $("#txtComentario").val(), $("#nFactura").val(), $("#ddlProveedor").val(), "apertura", token,
                                    $("#ddlTipoDocumento").val(), aGastos[i].afecto, $("#monto").val(), parseInt(i + 1), aGastos.length);
                            }
                        }
                        else {
                            swal({
                                title: 'Falta Completar Datos',
                                text: "La sumatoria de todos los clientes debe ser igual al monto neto que indico.",
                                type: 'warning',
                                showCancelButton: false,
                                confirmButtonColor: '#3085d6',
                                cancelButtonColor: '#d33',
                                confirmButtonText: 'Entiendo!',
                                cancelButtonText: 'No, Cancelar',
                                closeOnConfirm: false,
                                closeOnCancel: false,
                                allowOutsideClick: false
                            }).then((result) => {
                            });
                        }
                    }
                    else {
                        swal({
                            title: 'Falta Completar Datos',
                            text: "Debe indicar un monto neto",
                            type: 'warning',
                            showCancelButton: false,
                            confirmButtonColor: '#3085d6',
                            cancelButtonColor: '#d33',
                            confirmButtonText: 'Entiendo!',
                            cancelButtonText: 'No, Cancelar',
                            closeOnConfirm: false,
                            closeOnCancel: false,
                            allowOutsideClick: false
                        }).then((result) => {
                        });
                    }
                }
                else {
                    swal({
                        title: 'Falta Completar Datos',
                        text: "Debe seleccionar el proveedor para poder guardar el gasto.",
                        type: 'warning',
                        showCancelButton: false,
                        confirmButtonColor: '#3085d6',
                        cancelButtonColor: '#d33',
                        confirmButtonText: 'Entiendo!',
                        cancelButtonText: 'No, Cancelar',
                        closeOnConfirm: false,
                        closeOnCancel: false,
                        allowOutsideClick: false
                    }).then((result) => {
                    });
                }
            }
            else {
                swal({
                    title: 'Falta Completar Datos',
                    text: "Debe seleccionar el tipo de documento para poder guardar el gasto.",
                    type: 'warning',
                    showCancelButton: false,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Entiendo!',
                    cancelButtonText: 'No, Cancelar',
                    closeOnConfirm: false,
                    closeOnCancel: false,
                    allowOutsideClick: false
                }).then((result) => {
                });
            }
        }
        else {
            alert("Seleccione Cliente");
        }
    });

    var blur = 0;

    $("#nFactura").on("keydown", function () {
        var codigo = event.which || event.keyCode;

        if (codigo === 13) {
            $("#monto").focus();
        }
    });

    $("#nFactura").on("blur", function () {
        if ($(this).val() !== "") {
            if ($("#ddlTipoDocumento").val() !== null) {
                ajaxGetExisteDocumento(prefixDomain + prefix, $("#txbRutProveedor").val(), $(this).val(), $("#ddlTipoDocumento").val());
            } else {
                swal({
                    title: 'Falta Completar Datos',
                    text: "Debe seleccionar el tipo de documento..",
                    type: 'warning',
                    showCancelButton: false,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Entiendo!',
                    cancelButtonText: 'No, Cancelar',
                    closeOnConfirm: false,
                    closeOnCancel: false,
                    allowOutsideClick: false
                }).then((result) => {
                });
            }
        }

    });

    $("#txtCliente").on("blur", function () {
        if ($("#dllCliente").val() === "0") {
            alert("Seleccione Cliente");
        }
    });

    $(document).on("change", "#empresa", function (e) {
        e.preventDefault();
        ajaxObtenerPeriodoVigente(prefixDomain + prefix, $("#empresa").val());

        if ($("#empresa").val() === "TWO") {
            ajaxViewDdlClientesDistintos(prefixDomain + prefix);
        } else {
            ajaxViewDdlClientes(prefixDomain + prefix, $("#empresa").val());
        }
    });

    $(document).on("change", "#ddlProveedor", function (e) {
        e.preventDefault();
        $("#txbRutProveedor").attr("disabled", "disabled");
        $("select option:contains('Seleccione')").attr("disabled", "disabled");
        ajaxGetProveedorRutTXT(prefixDomain + prefix, $("#ddlProveedor").val());
    });

    $(document).on("change", "#ddlProveedor", function (e) {
        e.preventDefault();
        $("#txbRutProveedor").attr("disabled", "disabled");
        $("select option:contains('Seleccione')").attr("disabled", "disabled");
        ajaxGetProveedorRutTXT(prefixDomain + prefix, $("#ddlProveedor").val());
    });

    $(document).on("change", "#dllCliente", function (e) {
        $("#txtCliente").attr("disabled", "disabled");
        $("select option:contains('Seleccione')").attr("disabled", "disabled");
        document.getElementById("txtCliente").value = $("#dllCliente").val();

    });

    $(document).on("blur", ".dll-tipoReembolso", function (e) {
        $("#txtCliente").prop("disabled", true);

        var nCliente = $(this).attr("data-id");

        $("select option:contains('Seleccione')").attr("disabled", "disabled");

        $("#agregar_" + nCliente).removeClass("btn-secondary");
        $("#agregar_" + nCliente).addClass("btn-success");

        $("#siguiente_" + nCliente).addClass("btn-secondary");
        $("#siguiente_" + nCliente).addClass("btn-info");

        $("#afecto_" + nCliente).removeAttr("disabled").focus();

    });

    $(document).on("click", "#exenta", function (e) {

        $("#montoIVA").hide();
        $("#montoTotal").val(Math.round($("#monto").val()));

    });

    $(document).on("click", "#afecta", function (e) {
        $("#montoIVA").show();

        $("#montoIVA").val(Math.round($("#monto").val() * 0.19) + " IVA");
        $("#montoTotal").val(Math.round($("#monto").val() * 1.19) + " Total");
    });

    $(document).on("click", "#unica", function () {
        var iva = 0;
        var total = 0;

        if ($("input[name='tributaria']:checked").val() === "1") {
            if ($("#monto").val().length > 0) {
                iva = parseInt($("#montoIVA").val());
            }
        }

        if ($("#monto").val().length > 0) {
            total = parseInt($("#monto").val());
        }

        $("#montoTotal").val(Math.round(total + iva) + " Total");
    });

    $(document).on("click", "#apertura", function () {
        var total = 0;

        if ($("#monto").val().length > 0) {
            total = parseInt($("#monto").val());
        }

        $("#montoTotal").val(Math.round(total + 0) + " Total");
    });

    $(document).on("keyup", "#monto", function () {
        var constCalculo = 1;
        
        if ($(this).val().length > 0) {
            if ($("input[name='cliente']:checked").val() === "1") {
                if ($("input[name='tributaria']:checked").val() === "1") {
                    constCalculo = 0.19;
                    $("#montoIVA").val(Math.round($("#monto").val() * constCalculo) + " IVA");
                }
                else {
                    $("#montoIVA").val("0 IVA");
                }

                $("#montoTotal").val(Math.round(parseInt($("#monto").val()) + parseInt($("#montoIVA").val())) + " Total");
            }
            else
            {
                $("#montoIVA").val("0 IVA");
                $("#montoTotal").val(Math.round(parseInt($("#monto").val()) + parseInt($("#montoIVA").val())) + " Total");
            }
        }
        else {
            $("#montoIVA").val("0 IVA");
            $("#montoTotal").val("0 Total");
        }
        
            

        $("#txtCliente_1").removeAttr("disabled");
    });

    $(document).on("change", "#monto", function () {
        var constCalculo = 1;

        if ($(this).val().length > 0) {
            if ($("input[name='tributaria']:checked").val() === "1") {
                constCalculo = 0.19;
                $("#montoIVA").val(Math.round($("#monto").val() * constCalculo) + " IVA");
            }
            else {
                $("#montoIVA").val("0 IVA");
            }

            $("#montoTotal").val(Math.round(parseInt($("#monto").val()) + parseInt($("#montoIVA").val())) + " Total");
        }
        else {
            $("#montoIVA").val("0 IVA");
            $("#montoTotal").val("0 Total");
        }

        $("#txtCliente_1").removeAttr("disabled");
    });

    $(document).on("keydown", ".txb-montoNeto", function (e) {
        var nCliente = $(this).attr("data-id");

        if (e.keyCode === 13 || e.keyCode === 9) {
            $("#ddlReembolso_" + nCliente).removeAttr("disabled");
        }
    });

    $(document).on("keydown", ".txt-clienteApertura", function (e) {
        if (e.keyCode === 13 || e.keyCode === 9) {
            var nCliente = $(this).attr("data-id");
            if ($(this).val().toUpperCase() === 'ROL') {
                $("#txtMonto_" + nCliente).removeAttr("disabled");
            }
            else {
                ajaxObtenerClienteByNombreApertura(prefixDomain + prefix, $(this).val(), $("#empresa").val(), "txtMonto_" + nCliente);
            }
        }
    });

    $(document).on("keydown", "#txbRutProveedor", function (e) {

        if (e.keyCode === 13 || e.keyCode === 9) {
            $("#ddlProveedor").prop("disabled", true);

            ajaxGetProveedorRut(prefixDomain + prefix, $("#txbRutProveedor").val());
        }
    });

    $(document).on("keydown", "#txtCliente", function (e) {
        if (e.keyCode === 13 || e.keyCode === 9) {
            $("#dllCliente").prop("disabled", true);
            if ($("#empresa").val() === "TWO") {
                ajaxObtenerClienteByNombre(prefixDomain + prefix, $("#txtCliente").val(), $("#empresa").val());
            } else {
                ajaxObtenerClienteByNombre(prefixDomain + prefix, $("#txtCliente").val(), $("#empresa").val());
            }
        }
    });

    $(document).on("click", "#limpiarProveedor", function (e) {
        e.preventDefault();
        $("#ddlProveedor").removeAttr("disabled");
        $("#txbRutProveedor").removeAttr("disabled");
        $("#txbRutProveedor").val("");

        ajaxViewDDLProveedores(prefixDomain + prefix);
    });

    $(document).on("click", "#limpiarCliente", function (e) {
        e.preventDefault();

        if ($("#empresa").val() === "TWO") {
            ajaxViewDdlClientesDistintos(prefixDomain + prefix);
            $('#dllCliente').removeAttr('disabled');
            $('#txtCliente').removeAttr('disabled');
            $("#txtCliente").val("");
        } else if ($("#empresa").val() === "Seleccione") {
            alert("Seleccione una empresa para limpiar este gasto");
            $("#txtCliente").val("");
        } else {
            ajaxViewDdlClientes(prefixDomain + prefix, $("#empresa").val());
            $('#dllCliente').removeAttr('disabled');
            $('#txtCliente').removeAttr('disabled');
            $("#txtCliente").val("");
        }
         
    });
    
    /** eventos dinamicos */

    $(document).on("click", ".agregar-btn", function (e) {
        e.preventDefault();
        var nCliente = $(this).attr("data-Id");
        var sumatoriaMontos = 0;
        var unir = false;
        var positionUnir = 0;
        var nClienteLast = 0;
        
        for (var i = 0; i < aGastos.length; i++) {
            sumatoriaMontos = sumatoriaMontos + aGastos[i].monto;
        }
        
        if ($("#txtMonto_" + nCliente).val() !== "0" && $("#txtMonto_" + nCliente).val() !== "") {
            if (parseInt($("#txtMonto_" + nCliente).val()) <= parseInt($("#monto").val())) {
                if ((sumatoriaMontos + parseInt($("#txtMonto_" + nCliente).val())) <= parseInt($("#monto").val())) {
                    if ($("ddlReembolso_" + nCliente).val() !== null && $("ddlReembolso_" + nCliente).val() !== "0") {

                        for (var j = 0; j < aGastos.length; j++) {
                            if (aGastos[j].cliente.toUpperCase() === $("#txtCliente_" + nCliente).val().toUpperCase() && aGastos[j].tipoReembolso === $("#ddlReembolso_" + nCliente).val() &&
                                aGastos[j].afecto === $("#afecto_" + nCliente).val()) {
                                positionUnir = j;
                                unir = true;
                            }
                        }

                        if (!unir) {
                            aGastos.push({
                                cliente: $("#txtCliente_" + nCliente).val(),
                                monto: parseInt($("#txtMonto_" + nCliente).val()),
                                tipoReembolso: $("#ddlReembolso_" + nCliente).val(),
                                afecto: $("#afecto_" + nCliente).val()
                            });

                            $(this).hide();
                            $("#ddlReembolso_" + nCliente).attr("disabled", "disabled");
                            $("#txtCliente_" + nCliente).attr("disabled", "disabled");
                            $("#txtMonto_" + nCliente).attr("disabled", "disabled");
                            $("#afecto_" + nCliente).attr("disabled", "disabled");
                            $("#quitar_" + nCliente).removeClass("btn-secondary").addClass("btn-danger");
                            console.log(aGastos);
                        }
                        else
                        {
                            aGastos[positionUnir].monto = aGastos[positionUnir].monto + parseInt($("#txtMonto_" + nCliente).val());
                            $("#divApertura_" + nCliente).remove();
                            console.log(aGastos);
                            $("#txtMonto_" + (positionUnir + 1)).val(String(aGastos[positionUnir].monto));
                            nClienteLast = $(".clientesContent").length;
                            $("#divApertura_" + nClienteLast + " .lastOptions").prepend('<a href="#" data-id="' + nClienteLast + '" id="siguiente_' + nClienteLast + '" class="btn btn-circle-micro btn-secondary glyphicon-tw-agregar ml-5 siguiente-btn btn-info"></a>');
                        }
                    }
                    else {
                        swal({
                            title: 'Validación de Datos',
                            text: "El tipo de reembolso que esta indicando en el registro de cliente N° " + nCliente + ", debe ser diferente de seleccione.",
                            type: 'error',
                            showCancelButton: false,
                            confirmButtonColor: '#3085d6',
                            cancelButtonColor: '#d33',
                            confirmButtonText: 'Entiendo!',
                            cancelButtonText: 'No, Cancelar',
                            closeOnConfirm: false,
                            closeOnCancel: false,
                            allowOutsideClick: false
                        }).then((result) => {
                        });
                    }
                }
                else {
                    swal({
                        title: 'Validación de Datos',
                        text: "El monto que esta indicando en el registro de cliente N° " + nCliente + ", hace que la sumatoria de montos supere el monto neto.",
                        type: 'error',
                        showCancelButton: false,
                        confirmButtonColor: '#3085d6',
                        cancelButtonColor: '#d33',
                        confirmButtonText: 'Entiendo!',
                        cancelButtonText: 'No, Cancelar',
                        closeOnConfirm: false,
                        closeOnCancel: false,
                        allowOutsideClick: false
                    }).then((result) => {
                    });
                }
            }
            else {
                swal({
                    title: 'Validación de Datos',
                    text: "El monto que esta indicando en el registro de cliente N° " + nCliente + ", no puede superar al monto neto.",
                    type: 'error',
                    showCancelButton: false,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Entiendo!',
                    cancelButtonText: 'No, Cancelar',
                    closeOnConfirm: false,
                    closeOnCancel: false,
                    allowOutsideClick: false
                }).then((result) => {
                });
            }
        }
        else {
            swal({
                title: 'Validación de Datos',
                text: "El monto que esta indicando en el registro de cliente N° " + nCliente + ", debe ser mayor a 0 y diferente de vacio.",
                type: 'error',
                showCancelButton: false,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Entiendo!',
                cancelButtonText: 'No, Cancelar',
                closeOnConfirm: false,
                closeOnCancel: false,
                allowOutsideClick: false
            }).then((result) => {
            });
        }

    });

    $(document).on("click", ".siguiente-btn", function (e) {
        e.preventDefault();
        var nCliente = $(this).attr("data-Id");

        if ($("#agregar_" + nCliente).is(":visible")) {
            $("#agregar_" + nCliente).trigger("click");
        }

        if (!$("#agregar_" + nCliente).is(":visible")) {

            $("#siguiente_" + nCliente).hide();
            var nClienteSgte = $(".clientesContent").length + 1;

            $("#divCliente").append(
                '<div class="row ml-37 mr-37 mt-20 clientesContent" id="divApertura_' + nClienteSgte + '"> ' +
                '<div class="col-2"> <p class="textCliente">Cliente #' + nClienteSgte + '</p></div>' +
                '<div class="col-2"> ' +
                '<input type="text" data-cliente="cliente" data-id="' + nClienteSgte + '" class="form-control txt-clienteApertura" placeholder="Cliente" id="txtCliente_' + nClienteSgte + '" required="required" > ' +
                '</div>' +
                '<div class="col-2" id="txBMontoNeto"> ' +
                '<input type="text" class="form-control txb-montoNeto" data-id="' + nClienteSgte + '" placeholder="Monto Neto" id="txtMonto_' + nClienteSgte + '" required="required" disabled="disabled"> ' +
                '</div>' +
                '<div class="col-2" id="txBMontoNeto"> ' +
                '<select class="form-control dll-tipoReembolso" data-id="' + nClienteSgte + '" id="ddlReembolso_' + nClienteSgte + '" required="required" disabled> ' +
                '<option value="0">Seleccione</option> ' +
                '</select>' +
                '</div> ' +
                '<div class="col-2">' +
                '<select class="form-control" data-id="' + nClienteSgte + '" id="afecto_' + nClienteSgte + '" disabled>' +
                '<option value="1">Afecto</option>' +
                '<option value="2">Exento</option>' +
                '</select>' +
                '</div> ' +
                '<div class="col-2 lastOptions" style="position: relative; right: 30px;"> ' +
                '<a href="#" data-id="' + nClienteSgte + '" id="agregar_' + nClienteSgte + '" class="btn btn-circle-micro btn-secondary ml-5 glyphicon-tw-check-2 agregar-btn"/> ' +
                '<a href="#" data-id="' + nClienteSgte + '" id="siguiente_' + nClienteSgte + '" class="btn btn-circle-micro btn-secondary ml-5 glyphicon-tw-agregar siguiente-btn"/> ' +
                '<a href="#" data-id="' + nClienteSgte + '" id="quitar_' + nClienteSgte + '" class="btn btn-circle-micro btn-secondary ml-5 glyphicon-tw-delete quitar-btn"/> ' +
                '</div>' +
                '</div>');
            
        }
        

        ajaxViewDdlTipoReembolso(prefixDomain + prefix, "ddlReembolso_" + nClienteSgte);

    });

    $(document).on("click", ".quitar-btn", function (e) {
        e.preventDefault();
        var nCliente = $(this).attr("data-id");
        var nClienteFirst = 1;

        if ($(".clientesContent").length > 1) {
            if (parseInt(nCliente) === $(".clientesContent").length) {
                $("#divApertura_" + (nCliente - 1) + " .lastOptions").prepend('<a href="#" data-id="' + (nCliente - 1) + '" id="siguiente_' + (nCliente - 1) + '" class="btn btn-circle-micro btn-secondary glyphicon-tw-agregar ml-5 siguiente-btn btn-info"></a>');
            }

            $("#divApertura_" + nCliente).remove();
        }
        else
        {
            $("#divCliente").html(
                '<div class="row ml-37 mr-37 mt-20 clientesContent" id="divApertura_' + nClienteFirst + '"> ' +
                '<div class="col-2"> <p>Cliente #' + nClienteFirst + '</p></div>' +
                '<div class="col-2"> ' +
                '<input type="text" data-cliente="cliente" data-id="' + nClienteFirst + '" class="form-control txt-clienteApertura" placeholder="Cliente" id="txtCliente_' + nClienteFirst + '" required="required" > ' +
                '</div>' +
                '<div class="col-2" id="txBMontoNeto"> ' +
                '<input type="text" class="form-control txb-montoNeto" data-id="' + nClienteFirst + '" placeholder="Monto Neto" id="txtMonto_' + nClienteFirst + '" required="required" disabled="disabled"> ' +
                '</div>' +
                '<div class="col-2" id="txBMontoNeto"> ' +
                '<select class="form-control dll-tipoReembolso" data-id="' + nClienteFirst + '" id="ddlReembolso_' + nClienteFirst + '" required="required" disabled> ' +
                '<option value="0">Seleccione</option> ' +
                '</select>' +
                '</div> ' +
                '<div class="col-2">' +
                '<select class="form-control" data-id="' + nClienteFirst + '" id="afecto_' + nClienteFirst + '" disabled>' +
                '<option value="1">Afecto</option>' +
                '<option value="2">Exento</option>' +
                '</select>' +
                '</div> ' +
                '<div class="col-2 lastOptions" style="position: relative; right: 30px;"> ' +
                '<a href="#" data-id="' + nClienteFirst + '" id="agregar_' + nClienteFirst + '" class="btn btn-circle-micro btn-secondary ml-5 glyphicon-tw-check-2 agregar-btn"/> ' +
                '<a href="#" data-id="' + nClienteFirst + '" id="siguiente_' + nClienteFirst + '" class="btn btn-circle-micro btn-secondary glyphicon-tw-agregar siguiente-btn"/> ' +
                '<a href="#" data-id="' + nClienteFirst + '" id="quitar_' + nClienteFirst + '" class="btn btn-circle-micro btn-secondary ml-5 glyphicon-tw-delete quitar-btn"/> ' +
                '</div>' +
                '</div>');
            ajaxViewDdlTipoReembolso(prefixDomain + prefix, "ddlReembolso_" + nClienteFirst);
        }
        
        aGastos.splice(nCliente - 1, 1);

        for (var i = nCliente; i < $(".clientesContent").length + 1; i++) {
            $("#divApertura_" + (parseInt(i) + 1) + " .textCliente").html("Cliente #" + parseInt(i));
            $("#divApertura_" + (parseInt(i) + 1)).attr("id", "divApertura_" + parseInt(i));
            $("#txtCliente_" + (parseInt(i) + 1)).attr("id", "txtCliente_" + parseInt(i)).attr("data-id", parseInt(i));
            $("#txtMonto_" + (parseInt(i) + 1)).attr("id", "txtMonto_" + parseInt(i)).attr("data-id", parseInt(i));
            $("#ddlReembolso_" + (parseInt(i) + 1)).attr("id", "ddlReembolso_" + parseInt(i)).attr("data-id", parseInt(i));
            $("#afecto_" + (parseInt(i) + 1)).attr("id", "afecto_" + parseInt(i)).attr("data-id", parseInt(i));


            $("#agregar_" + (parseInt(i) + 1)).attr("id", "agregar_" + parseInt(i)).attr("data-id", parseInt(i));
            $("#siguiente_" + (parseInt(i) + 1)).attr("id", "siguiente_" + parseInt(i)).attr("data-id", parseInt(i));
            $("#quitar_" + (parseInt(i) + 1)).attr("id", "quitar_" + parseInt(i)).attr("data-id", parseInt(i));
        }

    });

    /** fin eventos dinamicos */

    $(document).on("click", ".rdpr", function (e) {

        switch ($(this).val()) {
            case '1':
                $("").html('');
                $("#monto").removeAttr("disabled");
                $("#empresa").removeAttr("disabled");
                $("#radiotrubutaria").show();

                if ($("input[name='tributaria']:checked").val() === "1") {
                    $("#montoIVA").show();
                }

                $(".div-tipoReembolso").html(
                    '<div class="col-3"> ' +
                    '<p>Tipo de Reembolso</p> ' +
                    '</div>' +
                    '<div class="col-6 ">' +
                    '<select class="form-control" id="ddlReembolso" required="required">' +
                    '</select>' +
                    '</div>');
                $("#divCliente").html(
                    '<div class="row ml-37 mr-37 mt-20 ">' +
                    '<div class="col-3">' +
                    '<p>Cliente</p>' +
                    '</div>' +
                    '<div class="col-2">' +
                    '<input type="text" class="form-control" placeholder="Cliente" id="txtCliente" required="required">' +
                    '</div>' +
                    '<div class="col-4">' +
                    '<select class="form-control" id="dllCliente" required="required" ></select>' +
                    '</div>' +
                    '<div class="col-1" style="align-items: center; display: flex; vertical-align: middle; position: relative; right: 25px;">' +
                    '<a href="#" id="limpiarCliente" class="btn btn-circle-micro btn-danger glyphicon-tw-delete">' +
                    '<span class="glyphicon glyphicon-tw-delete"></span> ' +
                    '</a>' +
                    '</div>' +
                    '</div>');
                $(".btn-add-gasto").html(
                    '<a href="#" class="btn btn-danger btnCrearGastoFinanzas" style="border-radius: 25px; height: 40px; text-align: left; color: #fff;" data-toggle="collapse" role="button" aria-expanded="false" aria-controls="contentDashboardXContrato">' +
                    '<i class="glyphicon-tw-7 glyphicon-tw-dinero" style="display: inline-block;"></i>' +
                    '<span style="display: inline-block; margin-top: -7px;"> <i style="font-family: \'Open Sans Condensed\', sans-serif; font-size: 14px; font-style: normal; display: block; margin-bottom: -8px;">&nbsp;&nbsp;Agregar&nbsp;&nbsp;</i>' +
                    '<i style="font-family: \'Open Sans Condensed\', sans-serif; font-size: 14px; font-style: normal; display: block;">&nbsp;&nbsp;Gasto&nbsp;&nbsp;</i>' +
                    '</span> </a>');

                ajaxViewDdlTipoReembolso(prefixDomain + prefix, "ddlReembolso");

                if ($("#empresa").val() === "TWO") {
                    ajaxViewDdlClientesDistintos(prefixDomain + prefix);
                } else {
                    ajaxViewDdlClientes(prefixDomain + prefix, $("#empresa").val());
                }
                
                break;

            case '2':
                nCliente = 1;
                $(".div-tipoReembolso").html("");
                $("#monto").removeAttr("disabled", "disabled");
                $("#empresa").attr("disabled", "disabled");
                $("#radiotrubutaria").hide();
                $("#montoIVA").hide();
                
                if ($("#monto").val().length > 0) {
                    $("#divCliente").html(
                        '<div class="row ml-37 mr-37 mt-20 clientesContent" id="divApertura_' + nCliente + '">' +
                        '<div class="col-2"> <p class="textCliente">Cliente #' + nCliente + '</p>' +
                        '</div><div class="col-2">' +
                        '<input type="text" data-cliente="cliente" data-id="' + nCliente + '" class="form-control txt-clienteApertura" placeholder="Cliente" id="txtCliente_' + nCliente + '" required="required">' +
                        '</div>' +
                        '<div class="col-2" id="txBMontoNeto">' +
                        '<input type="text" class="form-control txb-montoNeto" data-id="' + nCliente + '"  placeholder="Monto Neto" id="txtMonto_' + nCliente + '" required="required" disabled="disabled">' +
                        '</div>' +
                        '<div class="col-2" id="txBMontoNeto">' +
                        '<select class="form-control dll-tipoReembolso" data-id="' + nCliente + '"  id="ddlReembolso_' + nCliente + '" required="required" disabled>' +
                        '<option value="0">Seleccione</option>' +
                        '</select>' +
                        '</div>' +
                        '<div class="col-2">' +
                        '<select class="form-control" data-id="' + nCliente + '"  id="afecto_' + nCliente + '">' +
                        '<option value="1">Afecto</option>' +
                        '<option value="2">Exento</option>' +
                        '</select>' +
                        '</div>' +
                        '<div class="col-2 lastOptions" style="align-items: center; vertical-align: middle; position: relative; right: 25px;">' +
                        '<a href="#" data-id="' + nCliente + '" id="agregar_' + nCliente + '" class="btn btn-circle-micro btn-secondary ml-5 glyphicon-tw-check-2 agregar-btn"/>' +
                        '<a href="#" data-id="' + nCliente + '" id="siguiente_' + nCliente + '" class="btn btn-circle-micro btn-secondary ml-5 glyphicon-tw-agregar siguiente-btn"/>' +
                        '<a href="#" data-id="' + nCliente + '" id="quitar_' + nCliente + '" class="btn btn-circle-micro btn-secondary ml-5 glyphicon-tw-delete quitar-btn"/>' +
                        '</div>' +
                        '</div>');
                } else {
                    $("#divCliente").html(
                        '<div class="row ml-37 mr-37 mt-20 clientesContent" id="divApertura_' + nCliente + '">' +
                        '<div class="col-2"> <p class="textCliente">Cliente #' + nCliente + '</p>' +
                        '</div>' +
                        '<div class="col-2">' +
                        '<input type="text" data-cliente="cliente" data-id="' + nCliente + '" class="form-control txt-clienteApertura" placeholder="Cliente" id="txtCliente_' + nCliente + '" required="required" >' +
                        '</div>' +
                        '<div class="col-2" id="txBMontoNeto">' +
                        '<input type="text" class="form-control txb-montoNeto" placeholder="Monto Neto" data-id="' + nCliente + '" id="txtMonto_' + nCliente + '" required="required" disabled="disabled">' +
                        '</div><div class="col-2" id="txBMontoNeto">' +
                        '<select class="form-control dll-tipoReembolso" data-id="' + nCliente + '"  id="ddlReembolso_' + nCliente + '" required="required" disabled>' +
                        '<option value="0">Seleccione</option>' +
                        '</select></div> <div class="col-2">' +
                        '<select class="form-control" data-id="' + nCliente + '" id="afecto_' + nCliente + '" disabled>' +
                        '<option value="1">Afecto</option>' +
                        '<option value="2">Exento</option>' +
                        '</select>' +
                        '</div>' +
                        '<div class="col-2 lastOptions" style="align-items: center; vertical-align: middle; position: relative; right: 25px;">' +
                        '<a href="#" data-id="' + nCliente + '" id="agregar_' + nCliente + '" class="btn btn-circle-micro btn-secondary ml-5 glyphicon-tw-check-2 agregar-btn"/>' +
                        '<a href="#" data-id="' + nCliente + '" id="siguiente_' + nCliente + '" class="btn btn-circle-micro btn-secondary ml-5 glyphicon-tw-agregar siguiente-btn"/>' +
                        '<a href="#" data-id="' + nCliente + '" id="quitar_' + nCliente + '" class="btn btn-circle-micro btn-secondary ml-5 glyphicon-tw-delete quitar-btn"/>' +
                        '</div>' +
                        '</div>');
                }

                $(".btn-add-gasto").html(
                    '<a href="#" class="btn btn-danger btnCrearGastoFinanzasApertura" style="border-radius: 25px; height: 40px; text-align: left; color: #fff;" data-toggle="collapse" role="button" aria-expanded="false" aria-controls="contentDashboardXContrato">' +
                    '<i class="glyphicon-tw-7 glyphicon-tw-dinero" style="display: inline-block;"></i> ' +
                    '<span style="display: inline-block; margin-top: -7px;"> <i style="font-family: \'Open Sans Condensed\', sans-serif; font-size: 14px; font-style: normal; display: block; margin-bottom: -8px;">&nbsp;&nbsp;Agregar&nbsp;&nbsp;</i>' +
                    '<i style="font-family: \'Open Sans Condensed \', sans-serif; font-size: 14px; font-style: normal; display: block;">&nbsp;&nbsp;Gasto&nbsp;&nbsp;</i></span></a>');
               
                ajaxViewDdlTipoReembolso(prefixDomain + prefix, "ddlReembolso_" + nCliente);

                break;

        }

    });

});