
function ajaxCargaGastoFinanza(controller, empresa, cliente, periodo, monto, reembolso, comentario, nFactura, proveedor, etiqueta, token,
    tipoDocumento, afecto = "", montoNetoDocumento, round, totalInsert) {
    $.ajax({
        type: 'POST',
        url: controller + 'CargarGastoFinanzas',
        data: '{ empresa: "' + empresa + '",  cliente: "' + cliente + '",  periodo: "' + periodo + '",  monto: "' + monto + '",  reembolso: "' +
              reembolso + '",  comentario: "' + comentario + '",  nFactura: "' + nFactura + '",  proveedor: "' + proveedor + '",  etiqueta: "' +
            etiqueta + '",  token: "' + token + '",  tipoDocumento: "' + tipoDocumento + '", afecto: "' + afecto + '", montoNetoDocumento: "' + montoNetoDocumento + '" }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code !== "600") {
                if (response.Code === "200") {
                    
                    sessionStorage.setItem("CodigoGastosApplication", sessionStorage.getItem("CodigoGastosApplication") + "<br/>" + response.Codigo);

                    if (round === totalInsert) {
                        swal({
                            title: 'Gasto Correcto',
                            html: "<h4>Su codigo es el siguiente:</h4><h2><strong>" + sessionStorage.getItem("CodigoGastosApplication") + "</strong></h2>",
                            type: 'success',
                            showCancelButton: false,
                            confirmButtonColor: '#3085d6',
                            cancelButtonColor: '#d33',
                            confirmButtonText: 'Grandioso!',
                            cancelButtonText: 'No, Cancelar',
                            closeOnConfirm: false,
                            closeOnCancel: false,
                            allowOutsideClick: false
                        }).then((result) => {
                            sessionStorage.setItem("CodigoGastosApplication", "");
                            window.location.reload();
                        });
                        
                    }
                    
                }
                else if (response.Code === "206") {
                    alert("Factura duplicada N°" + nFactura + " para en el proveedor  " + proveedor );
                } else {
                    alert("Error en la aplicacion, favor comunicar con TI.");
                }
            }
            else {
                $("#modelExpiracion").modal("show");
                sessionStorage.clear();
            }
        },
        error: function (xhr) {
            alert("error de ajax");
        },
        complete: function () {
        }
    });
}

function ajaxViewDdlClientes(controller) {
    $.ajax({
        type: 'POST',
        url: controller + 'ViewDDLClientes',
        //data: '{ empresa: "' + empresa + '" }',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code !== "600") {
                if (response.Code === "200") {
                    $("#dllCliente").html('<option value="0">Seleccione</option>');   
                    for (var i = 0; i < response.Clientes.length; i++) {
                        $("#dllCliente").append('<option value="' + response.Clientes[i].Codigo + '">' + response.Clientes[i].Descripcion_Cliente + '</option>');
                    }

                    
                }
            }
            else {
                $("#modelExpiracion").modal("show");
                sessionStorage.clear();
            }
        },
        error: function (xhr) {
            alert("error de ajax");
        },
        complete: function () {
        }
    });
}

function ajaxViewDdlClientesDistintos(controller) {
    $.ajax({
        type: 'POST',
        url: controller + 'ViewDDLClientesDistintos',
        
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code !== "600") {
                if (response.Code === "200") {
                    $("#dllCliente").html('<option value="0">Seleccione</option>');
                    for (var i = 0; i < response.Clientes.length; i++) {
                        $("#dllCliente").append('<option value="' + response.Clientes[i].Codigo + '">' + response.Clientes[i].Descripcion_Cliente+ '</option>');
                    }


                }
            }
            else {
                $("#modelExpiracion").modal("show");
                sessionStorage.clear();
            }
        },
        error: function (xhr) {
            alert("error de ajax");
        },
        complete: function () {
        }
    });
}

function ajaxViewDdlClientesDistintosNombre(controller, cliente) {
    $.ajax({
        type: 'POST',
        url: controller + 'ViewDDLClientesDistintosNombre',
        data: '{ cliente: "' + cliente + '" }',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code !== "600") {
                if (response.Code === "200") {
                    for (var i = 0; i < response.Clientes.length; i++) {
                        $("#dllCliente").html('<option value="' + response.Clientes[i].Codigo + '">'  + response.Clientes[i].Descripcion_Cliente + '</option>');
                    }
                }
            }
            else {
                $("#modelExpiracion").modal("show");
                sessionStorage.clear();
            }
        },
        error: function (xhr) {
            alert("error de ajax");
        },
        complete: function () {
        }
    });
}

function ajaxViewDDLProveedores(controller) {
    $.ajax({
        type: 'POST',
        url: controller + 'ViewDDLProveedores',
        data: '{}',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code !== "600") {
                if (response.Code === "200") {
                    $("#proveedorDiv").html('<select id="ddlProveedor" class="form-control">');   
                    $("#proveedorDiv").append('</select>');  
                    $("#ddlProveedor").append('<option value="0">Seleccione</option>');
                    for (var i = 0; i < response.Proveedores.length; i++) {
                        $("#ddlProveedor").append('<option value ="' + response.Proveedores[i].Rut + '" >' + response.Proveedores[i].Nombre + '</option>');
                    }
                }
            }
            else {
                $("#modelExpiracion").modal("show");
                sessionStorage.clear();
            }
        },
        error: function (xhr) {
            alert("error de ajax");
        },
        complete: function () {
        }
    });
}

function ajaxViewDdlTipoReembolso(controller, ddlTipoReembolso) {
    $.ajax({
        type: 'POST',
        url: controller + 'ObtenerTipoReembolsoDDL',
        data: '{ empresa: "' + empresa + '" }',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code !== "600") {
                if (response.Code === "200") {
                    $("#" + ddlTipoReembolso).html('<option value="0">Seleccione</option>');
                    for (var i = 0; i < response.TipoReembolso.length; i++) {
                        $("#" + ddlTipoReembolso).append('<option value="' + response.TipoReembolso[i].Id + '">' + response.TipoReembolso[i].Codigo + ' ' + response.TipoReembolso[i].Nombre + '</option>');
                    }
                }
            }
            else {
                $("#modelExpiracion").modal("show");
                sessionStorage.clear();
            }
        },
        error: function (xhr) {
            alert("error de ajax");
        },
        complete: function () {
        }
    });
}

function ajaxGetProveedorRut(controller, rutproveedor) {
    $.ajax({
        type: 'POST',
        url: controller + 'ObtenerProveedorRut',
        data: '{ rutproveedor: "' + rutproveedor + '"}',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code !== "600") {
                if (response.Code === "200") {
                    for (var i = 0; i < response.Proveedores.length; i++) {
                        $("#ddlProveedor").html('<option value ="' + response.Proveedores[i].Rut + '" >' + response.Proveedores[i].Nombre + '</option>');
                        $("#ddlProveedor").prop("disabled", true);
                    }
                } else {
                    alert("Proveedor no encontrado.");
                }
            }
            else
            {
                $("#modelExpiracion").modal("show");
                sessionStorage.clear();
            }
        },
        error: function (xhr) {
            alert("error de ajax");
        },
        complete: function () {
        }
    });
}

function ajaxObtenerPeriodoVigente(controller, empresa) {
    $.ajax({
        type: 'POST',
        url: controller + 'ObtenerPeriodoVigente',
        data: '{ empresa: "' + empresa + '"}',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code !== "600") {
                if (response.Code === "200") {
                    for (var i = 0; i < response.Periodo.length; i++) {
                        $("#periodoVigente").html('<h2 class="family-teamwork fs-20 ml-20 mt-10">' + 'Periodo Vigente' + '</br>' + response.Periodo[i].Empresa + '</br><div id="periodo">' + response.Periodo[i].PeriodoVigente + '</div></h2>');
                        $("#optEmpresa").attr("disabled", "disabled");
                        $("#optproveedor").removeAttr("disabled", "disabled");
                        $("#txbRutProveedor").removeAttr("disabled", "disabled");
                        $("#ddlProveedor").removeAttr("disabled", "disabled");
                        $("#nFactura").removeAttr("disabled", "disabled");
                        $("#unica").removeAttr("disabled", "disabled");
                        $("#apertura").removeAttr("disabled", "disabled");
                        $("#afecta").removeAttr("disabled", "disabled");
                        $("#exenta").removeAttr("disabled", "disabled");
                        $("#ddlTipoDoc").removeAttr("disabled", "disabled");
                        $("#txtCliente").removeAttr("disabled", "disabled");
                        $("#ddlTipoDocumento").removeAttr("disabled", "disabled");
                        $("#monto").removeAttr("disabled", "disabled");
                        $("#ddlReembolso").removeAttr("disabled", "disabled");
                        $("#txtComentario").removeAttr("disabled", "disabled");
                        $("#dllCliente").removeAttr("disabled", "disabled");
                        $("select option:contains('Seleccione')").attr("disabled", "disabled");
                        $("select option:contains('0')").attr("disabled", "disabled");
                        $("#apertura").removeAttr("disabled");
                    } 
                } else {
                    $("#periodoVigente").html('<h2 class="family-teamwork fs-20 ml-20 mt-10">' + 'Periodo no Disponible' + '</br>' + '' + '</br><div id="periodo">' + '' + '</div></h2>');
                    
                    $("#optEmpresa").attr("disabled", "disabled");
                    $("#ddlTipoDoc").attr("disabled", "disabled");
                    $("#optproveedor").attr("disabled", "disabled");
                    $("#txbRutProveedor").attr("disabled", "disabled");
                    $("#ddlProveedor").attr("disabled", "disabled");
                    $("#ddlTipoDocumento").attr("disabled", "disabled");
                    $("#nFactura").attr("disabled", "disabled");
                    $("#unica").attr("disabled", "disabled");
                    $("#apertura").attr("disabled", "disabled");
                    $("#txtCliente").attr("disabled", "disabled");
                    $("#monto").attr("disabled", "disabled");
                    $("#ddlReembolso").attr("disabled", "disabled");
                    $("#txtComentario").attr("disabled", "disabled");
                    $("#dllCliente").attr("disabled", "disabled");
                    $("select option:contains('Seleccione')").attr("disabled", "disabled");
                    $("select option:contains('0')").attr("disabled", "disabled");
                    $("#apertura").attr("disabled", "disabled");
                    $("#afecto").attr("disabled", "disabled");
                    $("#exento").attr("disabled", "disabled");
                }
            }
            else {
                $("#modelExpiracion").modal("show");
                sessionStorage.clear();
            }
        },
        error: function (xhr) {
            alert("error de ajax");
        },
        complete: function () {
        }
    });
}

function ajaxGetProveedorRutTXT(controller, rutproveedor) {
    $.ajax({
        type: 'POST',
        url: controller + 'ObtenerProveedorRut',
        data: '{ rutproveedor: "' + rutproveedor + '"}',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code !== "600") {
                if (response.Code === "200") {
                    for (var i = 0; i < response.Proveedores.length; i++) {
                        $('#txbRutProveedor').val(response.Proveedores[i].Rut);
                    }
                } else {
                    alert("No encontrado");
                }
            }
            else {
                $("#modelExpiracion").modal("show");
                sessionStorage.clear();
            }
        },
        error: function (xhr) {
            alert("error de ajax");
        },
        complete: function () {
        }
    });
}

function ajaxObtenerClienteByNombre(controller, nombre) {
    $.ajax({
        type: 'POST',
        url: controller + 'ObtenerClienteByNombre',
        data: '{ nombre: "' + nombre  + '"}',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code !== "600") {
                if (response.Code === "200") {
                    for (var i = 0; i < response.Cliente.length; i++) {
                        $("#dllCliente").html('<option value="' + response.Cliente[i].Codigo + '">'+ response.Cliente[i].Descripcion_Cliente + '</option>');
                    }
                } else {
                    alert("Cliente " + nombre + " no encontrado.");
                }
            }
            else {
                $("#modelExpiracion").modal("show");
                sessionStorage.clear();
            }
        },
        error: function (xhr) {
            alert("error de ajax");
        },
        complete: function () {
        }
    });
}

function ajaxObtenerClienteByNombreApertura(controller, nombre, empresa, txtMonto) {
    $.ajax({
        type: 'POST',
        url: controller + 'ObtenerClienteByNombre',
        data: '{ nombre: "' + nombre + '",  empresa: "' + empresa + '"}',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code !== "600") {
                if (response.Code === "200") {
                    for (var i = 0; i < response.Cliente.length; i++) {
                        $("#" + txtMonto).removeAttr("disabled");

                    }
                } else {
                    alert("Cliente " + nombre + " no encontrado.");
                }
            }
            else {
                $("#modelExpiracion").modal("show");
                sessionStorage.clear();
            }
        },
        error: function (xhr) {
            alert("error de ajax");
        },
        complete: function () {
        }
    });
}

function ajaxGetExisteDocumento(controller, rutProveedor, numeroDocumento, tipoDocumento) {
    $.ajax({
        type: 'POST',
        url: controller + 'GetExisteDocumento',
        data: '{ rutProveedor: "' + rutProveedor + '", numeroDocumento: "' + numeroDocumento + '", tipoDocumento: "' + tipoDocumento + '" }',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code !== "600") {
                if (response.Cantidad !== "0") {
                    alert("Factura duplicada N° " + numeroDocumento + " para en el proveedor " + rutProveedor);
                }
            }
            else {
                $("#modelExpiracion").modal("show");
                sessionStorage.clear();
            }
        },
        error: function (xhr) {
            alert("error de ajax");
        },
        complete: function () {
        }
    });
}
