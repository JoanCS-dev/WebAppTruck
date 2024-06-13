using System.Data;
using Microsoft.Data.SqlClient;
using WebAppTruck.Models.ViewModels;

namespace WebAppTruck.Models.Services
{
    public class ModuleSrv : DBAccess
    {
        private readonly DBAccess cnx;

        public ModuleSrv(string connectionString) : base(connectionString)
        {
            this.cnx = new DBAccess(connectionString);
        }

        public ResponseVM<List<ModuleVM>> List(ModuleVM moduleVM)
        {
            ResponseVM<List<ModuleVM>> res = new ResponseVM<List<ModuleVM>>()
            {
                Data = new List<ModuleVM>()
            };

            try
            {
                var command = new SqlCommand("SPModule", Open()) { CommandType = CommandType.StoredProcedure };
                command.Parameters.AddRange(_parameters(moduleVM, moduleVM.ModuleID == 0 ? "SELECT_ALL" : "SELECT_BY_ID"));

                using (var dr = command.ExecuteReader())
                {
                    res.HasRow(dr);
                    while (dr.Read())
                    {
                        res.Data.Add(new ModuleVM
                        {
                            ModuleID = GetInt(dr, "ModuleID"),
                            MoDescription = GetString(dr, "MoDescription"),
                            MoController =GetString(dr, "MoController"),
                            MoIcon = GetString(dr, "MoIcon"),
                            MoStatus = GetString(dr, "MoStatus"),
                            MoPosition = GetInt(dr, "MoPosition"),
                            MoRDate = GetString(dr, "MoRDate"),
                        });
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

        public ResponseVM AddUpdate(ModuleVM moduleVM)
        {
            ResponseVM res = new ResponseVM();

            try
            {
                var command = new SqlCommand("SPModule", Open()) { CommandType = CommandType.StoredProcedure };
                command.Parameters.AddRange(_parameters(moduleVM, moduleVM.ModuleID == 0 ? "INSERT" : "UPDATE"));

                using (var dr = command.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        res.DBCatchResponseInOneLine(dr);
                    }
                }
            }
            catch (Exception e)
            {
                res.Error(e);
            }
            finally
            {
                Close();
            }
            return res;
        }

        private SqlParameter[] _parameters(ModuleVM moduleVM, string action)
        {
            return new SqlParameter[]
            {
                new SqlParameter("@ModuleID", moduleVM.ModuleID),
                new SqlParameter("@MoDescription", moduleVM.MoDescription),
                new SqlParameter("@MoController", moduleVM.MoController),
                new SqlParameter("@MoIcon", moduleVM.MoIcon),
                new SqlParameter("@MoStatus", moduleVM.MoStatus),
                new SqlParameter("@MoPosition", moduleVM.MoPosition),
                new SqlParameter("@MoRDate", moduleVM.MoRDate),
                new SqlParameter("@Case", action)
            };
        }
    }
}
