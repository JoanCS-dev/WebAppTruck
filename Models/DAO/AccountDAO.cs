using WebAppTruck.Models.ViewModels;
using System;
using Microsoft.Data.SqlClient;
using System.Data;
using WebAppTruck.Models.DTO;
using Azure;

namespace WebAppTruck.Models.DAO
{
    public class AccountDAO : DBAccess
    {
        

        public AccountDAO(string connectionString) : base(connectionString) { }

        public ResponseVM<List<AccountDTO>> List(AccountDTO accountDTO){
             ResponseVM<List<AccountDTO>> res = new ResponseVM<List<AccountDTO>>(){
                Data = new List<AccountDTO>()
             };
            
            try{
               var command = new SqlCommand("SPAccountNew", Open()) {CommandType = System.Data.CommandType.StoredProcedure};
               command.Parameters.AddRange(_parameters(accountDTO, accountDTO.AccountID == 0 ? "SELECT_ALL" : "SELECT_BY_ID"));
                using (var dr = command.ExecuteReader())
                {
                    res.HasRow(dr);
                    while(dr.Read()){
                       res.Data.Add(new AccountDTO{
                           AccountID = GetInt(dr, "AccountID"),
                           AcUser = GetString(dr, "AcUser"),
                           AcEmailAddress = GetString(dr, "AcEmailAddress"),
                           AcPhoneNumber = GetString(dr, "AcPhoneNumber"),
                           AcRDate = GetString(dr, "AcRDate"),
                           AcStatus = GetString(dr, "AcStatus"),
                           PeopleID = GetInt(dr, "PeopleID"),
                           PeFirstName = GetString(dr, "PeFirstName"),
                           PeLastName = GetString(dr, "PeLastName"),
                           PeMiddleInitial = GetString(dr, "PeMiddleInitial"),
                           ProfileID = GetInt(dr, "ProfileID"),
                           ProDescription = GetString(dr, "ProDescription")
                       });
                    
                    }

                }
                Close();
            }
            catch(Exception ex){
                Close();
                res.Error(ex);
            }
            return res;
        }

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

        public ResponseVM AddUpdate(AccountDTO accountDTO){
            ResponseVM res = new ResponseVM();

            try{
                var command = new SqlCommand("SPAccountNew", Open()) {CommandType = System.Data.CommandType.StoredProcedure};
               command.Parameters.AddRange(_parameters(accountDTO, accountDTO.AccountID == 0 ? "ADD" : "UPDATE"));
                using (var dr = command.ExecuteReader())
                {
                    
                    if(dr.Read()){
                        res.DBCatchResponseInOneLine(dr);
                    }

                }
            }catch (Exception e){
                Close();
                res.Error(e);
            }
            return res;
        }
       
        private SqlParameter [] _parameters(AccountDTO accountDTO, String Action){
            return new SqlParameter[]{
                    new SqlParameter("@Id", accountDTO.AccountID),
                    new SqlParameter("@User", accountDTO.AcUser),
                    new SqlParameter("@Password", accountDTO.AcPassword),
                    new SqlParameter("@Email", accountDTO.AcEmailAddress),
                    new SqlParameter("@PhoneNumber", accountDTO.AcPhoneNumber),
                    new SqlParameter("@Status", accountDTO.AcStatus),
                    new SqlParameter("@Fecha", accountDTO.AcRDate),
                    new SqlParameter("@PeopleID", accountDTO.PeopleID),
                    new SqlParameter("@FirstName", accountDTO.PeFirstName),
                    new SqlParameter("@MiddleInitial", accountDTO.PeMiddleInitial),
                    new SqlParameter("@LastName", accountDTO.PeLastName),
                    new SqlParameter("@Pestatus", accountDTO.PeStatus),
                    new SqlParameter("@PeFecha", accountDTO.PeRDate),
                    new SqlParameter("@ProfileID", accountDTO.ProfileID),
                    new SqlParameter("@Descripcion", accountDTO.ProDescription),
                    new SqlParameter("@Prostatus", accountDTO.ProStatus),
                    new SqlParameter("@ProFecha", accountDTO.ProRDate),
                    new SqlParameter("@Action", Action)
            };
        }

    }
}
