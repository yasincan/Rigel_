using Rigel.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Rigel.API.Controllers
{
    [AllowAnonymous]
    public class TokenController : Controller
    {
        private readonly ITokenService _tokenService;
        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("new")]
        public IActionResult GetToken([FromBody]Models.UserInfo user)
        {
            if (IsValidUserAndPassword(user.UserName, user.Password))
                return new ObjectResult(_tokenService.GenerateToken(user.UserName));
            return Unauthorized();
        }

        private bool IsValidUserAndPassword(string userName, string password)
        {
            //Sürekli true dönüyor. Normalde bir Identity mekanizması ile entegre etmemiz lazım.
            return true;
        }
    }
}