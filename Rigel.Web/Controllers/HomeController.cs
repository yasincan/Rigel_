using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Rigel.Business.Attributes;
using Rigel.ViewModels;
using System.Collections.Generic;
using System.Diagnostics;

namespace Rigel.Web.Controllers
{
    public class HomeController : Controller
    {
        readonly static List<string> values= new List<string> { "", "", "" };

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.Client, NoStore = true)]
        public IActionResult Error()
        {
            return View(new Error { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public List<string> ListValues()
        {
            return values;
        }
    }
}
