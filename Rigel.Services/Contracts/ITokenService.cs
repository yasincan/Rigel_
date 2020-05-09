using System;
using System.Collections.Generic;
using System.Text;

namespace Rigel.Services.Contracts
{
    public interface ITokenService
    {
        string GenerateToken(string userName);
    }
}
