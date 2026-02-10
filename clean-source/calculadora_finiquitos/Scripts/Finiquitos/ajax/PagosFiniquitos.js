$(document).ready(function () {

    $("#numeroCheque").on("blur", function () {
        validaDocumento("PagosFiniquitos.aspx", $(this).val());
    });

    function validaDocumento(page, numeroDocumento) {
        $.ajax({
            type: 'POST',
            url: page + '/GetValidaDocumentoPagos',
            data: '{ numeroDocumento: "' + numeroDocumento + '" }',
            dataType: 'json',
            contentType: 'application/json',
            async: true,
            success: function (response) {
                var data = JSON.parse(response.d);
                if(data.resultado.length > 0){

                    for (var i = 0; i < data.resultado.length; i++){
                        if (data.resultado[i].VALIDACION === "0") {
                            $("#contentValidaDocumento").html('<label>' + data.resultado[i].RESULTADO + '</label>');
                            validaDocumentoSession(page, "true");
                        } else {
                            $("#contentValidaDocumento").html('<label>' + data.resultado[i].RESULTADO + '</label>');
                            validaDocumentoSession(page, "false");
                        }
                    }

                } else {

                }
            },
            error: function (xhr) {
                console.log(xhr.responseText);
            },
            complete: function () {
            }
        });
    }

    function validaDocumentoSession(page, resultado) {
        $.ajax({
            type: 'POST',
            url: page + '/validacionGuardarRegistroPago',
            data: '{ resultValidaDocumento: "' + resultado + '" }',
            dataType: 'json',
            contentType: 'application/json',
            async: true,
            success: function (response) {

            },
            error: function (xhr) {
                console.log(xhr.responseText);
            },
            complete: function () {
            }
        });
    }

});