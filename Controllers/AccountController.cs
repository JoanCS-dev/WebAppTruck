using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebAppTruck.Controllers
{
  public class AccountController : Controller
  {
    private IConfiguration _configuration;
    public AccountController(IConfiguration _configuration){
      this._configuration = _configuration;
    }
    public IActionResult Auth()
    {
      ViewData["Cnx"] = _configuration["ConnectionStrings:Cnx"]??"";
      return View();
    }
  }
}