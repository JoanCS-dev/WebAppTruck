using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace WebAppTruck.Controllers;

public class HomeController : Controller
{
  public IActionResult Index()
  {
    return View();
  }
}