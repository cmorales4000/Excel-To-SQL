using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;


    public class ConsultaDatos
    {
        public static List<DataTable> executeStoreProcedure(string procedureName, List<Parametros> lstParams, string connectionString)
        {
            List<DataTable> functionReturnValue = null;
            SqlConnection connection = new SqlConnection(connectionString);
            List<DataTable> lstDt = new List<DataTable>();
            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            DataSet dsData = null;
            DataTable dtTmp = null;
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(procedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                //Asignamos los parametros
                foreach (Parametros param in lstParams)
                {
                    command.Parameters.Add(new SqlParameter(param.getNombre, param.Type, param.getTamanio));
                }

                //Asignamos los valores
                for (int i = 0; i <= lstParams.Count - 1; i++)
                {
                    command.Parameters[i].Direction = lstParams[i].getDireccion;
                    if (lstParams[i].getDireccion == ParameterDirection.Input)
                    {
                        command.Parameters[i].Value = lstParams[i].getValor;
                    }
                }

                dataAdapter.SelectCommand = command;
                dsData = new DataSet();
                dataAdapter.Fill(dsData);
                //Tabla de los datos devueltos
                if (dsData != null)
                {
                    for (int i = 0; i <= dsData.Tables.Count - 1; i++)
                    {
                        dtTmp = dsData.Tables[i];
                        dtTmp.TableName = "TablaDatos" + i;
                        lstDt.Add(dtTmp);
                    }
                }
                //Tabla con los parametros de salida
                dtTmp = new DataTable();
                dtTmp.TableName = "ParamsSalida";
                foreach (Parametros param in lstParams)
                {
                    if (param.getDireccion == ParameterDirection.Output)
                    {
                        dtTmp.Columns.Add(param.getNombre, Type.GetType("System.String"));
                    }
                }
                if (dtTmp.Columns.Count > 0)
                {
                    object[] valueParam = new object[dtTmp.Columns.Count];
                    int count = 0;
                    foreach (DataColumn dc in dtTmp.Columns)
                    {
                        valueParam[count] = command.Parameters[dc.ColumnName].Value.ToString();
                        count++;
                    }
                    dtTmp.Rows.Add(valueParam);
                }
                lstDt.Add(dtTmp);

            }
            catch (Exception ex)
            {
                LogError.insertarLog("ConsultaDatos.cs", "executeStoreProcedure", "", ex.Message.ToString(), "-1", "-1");
            }
            finally
            {
                dataAdapter.Dispose();
                connection.Close();
                functionReturnValue = lstDt;
            }
            return functionReturnValue;
        }
    }