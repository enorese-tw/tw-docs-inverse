using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace FiniquitosV2.Clases
{
    public class FNDescuentoFiniquito
    {
        public int ID { get; set; }
        public int IDDesvinculacion { get; set; }
        public string Descripcion { get; set; }
        public int Monto { get; set; }

        private string strSql;
        private DataSet ds;

        public void ObtenerId(string connectionString)
        {
            string strSql = string.Format("Select MAX(ID) as ID From FNDescuentosFiniquito");
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
            try
            {
                if (ID > -1)
                {
                    strSql = string.Format("INSERT INTO FNDescuentosFiniquito (ID,IDDesvinculacion,Descripcion,Monto) VALUES ({0},{1},'{2}',{3})",
                        ID,
                        IDDesvinculacion,
                        Descripcion,
                        Monto);
                }
                else
                {
                    //strSql = string.Format("UPDATE CLIENTE SET TC_ID_TIPO_CLIENTE = @IdTipoCliente, CPC_ID_CONDICION_PAGO = @IdCondicionPago, TF_ID_TIPO_FACTURACION = @IdTipoFacturacion, EC_ID_ESTADO_CLIENTE = @IdEstadoCliente, CLI_RUT = @Rut, CLI_RAZON_SOCIAL = @RazonSocial, CLI_GIRO = @Giro, CLI_PAYER = @Payer, CLI_SHIP_TO = @ShipTo, CLI_ACUERDO_ABASTECIMIENTO = @AcuerdoAbastecimiento, CLI_REPRESENTANTE_LEGAL = @RepresentanteLegal , CLI_EXTENSION_ARCHIVO = @ExtensionArchivo WHERE CLI_ID_CLIENTE = {0}", Id);
                }
                Interface.ExecuteQuery(Properties.Settings.Default.connectionString, strSql);
            }
            catch (Exception ex)
            {
                Utilidades.LogError(connectionString, "FNDescuentosFiniquito.Grabar", ex.Message);
                Descripcion = string.Format("Error: {0}", ex.Message);
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