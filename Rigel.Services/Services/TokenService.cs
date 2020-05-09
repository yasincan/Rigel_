using Rigel.Services.Contracts;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Rigel.Services.Services
{
    public class TokenService : ITokenService
    {
        public string GenerateToken(string userName)
        {
            var someClaims = new Claim[]{
                new Claim(ClaimTypes.Role,"Administrator"),
                new Claim(ClaimTypes.Role,"Admin"),
                new Claim(JwtRegisteredClaimNames.UniqueName,userName),
                new Claim(JwtRegisteredClaimNames.Email,"heimdall@mail.com"),
                new Claim(JwtRegisteredClaimNames.NameId,Guid.NewGuid().ToString())
            };

            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("tsdasdas das dasd as est"));
            var token = new JwtSecurityToken(
                issuer: "west-world",
                audience: "heimdall",
                claims: someClaims, 
                expires: DateTime.Now.AddMinutes(3),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.RsaSsaPssSha256)
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
