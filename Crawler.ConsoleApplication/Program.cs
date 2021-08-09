using Crawler.Logic;
using Crawler.Logic.Parsers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Crawler.Persistence;

namespace Crawler.ConsoleApplication
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();
            var app = host.Services.GetService<ConsoleApp>();
            app.Interract();
            await host.RunAsync();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddEfRepository<CrawlerDbContext>(options => options.UseSqlServer(@"Server=localhost;Database=CrawlerDB;Trusted_Connection=True"));
                    services.AddScoped<DbHandler>();
                    services.AddScoped<Downloader>();
                    services.AddScoped<Timer>();
                    services.AddScoped<Validator>();
                    services.AddScoped<CrawlerService>();
                    services.AddScoped<ParserHtml>();
                    services.AddScoped<ParserSitemap>();
                    services.AddScoped<HtmlCrawler>();
                    services.AddScoped<SitemapCrawler>();
                    services.AddScoped<Printer>();
                    services.AddScoped<ConsoleApp>();
                }).ConfigureLogging(options => options.SetMinimumLevel(LogLevel.Error));
    }
}



