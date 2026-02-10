using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FiniquitosV2.Clases
{
    public class FechaContable
    {
        public DateTime Fechainicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public int años { get; set; }
        public int meses { get; set; }
        public int dias { get; set; }

        public void calculocontable()
        {

            int mesesVariable, diasmeses, dias_inicial = 0, dias_Final = 0;
            DateTime fechaaños;
            TimeSpan ts1;
            
            fechaaños = Fechainicio;
            if ( fechaaños.AddYears((FechaFinal.Year - Fechainicio.Year)) < FechaFinal)
            {
                //if (int.Parse((FechaFinal.Month - Fechainicio.Month).ToString()) > 12)
                //{
                años = FechaFinal.Year - Fechainicio.Year;
                Fechainicio = fechaaños.AddYears((FechaFinal.Year - Fechainicio.Year));

                //fechaaños = Fechainicio.AddYears(años);
                //dias = int.Parse((FechaFinal.Day - fechaaños.Day).ToString());
                ////meses = int.Parse((FechaFinal.Month - fechaaños.Month).ToString());//dias / 30;
                //}
            }
            else
            {
                años = 0;
            }
            ts1 = FechaFinal - Fechainicio;
            meses = FechaFinal.Month - Fechainicio.Month;  //ts1.Days / 30;

            if (Fechainicio.Day > 1)
            {
                dias_inicial = 30 - Fechainicio.Day;
            }

            if (FechaFinal.Day > 1)
            {
                dias_Final = 30 - FechaFinal.Day;
            }

            ////////////if (meses > 12)

            ////////////{
            ////////////    meses = meses - 12;
            ////////////}
            //////////////meses = int.Parse((Fecha2- Fecha1).ToString());

            ////////////if (Fechainicio.Day < 30)
            ////////////{
            ////////////    dias_inicial = 30 - Fechainicio.Day;
            ////////////}

            ////////////if (FechaFinal.Day < 30)
            ////////////{
            ////////////    dias_Final = 30 - (30 - FechaFinal.Day);

            ////////////}
            ////////////if ((dias_Final + dias_inicial == 30))
            ////////////{
            ////////////    dias_Final = 0;
            ////////////    dias_inicial = 0;
            ////////////}
            ////////////if (dias_Final + dias_inicial > 30)
            ////////////{

            ////////////    dias_Final = (dias_inicial + dias_Final) - 30;
            ////////////    dias_inicial = 0;
            ////////////}

            dias = dias_Final + dias_inicial;

            //TimeSpan ts = Fecha2 - Fecha1;
            //dias = ts.Days;
            //meses = int.Parse((Fecha2.Month - Fecha1.Month).ToString());//dias / 30;
            //}

           
        
        }

        public void calculofecha1()
        {
            int AntDias = 0, AntMeses = 0, AntAños = 0;

            #region "ANTERIOR"
            //int DiasFI = Fechainicio.Day, MesFI = Fechainicio.Month, AñoFI = Fechainicio.Year;
            //int DiasFT = FechaFinal.Day, MesFT = FechaFinal.Month, AñoFT = FechaFinal.Year;

            //if (DiasFT < DiasFI)
            //{
            //    DiasFT += 30;
            //    MesFT -= 1;
            //}

            //// calcula dias de antiguedad
            //AntDias = (DiasFT - DiasFI);

            //if (MesFT < MesFI)
            //{
            //    MesFT += 12;
            //    AñoFT -= 1;
            //}
            //AntMeses = MesFT - MesFI;
            //if (AntDias >= 30)
            //{
            //    AntDias = 0;
            //    AntMeses = AntMeses + 1;

            //}
            //// calcula meses de antiguedad


            //// calcula años antiguedad
            //AntAños = AñoFT - AñoFI;
            //años = AntAños;
            //meses = AntMeses;



            //if (años + meses > 0)
            //{
            //    dias = AntDias;
            //}
            //else
            //{
            //    dias = AntDias + 1;
            //}

            #endregion

            int sumMonth = 1;
            int sumDays = 1;
            int year = 0;
            int months = 0;
            int days = 0;
            int descuento = 0;
            int sumDescuento = 1;
            DateTime fechaRecalculada = new DateTime();

            while (Fechainicio.AddMonths(sumMonth) <= FechaFinal)
            {

                if (months < 12)
                {
                    months = months + 1;
                }
                
                if(months == 12)
                {
                    months = 0;
                    year = year + 1;
                }

                sumMonth++;
            }

            switch (Fechainicio.AddMonths(sumMonth - 1).Day)
            {
                case 31:
                    fechaRecalculada = Fechainicio.AddMonths(sumMonth - 1).AddDays(-2);
                    break;
                default:
                    fechaRecalculada = Fechainicio.AddMonths(sumMonth - 1);
                    break;
            }

            while (fechaRecalculada.AddDays(sumDescuento) < FechaFinal)
            {
                if (fechaRecalculada.AddDays(sumDescuento).Day == 31)
                {
                    descuento = descuento + 1;
                }

                sumDescuento++;
            }

            //TimeSpan time = (FechaFinal - (months > 0 ? Fechainicio.AddMonths(sumMonth - 1).AddDays(1) : Fechainicio.AddMonths(sumMonth - 1)));

            //days = time.Days >= 0 ? time.Days : 0;

            while (fechaRecalculada.AddDays(sumDays) <= FechaFinal)
            {

                days = days + 1;

                sumDays++;
            }

            days = days - descuento;

            años = year;
            meses = months;
            dias = days;

            //años = Convert.ToInt32(countyear);
            //meses = Convert.ToInt32(countmeses);
            //dias = Convert.ToInt32(countdias);

        }



    }
}