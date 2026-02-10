import { renderDOM } from '../../../modules/render.js';
import { json } from '../../../modules/config.js';

const config = [json][0].config;

const __APP = config.app;
const __SERVERHOST = `${config.origin}${__APP}`;

const styleButtonGeneral = `border: none; border-radius: 25px; text-align: center; font-weight: 100; align-items: center; vertical-align: middle; position: relative;`;
const classButtonGeneral = `btn new-family-teamwork`;
const styleSpanGeneral = `position: relative;`;
const styleImagenGeneral = `position: relative; left: 1%; margin-right: 3px;`;

const ButtonCenterBlock = ({
    Imagen = null,
    WidthImagen = "20",
    HeightImagen = "20",
    Width = "50",
    Height = "50",
    Class = "",
    Name = "",
    Enable = true
}) => {
    return renderDOM(
        Enable
            ? `
                <button type="button" class="${classButtonGeneral} ${Class}"
                        style="${styleButtonGeneral} width: ${Width}; height: ${Height};">
                        <img src="${__SERVERHOST}/Resources/svgnuevos/${Imagen}" width="${WidthImagen}" height="${HeightImagen}" style="${styleImagenGeneral}">
                        <span style="${styleSpanGeneral}">${Name}</span> 
                </button>
              `
            : ``
    );
};

export { ButtonCenterBlock };