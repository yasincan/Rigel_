using Microsoft.Extensions.DependencyInjection;

namespace Rigel.Services.Extentsions
{
    public static class ServiceExtension
    {
        public  static IServiceCollection ServiceLayer(this IServiceCollection services)
        {
            return services;
        }
    }
}
