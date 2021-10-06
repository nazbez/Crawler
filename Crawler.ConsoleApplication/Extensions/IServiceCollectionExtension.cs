using Microsoft.Extensions.DependencyInjection;

namespace Crawler.ConsoleApplication.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static void AddConsoleApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<Printer>();
            services.AddScoped<ConsoleApp>();
        }
    }
}
