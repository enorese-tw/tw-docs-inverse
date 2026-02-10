using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;


namespace FiniquitosV2.Clases
{
    public class Usuario
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellidoP{ get; set; }
        public string apellidoM{ get; set; }
        public string correo{ get; set; }
        public string clave{ get; set; }
        public int idtipo { get; set; }

        private string strSql;
        private string strSqlDelete;
        private DataSet ds;

        public bool Login(string connectionString)
        {

            try
            {
                strSql = string.Format("SELECT * FROM FN_USUARIOS WHERE correo Like '{0}'", correo);
                DataSet ds = Interface.ExecuteDataSet(connectionString, strSql);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            if (1 == 1)//int.Parse(dr["EU_ID_ESTADO_USUARIO"].ToString()) == 1) // Usuario Activo
                            {
                                //string strClaveEncriptada = dr["USU_CLAVE"].ToString();
                                //string strClaveDesEncriptada = Utilidades.Desencriptar(strClaveEncriptada, "ecivreslio");
                                if (dr["clave"].ToString() ==clave)//strClaveDesEncriptada == TW_ClaveUsuario)
                                {
                                    id = int.Parse(dr["id"].ToString());
                                    idtipo = int.Parse(dr["idtipo"].ToString());
                                    //Obtener(connectionString);
                                    return true;
                                }
                                else
                                {
                                    nombre = "CLAVE INCORRECTA";
                                    return false;
                                }
                            }
                            else
                            {
                                nombre = "USUARIO NO ACTIVO.";
                                return false;
                            }
                        }
                        else
                        {
                            nombre = "USUARIO NO ENCONTRADO";
                            return false;
                        }
                    }
                    else
                    {
                        nombre = "USUARIO NO ENCONTRADO";
                        return false;
                    }
                }
                else
                {
                    nombre = "USUARIO NO ENCONTRADO";
                    return false;
                }
            }
            catch (Exception ex)
            {
                Utilidades.LogError(connectionString, "TW_Usuario.Login", ex.Message);
                nombre = string.Format("Error: {0}", ex.Message);
                return false;
            }
        }



    }
}