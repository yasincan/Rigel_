using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rigel.Business.Concrete;
using Rigel.Business.Contracts;
using Rigel.Data.RigelDB.Concretes.Context;
using Rigel.Data.RigelDB.Concretes.Repositories;
using Rigel.Data.RigelDB.Contracts;
using Rigel.Services.Contracts;
using Rigel.Services.Services;

namespace Rigel.Business.Extentsions
{
    public static class BusinessExtension
    {
        public static IServiceCollection BusinessServiceLayer(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<RigelContext>
                             /*options.UseInMemoryDatabase("RigelDB")*/
                             /*options.UseSqlServer(configuration.GetConnectionString("RigelConnectionMMSQL")*/
                (options => options.UseNpgsql(configuration.GetConnectionString("RigelConnectionPostgreSQL")));


            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Models.ModelMapper.AutoMapping());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);



            services.Configure<GoogleReCaptchaSettings>(options => configuration.GetSection("GoogleReCaptchaSettings"));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IGoogleReCaptchaService, GoogleReCaptchaService>();
            services.AddScoped<ITodoService, TodoManager>();
            services.AddScoped<ICategoryService, CategoryManager>();

            return services;
        }
    }
}
