using Microsoft.AspNetCore.Mvc;
using WebAppTruck.Models.Filters;
namespace WebAppTruck.Controllers
{
    [Session]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var email = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserEmail = email;
            return View();
        }
        public IActionResult Test()
        {
            var email = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserEmail = email;
            return View();
        }
    }
}
