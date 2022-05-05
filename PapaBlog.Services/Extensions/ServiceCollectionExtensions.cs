using Microsoft.Extensions.DependencyInjection;
using PapaBlog.Data.Abstract;
using PapaBlog.Data.Concrete;
using PapaBlog.Data.Concrete.EfCore.Contexts;
using PapaBlog.Services.Abstract;
using PapaBlog.Services.Concrete;

namespace PapaBlog.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadMyService(this IServiceCollection services)
        {
            services.AddDbContext<PapaBlogContext>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IArticleService, ArticleManager>();
            services.AddScoped<ICategoryService, CategoryManager>();

            return services;
        }
    }
}
