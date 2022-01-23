using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rigel.Business.Contracts
{
    public interface IGoogleReCaptchaService : IDisposable
    {
        bool IsReCaptchaValidate(string responseKey, string appSettingSectionKey);
    }
}
