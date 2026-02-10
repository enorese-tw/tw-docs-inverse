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

handleClickSubmit = () => {

    let inputValidate = [];

    //for (let i = 0; i < [document.getElementsByClassName('form__native__survey')].length; i++) {

    //}


    const rut = document.getElementById("input__dni").value;
    const name = document.getElementById("input__name").value;
    const nativeSurname = document.getElementById("input__nativesurname").value;
    const surname = document.getElementById("input__surname").value;
    const email = document.getElementById("input__email").value;
    const birthdate = document.getElementById("datepicker-desde-input").value;
    const phone = document.getElementById("input__phone").value;
    const address = document.getElementById("input__address").value;
    const comuna = document.getElementById("input__comuna").value;

    let xhr = new XMLHttpRequest();
    let __form = new FormData();

    __form.append("rut", rut);
    __form.append("nombres", name);
    __form.append("apellidoPaterno", nativeSurname);
    __form.append("apellidoMaterno", surname);
    __form.append("correo", email);
    __form.append("fechaNacimiento", birthdate);
    __form.append("telefono", phone);
    __form.append("direccion", address);
    __form.append("comuna", comuna);

    xhr.open('POST', `${prefix}PostulanteCreaOActualizaFichaPostulante`, true);
    xhr.overrideMimeType('text/xml; charset=UTF-8');
    xhr.setRequestHeader('X-Requested-With', 'XMLHttpRequest');

    xhr.send(__form);

    xhr.onload = () => {
        if (xhr.status >= 200 && xhr.status < 300) {

            const json = JSON.parse(xhr.responseText);

            [...json].map(x => {
                if (x.Code === "200") {

                    document.getElementById("content__ficha__postulante").innerHTML = 'Cargando los datos del formulario';
                    
                    xhr.open('POST', `${prefix}PostulanteConsultaFieldEncuesta`, true);
                    xhr.overrideMimeType('text/xml; charset=UTF-8');
                    xhr.setRequestHeader('X-Requested-With', 'XMLHttpRequest');

                    xhr.send();

                    xhr.onload = () => {

                        const json = JSON.parse(xhr.responseText);
                        let contentFieldEncuesta = '';


                        [...json].map(x => {
                            console.log(x);
                            contentFieldEncuesta +=
                                `
                                <div class="ml-auto mr-auto mt-50" style="width: 100%; vertical-align: top;">
                                    <input type="${x.Type}" class="teamwork__input new-family-teamwork" ${x.Required === 'S' ? `required` : ``} id="input__${x.Name}" onclick="handleClickInput(this)" onfocus="handleClickInput(this)" onblur="handleBlurInput(this)" data-target="legend__${x.Name}" />
                                    <label class="new-family-teamwork teamwork__legend" id="legend__${x.Name}" for="input__${x.Name}">${x.Placeholder}</label>
                                </div>
                                `;

                        });

                        document.getElementById("content__ficha__postulante").innerHTML =
                            `
                                ${contentFieldEncuesta}
                            ` ;

                    };
                }
            });
        }
    };

};

handleChangeTipoDNI = (e) => {
    const tiposDNI = [document.getElementsByName('tipoDNI')];
    for (let i = 0; i < tiposDNI[0].length; i++) {
        document.getElementById(tiposDNI[0][i].id).removeAttribute("checked");
    }

    e.setAttribute("checked", "");
};

document.getElementById("input__dni").addEventListener("blur", (e) => {
    
    const regValDNI = /[0-9]{7,8}-[0-9K]{1}/;
    let valid = true;

    let xhr = new XMLHttpRequest();
    let __form = new FormData();
    let checkedTipoDNI = "";
    const tiposDNI = [document.getElementsByName('tipoDNI')];

    for (let i = 0; i < tiposDNI[0].length; i++) {
        if (document.getElementById(tiposDNI[0][i].id).attributes.checked !== undefined) {
            checkedTipoDNI = document.getElementById(tiposDNI[0][i].id).defaultValue;
        }
    }

    if (checkedTipoDNI === "Rut") {
        if (!regValDNI.exec(e.target.value)) {
            e.target.setAttribute("novalid", "");
            valid = false;
        }
    }

    //xhr.open('GET', 'https://tinyurl.com/api-create.php?url=http://186.103.146.11:82/AplicacionOperaciones/Auth', true)

    //xhr.send();

    //xhr.onload = () => {
    //    console.log(xhr.responseText);
    //}

    if (valid) {
        document.getElementById('error__dni').innerHTML =
            `Estamos validando los datos, espere...`;

        __form.append("DNI", e.target.value);
        __form.append("tipoDNI", checkedTipoDNI);

        xhr.open('POST', `${prefix}PostulanteValidaFichaPersonal`, true);
        xhr.overrideMimeType('text/xml; charset=UTF-8');
        xhr.setRequestHeader('X-Requested-With', 'XMLHttpRequest');

        xhr.send(__form);

        xhr.onload = () => {
            if (xhr.status >= 200 && xhr.status < 300) {
                const json = JSON.parse(xhr.responseText);
                [...json].map(x => {
                    if (x.Code === "200") {
                        document.getElementById(e.target.dataset.error).innerHTML = "";

                        /** Obtención de ficha personal si existiera. */

                        document.getElementById('error__dni').innerHTML =
                            `Se estan buscando los datos, espere...`;

                        __form.append("DNI", e.target.value);

                        xhr.open('POST', `${prefix}PostulanteConsultaFichaPersonal`, true);
                        xhr.overrideMimeType('text/xml; charset=UTF-8');
                        xhr.setRequestHeader('X-Requested-With', 'XMLHttpRequest');

                        xhr.send(__form);

                        xhr.onload = () => {

                            if (xhr.status >= 200 && xhr.status < 300) {

                                const postulante = JSON.parse(xhr.responseText);

                                [...postulante].map(x => {
                                    document.getElementById('input__name').value = x.Nombre;
                                    document.getElementById('input__nativesurname').value = x.ApellidoPaterno;
                                    document.getElementById('input__surname').value = x.ApellidoMaterno;
                                    document.getElementById('input__email').value = x.Correo;
                                    document.getElementById('datepicker-desde-input').value = x.FechaNacimiento;
                                    document.getElementById('input__phone').value = x.Telefono;
                                    document.getElementById('input__address').value = x.Direccion;
                                    document.getElementById('input__comuna').value = x.Comuna;

                                    document.getElementById('input__comuna').focus();
                                    document.getElementById('input__address').focus();
                                    document.getElementById('input__phone').focus();
                                    document.getElementById('datepicker-desde-input').focus();
                                    document.getElementById('input__email').focus();
                                    document.getElementById('input__surname').focus();
                                    document.getElementById('input__nativesurname').focus();
                                    document.getElementById('input__name').focus();

                                    document.getElementById('error__dni').innerHTML =
                                        ``;
                                });

                            }

                        };

                    }
                    else
                    {
                        document.getElementById('input__name').value = '';
                        document.getElementById('input__nativesurname').value = '';
                        document.getElementById('input__surname').value = '';
                        document.getElementById('input__email').value = '';
                        document.getElementById('datepicker-desde-input').value = '';
                        document.getElementById('input__phone').value = '';
                        document.getElementById('input__address').value = '';
                        document.getElementById('input__comuna').value = '';

                        document.getElementById(e.target.dataset.error).innerHTML = `${x.Message}`;
                        document.getElementById(e.target.dataset.error).style.color = "#f00";
                        document.getElementById(e.target.id).style.color = "#f00";
                        document.getElementById(e.target.id).style.borderBottom = "1px solid #f00";
                        document.getElementById(e.target.dataset.target).style.color = "#f00";
                    }
                });
                
            }
        };
    }
    else {
        document.getElementById('input__name').value = '';
        document.getElementById('input__nativesurname').value = '';
        document.getElementById('input__surname').value = '';
        document.getElementById('input__email').value = '';
        document.getElementById('datepicker-desde-input').value = '';
        document.getElementById('input__phone').value = '';
        document.getElementById('input__address').value = '';
        document.getElementById('input__comuna').value = '';

        document.getElementById('input__comuna').focus();
        document.getElementById('input__address').focus();
        document.getElementById('input__phone').focus();
        document.getElementById('datepicker-desde-input').focus();
        document.getElementById('input__email').focus();
        document.getElementById('input__surname').focus();
        document.getElementById('input__nativesurname').focus();
        document.getElementById('input__name').focus();

        document.getElementById(e.target.dataset.error).innerHTML = 'El formato del Rut no es el correcto (Ejemplo: 12345678-9)';
        document.getElementById(e.target.dataset.error).style.color = "#f00";
        document.getElementById(e.target.id).style.color = "#f00";
        document.getElementById(e.target.id).style.borderBottom = "1px solid #f00";
        document.getElementById(e.target.dataset.target).style.color = "#f00";
    }
});