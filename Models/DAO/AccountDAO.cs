using WebAppTruck.Models.ViewModels;
using System;
using Microsoft.Data.SqlClient;
using System.Data;

namespace WebAppTruck.Models.DAO
{
    public class AccountDAO : DBAccess
    {
        public AccountDAO(string connectionString) : base(connectionString) { }

        public ResponseVM<string> Login(string email, string password)
        {
            ResponseVM<string> res = new ResponseVM<string>();
            try
            {
                using (var connection = Open())
                using (var command = new SqlCommand("SPAccount", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@Email", email));
                    command.Parameters.Add(new SqlParameter("@Password", password));

                    using (var dr = command.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            string mensaje = dr.GetString(0);
                            res.Data = mensaje;

                            if (mensaje == "Acceso permitido")
                            {
                                res.Success(mensaje);
                            }
                            else
                            {
                                res.Error(mensaje);
                            }
                        }
                        else
                        {
                            res.NotFind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.Error(ex);
            }
            finally
            {
                Close();
            }
            return res;
        }
    }
}
