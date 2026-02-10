$(document).ready(function () {

    var idDesvinculacion = sessionStorage.getItem("applicationIdDesvinculacionCalculo");

    $("#content-loader").loader('init');

    setTimeout(function () {
        ajaxVisualizarDatosTrabajador("VisualizacionCalculoFiniquito.aspx", idDesvinculacion);
        ajaxVisualizarDocumentos("VisualizacionCalculoFiniquito.aspx", idDesvinculacion);
        ajaxVisualizaContratos("VisualizacionCalculoFiniquito.aspx", idDesvinculacion);
        ajaxVisualizarTotalDias("VisualizacionCalculoFiniquito.aspx", idDesvinculacion);
        ajaxVisualizarPeriodosRemuneraciones("VisualizacionCalculoFiniquito.aspx", idDesvinculacion);
        ajaxVisualizarOtrosHaberesFinquito("VisualizacionCalculoFiniquito.aspx", idDesvinculacion);
        ajaxVisualizarDescuentosFiniquito("VisualizacionCalculoFiniquito.aspx", idDesvinculacion);
        ajaxVisualizarDiasVacaciones("VisualizacionCalculoFiniquito.aspx", idDesvinculacion);
        ajaxVisualizarTotalesFiniquito("VisualizacionCalculoFiniquito.aspx", idDesvinculacion);
        ajaxFiniquitoConfirmado("VisualizacionCalculoFiniquito.aspx", idDesvinculacion);
        ajaxConceptosAdicionales("VisualizacionCalculoFiniquito.aspx", idDesvinculacion);
        ajaxBonosAdicionales("VisualizacionCalculoFiniquito.aspx", idDesvinculacion);
        ajaxHaberesMensual("VisualizacionCalculoFiniquito.aspx", idDesvinculacion);
        ajaxValorUF("VisualizacionCalculoFiniquito.aspx", idDesvinculacion);
        $("#content-loader").loader('stop');
    }, 500);

    $("#btnObtenerDocumentoFiniquito").on("click", function (e) {
        e.preventDefault();
        $("#modalDocumentoFiniquito").modal("show");
    });

    $(document).on("click", "#btnConfirmarRecepcion", function (e) {
        e.preventDefault();
        $("#modalConfirmarRecepcion").modal("show");
    });

    $("#btnPagoCheque").on("click", function () {
        var idDesvinculacion = $("#btnConfirmarRecepcion").attr("data-iddesvinculacion");
        swal({
            title: 'Opción Finiquito Part Time',
            text: "¿Desea cambiar el monto total del finiquito?",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Sí, Cambiar Monto!',
            cancelButtonText: 'No, Mantener Monto',
            closeOnConfirm: false,
            closeOnCancel: false
        }).then((result) => {
            if (result) {
                swal({
                    input: 'text',
                    title: 'Nuevo Monto',
                    text: "Especifique el nuevo monto total para el finiquito!",
                    type: 'warning',
                    confirmButtonText: 'Siguiente',
                    cancelButtonText: 'Cancelar',
                    showCancelButton: true,
                    closeOnConfirm: false,
                    closeOnCancel: false
                }).then((result) => {
                    if (result != "") {
                        swal({
                            input: 'textarea',
                            html: "<h3>Monto Asignado <br/> $ <i id='montoConfirmacionFiniquitoNuevo'>" + result + "</i></h3><br/><label>Observación del cambio del monto total!</label>",
                            type: 'warning',
                            confirmButtonText: 'Guardar',
                            cancelButtonText: 'Cancelar',
                            showCancelButton: true,
                        }).then((result) => {
                            if (result != "") {
                                 ajaxConfirmarRetiroFiniquito("VisualizacionCalculoFiniquito.aspx", idDesvinculacion, "CHEQUE", $("#montoConfirmacionFiniquitoNuevo").html().split(".").join(""), result);
                                $("#modalConfirmarRecepcion").modal("hide");
                            }
                        }); 
                    }
                });
            }
        }, function(dismiss){
            if(dismiss === 'cancel'){
               ajaxConfirmarRetiroFiniquito("VisualizacionCalculoFiniquito.aspx", idDesvinculacion, "CHEQUE", "", "");
            }
            
        });
    });

    $("#btnPagoTransf").on("click", function () {
        var idDesvinculacion = $("#btnConfirmarRecepcion").attr("data-iddesvinculacion");
        swal({
            title: 'Opción Finiquito Part Time',
            text: "¿Desea cambiar el monto total del finiquito?",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Sí, Cambiar Monto!',
            cancelButtonText: 'No, Mantener Monto',
            closeOnConfirm: false,
            closeOnCancel: false
        }).then((result) => {
            if (result) {
                swal({
                    input: 'text',
                    title: 'Nuevo Monto',
                    text: "Especifique el nuevo monto total para el finiquito!",
                    type: 'warning',
                    confirmButtonText: 'Siguiente',
                    cancelButtonText: 'Cancelar',
                    showCancelButton: true,
                    closeOnConfirm: false,
                    closeOnCancel: false
                }).then((result) => {
                    if (result != "") {
                        swal({
                            input: 'textarea',
                            html: "<h3>Monto Asignado <br/> $ <i id='montoConfirmacionFiniquitoNuevo'>" + result + "</i></h3><br/><label>Observación del cambio del monto total!</label>",
                            type: 'warning',
                            confirmButtonText: 'Guardar',
                            cancelButtonText: 'Cancelar',
                            showCancelButton: true,
                        }).then((result) => {
                            if (result != "") {
                                    ajaxConfirmarRetiroFiniquito("VisualizacionCalculoFiniquito.aspx", idDesvinculacion, "CHEQUE", $("#montoConfirmacionFiniquitoNuevo").html().split(".").join(""), result);
                                $("#modalConfirmarRecepcion").modal("hide");
                            }
                        }); 
                    }
                });
            }
        }, function(dismiss){
            if(dismiss === 'cancel'){
                ajaxConfirmarRetiroFiniquito("VisualizacionCalculoFiniquito.aspx", idDesvinculacion, "TRANSFERENCIA", "", "");
            }
            
        });
    });

    $("#btnPagoNomina").on("click", function () {
        var idDesvinculacion = $("#btnConfirmarRecepcion").attr("data-iddesvinculacion");
        swal({
            title: 'Opción Finiquito Part Time',
            text: "¿Desea cambiar el monto total del finiquito?",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Sí, Cambiar Monto!',
            cancelButtonText: 'No, Mantener Monto',
            closeOnConfirm: false,
            closeOnCancel: false
        }).then((result) => {
            if (result) {
                swal({
                    input: 'text',
                    title: 'Nuevo Monto',
                    text: "Especifique el nuevo monto total para el finiquito!",
                    type: 'warning',
                    confirmButtonText: 'Siguiente',
                    cancelButtonText: 'Cancelar',
                    showCancelButton: true,
                    closeOnConfirm: false,
                    closeOnCancel: false
                }).then((result) => {
                    if (result != "") {
                        swal({
                            input: 'textarea',
                            html: "<h3>Monto Asignado <br/> $ <i id='montoConfirmacionFiniquitoNuevo'>" + result + "</i></h3><br/><label>Observación del cambio del monto total!</label>",
                            type: 'warning',
                            confirmButtonText: 'Guardar',
                            cancelButtonText: 'Cancelar',
                            showCancelButton: true,
                        }).then((result) => {
                            if (result != "") {
                                ajaxConfirmarRetiroFiniquito("VisualizacionCalculoFiniquito.aspx", idDesvinculacion, "NOMINA", $("#montoConfirmacionFiniquitoNuevo").html().split(".").join(""), result);
                                $("#modalConfirmarRecepcion").modal("hide");
                            }
                        }); 
                    }
                });
            }
        }, function(dismiss){
            if(dismiss === 'cancel'){
                ajaxConfirmarRetiroFiniquito("VisualizacionCalculoFiniquito.aspx", idDesvinculacion, "NOMINA", "", "");
            }
            
        });
    });

    $("#generarPDF").on("click", function (e) {
        e.preventDefault();
        genPDF();
    });
    //

    function genPDF() {
        $(".table-scroll").addClass("table-scroll2");
        html2canvas(document.getElementById("container"), {
            onrendered: function (canvas) {
                var img = canvas.toDataURL('image/jpg', 1.0);
                var doc = new jsPDF({
                    unit: 'px',
                    format: 'A4',
                });
                doc.addImage(img, 'png', 10, 16, 432, 590);
                doc.save('Documento-CALCULO.pdf');
                $(".table-scroll").removeClass("table-scroll2");
            },
        });
    }

    function ajaxVisualizarDatosTrabajador(page, idDesvinculacion) {
        $.ajax({
            type: 'POST',
            url: page + '/GetObtenerPagosPorFiltroPagosService',
            data: '{ idDesvinculacion: "' + idDesvinculacion + '" }',
            dataType: 'json',
            contentType: 'application/json',
            async: false,
            success: function (response) {
                var data = JSON.parse(response.d);
                var content = "";
                if (data.resultado.length > 0) {

                    for (var i = 0; i < data.resultado.length; i++) {
                        if (data.resultado[i].VALIDACION === "0") {
                            $("#numeroFolio").html("Folio: " + idDesvinculacion);
                            $("#empresa").val(data.resultado[i].EMPRESA);
                            $("#rutTrabajador").val(data.resultado[i].RUTTRABAJADOR);
                            $("#nombreTrabajador").val(data.resultado[i].NOMBRECOMPLETOTRABAJADOR);
                            $("#fechaAvisoDesvinculacion").val(data.resultado[i].FECHAAVISODESVINCULACION.split("T")[0]);
                            $("#contratoActivo").val(data.resultado[i].FICHATRABAJADOR);

                            $("#causal").val(data.resultado[i].CAUSAL);
                            $("#zonaExtrema").val(data.resultado[i].ZONAEXTREMA);
                            $("#partTime").val(data.resultado[i].PARTTIME);

                            $("#usuarioSolicitud").html('<i class="glyphicon-tw glyphicon-tw-user"></i> ' + data.resultado[i].USUARIOSOLICITUD);
                            $("#fechaSolicitud").html('<i class="glyphicon-tw glyphicon-tw-calendar"></i> ' + formatStringDate(data.resultado[i].FECHAHORASOLICITUD));

                            ajaxCargo("VisualizacionCalculoFiniquito.aspx", data.resultado[i].FICHATRABAJADOR, data.resultado[i].EMPRESA);
                            ajaxCentroCosto("VisualizacionCalculoFiniquito.aspx", data.resultado[i].FICHATRABAJADOR, data.resultado[i].EMPRESA);

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

    function ajaxVisualizaContratos(page, idDesvinculacion) {
        $.ajax({
            type: 'POST',
            url: page + '/GetVisualizarContratosCalculoService',
            data: '{ idDesvinculacion: "' + idDesvinculacion + '" }',
            dataType: 'json',
            contentType: 'application/json',
            async: false,
            success: function (response) {
                var data = JSON.parse(response.d);
                var content = "";
                if (data.resultado.length > 0) {
                    content = content + '<table id="tablaContratosCalculo" style="display: none;">';
                    content = content + '<thead>';
                    content = content + '<tr>';
                    content = content + '<th>Ficha o Contrato</th>';
                    content = content + '<th>Inicio de Contrato</th>';
                    content = content + '<th>Termino de Contrato</th>';
                    content = content + '<th>Días</th>';
                    content = content + '<th>Causal</th>';
                    content = content + '<th>P.D.</th>';
                    content = content + '<th>Contratos</th>';
                    content = content + '<th>Seguimiento</th>';
                    content = content + '</tr>';
                    content = content + '</thead>';
                    content = content + '<tbody>';
                    content = content + '</tbody>';
                    content = content + '</table>';

                    $(".tablaContratosCalculo").html(content);

                    content = "";

                    for (var i = 0; i < data.resultado.length; i++) {
                        if (data.resultado[i].VALIDACION === "0") {
                            content = content + '<tr>';
                            content = content + '<td>' + data.resultado[i].FICHA + '</td>';
                            content = content + '<td>' + data.resultado[i].FECHAINICIO.split("T")[0] + '</td>';
                            content = content + '<td>' + data.resultado[i].FECHAFINAL.split("T")[0] + '</td>';
                            content = content + '<td>' + data.resultado[i].DIAS + '</td>';
                            content = content + '<td>' + data.resultado[i].CAUSAL + '</td>';
                            content = content + '<td>' + data.resultado[i].PD + '</td>';
                            content = content + '<td>' + data.resultado[i].CONTRATOS + '</td>';
                            content = content + '<td>' + data.resultado[i].PAGADO + '</td>';
                            content = content + '</tr>';
                        }
                    }

                    $("#tablaContratosCalculo > tbody").html(content);
                    $("#tablaContratosCalculo").show();
                } 
            },
            error: function (xhr) {
                console.log(xhr.responseText);
            },
            complete: function () {
            }
        });
    }

    function ajaxVisualizarDocumentos(page, idDesvinculacion) {
        $.ajax({
            type: 'POST',
            url: page + '/GetVisualizarDocumentosCalculoService',
            data: '{ idDesvinculacion: "' + idDesvinculacion + '" }',
            dataType: 'json',
            contentType: 'application/json',
            async: false,
            success: function (response) {
                var data = JSON.parse(response.d);
                var content = "";
                if (data.resultado.length > 0) {
                    content = content + '<table id="tablaDocumentosCalculo" style="display: none;">';
                    content = content + '<thead>';
                    content = content + '<tr>';
                    content = content + '<th>Documento</th>';
                    content = content + '<th>Seleccionado</th>';
                    content = content + '</tr>';
                    content = content + '</thead>';
                    content = content + '<tbody>';
                    content = content + '</tbody>';
                    content = content + '</table>';

                    $(".tablaDocumentosCalculo").html(content);

                    content = "";

                    for (var i = 0; i < data.resultado.length; i++) {
                        if (data.resultado[i].VALIDACION === "0") {
                            content = content + '<tr>';
                            content = content + '<td>' + data.resultado[i].NOMBRE_DOCUMENTO + '</td>';
                            content = content + '<td>' + data.resultado[i].SELECCIONADO + '</td>';
                            content = content + '</tr>';
                        }
                    }

                    $("#tablaDocumentosCalculo > tbody").html(content);
                    $("#tablaDocumentosCalculo").show();

                }
            },
            error: function (xhr) {
                console.log(xhr.responseText);
            },
            complete: function () {
            }
        });
    }

    function ajaxVisualizarTotalDias(page, idDesvinculacion) {
        $.ajax({
            type: 'POST',
            url: page + '/GetVisualizarTotalDiasCalculoCalculoService',
            data: '{ idDesvinculacion: "' + idDesvinculacion + '" }',
            dataType: 'json',
            contentType: 'application/json',
            async: false,
            success: function (response) {
                var data = JSON.parse(response.d);
                var content = "";
                if (data.resultado.length > 0) {

                    for (var i = 0; i < data.resultado.length; i++) {
                        if (data.resultado[i].VALIDACION === "0") {
                            switch(data.resultado[i].EMPRESA){
                                case "TWEST":
                                    $("#totalDias").val(data.resultado[i].TOTALDIAS);
                                    break;
                                case "TEAMRRHH":
                                    $("#contentTotalDias").hide();
                                    $("#separateTtotalDias").hide();
                                    break;
                            }
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

    function ajaxVisualizarPeriodosRemuneraciones(page, idDesvinculacion) {
        $.ajax({
            type: 'POST',
            url: page + '/GetVisualizarPeriodosCalculoService',
            data: '{ idDesvinculacion: "' + idDesvinculacion + '" }',
            dataType: 'json',
            contentType: 'application/json',
            async: false,
            success: function (response) {
                var data = JSON.parse(response.d);
                var content = "";
                var nPeriodos = 0;
                var arrayRemuneraciones = [];
                if (data.resultado.length > 0) {

                    content = content + '<table id="tablaPeriodosRemuneraciones" style="display: none;">';
                    content = content + '<thead>';
                    content = content + '</thead>';
                    content = content + '<tbody>';
                    content = content + '</tbody>';
                    content = content + '</table>';

                    $(".tablaPeriodosRemuneraciones").html(content);

                    for (var i = 0; i < data.resultado.length; i++) {
                        if (data.resultado[i].VALIDACION === "0") {
                            if(i == 0){
                                nPeriodos = parseInt(data.resultado[i].NPERIODOS);
                                content = content + '<tr>';
                                content = content + '<th>' + data.resultado[i].HABER + '</th>';

                                switch(nPeriodos){
                                    case 1:
                                        content = content + '<th>' + data.resultado[i].PERIODO1 + '</th>';
                                        break;
                                    case 2:
                                        content = content + '<th>' + data.resultado[i].PERIODO1 + '</th>';
                                        content = content + '<th>' + data.resultado[i].PERIODO2 + '</th>';
                                        break;
                                    case 3:
                                        content = content + '<th>' + data.resultado[i].PERIODO1 + '</th>';
                                        content = content + '<th>' + data.resultado[i].PERIODO2 + '</th>';
                                        content = content + '<th>' + data.resultado[i].PERIODO3 + '</th>';
                                        break;
                                    case 4:
                                        content = content + '<th>' + data.resultado[i].PERIODO1 + '</th>';
                                        content = content + '<th>' + data.resultado[i].PERIODO2 + '</th>';
                                        content = content + '<th>' + data.resultado[i].PERIODO3 + '</th>';
                                        content = content + '<th>' + data.resultado[i].PERIODO4 + '</th>';
                                        break;
                                    case 5:
                                        content = content + '<th>' + data.resultado[i].PERIODO1 + '</th>';
                                        content = content + '<th>' + data.resultado[i].PERIODO2 + '</th>';
                                        content = content + '<th>' + data.resultado[i].PERIODO3 + '</th>';
                                        content = content + '<th>' + data.resultado[i].PERIODO4 + '</th>';
                                        content = content + '<th>' + data.resultado[i].PERIODO5 + '</th>';
                                        break;
                                    case 6:
                                        content = content + '<th>' + data.resultado[i].PERIODO1 + '</th>';
                                        content = content + '<th>' + data.resultado[i].PERIODO2 + '</th>';
                                        content = content + '<th>' + data.resultado[i].PERIODO3 + '</th>';
                                        content = content + '<th>' + data.resultado[i].PERIODO4 + '</th>';
                                        content = content + '<th>' + data.resultado[i].PERIODO5 + '</th>';
                                        content = content + '<th>' + data.resultado[i].PERIODO6 + '</th>';
                                        break;
                                }

                                content = content + '</tr>';

                                $("#tablaPeriodosRemuneraciones > thead").html(content);

                                content = "";

                            }

                            if (i > 0) {
                                content = content + '<tr>';
                                content = content + '<td>' + data.resultado[i].HABER + '</td>';
                                switch(nPeriodos){
                                    case 1:
                                        if(data.resultado[i].HABER !== "DIAS TRABAJADOS"){
                                            if (data.resultado[i].PERIODO1 !== "") {
                                                content = content + '<td>' + formatNumber.new(data.resultado[i].PERIODO1, "$") + '</td>';
                                            } else {
                                                content = content + '<td>' + data.resultado[i].PERIODO1 + '</td>';
                                            }
                                        } else {
                                            content = content + '<td>' + data.resultado[i].PERIODO1 + '</td>';
                                        }
                                        break;
                                    case 2:
                                        if(data.resultado[i].HABER !== "DIAS TRABAJADOS"){
                                            if (data.resultado[i].PERIODO1 !== "") {
                                                content = content + '<td>' + formatNumber.new(data.resultado[i].PERIODO1, "$") + '</td>';
                                            } else {
                                                content = content + '<td>' + data.resultado[i].PERIODO1 + '</td>';
                                            }
                                            if (data.resultado[i].PERIODO2 !== "") {
                                                content = content + '<td>' + formatNumber.new(data.resultado[i].PERIODO2, "$") + '</td>';
                                            } else {
                                                content = content + '<td>' + data.resultado[i].PERIODO2 + '</td>';
                                            }
                                        } else {
                                            content = content + '<td>' + data.resultado[i].PERIODO1 + '</td>';
                                            content = content + '<td>' + data.resultado[i].PERIODO2 + '</td>';
                                        }
                                        break;
                                    case 3:
                                        if(data.resultado[i].HABER !== "DIAS TRABAJADOS"){
                                            if (data.resultado[i].PERIODO1 !== "") {
                                                content = content + '<td>' + formatNumber.new(data.resultado[i].PERIODO1, "$") + '</td>';
                                            } else {
                                                content = content + '<td>' + data.resultado[i].PERIODO1 + '</td>';
                                            }
                                            if (data.resultado[i].PERIODO2 !== "") {
                                                content = content + '<td>' + formatNumber.new(data.resultado[i].PERIODO2, "$") + '</td>';
                                            } else {
                                                content = content + '<td>' + data.resultado[i].PERIODO2 + '</td>';
                                            }
                                            if (data.resultado[i].PERIODO3 !== "") {
                                                content = content + '<td>' + formatNumber.new(data.resultado[i].PERIODO3, "$") + '</td>';
                                            } else {
                                                content = content + '<td>' + data.resultado[i].PERIODO3 + '</td>';
                                            }
                                        } else {
                                            content = content + '<td>' + data.resultado[i].PERIODO1 + '</td>';
                                            content = content + '<td>' + data.resultado[i].PERIODO2 + '</td>';
                                            content = content + '<td>' + data.resultado[i].PERIODO3 + '</td>';
                                        }
                                        break;
                                    case 4:
                                        if(data.resultado[i].HABER !== "DIAS TRABAJADOS"){
                                            if (data.resultado[i].PERIODO1 !== "") {
                                                content = content + '<td>' + formatNumber.new(data.resultado[i].PERIODO1, "$") + '</td>';
                                            } else {
                                                content = content + '<td>' + data.resultado[i].PERIODO1 + '</td>';
                                            }
                                            if (data.resultado[i].PERIODO2 !== "") {
                                                content = content + '<td>' + formatNumber.new(data.resultado[i].PERIODO2, "$") + '</td>';
                                            } else {
                                                content = content + '<td>' + data.resultado[i].PERIODO2 + '</td>';
                                            }
                                            if (data.resultado[i].PERIODO3 !== "") {
                                                content = content + '<td>' + formatNumber.new(data.resultado[i].PERIODO3, "$") + '</td>';
                                            } else {
                                                content = content + '<td>' + data.resultado[i].PERIODO3 + '</td>';
                                            }
                                            if (data.resultado[i].PERIODO4 !== "") {
                                                content = content + '<td>' + formatNumber.new(data.resultado[i].PERIODO4, "$") + '</td>';
                                            } else {
                                                content = content + '<td>' + data.resultado[i].PERIODO4 + '</td>';
                                            }
                                        } else {
                                            content = content + '<td>' + data.resultado[i].PERIODO1 + '</td>';
                                            content = content + '<td>' + data.resultado[i].PERIODO2 + '</td>';
                                            content = content + '<td>' + data.resultado[i].PERIODO3 + '</td>';
                                            content = content + '<td>' + data.resultado[i].PERIODO4 + '</td>';
                                        }
                                        break;
                                    case 5:
                                        if(data.resultado[i].HABER !== "DIAS TRABAJADOS"){
                                            if (data.resultado[i].PERIODO1 !== "") {
                                                content = content + '<td>' + formatNumber.new(data.resultado[i].PERIODO1, "$") + '</td>';
                                            } else {
                                                content = content + '<td>' + data.resultado[i].PERIODO1 + '</td>';
                                            }
                                            if (data.resultado[i].PERIODO2 !== "") {
                                                content = content + '<td>' + formatNumber.new(data.resultado[i].PERIODO2, "$") + '</td>';
                                            } else {
                                                content = content + '<td>' + data.resultado[i].PERIODO2 + '</td>';
                                            }
                                            if (data.resultado[i].PERIODO3 !== "") {
                                                content = content + '<td>' + formatNumber.new(data.resultado[i].PERIODO3, "$") + '</td>';
                                            } else {
                                                content = content + '<td>' + data.resultado[i].PERIODO3 + '</td>';
                                            }
                                            if (data.resultado[i].PERIODO4 !== "") {
                                                content = content + '<td>' + formatNumber.new(data.resultado[i].PERIODO4, "$") + '</td>';
                                            } else {
                                                content = content + '<td>' + data.resultado[i].PERIODO4 + '</td>';
                                            }
                                            if (data.resultado[i].PERIODO5 !== "") {
                                                content = content + '<td>' + formatNumber.new(data.resultado[i].PERIODO5, "$") + '</td>';
                                            } else {
                                                content = content + '<td>' + data.resultado[i].PERIODO5 + '</td>';
                                            }
                                        } else {
                                            content = content + '<td>' + data.resultado[i].PERIODO1 + '</td>';
                                            content = content + '<td>' + data.resultado[i].PERIODO2 + '</td>';
                                            content = content + '<td>' + data.resultado[i].PERIODO3 + '</td>';
                                            content = content + '<td>' + data.resultado[i].PERIODO4 + '</td>';
                                            content = content + '<td>' + data.resultado[i].PERIODO5 + '</td>';
                                        }
                                        break;
                                    case 6:
                                        if(data.resultado[i].HABER !== "DIAS TRABAJADOS"){
                                            if (data.resultado[i].PERIODO1 !== "") {
                                                content = content + '<td>' + formatNumber.new(data.resultado[i].PERIODO1, "$") + '</td>';
                                            } else {
                                                content = content + '<td>' + data.resultado[i].PERIODO1 + '</td>';
                                            }
                                            if (data.resultado[i].PERIODO2 !== "") {
                                                content = content + '<td>' + formatNumber.new(data.resultado[i].PERIODO2, "$") + '</td>';
                                            } else {
                                                content = content + '<td>' + data.resultado[i].PERIODO2 + '</td>';
                                            }
                                            if (data.resultado[i].PERIODO3 !== "") {
                                                content = content + '<td>' + formatNumber.new(data.resultado[i].PERIODO3, "$") + '</td>';
                                            } else {
                                                content = content + '<td>' + data.resultado[i].PERIODO3 + '</td>';
                                            }
                                            if (data.resultado[i].PERIODO4 !== "") {
                                                content = content + '<td>' + formatNumber.new(data.resultado[i].PERIODO4, "$") + '</td>';
                                            } else {
                                                content = content + '<td>' + data.resultado[i].PERIODO4 + '</td>';
                                            }
                                            if (data.resultado[i].PERIODO5 !== "") {
                                                content = content + '<td>' + formatNumber.new(data.resultado[i].PERIODO5, "$") + '</td>';
                                            } else {
                                                content = content + '<td>' + data.resultado[i].PERIODO5 + '</td>';
                                            }
                                            if (data.resultado[i].PERIODO6 !== "") {
                                                content = content + '<td>' + formatNumber.new(data.resultado[i].PERIODO6, "$") + '</td>';
                                            } else {
                                                content = content + '<td>' + data.resultado[i].PERIODO6 + '</td>';
                                            }
                                        } else {
                                            content = content + '<td>' + data.resultado[i].PERIODO1 + '</td>';
                                            content = content + '<td>' + data.resultado[i].PERIODO2 + '</td>';
                                            content = content + '<td>' + data.resultado[i].PERIODO3 + '</td>';
                                            content = content + '<td>' + data.resultado[i].PERIODO4 + '</td>';
                                            content = content + '<td>' + data.resultado[i].PERIODO5 + '</td>';
                                            content = content + '<td>' + data.resultado[i].PERIODO6 + '</td>';
                                        }
                                        break;
                                }
                                    
                                content = content + '</tr>';

                                /** LLENADO ARREGLO REMUNERACIONES */

                                if ((i + 1) == data.resultado.length) {
                                    switch(nPeriodos){
                                        case 1:
                                            arrayRemuneraciones.push(data.resultado[i].PERIODO1);
                                            break;
                                        case 2:
                                            arrayRemuneraciones.push(data.resultado[i].PERIODO1);
                                            arrayRemuneraciones.push(data.resultado[i].PERIODO2);
                                            break;
                                        case 3:
                                            arrayRemuneraciones.push(data.resultado[i].PERIODO1);
                                            arrayRemuneraciones.push(data.resultado[i].PERIODO2);
                                            arrayRemuneraciones.push(data.resultado[i].PERIODO3);
                                            break;
                                        case 4:
                                            arrayRemuneraciones.push(data.resultado[i].PERIODO1);
                                            arrayRemuneraciones.push(data.resultado[i].PERIODO2);
                                            arrayRemuneraciones.push(data.resultado[i].PERIODO3);
                                            arrayRemuneraciones.push(data.resultado[i].PERIODO4);
                                            break;
                                        case 5:
                                            arrayRemuneraciones.push(data.resultado[i].PERIODO1);
                                            arrayRemuneraciones.push(data.resultado[i].PERIODO2);
                                            arrayRemuneraciones.push(data.resultado[i].PERIODO3);
                                            arrayRemuneraciones.push(data.resultado[i].PERIODO4);
                                            arrayRemuneraciones.push(data.resultado[i].PERIODO5);
                                            break;
                                        case 6:
                                            arrayRemuneraciones.push(data.resultado[i].PERIODO1);
                                            arrayRemuneraciones.push(data.resultado[i].PERIODO2);
                                            arrayRemuneraciones.push(data.resultado[i].PERIODO3);
                                            arrayRemuneraciones.push(data.resultado[i].PERIODO4);
                                            arrayRemuneraciones.push(data.resultado[i].PERIODO5);
                                            arrayRemuneraciones.push(data.resultado[i].PERIODO6);
                                            break;
                                    }

                                    ajaxVisualizarPromedioCalculo(arrayRemuneraciones, $("#totalDias").val(), data.resultado[i].EMPRESA);
                                }
                            }
                        }
                    }

                    $("#tablaPeriodosRemuneraciones > tbody").html(content);
                    $("#tablaPeriodosRemuneraciones").show();

                }
            },
            error: function (xhr) {
                console.log(xhr.responseText);
            },
            complete: function () {
            }
        });
    }

    function ajaxVisualizarPromedioCalculo(remuneraciones, dias, empresa) {
        switch(empresa){
            case "TWEST":
                var content = "";
                var sumaRemuneraciones = 0;

                for (var i = 0; i < remuneraciones.length; i++) {
                    sumaRemuneraciones = sumaRemuneraciones + parseInt(remuneraciones[i]);
                }

                content = content + '<table id="tablaPromediosCalculo" style="display: none;">';
                content = content + '<thead>';
                content = content + '<tr>';
                content = content + '<th>Promedio Diario</th>';
                content = content + '<th>Días</th>';
                content = content + '</tr>';
                content = content + '</thead>';
                content = content + '<tbody>';
                content = content + '<tr>';
                content = content + '<td>' + formatNumber.new(Math.round(sumaRemuneraciones / parseInt(dias)), "$") + '</td>';
                content = content + '<td>' + dias + '</td>';
                content = content + '</tr>';
                content = content + '</tbody>';
                content = content + '</table>';

                $(".tablaPromediosCalculo").html(content);
                $("#tablaPromediosCalculo").show();
                break;
            case "TEAMRRHH":
                $("#tbTWESTProm").hide();
                break;
        }
    }

    function ajaxVisualizarOtrosHaberesFinquito(page, idDesvinculacion) {
        $.ajax({
            type: 'POST',
            url: page + '/GetVisualizarOtrosHaberesFiniquitoCalculoService',
            data: '{ idDesvinculacion: "' + idDesvinculacion + '" }',
            dataType: 'json',
            contentType: 'application/json',
            async: false,
            success: function (response) {
                var data = JSON.parse(response.d);
                var content = "";
                if (data.resultado.length > 0) {

                    content = content + '<table id="tablaOtrosHaberesFiniquito" style="display: none;">';
                    content = content + '<thead>';
                    content = content + '<tr>';
                    content = content + '<th>Haber</th>';
                    content = content + '<th>Monto</th>';
                    content = content + '</tr>';
                    content = content + '</thead>';
                    content = content + '<tbody>';
                    content = content + '</tbody>';
                    content = content + '</table>';

                    $(".tablaOtrosHaberesFiniquito").html(content);

                    content = "";

                    for (var i = 0; i < data.resultado.length; i++) {
                        if (data.resultado[i].VALIDACION === "0") {

                            if ((i + 1) < data.resultado.length) {
                                content = content + '<tr>';
                                content = content + '<td>' + data.resultado[i].HABER + '</td>';
                                content = content + '<td>' + formatNumber.new(data.resultado[i].MONTO, "$") + '</td>';
                                content = content + '</tr>';
                            }

                            if ((i + 1) == data.resultado.length) {
                                content = content + '<tr>';
                                content = content + '<td></td>';
                                content = content + '<td></td>';
                                content = content + '</tr>';
                                content = content + '<tr>';
                                content = content + '<td>' + data.resultado[i].HABER + '</td>';
                                content = content + '<td>' + formatNumber.new(data.resultado[i].MONTO, "$") + '</td>';
                                content = content + '</tr>';
                            }
                        } else {

                            content = content + '<tr>';
                            content = content + '<td>' + data.resultado[i].HABER + '</td>';
                            content = content + '<td>' + formatNumber.new(data.resultado[i].MONTO, "$") + '</td>';
                            content = content + '</tr>';

                        }
                    }

                    $("#tablaOtrosHaberesFiniquito > tbody").html(content);
                    $("#tablaOtrosHaberesFiniquito").show();

                }
            },
            error: function (xhr) {
                console.log(xhr.responseText);
            },
            complete: function () {
            }
        });
    }

    function ajaxVisualizarDescuentosFiniquito(page, idDesvinculacion) {
        $.ajax({
            type: 'POST',
            url: page + '/GetVisualizarDescuentosFiniquitoCalculoService',
            data: '{ idDesvinculacion: "' + idDesvinculacion + '" }',
            dataType: 'json',
            contentType: 'application/json',
            async: false,
            success: function (response) {
                var data = JSON.parse(response.d);
                var content = "";
                if (data.resultado.length > 0) {

                    content = content + '<table id="tablaDescuentosFiniquito" style="display: none;">';
                    content = content + '<thead>';
                    content = content + '<tr>';
                    content = content + '<th>Descuento</th>';
                    content = content + '<th>Monto</th>';
                    content = content + '</tr>';
                    content = content + '</thead>';
                    content = content + '<tbody>';
                    content = content + '</tbody>';
                    content = content + '</table>';

                    $(".tablaDescuentosFiniquito").html(content);

                    content = "";

                    for (var i = 0; i < data.resultado.length; i++) {
                        if (data.resultado[i].VALIDACION === "0") {

                            if((i + 1) < data.resultado.length){
                                content = content + '<tr>';
                                content = content + '<td>' + data.resultado[i].HABER + '</td>';
                                content = content + '<td>' + formatNumber.new(data.resultado[i].MONTO, "$") + '</td>';
                                content = content + '</tr>';
                            }

                            if ((i + 1) == data.resultado.length) {
                                content = content + '<tr>';
                                content = content + '<td></td>';
                                content = content + '<td></td>';
                                content = content + '</tr>';
                                content = content + '<tr>';
                                content = content + '<td>' + data.resultado[i].HABER + '</td>';
                                content = content + '<td>' + formatNumber.new(data.resultado[i].MONTO, "$") + '</td>';
                                content = content + '</tr>';
                            }

                        } else {

                            content = content + '<tr>';
                            content = content + '<td>' + data.resultado[i].HABER + '</td>';
                            content = content + '<td>' + formatNumber.new(data.resultado[i].MONTO, "$") + '</td>';
                            content = content + '</tr>';

                        }
                    }

                    $("#tablaDescuentosFiniquito > tbody").html(content);
                    $("#tablaDescuentosFiniquito").show();

                }
            },
            error: function (xhr) {
                console.log(xhr.responseText);
            },
            complete: function () {
            }
        });
    }

    function ajaxVisualizarDiasVacaciones(page, idDesvinculacion) {
        $.ajax({
            type: 'POST',
            url: page + '/GetVisualizarDiasVacacionesCalculoService',
            data: '{ idDesvinculacion: "' + idDesvinculacion + '" }',
            dataType: 'json',
            contentType: 'application/json',
            async: false,
            success: function (response) {
                var data = JSON.parse(response.d);
                var content = "";
                var empresa = "";
                if (data.resultado.length > 0) {

                    for (var i = 0; i < data.resultado.length; i++) {
                        if (data.resultado[i].VALIDACION === "0") {
                            if(i == 0){
                                content = content + '<table id="tablaDiasVacaciones" style="display: none;">';
                                content = content + '<thead>';
                                content = content + '<tr style="border: none;">';
                                switch(data.resultado[i].EMPRESA){
                                    case "TWEST":
                                        content = content + '<td colspan="2" style="color: #fff; font-weight: bold;">Días Vacaciones</td>';
                                        break;
                                    case "TEAMRRHH":
                                        content = content + '<td colspan="3" style="color: #fff; font-weight: bold;">Días Vacaciones</td>';
                                        break;
                                }
                                content = content + '</tr>';
                                content = content + '</thead>';
                                content = content + '<tbody>';
                                content = content + '</tbody>';
                                content = content + '</table>';

                                switch(data.resultado[i].EMPRESA){
                                    case "TWEST":
                                        $("#tbTWESTTotales .tablaDiasVacaciones").html(content);
                                        break;
                                    case "TEAMRRHH":
                                        $("#tbTEAMRRHHTotales .tablaDiasVacaciones").html(content);
                                        break;
                                }
                                content = "";
                            }

                            switch(data.resultado[i].EMPRESA){
                                case "TWEST":
                                    content = content + '<tr>';
                                    content = content + '<td>' + data.resultado[i].CONCEPTO + '</td>';
                                    content = content + '<td>' + data.resultado[i].VALOR1 + '</td>';
                                    content = content + '</tr>'
                                    break;
                                case "TEAMRRHH":
                                    content = content + '<tr>';
                                    content = content + '<td>' + data.resultado[i].CONCEPTO + '</td>';
                                    content = content + '<td>' + data.resultado[i].VALOR1 + '</td>';
                                    content = content + '<td>' + data.resultado[i].VALOR2 + '</td>';
                                    content = content + '</tr>'
                                    break;
                            }
                        }
                    }

                    $("#tablaDiasVacaciones > tbody").html(content);
                    $("#tablaDiasVacaciones").show();

                }
            },
            error: function (xhr) {
                console.log(xhr.responseText);
            },
            complete: function () {
            }
        });
    }

    function ajaxVisualizarTotalesFiniquito(page, idDesvinculacion) {
        $.ajax({
            type: 'POST',
            url: page + '/GetVisualizarTotalesFiniquitoCalculoService',
            data: '{ idDesvinculacion: "' + idDesvinculacion + '" }',
            dataType: 'json',
            contentType: 'application/json',
            async: false,
            success: function (response) {
                var data = JSON.parse(response.d);
                var content = "";
                if (data.resultado.length > 0) {

                    for (var i = 0; i < data.resultado.length; i++) {
                        if (data.resultado[i].VALIDACION === "0") {

                            if(i == 0){
                                content = content + '<table id="tablaTotalesFiniquitos" style="display: none;">';
                                content = content + '<thead>';
                                content = content + '<tr style="border: none;">';
                                switch(data.resultado[i].EMPRESA){
                                    case "TWEST":
                                        content = content + '<td colspan="2" style="color: #fff; font-weight: bold;">Totales</td>';
                                        break;
                                    case "TEAMRRHH":
                                        content = content + '<td colspan="2" style="color: #fff; font-weight: bold;">Total Pagos</td>';
                                        break;
                                }
                                content = content + '</tr>';
                                content = content + '</thead>';
                                content = content + '<tbody>';
                                content = content + '</tbody>';
                                content = content + '</table>';

                                switch(data.resultado[i].EMPRESA){
                                    case "TWEST":
                                        $("#tbTWESTTotales .tablaTotalesFiniquitos").html(content);
                                        break;
                                    case "TEAMRRHH":
                                        $("#tbTEAMRRHHTotales .tablaTotalesFiniquitos").html(content);
                                        break;
                                }

                                content = "";
                            }

                            //content = content + '<tr>';
                            //content = content + '<td>A Pagar por Feriado Proporcional</td>';
                            //content = content + '<td>' + formatNumber.new(data.resultado[i].FERIADOPROPORCIONAL, "$") + '</td>';
                            //content = content + '</tr>';
                            //content = content + '<tr>';
                            //content = content + '<td>Otros Haberes </td>';
                            //content = content + '<td>' + formatNumber.new(data.resultado[i].OTROSHABERES, "$") + '</td>';
                            //content = content + '</tr>';
                            //content = content + '<tr>';
                            //content = content + '<td>Total Descuentos</td>';
                            //content = content + '<td>' + formatNumber.new(data.resultado[i].TOTALDESCUENTO, "$") + '</td>';
                            //content = content + '</tr>';
                            //content = content + '<tr>';
                            //content = content + '<td></td>';
                            //content = content + '<td></td>';
                            //content = content + '</tr>';
                            //content = content + '<tr>';
                            //content = content + '<td>Total Finiquito</td>';
                            //content = content + '<td>' + formatNumber.new(data.resultado[i].TOTALHABERES, "$") + '</td>';
                            //content = content + '</tr>';

                            content = content + '<tr>';
                            content = content + '<td>' + data.resultado[i].CONCEPTO + '</td>';
                            if(data.resultado[i].VALOR !== ""){
                                content = content + '<td>' + formatNumber.new(data.resultado[i].VALOR, "$") + '</td>';
                            } else {
                                content = content + '<td></td>';
                            }
                            content = content + '</tr>'
                        }
                    }

                    $("#tablaTotalesFiniquitos > tbody").html(content);
                    $("#tablaTotalesFiniquitos").show();

                }
            },
            error: function (xhr) {
                console.log(xhr.responseText);
            },
            complete: function () {
            }
        });
    }

    function ajaxFiniquitoConfirmado(page, idDesvinculacion) {
        $.ajax({
            type: 'POST',
            url: page + '/GetValidarConfirmadoFiniquitoCalculoService',
            data: '{ idDesvinculacion: "' + idDesvinculacion + '" }',
            dataType: 'json',
            contentType: 'application/json',
            async: false,
            success: function (response) {
                var data = JSON.parse(response.d);
                if (data.resultado.length > 0) {

                    for (var i = 0; i < data.resultado.length; i++) {
                        if (data.resultado[i].VALIDACION === "0") {
                            $("#confirmacionFiniquitoRealizado").show();
                            $("#confirmacionFiniquito").hide();
                        } else {
                            $("#confirmacionFiniquitoRealizado").hide();
                            $("#confirmacionFiniquito").show();
                            $("#btnConfirmarRecepcion").attr("data-iddesvinculacion", idDesvinculacion);
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

    function ajaxConfirmarRetiroFiniquito(page, idDesvinculacion, medioPago, monto, observacion) {
        $.ajax({
            type: 'POST',
            url: page + '/SetConfirmarRetiroCalculoService',
            data: '{ idDesvinculacion: "' + idDesvinculacion + '", medioPago: "' + medioPago + '", monto: "' + monto + '", observacion: "' + observacion + '" }',
            dataType: 'json',
            contentType: 'application/json',
            async: true,
            success: function (response) {
                var data = JSON.parse(response.d);
                var content = "";
                if (data.resultado.length > 0) {

                    for (var i = 0; i < data.resultado.length; i++) {
                        if (data.resultado[i].VALIDACION === "0") {
                            swal({
                                type: 'success',
                                title: 'Confirmación exitosa!',
                                text: data.resultado[i].RESULTADO,
                                showConfirmButton: true,
                                textConfirmButton: 'Aceptar'
                            });
                        } else {
                            swal({
                                type: 'warning',
                                title: 'Atención!',
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
                ajaxFiniquitoConfirmado("VisualizacionCalculoFiniquito.aspx", idDesvinculacion);
            }
        });
    }

    function ajaxCargo(page, ficha, empresa) {

        var method = "";

        switch(empresa){
            case "TWEST":
                method = "GetCargoService"
                break;
            case "TEAMRRHH":
                method = "GetCargoOUTService"
                break;
            case "TEAMWORK":
                method = "";
                break;
        }

        $.ajax({
            type: 'POST',
            url: page + '/' + method,
            data: '{ ficha: "' + ficha + '" }',
            dataType: 'json',
            contentType: 'application/json',
            async: false,
            success: function (response) {
                var data = JSON.parse(response.d);
                var content = "";
                if (data.resultado.length > 0) {

                    for (var i = 0; i < data.resultado.length; i++) {
                        $("#cargo").val(data.resultado[i].CarNom);
                        $("#cargoMod").val(data.resultado[i].glosa);
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

    function ajaxValorUF(page, idDesvinculacion){
        $.ajax({
            type: 'POST',
            url: page + '/GetVisualizarValorUfFiniquitoCalculoService',
            data: '{ idDesvinculacion: "' + idDesvinculacion + '" }',
            dataType: 'json',
            contentType: 'application/json',
            async: false,
            success: function (response) {
                var data = JSON.parse(response.d);
                var content = "";
                if (data.resultado.length > 0) {

                    for (var i = 0; i < data.resultado.length; i++) {
                        if (data.resultado[i].VALIDACION === "0") {
                            $("#valorUF").html(formatNumber.new(data.resultado[i].VALORUF, "$"));
                            $("#valorUFTope").html(formatNumber.new(data.resultado[i].TOPEUF, "$"));
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

    function ajaxHaberesMensual(page, idDesvinculacion){
        $.ajax({
            type: 'POST',
            url: page + '/GetVisualizarHabaeresMensualFiniquitoCalculoService',
            data: '{ idDesvinculacion: "' + idDesvinculacion + '" }',
            dataType: 'json',
            contentType: 'application/json',
            async: false,
            success: function (response) {
                var data = JSON.parse(response.d);
                var content = "";
                if (data.resultado.length > 0) {

                    for (var i = 0; i < data.resultado.length; i++) {
                        if (data.resultado[i].VALIDACION === "0") {
                            if(i == 0){
                                content = content + '<table id="tablaHaberesMensual" style="display: none;">';
                                content = content + '<thead>';
                                content = content + '<tr style="border: none;">';
                                content = content + '<th style="color: #fff; font-weight: bold;">Haberes</th>';
                                content = content + '<th style="color: #fff; font-weight: bold;">Mensual</th>';
                                content = content + '</tr>';
                                content = content + '</thead>';
                                content = content + '<tbody>';
                                content = content + '</tbody>';
                                content = content + '</table>';

                                switch(data.resultado[i].EMPRESA){
                                    case "TEAMRRHH":
                                        $("#tbTEAMRRHHTotales .tablaHaberesMensual").html(content);
                                        break;
                                }
                                content = "";
                            }

                            switch(data.resultado[i].EMPRESA){
                                case "TEAMRRHH":
                                    content = content + '<tr>';
                                    content = content + '<td>' + data.resultado[i].CONCEPTO + '</td>';
                                    content = content + '<td>' + formatNumber.new(data.resultado[i].VALOR, "$") + '</td>';
                                    content = content + '</tr>';
                                    break;
                            }
                        }
                    }

                    $("#tablaHaberesMensual > tbody").html(content);
                    $("#tablaHaberesMensual").show();

                }
            },
            error: function (xhr) {
                console.log(xhr.responseText);
            },
            complete: function () {
            }
        });
    }

    function ajaxConceptosAdicionales(page, idDesvinculacion){
        $.ajax({
            type: 'POST',
            url: page + '/GetVisualizarConceptosAdicionalesFiniquitoCalculoService',
            data: '{ idDesvinculacion: "' + idDesvinculacion + '" }',
            dataType: 'json',
            contentType: 'application/json',
            async: false,
            success: function (response) {
                var data = JSON.parse(response.d);
                var content = "";
                if (data.resultado.length > 0) {

                    for (var i = 0; i < data.resultado.length; i++) {
                        if (data.resultado[i].VALIDACION === "0") {
                            switch(data.resultado[i].EMPRESA){
                                case "TEAMRRHH":
                                    if(i == 0){
                                        content = content + '<table id="tablaOtrosConceptos">';
                                        content = content + '<thead>';
                                        content = content + '<tr>';
                                        content = content + '<th>' + data.resultado[i].CONCEPTO + '</th>';
                                        content = content + '<th>' + data.resultado[i].CONCEPTO2 + '</th>';
                                        content = content + '</tr>';
                                        content = content + '</thead>';
                                        content = content + '<tbody>';
                                        content = content + '</tbody>';
                                        content = content + '</table>';

                                        $(".tablaOtrosConceptos").html(content);

                                        content = "";
                                    } else {
                                        content = content + '<tr>';
                                        content = content + '<td>' + data.resultado[i].CONCEPTO + '</td>';
                                        content = content + '<td>' + formatNumber.new(data.resultado[i].CONCEPTO2, "$") + '</td>';
                                        content = content + '</tr>';
                                    }
                                    break;
                                case "TWEST":
                                    $("#contentOtrosConcBonos").hide();
                                    $("#separateOtrosConcBonos").hide();
                                    break;
                            }

                            $("#tablaOtrosConceptos > tbody").html(content);
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

    function ajaxBonosAdicionales(page, idDesvinculacion){
        $.ajax({
            type: 'POST',
            url: page + '/GetVisualizarBonosAdicionalesFiniquitoCalculoService',
            data: '{ idDesvinculacion: "' + idDesvinculacion + '" }',
            dataType: 'json',
            contentType: 'application/json',
            async: false,
            success: function (response) {
                var data = JSON.parse(response.d);
                var content = "";
                if (data.resultado.length > 0) {
                    
                    for (var i = 0; i < data.resultado.length; i++) {
                        if (data.resultado[i].VALIDACION === "0") {
                            switch(data.resultado[i].EMPRESA){
                                case "TEAMRRHH":
                                    if(i == 0){
                                        content = content + '<table id="tablaBonosAdicionales">';
                                        content = content + '<thead>';
                                        content = content + '<tr>';
                                        content = content + '<th>' + data.resultado[i].CONCEPTO + '</th>';
                                        content = content + '<th>' + data.resultado[i].VALOR + '</th>';
                                        content = content + '<th>' + data.resultado[i].PERIODO + '</th>';
                                        content = content + '</tr>';
                                        content = content + '</thead>';
                                        content = content + '<tbody>';
                                        content = content + '</tbody>';
                                        content = content + '</table>';

                                        $(".tablaBonosAdicionales").html(content);

                                        content = "";
                                    } else {
                                        content = content + '<tr>';
                                        content = content + '<td>' + data.resultado[i].CONCEPTO + '</td>';
                                        content = content + '<td>' + formatNumber.new(data.resultado[i].VALOR, "$") + '</td>';
                                        content = content + '<td>' + data.resultado[i].PERIODO + '</td>';
                                        content = content + '</tr>';
                                    }
                                    break;
                                case "TWEST":
                                    $("#contentOtrosConcBonos").hide();
                                    $("#separateOtrosConcBonos").hide();
                                    break;
                            }

                            $("#tablaBonosAdicionales > tbody").html(content);
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

    function ajaxCentroCosto(page, ficha, empresa) {
        var method = "";

        switch(empresa){
            case "TWEST":
                method = "GetCentroCostosService";
                break;
            case "TEAMRRHH":
                method = "GetCentroCostosOUTService";
                break;
        }

        $.ajax({
            type: 'POST',
            url: page + '/' + method,
            data: '{ ficha: "' + ficha + '" }',
            dataType: 'json',
            contentType: 'application/json',
            async: false,
            success: function (response) {
                var data = JSON.parse(response.d);
                var content = "";
                if (data.resultado.length > 0) {

                    for (var i = 0; i < data.resultado.length; i++) {
                        $("#cliente").val(data.resultado[i].desarn);
                        $("#areaNegocio").val(data.resultado[i].codarn);
                        $("#centroCosto").val(data.resultado[i].DescCC);
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