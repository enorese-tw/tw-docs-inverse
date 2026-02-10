$(document).ready(function () {


    $(".datepicker").datepicker({
        dateFormat: "yy-mm-dd",
        showButtonPanel: false,
        changeMonth: false,
        changeYear: false,
        inline: true
    });

    $.datepicker.regional["es"] = {
        closeText: "Cerrar",
        prevText: "Anterior",
        nextText: "Siguiente",
        currentText: "Hoy",
        monthNames: [
          "Enero",
          "Febrero",
          "Marzo",
          "Abril",
          "Mayo",
          "Junio",
          "Julio",
          "Agosto",
          "Septiembre",
          "Octubre",
          "Noviembre",
          "Diciembre"
        ],
        monthNamesShort: [
          "Ene",
          "Feb",
          "Mar",
          "Abr",
          "May",
          "Jun",
          "Jul",
          "Ago",
          "Sep",
          "Oct",
          "Nov",
          "Dic"
        ],
        dayNames: [
          "Domingo",
          "Lunes",
          "Martes",
          "Miércoles",
          "Jueves",
          "Viernes",
          "Sábado"
        ],
        dayNamesShort: ["Dom", "Lun", "Mar", "Mié", "Juv", "Vie", "Sáb"],
        dayNamesMin: ["Do", "Lu", "Ma", "Mi", "Ju", "Vi", "Sá"],
        weekHeader: "Sm",
        dateFormat: "dd/mm/yy",
        firstDay: 1,
        isRTL: false,
        showMonthAfterYear: false,
        yearSuffix: ""
    };
    $.datepicker.setDefaults($.datepicker.regional["es"]);

    $("#loadingBasePagos").html('<span class="loaderTW"></span><label class="loading-text-html">ESTAMOS CARGANDO LOS REGISTROS DE PAGOS, ESPERE...</label>');
    setTimeout(function () {
        ajaxObtenerPagos("BasePagoProveedores.aspx", "", "", "", "");
    }, 500);

    $("#agregarFiltroBusqueda").on("change", function () {
        switch ($(this).val()) {
            case "0":
                $("#analytics").html("");
                $("#btnImportarExcel").show().html('<i class="glyphicon-tw glyphicon-tw-exportarexcel"></i> Generar Reporte Completo');
                $(".paginationPagos").html("");
                $("#tablaBasePagos tbody").html("");
                $("#fechaDesdeBusqueda").hide().val("");
                $("#fechaHastaBusqueda").hide().val("");
                $("#loadingBasePagos").html('<span class="loaderTW"></span><label class="loading-text-html">ESTAMOS CARGANDO LOS REGISTROS DE PAGOS, ESPERE...</label>');
                setTimeout(function () {
                    ajaxObtenerPagos("BasePagoProveedores.aspx", "", "", "", "");
                }, 500);
                break;
            case "FECHAS":
                $("#analytics").html("");
                $("#btnImportarExcel").hide().html('<i class="glyphicon-tw glyphicon-tw-exportarexcel"></i> Generar Reporte Filtrado');
                $(".paginationPagos").html("");
                $("#tablaBasePagos tbody").html("");
                $("#fechaDesdeBusqueda").show();
                $("#fechaHastaBusqueda").show();
                break;
        }
    });

    $("#fechaDesdeBusqueda").on("change", function () {
        if ($(this).val().length == 10 && ($(this).val().split("-").length - 1) == 2) {
            $(this).css("border", "1px solid #ccc");
            if ($("#fechaHastaBusqueda").val().length == 10 && ($("#fechaHastaBusqueda").val().split("-").length - 1) == 2) {
                $(".paginationPagos").html("");
                $("#tablaBasePagos tbody").html("");
                $("#analytics").html("");
                $("#loadingBasePagos").html('<span class="loaderTW"></span><label class="loading-text-html">ESTAMOS CARGANDO LOS REGISTROS DE PAGOS, ESPERE...</label>');
                setTimeout(function () {
                    ajaxObtenerPagos("BasePagoProveedores.aspx", $("#agregarFiltroBusqueda").val(), $("#fechaDesdeBusqueda").val(), $("#fechaHastaBusqueda").val(), "");
                }, 500);
            } else {
                $("#fechaHastaBusqueda").css("border", "1px solid #f00");
            }
        } else {
            $(this).css("border", "1px solid #f00");
        }
    });

    $("#fechaHastaBusqueda").on("change", function () {
        if ($(this).val().length == 10 && ($(this).val().split("-").length - 1) == 2) {
            $(this).css("border", "1px solid #ccc");
            if ($("#fechaDesdeBusqueda").val().length == 10 && ($("#fechaDesdeBusqueda").val().split("-").length - 1) == 2) {
                $(".paginationPagos").html("");
                $("#tablaBasePagos tbody").html("");
                $("#analytics").html("");
                $("#loadingBasePagos").html('<span class="loaderTW"></span><label class="loading-text-html">ESTAMOS CARGANDO LOS REGISTROS DE PAGOS, ESPERE...</label>');
                setTimeout(function () {
                    ajaxObtenerPagos("BasePagoProveedores.aspx", $("#agregarFiltroBusqueda").val(), $("#fechaDesdeBusqueda").val(), $("#fechaHastaBusqueda").val(), "");
                }, 500);
            } else {
                $("#fechaDesdeBusqueda").css("border", "1px solid #f00");
            }
        } else {
            $(this).css("border", "1px solid #f00");
        }
    });

    function ajaxObtenerPagos(page, filtrarPor, filtro, filtro2, filtro3) {
        $.ajax({
            type: 'POST',
            url: page + '/GetObtenerPagosService',
            data: '{ filtrarPor: "' + filtrarPor + '", filtro: "' + filtro + '", filtro2: "' + filtro2 + '", filtro3: "' + filtro3 + '" }',
            dataType: 'json',
            contentType: 'application/json',
            async: true,
            success: function (response) {
                var data = JSON.parse(response.d);
                var content = "";

                if (data.resultado.length > 0) {

                    for (var i = 0; i < data.resultado.length; i++) {
                        if (data.resultado[i].VALIDACION === "0") {

                            content = content + '<tr class="items_' + (i + 1) + '">';
                            content = content + '<td>';
                            content = content + '<div class="row" style="border: 1px solid rgb(200, 200, 200)">';
                            content = content + '<div class="col-xs-12 col-sm-12 col-md-2 col-lg-1">';
                            content = content + '<i class="glyphicon-tw-estado glyphicon-tw-disponiblepago teamwork" title="DISPONIBLE A PAGO"></i>';
                            content = content + '</div>';
                            content = content + '<div class="col-xs-12 col-sm-12 col-md-5 col-lg-6">';
                            content = content + '<div class="row" style="text-align: left;">';
                            content = content + '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="margin-top: 10px; margin-bottom: -10px;">';
                            content = content + '<label><i class="etiq-title">PROVEEDOR: </i><i class="etiq-data">' + data.resultado[i].PROVEEDOR + '</i></label>';
                            content = content + '</div>';
                            content = content + '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="margin-bottom: -10px;">';
                            content = content + '<label><i class="etiq-title">RUT ' + data.resultado[i].RUT_PROVEEDOR + ' &bull; ' + data.resultado[i].TIPO_PROVEEDOR + '</i></label>';
                            content = content + '</div>';
                            content = content + '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="margin-bottom: -10px;">';
                            content = content + '<label><i class="etiq-title">' + data.resultado[i].BANCO + '</i></label>';
                            content = content + '</div>';
                            content = content + '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="margin-bottom: -10px;">';
                            content = content + '<label><i class="etiq-title">Pago registrado por </i><i class="etiq-data">' + data.resultado[i].TIPO_PAGO + '</i>';
                            if (data.resultado[i].TIPO_PAGO === "CHEQUE") {
                                content = content + '<i class="etiq-title">, N° Serie </i><i class="etiq-data">' + data.resultado[i].NSERIE_DOCUMENTO + '</i>';
                            }
                            content = content + '</label>';
                            content = content + '</div>';
                            content = content + '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="margin-bottom: 10px;">';
                            content = content + '<label><i class="etiq-title">Fecha de generación ' + formatStringDate(data.resultado[i].FECHA_GENERACION) + '</i></label>';
                            content = content + '</div>';
                            content = content + '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="margin-bottom: 10px;">';
                            content = content + '<span class="btn btn-primary">Monto Finiquito ' + formatNumber.new(data.resultado[i].MONTO, "$") + '</span>';
                            content = content + '</div>';
                            content = content + '</div>';
                            content = content + '</div>';
                            content = content + '<div class="col-xs-12 col-sm-12 col-md-5 col-lg-4">';
                            content = content + '<div class="row" style="text-align: left;">';
                            content = content + '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="margin-top: 10px; margin-bottom: 5px;">';
                            content = content + '<label><i class="etiq-title">DESGLOSE MONTOS ASOCIADOS AL FINIQUITO</i></label>';
                            content = content + '</div>';
                            content = content + '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="margin-bottom: 10px;">';
                            content = content + '<span class="btn btn-primary">' + data.resultado[i].OBSERVACION + '</span>';
                            content = content + '</div>';
                            content = content + '</div>';
                            content = content + '</div>';
                            content = content + '</div>';
                            content = content + '</td>';
                            content = content + '</tr>';

                            if ((i + 1) == data.resultado.length) {
                                $("#btnImportarExcel").show();
                                var filtroHabilitado = "N";
                                if (filtrarPor !== "") {
                                    filtroHabilitado = "S";
                                }

                                createAnalytics(data.resultado.length, filtroHabilitado, filtrarPor, filtro, filtro2, filtro3);
                            }

                        } else {

                            createAnalytics(0, "N", filtrarPor, filtro, filtro2, filtro3);
                            $(".paginationPagos").html("");
                            content = content + '<span style="display: block; margin: 30px auto 30px auto;"><h1 style="font-size: 20px; color: rgb(150, 150, 150);">' + data.resultado[i].RESULTADO + '</h1></span>';

                        }
                    }

                    $("#tablaBasePagos tbody").html(content);

                } else {
                    swal({
                        type: 'error',
                        title: 'Ha ocurrido un problema!',
                        text: 'No es posible comunicarse con el servicio, intentelo más tarde.'
                    });
                }
            },
            error: function (xhr) {
                console.log(xhr.responseText);
            },
            complete: function () {
                pagination(".paginationPagos");

                $("#loadingBasePagos").html('');
            }
        });
    }

    function createAnalytics(resultados, filtroHabilitado, filtrarPor, filtro, filtro2, filtro3) {
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
                case "FECHASYEMPRESA":
                    content = content + '<label><i class="etiq-analytics">Filtrado: </i><i class="etiq-data-analytics">EMPRESA : ' + filtro + ' &bull; DESDE : ' + filtro2 + ' &bull; HASTA: ' + filtro3 + '</i></label>';
                    break;
                case "EMPRESA":
                    content = content + '<label><i class="etiq-analytics">Filtrado: </i><i class="etiq-data-analytics">EMPRESA : ' + filtro + '</i></label>';
                    break;
                case "TRABAJADOR":
                    content = content + '<label><i class="etiq-analytics">Filtrado: </i><i class="etiq-data-analytics">TRABAJADOR : ' + filtro + '</i></label>';
                    break;
                case "CLIENTE":
                    content = content + '<label><i class="etiq-analytics">Filtrado: </i><i class="etiq-data-analytics">CLIENTE : ' + filtro + '</i></label>';
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