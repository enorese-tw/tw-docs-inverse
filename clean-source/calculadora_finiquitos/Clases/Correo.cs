using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

namespace FiniquitosV2.Clases
{
    public class Correo
    {
        MailMessage m = new MailMessage();
        SmtpClient smtp = new SmtpClient();

        public bool enviar(string de, string clave, string para, string mensaje, string asunto, string cc)
        {
            try
            {
                m.From = new MailAddress(de);
                m.To.Add(new MailAddress(para));
                m.Body = mensaje;
                m.Subject = asunto;
                m.Priority = MailPriority.High;
                m.IsBodyHtml = true;
                char[] delimitador = { ';' };
                string[] cadena2 = cc.Split(delimitador);
                //desmenbramiento cadena con correos para copia
                foreach (string word in cadena2)
                {
                    if (word != null && word != "vacio")
                    {
                        m.CC.Add(word.Trim());
                    }
                }
                smtp.Host = "team-work.cl";
                smtp.Port = 587;
                smtp.Credentials = new NetworkCredential(de, clave);
                //smtp.EnableSsl = true;
                smtp.Send(m);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }

        }
    }
}