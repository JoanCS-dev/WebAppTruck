using Microsoft.AspNetCore.Mvc;
using WebAppTruck.Models.Services;
using WebAppTruck.Models.ViewModels;

namespace WebAppTruck.Controllers
{
    public class AuthController : Controller
    {
        private readonly AccountSrv _accounSrv;
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
            _accounSrv = new AccountSrv(_configuration["ConnectionStrings:Cnx"] ?? "");
        }

        public IActionResult Auth()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            ResponseVM<string> response = _accounSrv.Login(email, password);

            if (response.Ok)
            {
                return Json(new { success = true, redirectUrl = Url.Action("Dashboard", "Dashboard") });
            }
            else
            {
                return Json(new { success = false, message = response.Message });
            }
        }
    }
}
