$(document).ready(function () {

    $('#pagoCheque-tab').on('click', function (e) {
        e.preventDefault()
        $("#pagoCheque-tabContent").tab('show');
        $("#pagoTransf-tabContent").tab('hide');
    });
    $('#pagoTransf-tab').on('click', function (e) {
        e.preventDefault()
        $("#pagoCheque-tabContent").tab('hide');
        $("#pagoTransf-tabContent").tab('show');
    });

    $("#btnReporteria").on("click", function (e) {
        e.preventDefault();
        $("#modalReporteriaFiniquitos").modal("show");
    });

    ajaxValidateInitProcess("Inicio.aspx");
    ajaxKnowChequesInProcess("Inicio.aspx");
    ajaxConfirmados("Inicio.aspx", "CHEQUE", "", "");
    ajaxConfirmados("Inicio.aspx", "TRANSFERENCIA", "", "");
    ajaxConfirmados("Inicio.aspx", "NOMINA", "", "");
    $("#loadingFiniquitos").html('<span class="loaderTW"></span><label class="loading-text-html">ESTAMOS CARGANDO LOS FINIQUITOS, ESPERE...</label>');
    setTimeout(function () {
        ajaxRecientes("Inicio.aspx", "", "");
    }, 500);

    /** FILTRADO CONFIRMADOS POR CHEQUE */
    $("#filtrarPorConfirmados").on("keyup", function () {
        if ($(this).val().length > 0) {
            ajaxConfirmados("Inicio.aspx", "CHEQUE", $("#filtroConfirmados").val(), $(this).val());
        } else {
            ajaxConfirmados("Inicio.aspx", "CHEQUE", "", "");
        }
    });

    $("#filtrarEmpresaConfirmados").on("change", function () {
        switch ($(this).val()) {
            case "ALL":
                ajaxConfirmados("Inicio.aspx", "CHEQUE", "", "");
                break;
            default:
                ajaxConfirmados("Inicio.aspx", "CHEQUE", $("#filtroConfirmados").val(), $(this).val());
                break;
        }
    });

    $("#filtroConfirmados").on("change", function () {
        switch ($(this).val()) {
            case "SIN FILTRO":
                ajaxConfirmados("Inicio.aspx", "CHEQUE", "", "");
                $("#filtrarPorConfirmados").hide();
                $("#filtrarEmpresaConfirmados").hide();
                break;
            case "FOLIO":
                $("#filtrarPorConfirmados").show().attr("placeholder", "N° de Folio");
                $("#filtrarEmpresaConfirmados").hide();
                break;
            case "RUT":
                ajaxConfirmados("Inicio.aspx", "CHEQUE", "", "");
                $("#filtrarPorConfirmados").show().attr("placeholder", "Ej. 018.425.683-5");
                $("#filtrarEmpresaConfirmados").hide();
                break;
            case "NOMBRE":
                ajaxConfirmados("Inicio.aspx", "CHEQUE", "", "");
                $("#filtrarPorConfirmados").show().attr("placeholder", "Nombre del Trabajador");
                $("#filtrarEmpresaConfirmados").hide();
                break;
            case "EMPRESA":
                $("#filtrarPorConfirmados").hide();
                $("#filtrarEmpresaConfirmados").show();
                break;
        }
    });

    $("#initProcessGeneracionCheque").on("click", function (e) {
        e.preventDefault();
        ajaxInitProcess("Inicio.aspx");
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

    $(document).on("click", ".eventSelFolio", function (e) {
        e.preventDefault();
        var idDesvinculacion = $(this).attr("data-iddesvinculacion");
        ajaxObtenerMontoCheque("Inicio.aspx", idDesvinculacion);
    });

    $(document).on("click", ".eventSelFolioTransfNomin", function (e) {
        e.preventDefault();
        var idDesvinculacion = $(this).attr("data-iddesvinculacion");
        ajaxObtenerMontoPago("Inicio.aspx", idDesvinculacion);
    });

    $(document).on("click", ".btnVisualizarCalculo", function (e) {
        e.preventDefault();
        sessionStorage.setItem("applicationIdDesvinculacionCalculo", $(this).attr("data-iddesvinculacion"));
        window.location.href = "VisualizacionCalculoFiniquito.aspx";
    });

    $(document).on("click", ".btnAnularCalculo", function (e) {
        e.preventDefault();
        swal({
            title: 'Anulación de Folio N° ' + $(this).attr("data-iddesvinculacion"),
            text: "Esta a punto de anular un folio, esto conlleva la eliminación de todos los datos asociados, ¿Quiere anular el folio?",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Sí, Anular!',
            cancelButtonText: 'No, Cancelar',
            closeOnConfirm: false,
            closeOnCancel: false
        }).then((result) => {
            ajaxAnularFolio("Inicio.aspx", $(this).attr("data-iddesvinculacion"));
        });
    });

    $(document).on("click", ".btnAnularPago", function (e) {
        e.preventDefault();
        swal({
            title: 'Anulación Pago para Folio N° ' + $(this).attr("data-iddesvinculacion"),
            text: "Esta a punto de anular el pago y la emisión de cheque, el folio de finiquito quedará como historia, ¿Desea proseguir con la anulación?",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Sí, Continuar!',
            cancelButtonText: 'No, Cancelar',
            closeOnConfirm: false,
            closeOnCancel: false
        }).then((result) => {
            swal({
                input: 'textarea',
                html: '<h3>Motivo de la anulación del pago</h3>',
                type: 'warning',
                confirmButtonText: 'Guardar',
                cancelButtonText: 'Cancelar',
                showCancelButton: true,
            }).then((result) => {
                if(result != ""){
                    ajaxAnularPago("Inicio.aspx", $(this).attr("data-iddesvinculacion"), result);
                } else { 
                    swal({
                        type: 'warning',
                        title: 'No podemos proseguir',
                        text: 'El motivo de la anulación es obligatoria, favor completarla para anular pago'
                    });
                }
            }); 
        });
    });

    $(document).on("click", ".btnRevertirConfirmacion", function (e) {
        e.preventDefault();
        swal({
            title: 'Revertir confirmación de Folio N° ' + $(this).attr("data-iddesvinculacion"),
            text: "Esta a punto de revertir la confirmación del finiquito para pago, ¿Quiere revertir la confirmación?",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Sí, Revertir!',
            cancelButtonText: 'No, Cancelar',
            closeOnConfirm: false,
            closeOnCancel: false
        }).then((result) => {
            ajaxRevertirConfirmacionPago("Inicio.aspx", $(this).attr("data-iddesvinculacion"));
        });
    });

    $(document).on("click", ".btnNotariado", function (e) {
        e.preventDefault();
        swal({
            title: 'Finiquito Notariado con Folio N° ' + $(this).attr("data-iddesvinculacion"),
            text: "Esta a punto de marcar como notariado el finiquito, ¿Quiere marcarlo como notariado?",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Sí, Revertir!',
            cancelButtonText: 'No, Cancelar',
            closeOnConfirm: false,
            closeOnCancel: false
        }).then((result) => {
            ajaxNotariadoFiniquito("Inicio.aspx", $(this).attr("data-iddesvinculacion"));
        });
    });

    $(document).on("click", ".btnPagarFiniquito", function (e) {
        e.preventDefault();
        swal({
            title: 'Pago de Finiquito Folio N° ' + $(this).attr("data-iddesvinculacion"),
            text: "Esta a punto de marcar el pago del finiquito, esto conlleva el cambio de estado a pagado de todos los contratos asociados al finiquito, ¿Quiere marcar pago del finiquito?",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Sí, Pagar!',
            cancelButtonText: 'No, Cancelar',
            closeOnConfirm: false,
            closeOnCancel: false
        }).then((result) => {
            ajaxPagarFiniquito("Inicio.aspx", $(this).attr("data-iddesvinculacion"));
        });
    });

    $("#filtro").on("keyup", function () {
        switch ($("#filtrarPor").val()) {
            case "SIN FILTRO":
                $("#loadingFiniquitos").html('<span class="loaderTW"></span><label class="loading-text-html">ESTAMOS CARGANDO LOS FINIQUITOS, ESPERE...</label>');
                setTimeout(function () {
                    ajaxRecientes("Inicio.aspx", "", "");
                }, 500);
                break;
            default:
                if ($("#filtro").val().length > 0) {
                    $("#loadingFiniquitos").html('<span class="loaderTW"></span><label class="loading-text-html">ESTAMOS CARGANDO LOS FINIQUITOS, ESPERE...</label>');
                    setTimeout(function () {
                        ajaxRecientes("Inicio.aspx", $("#filtrarPor").val(), $("#filtro").val());
                    }, 500);
                } else {
                    $("#loadingFiniquitos").html('<span class="loaderTW"></span><label class="loading-text-html">ESTAMOS CARGANDO LOS FINIQUITOS, ESPERE...</label>');
                    setTimeout(function () {
                        ajaxRecientes("Inicio.aspx", "", "");
                    }, 500);
                }
                break;
        }
    });

    $("#filtroEmpresa").on("change", function () {
        switch($(this).val()){
            case "SELECCIONE EMPRESA":
                $("#loadingFiniquitos").html('<span class="loaderTW"></span><label class="loading-text-html">ESTAMOS CARGANDO LOS FINIQUITOS, ESPERE...</label>');
                setTimeout(function () {
                    ajaxRecientes("Inicio.aspx", "", "");
                }, 500);
                break;
            default:
                $("#loadingFiniquitos").html('<span class="loaderTW"></span><label class="loading-text-html">ESTAMOS CARGANDO LOS FINIQUITOS, ESPERE...</label>');
                setTimeout(function () {
                    ajaxRecientes("Inicio.aspx", $("#filtrarPor").val(), $("#filtroEmpresa").val());
                }, 500);
                break;
        }
    });

    $("#filtrarPor").on("change", function () {
        switch($(this).val()){
            case "SIN FILTRO":
                $("#filtroEmpresa").hide();
                $("#tablaFiniquitosRecientes > thead").html("");
                $("#tablaFiniquitosRecientes > tbody").html("");
                $(".paginationRecientes").html("");
                $("#filtro").hide().removeAttr("placeholder");
                $("#loadingFiniquitos").html('<span class="loaderTW"></span><label class="loading-text-html">ESTAMOS CARGANDO LOS FINIQUITOS, ESPERE...</label>');
                setTimeout(function () {
                    ajaxRecientes("Inicio.aspx", "", "");
                }, 500);
                break;
            case "NOMBRE":
                $("#filtroEmpresa").hide();
                $("#filtro").show().val("").attr("placeholder", "Nombre Trabajador");
                break;
            case "FOLIO":
                $("#filtroEmpresa").hide();
                $("#filtro").show().val("").attr("placeholder", "Número de Folio");
                break;
            case "RUT":
                $("#filtroEmpresa").hide();
                $("#filtro").show().val("").attr("placeholder", "Rut Trabajador (Ej. 018.425.683-5)");
                break;
            case "EMPRESA":
                $("#filtroEmpresa").show();
                $("#filtro").hide().removeAttr("placeholder");
                break;
            case "PARTTIME":
                $("#filtroEmpresa").hide();
                $("#loadingFiniquitos").html('<span class="loaderTW"></span><label class="loading-text-html">ESTAMOS CARGANDO LOS FINIQUITOS, ESPERE...</label>');
                setTimeout(function () {
                    ajaxRecientes("Inicio.aspx", $("#filtrarPor").val(), $("#filtrarPor").text());
                }, 500);
                break;
            case "FULLTIME":
                $("#filtroEmpresa").hide();
                $("#loadingFiniquitos").html('<span class="loaderTW"></span><label class="loading-text-html">ESTAMOS CARGANDO LOS FINIQUITOS, ESPERE...</label>');
                setTimeout(function () {
                    ajaxRecientes("Inicio.aspx", $("#filtrarPor").val(), $("#filtrarPor").text());
                }, 500);
                break;
        }
    });
    
    $("#btnGenerarCheque").on("click", function (e) {
        e.preventDefault();
        var idDesvinculacion = $(this).attr("data-idDesvinculacion");
        var nominativo = $("#nominativo").val();
        var empresa = $("#empresa").val();

        if (empresa !== "0") {
            ajaxCargaChequeFiniquitos("Inicio.aspx", idDesvinculacion, nominativo, empresa);
        } else {
            swal({
                type: 'error',
                title: 'No se puede generar cheque',
                text: 'Debe seleccionar la empresa asociada al cheque',
                showConfirmButton: true,
                textConfirmButton: 'Aceptar'
            });
        }
        
    });

    $("#btnGenerarPago").on("click", function (e) {
        e.preventDefault();
        var idDesvinculacion = $(this).attr("data-idDesvinculacion");
        var nominativo = $("#nominativo").val();
        var empresa = $("#empresa").val();

        if (empresa !== "0") {
            ajaxCargaPagoFiniquitos("Inicio.aspx", idDesvinculacion);
        } else {
            swal({
                type: 'error',
                title: 'No se puede generar cheque',
                text: 'Debe seleccionar la empresa asociada al cheque',
                showConfirmButton: true,
                textConfirmButton: 'Aceptar'
            });
        }

    });

    function ajaxCargaChequeFiniquitos(page, idDesvinculacion, nominativo, empresa) {
        $.ajax({
            type: 'POST',
            url: page + '/SetTMPCargaChequeFiniquitosService',
            data: '{ idDesvinculacion: "' + idDesvinculacion + '", nominativo: "' + nominativo + '", empresa: "' + empresa + '" }',
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
                            ajaxKnowChequesInProcess("Inicio.aspx");
                            if ($("#filtroConfirmados").val() === "SIN FILTRO") {
                                ajaxConfirmados("Inicio.aspx", "CHEQUE", "", "");
                            } else {
                                if ($("#filtroConfirmados").val() !== "EMPRESA") {
                                    ajaxConfirmados("Inicio.aspx", "CHEQUE", $("#filtroConfirmados").val(), $("#filtrarPorConfirmados").val());
                                } else {
                                    if ($("#filtrarEmpresaConfirmados").val() !== "ALL") {
                                        ajaxConfirmados("Inicio.aspx", "CHEQUE", $("#filtroConfirmados").val(), $("#filtrarEmpresaConfirmados").val());
                                    } else {
                                        ajaxConfirmados("Inicio.aspx", "CHEQUE", "", "");
                                    }
                                }
                            }
                            
                            $("#modalConfirmacion").modal("hide");
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
            ajaxCloseProcess("Inicio.aspx");
        }, 500);
    });

    function ajaxObtenerMontoCheque(page, idDesvinculacion) {
        $.ajax({
            type: 'POST',
            url: page + '/GetObtenerMontoChequePagosService',
            data: '{ idDesvinculacion: "' + idDesvinculacion + '" }',
            dataType: 'json',
            contentType: 'application/json',
            async: true,
            success: function (response) {
                var data = JSON.parse(response.d);
                if (data.resultado.length > 0) {
                    for (var i = 0; i < data.resultado.length; i++) {
                        if (data.resultado[i].VALIDACION == 0) {
                            $("#montoFiniquitoConfirmado").html("$ " + formatNumber.new(data.resultado[i].TOTALHABER));
                            $('#btnGenerarCheque').attr('data-idDesvinculacion', idDesvinculacion);
                        }
                    }

                    $("#modalConfirmacion").modal("show");
                }
            },
            error: function (xhr) {
                console.log(xhr.responseText);
            },
            complete: function () {

            }
        });
    }

    function ajaxObtenerMontoPago(page, idDesvinculacion) {
        $.ajax({
            type: 'POST',
            url: page + '/GetObtenerMontoPagoPagosService',
            data: '{ idDesvinculacion: "' + idDesvinculacion + '" }',
            dataType: 'json',
            contentType: 'application/json',
            async: true,
            success: function (response) {
                var data = JSON.parse(response.d);
                if (data.resultado.length > 0) {
                    for (var i = 0; i < data.resultado.length; i++) {
                        if (data.resultado[i].VALIDACION == 0) {
                            $("#montoPago").html("$ " + formatNumber.new(data.resultado[i].TOTALHABER));
                            $('#btnGenerarPago').attr('data-idDesvinculacion', idDesvinculacion);
                        }
                    }

                    $("#modalConfirmacion").modal("show");
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
            url: page + '/SetTMPInitProcessChequeFiniquitosService',
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

    function ajaxValidateInitProcess(page) {
        $.ajax({
            type: 'POST',
            url: page + '/GetTMPValidateProcessInitFiniquitosService',
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
                            $("#stopProcess").show();
                        } else {
                            $("#initProcessGeneracionCheque").hide();
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

    function ajaxKnowChequesInProcess(page) {
        $.ajax({
            type: 'POST',
            url: page + '/GetTMPChequesInProcessFiniquitoService',
            data: '{ }',
            dataType: 'json',
            contentType: 'application/json',
            async: true,
            success: function (response) {
                var data = JSON.parse(response.d);
                var content = "";
                var total = 0;
                if (data.resultado.length > 0) {
                    for (var i = 0; i < data.resultado.length; i++) {
                        if (data.resultado[i].VALIDACION == 0) {
                            if (i == 0) {
                                $("#cantidadGeneradoCheque").html(data.resultado.length);
                            }

                            total = total + parseInt(data.resultado[i].MONTO);

                            content = content + '<hr />';
                            content = content + '<p style="font-size: 15px;">';
                            content = content + data.resultado[i].NOMBRETRABAJADOR + '<br />';
                            content = content + 'EMPRESA ' + data.resultado[i].EMPRESA + ' &bull; N° SERIE ' + data.resultado[i].SERIE + ' &bull; <i style="color: #0094ff; font-style: normal;">Monto $ ' + formatNumber.new(data.resultado[i].MONTO) + '</i> <br/>';
                            content = content + '<i style="color: #0094ff; font-style: normal; font-size: 14px;">Generado el ' + formatStringDate(data.resultado[i].FECHA_GENERACION) + '</i>';
                            content = content + '</p>';

                            if ((i + 1) == data.resultado.length) {
                                $("#totalGeneradoCheque").html(formatNumber.new(String(total)));
                            }

                        } else {
                            $("#cantidadGeneradoCheque").html("0");
                            content = content + '<hr />';
                            content = content + '<p style="font-size: 15px; text-align: center;">';
                            content = content + data.resultado[i].RESULTADO;
                            content = content + '</p>';
                        }
                    }

                    $("#listChequesFiniquitos").html(content);

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
            url: page + '/SetTMPCloseProcessChequeFiniquitoService',
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
                        ajaxValidateInitProcess("Inicio.aspx");
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

    function ajaxConfirmados(page, medioPago, filtro, filtrarPor) {
        $.ajax({
            type: 'POST',
            url: page + '/GetFiniquitosConfirmadosService',
            data: '{ medioPago: "' + medioPago + '" }',
            dataType: 'json',
            contentType: 'application/json',
            async: true,
            success: function (response) {
                var data = JSON.parse(response.d);
                var content = "";
                if (data.resultado.length > 0) {
                    for (var i = 0; i < data.resultado.length; i++) {
                        if (data.resultado[i].VALIDACION == 0) {
                            if (filtro == "") {
                                content = content + '<tr>';
                                content = content + '<td>' + data.resultado[i].ID + '</td>';
                                content = content + '<td>' + formatStringDate(data.resultado[i].FECHAHORASOLICITUD) + '</td>';
                                content = content + '<td>' + data.resultado[i].RUTTRABAJADOR + '</td>';
                                content = content + '<td>' + data.resultado[i].FICHATRABAJADOR + '</td>';
                                content = content + '<td>' + data.resultado[i].NOMBRECOMPLETOTRABAJADOR + '</td>';
                                content = content + '<td>' + data.resultado[i].EMPRESATEXTO + '</td>';
                                switch(medioPago){
                                    case "CHEQUE":
                                        content = content + '<td><a href="#" data-iddesvinculacion="' + data.resultado[i].ID + '" class="btn btn-primary eventSelFolio">Seleccionar</a></td>';
                                        break;
                                    case "TRANSFERENCIA":
                                        content = content + '<td><a href="#" data-iddesvinculacion="' + data.resultado[i].ID + '" class="btn btn-primary eventSelFolioTransfNomin">Seleccionar</a></td>';
                                        break;
                                    case "NOMINA":
                                        content = content + '<td><a href="#" data-iddesvinculacion="' + data.resultado[i].ID + '" class="btn btn-primary eventSelFolioTransfNomin">Seleccionar</a></td>';
                                        break;
                                }
                                content = content + '</tr>';
                            } else if (filtro !== "") {
                                switch (filtro) {
                                    case "FOLIO":
                                        if (filtrarPor == data.resultado[i].ID) {
                                            content = content + '<tr>';
                                            content = content + '<td>' + data.resultado[i].ID + '</td>';
                                            content = content + '<td>' + formatStringDate(data.resultado[i].FECHAHORASOLICITUD) + '</td>';
                                            content = content + '<td>' + data.resultado[i].RUTTRABAJADOR + '</td>';
                                            content = content + '<td>' + data.resultado[i].FICHATRABAJADOR + '</td>';
                                            content = content + '<td>' + data.resultado[i].NOMBRECOMPLETOTRABAJADOR + '</td>';
                                            content = content + '<td>' + data.resultado[i].EMPRESATEXTO + '</td>';
                                            switch (medioPago) {
                                                case "CHEQUE":
                                                    content = content + '<td><a href="#" data-iddesvinculacion="' + data.resultado[i].ID + '" class="btn btn-primary eventSelFolio">Seleccionar</a></td>';
                                                    break;
                                                case "TRANSFERENCIA":
                                                    content = content + '<td><a href="#" data-iddesvinculacion="' + data.resultado[i].ID + '" class="btn btn-primary eventSelFolioTransfNomin">Seleccionar</a></td>';
                                                    break;
                                                case "NOMINA":
                                                    content = content + '<td><a href="#" data-iddesvinculacion="' + data.resultado[i].ID + '" class="btn btn-primary eventSelFolioTransfNomin">Seleccionar</a></td>';
                                                    break;
                                            }
                                            content = content + '</tr>';
                                        }
                                        break;
                                    case "RUT":
                                        if (filtrarPor === data.resultado[i].RUTTRABAJADOR) {
                                            content = content + '<tr>';
                                            content = content + '<td>' + data.resultado[i].ID + '</td>';
                                            content = content + '<td>' + formatStringDate(data.resultado[i].FECHAHORASOLICITUD) + '</td>';
                                            content = content + '<td>' + data.resultado[i].RUTTRABAJADOR + '</td>';
                                            content = content + '<td>' + data.resultado[i].FICHATRABAJADOR + '</td>';
                                            content = content + '<td>' + data.resultado[i].NOMBRECOMPLETOTRABAJADOR + '</td>';
                                            content = content + '<td>' + data.resultado[i].EMPRESATEXTO + '</td>';
                                            switch (medioPago) {
                                                case "CHEQUE":
                                                    content = content + '<td><a href="#" data-iddesvinculacion="' + data.resultado[i].ID + '" class="btn btn-primary eventSelFolio">Seleccionar</a></td>';
                                                    break;
                                                case "TRANSFERENCIA":
                                                    content = content + '<td><a href="#" data-iddesvinculacion="' + data.resultado[i].ID + '" class="btn btn-primary eventSelFolioTransfNomin">Seleccionar</a></td>';
                                                    break;
                                                case "NOMINA":
                                                    content = content + '<td><a href="#" data-iddesvinculacion="' + data.resultado[i].ID + '" class="btn btn-primary eventSelFolioTransfNomin">Seleccionar</a></td>';
                                                    break;
                                            }
                                            content = content + '</tr>';
                                        }
                                        break;
                                    case "NOMBRE":
                                        if (filtrarPor === data.resultado[i].NOMBRECOMPLETOTRABAJADOR) {
                                            content = content + '<tr>';
                                            content = content + '<td>' + data.resultado[i].ID + '</td>';
                                            content = content + '<td>' + formatStringDate(data.resultado[i].FECHAHORASOLICITUD) + '</td>';
                                            content = content + '<td>' + data.resultado[i].RUTTRABAJADOR + '</td>';
                                            content = content + '<td>' + data.resultado[i].FICHATRABAJADOR + '</td>';
                                            content = content + '<td>' + data.resultado[i].NOMBRECOMPLETOTRABAJADOR + '</td>';
                                            content = content + '<td>' + data.resultado[i].EMPRESATEXTO + '</td>';
                                            switch (medioPago) {
                                                case "CHEQUE":
                                                    content = content + '<td><a href="#" data-iddesvinculacion="' + data.resultado[i].ID + '" class="btn btn-primary eventSelFolio">Seleccionar</a></td>';
                                                    break;
                                                case "TRANSFERENCIA":
                                                    content = content + '<td><a href="#" data-iddesvinculacion="' + data.resultado[i].ID + '" class="btn btn-primary eventSelFolioTransfNomin">Seleccionar</a></td>';
                                                    break;
                                                case "NOMINA":
                                                    content = content + '<td><a href="#" data-iddesvinculacion="' + data.resultado[i].ID + '" class="btn btn-primary eventSelFolioTransfNomin">Seleccionar</a></td>';
                                                    break;
                                            }
                                            content = content + '</tr>';
                                        }
                                        break;
                                    case "EMPRESA":
                                        if (filtrarPor === data.resultado[i].EMPRESATEXTO) {
                                            content = content + '<tr>';
                                            content = content + '<td>' + data.resultado[i].ID + '</td>';
                                            content = content + '<td>' + formatStringDate(data.resultado[i].FECHAHORASOLICITUD) + '</td>';
                                            content = content + '<td>' + data.resultado[i].RUTTRABAJADOR + '</td>';
                                            content = content + '<td>' + data.resultado[i].FICHATRABAJADOR + '</td>';
                                            content = content + '<td>' + data.resultado[i].NOMBRECOMPLETOTRABAJADOR + '</td>';
                                            content = content + '<td>' + data.resultado[i].EMPRESATEXTO + '</td>';
                                            switch (medioPago) {
                                                case "CHEQUE":
                                                    content = content + '<td><a href="#" data-iddesvinculacion="' + data.resultado[i].ID + '" class="btn btn-primary eventSelFolio">Seleccionar</a></td>';
                                                    break;
                                                case "TRANSFERENCIA":
                                                    content = content + '<td><a href="#" data-iddesvinculacion="' + data.resultado[i].ID + '" class="btn btn-primary eventSelFolioTransfNomin">Seleccionar</a></td>';
                                                    break;
                                                case "NOMINA":
                                                    content = content + '<td><a href="#" data-iddesvinculacion="' + data.resultado[i].ID + '" class="btn btn-primary eventSelFolioTransfNomin">Seleccionar</a></td>';
                                                    break;
                                            }
                                            content = content + '</tr>';
                                        }
                                        break;
                                }
                            }
                        }
                    }

                    switch (medioPago) {
                        case "CHEQUE":
                            $("#tablaConfirmados > tbody").html(content);
                            break;
                        case "TRANSFERENCIA":
                            $("#tablaConfirmadosTransferencia > tbody").html(content);
                            break;
                        case "NOMINA":
                            $("#tablaConfirmadosNomina > tbody").html(content);
                            break;
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

    function ajaxAnularFolio(page, idDesvinculacion){
        $.ajax({
            type: 'POST',
            url: page + '/SetAnularFiniquitoService',
            data: '{ idDesvinculacion: "' + idDesvinculacion + '" }',
            dataType: 'json',
            contentType: 'application/json',
            async: true,
            success: function (response) {
                var data = JSON.parse(response.d);
                if(data.resultado.length > 0){
                    for(var i = 0; i < data.resultado.length; i++){
                        if(data.resultado[i].VALIDACION === '0'){
                            swal({
                                type: 'success',
                                title: 'Folio Anulado',
                                text: 'El folio ha sido anulado exitosamente!'
                            });
                            ajaxRecientes("Inicio.aspx", "", "");
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

    function ajaxAnularPago(page, idDesvinculacion, motivoAnulacion){
        $.ajax({
            type: 'POST',
            url: page + '/SetAnularPagosService',
            data: '{ idDesvinculacion: "' + idDesvinculacion + '", motivoAnulacion: "' + motivoAnulacion + '" }',
            dataType: 'json',
            contentType: 'application/json',
            async: true,
            success: function (response) {
                var data = JSON.parse(response.d);
                if(data.resultado.length > 0){
                    for(var i = 0; i < data.resultado.length; i++){
                        if(data.resultado[i].VALIDACION === '0'){
                            swal({
                                type: 'success',
                                title: 'Pago anulado y cheque',
                                text: data.resultado[i].RESULTADO
                            });
                            ajaxRecientes("Inicio.aspx", "", "");
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

    function ajaxRevertirConfirmacionPago(page, idDesvinculacion){
        $.ajax({
            type: 'POST',
            url: page + '/SetRevertirConfirmacionPagosService',
            data: '{ idDesvinculacion: "' + idDesvinculacion + '" }',
            dataType: 'json',
            contentType: 'application/json',
            async: true,
            success: function (response) {
                var data = JSON.parse(response.d);
                if(data.resultado.length > 0){
                    for(var i = 0; i < data.resultado.length; i++){
                        if(data.resultado[i].VALIDACION === '0'){
                            swal({
                                type: 'success',
                                title: 'Confirmación revertida volvio a estado calculado',
                                text: data.resultado[i].RESULTADO
                            });
                            ajaxRecientes("Inicio.aspx", "", "");
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

    function ajaxNotariadoFiniquito(page, idDesvinculacion){
        $.ajax({
            type: 'POST',
            url: page + '/SetNotariadoPagosService',
            data: '{ idDesvinculacion: "' + idDesvinculacion + '" }',
            dataType: 'json',
            contentType: 'application/json',
            async: true,
            success: function (response) {
                var data = JSON.parse(response.d);
                if(data.resultado.length > 0){
                    for(var i = 0; i < data.resultado.length; i++){
                        if(data.resultado[i].VALIDACION === '0'){
                            swal({
                                type: 'success',
                                title: 'El finiquito ha sido notariado',
                                text: data.resultado[i].RESULTADO
                            });
                            ajaxRecientes("Inicio.aspx", "", "");
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
    
    function ajaxPagarFiniquito(page, idDesvinculacion){
        $.ajax({
            type: 'POST',
            url: page + '/SetFiniquitoPagadoService',
            data: '{ idDesvinculacion: "' + idDesvinculacion + '" }',
            dataType: 'json',
            contentType: 'application/json',
            async: true,
            success: function (response) {
                var data = JSON.parse(response.d);
                if(data.resultado.length > 0){
                    for(var i = 0; i < data.resultado.length; i++){
                        if(data.resultado[i].VALIDACION === '0'){
                            swal({
                                type: 'success',
                                title: 'Finiquito Pagado',
                                text: 'El finiquito ha sido pagado, los contratos asociados pasaron a estado pagado!'
                            });
                            ajaxRecientes("Inicio.aspx", "", "");
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

    function ajaxRecientes(page, filtrarPor, filtro) {
        $("#tablaFiniquitosRecientes > tbody").html("");
        $.ajax({
            type: 'POST',
            url: page + '/GetFiniquitosRecientesService',
            data: '{ filtrarPor: "' + filtrarPor + '", filtro: "' + filtro + '" }',
            dataType: 'json',
            contentType: 'application/json',
            async: true,
            success: function (response) {
                var data = JSON.parse(response.d);
                var content = "";
                var contentH = "";
                if (data.resultado.length > 0) {

                    contentH = contentH + '<table id="tablaFiniquitosRecientes" class="paginations">';
                    contentH = contentH + '<thead>';
                    contentH = contentH + '<tr>';
                    contentH = contentH + '<th>Folio</th>';
                    contentH = contentH + '<th>Fecha y Hora</th>';
                    contentH = contentH + '<th>Usuario</th>';
                    contentH = contentH + '<th style="width: 120px">RUT</th>';
                    contentH = contentH + '<th>Ficha</th>';
                    contentH = contentH + '<th>Nombre</th>';
                    contentH = contentH + '<th>Cargo Mod</th>';
                    contentH = contentH + '<th>Causal</th>';
                    contentH = contentH + '<th>Empresa</th>';
                    contentH = contentH + '<th style="width: 200px;">Estado</th>';
                    contentH = contentH + '<th></th>';
                    contentH = contentH + '</tr>';
                    contentH = contentH + '</thead>';
                    contentH = contentH + '<tbody>';
                    contentH = contentH + '</tbody>';
                    contentH = contentH + '</table>';

                    for (var i = 0; i < data.resultado.length; i++) {
                        if (data.resultado[i].VALIDACION === "0") {

                            content = content + '<tr class="items_' + (i + 1) + '">';
                            content = content + '<td>';
                            switch(data.resultado[i].ESTADO_ACTUAL){
                                case "FINIQUITO NO HA SIDO PAGADO":
                                    content = content + '<div class="row" style="border: 1px solid rgb(200, 200, 200); border-left: 3px solid rgb(91, 192, 222);">';
                                    break;
                                case "FINIQUITO CONFIRMADO PARA PAGO":
                                    content = content + '<div class="row" style="border: 1px solid rgb(200, 200, 200); border-left: 3px solid rgb(240, 173, 78);">';
                                    break;
                                case "FINIQUITO DISPONIBLE PARA PAGO":
                                    content = content + '<div class="row" style="border: 1px solid rgb(200, 200, 200); border-left: 3px solid #428bca;">';
                                    break;
                                case "FINIQUITO HA SIDO PAGADO":
                                    content = content + '<div class="row" style="border: 1px solid rgb(200, 200, 200); border-left: 3px solid rgb(92, 184, 92);">';
                                    break;
                                case "FINIQUITO HA SIDO NOTARIADO":
                                    content = content + '<div class="row" style="border: 1px solid rgb(200, 200, 200); border-left: 3px solid rgb(92, 184, 92);"><span class="glyphicon-tw-1x glyphicon-tw-protegido"></span>';
                                    break;
                                case "ESPERANDO IMPRESIÓN DEL CHEQUE.":
                                    content = content + '<div class="row" style="border: 1px solid rgb(200, 200, 200); border-left: 3px solid rgb(217, 83, 79);">';
                                    break;
                            }
                            content = content + '<div class="col-xs-12 col-sm-12 col-md-2 col-lg-1"  style="min-height: 170px; display: flex; justify-content: center; align-items: center;">';
                            switch(data.resultado[i].ESTADO_ACTUAL){
                                case "FINIQUITO NO HA SIDO PAGADO":
                                    content = content + '<span class="btn btn-info" style="text-align: center;">FOLIO<br />' + data.resultado[i].ID + '</span>';
                                    break;
                                case "FINIQUITO CONFIRMADO PARA PAGO":
                                    content = content + '<span class="btn btn-warning" style="text-align: center;">FOLIO<br />' + data.resultado[i].ID + '</span>';
                                    break;
                                case "FINIQUITO DISPONIBLE PARA PAGO":
                                    content = content + '<span class="btn btn-primary" style="text-align: center;">FOLIO<br />' + data.resultado[i].ID + '</span>';
                                    break;
                                case "FINIQUITO HA SIDO PAGADO":
                                    content = content + '<span class="btn btn-success" style="text-align: center;">FOLIO<br />' + data.resultado[i].ID + '</span>';
                                    break;
                                case "FINIQUITO HA SIDO NOTARIADO":
                                    content = content + '<span class="btn btn-success" style="text-align: center;">FOLIO<br />' + data.resultado[i].ID + '</span>';
                                    break;
                                case "ESPERANDO IMPRESIÓN DEL CHEQUE.":
                                    content = content + '<span class="btn btn-danger" style="text-align: center;">FOLIO<br />' + data.resultado[i].ID + '</span>';
                                    break;
                            }
                            content = content + '</div>';
                            content = content + '<div class="col-xs-12 col-sm-12 col-md-2 col-lg-2" style="min-height: 170px; display: flex; justify-content: center; align-items: center;">';
                            content = content + '<label>';
                            content = content + '<i class="etiq-title">USUARIO CALCULO</i><br/><i class="etiq-data">' + data.resultado[i].USUARIOSOLICITUD + '</i>';
                            content = content + '</label>';
                            content = content + '</div>';
                            content = content + '<div class="col-xs-12 col-sm-12 col-md-3 col-lg-4">';
                            content = content + '<div class="row" style="text-align: left;">';
                            content = content + '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="margin-top: 10px; margin-bottom: -10px;">';
                            content = content + '<label>';
                            content = content + '<i class="etiq-title">EMPRESA </i><i class="etiq-data">' + data.resultado[i].EMPRESA + '</i>';
                            content = content + '</label>';
                            content = content + '</div>';
                            content = content + '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="margin-bottom: -10px;">';
                            content = content + '<label>';
                            content = content + '<i class="etiq-title">EMPLEADO </i><i class="etiq-data">' + data.resultado[i].NOMBRECOMPLETOTRABAJADOR + '</i>';
                            content = content + '</label>';
                            content = content + '</div>';
                            content = content + '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="margin-bottom: -10px;">';
                            content = content + '<label>';
                            content = content + '<i class="etiq-title">RUT </i><i class="etiq-data">' + data.resultado[i].RUTTRABAJADOR  + '</i>';
                            content = content + '<i class="etiq-title"> &bull; FICHA </i><i class="etiq-data">' + data.resultado[i].FICHATRABAJADOR + '</i>';
                            content = content + '</label>';
                            content = content + '</div>';
                            content = content + '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="margin-bottom: -10px;">';
                            content = content + '<label><i class="etiq-title">CARGO MOD </i><i class="etiq-data">' + data.resultado[i].CARGOMOD + '</i></label>';
                            content = content + '</div>';
                            content = content + '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="margin-bottom: -10px;">';
                            content = content + '<label><i class="etiq-title">CAUSAL </i><i class="etiq-data">' + data.resultado[i].CAUSAL + '</i></label>';
                            content = content + '</div>';
                            content = content + '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="margin-bottom: 10px;"><label>';
                            content = content + '<i class="etiq-title">Generado el ' + formatStringDate(data.resultado[i].FECHAHORASOLICITUD) + '</i></label>';
                            content = content + '</div>';
                            content = content + '</div>';
                            content = content + '</div>';
                            content = content + '<div class="col-xs-12 col-sm-12 col-md-3 col-lg-2" style="min-height: 170px; display: flex; justify-content: center; align-items: center;">';
                            content = content + '<label>';
                            content = content + '<i class="etiq-title">MONTO FINIQUITO </i><br/><i class="etiq-data" style="font-size: 20px;">' + data.resultado[i].MONTOFINIQUITO + '</i>';
                            content = content + '</label>';
                            content = content + '</div>';
                            content = content + '<div class="col-xs-12 col-sm-12 col-md-5 col-lg-3" style="min-height: 170px; display: flex; justify-content: center; align-items: center;">';
                            //content = content + '<a href="#" class="btn btn-info" style="margin-left: 5px;"><i class="glyphicon-tw glyphicon-tw-calculo"></i></a>';
                            if(data.resultado[i].OPTION_PAGAR_FOLIO === 'S'){
                                content = content + '<a href="#" class="btn btn-success btn-pagado btnPagarFiniquito" style="margin-left: 5px;" data-iddesvinculacion="' + data.resultado[i].ID + '"><span class="text-pagado"></span> <i class="glyphicon-tw glyphicon-tw-pagar"></i></a>';
                            }
                            if (data.resultado[i].OPTION_CONF_FOLIO === 'S') {
                                content = content + '<a href="#" class="btn btn-warning btn-confPago btnConfirmarPago" style="margin-left: 5px;" data-iddesvinculacion="' + data.resultado[i].ID + '"><span class="text-confPago"></span> <i class="glyphicon-tw glyphicon-tw-megusta"></i></a>';
                            }
                            content = content + '<a href="#" class="btn btn-primary btn-visualizar btnVisualizarCalculo" style="margin-left: 5px;" data-iddesvinculacion="' + data.resultado[i].ID + '"><span class="text-visualizar"></span> <i class="glyphicon-tw glyphicon-tw-calculor"></i></a>';
                            if(data.resultado[i].OPTION_ANULAR_FOLIO === 'S'){
                                content = content + '<a href="#" class="btn btn-danger btn-anular btnAnularCalculo" style="margin-left: 5px;" data-iddesvinculacion="' + data.resultado[i].ID + '"><span class="text-anular"></span> <i class="glyphicon-tw glyphicon-tw-anular"></i></a>';
                            }
                            if(data.resultado[i].OPTION_ANULAR_PAGO === 'S'){
                                content = content + '<a href="#" class="btn btn-danger btn-anularP btnAnularPago" style="margin-left: 5px;" data-iddesvinculacion="' + data.resultado[i].ID + '"><span class="text-anularP"></span> <i class="glyphicon-tw glyphicon-tw-anular"></i></a>';
                            }
                            
                            if(data.resultado[i].OPTION_NOTARIADO === 'S'){
                                content = content + '<a href="#" class="btn btn-success btn-notariado btnNotariado" style="margin-left: 5px;" data-iddesvinculacion="' + data.resultado[i].ID + '"><span class="text-notariado"></span> <i class="glyphicon-tw glyphicon-tw-notariado"></i></a>';
                            }
                            //content = content + '<a href="#" class="btn btn-danger btn-anotacion " style="margin-left: 5px;" data-iddesvinculacion="' + data.resultado[i].ID + '"><span class="text-anotacion"></span> <i class="glyphicon-tw glyphicon-tw-anular"></i></a>';
                            
                            if(data.resultado[i].OPTION_REVERTIRCONF === "S"){
                                content = content + '<a href="#" class="btn btn-warning btn-revertirConf btnRevertirConfirmacion" style="margin-left: 5px;" data-iddesvinculacion="' + data.resultado[i].ID + '"><span class="text-revertirConf"></span> <i class="glyphicon-tw glyphicon-tw-revertirConf"></i></a>';
                            }
                            content = content + '</div>';
                            content = content + '</div>';
                            content = content + '</td>';
                            content = content + '</tr>';

                            if((i + 1) == data.resultado.length){
                                var filtroHabilitado = "N";
                                if (filtrarPor !== "") {
                                    filtroHabilitado = "S";
                                }

                                createAnalytics(data.resultado.length, filtroHabilitado, filtrarPor, filtro);
                            }
                        }
                    }

                    $("#tablaFiniquitosRecientes > thead").html(contentH);
                    $("#tablaFiniquitosRecientes > tbody").html(content);

                    $("#loadingFiniquitos").html("");
                }
                else
                {
                    createAnalytics(0, "N", filtrarPor, filtro);
                    $("#tablaFiniquitosRecientes > thead").html("");
                    $(".paginationRecientes").html("");
                    $("#loadingFiniquitos").html('<div class="btn" style="margin-top: 30px;"><i class="glyphicon-tw-lg glyphicon-tw-errorFace"></i><h3>Lo sentimos no se han encontrado resultados</h3></div>');
                }
            },
            error: function (xhr) {
                console.log(xhr.responseText);
            },
            complete: function () {
                pagination(".paginationRecientes");

            }
        });
    }

    

    function createAnalytics(resultados, filtroHabilitado, filtrarPor, filtro) {
        content = "";

        /** ADAPTATIVE CONTENT */

        content = content + '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="text-align: left;">';
        content = content + '<div class="row">';
        content = content + '<div class="col-xs-12 col-sm-12 col-md-6 col-lg-6" style="text-align: left; margin-bottom: -10px;">';
        content = content + '<label><i class="etiq-analytics">Se han encontrado ';
        if (resultados > 1 || resultados == 0) {
            content = content + String(resultados) + ' resultados';
        } else {
            content = content + String(resultados) + ' resultado';
        }
        content = content + '</i></label>';
        content = content + '</div>';
        if (filtroHabilitado === "S") {
            content = content + '<div class="col-xs-12 col-sm-12 col-md-23 col-lg-23">';
            switch (filtrarPor) {
                case "NOMBRE":
                    content = content + '<label><i class="etiq-analytics">Filtrado: </i><i class="etiq-data-analytics">NOMBRE : ' + filtro + '</i></label>';
                    break;
                case "FOLIO":
                    content = content + '<label><i class="etiq-analytics">Filtrado: </i><i class="etiq-data-analytics">FOLIO N° : ' + filtro + '</i></label>';
                    break;
                case "RUT":
                    content = content + '<label><i class="etiq-analytics">Filtrado: </i><i class="etiq-data-analytics">RUT : ' + filtro + '</i></label>';
                    break;
                case "EMPRESA":
                    content = content + '<label><i class="etiq-analytics">Filtrado: </i><i class="etiq-data-analytics">EMPRESA : ' + filtro + '</i></label>';
                    break;
                case "PARTTIME":
                    content = content + '<label><i class="etiq-analytics">Filtrado: </i><i class="etiq-data-analytics">PART TIME</i></label>';
                    break;
                case "FULLTIME":
                    content = content + '<label><i class="etiq-analytics">Filtrado: </i><i class="etiq-data-analytics">FULL TIME</i></label>';
                    break;
            }
            content = content + '</div>';
        }
        content = content + '</div>';
        content = content + '</div>';

        /** END ADAPTATIVE CONTENT */

        $("#analytics").html(content);
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