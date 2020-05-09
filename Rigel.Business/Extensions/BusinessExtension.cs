using Microsoft.Extensions.DependencyInjection;
using Rigel.Business.Concrete;
using Rigel.Business.Contracts;

namespace Rigel.Business.Extentsions
{
    public static class BusinessExtension
    {
        public  static IServiceCollection BusinessServiceLayer(this IServiceCollection services)
        {
            services.AddScoped<ITodoService, TodoManager>();
            services.AddScoped<ICategoryService, CategoryManager>();
            return services;
        }
    }
}
