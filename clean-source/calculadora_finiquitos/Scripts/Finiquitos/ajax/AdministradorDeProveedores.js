$(document).ready(function () {

    ajaxObtenerProveedores("AdministradorDeProveedores.aspx");

    function ajaxObtenerProveedores(page) {
        $.ajax({
            type: 'POST',
            url: page + '/GetObtenerProveedoresProveedorService',
            data: '{  }',
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
                            content = content + '<div class="row" style="margin-bottom: 10px; min-height: 100px; border-top: 1px solid rgb(200, 200, 200); border-right: 1px solid rgb(200, 200, 200); border-bottom: 1px solid rgb(200, 200, 200); border-left: 5px solid #f00;">';
                            content = content + '<div class="col-xs-12 col-sm-12 col-md-6 col-lg-7">';
                            content = content + ' <div class="row" style="text-align: left;">';
                            content = content + '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="margin-top: 10px; margin-bottom: -10px;">';
                            content = content + '<label><i class="etiq-title">PROVEEDOR </i><i class="etiq-data">' + data.resultado[i].NOMBRE + '</i></label>';
                            content = content + '</div>';
                            content = content + '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="margin-bottom: -10px;">';
                            content = content + '<label><i class="etiq-title">RUT </i><i class="etiq-data">' + data.resultado[i].RUT + '</i><i class="etiq-title"> &bull; TIPO DE PROVEEDOR </i><i class="etiq-data">' + data.resultado[i].TIPO_PROVEEDOR + '</i></label>';
                            content = content + '</div>';
                            content = content + '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="margin-bottom: -10px;">';
                            content = content + '<label><i class="etiq-title">Fecha de publicación 13 de Agosto de 2018 a las 13:00:00 horas.</i></label>';
                            content = content + '</div>';
                            content = content + '</div>';
                            content = content + '</div>';
                            content = content + '<div class="col-xs-12 col-sm-6 col-md-3 col-lg-2">';
                            content = content + '<div class="row">';
                            content = content + '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="margin-top: 30px;">';
                            content = content + '<span class="btn btn-success">ACTIVO</span>';
                            content = content + '</div>';
                            content = content + '</div>';
                            content = content + '</div>';
                            content = content + '<div class="col-xs-12 col-sm-6 col-md-3 col-lg-3">';
                            content = content + '<div class="row">';
                            content = content + '<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="margin-top: 30px;">';
                            content = content + '<span class="btn btn-warning"><i class="glyphicon-tw glyphicon-tw-cargaMasv"></i></span>';
                            content = content + '<span class="btn btn-danger"><i class="glyphicon-tw glyphicon-tw-cargaMasv"></i></span>';
                            content = content + '</div>';
                            content = content + '</div>';
                            content = content + '</div>';
                            content = content + '</div>';
                            content = content + '</td>';
                            content = content + '</tr>';
                        }
                    }

                    $("#tablaProveedores tbody").html(content);

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

})