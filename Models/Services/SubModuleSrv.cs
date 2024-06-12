using System.Data;
using Microsoft.Data.SqlClient;
using WebAppTruck.Models.ViewModels;

namespace WebAppTruck.Models.Services
{
    public class SubModuleSrv : DBAccess
    {
        private readonly DBAccess cnx;

        public SubModuleSrv(string connectionString) : base(connectionString)
        {
            this.cnx = new DBAccess(connectionString);
        }

        public ResponseVM<List<SubModuleVM>> List(SubModuleVM subModuleVM)
        {
            ResponseVM<List<SubModuleVM>> res = new ResponseVM<List<SubModuleVM>>()
            {
                Data = new List<SubModuleVM>()
            };

            try
            {
                var command = new SqlCommand("SPSubmodule", Open()) { CommandType = CommandType.StoredProcedure };
                command.Parameters.AddRange(_parameters(subModuleVM, subModuleVM.SubModuleID == 0 ? "SELECT_ALL" : "SELECT_BY_ID"));

                using (var dr = command.ExecuteReader())
                {
                    res.HasRow(dr);
                    while (dr.Read())
                    {
                        res.Data.Add(new SubModuleVM
                        {
                            SubModuleID = GetInt(dr, "SubModuleID"),
                            SubDescription = GetString(dr, "SubDescription"),
                            SubAction = GetString(dr, "SubAction"),
                            SubStatus = GetString(dr, "SubStatus"),
                            SubPosition = GetInt(dr, "SubPosition"),
                            SubRDate = GetString (dr,"SubRDate"),
                            ModuleID = GetInt(dr, "ModuleID"),
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

        public ResponseVM AddUpdate(SubModuleVM submoduleVM)
        {
            ResponseVM res = new ResponseVM();

            try
            {
                var command = new SqlCommand("SPSubmodule", Open()) { CommandType = CommandType.StoredProcedure };
                command.Parameters.AddRange(_parameters(submoduleVM, submoduleVM.SubModuleID == 0 ? "INSERT" : "UPDATE"));

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

        private SqlParameter[] _parameters(SubModuleVM submoduleVM, string action)
        {
            return new SqlParameter[]
            {
                new SqlParameter("@SubmoduleID", submoduleVM.SubModuleID),
                new SqlParameter("@SubDescription", submoduleVM.SubDescription),
                new SqlParameter("@SubAction", submoduleVM.SubAction),
                new SqlParameter("@SubStatus", submoduleVM.SubStatus),
                new SqlParameter("@SubPosition", submoduleVM.SubPosition),
                new SqlParameter("@SubRDate", submoduleVM.SubRDate),
                new SqlParameter("@ModuleID", submoduleVM.ModuleID),
                new SqlParameter("@Case", action)
            };
        }
    }
}
