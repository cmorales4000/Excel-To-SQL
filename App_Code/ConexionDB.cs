using System;
using System.Configuration;


public class ConexionDB
{
    

    
    public static string dbProduccion(string idEmpSes)
    {
        string sServidor = "Server=YOUR SERVER";
        string sCatalogo = "YOUR DEFAULT DB";
        string sUsuario = "YOUR DB USER";
        string sContrasena = "YOUR DB PASSWORD";
        string sParamExtra = "Persist Security Info=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Language=Spanish;";
        string sCadena = "";
        switch (idEmpSes)
        {
            case "2": //If have more than one database
                sCatalogo = "YOUR DB 2";
                break;
            case "3": //If have more than one database
                sCatalogo = "YOUR DB 3";
                break;
            
        }
        sCadena = sServidor + ";Initial Catalog=" + sCatalogo + ";User ID=" + sUsuario + ";Password=" + sContrasena + ";" + sParamExtra;
       
        return sCadena;
    }
}
