using Microsoft.AspNetCore.Mvc;
using WebAppTruck.Models.Services;
using WebAppTruck.Models.ViewModels;

namespace WebAppTruck.Controllers
{
    public class AuthController : Controller
    {
        private readonly AccountSrv _accountSrv;
        private readonly IConfiguration _configuration;
        private readonly string _salt;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
            _accountSrv = new AccountSrv(_configuration["ConnectionStrings:Cnx"] ?? "");
            _salt = _configuration["AppSettings:Salt"] ?? "";
        }

        public IActionResult Auth()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            if (!string.IsNullOrEmpty(password))
            {
                password = Encrypt.GetSHA256(password, _salt);
            }

            ResponseVM<string> response = _accountSrv.Login(email, password);

            if (response.Ok)
            {
                var userName = _accountSrv.GetUserNameByEmail(email);
                HttpContext.Session.SetString("UserName", userName);
                return Json(new { success = true, redirectUrl = Url.Action("Dashboard", "Dashboard") });
            }
            else
            {
                return Json(new { success = false, message = response.Message });
            }
        }

        public IActionResult Dashboard()
        {
            var userName = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(userName))
            {
                return RedirectToAction("Auth");
            }
            ViewData["UserName"] = userName;
            return View();
        }
    }
}
