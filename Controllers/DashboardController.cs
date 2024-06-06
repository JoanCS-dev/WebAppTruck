using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAppTruck.Models.Services;

namespace WebAppTruck.Controllers{

public class DashboardController : Controller{

public IActionResult Dashboard()
    {
      
      return View();
    }
}

}