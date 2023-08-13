using System;
using System.Collections.Generic;
using System.Data;

    public class LogError
    {
        public static void insertarLog(string pagina, string funcion, string parametros, string logError, string usuario, string empId)
        {
            try
            {
                List<Parametros> lstParams = new List<Parametros>();
                lstParams.Add(new Parametros("@pagina", SqlDbType.VarChar, pagina, ParameterDirection.Input));
                lstParams.Add(new Parametros("@funcion", SqlDbType.VarChar, funcion, ParameterDirection.Input));
                lstParams.Add(new Parametros("@param", SqlDbType.VarChar, parametros, ParameterDirection.Input));
                lstParams.Add(new Parametros("@logError", SqlDbType.VarChar, logError, ParameterDirection.Input));
                lstParams.Add(new Parametros("@usuario", SqlDbType.VarChar, usuario, ParameterDirection.Input, 50));
                lstParams.Add(new Parametros("@empId", SqlDbType.VarChar, empId, ParameterDirection.Input, 10));

                ConsultaDatos.executeStoreProcedure("PA_SITE_INSERT_LOGERROR", lstParams, ConexionDB.dbProduccion(empId));
            }
            catch (Exception ex)
            {
            }
            finally
            {

            }
        }
    }