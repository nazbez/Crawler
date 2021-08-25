using Microsoft.Extensions.DependencyInjection;

namespace Crawler.Services.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static void AddLogicServices(this IServiceCollection services)
        {
            services.AddScoped<DbHandler>();
            services.AddScoped<CrawlerService>();
        }
    }
}
