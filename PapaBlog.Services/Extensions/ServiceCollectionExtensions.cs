using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PapaBlog.Data.Abstract;
using PapaBlog.Data.Concrete;
using PapaBlog.Data.Concrete.EfCore.Contexts;
using PapaBlog.Entities.Concrete;
using PapaBlog.Services.Abstract;
using PapaBlog.Services.Concrete;
using System;

namespace PapaBlog.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadMyService(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IArticleService, ArticleManager>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICommentService, CommentManager>();

            return services;
        }

        public static IServiceCollection LoadMyUserSetting(this IServiceCollection services, string connecitonString)
        {
            services.AddDbContext<PapaBlogContext>(options =>
            {
                options.UseSqlServer(connecitonString);
            });

            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;

                options.User.RequireUniqueEmail = false;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+/";
            })
                .AddEntityFrameworkStores<PapaBlogContext>();

            return services;
        }

        public static IServiceCollection LoadMyCookieSetting(this IServiceCollection services)
        {
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Admin/User/Login");
                options.LogoutPath = new PathString("/Admin/User/Logout");
                options.AccessDeniedPath = new PathString("/Admin/User/AccessDenied");

                options.Cookie = new CookieBuilder
                {
                    Name = "PapaBlog",
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict,
                    SecurePolicy = CookieSecurePolicy.SameAsRequest
                };
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(15);
            });

            return services;
        }
    }
}
