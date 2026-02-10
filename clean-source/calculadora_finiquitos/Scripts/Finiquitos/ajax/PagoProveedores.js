$(document).ready(function () {

    ajaxValidateInitProcess("PagoProveedores.aspx");
    ajaxKnowChequesInProcess("PagoProveedores.aspx");
    ajaxVisualizarTiposProveedores("PagoProveedores.aspx");

    $("#btnAgregarProveedor").on("click", function (e) {
        e.preventDefault();
        $("#modalAgregarProveedor").modal("show");
    });

    $("#btnAgregarNuevoProveedorEvent").on("click", function (e) {
        e.preventDefault();
        var rut = $("#rutProveedorNuevo").val();
        var nombre = $("#nombreProveedorNuevo").val();
        var tipo = $("#tipoProveedorNuevo").val();
        if (rut !== "" && nombre !== "" && tipo != 0) {
            ajaxAgregarProveedor("PagoProveedores.aspx", rut, nombre, tipo);
        } else {
            swal({
                type: 'warning',
                title: 'Atención!',
                text: 'Debe completar todos los campos para ingresar',
                showConfirmButton: true,
                textConfirmButton: 'Aceptar'
            });
        }
    });

    $("#initProcessGeneracionCheque").on("click", function (e) {
        e.preventDefault();
        ajaxInitProcess("PagoProveedores.aspx");
    });

    $("#continueProcessGeneracionCheque").on("click", function (e) {
        e.preventDefault();
        $("#stopProcess").hide();
        $("#initProcess").show();
        $("#closeProcessGeneracionCheque").show();
        $("#listProcessGeneracionCheque").show();
    });

    $("#listProcessGeneracionCheque").on("click", function (e) {
        e.preventDefault();
        $("#modalListadoCheques").modal("show");
    });

    $(document).on("blur", "#montoCheque", function () {
        ajaxCifraString("PagoProveedores.aspx", $(this).val());
    });

    $("#btnBuscarProveedor").on("click", function (e) {
        e.preventDefault();
        var rutProveedor = $("#rutProveedor").val();
        var empresa = $("#empresa").val();
        if(rutProveedor !== ""){
            if (empresa !== "0") {
                $(this).html("Buscando...").attr("disabled", "disabled");
                $("#infoProveedor").html('<span class="loaderTW"></span><label style="display: block; margin: 10px auto auto auto; text-align: center;">Estamos cargando los datos asociados al proveedor</label>');
                $("#infoCheque").html('<span class="loaderTW"></span><label style="display: block; margin: 10px auto auto auto; text-align: center;">Estamos cargando el correlativo disponible para el cheque.</label>');
                $("#simulaCheque").html('<span class="loaderTW"></span><label style="display: block; margin: 10px auto auto auto; text-align: center;">Estamos cargando el cheque tipo para pago de proveedor</label>');
                setTimeout(function () {
                    ajaxConsultarProveedor("PagoProveedores.aspx", rutProveedor, empresa);
                }, 500);
            } else {
                swal({
                    type: 'error',
                    title: 'No se puede consultar Proveedor',
                    text: 'Debe seleccionar la empresa asociada al cheque',
                    showConfirmButton: true,
                    textConfirmButton: 'Aceptar'
                });
            }
        } else{
            swal({
                type: 'error',
                title: 'No se puede consultar el proveedor',
                text: 'Debe indicar el rut del proveedor',
                showConfirmButton: true,
                textConfirmButton: 'Aceptar'
            });
        }
    });

    $(document).on("click", "#btnGenerarCheque", function (e) {
        e.preventDefault();
        var rutProveedor = $("#rutProveedor").val();
        var correlativo = $("#correlativoCheque").attr("data-correlativo");
        var nserieCheque = $("#nseriecheque").text();
        var monto = $("#montoCheque").val();
        var ciudad = $("#ciudadCheque").val();
        var dia = $("#diaCheque").val();
        var mes = $("#mesCheque").val();
        var year = $("#yearCheque").val();
        var quien = $("#nombrePagoACheque").val();
        var cifra = $("#cifraAPagarCheque").val();
        var siglaBanco = $("#siglaBanco").text();
        var nominativo = $("#nominativo").val();
        var empresa = $("#empresaAsociada").text();
        var observacion = $("#observacion").val();

        ajaxCargaChequeProveedores("PagoProveedores.aspx", quien, monto, dia, mes, year, empresa, cifra, rutProveedor, correlativo, nserieCheque, ciudad, siglaBanco, nominativo, observacion);
            //ajaxPrintChequeProveedor("PagoProveedores.aspx", rutProveedor, correlativo, nserieCheque, monto, ciudad, dia, mes, year, quien, cifra, siglaBanco, nominativo, empresa);
    });

    $("#closeProcessGeneracionCheque").on("click", function (e) {
        e.preventDefault();

        swal({
            title: 'Espere!',
            text: 'Estamos preparando el archivo tipo para la impresión del cheque.',
            imageUrl: 'http://angelsysdoc.com/images/esperar.png',
            imageWidth: 50,
            imageHeight: 50,
            showConfirmButton: false,
            imageAlt: 'Custom image',
            animation: true
        });

        setTimeout(function () {
            ajaxCloseProcess("PagoProveedores.aspx");
        }, 500);
    });

    function ajaxAgregarProveedor(page, rut, nombre, tipo) {
        $.ajax({
            type: 'POST',
            url: page + '/SetInsertarProveedorService',
            data: '{ rut: "' + rut + '", nombre: "' + nombre + '", tipo: "' + tipo + '" }',
            dataType: 'json',
            contentType: 'application/json',
            async: true,
            success: function (response) {
                var data = JSON.parse(response.d);
                for (var i = 0; i < data.resultado.length; i++) {
                    if (data.resultado[i].VALIDACION === "0") {
                        swal({
                            type: 'success',
                            title: 'Se ha agregado exitosamente!',
                            text: data.resultado[i].RESULTADO,
                            showConfirmButton: true,
                            textConfirmButton: 'Aceptar'
                        });

                        $("#rutProveedorNuevo").val("");
                        $("#nombreProveedorNuevo").val("");
                        $("#tipoProveedorNuevo > option[value='0']").attr("selected", "selected");

                    } else {
                        swal({
                            type: 'error',
                            title: 'Ha ocurrido algo inesperado!',
                            text: data.resultado[i].RESULTADO,
                            showConfirmButton: true,
                            textConfirmButton: 'Aceptar'
                        });
                    }
                }
            },
            error: function (xhr) {
                console.log(xhr.responseText);
            },
            complete: function () {

            }
        });
    }

    function ajaxVisualizarTiposProveedores(page) {
        $.ajax({
            type: 'POST',
            url: page + '/GetVisualizarTiposProveedoresService',
            data: '{ }',
            dataType: 'json',
            contentType: 'application/json',
            async: true,
            success: function (response) {
                var data = JSON.parse(response.d);
                for (var i = 0; i < data.resultado.length; i++) {
                    if (data.resultado[i].VALIDACION === "0") {
                        $("#tipoProveedorNuevo").append('<option value="' + data.resultado[i].ID + '">' + data.resultado[i].NOMBRE + '</option>');
                    }
                }
            },
            error: function (xhr) {
                console.log(xhr.responseText);
            },
            complete: function () {

            }
        });
    }

    function ajaxCifraString(page, monto) {
        $.ajax({
            type: 'POST',
            url: page + '/GetMontoCifra',
            data: '{ monto: "' + monto + '" }',
            dataType: 'json',
            contentType: 'application/json',
            async: true,
            success: function (response) {
                $("#cifraAPagarCheque").val(response.d);
            },
            error: function (xhr) {
                console.log(xhr.responseText);
            },
            complete: function () {

            }
        });
    }

    function ajaxConsultarProveedor(page, rutproveedor, empresa) {
        $.ajax({
            type: 'POST',
            url: page + '/GetObtenerProveedorService',
            data: '{ rutProveedor: "' + rutproveedor + '" }',
            dataType: 'json',
            contentType: 'application/json',
            async: true,
            success: function (response) {
                var data = JSON.parse(response.d);
                var content = "";
                if (data.resultado.length > 0) {

                    for (var i = 0; i < data.resultado.length; i++) {
                        if (data.resultado[i].VALIDACION == 0) {
                            content = content + '<div class="row" style="text-align: left;">';
                            content = content + '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">';
                            content = content + '<h3>Información asociada al proveedor</h3>';
                            content = content + '<label style="display: inline-block; width: 200px;">Nombre Proveedor</label>';
                            content = content + '<label style="margin: auto 5px auto 5px;">:</label>';
                            content = content + '<label>' + data.resultado[i].NOMBRE + '</label> <br />';
                            content = content + '<label style="display: inline-block; width: 200px;">Rut Proveedor</label>';
                            content = content + '<label style="margin: auto 5px auto 5px;">:</label>';
                            content = content + '<label>' + data.resultado[i].RUT + '</label> <br />';
                            content = content + '<label style="display: inline-block; width: 200px;">Tipo Proveedor</label>';
                            content = content + '<label style="margin: auto 5px auto 5px;">:</label>';
                            content = content + '<label>' + data.resultado[i].TIPO + '</label> <br />';
                            content = content + '</div>';
                            content = content + '</div>';
                        } else {
                            content = content + '<label>' + data.resultado[i].RESULTADO + '</label>';
                        }
                    }
                    $("#infoProveedor").html(content);

                } else {
                    swal({
                        type: 'warning',
                        title: "Atención!",
                        text: "Servicio no disponible en este momento, intentelo más tarde.",
                        showConfirmButton: false,
                        timer: 2000
                    });
                }
            },
            error: function () {
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

        $('#nominativo').on('change', function () {
            if ($(this).is(':checked')) {
                $(this).val("S");
            } else {
                $(this).val("N");
            }
        });

        $.ajax({
            type: 'POST',
            url: page + '/GetObtenerCorrelativoDisponibleProveedoresService',
            data: '{ empresa: "' + empresa + '" }',
            dataType: 'json',
            contentType: 'application/json',
            async: true,
            success: function (response) {
                var data = JSON.parse(response.d);
                var content = "";
                if (data.resultado.length > 0) {

                    for (var i = 0; i < data.resultado.length; i++) {
                        if (data.resultado[i].VALIDACION == 0) {
                            content = content + '<div class="row" style="text-align: left;">';
                            content = content + '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">';
                            content = content + '<h3>Información asociada al Cheque a generar</h3>';
                            content = content + '<label style="display: inline-block; width: 200px;">Banco Asociado</label>';
                            content = content + '<label style="margin: auto 5px auto 5px;">:</label>';
                            content = content + '<label>' + data.resultado[i].BANCO + ' (</label><label id="siglaBanco">' + data.resultado[i].SIGLABANCO + '</label><label>)</label> <br />';
                            content = content + '<label style="display: inline-block; width: 200px;">Correlativo del Cheque</label>';
                            content = content + '<label style="margin: auto 5px auto 5px;">:</label>';
                            content = content + '<label>N°</label> <label id="correlativoCheque" data-correlativo="' + data.resultado[i].ID + '">' + data.resultado[i].ID_CORRELATIVO + '</label> <br />';
                            content = content + '<label style="display: inline-block; width: 200px;">Empresa asociada</label>';
                            content = content + '<label style="margin: auto 5px auto 5px;">:</label>';
                            content = content + '<label id="empresaAsociada">' + data.resultado[i].EMPRESA + '</label> <br />';
                            content = content + '<label style="display: inline-block; width: 200px;">Número Serie del Cheque</label>';
                            content = content + '<label style="margin: auto 5px auto 5px;">:</label>';
                            content = content + '<label>N°</label> <label id="nseriecheque">' + creaNumeroSerieCheque(data.resultado[i].ID_CORRELATIVO) + '</label> <br />';
                            content = content + '<label style="display: inline-block; width: 200px;">Cheque nominativo</label>';
                            content = content + '<div class="form-group" style="display: inline-block;">';
                            content = content + '<select id="nominativo" class="form-control" style="display: inline-block;">';
                            content = content + '<option value="S">Sí</option>';
                            content = content + '<option value="N">No</option>';
                            content = content + '</select>';
                            content = content + '</div>';
                            content = content + '</div>';
                            content = content + '</div>';
                        } else {
                            content = content + '<label>' + data.resultado[i].RESULTADO + '</label>';
                        }
                    }
                    $("#infoCheque").html(content);

                } else {
                    swal({
                        type: 'warning',
                        title: "Atención!",
                        text: "Servicio no disponible en este momento, intentelo más tarde.",
                        showConfirmButton: false,
                        timer: 2000
                    });
                }
            },
            error: function () {
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

        $.ajax({
            type: 'POST',
            url: page + '/GetObtenerProveedorService',
            data: '{ rutProveedor: "' + rutproveedor + '" }',
            dataType: 'json',
            contentType: 'application/json',
            async: true,
            success: function (response) {
                var data = JSON.parse(response.d);
                var content = "";
                if (data.resultado.length > 0) {

                    for (var i = 0; i < data.resultado.length; i++) {
                        if (data.resultado[i].VALIDACION == 0) {
                            
                            content = content + '<div class="row" style="text-align: left; width: 92%; margin: auto;">';
                            content = content + '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">';
                            content = content + '<h3>Observación asociado al pago: </h3>';
                            content = content + '</div>';
                            content = content + '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">';
                            content = content + '<textarea class="form-control" id="observacion"></textarea>';
                            content = content + '</div>';
                            content = content + '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">';
                            content = content + '<h3>Incorpore el monto del cheque y la ciudad para pago del cheque: </h3>';
                            content = content + '</div>';
                            content = content + '</div>';
                            content = content + '<div class="row" style="width: 80%; margin: auto auto 50px auto; border: 1px solid rgb(200,200,200);">';
                            content = content + '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="text-align: right;">';
                            content = content + '<div class="form-group" style="margin-right: 10px; margin-top: 20px;">';
                            content = content + '<label for="exampleInputEmail1">$</label> ';
                            content = content + '<input type="email" class="form-control" style="display: inline-block; width: 40%;" id="montoCheque" aria-describedby="emailHelp" placeholder="Monto del Cheque" />';
                            content = content + '</div>';
                            content = content + '<div class="form-group" style="margin-right: 10px; margin-top: 10px;">';
                            content = content + '<input type="email" class="form-control" style="display: inline-block; width: 10%; text-align: center; margin-right: 20px;" id="ciudadCheque" aria-describedby="emailHelp" placeholder="Ciudad" value="Stgo" readonly="readonly" /> ';
                            content = content + '<input type="email" class="form-control" style="display: inline-block; width: 10%; text-align: center;" id="diaCheque" aria-describedby="emailHelp" placeholder="DD" readonly="readonly" value="' + data.resultado[i].DIA + '" /> ';
                            content = content + '<input type="email" class="form-control" style="display: inline-block; width: 10%; text-align: center;" id="mesCheque" aria-describedby="emailHelp" placeholder="MM" readonly="readonly" value="' + data.resultado[i].MES + '" /> ';
                            content = content + '<input type="email" class="form-control" style="display: inline-block; width: 15%; text-align: center;" id="yearCheque" aria-describedby="emailHelp" placeholder="AAAA" readonly="readonly" value="' + data.resultado[i].YEAR + '" /> ';
                            content = content + '</div>';
                            content = content + '</div>';
                            content = content + '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">';
                            content = content + '<div class="form-group" style="margin-right: 10px; margin-top: 20px;">';
                            content = content + '<input type="email" class="form-control" style="width: 100%; display: inline-block;" id="nombrePagoACheque" aria-describedby="emailHelp" placeholder="Paguese a la orden de" readonly="readonly" value="' + data.resultado[i].NOMBRE + '" />';
                            content = content + '</div>';
                            content = content + '</div>';
                            content = content + '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">';
                            content = content + '<div class="form-group" style="margin-right: 10px; margin-top: 5px;">';
                            content = content + '<input type="email" class="form-control" style="width: 100%; display: inline-block;" id="cifraAPagarCheque" aria-describedby="emailHelp" placeholder="La suma de" readonly="readonly" />';
                            content = content + '</div>';
                            content = content + '</div>';
                            content = content + '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="margin-bottom: 20px;">';
                            content = content + '<a href="#" class="btn btn-primary" id="btnGenerarCheque">Generar Cheque</a>';
                            content = content + '</div>';
                            content = content + '</div>';
                            
                        } else {
                            content = content + '<label>' + data.resultado[i].RESULTADO + '</label>';
                        }
                    }

                    
                    $("#simulaCheque").html(content);

                } else {
                    swal({
                        type: 'warning',
                        title: "Atención!",
                        text: "Servicio no disponible en este momento, intentelo más tarde.",
                        showConfirmButton: false,
                        timer: 2000
                    });
                }
            },
            error: function () {
                swal({
                    type: 'warning',
                    title: "Atención!",
                    text: "Servicio no disponible en este momento, intentelo más tarde.",
                    showConfirmButton: false,
                    timer: 2000
                });
            },
            complete: function () {
                $("#btnBuscarProveedor").html("Buscar Proveedor").removeAttr("disabled");
            }
        });
    }

    function ajaxValidateInitProcess(page) {
        $.ajax({
            type: 'POST',
            url: page + '/GetTMPValidateProcessInitProveedoresService',
            data: '{ }',
            dataType: 'json',
            contentType: 'application/json',
            async: true,
            success: function (response) {
                var data = JSON.parse(response.d);
                if (data.resultado.length > 0) {

                    for (var i = 0; i < data.resultado.length; i++) {
                        if (data.resultado[i].VALIDACION == 0) {
                            $("#initProcessGeneracionCheque").show();
                            $("#closeProcessGeneracionCheque").hide();
                            $("#listProcessGeneracionCheque").hide();
                            $("#initProcess").hide();
                            $("#stopProcess").show()
                            $("#infoProveedor").html("");
                            $("#infoCheque").html("");
                            $("#simulaCheque").html("");
                        } else {
                            $("#continueProcessGeneracionCheque").show();
                        }
                    }

                }
            },
            error: function (xhr) {
                console.log(xhr.responseText);
            },
            complete: function () {

            }
        });
    }

    function ajaxInitProcess(page) {
        $.ajax({
            type: 'POST',
            url: page + '/SetTMPInitProcessChequeProveedorProveedoresService',
            data: '{ }',
            dataType: 'json',
            contentType: 'application/json',
            async: true,
            success: function (response) {
                var data = JSON.parse(response.d);
                if (data.resultado.length > 0) {

                    for (var i = 0; i < data.resultado.length; i++) {
                        if (data.resultado[i].VALIDACION == 0) {
                            $("#stopProcess").hide();
                            $("#initProcess").show();
                            $("#closeProcessGeneracionCheque").show();
                            $("#listProcessGeneracionCheque").show();
                        }
                    }

                }
            },
            error: function (xhr) {
                console.log(xhr.responseText);
            },
            complete: function () {

            }
        });
    }

    //function ajaxPrintChequeProveedor(page, rutProveedor, correlativo, nseriecheque, monto, ciudad, dia, mes, year, quien, cifra, siglaBanco, nominativo, empresa) {
    //    $.ajax({
    //        type: 'POST',
    //        url: page + '/PrintChequeProveedores',
    //        data: '{ rutProveedor: "' + rutProveedor + '", correlativo: "' + correlativo + '", nseriecheque: "' + nseriecheque + '", monto: "' + monto + '", ciudad: "' + ciudad + '", ' +
    //            ' dia: "' + dia + '", mes: "' + mes +'", year: "' + year + '", quien: "' +  quien + '", cifra: "' + cifra + '", siglaBanco: "' + siglaBanco + '", nominativo: "' + nominativo + '", empresa: "' + empresa + '" }',
    //        dataType: 'json',
    //        contentType: 'application/json',
    //        async: true,
    //        success: function (response) {
    //            if (response.d !== "") {
    //                if (response.d.indexOf(".xlsx") > -1) {
    //                    swal({
    //                        type: 'success',
    //                        title: 'Listo para generar la impresión',
    //                        text: 'El archivo para imprimir el cheque es ' + response.d + ', una vez impreso el cheque acepta este cuadro.',
    //                        showConfirmButton: true,
    //                        textConfirmButton: 'Aceptar'
    //                    });
    //                } else {
    //                    swal({
    //                        type: 'error',
    //                        title: 'No se puede generar el cheque',
    //                        text: response.d,
    //                        showConfirmButton: true,
    //                        textConfirmButton: 'Aceptar'
    //                    });
    //                }
    //            }
    //        },
    //        error: function (xhr) {
    //            console.log(xhr.responseText);
    //        },
    //        complete: function () {

    //        }
    //    });
    //}

    function ajaxCargaChequeProveedores(page, nombreTrabajador, monto, dia, mes, year, empresa, cifra, rutProveedor, correlativo, nseriecheque, ciudad, siglaBanco, nominativo, observacion) {
        $.ajax({
            type: 'POST',
            url: page + '/SetTMPCargaChequeProveedorProveedoresService',
            data: '{ nombreTrabajador: "' + nombreTrabajador + '", monto: "' + monto + '", dia: "' + dia + '", mes: "' + mes + '", year: "' + year + '", empresa: "' + empresa + '",' +
                  ' cifra: "' + cifra + '", rutProveedor: "' + rutProveedor + '", correlativo: "' + correlativo + '", nseriecheque: "' + nseriecheque + '", ciudad: "' + ciudad + '",' +
                  ' siglaBanco: "' + siglaBanco + '",  nominativo: "' + nominativo + '", observacion: "' + observacion + '" }',
            dataType: 'json',
            contentType: 'application/json',
            async: true,
            success: function (response) {
                var data = JSON.parse(response.d);
                if (data.resultado.length > 0) {

                    for (var i = 0; i < data.resultado.length; i++) {
                        if (data.resultado[i].VALIDACION == 0) {
                            swal({
                                type: 'success',
                                title: 'Se ha cargado exitosamente!',
                                text: data.resultado[i].RESULTADO,
                                showConfirmButton: true,
                                textConfirmButton: 'Aceptar'
                            });
                            ajaxKnowChequesInProcess("PagoProveedores.aspx");
                        } else {
                            swal({
                                type: 'success',
                                title: 'No se pudo cargar!',
                                text: data.resultado[i].RESULTADO,
                                showConfirmButton: true,
                                textConfirmButton: 'Aceptar'
                            });
                        }
                    }

                }
            },
            error: function (xhr) {
                console.log(xhr.responseText);
            },
            complete: function () {

            }
        });
    }
    
    function ajaxKnowChequesInProcess(page) {
        $.ajax({
            type: 'POST',
            url: page + '/GetTMPChequesInProcessProveedorService',
            data: '{ }',
            dataType: 'json',
            contentType: 'application/json',
            async: true,
            success: function (response) {
                var data = JSON.parse(response.d);
                var content = "";
                if (data.resultado.length > 0) {
                    for (var i = 0; i < data.resultado.length; i++) {
                        if (data.resultado[i].VALIDACION == 0) {
                            if(i == 0){
                                $("#cantidadGeneradoCheque").html(data.resultado.length);
                            }

                            $("#totalGeneradoCheque").html(formatNumber.new(String(parseInt($("#totalGeneradoCheque").html().split(".").join("")) + parseInt(data.resultado[i].MONTO))));

                            content = content + '<hr />';
                            content = content + '<p style="font-size: 15px;">';
                            content = content + data.resultado[i].NOMBRETRABAJADOR + '<br />';
                            content = content + 'EMPRESA ' + data.resultado[i].EMPRESA + ' &bull; N° SERIE ' + data.resultado[i].SERIE + ' &bull; <i style="color: #0094ff; font-style: normal;">Monto $ ' + formatNumber.new(data.resultado[i].MONTO) + '</i> <br/>';
                            content = content + '<i style="color: #0094ff; font-style: normal; font-size: 14px;">Generado el ' + formatStringDate(data.resultado[i].FECHA_GENERACION) + '</i>';
                            content = content + '</p>';
                        } else {
                            $("#cantidadGeneradoCheque").html("0");
                            content = content + '<hr />';
                            content = content + '<p style="font-size: 15px; text-align: center;">';
                            content = content + data.resultado[i].RESULTADO;
                            content = content + '</p>';
                        }
                    }

                    $("#listChequesProveedores").html(content);

                }
            },
            error: function (xhr) {
                console.log(xhr.responseText);
            },
            complete: function () {

            }
        });
    }

    function ajaxCloseProcess(page) {
        $.ajax({
            type: 'POST',
            url: page + '/SetTMPCloseProcessChequeProveedorService',
            data: '{ }',
            dataType: 'json',
            contentType: 'application/json',
            async: true,
            success: function (response) {
                if (response.d !== "") {
                    if (response.d.indexOf(".xlsx") > -1) {
                        swal({
                            type: 'success',
                            title: 'Archivo de impresión generado!',
                            text: 'Se ha generado el archivo de impresión el cheque el cual es ' + response.d + '.',
                            showConfirmButton: true,
                            textConfirmButton: 'Aceptar'
                        });
                        ajaxValidateInitProcess("PagoProveedores.aspx");
                    } else {
                        swal({
                            type: 'error',
                            title: 'No se puede generar el cheque',
                            text: response.d,
                            showConfirmButton: true,
                            textConfirmButton: 'Aceptar'
                        });
                    }
                }
            },
            error: function (xhr) {
                console.log(xhr.responseText);
            },
            complete: function () {

            }
        });
    }

    function creaNumeroSerieCheque(correlativo) {
        var numeroSerie = "";

        switch(String(correlativo).length){
            case 1:
                numeroSerie = "000000" + correlativo;
                break;
            case 2:
                numeroSerie = "00000" + correlativo;
                break;
            case 3:
                numeroSerie = "0000" + correlativo;
                break;
            case 4:
                numeroSerie = "000" + correlativo;
                break;
            case 5:
                numeroSerie = "00" + correlativo;
                break;
            case 6:
                numeroSerie = "0" + correlativo;
                break;
            case 7:
                numeroSerie = correlativo;
                break;
        }

        return numeroSerie;
    }

    var formatNumber = {
        separador: ".", // separador para los miles
        sepDecimal: ',', // separador para los decimales
        formatear: function (num) {
            num += '';
            var splitStr = num.split('.');
            var splitLeft = splitStr[0];
            var splitRight = splitStr.length > 1 ? this.sepDecimal + splitStr[1] : '';
            var regx = /(\d+)(\d{3})/;
            while (regx.test(splitLeft)) {
                splitLeft = splitLeft.replace(regx, '$1' + this.separador + '$2');
            }
            return this.simbol + splitLeft + splitRight;
        },
        new: function (num, simbol) {
            this.simbol = simbol || '';
            return this.formatear(num);
        }
    }

    function formatStringDate(fecha) {
        var fechaFormat = "";
        var arrayDateTime = fecha.split("T");

        var arrayFecha = arrayDateTime[0].split("-");

        fechaFormat = arrayFecha[2];

        switch (parseInt(arrayFecha[1])) {
            case 1:
                fechaFormat = fechaFormat + " de enero de ";
                break;
            case 2:
                fechaFormat = fechaFormat + " de feberero de ";
                break;
            case 3:
                fechaFormat = fechaFormat + " de marzo de ";
                break;
            case 4:
                fechaFormat = fechaFormat + " de abril de ";
                break;
            case 5:
                fechaFormat = fechaFormat + " de mayo de ";
                break;
            case 6:
                fechaFormat = fechaFormat + " de junio de ";
                break;
            case 7:
                fechaFormat = fechaFormat + " de julio de ";
                break;
            case 8:
                fechaFormat = fechaFormat + " de agosto de ";
                break;
            case 9:
                fechaFormat = fechaFormat + " de septiembre de ";
                break;
            case 10:
                fechaFormat = fechaFormat + " de octubre de ";
                break;
            case 11:
                fechaFormat = fechaFormat + " de noviembre de ";
                break;
            case 12:
                fechaFormat = fechaFormat + " de diciembre de ";
                break;
        }

        fechaFormat = fechaFormat + arrayFecha[0] + ", a las " + arrayDateTime[1].split(".")[0] + " horas.";

        return fechaFormat;
    }

});