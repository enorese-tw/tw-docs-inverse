$(document).ready(function () {

    /** DETECCION DE NAVEGADOR EN USO CON LA APLICACION */

    var navegador = navigator.userAgent.toLowerCase();
    if (navegador.indexOf('chrome') === -1 && navegador.indexOf('firefox') === -1 && navegador.indexOf('opera') === -1) {
        $("#modalNoCompatible").modal("show");
    }
    else {
        if (navegador.indexOf('edge') > -1) {
            $("#modalNoCompatible").modal("show");
        }
    }

    /** END DETECCION DE NAVEGADOR EN USO CON LA APLICACION */

    /** OBTENCION DINAMICA DE URL DE APLICACION PARA ACCESO A METODOS INTERNOS */

    let prefixDomain = window.location.origin;
    let prefix = "";
    let controllerOwner = "";

    const { origin, pathname } = window.location;
    prefix = `${origin}/Auth`;
    controller = "Auth";
    /** END OBTENCION DINAMICA DE URL DE APLICACION PARA ACCESO A METODOS INTERNOS */
    
    let location = {};

    if (window.location.search !== "") {
        const search = [...window.location.search.split("&")];
        console.log(search);

        location.controller = search[1].replace("controller=", "");
        location.oauth = search[2].replace("oauth=", "");
        location.ref = search[3].replace("ref=", "");

        controllerOwner = `/${window.location.pathname.split("/")[window.location.pathname.split("/").length - 2]}`;
    }

    $(document).on("click", "#signin", function (e) {
        e.preventDefault();

        var cookieActive = "N";
        var token = "";

        if ($("#cookieActive").is(":checked")) {
            cookieActive = "S";
        }

        let methodAuth = "ActiveDirectory";

        if ($("#username").val().includes("@")) {
            methodAuth = 'Default';
        }
        
        ViewPartialLoadingSignIn(prefix, btoa($("#username").val()), btoa($("#password").val()), cookieActive, methodAuth, token);
    });

    $(document).on("keyup", "#username", function (e) {
        var code = e.keyCode ? e.keyCode : e.which;
        if (code === 13) {
            $("#signin").trigger("click");
        }
    });

    $(document).on("keyup", "#password", function (e) {
        var code = e.keyCode ? e.keyCode : e.which;
        if (code === 13) {
            $("#signin").trigger("click");
        }
    });

    function ajaxSignIn(controller, username, password, cookie, methodAuth, token) {
        $.ajax({
            type: 'POST',
            url: controller + '/SignIn',
            data: '{ username: "' + username + '", password: "' + password + '", cookie: "' + cookie + '", token: "' + token + '", method: "' + methodAuth + '" }',
            dataType: 'json',
            contentType: 'application/json',
            async: true,
            success: function (response) {
                if (response.Code === "200") {
                    console.log({ response }  );
                    if (window.location.search !== "") {
                        window.location.href = `${prefix.replace(controllerOwner, "")}${location.controller}/${location.oauth}/${location.ref}/Detail`;
                    }
                    else
                    {
                        window.location.href = response.PathRedirect;
                    }
                } else {
                    ViewPartialFormSignIn(controller, response.Message, atob(response.Correo));
                }
            },
            error: function (xhr) {
            },
            complete: function () {
            }
        });
    }

    function ajaxViewPartialErrorSignIn(controller, message) {
        $.ajax({
            type: 'POST',
            url: controller + '/ViewPartialErrorSignIn',
            data: '{ message: "' + message + '" }',
            dataType: 'json',
            contentType: 'application/json',
            async: true,
            error: function (xhr) {
                $("#errorSignIn").html(xhr.responseText);
            }
        });
    }

    function ViewPartialLoadingSignIn(controller, username, password, cookie, methodAuth, token) {
        $.ajax({
            type: 'POST',
            url: controller + '/ViewPartialLoadingSignIn',
            data: '{ }',
            dataType: 'json',
            contentType: 'application/json',
            async: true,
            error: function (xhr) {
                $("#contentLogin").html(xhr.responseText);
                ajaxSignIn(controller, username, password, cookie, methodAuth, token);
            }
        });
    }

    function ViewPartialFormSignIn(controller, message, correo) {
        $.ajax({
            type: 'POST',
            url: controller + '/ViewPartialFormSignIn',
            data: '{ }',
            dataType: 'json',
            contentType: 'application/json',
            async: true,
            error: function (xhr) {
                $("#contentLogin").html(xhr.responseText);
                $("#username").val(correo);
                ajaxViewPartialErrorSignIn(controller, message);
            }
        });
    }
    
});