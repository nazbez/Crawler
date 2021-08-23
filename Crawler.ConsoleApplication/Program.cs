using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Crawler.Persistence;
using Crawler.Logic.Extensions;
using Crawler.ConsoleApplication.Extensions;
using Crawler.DbLogic.Extensions;

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
                    services.AddCrawlerLogicServices();
                    services.AddDbHandler();
                    services.AddConsoleApplicationServices();
                }).ConfigureLogging(options => options.SetMinimumLevel(LogLevel.Error));
    }
}



