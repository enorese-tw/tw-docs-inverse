using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FiniquitosV2.Clases
{
    public class Plantillas
    {
        public Plantillas()
        {

        }

        public string tituloFiniquito(int tipoFiniquito, string empresa)
        {
            string body = string.Empty;
            /** SI VARIABLE tipoFiniquito viene en 0 es un finiquito menor a igual a 30 días, si viene en 1 es un finiquito mayor a 30 días.  */
            switch(empresa)
            {
                case "EST":
                    switch (tipoFiniquito)
                    {
                        case 0:
                            body = "'FINIQUITO AL CONTRATO DE TRABAJO \n DE DURACION NO SUPERIOR A 30 DIAS' \n (ART 177 Y ART 44, DEL CODIGO DEL TRABAJO)";
                            break;
                        case 1:
                            body = "FINIQUITO AL CONTRATO DE TRABAJO";
                            break;
                    }
                    break;
                case "OUT":
                    switch (tipoFiniquito)
                    {
                        case 0:
                            body = "'FINIQUITO AL CONTRATO DE TRABAJO \n DE DURACION NO SUPERIOR A 60 DIAS' \n (ART 177 Y ART 44, DEL CODIGO DEL TRABAJO)";
                            break;
                        case 1:
                            body = "FINIQUITO AL CONTRATO DE TRABAJO";
                            break;
                    }
                    break;
            }
            return body.Replace(@"'", @"""");
        }

        public string inicioFiniquito(int tipoFiniquito)
        {
            string body = string.Empty;
            /** SI VARIABLE tipoFiniquito viene en 0 es un finiquito menor a igual a 30 días, si viene en 1 es un finiquito mayor a 30 días.  */
            switch (tipoFiniquito)
            {
                case 0:
                    body = "Se entiende incluido en la remuneración del contrato por 30 días o menos y de aquellas prórrogas no superior a 60 días, todo lo que deba pagarse por feriado " +
                        " y demás derechos que se devenguen en proporción al tiempo servido (Artículo 44). No tendrá a lugar, la firma del Finiquito por escrito en los casos de Contratos de duración no superior a 30 (Artículo 177)";
                    break;
            }

            return body;
        }

        public string seguidoFiniquitoF(string fechaSistema, string empresa)
        {
            string body = string.Empty;

            switch(empresa){
                case "EST":
                    body = "En Santiago, a " + formatDateDD_STRINGMONTH_YYYY(fechaSistema) + ", entre E.S.T. SERVICE LTDA., representada por don Eduardo Andina Alegria, C.I. 7.708.305-7" +
                        " en adelante el 'ex - empleador', domiciliado para estos efectos en la Concepción 141, Oficina 1101, Comuna de Providencia, y por la otra, don(a) ";
                    break;
                case "OUT":
                    body = "En Santiago, a " + formatDateDD_STRINGMONTH_YYYY(fechaSistema) + ", entre TEAMWORK RECURSOS HUMANOS LTDA., representada por don Eduardo Andina Alegria, C.I. 7.708.305-7" +
                        " en adelante el 'ex - empleador', domiciliado para estos efectos en la Concepción 141, Oficina 1101, Comuna de Providencia, y por la otra, don(a) ";
                    break;
            }

            return body;
        }

        public string seguidoFiniquitoS(string nombreTrabajador)
        {
            string body = string.Empty;

            body = nombreTrabajador;

            return body;
        }

        public string seguidoFiniquitoT(string rutTrabajador, string empresa)
        {
            string body = string.Empty;

            switch(empresa){
                case "EST":
                    body = ", C.I. " + rutTrabajador + ", en adelante el 'ex - trabajador', se ha convenido el siguiente finiquito al Contrato de Trabajo.";
                    break;
                case "OUT":
                    body = ", Rut " + rutTrabajador + ", en adelante el 'ex - trabajador', se ha convenido el siguiente finiquito al Contrato de Trabajo.";
                    break;
            }

            return body;
        }

        public string parrafoPrimeroFiniquito(int tipoFiniquito, string cargo, List<string[]> fechas, string causal, string articuloCausalDocumento, string descripcionCausalDocumento, string empresa)
        {
            string body = string.Empty;
            /** SI VARIABLE tipoFiniquito viene en 0 es un finiquito menor a igual a 30 días, si viene en 1 es un finiquito mayor a 30 días.  */
            switch(empresa){
                case "EST":
                    switch (tipoFiniquito)
                    {
                        case 0:
                            body = "PRIMERO: El 'ex - trabajador' se desempeñó en calidad de " + cargo + " por medio de la causal " + articuloCausalDocumento.Replace("Art", "Artículo") + "" + descripcionCausalDocumento + ", ";

                            for (var i = (fechas.Count - 1); i >= 0; i--)
                            {
                                if (fechas.Count > 1)
                                {
                                    if ((i + 1) == 1)
                                    {
                                        body = body + " desde el " + formatDateDD_STRINGMONTH_YYYY(fechas[i][0]) + " al " + formatDateDD_STRINGMONTH_YYYY(fechas[i][1]) + "";
                                    }
                                    if ((i + 1) > 1 && (i + 1) < fechas.Count)
                                    {
                                        body = body + " desde el " + formatDateDD_STRINGMONTH_YYYY(fechas[i][0]) + " al " + formatDateDD_STRINGMONTH_YYYY(fechas[i][1]) + ", ";
                                    }
                                    else if ((i + 1) == fechas.Count)
                                    {
                                        body = body + " desde el " + formatDateDD_STRINGMONTH_YYYY(fechas[i][0]) + " al " + formatDateDD_STRINGMONTH_YYYY(fechas[i][1]) + ", ";
                                    }
                                }
                                else if (fechas.Count == 1)
                                {
                                    body = body + formatDateDD_STRINGMONTH_YYYY(fechas[i][0]) + " al " + formatDateDD_STRINGMONTH_YYYY(fechas[i][1]);
                                }

                            }

                            body = body + ", fecha ésta última de terminación de sus servicios por la causal contemplada en el " + causal.Split('|')[0].Replace("Art.", "Artículo ").Replace("N", " Numeral ").Replace("Inciso", " Inciso ").Replace("bis", " bis") +
                                " del Código del trabajo, esto es, " + causal.Split('|')[1] + ".";

                            break;
                        case 1:
                            body = "PRIMERO: El 'ex - trabajador' se desempeñó en calidad de " + cargo + ", ";

                            for (var i = (fechas.Count - 1); i >= 0; i--)
                            {
                                if (fechas.Count > 1)
                                {
                                    if ((i + 1) == 1)
                                    {
                                        body = body + " desde el " + formatDateDD_STRINGMONTH_YYYY(fechas[i][0]) + " al " + formatDateDD_STRINGMONTH_YYYY(fechas[i][1]) + "";
                                    }
                                    if ((i + 1) > 1 && (i + 1) < fechas.Count)
                                    {
                                        body = body + " desde el " + formatDateDD_STRINGMONTH_YYYY(fechas[i][0]) + " al " + formatDateDD_STRINGMONTH_YYYY(fechas[i][1]) + ", ";
                                    }
                                    else if ((i + 1) == fechas.Count)
                                    {
                                        body = body + " desde el " + formatDateDD_STRINGMONTH_YYYY(fechas[i][0]) + " al " + formatDateDD_STRINGMONTH_YYYY(fechas[i][1]) + ", ";
                                    }
                                }
                                else if (fechas.Count == 1)
                                {
                                    body = body + formatDateDD_STRINGMONTH_YYYY(fechas[i][0]) + " al " + formatDateDD_STRINGMONTH_YYYY(fechas[i][1]);
                                }

                            }

                            body = body + ", fecha ésta última de terminación de sus servicios por la causal contemplada en el " + causal.Split('|')[0].Replace("Art.", "Artículo ").Replace("N", " Numeral ").Replace("Inciso", " Inciso ").Replace("bis", " bis") + " del Código del Trabajo, esto es, " + causal.Split('|')[1] + ".";
                            break;
                    }
                    break;
                case "OUT":
                    body = "PRIMERO: El 'ex - trabajador' se desempeñó en calidad de " + cargo + ", ";

                    for (var i = (fechas.Count - 1); i >= 0; i--)
                    {
                        if (fechas.Count > 1)
                        {
                            if ((i + 1) == 1)
                            {
                                body = body + " desde el " + formatDateDD_STRINGMONTH_YYYY(fechas[i][0]) + " al " + formatDateDD_STRINGMONTH_YYYY(fechas[i][1]) + "";
                            }
                            if ((i + 1) > 1 && (i + 1) < fechas.Count)
                            {
                                body = body + " desde el " + formatDateDD_STRINGMONTH_YYYY(fechas[i][0]) + " al " + formatDateDD_STRINGMONTH_YYYY(fechas[i][1]) + ", ";
                            }
                            else if ((i + 1) == fechas.Count)
                            {
                                body = body + " desde el " + formatDateDD_STRINGMONTH_YYYY(fechas[i][0]) + " al " + formatDateDD_STRINGMONTH_YYYY(fechas[i][1]) + ", ";
                            }
                        }
                        else if (fechas.Count == 1)
                        {
                            body = body + " desde el " + formatDateDD_STRINGMONTH_YYYY(fechas[i][0]) + " al " + formatDateDD_STRINGMONTH_YYYY(fechas[i][1]);
                        }

                    }

                    body = body + ", fecha ésta última de terminación de sus servicios por la causal contemplada en el " + causal.Split('|')[0].Replace("Art.", "Artículo ").Replace("N", " Numeral ").Replace("Inciso", " Inciso ").Replace("bis", " bis") + " del Código del Trabajo, esto es, " + causal.Split('|')[1] + ".";
                    break;
            }

            return body;
        }

        public string parrafoSegundoFiniquitoF(int tipoFiniquito, string nombreTrabajador, string empresa)
        {
            string body = string.Empty;
            /** SI VARIABLE tipoFiniquito viene en 0 es un finiquito menor a igual a 30 días, si viene en 1 es un finiquito mayor a 30 días.  */
            switch(empresa){
                case "EST":
                    switch (tipoFiniquito)
                    {
                        case 0:
                            //body = "SEGUNDO: El 'ex - trabajador' declará recibir en este acto, a su entera satisfacción, de parte del ex - empleador las sumas que a continuación se indican, " +
                            //    "por los siguientes conceptos:";
                            body = "SEGUNDO: Don(ña) " + nombreTrabajador + " deja constancia que durante todo el tiempo en el que prestó servicios a E.S.T. SERVICE LTDA.," +
                                " recibió de ésta, correcta y oportunamente, el total de las remuneraciones convenidas, de acuerdo con su contrato de trabajo," +
                                " clase de trabajo ejecutado, reajustes convenidos, pago de asignaciones familiares autorizadas por la respectiva Institución de Previsión," +
                                " horas extraordinarias, cuando las trabajó, feriados legales, gratificaciones legales, en conformidad a la Ley," +
                                " y que nada se le adeuda por los conceptos antes señalados, ni por ninguno otro, sea de origen legal o contractual derivado de la prestación de" +
                                " sus servicios,  motivo por el cual, no teniendo cargo alguno que formular en contra del ex - empleador, le otorga el más amplio y total finiquito," +
                                " respecto de las relaciones contractuales que existieron entre el ex - trabajador y el ex - empleador. Asimismo, declara que siempre existió un trato con dignidad y pleno" +
                                " respeto a sus derechos fundamentales por parte de su ex - empleador, sin menoscabo ni represalias incluyendo la época de relación laboral.";
                            break;
                        case 1:
                            body = "SEGUNDO: Don(ña) " + nombreTrabajador + " deja constancia que durante todo el tiempo en el que prestó servicios a E.S.T. SERVICE LTDA.," +
                                " recibió de ésta, correcta y oportunamente, el total de las remuneraciones convenidas, de acuerdo con su contrato de trabajo," +
                                " clase de trabajo ejecutado, reajustes convenidos, pago de asignaciones familiares autorizadas por la respectiva Institución de Previsión," +
                                " horas extraordinarias, cuando las trabajó, feriados legales, gratificaciones legales, en conformidad a la Ley," +
                                " y que nada se le adeuda por los conceptos antes señalados, ni por ninguno otro, sea de origen legal o contractual derivado de la prestación de" +
                                " sus servicios,  motivo por el cual, no teniendo cargo alguno que formular en contra del ex - empleador, le otorga el más amplio y total finiquito," +
                                " respecto de las relaciones contractuales que existieron entre el ex - trabajador y el ex - empleador. Asimismo, declara que siempre existió un trato con dignidad y pleno" +
                                " respeto a sus derechos fundamentales por parte de su ex - empleador, sin menoscabo ni represalias incluyendo la época de relación laboral.";

                            break;
                    }
                    break;
                case "OUT":
                    switch (tipoFiniquito)
                    {
                        case 0:
                            //body = "SEGUNDO: El 'ex - trabajador' declará recibir en este acto, a su entera satisfacción, de parte del ex - empleador las sumas que a continuación se indican, " +
                            //    "por los siguientes conceptos:";
                            body = "SEGUNDO: Don(ña) " + nombreTrabajador + " deja constancia que durante todo el tiempo en el que prestó servicios a TEAMWORK RECURSOS HUMANOS LTDA.," +
                                " recibió de ésta, correcta y oportunamente, el total de las remuneraciones convenidas, de acuerdo con su contrato de trabajo," +
                                " clase de trabajo ejecutado, reajustes convenidos,  pago de asignaciones familiares autorizadas por la respectiva Institución de Previsión," +
                                " horas extraordinarias, cuando las trabajó, feriados legales, gratificaciones legales, en conformidad a la Ley," +
                                " y que nada se le adeuda por los conceptos antes señalados, ni por ninguno otro, sea de origen legal o contractual derivado de la prestación de" +
                                " sus servicios,  motivo por el cual,  no teniendo cargo alguno que formular en contra del ex - empleador, le otorga el más amplio y total finiquito," +
                                " respecto de las relaciones contractuales que existieron entre el ex - trabajador y el ex - empleador. Asimismo, declara que siempre existió un trato con dignidad y pleno" +
                                " respeto a sus derechos fundamentales por parte de su ex - empleador, sin menoscabo ni represalias incluyendo la época de relación laboral.";
                            break;
                        case 1:
                            body = "SEGUNDO: Don(ña) " + nombreTrabajador + " deja constancia que durante todo el tiempo en el que prestó servicios a TEAMWORK RECURSOS HUMANOS LTDA.," +
                                " recibió de ésta, correcta y oportunamente, el total de las remuneraciones convenidas, de acuerdo con su contrato de trabajo," +
                                " clase de trabajo ejecutado, reajustes convenidos,  pago de asignaciones familiares autorizadas por la respectiva Institución de Previsión," +
                                " horas extraordinarias, cuando las trabajó, feriados legales, gratificaciones legales, en conformidad a la Ley," +
                                " y que nada se le adeuda por los conceptos antes señalados, ni por ninguno otro, sea de origen legal o contractual derivado de la prestación de" +
                                " sus servicios,  motivo por el cual,  no teniendo cargo alguno que formular en contra del ex - empleador, le otorga el más amplio y total finiquito," +
                                " respecto de las relaciones contractuales que existieron entre el ex - trabajador y el ex - empleador. Asimismo, declara que siempre existió un trato con dignidad y pleno" +
                                " respeto a sus derechos fundamentales por parte de su ex - empleador, sin menoscabo ni represalias incluyendo la época de relación laboral.";
                            break;
                    }
                    break;
            }

            return body;
        }

        public string parrafoSegundoFiniquitoS(int tipoFiniquito, string nombreTrabajador, string empresa)
        {
            string body = string.Empty;
            /** SI VARIABLE tipoFiniquito viene en 0 es un finiquito menor a igual a 30 días, si viene en 1 es un finiquito mayor a 30 días.  */
            
            switch(empresa){
                case "EST":
                    switch (tipoFiniquito)
                    {
                        case 0:
                            body = "Finalmente, Don(ña) " + nombreTrabajador + ", deja expresa constancia por este acto que transige todo reclamo, demanda, denuncia o cuestión actual" +
                                " o futura que pudiere existir entre el ex - trabajador y el ex - empleador en virtud de la relación laboral que entre ambos existió, y de su terminación, en la" +
                                " cantidad antes indicada y pagada, renunciando el ex - trabajador al ejercicio de cualquier acción, demanda o reclamo, sean estas judiciales o extrajudiciales," +
                                " civiles, comerciales, laborales, incluidas las de accidentes de trabajo y/o vulneración de derechos fundamentales, penales o administrativas" +
                                " en contra de E.S.T. SERVICE LTDA., sea civil, penal o contravencional, incluso renunciando a las acciones por indemnizaciones de perjuicios, por lucro" +
                                " cesante, daño emergente o daño moral que pudieren corresponderle por cualquier causa o motivo, incluidas las acciones indemnizatorias por acciones del" +
                                " trabajo o enfermedades profesionales, establecidas en la Ley 16.744.-, leyes generales y especiales e incluso el daño moral que de ellas pudieren derivar, motivo por" +
                                " el cual, no teniendo reclamo ni cargo alguno que formalizar en contra de EST SERVICE LTDA., le otorga el más amplio y total finiquito, declaración que" +
                                " formula libre y espontáneamente, en perfecto y cabal conocimiento de cada uno y de todos sus derechos.";
                            break;
                        case 1:
                            body = "Finalmente, Don(ña) " + nombreTrabajador + ", deja expresa constancia por este acto que transige todo reclamo, demanda, denuncia o cuestión actual" +
                                " o futura que pudiere existir entre el ex - trabajador y el ex - empleador en virtud de la relación laboral que entre ambos existió, y de su terminación, en la" +
                                " cantidad antes indicada y pagada, renunciando el ex - trabajador al ejercicio de cualquier acción, demanda o reclamo, sean estas judiciales o extrajudiciales," +
                                " civiles, comerciales, laborales, incluidas las de accidentes de trabajo y/o vulneración de derechos fundamentales, penales o administrativas" +
                                " en contra de E.S.T. SERVICE LTDA., sea civil, penal o contravencional, incluso renunciando a las acciones por indemnizaciones de perjuicios, por lucro" +
                                " cesante, daño emergente o daño moral que pudieren corresponderle por cualquier causa o motivo, incluidas las acciones indemnizatorias por acciones del" +
                                " trabajo o enfermedades profesionales, establecidas en la Ley 16.744.-, leyes generales y especiales e incluso el daño moral que de ellas pudieren derivar, motivo por" +
                                " el cual, no teniendo reclamo ni cargo alguno que formalizar en contra de EST SERVICE LTDA., le otorga el más amplio y total finiquito, declaración que" +
                                " formula libre y espontáneamente, en perfecto y cabal conocimiento de cada uno y de todos sus derechos.";
                            break;
                    }
                    break;
                case "OUT":
                    switch (tipoFiniquito)
                    {
                        case 0:
                            //body = "";
                            body = "Finalmente, Don(ña) " + nombreTrabajador + ", deja expresa constancia por este acto que transige todo reclamo, demanda, denuncia o cuestión actual" +
                                " o futura que pudiere existir entre el ex - trabajador y el ex - empleador en virtud de la relación laboral que entre ambos existió, y de su terminación, en la" +
                                " cantidad antes indicada y pagada, renunciando el ex - trabajador al ejercicio de cualquier acción, demanda o reclamo, sean estas judiciales o extrajudiciales," +
                                " civiles, comerciales, laborales, incluidas las de accidentes de trabajo y/o vulneración de derechos fundamentales, penales o administrativas" +
                                " en contra de TEAMWORK RECURSOS HUMANOS LTDA., sea civil, penal o contravencional, incluso renunciando a las acciones por indemnizaciones de perjuicios, por lucro" +
                                " cesante, daño emergente o daño moral que pudieren corresponderle por cualquier causa o motivo, incluidas las acciones indemnizatorias por acciones del" +
                                " trabajo o enfermedades profesionales, establecidas en leyes generales y especiales e incluso el daño moral que de ellas pudieren derivar, motivo por" +
                                " el cual, no teniendo reclamo ni cargo alguno que formalizar en contra de TEAMWORK RECURSOS HUMANOS LTDA., le otorga el más amplio y total finiquito, declaración que" +
                                " formula libre y espontáneamente, en perfecto y cabal conocimiento de cada uno y de todos sus derechos.";
                            break;
                        case 1:
                            body = "Finalmente, Don(ña) " + nombreTrabajador + ", deja expresa constancia por este acto que transige todo reclamo, demanda, denuncia o cuestión actual" +
                                " o futura que pudiere existir entre el ex - trabajador y el ex - empleador en virtud de la relación laboral que entre ambos existió, y de su terminación, en la" +
                                " cantidad antes indicada y pagada, renunciando el ex - trabajador al ejercicio de cualquier acción, demanda o reclamo, sean estas judiciales o extrajudiciales," +
                                " civiles, comerciales, laborales, incluidas las de accidentes de trabajo y/o vulneración de derechos fundamentales, penales o administrativas" +
                                " en contra de TEAMWORK RECURSOS HUMANOS LTDA., sea civil, penal o contravencional, incluso renunciando a las acciones por indemnizaciones de perjuicios, por lucro" +
                                " cesante, daño emergente o daño moral que pudieren corresponderle por cualquier causa o motivo, incluidas las acciones indemnizatorias por acciones del" +
                                " trabajo o enfermedades profesionales, establecidas en leyes generales y especiales e incluso el daño moral que de ellas pudieren derivar, motivo por" +
                                " el cual, no teniendo reclamo ni cargo alguno que formalizar en contra de TEAMWORK RECURSOS HUMANOS LTDA., le otorga el más amplio y total finiquito, declaración que" +
                                " formula libre y espontáneamente, en perfecto y cabal conocimiento de cada uno y de todos sus derechos.";
                            break;
                    }
                    break;
            }

            return body;
        }

        public string parrafoTerceroFiniquito(int tipoFiniquito)
        {
            string body = string.Empty;

            switch (tipoFiniquito)
            {
                case 0:
                    body = "TERCERO: El 'ex - trabajador' declará recibir en este acto, a su entera satisfacción, de parte del ex - empleador las sumas que a continuación se indican, " +
                        "por los siguientes conceptos:";
                    break;
                case 1:
                    body = "TERCERO: El 'ex - trabajador' declará recibir en este acto, a su entera satisfacción, de parte del ex - empleador las sumas que a continuación se indican, " +
                        "por los siguientes conceptos:";
                    break;
            }

            return body;
        }

        public string parrafoCuartoFiniquito(int tipoFiniquito)
        {
            string body = string.Empty;

            switch(tipoFiniquito) {
                case 0:
                    body = "CUARTO:  El 'ex – trabajador' deja constancia que durante todo el tiempo en el que prestó servicios al ex - empleador," +
                        " recibió de éste, correcta y oportunamente, el total de las remuneraciones convenidas, de acuerdo con su contrato de trabajo," +
                        " clase de trabajo ejecutado, reajustes convenidos, pago de asignaciones familiares autorizadas por la respectiva Institución de Previsión," +
                        " horas extraordinarias, cuando las trabajó, feriados legales, gratificaciones legales, en conformidad a la Ley, y que nada se le adeuda por" +
                        " los conceptos antes señalados, ni por ninguno otro, sea de origen legal, judicial o contractual derivado de la prestación de sus servicios," +
                        " motivo por el cual, no teniendo cargo alguno que formular en contra del ex - empleador, le otorga el más amplio y total finiquito, respecto de las" +
                        " relaciones contractuales que existieron entre él y el ex - empleador.";
                    break;
                case 1:
                    body = "CUARTO: Las partes dejan constancia que el presente finiquito sólo da cuenta del término de la relación laboral existente entre ellos, en los términos" +
                        " expuestos en la Cláusula Segundo del presente documento. Sin embargo, el ex - empleador, podrá revisar, por cualquier medio, los asuntos y materias que le fueron confiados. Por lo tanto," +
                        " en caso de determinarse que su desempeño comprometió los intereses del ex - empleador, éste quedará facultado para iniciar las acciones" +
                        " correspondientes ante las autoridades, a fin de establecer las responsabilidades que procedan y obtener la reparación de los perjuicios ocasionales.";
                    break;
            }

            return body;
        }

        public string parrafoQuintoFiniquitoF()
        {
            string body = string.Empty;

            body = "QUINTO: Se deja constancia que el feriado proporcional pagado en este acto, corresponde a los periodos comprendidos por los contratos de trabajo de fecha ";

            return body;
        }

        public string parrafoQuintoFiniquitoS(List<string[]> fechas)
        {
            string body = string.Empty;

            for (var i = (fechas.Count - 1); i >= 0; i--)
            {
                if (fechas.Count > 1)
                {
                    if ((i + 1) == 1)
                    {
                        body = body + formatDateDD_STRINGMONTH_YYYY(fechas[i][0]) + " al " + formatDateDD_STRINGMONTH_YYYY(fechas[i][1]) + "";
                    }
                    if ((i + 1) > 1 && (i + 1) < fechas.Count)
                    {
                        body = body + " desde el " + formatDateDD_STRINGMONTH_YYYY(fechas[i][0]) + " al " + formatDateDD_STRINGMONTH_YYYY(fechas[i][1]) + ", ";
                    }
                    else if ((i + 1) == fechas.Count)
                    {
                        body = body + " desde el " + formatDateDD_STRINGMONTH_YYYY(fechas[i][0]) + " al " + formatDateDD_STRINGMONTH_YYYY(fechas[i][1]) + ", ";
                    }
                }
                else if (fechas.Count == 1)
                {
                    body = body + formatDateDD_STRINGMONTH_YYYY(fechas[i][0]) + " al " + formatDateDD_STRINGMONTH_YYYY(fechas[i][1]);
                }

            }

            return body;
        }

        public string parrafoQuintoFiniquitoT()
        {
            string body = string.Empty;

            body = body + ", de acuerdo a lo establecido en el Artículo 183-V del Código del Trabajo.";

            return body;
        }

        public string parrafoSextoFiniquito(int tipoFiniquito)
        {
            string body = string.Empty;

            switch (tipoFiniquito)
            {
                case 0:
                    body = "QUINTO: Se deja constancia que en atención a Ord. N° 4316/12 del 23 de diciembre de 2002 de la Dirección del Trabajo, el saldo pendiente de crédito" +
                        " social vigente con CCAF Los Andes no será descontado de finiquito a petición del ex - trabajador, por lo que es exclusiva responsabilidad del ex - " +
                        " trabajador el pago de dicho crédito.";
                    break;
                case 1:
                    body = "SEXTO: Se deja constancia que en atención a Ord. N° 4316/12 del 23 de diciembre de 2002 de la Dirección del Trabajo, el saldo pendiente de crédito" +
                        " social vigente con CCAF Los Andes no será descontado de finiquito a petición del ex - trabajador, por lo que es exclusiva responsabilidad del ex - " +
                        " trabajador el pago de dicho crédito.";
                    break;
            }

            return body;
        }

        public string parrafoCopias()
        {
            string body = string.Empty;

            body = "Para constancia, las partes firman el presente documento en dos ejemplares, quedando uno de ellos en poder de cada parte.";

            return body;
        }

        private string formatDateDD_STRINGMONTH_YYYY(string fecha)
        {
            string fechaDD_STRINGMONTH_YYYY = string.Empty;
            string[] arrayFechas = fecha.Split('-');

            fechaDD_STRINGMONTH_YYYY = arrayFechas[0] + " de ";

            switch (Convert.ToInt32(arrayFechas[1]))
            {
                case 1:
                    fechaDD_STRINGMONTH_YYYY = fechaDD_STRINGMONTH_YYYY + "enero de ";
                    break;
                case 2:
                    fechaDD_STRINGMONTH_YYYY = fechaDD_STRINGMONTH_YYYY + "febrero de ";
                    break;
                case 3:
                    fechaDD_STRINGMONTH_YYYY = fechaDD_STRINGMONTH_YYYY + "marzo de ";
                    break;
                case 4:
                    fechaDD_STRINGMONTH_YYYY = fechaDD_STRINGMONTH_YYYY + "abril de ";
                    break;
                case 5:
                    fechaDD_STRINGMONTH_YYYY = fechaDD_STRINGMONTH_YYYY + "mayo de ";
                    break;
                case 6:
                    fechaDD_STRINGMONTH_YYYY = fechaDD_STRINGMONTH_YYYY + "junio de ";
                    break;
                case 7:
                    fechaDD_STRINGMONTH_YYYY = fechaDD_STRINGMONTH_YYYY + "julio de ";
                    break;
                case 8:
                    fechaDD_STRINGMONTH_YYYY = fechaDD_STRINGMONTH_YYYY + "agosto de ";
                    break;
                case 9:
                    fechaDD_STRINGMONTH_YYYY = fechaDD_STRINGMONTH_YYYY + "septiembre de ";
                    break;
                case 10:
                    fechaDD_STRINGMONTH_YYYY = fechaDD_STRINGMONTH_YYYY + "octubre de ";
                    break;
                case 11:
                    fechaDD_STRINGMONTH_YYYY = fechaDD_STRINGMONTH_YYYY + "noviembre de ";
                    break;
                case 12:
                    fechaDD_STRINGMONTH_YYYY = fechaDD_STRINGMONTH_YYYY + "diciembre de ";
                    break;
            }

            fechaDD_STRINGMONTH_YYYY = fechaDD_STRINGMONTH_YYYY + arrayFechas[2];

            return fechaDD_STRINGMONTH_YYYY;
        }

        private string convertToMonth(string fecha)
        {
            string fechaConvert = string.Empty;
            string[] arrayFecha = fecha.Split('-');

            switch (Convert.ToInt32(arrayFecha[1].ToString()))
            {
                case 1:
                    fechaConvert = "enero";
                    break;
                case 2:
                    fechaConvert = "febrero";
                    break;
                case 3:
                    fechaConvert = "marzo";
                    break;
                case 4:
                    fechaConvert = "abril";
                    break;
                case 5:
                    fechaConvert = "mayo";
                    break;
                case 6:
                    fechaConvert = "junio";
                    break;
                case 7:
                    fechaConvert = "julio";
                    break;
                case 8:
                    fechaConvert = "agosto";
                    break;
                case 9:
                    fechaConvert = "septiembre";
                    break;
                case 10:
                    fechaConvert = "octubre";
                    break;
                case 11:
                    fechaConvert = "noviembre";
                    break;
                case 12:
                    fechaConvert = "diciembre";
                    break;
            }

            return fechaConvert;
        }
    }
}