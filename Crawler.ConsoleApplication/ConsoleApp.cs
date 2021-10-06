using Crawler.Logic;
using Crawler.Services;
using System;
using System.Linq;

namespace Crawler.ConsoleApplication
{
    public class ConsoleApp
    {
        private readonly Printer _printer;
        private readonly CrawlerHandler _service;
        private readonly CrawlerService _crawlerService;

        public ConsoleApp(Printer printer, CrawlerHandler service, CrawlerService crawlerService)
        {
            _printer = printer;
            _service = service;
            _crawlerService = crawlerService;
        }

        public async void Interract()
        {
            Console.WriteLine("Enter the url");

            string url = DeleteSlashAtTheEnd(Console.ReadLine());

            try
            {
                Console.WriteLine("\nWait for crawling...\n");

                var crawlingResults = _service.GetLinksFromHtmlAndSitemap(url);

                _printer.PrintHtmlLinks(crawlingResults
                    .Where(x => x.IsInHtml && !x.IsInSitemap)
                    .Select(x => x.Url));

                _printer.PrintSitemapLinks(crawlingResults
                    .Where(x => !x.IsInHtml && x.IsInSitemap)
                    .Select(x => x.Url));

                Console.WriteLine("\nWait for checking response time...\n");

                var timeOfResponseResults = _service.GetResponseTime(crawlingResults);

                timeOfResponseResults = timeOfResponseResults.OrderBy(x => x.Time);

                _printer.PrintTimeOfResponse(timeOfResponseResults);

                _printer.PrintCountOfLinks(crawlingResults.Count(x => x.IsInHtml), crawlingResults.Count(x => x.IsInSitemap));

                Console.WriteLine("\nSaving to database\n");

                await _crawlerService.SaveResultAsync(url, crawlingResults, timeOfResponseResults);
            }
            catch (ArgumentException err)
            {
                Console.WriteLine(err.Message);
            }

            Environment.Exit(0);
        }

        private string DeleteSlashAtTheEnd(string url)
        {
            return url.EndsWith("/") ?
                url.Substring(0, url.Length - 1) :
                url;
        }
    }
}
