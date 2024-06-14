using Microsoft.AspNetCore.Mvc;
using WebAppTruck.Models.Services;
using WebAppTruck.Models.ViewModels;
using System.Security.Cryptography;
using System.Text;
namespace WebAppTruck.Controllers
{
  public class AccountController : Controller
  {
    private readonly AccountSrv _accountSrv;

    private readonly IConfiguration _configuration;
    private readonly string _salt;
    public AccountController(IConfiguration configuration)
    {
      _configuration = configuration;
      _accountSrv = new AccountSrv(_configuration["ConnectionStrings:Cnx"] ?? "");
      _salt = _configuration["AppSettings:Salt"] ?? "";
    }
    public IActionResult Account()
    {

      return View();
    }


    [HttpPost]
    public JsonResult List(AccountVM accountVM)
    {
      return Json(_accountSrv.List(accountVM));
    }

    [HttpPost]
    public JsonResult AddUpdate(AccountVM accountVM)
    {
      if (!string.IsNullOrEmpty(accountVM.AcPassword))
      {
        accountVM.AcPassword = Encrypt.GetSHA256(accountVM.AcPassword, _salt);
      }

      return Json(_accountSrv.AddUpdate(accountVM));
    }

    [HttpPost]
    public JsonResult Activate(int AccountID)
    {
      var accountVM = new AccountVM
      {
        AccountID = AccountID,
        AcStatus = "Activo"
      };
      return Json(_accountSrv.Activate(accountVM));
    }
    [HttpPost]
    
    public JsonResult ChangePass(int AccountID, string AcPassword)
    {
      if (!string.IsNullOrEmpty(AcPassword))
      {
        AcPassword = Encrypt.GetSHA256(AcPassword, _salt);
      }
      var accountVM = new AccountVM
      {
        AccountID =  AccountID,
        AcPassword = AcPassword
      };

      return Json(_accountSrv.ChangePass(accountVM));
    }


  }

}




