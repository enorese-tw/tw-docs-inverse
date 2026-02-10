using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teamwork.Model.Operaciones;

namespace AplicacionOperaciones.Collections
{
    public class InstanceCargaMasiva
    {
        public static ProcesoCargaMasiva __CreateObjectInstance(dynamic objets, dynamic request)
        {
            ProcesoCargaMasiva instance = new ProcesoCargaMasiva();

            instance.UploadCifrado = objets.UploadCifrado.ToString();

            instance.Glyphicon = objets.Glyphicon.ToString();
            instance.GlyphiconColor = objets.GlyphiconColor.ToString();
            instance.RenderizadoTituloOne = objets.RenderizadoTituloOne.ToString();
            instance.RenderizadoTituloTwo = objets.RenderizadoTituloTwo.ToString();
            instance.RenderizadoDescripcion = objets.RenderizadoDescripcion.ToString();
            instance.UploadRenderizadoColor = objets.UploadRenderizadoColor.ToString();
            instance.UploadNombrePlantilla = objets.UploadNombrePlantilla.ToString();
            instance.UploadNombreHojaCargaMasiva = objets.UploadNombreHojaCargaMasiva.ToString();
            instance.UploadCifrado = objets.UploadCifrado.ToString();
            instance.UploadColumnas = objets.UploadColumnas.ToString();
            instance.UploadNodoPadre = objets.UploadNodoPadre.ToString();
            instance.UploadNodoHijo = objets.UploadNodoHijo.ToString();
            instance.UploadRenderizadoGlyphicon = objets.UploadRenderizadoGlyphicon.ToString();
            instance.UploadRenderizadoOneTexto = objets.UploadRenderizadoOneTexto.ToString();
            instance.UploadRenderizadoSecTexto = objets.UploadRenderizadoSecTexto.ToString();
            instance.UploadRenderizadoMensajeImpt = objets.UploadRenderizadoMensajeImpt.ToString();
            instance.RenderizadoTituloDownload = objets.RenderizadoTituloDownload.ToString();
            instance.DownloadCifrado = objets.DownloadCifrado.ToString();
            instance.DownloadRenderizadoColor = objets.DownloadRenderizadoColor.ToString();
            instance.DownloadRenderizadoGlyphicon = objets.DownloadRenderizadoGlyphicon.ToString();
            instance.DownloadRenderizadoOneTexto = objets.DownloadRenderizadoOneTexto.ToString();
            instance.DownloadRenderizadoSecTexto = objets.DownloadRenderizadoSecTexto.ToString();
            instance.DownloadRenderizadoMensajeImpt = objets.DownloadRenderizadoMensajeImpt.ToString();

            switch (instance.DownloadCifrado)
            {
                case "NDY="://ASISTENCIA
                    instance.Path = request + "/Asistencia/DownloadFileCargaMasiva?template=" + objets.DownloadCifrado.ToString();
                    break;
                case "NTA="://ASISTENCIA RETAIL
                    instance.Path = request + "/Asistencia/DownloadFileCargaMasivaDinamico?template=" + objets.DownloadCifrado.ToString();
                    break;
                case "NTQ="://ASISTENCIA RELOJ CONTROL
                    instance.Path = request + "/Asistencia/DownloadFileCargaMasivaRelojControl?template=" + objets.DownloadCifrado.ToString();
                    break;
                case "NTI="://ASISTENCIA RELOJ CONTROL GEOVICTORIA
                    instance.Path = request + "/Asistencia/DownloadFileCargaMasivaRelojControl?template=" + objets.DownloadCifrado.ToString();
                    break;
                case "NDg="://HORAS EXTRAS
                    instance.Path = request + "/Asistencia/DownloadFileCargaMasiva?template=" + objets.DownloadCifrado.ToString();
                    break;
                case "NTY="://BONOS
                    instance.Path = request + "/Asistencia/DownloadFileCargaMasiva?template=" + objets.DownloadCifrado.ToString();
                    break;
                default:
                    instance.Path = request + "/Operaciones/DownloadFileCargaMasiva?template=" + objets.DownloadCifrado.ToString();
                    break;
            }

            return instance;
        }
    }
}