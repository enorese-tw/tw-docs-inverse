import { renderDOM } from '../../../modules/render.js';
import { json } from '../../../modules/config.js';

const config = [json][0].config;

const __APP = config.app;
const __SERVERHOST = `${config.origin}${__APP}`;

const renderBodyConsulta = () => {
    return renderDOM(
        `
            <div class="mt-100 ml-auto mr-auto" style="width: 92%;">
                <h1 class="new-family-teamwork" style="color: rgb(150, 150, 150); font-weight: 100;">Consulta de Datos Trabajador</h1>
                <div class="container__ficha__personal">

                </div>
                <div class="mb-100">
                    <h1 class="new-family-teamwork" style="font-weight: bold; font-size: 40px; color: rgb(100, 100, 100);">Historial de Contrataciones</h1>
                    <div>
                        <table style="width: 100%;">
                            <thead>
                                <tr>
                                    <td style="text-align: center; padding-top: 10px; padding-bottom: 10px; border-bottom: 1px solid rgb(200, 200, 200); color: rgb(70, 70, 70);"></td>
                                    <td style="text-align: center; padding-top: 10px; padding-bottom: 10px; border-bottom: 1px solid rgb(200, 200, 200); color: rgb(70, 70, 70);">Cliente</td>
                                    <td style="text-align: center; padding-top: 10px; padding-bottom: 10px; border-bottom: 1px solid rgb(200, 200, 200); color: rgb(70, 70, 70);">Ficha</td>
                                    <td style="text-align: center; padding-top: 10px; padding-bottom: 10px; border-bottom: 1px solid rgb(200, 200, 200); color: rgb(70, 70, 70);">Empresa Mandante</td>
                                    <td style="text-align: center; padding-top: 10px; padding-bottom: 10px; border-bottom: 1px solid rgb(200, 200, 200); color: rgb(70, 70, 70);">Fecha Inicio</td>
                                    <td style="text-align: center; padding-top: 10px; padding-bottom: 10px; border-bottom: 1px solid rgb(200, 200, 200); color: rgb(70, 70, 70);">Fecha Termino</td>
                                    <td style="text-align: center; padding-top: 10px; padding-bottom: 10px; border-bottom: 1px solid rgb(200, 200, 200); color: rgb(70, 70, 70);">Cargo Mod</td>
                                    <td style="text-align: center; padding-top: 10px; padding-bottom: 10px; border-bottom: 1px solid rgb(200, 200, 200); color: rgb(70, 70, 70);">Causal</td>
                                    <td style="text-align: center; padding-top: 10px; padding-bottom: 10px; border-bottom: 1px solid rgb(200, 200, 200); color: rgb(70, 70, 70);"></td>
                                    <td style="text-align: center; padding-top: 10px; padding-bottom: 10px; border-bottom: 1px solid rgb(200, 200, 200); color: rgb(70, 70, 70);"></td>
                                </tr>
                            </thead>
                            <tbody class="container__contrato__personal">

                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        `
    );
};

const renderHeaderConsulta = () => {
    return renderDOM(
        `
            <div style="font-weight: 100; display: inline-flex; align-items: center; vertical-align: middle; width: 100%; height: 100vh;">
                <div class="ml-auto mr-auto" style="width: 90%;">
                    <h1 class="new-family-teamwork pdb-50 pdt-50" style="color: rgb(100, 100, 100); font-weight: bold; display: block; width: 100%; text-align: center; font-size: 40px;">Consulta de Datos Trabajador</h1>
                    <div style="width: calc(100% / 4 - 5px); display: inline-block;">
                        <label class="label-teamwork-report" for="data_filter">¿Qué filtro desea buscar?</label>
                        <select class="pdt-20 pdb-20 type__filter" style="display: block; width: 95%; border-left: none; border-top: none; border-right: none; outline: none; border-bottom: 1px solid rgb(150, 150, 150);">
                            <option value="Rut" selected>Rut</option>
                            <option value="Ficha">Ficha</option>
                        </select>
                    </div>
                    <div class="container__rut" style="width: calc(100% / 2 - 5px); display: inline-block;">
                        <label class="label-teamwork-report" for="data_filter">¿Qué rut esta buscando?</label>
                        <input type="text" value="" id="rut__filter" placeholder="Ejemplo (12345678-9)" class="pdt-20 pdb-20" style="text-align: center; display: block; width: 95%; border-left: none; border-top: none; border-right: none; outline: none; border-bottom: 1px solid rgb(150, 150, 150);" />
                    </div>
                    <div class="container__ficha" style="width: calc(100% / 4 - 5px); display: none;">
                        <label class="label-teamwork-report" for="data_filter">¿Qué ficha esta Buscando?</label>
                        <input type="text" value="" id="ficha__filter" class="pdt-20 pdb-20" style="display: block; width: 95%; border-left: none; border-top: none; border-right: none; outline: none; border-bottom: 1px solid rgb(150, 150, 150);" />
                    </div>
                    <div class="container__ficha" style="width: calc(100% / 4 - 5px); display: none;">
                        <label class="label-teamwork-report" for="data_filter">¿Qué empresa mandante?</label>
                        <select class="pdt-20 pdb-20" id="empresa__filter" style="display: block; width: 95%; border-left: none; border-top: none; border-right: none; outline: none; border-bottom: 1px solid rgb(150, 150, 150);">
                            <option></option>
                            <option>TWEST</option>
                            <option>TWRRHH</option>
                            <option>TWC</option>
                        </select>
                    </div>
                    <div style="width: calc(100% / 4 - 5px); display: inline-block;">
                        <button class="btn btn-teamwork pdt-25 pdb-25 color-fx3 button__consultar__datos" style="border-radius: 100px; width: 100%;">
                            <img src="${__SERVERHOST}/Resources/buscar.svg" width="20" height="20">
                            <span>Consultar Datos</span>
                        </button>
                    </div>
                </div>
            </div>
            
        `
    );
};

const renderLoading = () => {
    return renderDOM(
        `
            <div style="text-align: center;">
                <div class="holder">
                    <div class="preloader"><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div></div>
                </div>
            </div>
            
        `
    );
};

const renderLoadingTable = (colspan = null) => {
    return renderDOM(
        `
            <tr>
                <td ${colspan !== null ? `colspan="${colspan}"` : ``} class="pdt-10 pdb-10" style="text-align: center;">
                    <div class="holder">
                        <div class="preloader"><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div></div>
                    </div>
                </td>
            </tr>
            
        `
    );
};

const renderContrato = (contrato) => {

    const {Ficha,
        Empresa,
        Cliente,
        CargoMod,
        CodigoSucursal,
        NombreSucursal,
        DireccionSucursal,
        ComunaSucursal,
        CiudadSucursal,
        RegionSucursal,
        Causal,
        TopeLegal,
        FechaInicioContrato,
        FechaTerminoContrato,
        HorasSemanales,
        Horarios,
        Colacion,
        TipoReemplazo,
        Reemplazo,
        TipoMovimiento,
        NombreCargo,
        Tag } = contrato;

    return renderDOM(
        `
            <tr>
                <td style="text-align: center; padding-top: 20px; padding-bottom: 10px;"><span class="success color-fx3 pdl-10 pdr-10 pdb-5 pdt-5" style="border-radius: 5px;">Contrato</span></td>
                <td style="text-align: center; padding-top: 20px; padding-bottom: 10px;">${Cliente}</td>
                <td style="text-align: center; padding-top: 20px; padding-bottom: 10px;">${Ficha}</td>
                <td style="text-align: center; padding-top: 20px; padding-bottom: 10px;">${Empresa}</td>
                <td style="text-align: center; padding-top: 20px; padding-bottom: 10px;">${FechaInicioContrato}</td>
                <td style="text-align: center; padding-top: 20px; padding-bottom: 10px;">${FechaTerminoContrato}</td>
                <td style="text-align: center; padding-top: 20px; padding-bottom: 10px;">${CargoMod}</td>
                <td style="text-align: center; padding-top: 20px; padding-bottom: 10px;">${Causal}</td>
                <td style="text-align: center; padding-top: 20px; padding-bottom: 10px;">
                    ${Tag}
                </td>
                <td class="pdt-10">
                    <button type="submit" class="dspl-inline-block btn btn-teamwork new-family-teamwork color-fx3 button__consulta__contrato"
                            style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; "
                                data-ficha="${Ficha}" data-empresa="${Empresa}">
                        <img src="${__SERVERHOST}/Resources/svgnuevos/escritura.svg" width="20" height="20" style="position: relative; left: 1%; margin-top: -5%; margin-right: 5px;"
                                data-ficha="${Ficha}" data-empresa="${Empresa}">
                        <span style="position: relative; float: right;"  data-ficha="${Ficha}" data-empresa="${Empresa}">Detalle</span>
                    </button>
                </td>
            </tr>
            <tr>
                <td colspan="2"></td>
                <td colspan="7">
                    <table style="width: 100%;">
                        <thead class="container__add__baja__${Ficha}">
                        
                        </thead>
                        <tbody class="container__add__renov__${Ficha}">
                            
                        </tbody>
                    </table>
                </td>
            </tr>
        `
    );

};

const renderContratoFull = (contrato) => {

    const { Ficha,
        Empresa,
        Cliente,
        CargoMod,
        CodigoSucursal,
        NombreSucursal,
        DireccionSucursal,
        ComunaSucursal,
        CiudadSucursal,
        RegionSucursal,
        Causal,
        TopeLegal,
        FechaInicioContrato,
        FechaTerminoContrato,
        HorasSemanales,
        Horarios,
        Colacion,
        TipoReemplazo,
        Reemplazo,
        TipoMovimiento,
        NombreCargo } = contrato;

    return renderDOM(
        `
            <div style="width: 90%;" class="ml-auto mr-auto pdt-20 pdb-20">
                <h1 class="new-family-teamwork pdt-10 pdb-10" style="color: rgb(150, 150, 150); font-weight: bold;">${Ficha}</h1>
                <div class="pdb-20">
                    <div style="width: calc(100% / 3 - 10px); display: inline-block; vertical-align: top;">
                        <label style="color: rgb(100, 100, 100);">Empresa Mandante</label>
                        <p>${Empresa}</p>
                    </div>
                    <div style="width: calc(100% / 3 - 10px); display: inline-block; vertical-align: top;">
                        <label style="color: rgb(100, 100, 100);">Cliente</label>
                        <p>${Cliente}</p>
                    </div>
                </div>
                <h2 style="color: rgb(150, 150, 150); font-size: 25px;">Fechas de Contrato</h2>
                <div class="pdb-20">
                    <div style="width: calc(100% / 4 - 10px); display: inline-block; vertical-align: top;">
                        <label style="color: rgb(100, 100, 100);">Fecha Inicio</label>
                        <p>${FechaInicioContrato}</p>
                    </div>
                    <div style="width: calc(100% / 4 - 10px); display: inline-block; vertical-align: top;">
                        <label style="color: rgb(100, 100, 100);">Fecha Termino</label>
                        <p>${FechaTerminoContrato}</p>
                    </div>
                    <div style="width: calc(100% / 4 - 10px); display: inline-block; vertical-align: top;">
                        <label style="color: rgb(100, 100, 100);">Tope Legal</label>
                        <p>${TopeLegal}</p>
                    </div>
                    <div style="width: calc(100% / 4 - 10px); display: inline-block; vertical-align: top;">
                        <label style="color: rgb(100, 100, 100);">Causal</label>
                        <p>${Causal}</p>
                    </div>
                </div>
                <div class="pdb-20">
                    <div style="width: calc(100% / 3 - 10px); display: inline-block; vertical-align: top;">
                        <label style="color: rgb(100, 100, 100);">Tipo Movimiento</label>
                        <p>${TipoMovimiento}</p>
                    </div>
                    <div style="width: calc(100% / 3 - 10px); display: inline-block; vertical-align: top;">
                        <label style="color: rgb(100, 100, 100);">Tipo Reemplazo</label>
                        <p>${TipoReemplazo}</p>
                    </div>
                    <div style="width: calc(100% / 3 - 10px); display: inline-block; vertical-align: top;">
                        <label style="color: rgb(100, 100, 100);">Reemplazo</label>
                        <p>${Reemplazo}</p>
                    </div>
                </div>
                <h2 style="color: rgb(150, 150, 150); font-size: 25px;">Cargo Mod y función</h2>
                <div class="pdb-20">
                    <div style="width: calc(100% / 2 - 10px); display: inline-block; vertical-align: top;">
                        <label style="color: rgb(100, 100, 100);">Cargo Mod</label>
                        <p>${CargoMod}</p>
                    </div>
                    <div style="width: calc(100% / 2 - 10px); display: inline-block; vertical-align: top;">
                        <label style="color: rgb(100, 100, 100);">Cargo</label>
                        <p>${NombreCargo}</p>
                    </div>
                </div>
                <div class="pdb-20">
                    <div style="width: 100%; display: inline-block; vertical-align: top;">
                        <label style="color: rgb(100, 100, 100);">Horarios</label>
                        <p>${Horarios}</p>
                    </div>
                </div>
                <div class="pdb-20">
                    <div style="width: calc(100% / 2 - 10px); display: inline-block; vertical-align: top;">
                        <label style="color: rgb(100, 100, 100);">Horas Semanales</label>
                        <p>${HorasSemanales}</p>
                    </div>
                    <div style="width: calc(100% / 2 - 10px); display: inline-block; vertical-align: top;">
                        <label style="color: rgb(100, 100, 100);">Coleción Minutos</label>
                        <p>${Colacion}</p>
                    </div>
                </div>
                
                <h2 style="color: rgb(150, 150, 150); font-size: 25px;">Dependencias de Trabajo</h2>
                <div class="pdb-20">
                    <div style="width: calc(100% / 2 - 10px); display: inline-block; vertical-align: top;">
                        <label style="color: rgb(100, 100, 100);">Codigo Sucursal/Tienda</label>
                        <p>${CodigoSucursal}</p>
                    </div>
                    <div style="width: calc(100% / 2 - 10px); display: inline-block; vertical-align: top;">
                        <label style="color: rgb(100, 100, 100);">Nombre Sucursal/Tienda</label>
                        <p>${NombreSucursal}</p>
                    </div>
                </div>
                <div class="pdb-20">
                    <div style="width: 100%; display: inline-block; vertical-align: top;">
                        <label style="color: rgb(100, 100, 100);">Direccion Sucursal/Tienda</label>
                        <p>${DireccionSucursal}</p>
                    </div>
                </div>
                <div class="pdb-20">
                    <div style="width: calc(100% / 3 - 10px); display: inline-block; vertical-align: top;">
                        <label style="color: rgb(100, 100, 100);">Comuna Sucursal/Tienda</label>
                        <p>${ComunaSucursal}</p>
                    </div>
                    <div style="width: calc(100% / 3 - 10px); display: inline-block; vertical-align: top;">
                        <label style="color: rgb(100, 100, 100);">Ciudad Sucursal/Tienda</label>
                        <p>${CiudadSucursal}</p>
                    </div>
                    <div style="width: calc(100% / 3 - 10px); display: inline-block; vertical-align: top;">
                        <label style="color: rgb(100, 100, 100);">Región Sucursal/Tienda</label>
                        <p>${RegionSucursal}</p>
                    </div>
                </div>
                
            </div>
        `
    );

};

const renderRenovacion = (renovaciones) => {

    const { FechaInicio, FechaTermino, Numero } = renovaciones;
    
    return renderDOM(
        `
            <tr>
                <td><img src="${__SERVERHOST}/Resources/svgnuevos/right-arrow.svg" width="30" /></td>
                <td style="text-align: center; padding-top: 10px; padding-bottom: 10px;"><span class="teamwork color-fx3 pdl-10 pdr-10 pdb-5 pdt-5" style="border-radius: 5px;">Renovación</span></td>
                <td style="text-align: center; padding-top: 10px; padding-bottom: 10px;">${Numero}</td>
                <td style="text-align: center; padding-top: 10px; padding-bottom: 10px;">${FechaInicio} </td>
                <td style="text-align: center; padding-top: 10px; padding-bottom: 10px;">${FechaTermino}</td>
            </tr>
        `
    );
};

const renderBaja = (baja) => {

    const { Estado, PoderDe } = baja;

    return renderDOM(
        `
            <tr>
                <td><img src="${__SERVERHOST}/Resources/svgnuevos/right-arrow.svg" width="30" /></td>
                <td style="text-align: center; padding-top: 10px; padding-bottom: 10px;"><span class="danger color-fx3 pdl-10 pdr-10 pdb-5 pdt-5" style="border-radius: 5px;">Baja</span></td>
                <td style="text-align: center; padding-top: 10px; padding-bottom: 10px;">${Estado}</td>
                <td style="text-align: center; padding-top: 10px; padding-bottom: 10px;">${PoderDe}</td>
                <td style="text-align: center; padding-top: 10px; padding-bottom: 10px;"></td>
            </tr>
        `
    );
};

const renderFichaPersonal = (fichapersonal, filter = "", ficha = "", empresa = "", rut = "") => {

    const {
        Rut, Nombres, Nombre, ApellidoPaterno, ApellidoMaterno, Direccion, Comuna, Ciudad, Genero, Correo, Telefono, FechaNacimiento, EstadoCivil, Nacionalidad, Afp, Salud, MontoIsapre, TipoPago, Banco, TipoCuenta, NumeroCuenta, CargasFamiliares, Tramo, ValorTramo, Visa, VencimientoVisa
    } = fichapersonal;

    return renderDOM(
        `
            <div>
                <div style="width: calc(100% / 2 - 10px); display: inline-block; vertical-align: top;">
                    <div>
                        <button type="submit" class="dspl-inline-block btn btn-teamwork new-family-teamwork color-fx3 button__consultar__ficha"
                                style="min-width: 50px; height: 50px; border: none; border-radius: 25px; text-align: center; font-weight: 100; "
                                data-filter="${filter}" data-ficha="${ficha}" data-empresa="${empresa}" data-rut="${rut}">
                            <img src="${__SERVERHOST}/Resources/svgnuevos/red-de-personas.svg" width="20" height="20" style="position: relative; left: 1%; margin-top: -5%; margin-right: 5px;"
                                 data-filter="${filter}" data-ficha="${ficha}" data-empresa="${empresa}" data-rut="${rut}">
                            <span style="position: relative; float: right;" data-filter="${filter}" data-ficha="${ficha}" data-empresa="${empresa}" data-rut="${rut}">Ficha Completa</span>
                        </button>
                        <h1 class="new-family-teamwork" style="color: rgb(150, 150, 150); font-weight: bold;">${Rut}</h1>
                        <h1 class="new-family-teamwork mt-5neg" style="color: rgb(150, 150, 150); font-weight: bold;">${Nombres}</h1>
                    </div>
                    <div class="pdb-20 mt-50">
                        <div style="width: calc(100% / 3 - 10px); display: inline-block; vertical-align: top;">
                             <label style="color: rgb(100, 100, 100);">Dirección Trabajador</label>
                             <p>${Direccion}</p>
                        </div>
                        <div style="width: calc(100% / 3 - 10px); display: inline-block; vertical-align: top;">
                            <label style="color: rgb(100, 100, 100);">Comuna Trabajador</label>
                            <p>${Comuna}</p>
                        </div>
                        <div style="width: calc(100% / 3 - 10px); display: inline-block; vertical-align: top;">
                            <label style="color: rgb(100, 100, 100);">Ciudad Trabajador</label>
                            <p>${Ciudad}</p>
                        </div>
                    </div>   
                    <div class="pdb-20">
                        <div style="width: calc(100% / 4 - 10px); display: inline-block; vertical-align: top;">
                            <label style="color: rgb(100, 100, 100);">Fecha de Nacimiento</label>
                            <p>${FechaNacimiento}</p>
                        </div>
                        <div style="width: calc(100% / 4 - 10px); display: inline-block; vertical-align: top;">
                             <label style="color: rgb(100, 100, 100);">Estado Civil</label>
                             <p>${EstadoCivil}</p>
                        </div>
                        <div style="width: calc(100% / 4 - 10px); display: inline-block; vertical-align: top;">
                             <label style="color: rgb(100, 100, 100);">Nacionalidad</label>
                             <p>${Nacionalidad}</p>
                        </div>
                        <div style="width: calc(100% / 4 - 10px); display: inline-block; vertical-align: top;">
                             <label style="color: rgb(100, 100, 100);">Genero</label>
                             <p>${Genero}</p>
                        </div>
                    </div>   
                </div>
                <div style="width: calc(100% / 2 - 10px); display: inline-block; vertical-align: top;">
                    <h2 style="color: rgb(150, 150, 150); font-size: 25px;">Información de Contacto Trabajador</h2>
                    <div class="pdb-20">
                        <div style="width: calc(100% / 2 - 10px); display: inline-block; vertical-align: top;">
                             <label style="color: rgb(100, 100, 100);">Correo</label>
                             <p>${Correo}</p>
                        </div>
                        <div style="width: calc(100% / 2 - 10px); display: inline-block; vertical-align: top;">
                            <label style="color: rgb(100, 100, 100);">Telefono</label>
                            <p>${Telefono}</p>
                        </div>
                    </div>
                    <div class="pdb-20">
                        <div style="width: calc(100% / 2 - 10px); display: inline-block; vertical-align: top;">
                            <label style="color: rgb(100, 100, 100);">Tipo de Pago</label>
                            <p>${TipoPago}</p>
                        </div>
                        <div style="width: calc(100% / 2 - 10px); display: inline-block; vertical-align: top;">
                             <label style="color: rgb(100, 100, 100);">Banco</label>
                             <p>${Banco}</p>
                        </div>
                    </div>
                    <div>
                        <div style="width: calc(100% / 2 - 10px); display: inline-block; vertical-align: top;">
                             <label style="color: rgb(100, 100, 100);">Tipo de Cuenta</label>
                             <p>${TipoCuenta}</p>
                        </div>
                        <div style="width: calc(100% / 2 - 10px); display: inline-block; vertical-align: top;">
                             <label style="color: rgb(100, 100, 100);">Número de Cuenta</label>
                             <p>${NumeroCuenta}</p>
                        </div>
                    </div> 
                </div>
                
            </div>
        `
    );
};

const renderFichaCompleta = (fichapersonal) => {

    const {
        Rut, Nombres, Nombre, ApellidoPaterno, ApellidoMaterno, Direccion, Comuna, Ciudad, Genero, Correo, Telefono, FechaNacimiento, EstadoCivil, Nacionalidad, Afp, Salud, MontoIsapre, TipoPago, Banco, TipoCuenta, NumeroCuenta, CargasFamiliares, Tramo, ValorTramo, Visa, VencimientoVisa
    } = fichapersonal;

    return renderDOM(
        `
            <div style="width: 90%;" class="ml-auto mr-auto pdt-20 pdb-20">
                <div>
                    <h1 class="new-family-teamwork" style="color: rgb(150, 150, 150); font-weight: bold;">${Rut}</h1>
                    <h1 class="new-family-teamwork mt-5neg" style="color: rgb(150, 150, 150); font-weight: bold;">${Nombres}</h1>
                </div>
                <div class="pdb-20">
                    <div style="width: calc(100% / 3 - 10px); display: inline-block; vertical-align: top;">
                        <label style="color: rgb(100, 100, 100);">Nombre</label>
                        <p>${Nombre}</p>
                    </div>
                    <div style="width: calc(100% / 3 - 10px); display: inline-block; vertical-align: top;">
                        <label style="color: rgb(100, 100, 100);">Apellido Paterno</label>
                        <p>${ApellidoPaterno}</p>
                    </div>
                    <div style="width: calc(100% / 3 - 10px); display: inline-block; vertical-align: top;">
                        <label style="color: rgb(100, 100, 100);">Apellido Materno</label>
                        <p>${ApellidoMaterno}</p>
                    </div>
                </div>
                <h2 style="color: rgb(150, 150, 150); font-size: 25px;">Domicilio Trabajador</h2>
                <div class="pdb-20">
                    <div style="width: calc(100% / 3 - 10px); display: inline-block; vertical-align: top;">
                         <label style="color: rgb(100, 100, 100);">Dirección Trabajador</label>
                         <p>${Direccion}</p>
                    </div>
                    <div style="width: calc(100% / 3 - 10px); display: inline-block; vertical-align: top;">
                        <label style="color: rgb(100, 100, 100);">Comuna Trabajador</label>
                        <p>${Comuna}</p>
                    </div>
                    <div style="width: calc(100% / 3 - 10px); display: inline-block; vertical-align: top;">
                        <label style="color: rgb(100, 100, 100);">Ciudad Trabajador</label>
                        <p>${Ciudad}</p>
                    </div>
                </div>       
                <h2 style="color: rgb(150, 150, 150); font-size: 25px;">Información de Contacto Trabajador</h2>
                <div class="pdb-20">
                    <div style="width: calc(100% / 2 - 10px); display: inline-block; vertical-align: top;">
                         <label style="color: rgb(100, 100, 100);">Correo</label>
                         <p>${Correo}</p>
                    </div>
                    <div style="width: calc(100% / 2 - 10px); display: inline-block; vertical-align: top;">
                        <label style="color: rgb(100, 100, 100);">Telefono</label>
                        <p>${Telefono}</p>
                    </div>
                </div>     
                <h2 style="color: rgb(150, 150, 150); font-size: 25px;">Otra información del trabajador</h2>
                <div class="pdb-20">
                    <div style="width: calc(100% / 4 - 10px); display: inline-block; vertical-align: top;">
                        <label style="color: rgb(100, 100, 100);">Fecha de Nacimiento</label>
                        <p>${FechaNacimiento}</p>
                    </div>
                    <div style="width: calc(100% / 4 - 10px); display: inline-block; vertical-align: top;">
                         <label style="color: rgb(100, 100, 100);">Estado Civil</label>
                         <p>${EstadoCivil}</p>
                    </div>
                    <div style="width: calc(100% / 4 - 10px); display: inline-block; vertical-align: top;">
                         <label style="color: rgb(100, 100, 100);">Nacionalidad</label>
                         <p>${Nacionalidad}</p>
                    </div>
                    <div style="width: calc(100% / 4 - 10px); display: inline-block; vertical-align: top;">
                         <label style="color: rgb(100, 100, 100);">Genero</label>
                         <p>${Genero}</p>
                    </div>
                </div>   
                <h2 style="color: rgb(150, 150, 150); font-size: 25px;">Afiliación y Salud</h2>
                 <div class="pdb-20">
                    <div style="width: calc(100% / 3 - 10px); display: inline-block; vertical-align: top;">
                         <label style="color: rgb(100, 100, 100);">Afp</label>
                         <p>${Afp}</p>
                    </div>
                    <div style="width: calc(100% / 3 - 10px); display: inline-block; vertical-align: top;">
                        <label style="color: rgb(100, 100, 100);">Salud</label>
                        <p>${Salud}</p>
                    </div>
                    <div style="width: calc(100% / 3 - 10px); display: inline-block; vertical-align: top;">
                        <label style="color: rgb(100, 100, 100);">Monto Isapre</label>
                        <p>${MontoIsapre}</p>
                    </div>
                </div>
                <div>
                    <div style="width: calc(100% / 3 - 10px); display: inline-block; vertical-align: top;">
                         <label style="color: rgb(100, 100, 100);">Cargas Familiares</label>
                         <p>${CargasFamiliares}</p>
                    </div>
                    <div style="width: calc(100% / 3 - 10px); display: inline-block; vertical-align: top;">
                        <label style="color: rgb(100, 100, 100);">Tramo</label>
                        <p>${Tramo}</p>
                    </div>
                    <div style="width: calc(100% / 3 - 10px); display: inline-block; vertical-align: top;">
                        <label style="color: rgb(100, 100, 100);">Valor Tramo</label>
                        <p>${ValorTramo}</p>
                    </div>
                </div>
                <h2 style="color: rgb(150, 150, 150); font-size: 25px;">Datos Bancarios</h2>
                <div class="pdb-20">
                    <div style="width: calc(100% / 2 - 10px); display: inline-block; vertical-align: top;">
                        <label style="color: rgb(100, 100, 100);">Tipo de Pago</label>
                        <p>${TipoPago}</p>
                    </div>
                    <div style="width: calc(100% / 2 - 10px); display: inline-block; vertical-align: top;">
                         <label style="color: rgb(100, 100, 100);">Banco</label>
                         <p>${Banco}</p>
                    </div>
                </div>
                <div>
                    <div style="width: calc(100% / 2 - 10px); display: inline-block; vertical-align: top;">
                         <label style="color: rgb(100, 100, 100);">Tipo de Cuenta</label>
                         <p>${TipoCuenta}</p>
                    </div>
                    <div style="width: calc(100% / 2 - 10px); display: inline-block; vertical-align: top;">
                         <label style="color: rgb(100, 100, 100);">Número de Cuenta</label>
                         <p>${NumeroCuenta}</p>
                    </div>
                </div> 
                <h2 style="color: rgb(150, 150, 150); font-size: 25px;">Datos Extranjero</h2>
                <div class="pdb-20">
                    <div style="width: calc(100% / 2 - 10px); display: inline-block; vertical-align: top;">
                         <label style="color: rgb(100, 100, 100);">Visa</label>
                         <p>${Visa}</p>
                    </div>
                    <div style="width: calc(100% / 2 - 10px); display: inline-block; vertical-align: top;">
                         <label style="color: rgb(100, 100, 100);">Vencimiento de Visa</label>
                         <p>${VencimientoVisa}</p>
                    </div>
                </div> 
            </div>
        `
    );
};


export { renderFichaPersonal, renderContrato, renderRenovacion, renderFichaCompleta, renderLoadingTable, renderBaja, renderContratoFull, renderHeaderConsulta, renderBodyConsulta, renderLoading };