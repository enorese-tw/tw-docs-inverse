using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teamwork.Model.Operaciones;

namespace AplicacionOperaciones.Collections
{
    public class InstanceReporte
    {

        public static ReporteCargaMasivaContrato __CreateObjectInstanceContrato(dynamic objets, dynamic request)
        {
            ReporteCargaMasivaContrato instance = new ReporteCargaMasivaContrato();

            instance.Code = objets.Code.ToString();
            instance.FechaTransaccion  = objets.FechaTransaccion.ToString();
            instance.Ficha  = objets.Ficha.ToString();
            instance.Codigo  = objets.Codigo.ToString();
            instance.Empresa  = objets.Empresa.ToString();
            instance.RutEmpresa  = objets.RutEmpresa.ToString();
            instance.Rut  = objets.Rut.ToString();
            instance.Nombre  = objets.Nombre.ToString();
            instance.CargoBPO  = objets.CargoBPO.ToString();
            instance.CargoMod  = objets.CargoMod.ToString();
            instance.Cargo  = objets.Cargo.ToString();
            instance.Sucursal  = objets.Sucursal.ToString();
            instance.Ejecutivo  = objets.Ejecutivo.ToString();
            instance.FechaInicio  = objets.FechaInicio.ToString();
            instance.FechaTermino  = objets.FechaTermino.ToString();
            instance.Causal  = objets.Causal.ToString();
            instance.Reemplazo  = objets.Reemplazo.ToString();
            instance.TipoContrato  = objets.TipoContrato.ToString();
            instance.CC  = objets.CC.ToString();
            instance.Direccion  = objets.Direccion.ToString();
            instance.Comuna  = objets.Comuna.ToString();
            instance.Ciudad  = objets.Ciudad.ToString();
            instance.Region  = objets.Region.ToString();
            instance.Division  = objets.Division.ToString();
            instance.Horarios  = objets.Horarios.ToString();
            instance.HorasTrab  = objets.HorasTrab.ToString();
            instance.Colacion  = objets.Colacion.ToString();
            instance.Nacionalidad  = objets.Nacionalidad.ToString();
            instance.Visa  = objets.Visa.ToString();
            instance.VencVisa  = objets.VencVisa.ToString();
            instance.Firmante  = objets.Firmante.ToString();
            instance.DescripcionFunciones  = objets.DescripcionFunciones.ToString();
            instance.FechaInicioPPlazo  = objets.FechaInicioPPlazo.ToString();
            instance.FechaTerminoPPlazo  = objets.FechaTerminoPPlazo.ToString();
            instance.FechaInicioSPlazo  = objets.FechaInicioSPlazo.ToString();
            instance.FechaTerminoSPlazo  = objets.FechaTerminoSPlazo.ToString();
            instance.RenovacionAutomatica  = objets.RenovacionAutomatica.ToString();
            instance.RutPrevencionista = objets.RutPrevencionista.ToString();
            instance.RutPrevencionistaCliente = objets.RutPrevencionistaCliente.ToString();
            instance.EmpresaMandante = objets.EmpresaMandante.ToString();
            instance.TarifaDiaria = objets.TarifaDiaria.ToString();
            instance.RutRepresentanteTeamwork = objets.RutRepresentanteTeamwork.ToString();
            instance.NumeroRegistro = objets.NumeroRegistro.ToString();

            return instance;
        }

        public static ReporteCargaMasivaRenovacion __CreateObjectInstanceRenovacion(dynamic objets, dynamic request)
        {
            ReporteCargaMasivaRenovacion instance = new ReporteCargaMasivaRenovacion();

            instance.FechaTransaccion = objets.FechaTransaccion.ToString();
            instance.Code = objets.Code.ToString();
            instance.Codigo = objets.Codigo.ToString();
            instance.Ficha = objets.Ficha.ToString();
            instance.Rut = objets.Rut.ToString();
            instance.NombreCompleto = objets.NombreCompleto.ToString();
            instance.CargoBPO = objets.CargoBPO.ToString();
            instance.CargoMod = objets.CargoMod.ToString();
            instance.FechaInicioContrato = objets.FechaInicioContrato.ToString();
            instance.FechaTerminoContrato = objets.FechaTerminoContrato.ToString();
            instance.Causal = objets.Causal.ToString();
            instance.FechaInicioRenov = objets.FechaInicioRenov.ToString();
            instance.FechaTerminoRenov = objets.FechaTerminoRenov.ToString();
            instance.TopeLegal = objets.TopeLegal.ToString();
            instance.ClienteFirmante = objets.ClienteFirmante.ToString();
            instance.Empresa = objets.Empresa.ToString();
            instance.TarifaDiaria = objets.TarifaDiaria.ToString();
            instance.RutRepresentanteTeamwork = objets.RutRepresentanteTeamwork.ToString();

            return instance;
        }

    }
}