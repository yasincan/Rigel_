using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Rigel.Services.Contracts;

namespace Rigel.Business.Attributes
{
    public class ValidateGoogleReCaptcha : ActionFilterAttribute
    {
        private readonly string _appSettingSectionKey;
        public ValidateGoogleReCaptcha(string appSettingSectionKey)
        {
            _appSettingSectionKey = appSettingSectionKey;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var _googleReCaptcha = context.HttpContext.RequestServices.GetService<IGoogleReCaptchaService>();
            var captchaResponseKey = context.HttpContext.Request.Form["g-recaptcha-response"];

            bool isCaptchaValidate = _googleReCaptcha.IsReCaptchaValidate(captchaResponseKey, _appSettingSectionKey);

            if (!isCaptchaValidate)
                context.ModelState.AddModelError("Captcha", "Doğrulama hatalıdır.");
        }
    }
}
