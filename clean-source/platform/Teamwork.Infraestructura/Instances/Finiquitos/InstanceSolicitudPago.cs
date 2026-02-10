using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teamwork.Model.Bajas;

namespace Teamwork.Infraestructura.Instances.Finiquitos
{
    public class InstanceSolicitudPago
    {
        public static SolicitudPago __CreateObjectInstance(dynamic objects)
        {
            SolicitudPago instance = new SolicitudPago();

            instance.Code = objects.Code.ToString();
            instance.Message = objects.Message.ToString();
            instance.Rut = objects.Rut.ToString();
            instance.Beneficiario = objects.Beneficiario.ToString();
            instance.Cuenta = objects.Cuenta.ToString();
            instance.Banco = objects.Banco.ToString();
            instance.Total = objects.Total.ToString();
            instance.Tipo = objects.Tipo.ToString();
            instance.MontoAdm = objects.MontoAdm.ToString();
            instance.MontoFiniquito = objects.MontoFiniquito.ToString();

            return instance;
        }
    }
}