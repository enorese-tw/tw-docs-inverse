using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace FiniquitosV2.Clases
{
    public class FN_ENCDESVINCULACIONES
    {
        private static string strSql;
        private static DataSet ds;

        public static List<FN_ENCDESVINCULACION> solicitudesBaja(string connectionString)
        {
            try
            {
                strSql = string.Format("SELECT D.ID AS ID, D.FECHAHORASOLICITUD AS FECHAHORASOLICITUD, D.USUARIOSOLICITUD AS USUARIOSOLICITUD, D.RUTTRABAJADOR AS RUTTRABAJADOR,D.FICHATRABAJADOR AS FICHATRABAJADOR,D.NOMBRECOMPLETOTRABAJADOR AS NOMBRECOMPLETOTRABAJADOR, ES.DESCRIPCION AS ESTADOTEXTO, D.IDCAUSAL AS IDCAUSAL,EM.NOMBRE AS EMPRESATEXTO, D.ESTADO_ACTUAL AS ESTADOACTUAL FROM FN_ENCDESVINCULACION D INNER JOIN FN_EMPRESAS EM ON EM.ID LIKE D.IDEMPRESA INNER JOIN FN_ESTADOS ES ON ES.ID LIKE D.ESTADOSOLICITUD WHERE ES.ID = 2 order by D.ID DESC");
                ds = Interface.ExecuteDataSet(connectionString, strSql);
                List<FN_ENCDESVINCULACION> result = new List<FN_ENCDESVINCULACION>();
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                FN_ENCDESVINCULACION FNDES = new FN_ENCDESVINCULACION();
                                FNDES.ID = int.Parse(dr["ID"].ToString());
                                FNDES.FECHAHORASOLICITUD = dr["FECHAHORASOLICITUD"].ToString();
                                FNDES.USUARIOSOLICITUD = dr["USUARIOSOLICITUD"].ToString();
                                FNDES.RUTTRABAJADOR = dr["RUTTRABAJADOR"].ToString();
                                FNDES.FICHATRABAJADOR = dr["FICHATRABAJADOR"].ToString();
                                FNDES.NOMBRECOMPLETOTRABAJADOR = dr["NOMBRECOMPLETOTRABAJADOR"].ToString();
                                FNDES.ESTADOTEXTO = dr["ESTADOTEXTO"].ToString();
                                FNDES.IDCAUSAL = dr["IDCAUSAL"].ToString();
                                FNDES.EMPRESATEXTO = dr["EMPRESATEXTO"].ToString();
                                FNDES.ESTADOACTUAL = dr["ESTADOACTUAL"].ToString();
                                result.Add(FNDES);
                            }
                            return result;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Utilidades.LogError(connectionString, "TelemedicionLib.Usuarios.Listar", ex.Message);
                return null;
            }
        }

        public static List<FN_ENCDESVINCULACION>ListarKAM(string connectionString, string kam)
        {
            try
            {
                strSql = string.Format("SELECT D.ID AS ID, D.FECHAHORASOLICITUD AS FECHAHORASOLICITUD, D.USUARIOSOLICITUD AS USUARIOSOLICITUD, D.RUTTRABAJADOR AS RUTTRABAJADOR,D.FICHATRABAJADOR AS FICHATRABAJADOR,D.NOMBRECOMPLETOTRABAJADOR AS NOMBRECOMPLETOTRABAJADOR, ES.DESCRIPCION AS ESTADOTEXTO, D.IDCAUSAL AS IDCAUSAL,EM.NOMBRE AS EMPRESATEXTO FROM FN_ENCDESVINCULACION D INNER JOIN FN_EMPRESAS EM ON EM.ID LIKE D.IDEMPRESA INNER JOIN FN_ESTADOS ES ON ES.ID LIKE D.ESTADOSOLICITUD where D.USUARIOSOLICITUD LIKE '" + kam + "' order by D.ID DESC ;");
                ds = Interface.ExecuteDataSet(connectionString, strSql);
                List<FN_ENCDESVINCULACION> result = new List<FN_ENCDESVINCULACION>();
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                FN_ENCDESVINCULACION FNDES = new FN_ENCDESVINCULACION();
                                FNDES.ID = int.Parse(dr["ID"].ToString());
                                FNDES.FECHAHORASOLICITUD = dr["FECHAHORASOLICITUD"].ToString();
                                FNDES.USUARIOSOLICITUD = dr["USUARIOSOLICITUD"].ToString();
                                FNDES.RUTTRABAJADOR = dr["RUTTRABAJADOR"].ToString();
                                FNDES.FICHATRABAJADOR = dr["FICHATRABAJADOR"].ToString();
                                FNDES.NOMBRECOMPLETOTRABAJADOR = dr["NOMBRECOMPLETOTRABAJADOR"].ToString();
                                FNDES.ESTADOTEXTO = dr["ESTADOTEXTO"].ToString();
                                FNDES.IDCAUSAL = dr["IDCAUSAL"].ToString();
                                FNDES.EMPRESATEXTO = dr["EMPRESATEXTO"].ToString();
                                result.Add(FNDES);
                            }
                            return result;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Utilidades.LogError(connectionString, "TelemedicionLib.Usuarios.Listar", ex.Message);
                return null;
            }
        }


    }
}