using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Rigel.Business.Models.ViewModels;
using System.Collections.Generic;
using System.Diagnostics;

namespace Rigel.Web.Controllers
{
    public class HomeController : Controller
    {
        readonly static List<string> values= new() { "", "", "" };

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
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public List<string> ListValues()
        {
            return values;
        }
    }
}
