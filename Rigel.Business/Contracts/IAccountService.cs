using Rigel.Business.Models.Dtos;
using Rigel.Business.Models.JWTModels;
using System.Threading.Tasks;

namespace Rigel.Business.Contracts
{
    public interface IAccountService
    {
        Task<string> Authenticate(UserForLoginDto userDto);
    }
}
