using Microsoft.AspNetCore.Mvc;
using WebAppTruck.Models.Services;
using WebAppTruck.Models.ViewModels;

namespace WebAppTruck.Controllers
{
   public class ModuleController : Controller
    {
        private readonly ModuleSrv moduleSrv;
        private readonly IConfiguration _configuration;
       public ModuleController(IConfiguration configuration)
        {
            _configuration = configuration;
            moduleSrv = new ModuleSrv(_configuration["ConnectionStrings:Cnx"] ?? "");
        }
    public IActionResult Module()
    {

      return View();
    }
    [HttpPost]
        public JsonResult ListModule(ModuleVM moduleVM)
        {
           return Json(moduleSrv.List(moduleVM));
        }

  [HttpPost]
        public JsonResult AddUpdate(ModuleVM moduleVM)
        {
           return Json(moduleSrv.AddUpdate(moduleVM));
        }
  }
}