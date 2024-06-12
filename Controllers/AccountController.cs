using Microsoft.AspNetCore.Mvc;
using WebAppTruck.Models.Services;
using WebAppTruck.Models.ViewModels;

namespace WebAppTruck.Controllers
{
  public class AccountController : Controller
  {
    private readonly AccountSrv _accountSrv;
    private readonly IConfiguration _configuration;
    public AccountController(IConfiguration configuration)
    {
      _configuration = configuration;
      _accountSrv = new AccountSrv(_configuration["ConnectionStrings:Cnx"] ?? "");
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
  }

}