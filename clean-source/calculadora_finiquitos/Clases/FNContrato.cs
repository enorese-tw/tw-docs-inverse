using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;


namespace FiniquitosV2.Clases
{
    public class FNContrato
    {
        public int ID { get; set; }
        public int IDDesvinculacion { get; set; }
        public string Ficha { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFinal { get; set; }
        public int Dias { get; set; }
        public string Causal { get; set; }
        public int Estado { get; set; }
        public string EstadoTexto { get; set; }


        private string strSql;
        private DataSet ds;

        public void ObtenerIdFNContrato(string connectionString)
        {
            string strSql = string.Format("Select MAX(ID) as ID From FNContratos");
            ds = Clases.Interface.ExecuteDataSet(connectionString, strSql);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        DataRow dr = ds.Tables[0].Rows[0];
                        if (dr["ID"] != System.DBNull.Value)
                        {
                            ID = int.Parse(dr["ID"].ToString()) + 1;
                        }
                        else { ID = 1; }

                    }
                }
            }
        }

        public void Grabar(string connectionString)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand comando = new SqlCommand();
            string dia = DateTime.Now.Day.ToString();
            string mes = DateTime.Now.Month.ToString();
            string year = DateTime.Now.Year.ToString();
            string hour = DateTime.Now.Hour.ToString();
            string minutes = DateTime.Now.Minute.ToString();
            string second = DateTime.Now.Second.ToString();
            try
            {
                if (ID > -1)
                {
                    strSql = string.Format("INSERT INTO FNContratos (ID,IDDesvinculacion,Ficha,FechaInicio,FechaFinal,Dias,Causal,Estado, Seguimiento, FechaSeguimiento) VALUES ({0},{1},'{2}', CAST('{3}' AS DATETIME), CAST('{4}' AS DATETIME),{5},'{6}',{7},'{8}',{9})",
                        ID,
                        IDDesvinculacion,
                        Ficha,
                        FechaInicio,
                        FechaFinal,
                        Dias,
                        Causal,
                        Estado,
                        "CALCULADO",
                        "GETDATE()");
                }
                else
                {
                    //strSql = string.Format("UPDATE CLIENTE SET TC_ID_TIPO_CLIENTE = @IdTipoCliente, CPC_ID_CONDICION_PAGO = @IdCondicionPago, TF_ID_TIPO_FACTURACION = @IdTipoFacturacion, EC_ID_ESTADO_CLIENTE = @IdEstadoCliente, CLI_RUT = @Rut, CLI_RAZON_SOCIAL = @RazonSocial, CLI_GIRO = @Giro, CLI_PAYER = @Payer, CLI_SHIP_TO = @ShipTo, CLI_ACUERDO_ABASTECIMIENTO = @AcuerdoAbastecimiento, CLI_REPRESENTANTE_LEGAL = @RepresentanteLegal , CLI_EXTENSION_ARCHIVO = @ExtensionArchivo WHERE CLI_ID_CLIENTE = {0}", Id);
                }
                Interface.ExecuteQuery(Properties.Settings.Default.connectionString, strSql);


            }
            catch (Exception ex)
            {
                Utilidades.LogError(connectionString, "FN_ENCDESVINCULACION.Grabar", ex.Message);
                //descripcion = string.Format("Error: {0}", ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
    }
}