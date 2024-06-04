using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppTruck.Models.ViewModels
{
  public class ResponseVM : Prms
  {
    public object Data { get; set; } = new { };
  }
  public class ResponseVM<T> : Prms where T : new()
  {
    public T Data { get; set; } = new T();
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
      setData(true, "Procesos realizado correctamente.", "Success", "", "");
    }
    public void Success(string message)
    {
      setData(true, message, "Success", "", "");
    }
    public void Error(string message)
    {
      setData(false, message, "Error", "", "");
    }
    public void Error(Exception ex)
    {
      setData(false, ex.Message, "Error", "", "");
    }
    public void Info(string message)
    {
      setData(false, message, "Info", "", "");
    }
    public void Find()
    {
      setData(true, "Información encontrada", "Info", "", "");
    }
    public void NotFind()
    {
      setData(false, "Información no encontrada", "Info", "", "");
    }
    private void setData(bool Ok, string Message, string Type, string Exception, string Other)
    {
      this.Ok = Ok;
      this.Message = Message;
      this.Type = Type;
      this.Exception = Exception;
      this.Other = Other;
    }
  }
}