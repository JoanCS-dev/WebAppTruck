using Microsoft.AspNetCore.Mvc;
using WebAppTruck.Models.Services;
using WebAppTruck.Models.ViewModels;

namespace WebAppTruck.Controllers
{

    public class AccountController : Controller
    {
        private readonly AccountSrv _accountSrv;
        private readonly IConfiguration _configuration;
        private readonly string _salt;

        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
            _accountSrv = new AccountSrv(_configuration["ConnectionStrings:Cnx"] ?? "");
            _salt = _configuration["AppSettings:Salt"] ?? "";
        }

        public IActionResult Auth()
        {
            return View();
        }

        public IActionResult Admon()
        {
            var email = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Auth", "Account");
            }
            ViewBag.UserEmail = email;
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
                HttpContext.Session.SetString("UserEmail", email);
                return Json(new { success = true, redirectUrl = Url.Action("Index", "Home") });
            }
            else
            {
                return Json(new { success = false, message = response.Message });
            }
        }

        [HttpPost]
        public JsonResult List(AccountVM accountVM)
        {
            return Json(_accountSrv.List(accountVM));
        }

        [HttpPost]
        public JsonResult AddUpdate(AccountVM accountVM)
        {
            if (!string.IsNullOrEmpty(accountVM.AcPassword))
            {
                accountVM.AcPassword = Encrypt.GetSHA256(accountVM.AcPassword, _salt);
            }

            return Json(_accountSrv.AddUpdate(accountVM));
        }

        [HttpPost]
        public JsonResult HandleStatus(int AccountID, string Action)
        {
            var accountVM = new AccountVM
            {
                AccountID = AccountID,
                AcStatus = (Action == "Activate") ? "Activo" :
                           (Action == "Deactivate") ? "Pendiente" : ""
            };

            var result = _accountSrv.HandleStatus(accountVM, Action);

            return Json(result);
        }


        [HttpPost]
        public JsonResult ChangePass(int AccountID, string AcPassword)
        {
            if (!string.IsNullOrEmpty(AcPassword))
            {
                AcPassword = Encrypt.GetSHA256(AcPassword, _salt);
            }
            var accountVM = new AccountVM
            {
                AccountID = AccountID,
                AcPassword = AcPassword
            };

            return Json(_accountSrv.ChangePass(accountVM));
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Auth", "Account");
        }
    }
}
