using FiniquitosV2.Clases;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;

namespace FiniquitosV2.api
{
    public class ResponseApi
    {
        string api = Properties.Settings.Default.ApiByf;
        string connectionString = Properties.Settings.Default.connectionPlataforma;

        public void CreatePdfDocument(string idfiniquito, string document, string glcid)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand comando = new SqlCommand();
            string sql = "";

            if (document == "caratula")
            {
                sql = "EXEC [finiquitos].[__PdfFiniquitoCaratula] " + idfiniquito;
            }

            if (document == "documento")
            {
                sql = "EXEC [finiquitos].[__PdfFiniquitoDocumento] " + idfiniquito;
            }

            if (document == "propuesta")
            {
                sql = "EXEC [finiquitos].[__PdfCarta] " + idfiniquito;
            }

            Interface.ExecuteQuery(connectionString, sql);
            connection.Close();
        }
        
    }
}