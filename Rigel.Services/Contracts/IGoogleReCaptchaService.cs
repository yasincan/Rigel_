using System;
using System.Collections.Generic;
using System.Text;

namespace Rigel.Services.Contracts
{
    public interface IGoogleReCaptchaService: IDisposable
    {
        bool IsReCaptchaValidate(string responseKey);
    }
}
