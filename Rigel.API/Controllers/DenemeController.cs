using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Rigel.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DenemeController : ControllerBase
    {
        private readonly IStringLocalizer<DenemeController> _localizer;

        public DenemeController(IStringLocalizer<DenemeController> localizer)
        {
            _localizer = localizer;
        }


        [HttpGet]
        public string Get()
        {
            return _localizer["About Title"];
        }
    }
}