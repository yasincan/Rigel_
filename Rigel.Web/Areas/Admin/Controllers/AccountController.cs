using Microsoft.AspNetCore.Mvc;
using Rigel.Business.Attributes;
using Rigel.Business.Models.ViewModels;

namespace Rigel.Web.Areas.Admin.Controllers
{
   [Area("Admin")]
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateGoogleReCaptcha("LoginFormKey")]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
              
            }
            return View();
        }
    }
}