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

        [HttpPost]
        public JsonResult AssignPermission(long profileID, List<long> permissionIDs, List<long> submodulesIDs)
        {
            var result = _permissionSrv.AssignPermissions(profileID, permissionIDs, submodulesIDs);
            return Json(result);
        }

    }
}
