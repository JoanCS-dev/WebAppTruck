using System.Data;
using Microsoft.Data.SqlClient;
using WebAppTruck.Models.ViewModels;

namespace WebAppTruck.Models.Services
{
    public class PermissionSrv : DBAccess
    {
        private readonly DBAccess cnx;

        public PermissionSrv(string connectionString) : base(connectionString)
        {
            this.cnx = new DBAccess(connectionString);
        }

        public ResponseVM<List<PermissionVM>> List(PermissionVM permissionVM)
        {
            ResponseVM<List<PermissionVM>> res = new ResponseVM<List<PermissionVM>>()
            {
                Data = new List<PermissionVM>()
            };

            try
            {
                var command = new SqlCommand("SPPermission", Open()) { CommandType = CommandType.StoredProcedure };
                command.Parameters.AddRange(_parameters(permissionVM, permissionVM.PermissionID == 0 ? "SELECT_ALL" : "SELECT_BY_ID"));

                using (var dr = command.ExecuteReader())
                {
                    res.HasRow(dr);
                    while (dr.Read())
                    {
                        res.Data.Add(new PermissionVM
                        {
                            PermissionID = GetInt(dr, "PermissionID"),
                            PerStatus = GetString(dr, "PerStatus"),
                            SubRDate = GetString(dr, "SubRDate"),
                            SubModuleID= GetInt(dr, "SubModuleID"),
                            ProfileID = GetInt(dr, "ProfileID")
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

        public ResponseVM AddUpdate(PermissionVM permissionVM)
        {
            ResponseVM res = new ResponseVM();

            try
            {
                var command = new SqlCommand("SPPermission", Open()) { CommandType = CommandType.StoredProcedure };
                command.Parameters.AddRange(_parameters(permissionVM, permissionVM.PermissionID == 0 ? "INSERT" : "UPDATE"));

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

        private SqlParameter[] _parameters(PermissionVM permissionVM, string action)
        {
            return new SqlParameter[]
            {
                new SqlParameter("@PermissionID", permissionVM.PermissionID),
                new SqlParameter("@PerStatus", permissionVM.PerStatus),
                new SqlParameter("@SubRDate", permissionVM.SubRDate),
                new SqlParameter("@SubModuleID", permissionVM.SubModuleID),
                new SqlParameter("@ProfileID", permissionVM.ProfileID),
                new SqlParameter("@Case", action)
            };
        }
    }
}
