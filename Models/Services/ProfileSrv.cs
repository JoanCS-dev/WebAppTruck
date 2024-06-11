using WebAppTruck.Models.ViewModels;
using Microsoft.Data.SqlClient;

namespace WebAppTruck.Models.Services{
    public class ProfileSrv : DBAccess{
        public ProfileSrv(string connectionString) : base (connectionString){}

        public ResponseVM<List<ProfileVM>> List (ProfileVM profileVM){
            ResponseVM<List<ProfileVM>> res = new ResponseVM<List<ProfileVM>>(){
                Data = new List<ProfileVM>()
            };
            try{
                var command = new SqlCommand("SPProfiles", Open()) {CommandType = System.Data.CommandType.StoredProcedure};
                command.Parameters.AddRange(_parameters(profileVM, profileVM.ProfileID == 0 ? "SELECT_ALL": "SELECT_BY_ID"));
                using (var dr = command.ExecuteReader())
                {
                    res.HasRow(dr);
                    while(dr.Read()){
                        res.Data.Add(new ProfileVM(){
                           ProfileID = GetInt(dr,"ProfileID"),
                           ProDescription = GetString(dr,"ProDescription"),
                           ProStatus = GetString(dr,"ProStatus"),
                           ProRDate = GetString(dr,"ProRDate")
                        });
                    }
                }
                Close();

            }
            catch(Exception e){
                Close();
                res.Error(e);
            }
            return res;
        }

        private SqlParameter[] _parameters(ProfileVM profileVM, String Action) {
            return new SqlParameter[] {
                new SqlParameter("@Id", profileVM.ProfileID),
                new SqlParameter("@Descripcion", profileVM.ProDescription),
                new SqlParameter("@Estatus", profileVM.ProStatus),
                new SqlParameter("@Fecha", profileVM.ProRDate),
                new SqlParameter("@Action", Action)
            };
        }

    }
}