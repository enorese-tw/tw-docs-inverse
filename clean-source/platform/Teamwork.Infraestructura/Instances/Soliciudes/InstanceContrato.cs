using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teamwork.Model.Kam;

namespace Teamwork.Infraestructura.Instances.Soliciudes
{
    public class InstanceContrato
    {
        public static Contrato __CreateObjectInstance(dynamic objects)
        {
            Contrato instance = new Contrato();

            instance.Ficha = objects.Ficha.ToString();
            instance.Empresa = objects.Empresa.ToString();
            instance.Cliente = objects.Cliente.ToString();
            instance.CargoMod = objects.CargoMod.ToString();
            instance.CodigoSucursal = objects.CodigoSucursal.ToString();
            instance.NombreSucursal = objects.NombreSucursal.ToString();
            instance.DireccionSucursal = objects.DireccionSucursal.ToString();
            instance.ComunaSucursal = objects.ComunaSucursal.ToString();
            instance.CiudadSucursal = objects.CiudadSucursal.ToString();
            instance.RegionSucursal = objects.RegionSucursal.ToString();
            instance.Causal = objects.Causal.ToString();
            instance.TopeLegal = objects.TopeLegal.ToString();
            instance.FechaInicioContrato = objects.FechaInicioContrato.ToString();
            instance.FechaTerminoContrato = objects.FechaTerminoContrato.ToString();
            instance.HorasSemanales = objects.HorasSemanales.ToString();
            instance.Horarios = objects.Horarios.ToString();
            instance.Colacion = objects.Colacion.ToString();
            instance.TipoReemplazo = objects.TipoReemplazo.ToString();
            instance.Reemplazo = objects.Reemplazo.ToString();
            instance.TipoMovimiento = objects.TipoMovimiento.ToString();
            instance.NombreCargo = objects.NombreCargo.ToString();
            instance.Tag = objects.Tag.ToString();

            return instance;
        }
    }
}