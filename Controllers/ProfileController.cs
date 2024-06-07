using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebAppTruck.Models.DAO;
using WebAppTruck.Models.DTO;

namespace WebAppTruck.Controllers;

public class ProfileController : Controller
{
 private readonly IConfiguration _configuration;
 private readonly ProfileDAO profileDAO;

 public ProfileController(IConfiguration configuration)
 {
   _configuration = configuration;
    profileDAO = new ProfileDAO(_configuration["ConnectionStrings:Cnx"]?? "");
 }

 [HttpPost]
 public JsonResult List(ProfileDTO profileDTO){
    return Json(profileDAO.List(profileDTO));
 }
}
