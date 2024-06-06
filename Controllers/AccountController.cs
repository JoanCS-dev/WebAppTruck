using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebAppTruck.Models.DAO;
using WebAppTruck.Models.ViewModels;

namespace WebAppTruck.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountDAO _accountDAO;
        private readonly IConfiguration _configuration;

        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
            _accountDAO = new AccountDAO(_configuration["ConnectionStrings:Cnx"] ?? "");
        }

        public IActionResult Auth()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            ResponseVM<string> response = _accountDAO.Login(email, password);

            if (response.Ok)
            {
                return Json(new { success = true, redirectUrl = Url.Action("Index", "Home") });
            }
            else
            {
                return Json(new { success = false, message = response.Message });
            }
        }
    }
}
