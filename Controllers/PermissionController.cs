using Microsoft.AspNetCore.Mvc;
using WebAppTruck.Models.Services;
using WebAppTruck.Models.ViewModels;
using WebAppTruck.Models.Filters;

namespace WebAppTruck.Controllers
{
    [Session]
    public class PermissionController : Controller
    {
        private readonly PermissionSrv _permissionSrv;
        private readonly IConfiguration _configuration;

        public PermissionController(IConfiguration configuration)
        {
            _configuration = configuration;
            _permissionSrv = new PermissionSrv(_configuration["ConnectionStrings:Cnx"] ?? "");
        }

        public IActionResult Admon()
        {
            var email = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserEmail = email;
            return View();
        }

        [HttpPost]
        public JsonResult ListPermission(PermissionVM permissionVM)
        {
            return Json(_permissionSrv.List(permissionVM));
        }

        [HttpPost]
        public JsonResult AddUpdate(PermissionVM permissionVM)
        {
            return Json(_permissionSrv.AddUpdate(permissionVM));
        }
    }
}
