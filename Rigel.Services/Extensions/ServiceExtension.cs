using Rigel.Services.Contracts;
using Rigel.Services.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rigel.Services.Extentsions
{
    public static class ServiceExtension
    {
        public  static IServiceCollection ServiceLayer(this IServiceCollection services)
        {
            services.AddSingleton<ITokenService, TokenService>();
            services.AddScoped<IGoogleReCaptchaService, GoogleReCaptchaService>();
            return services;
        }
    }
}
