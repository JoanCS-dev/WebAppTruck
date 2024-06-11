using Microsoft.AspNetCore.Mvc;

namespace WebAppTruck.Controllers{

public class DashboardController : Controller{

public IActionResult Dashboard()
    {
      
      return View();
    }
}

}