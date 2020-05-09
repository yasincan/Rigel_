using Microsoft.Extensions.DependencyInjection;
using Rigel.Data.Contracts;
using Rigel.Data.Repository;

namespace Rigel.Business.Extentsions
{
    public static class DataExtension
    {
        public  static IServiceCollection DataServiceLayer(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
