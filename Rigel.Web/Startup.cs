using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rigel.Business.Extentsions;
using Rigel.Data.Contexts;
using Rigel.Services.Extentsions;
using Rigel.Services.Services;
using Rigel.Web.AutoMapper;
using System.Text.Json.Serialization;

namespace Rigel.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<GoogleReCaptchaSettings>(Configuration.GetSection("GoogleReCaptchaSettings"));

            services.BusinessServiceLayer();
            services.DataServiceLayer();
            services.ServiceLayer();

            services.AddDbContext<RigelContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("RigelConnection"),
                    assembly => assembly.MigrationsAssembly(typeof(RigelContext).Assembly.FullName));
            });
           
            services.AddControllersWithViews()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                });
            services.AddSession();
            services.AddAutoMapper(typeof(Startup));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
           // app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                    name: "areas",
                    "Admin",
                    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
