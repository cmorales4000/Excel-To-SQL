using ExcelToSQL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ExcelToSQL
{
    public class ExcelService
    {
        public bool Save(StaffInfoViewModel model)
        {

            foreach (var item in model.StaffList)
            {

                using (SqlConnection con = new SqlConnection(/*your connectionstring*/))
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.CommandText = "[dbo].[Survey.List]";
                            cmd.Connection = con;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("url", model.url);
                            cmd.Parameters.AddWithValue("empId", model.empId);
                            cmd.Parameters.AddWithValue("camId", model.camId);
                            cmd.Parameters.AddWithValue("locId", model.locId);
                            cmd.Parameters.AddWithValue("locNom", model.locNom);
                            using (var reader = cmd.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    reader.Read();
                                }
                            }
                            con.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                    return true;
                }

            }

            return true;

        }
    }
}
