using Microsoft.AspNetCore.Mvc;
using WebAppTruck.Models.Services;
using WebAppTruck.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace WebAppTruck.Controllers
{
    public class ProfilesController : Controller
    {
        private readonly ProfileSrv _profileSrv;
        private readonly IConfiguration _configuration;

        public ProfilesController(IConfiguration configuration)
        {
            _configuration = configuration;
            _profileSrv = new ProfileSrv(_configuration["ConnectionStrings:Cnx"] ?? "");
        }

        public IActionResult Admon()
        {
            var email = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserEmail = email;
            return View();
        }

        [HttpPost]
        public JsonResult List(ProfileVM profileVM)
        {
            return Json(_profileSrv.List(profileVM));
        }

        [HttpPost]
        public JsonResult AddUpdate(ProfileVM profileVM)
        {
            return Json(_profileSrv.AddUpdate(profileVM));
        }
    }
}
