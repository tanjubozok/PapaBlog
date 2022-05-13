using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PapaBlog.MvcWebUI.AutoMapper.Profiles;
using PapaBlog.Services.AutoMapper.Profiles;
using PapaBlog.Services.Extensions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PapaBlog.MvcWebUI
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.Converters.Add(
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));

                opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });

            services.AddSession();
            services.AddAutoMapper(typeof(ArticleProfile), typeof(CategoryProfile), typeof(UserProfile));

            services.LoadMyService();
            services.LoadMyUserSetting();
            services.LoadMyCookieSetting();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }

            app.UseSession();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "Admin",
                    areaName: "Admin",
                    pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
