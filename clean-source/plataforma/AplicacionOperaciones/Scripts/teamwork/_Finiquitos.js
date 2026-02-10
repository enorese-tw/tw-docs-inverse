$(document).ready(function () {
    $(document).on("click", ".typePayments", function () {
        switch ($(this).val()) {
            case "Cheque":
                $("#btnConfirmarCheque").show();
                $("#btnConfirmarTEF").hide();
                break;
            case "TEF":
                $("#btnConfirmarTEF").show();
                $("#btnConfirmarCheque").hide();
                break;
        }
    });

    $(document).on("click", ".eventRestore", function (e) {
        e.preventDefault();
        window.location.reload();
    });

    $(document).on("change", "#typeFilter", function () {
        switch ($(this).val()) {
            case "Q29zdG9DZXJv":
                $("#btnBuscarSolicitud").addClass("dspl-inline-block").removeClass("dspl-none");

                /** remueve elementos */
                RemoverElementosHtmlfiltrado("", "S", "", "", "", "");
                break;
            case "bm9uZQ==":
                $("#filterDatos").addClass("dspl-inline-block").removeClass("dspl-none").attr("placeholder", "Indique el folio");
                $("#btnBuscarSolicitud").addClass("dspl-inline-block").removeClass("dspl-none");

                /** remueve elementos */
                RemoverElementosHtmlfiltrado("", "S", "", "", "", "");
                break;
            case "UnV0VHJhYmFqYWRvcg==":
                $("#filterDatos").addClass("dspl-inline-block").removeClass("dspl-none").attr("placeholder", "Indique el Rut del trabajador");
                $("#btnBuscarSolicitud").addClass("dspl-inline-block").removeClass("dspl-none");

                /** remueve elementos */
                RemoverElementosHtmlfiltrado("", "S", "", "", "", "");
                break;
            case "RW1wcmVzYQ==":
                $("#filterEmpresa").addClass("dspl-inline-block").removeClass("dspl-none");
                $("#btnBuscarSolicitud").addClass("dspl-inline-block").removeClass("dspl-none");

                /** remueve elementos */
                RemoverElementosHtmlfiltrado("", "", "S", "", "", "");
                break;
            case "QXJlYSBOZWdvY2lv":
                $("#filterDatos").addClass("dspl-inline-block").removeClass("dspl-none").attr("placeholder", "Indique el área de negocio");
                $("#btnBuscarSolicitud").addClass("dspl-inline-block").removeClass("dspl-none");

                /** remueve elementos */
                RemoverElementosHtmlfiltrado("", "S", "", "", "", "");
                break;
            case "Tm9tYnJlIFRyYWJhamFkb3I=":
                $("#filterDatos").addClass("dspl-inline-block").removeClass("dspl-none").attr("placeholder", "Indique el nombre del trabajador");
                $("#btnBuscarSolicitud").addClass("dspl-inline-block").removeClass("dspl-none");
                /** remueve elementos */
                RemoverElementosHtmlfiltrado("", "S", "", "", "", "");
                break;
            case "Q2F1c2FsIERlc3ZpbmN1bGFjacOzbg==":
                $("#filterCausal").addClass("dspl-inline-block").removeClass("dspl-none");
                $("#btnBuscarSolicitud").addClass("dspl-inline-block").removeClass("dspl-none");

                /** remueve elementos */
                RemoverElementosHtmlfiltrado("", "", "", "S", "", "");
                break;
            case "RmVjaGFz":
                $("#filterDatoFechaIni").addClass("dspl-inline-block").removeClass("dspl-none");
                $("#filterDatoFechaEnd").addClass("dspl-inline-block").removeClass("dspl-none");
                $("#btnBuscarSolicitud").addClass("dspl-inline-block").removeClass("dspl-none");

                /** remueve elementos */
                RemoverElementosHtmlfiltrado("", "", "", "", "", "N");
                break;
            case "RXN0YWRv":
                $("#filterEstado").addClass("dspl-inline-block").removeClass("dspl-none");
                $("#btnBuscarSolicitud").addClass("dspl-inline-block").removeClass("dspl-none");

                /** remueve elementos */
                RemoverElementosHtmlfiltrado("", "", "", "", "S", "");
                break;
            default:
                RemoverElementosHtmlfiltrado("S", "", "", "", "", "");
                break;
        }
    });
});