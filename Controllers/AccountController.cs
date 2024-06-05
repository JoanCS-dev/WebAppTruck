using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAppTruck.Models.Services;

namespace WebAppTruck.Controllers
{
  public class AccountController : Controller
  {
    private IConfiguration _configuration;
    private AccountSrv accountSrv;
    public AccountController(IConfiguration _configuration){
      this._configuration = _configuration;
      this.accountSrv= new AccountSrv(_configuration["ConnectionStrings:Cnx"]??"");
    }
    public IActionResult Auth()
    {
      
      return View();
    }
  }
}