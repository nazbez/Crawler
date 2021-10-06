using Crawler.Logic.Parsers;
using Microsoft.Extensions.DependencyInjection;

namespace Crawler.Logic.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static void AddCrawlerLogicServices(this IServiceCollection services)
        {
            services.AddScoped<Downloader>();
            services.AddScoped<Timer>();
            services.AddScoped<Validator>();
            services.AddScoped<CrawlerHandler>();
            services.AddScoped<ParserHtml>();
            services.AddScoped<ParserSitemap>();
            services.AddScoped<HtmlCrawler>();
            services.AddScoped<SitemapCrawler>();
        }
    }
}
