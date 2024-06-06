using System;
using Microsoft.Data.SqlClient;

namespace WebAppTruck.Models.ViewModels
{
    public class ResponseVM : Prms
    {
        public object Data { get; set; } = new { };
    }

    public class ResponseVM<T> : Prms
    {
        public T Data { get; set; }

        public ResponseVM()
        {
            Data = default(T);
        }
    }

    public class Prms
    {
        public bool Ok { get; set; } = false;
        public string Message { get; set; } = "";
        public string Type { get; set; } = "";
        public string Exception { get; set; } = "";
        public string Other { get; set; } = "";

        public void Success()
        {
            SetData(true, "Proceso realizado correctamente.", "Success", "", "");
        }

        public void Success(string message)
        {
            SetData(true, message, "Success", "", "");
        }

        public void Error(string message)
        {
            SetData(false, message, "Error", "", "");
        }

        public void Error(Exception ex)
        {
            SetData(false, ex.Message, "Error", "", "");
        }

        public void Info(string message)
        {
            SetData(false, message, "Info", "", "");
        }

        public void Find()
        {
            SetData(true, "Información encontrada", "Info", "", "");
        }

        public void NotFind()
        {
            SetData(false, "Información no encontrada", "Info", "", "");
        }

        public void HasRow(SqlDataReader sqlDataReader){
            if(sqlDataReader.HasRows) Find();
            else NotFind();
        }
        private void SetData(bool ok, string message, string type, string exception, string other)
        {
            Ok = ok;
            Message = message;
            Type = type;
            Exception = exception;
            Other = other;
        }
    }
}
