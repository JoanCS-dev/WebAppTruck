using Microsoft.AspNetCore.Mvc;
using WebAppTruck.Models.Services;
using WebAppTruck.Models.ViewModels;


namespace WebAppTruck.Controllers
{
   public class SubModuleController : Controller
    {
        private readonly SubModuleSrv submoduleSrv;
        private readonly IConfiguration _configuration;
       public SubModuleController(IConfiguration configuration)
        {
            _configuration = configuration;
            submoduleSrv = new SubModuleSrv(_configuration["ConnectionStrings:Cnx"] ?? "");
        }
    public IActionResult SubModule()
    {
      var email = HttpContext.Session.GetString("UserEmail");
      ViewBag.UserEmail = email;
      return View();
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