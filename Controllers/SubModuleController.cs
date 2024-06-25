using Microsoft.AspNetCore.Mvc;
using WebAppTruck.Models.Services;
using WebAppTruck.Models.ViewModels;
using WebAppTruck.Models.Filters;

namespace WebAppTruck.Controllers
{
  [Session]
   public class SubModuleController : Controller
    {
        private readonly SubModuleSrv submoduleSrv;
        private readonly IConfiguration _configuration;
       public SubModuleController(IConfiguration configuration)
        {
            _configuration = configuration;
            submoduleSrv = new SubModuleSrv(_configuration["ConnectionStrings:Cnx"] ?? "");
        }
        
    [HttpPost]
        public JsonResult ListSubModule(SubModuleVM submoduleVM)
        {
           return Json(submoduleSrv.List(submoduleVM));
        }

  [HttpPost]
        public JsonResult AddUpdate(SubModuleVM submoduleVM)
        {
           return Json(submoduleSrv.AddUpdate(submoduleVM));
        }
  }
}