using Microsoft.AspNetCore.Mvc;
using System;

namespace Rigel.API.Controllers
{
    public class PromotionController : BaseController
    {
        [HttpGet("ödenek")]
        public IActionResult Get()
        {
            foreach (var claim in HttpContext.User.Claims)
            {
                Console.WriteLine("Claim Type: {0}:\nClaim Value:{1}\n", claim.Type, claim.Value);
            }
            var promotionCode = Guid.NewGuid();
            return new ObjectResult($"Your promotion code is {promotionCode}");
        }
    }
}