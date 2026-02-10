using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teamwork.Model.Bajas;

namespace Teamwork.Infraestructura.Instances.Finiquitos
{
    public class InstanceTEF
    {
        public static DatosTEF __CreateObjectInstance(dynamic objects)
        {
            DatosTEF instance = new DatosTEF();

            instance.Code = objects.Code.ToString();
            instance.Message = objects.Message.ToString();
            instance.Rut = objects.Rut.ToString();
            instance.Beneficiario = objects.Beneficiario.ToString();
            instance.Cuenta = objects.Cuenta.ToString();
            instance.Banco = objects.Banco.ToString();
            instance.Total = objects.Total.ToString();
            instance.TotalGlosa = objects.TotalGlosa.ToString();

            return instance;
        }
    }
}