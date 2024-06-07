using WebAppTruck.Models.ViewModels;
using System;
using Microsoft.Data.SqlClient;
using System.Data;
using WebAppTruck.Models.DTO;

namespace WebAppTruck.Models.DAO{
    public class ProfileDAO : DBAccess{
        public ProfileDAO(string connectionString) : base (connectionString){}

        public ResponseVM<List<ProfileDTO>> List (ProfileDTO profileDTO){
            ResponseVM<List<ProfileDTO>> res = new ResponseVM<List<ProfileDTO>>(){
                Data = new List<ProfileDTO>()
            };
            try{
                var command = new SqlCommand("SPProfiles", Open()) {CommandType = System.Data.CommandType.StoredProcedure};
                command.Parameters.AddRange(_parameters(profileDTO, profileDTO.ProfileID == 0 ? "SELECT_ALL": "SELECT_BY_ID"));
                using (var dr = command.ExecuteReader())
                {
                    res.HasRow(dr);
                    while(dr.Read()){
                        res.Data.Add(new ProfileDTO(){
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

        private SqlParameter[] _parameters(ProfileDTO profileDTO, String Action) {
            return new SqlParameter[] {
                new SqlParameter("@Id", profileDTO.ProfileID),
                new SqlParameter("@Descripcion", profileDTO.ProDescription),
                new SqlParameter("@Estatus", profileDTO.ProStatus),
                new SqlParameter("@Fecha", profileDTO.ProRDate),
                new SqlParameter("@Action", Action)
            };
        }

    }
}