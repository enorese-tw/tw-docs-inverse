import { renderDOM } from '../../modules/render.js';

const RowSolicitud = (solicitud, type) => {

    const { BorderColor, Color, Prioridad, NombreSolicitud, Creado, NombreProceso, FechaRealInicio, FechaRealTermino, Comentarios, CodigoSolicitud } = solicitud;

    return renderDOM(
        `
        <tr class="${BorderColor} br-1-solid-200x3 bb-1-solid-200x3 bt-1-solid-200x3 mb-5">
            <td style="width: 10%;" class="ta-center">
                <span class="${Color} pdt-5 pdb-5 pdr-20 pdl-20 color-fx3" style="border-radius: 50px;">${Prioridad}</span>
            </td>
            <td style="width: 50%;">
                <h1 style="font-size: 20px;" class="mt-20 color-70x3">${NombreSolicitud}</h1>
                <p>Creado <b>${Creado}</b></p>
                <p class="mt-15neg">Nombre Proceso: <b>${NombreProceso}</b></p>
                <p class="mt-15neg">Fechas de compromiso: <b>Fecha Inicio (${FechaRealInicio}) al Fecha Termino (${FechaRealTermino})</b>, Con Prioridad <b>${Prioridad}</b></p>
                <p>Comentarios Solicitud: ${Comentarios}</p>
            </td>
            <td class="ta-right pdr-10">
                <button class="btn btn-danger color-fx3 new-family-teamwork pdt-10 pdl-20 pdr-20 pdb-10 button__rechazarsolicitud__${type}" style="font-weight: 100; border: none; border-radius: 50px;"
                    data-name="${NombreSolicitud}" data-codigosolicitud="${CodigoSolicitud}" data-type="Solicitud${type}">
                        Rechazar Solicitud
                </button>
            </td>
        </tr>
        `
    );
};

const PaginationSolicitud = (page, type) => {

    const { Class, Rango, Filter, DataFilter, NumeroPagina, Properties } = page;

    return renderDOM(
        `
            <button style="border: none; ${Class !== "active " ? `background-color: transparent; ` : `border-radius: 50%; width: 30px; height: 30px; `}" class="${Class === "active " ? `btn-teamwork color-fx3` : ``} 
                button__pagination__${type}"
                data-page="${Rango}" data-filter="${Filter}" data-dfilter="${DataFilter}" ${Properties}>${NumeroPagina}</button>
        `
    );
};

const RowSolicitudLoading = (loaderCount) => {
    let loading = "";

    for (let i = 0; i < loaderCount; i++) {
        loading += RowSolicitudLoader();
    }

    return loading;
};

const RowSolicitudLoader = () => {
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

export { RowSolicitud, RowSolicitudLoading, PaginationSolicitud };