using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAppTruck.Models.DAO;
using WebAppTruck.Models.DTO;
using WebAppTruck.Models.Services;
using WebAppTruck.Models.ViewModels;

namespace WebAppTruck.Controllers
{
  public class AccountDshController : Controller
  {
       private readonly AccountDAO _accountDAO;
        private readonly IConfiguration _configuration;
        public AccountDshController(IConfiguration configuration)
        {
            _configuration = configuration;
            _accountDAO = new AccountDAO(_configuration["ConnectionStrings:Cnx"] ?? "");
        }
    public IActionResult Account()
    {
      
      return View();
    }
     

  [HttpPost]
        public JsonResult List(AccountDTO accountDTO)
        {
           return Json(_accountDAO.List(accountDTO));
        }
  }
}