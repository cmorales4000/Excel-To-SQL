using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for Juegos
/// </summary>
public class Juegos
{
    public Juegos()
    {
    }

    public static List<DataTable> checkJuegos(string funcion, string param1, string param2, string param3, string param4, string param5, string param6, string param7, string param8, string param9, string param10, string xmlData, string usuario, string idEmpSes)
    {
        List<DataTable> functionReturnValue = null;
        List<DataTable> lstDt = new List<DataTable>();
        try
        {
            List<Parametros> lstParams = new List<Parametros>();
            object _param1 = DBNull.Value;
            if (param1 != "") _param1 = param1;
            object _param2 = DBNull.Value;
            if (param2 != "") _param2 = param2;
            object _param3 = DBNull.Value;
            if (param3 != "") _param3 = param3;
            object _param4 = DBNull.Value;
            if (param4 != "") _param4 = param4;
            object _param5 = DBNull.Value;
            if (param5 != "") _param5 = param5;
            object _param6 = DBNull.Value;
            if (param6 != "") _param6 = param6;
            object _param7 = DBNull.Value;
            if (param7 != "") _param7 = param7;
            object _param8 = DBNull.Value;
            if (param8 != "") _param8 = param8;
            object _param9 = DBNull.Value;
            if (param9 != "") _param9 = param9;
            object _param10 = DBNull.Value;
            if (param10 != "") _param10 = param10;
            lstParams.Add(new Parametros("@funcion", SqlDbType.VarChar, funcion, ParameterDirection.Input, 4));
            lstParams.Add(new Parametros("@param1", SqlDbType.VarChar, _param1, ParameterDirection.Input, 500));
            lstParams.Add(new Parametros("@param2", SqlDbType.VarChar, _param2, ParameterDirection.Input, 500));
            lstParams.Add(new Parametros("@param3", SqlDbType.VarChar, _param3, ParameterDirection.Input, 500));
            lstParams.Add(new Parametros("@param4", SqlDbType.VarChar, _param4, ParameterDirection.Input, 500));
            lstParams.Add(new Parametros("@param5", SqlDbType.VarChar, _param5, ParameterDirection.Input, 500));
            lstParams.Add(new Parametros("@param6", SqlDbType.VarChar, _param6, ParameterDirection.Input, 500));
            lstParams.Add(new Parametros("@param7", SqlDbType.VarChar, _param7, ParameterDirection.Input, 500));
            lstParams.Add(new Parametros("@param8", SqlDbType.VarChar, _param8, ParameterDirection.Input, 500));
            lstParams.Add(new Parametros("@param9", SqlDbType.VarChar, _param9, ParameterDirection.Input, 500));
            lstParams.Add(new Parametros("@param10", SqlDbType.Text, _param10, ParameterDirection.Input));
            lstParams.Add(new Parametros("@xmlData", SqlDbType.Text, xmlData, ParameterDirection.Input));
            lstParams.Add(new Parametros("@msgRta", SqlDbType.VarChar, DBNull.Value, ParameterDirection.Output, 1000));

            lstDt = ConsultaDatos.executeStoreProcedure("PA_SITE_JUEGOS", lstParams, ConexionDB.dbProduccion(idEmpSes));
        }
        catch (Exception ex)
        {
            LogError.insertarLog("Juegos.cs", "checkJuegos", "", ex.Message.ToString(), usuario, idEmpSes);
        }
        finally
        {
            functionReturnValue = lstDt;
        }
        return functionReturnValue;
    }

}