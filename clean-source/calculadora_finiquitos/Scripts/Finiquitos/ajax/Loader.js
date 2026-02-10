$.fn.loader = function (options) {
    if (options == undefined) {
        var $this = this,
            configurations = {
                componentDestino: this,
                ajax: true,
                action: "init"
            },
            settings = $.extend(configurations, options);
    } else {
        var $this = this,
            configurations = {
                componentDestino: this,
                ajax: true,
                action: options
            },
            settings = $.extend(configurations, options);
    }
    var cortina = $(".loader-cortina");
    var funciones = {}

    if (configurations.ajax) {

        if (configurations.action === "init") {
            funciones.init = function () {
                configurations.componentDestino.html('<div class="loader-cortina"><div class="loader"><div class="loader-tw-long"></div><img src="Images/logo-teamwork-chile.png" width="150" style="margin-top: 20px;" /><p class="loading-text">Estamos cargando la información...</p></div></div>');
                cortina.show();
            };
        }

        if (configurations.action === "stop") {
            funciones.stop = function () {
                cortina.fadeOut(500);
                configurations.componentDestino.html("");
            };
        }
    }

    return funciones[configurations.action]();
}