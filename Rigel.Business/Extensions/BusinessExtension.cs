using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Rigel.Business.Concrete;
using Rigel.Business.Contracts;
using Rigel.Data.RigelDB.Concretes.Context;
using Rigel.Data.RigelDB.Concretes.Repositories;
using Rigel.Data.RigelDB.Contracts;
using System.Text;

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

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer(options =>
                 {
                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateIssuer = true,
                         ValidateAudience = true,
                         ValidateLifetime = true,
                         ValidateIssuerSigningKey = true,
                         ValidIssuer = configuration["Jwt:Issuer"],
                         ValidAudience = configuration["Jwt:Issuer"],
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                     };
                 });



            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Models.ModelMapper.AutoMapping());
            });
            IMapper mapper = mappingConfig.CreateMapper();


            services.AddSingleton(mapper);


            services.Configure<GoogleReCaptchaSettings>(options => configuration.GetSection("GoogleReCaptchaSettings"));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IGoogleReCaptchaService, GoogleReCaptchaManager>();
            services.AddScoped<ITodoService, TodoManager>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<IAccountService, AccountService>();

            return services;
        }
    }
}
