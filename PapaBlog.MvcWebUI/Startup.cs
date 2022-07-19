using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NToastNotify;
using PapaBlog.MvcWebUI.AutoMapper.Profiles;
using PapaBlog.MvcWebUI.Helpers.Abstract;
using PapaBlog.MvcWebUI.Helpers.Concrete;
using PapaBlog.Services.AutoMapper.Profiles;
using PapaBlog.Services.Extensions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PapaBlog.MvcWebUI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()
                .AddNToastNotifyToastr(new ToastrOptions()
                {
                    ProgressBar = true,
                    CloseButton = true,
                    TimeOut = 6000,
                    PositionClass = ToastPositions.TopRight
                })
                .AddJsonOptions(opt =>
                {
                    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
                    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                });

            services.AddSession();
            services.AddAutoMapper(
                typeof(ArticleProfile),
                typeof(CategoryProfile),
                typeof(UserProfile),
                typeof(ViewModelsProfile));

            services.LoadMyUserSetting(Configuration.GetConnectionString("LocalDb"));
            services.LoadMyService();
            services.LoadMyCookieSetting();

            services.AddScoped<IImageHelper, ImageHelper>();
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

            app.UseNToastNotify();

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