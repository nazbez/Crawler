using Crawler.WebApplication.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Crawler.WebApplication.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static void AddWebAppServices(this IServiceCollection services)
        {
            services.AddScoped<Mapper>();
        }
    }
}
