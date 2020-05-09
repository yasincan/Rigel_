using Microsoft.AspNetCore.Mvc.Filters;
using Rigel.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Routing;

namespace Rigel.Business.Attributes
{
    public class ValidateGoogleReCaptcha : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var _googleReCaptcha = context.HttpContext.RequestServices.GetService<IGoogleReCaptchaService>();
            var captchaResponseKey = context.HttpContext.Request.Form["g-recaptcha-response"];

            bool isCaptchaValidate = _googleReCaptcha.IsReCaptchaValidate(captchaResponseKey);

            //var routeValues = new RouteValueDictionary
            //    {
            //        { "controller","Account"},
            //        { "action","Login"},
            //        { "areas","Admin"}
            //    };
            if (!isCaptchaValidate)
                context.ModelState.AddModelError("Captcha", "Doğrulama hatalıdır.");
        }
    }
}
