import { renderDOM } from '../../modules/render.js';

const RowProceso = (proceso) => {

    const { BorderColor, Color, Prioridad, CodigoTransaccion, NombreProceso, Creado, EjecutivoName, AsignadoTo, Comentarios } = proceso;

    return renderDOM(
        `<tr class="${BorderColor} br-1-solid-200x3 bb-1-solid-200x3 bt-1-solid-200x3 mb-5">
            <td style="width: 10%;" class="ta-center">
                <span class="${Color} pdt-5 pdb-5 pdr-20 pdl-20 color-fx3" style="border-radius: 50px;">${Prioridad}</span>
            </td>
            <td style="width: 50%;">
                <a href="${window.location.origin}/Proceso/Contratos/${btoa(CodigoTransaccion)}">
                    <h1 style="font-size: 20px;" class="mt-20 color-70x3">${NombreProceso}</h1>
                    <p class="mt-5">Creado <b>${Creado}</b></p>
                    <p>Creado Por <b>${EjecutivoName}</b></p>
                    <p class="mt-15neg">Ejecutivo Cuenta <b>${EjecutivoName}</b></p>
                    <p class="mt-15neg">Asignado A <b>${AsignadoTo}</b></p>
                    <p>Comentarios Proceso: ${Comentarios}</p>
                </a>
            </td>
            <td class="ta-right pdr-10">
                <button class="btn btn-teamwork color-fx3 new-family-teamwork pdt-10 pdl-20 pdr-20 pdb-10 mb-10" style="font-weight: 100; border: none; border-radius: 50px;"
                    data-codigosolicitud="${CodigoTransaccion}" data-name="${NombreProceso}" data-type="SolicitudContratos" onclick="handleModalConfirmacionModificarNombre(this)">
                    Cambiar Nombre Proceso
                </button>
                <button class="btn btn-danger color-fx3 new-family-teamwork pdt-10 pdl-20 pdr-20 pdb-10 mb-10" style="font-weight: 100; border: none; border-radius: 50px;"
                    data-name="${NombreProceso}" data-codigosolicitud="${CodigoTransaccion}" data-type="SolicitudContratos" data-functionrechazo="handleClickAnulacionContratos"
                    onclick="handleModalConfirmacionAnulacion(this)">Rechazar Proceso</button>
            </td>
        </tr>
        `
    );
};

const RowProcesoLoading = (loaderCount) => {
    let loading = "";

    for (let i = 0; i < loaderCount; i++) {
        loading += RowProcesLoader();
    }

    return loading;
};

const RowProcesLoader = () => {
    return renderDOM(
        `
        <tr style="border-left: 5px solid rgb(200, 200, 200);" class="br-1-solid-200x3 bb-1-solid-200x3 bt-1-solid-200x3 mb-5">
            <td style="width: 10%;" class="ta-center">
                <span class="pdt-5 pdb-5 pdr-20 pdl-20 color-fx3" style="border-radius: 50px; background-color: rgb(200, 200, 200);"></span>
            </td>
            <td style="width: 50%;">
                <h1 style="font-size: 20px; height: 20px; width: 70%; background-color: rgb(200, 200, 200);" class="mt-20 color-70x3"></h1>
                <p>Creado <b>Cargando información...</b></p>
                <p class="mt-15neg">Nombre Proceso: Cargando información...<b></b></p>
                <p class="mt-15neg">Fechas de compromiso: <b>Cargando información...</b>, Con Prioridad <b>Cargando información...</b></p>
                <p>Comentarios Solicitud: Cargando información...</p>
            </td>
            <td></td>
        </tr>
        `
    );
};

export { RowProceso, RowProcesoLoading };