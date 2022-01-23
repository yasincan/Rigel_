using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Rigel.Business.Contracts;
using Rigel.Business.Models.Dtos;
using Rigel.Business.Models.JWTModels;
using Rigel.Data.RigelDB.Concretes.Entities;
using Rigel.Data.RigelDB.Contracts;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Rigel.Business.Concrete
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtSettings _jwtSettings;
        public AccountService(IUnitOfWork unitOfWork, IOptions<JwtSettings> jwtSettings)
        {
            _unitOfWork = unitOfWork;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<string> Authenticate(UserForLoginDto userDto)
        {
            var user = await _unitOfWork.Repository<User>().FindAsync(x => x.UserName == userDto.UserName && x.Password == userDto.Password);
            if (user == null)
                return null;

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim("CreatedDate", user.CreatedDate.ToString("yyyy-MM-dd")),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
             };

            var token = new JwtSecurityToken(_jwtSettings.Issuer,
             _jwtSettings.Issuer,
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            var result = new JwtSecurityTokenHandler().WriteToken(token);
            return await Task.FromResult(result);
        }

    }
}
