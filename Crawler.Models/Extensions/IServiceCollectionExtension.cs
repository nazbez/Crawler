using Microsoft.Extensions.DependencyInjection;

namespace Crawler.DbLogic.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static void AddDbHandler(this IServiceCollection services)
        {
            services.AddScoped<DbHandler>();
        }
    }
}
