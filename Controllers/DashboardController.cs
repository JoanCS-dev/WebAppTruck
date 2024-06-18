using Microsoft.AspNetCore.Mvc;

namespace WebAppTruck.Controllers
{

  public class DashboardController : Controller
  {

    public IActionResult Dashboard()
    {
      var email = HttpContext.Session.GetString("UserEmail");
      ViewBag.UserEmail = email;
      return View();
    }
  }
}