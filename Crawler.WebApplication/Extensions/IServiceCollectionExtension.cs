using Microsoft.Extensions.DependencyInjection;
using Crawler.WebApplication.Services;

namespace Crawler.WebApplication.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static void AddWebAppServices(this IServiceCollection services)
        {
            services.AddScoped<DbMapper>();
            services.AddScoped<CrawlerService>();
        }
    }
}
