using System;
using System.Text;

public class Validacion
{
    public Validacion()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public string EncryptarText(string strText)
    {

        return Encryptar(strText, "C2Ms3rv1c3");
    }

    public string DesencryptarText(string strText)
    {
        return Desencryptar(strText, "C2Ms3rv1c3");
    }

    private string Encryptar(string strText, string strEncrKey)
    {
        byte[] byKey = { };
        byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        try
        {
            byKey = System.Text.Encoding.UTF8.GetBytes(Left(strEncrKey, 8));

            System.Security.Cryptography.DESCryptoServiceProvider des = new System.Security.Cryptography.DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(strText);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            System.Security.Cryptography.CryptoStream cs = new System.Security.Cryptography.CryptoStream(ms, des.CreateEncryptor(byKey, IV), System.Security.Cryptography.CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());

        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }


    private string Desencryptar(string strText, string sDecrKey)
    {
        byte[] byKey = { };
        byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        byte[] inputByteArray = new byte[(strText.Length + 1)];

        try
        {
            byKey = System.Text.Encoding.UTF8.GetBytes(Left(sDecrKey, 8));
            System.Security.Cryptography.DESCryptoServiceProvider des = new System.Security.Cryptography.DESCryptoServiceProvider();
            inputByteArray = Convert.FromBase64String(strText);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            System.Security.Cryptography.CryptoStream cs = new System.Security.Cryptography.CryptoStream(ms, des.CreateDecryptor(byKey, IV), System.Security.Cryptography.CryptoStreamMode.Write);

            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;

            return encoding.GetString(ms.ToArray());

        }
        catch (Exception ex)
        {
            return ex.Message;
        }

    }


    public static string Left(string text, int length)
    {
        if (length < 0)
            throw new ArgumentOutOfRangeException("length", length, "length must be > 0");
        else if (length == 0 || text.Length == 0)
            return "";
        else if (text.Length <= length)
            return text;
        else
            return text.Substring(0, length);
    }
}