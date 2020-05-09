using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rigel.Business.Attributes;
using Rigel.ViewModels;

namespace Rigel.Web.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateGoogleReCaptcha]
        public IActionResult Login(User user)
        {
            var usr = new User
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                IsActive = true,
                Password = "11",
                Todoes = new List<Todo>
                {
                    new Todo {
                    Id = Guid.NewGuid(),
                    TodoName="yasi",
                    CreatedDate = DateTime.Now,
                    IsActive = true
                    }
                },
                UserName = "yasin.can",
                DeletedDate=DateTime.Now,
                UpdatedDate=DateTime.Now
                
            };

            if (ModelState.IsValid)
            {
                var x = 15;
            }


            return View();

        }
    }
}