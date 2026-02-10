using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teamwork.Model.Kam;

namespace Teamwork.Infraestructura.Instances.Soliciudes
{
    public class InstanceFichaPersonal
    {
        public static FichaPersonal __CreateObjectInstance(dynamic objects)
        {
            FichaPersonal instance = new FichaPersonal();

            instance.Rut = objects.Rut.ToString();
            instance.Nombres = objects.Nombres.ToString();
            instance.Nombre = objects.Nombre.ToString();
            instance.ApellidoPaterno = objects.ApellidoPaterno.ToString();
            instance.ApellidoMaterno = objects.ApellidoMaterno.ToString();
            instance.Direccion = objects.Direccion.ToString();
            instance.Comuna = objects.Comuna.ToString();
            instance.Ciudad = objects.Ciudad.ToString();
            instance.Genero = objects.Genero.ToString();
            instance.Correo = objects.Correo.ToString();
            instance.Telefono = objects.Telefono.ToString();
            instance.FechaNacimiento = objects.FechaNacimiento.ToString();
            instance.EstadoCivil = objects.EstadoCivil.ToString();
            instance.Nacionalidad = objects.Nacionalidad.ToString();
            instance.Afp = objects.Afp.ToString();
            instance.Salud = objects.Salud.ToString();
            instance.MontoIsapre = objects.MontoIsapre.ToString();
            instance.TipoPago = objects.TipoPago.ToString();
            instance.Banco = objects.Banco.ToString();
            instance.TipoCuenta = objects.TipoCuenta.ToString();
            instance.NumeroCuenta = objects.NumeroCuenta.ToString();
            instance.CargasFamiliares = objects.CargasFamiliares.ToString();
            instance.Tramo = objects.Tramo.ToString();
            instance.ValorTramo = objects.ValorTramo.ToString();
            instance.Visa = objects.Visa.ToString();
            instance.VencimientoVisa = objects.VencimientoVisa.ToString();

            return instance;
        }
    }
}