using Microsoft.AspNetCore.Mvc;

namespace WebAppTruck.Controllers;

public class HomeController : Controller
{
  public IActionResult Index()
  {
    var email = HttpContext.Session.GetString("UserEmail");
    ViewBag.UserEmail = email;
    return View();
  }
}
