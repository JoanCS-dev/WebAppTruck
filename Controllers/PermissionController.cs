using Microsoft.AspNetCore.Mvc;
using WebAppTruck.Models.Services;
using WebAppTruck.Models.ViewModels;

namespace WebAppTruck.Controllers
{
   public class PermisionController : Controller
    {
        private readonly PermissionSrv permissionSrv;
        private readonly IConfiguration _configuration;
       public PermisionController(IConfiguration configuration)
        {
            _configuration = configuration;
            permissionSrv = new PermissionSrv(_configuration["ConnectionStrings:Cnx"] ?? "");
        }
    public IActionResult Permission()
    {

      return View();
    }
    [HttpPost]
        public JsonResult ListPermission(PermissionVM permissionVM)
        {
           return Json(permissionSrv.List(permissionVM));
        }

  [HttpPost]
        public JsonResult AddUpdate(PermissionVM permissionVM)
        {
           return Json(permissionSrv.AddUpdate(permissionVM));
        }
  }
}