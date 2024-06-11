using Microsoft.AspNetCore.Mvc;
using WebAppTruck.Models.Services;
using WebAppTruck.Models.ViewModels;

namespace WebAppTruck.Controllers;

public class ProfileController : Controller
{
 private readonly IConfiguration _configuration;
 private readonly ProfileSrv profileSrv;

 public ProfileController(IConfiguration configuration)
 {
   _configuration = configuration;
    profileSrv = new ProfileSrv(_configuration["ConnectionStrings:Cnx"]?? "");
 }

 [HttpPost]
 public JsonResult List(ProfileVM profileVM){
    return Json(profileSrv.List(profileVM));
 }
}
