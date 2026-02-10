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
            prefix = prefix + "/" + window.location.href.split('/')[window.location.href.split('/').length - 1] + "/";
        } else {
            prefix = prefix + "/" + window.location.href.split('/')[4] + "/";
        }
    }

    /** END OBTENCION DINAMICA DE URL DE APLICACION PARA ACCESO A METODOS INTERNOS */
    
    $(document).on("click", "#fileUpload", function (e) {
        e.preventDefault();
        $("#file").val(null);
        $("#messagePlataforma").html('');
        sessionStorage.setItem("ApplicationError", "N");
        $("#file").trigger("click");
    });

    $("#file").on("change", function () {
        $("#eventFileUpload").trigger("click");
    });

    $(document).on("click", "#closeErrorGeneric", function () {
        $("#errorGeneric").hide();
    });
    
    $("#selectEmpresa").on("change", function () {
        var Empresa = $("#selectEmpresa").val();

        switch (Empresa) {
            case "2":
                $("#Causales").removeClass("dspl-none");
                break;
            case "3":
                $("#Causales").removeClass("dspl-none");
                break;
            default:
                $("#Causales").addClass("dspl-none");
                break;
        }
    });

    $("#selectTodas").on("change", function () {
        var Empresa = $("#selectTodas").val();

        switch (Empresa) {
            case "1":
                $("#filtros").removeClass("dspl-none");
                break;
            case "2":
                $("#filtros").removeClass("dspl-none");
                break;
            default:
                $("#filtros").addClass("dspl-none");
                break;
        }
    });
    $("#GenerarArchivoBPONoConfirmado").on("click", function (e) {
        e.preventDefault();
        var Empresa = $("#selectEmpresaNC").val();
        var FechaInicio = $("#fechaInicioNC").val();
        var FechaTermino = $("#fechaTerminoNC").val();
        var Tipo = $("#selectTipoNoConfirmadas").val();

        switch (Empresa) {
            case "1":
                if (Tipo === "0") {
                    window.location.href =
                        prefixDomain + prefix +
                        'GenerateExcel?excel=YnBvZmlsdHJv&empresa=' + btoa("EST") + '&fechainicio=' + btoa(FechaInicio) + '&fechatermino=' + btoa(FechaTermino) + '&fuente=POSIBLESBAJAS&tipo=BajaNoConfirmada' /*+ Empresa, FechaInicio, FechaTermino, Todas);*/
                }
                else {
                    window.location.href =
                        prefixDomain + prefix +
                    'GenerateExcel?excel=YnBvZmlsdHJv&empresa=' + btoa("EST") + '&fechainicio=' + btoa(FechaInicio) + '&fechatermino=' + btoa(FechaTermino) + '&fuente=POSIBLESBAJASHISTORICO&tipo=BajaNoConfirmada' /*+ Empresa, FechaInicio, FechaTermino, Todas);*/
                }
                
                break;
            default:
                alert("Especifique la empresa");
                break;
        }
    });

    //boton bajas confirmada
    $("#GenerarArchivoBPOFiltrado").on("click", function (e) {
        e.preventDefault();
        var Empresa = $("#selectEmpresa").val();
        var FechaInicio = $("#fechaInicio").val();
        var FechaTermino = $("#fechaTermino").val();
        var Todas = $("#selectTodas").val();


        switch (Empresa) {
            case "0":
                $("#selectEmpresa").focus();
                alert("Especifique la empresa");
                break;

            case "1":
                if (Todas === "0") {
                    FechaInicio = "";
                    FechaTermino = "";
                    window.location.href =
                        prefixDomain + prefix +
                    'GenerateExcel?excel=YnBvZmlsdHJv&empresa=' + btoa("EST") + '&fechainicio=' + btoa(FechaInicio) + '&fechatermino=' + btoa(FechaTermino) + '&fuente=BPO&tipo=BajaConfirmada'  /*+ Empresa, FechaInicio, FechaTermino, Todas);*/
                }
                if (Todas === "1") {
                    if (FechaInicio !== '') {
                        if (FechaTermino !== '') {
                            var url = prefixDomain + prefix + 'GenerateExcel?excel=YnBvZmlsdHJv&empresa=' + btoa("EST") + '&fechainicio=' + btoa(FechaInicio) + '&fechatermino=' + btoa(FechaTermino) + '&fuente=FILTRADO&tipo=BajaConfirmada'  /*+ Empresa, FechaInicio, FechaTermino, Todas);*/

                            window.location.href =
                                prefixDomain + prefix +
                            'GenerateExcel?excel=YnBvZmlsdHJv&empresa=' + btoa("EST") + '&fechainicio=' + btoa(FechaInicio) + '&fechatermino=' + btoa(FechaTermino) + '&fuente=FILTRADO&tipo=BajaConfirmada'  /*+ Empresa, FechaInicio, FechaTermino, Todas);*/
                        }
                        else {
                            $("#fechaTermino").focus();
                        }
                    }
                    else {
                        $("#fechaInicio").focus();
                    }
                }
                if (Todas === "2") {
                    if (FechaInicio !== '') {
                        if (FechaTermino !== '') {
                            window.location.href =
                                prefixDomain + prefix +
                            'GenerateExcel?excel=YnBvZmlsdHJv&empresa=' + btoa("EST") + '&fechainicio=' + btoa(FechaInicio) + '&fechatermino=' + btoa(FechaTermino) + '&fuente=HISTORICO&tipo=BajaConfirmada'  /*+ Empresa, FechaInicio, FechaTermino, Todas);*/
                        }
                        else {
                            $("#fechaTermino").focus();
                        }
                    }
                    else {
                        $("#fechaInicio").focus();
                    }
                }
                break;

            case "2":
                if (FechaInicio !== '') {
                    if (FechaTermino !== '') {
                        //
                    }
                    else {
                        $("#fechaTermino").focus();
                    }
                }
                else {
                    $("#fechaInicio").focus();
                }
                break;

            case "3":
                if (FechaInicio !== '') {
                    if (FechaTermino !== '') {
                        //
                    }
                    else {
                        $("#fechaTermino").focus();
                    }
                }
                else {
                    $("#fechaInicio").focus();
                }
                break;
        }

    });

});