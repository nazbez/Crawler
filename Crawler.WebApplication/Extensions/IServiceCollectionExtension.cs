using Microsoft.Extensions.DependencyInjection;
using Crawler.WebApplication.Services;

namespace Crawler.WebApplication.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static void AddWebAppServices(this IServiceCollection services)
        {
            services.AddScoped<DbService>();
            services.AddScoped<CrawlerService>();
        }
    }
}
