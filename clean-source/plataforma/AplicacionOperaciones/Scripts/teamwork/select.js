

/** OBTENCION DINAMICA DE URL DE APLICACION PARA ACCESO A METODOS INTERNOS */

let prefixDomain = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2];
let prefix = "";

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

localStorage.setItem('applicacion__masive__clientes', '');

document.querySelector('html').addEventListener("click", (e) => {

    let show = false;

    [...e.target.classList].map(x => {
        if (x === "selectteamwork") {
            show = true;
        }
    });

    if (!show) {
        for (let i = 0; i < [document.querySelectorAll('.content__select')][0].length; i++) {
            ([document.querySelectorAll('.content__select')][0])[i].innerHTML = '';
            if (document.getElementById(document.getElementById(([document.querySelectorAll('.content__select')][0])[i].dataset.target).attributes.for.nodeValue).innerText === "") {
                document.getElementById(([document.querySelectorAll('.content__select')][0])[i].dataset.target).style.transform = "translate3d(0, 0, 0)";
                document.getElementById(([document.querySelectorAll('.content__select')][0])[i].dataset.target).style.transition = "all .15s ease-out";
            }
            
        }
    }
});

handleClickRemoveClienteMasive = (e) => {
    let masiveClientes = JSON.parse(localStorage.getItem('applicacion__masive__clientes'));
    let position = 0;
    let remove = false;
    let round = 0;

    masiveClientes.forEach(cliente => {
        if (cliente === e.dataset.value) {
            remove = true;
            position = round;

        }
        round = round + 1;
    });

    if (remove) {
        masiveClientes.splice(position, 1);
        localStorage.setItem('applicacion__masive__clientes', JSON.stringify(masiveClientes));

        const elementDeleted = document.querySelector(`#masive__cliente__${e.dataset.value}`);

        const parentNode = elementDeleted.parentNode;
        parentNode.removeChild(elementDeleted);
    }
};

handleClickOptionsSelect = (e) => {

    const masiveCheck = document.querySelector('#check__masive__clientes');

    if (e.dataset.target !== "select__cliente" || !masiveCheck.checked) {
        document.getElementById(e.dataset.target).innerHTML = e.innerHTML;
        document.getElementById(e.dataset.target).dataset.value = e.dataset.value;
    }

    if (e.dataset.target === "select__cliente" && e.value !== "ALL") {
        if (masiveCheck.checked) {
            let add = false;
            let masiveClientes = localStorage.getItem('applicacion__masive__clientes') !== ""
                ? JSON.parse(localStorage.getItem('applicacion__masive__clientes')) : [];

            masiveClientes.forEach(clientes => {
                if (clientes === e.dataset.value) {
                    add = true;
                }
            });

            if (!add) {

                const createElementMasive = document.createElement('span');
                createElementMasive.innerHTML =
                    `
                        <span class="pdt-10 pdb-10 pdl-30 pdr-60 new-family-teamwork dspl-inline-block mr-5" style="border-radius: 50px; font-weight: 100; border: 1px solid #428bca; color: #428bca; position: relative;" id="masive__cliente__${e.dataset.value}">
                            ${e.dataset.value}
                            <button class="button-trash-masive-cliente" onclick="handleClickRemoveClienteMasive(this)" class="dspl-inline-block" data-value="${e.dataset.value}"></button>
                        </span>
                    `;

                document.querySelector('#content__masive__clientes').appendChild(createElementMasive);

                masiveClientes.push(e.dataset.value);

                localStorage.setItem('applicacion__masive__clientes', JSON.stringify(masiveClientes));
            }

        }
        
    }
};

handleClickSelectHtml = (e) => {
    document.getElementById(e.dataset.target).click();
};

handleClickSelect = (e) => {

    for (let i = 0; i < [document.querySelectorAll('.content__select')][0].length; i++) {
        ([document.querySelectorAll('.content__select')][0])[i].innerHTML = '';
        if (document.getElementById(document.getElementById(([document.querySelectorAll('.content__select')][0])[i].dataset.target).attributes.for.nodeValue).innerText === "") {
            document.getElementById(([document.querySelectorAll('.content__select')][0])[i].dataset.target).style.transform = "translate3d(0, 0, 0)";
            document.getElementById(([document.querySelectorAll('.content__select')][0])[i].dataset.target).style.transition = "all .15s ease-out";
        }
    }

    let contentSelect = "";
    let ajax = false;

    switch (e.attributes.for.nodeValue) {
        case "select__estado":
            contentSelect =
                `
                <div class="list__hidden__select">
                    <label onclick="handleClickOptionsSelect(this)" class="new-family-teamwork bl-5-solid-007BFF" data-value="ALL" data-target="select__estado">Todos</label>
                    <label onclick="handleClickOptionsSelect(this)" class="new-family-teamwork bl-5-solid-calculado" data-value="PDC" data-target="select__estado">Pendiente de Calcular</label>
                    <label onclick="handleClickOptionsSelect(this)" class="new-family-teamwork bl-5-solid-calculado" data-value="CAL" data-target="select__estado">Calculados</label>
                    <label onclick="handleClickOptionsSelect(this)" class="new-family-teamwork bl-5-solid-24017378" data-value="VERPCONF" data-target="select__estado">Pendientes de Confirmar</label>
                    <label onclick="handleClickOptionsSelect(this)" class="new-family-teamwork bl-5-solid-24017378" data-value="CONF" data-target="select__estado">Pendientes de Emisión de Pago</label>
                    <label onclick="handleClickOptionsSelect(this)" class="new-family-teamwork bl-5-solid-428BCA" data-value="TEFP" data-target="select__estado">Pendientes de Pago (TEF)</label>
                    <label onclick="handleClickOptionsSelect(this)" class="new-family-teamwork bl-5-solid-428BCA" data-value="PPL90" data-target="select__estado">Pendientes de Pago (Cheques Mayores a 90 dias)</label>
                    <label onclick="handleClickOptionsSelect(this)" class="new-family-teamwork bl-5-solid-2178379" data-value="PPT90" data-target="select__estado">Pendientes de Pago (Cheques Mayores a 90 dias)</label>
                    <label onclick="handleClickOptionsSelect(this)" class="new-family-teamwork bl-5-solid-9218492" data-value="PAG" data-target="select__estado">Pendientes de Notariar</label>
                    <label onclick="handleClickOptionsSelect(this)" class="new-family-teamwork bl-5-solid-9218492" data-value="TER" data-target="select__estado">Terminados</label>
                </div>
                 `;
            break;
        case "select__doc":
            contentSelect =
                `
                <div class="list__hidden__select">
                    <label onclick="handleClickOptionsSelect(this)" class="new-family-teamwork" data-value="ALL" data-target="select__doc">Todos</label>
                    <label onclick="handleClickOptionsSelect(this)" class="new-family-teamwork" data-value="finiquito" data-target="select__doc">Finiquito</label>
                    <label onclick="handleClickOptionsSelect(this)" class="new-family-teamwork" data-value="complemento" data-target="select__doc">Complemento</label>
                </div>
                `;
            break;
        case "select__empresa":
            contentSelect =
                `
                <div class="list__hidden__select">
                    <label onclick="handleClickOptionsSelect(this)" class="new-family-teamwork" data-value="ALL" data-target="select__doc">Todos</label>
                    <label onclick="handleClickOptionsSelect(this)" class="new-family-teamwork" data-value="TWEST" data-target="select__empresa">TWEST</label>
                    <label onclick="handleClickOptionsSelect(this)" class="new-family-teamwork" data-value="TWRRHH" data-target="select__empresa">TWRRHH</label>
                    <label onclick="handleClickOptionsSelect(this)" class="new-family-teamwork" data-value="TWC" data-target="select__empresa">TWC</label>
                </div>
                `;
            break;
        case "select__cliente":
            document.getElementById(`content__${e.attributes.for.nodeValue}`).innerHTML =
                `<div class="list__hidden__select">
                    <label class="new-family-teamwork">Cargando los datos...</label>
                 </div>`;
            ajax = true;

            let xhr = new XMLHttpRequest();

            xhr.open('POST', `${prefix}/Bajas/Clientes`, true);
            xhr.overrideMimeType('text/xml; charset=UTF-8');
            xhr.setRequestHeader('X-Requested-With', 'XMLHttpRequest');

            xhr.send();

            xhr.onload = () => {
                if (xhr.status >= 200 && xhr.status < 300) {
                    const request = JSON.parse(xhr.responseText);
                    let content = "";

                    [...request].map(x => {
                        content += `<label onclick="handleClickOptionsSelect(this)" class="new-family-teamwork" data-value="${x.Codigo}" data-target="select__cliente">${x.Codigo}</label>`;
                    });

                    contentSelect =
                        `
                        <div class="list__hidden__select">
                            <label onclick="handleClickOptionsSelect(this)" class="new-family-teamwork" data-value="ALL" data-target="select__cliente">Todos</label>
                            ${content}
                        </div>`;

                    document.getElementById(`content__${e.attributes.for.nodeValue}`).innerHTML = contentSelect;

                }
            };
            break;
    }
    
    e.style.transform = "translate3d(0, -30px, 0)";
    e.style.transition = "all .15s ease-out";

    if (!ajax) {
        document.getElementById(`content__${e.attributes.for.nodeValue}`).innerHTML = contentSelect;
    }
    
};