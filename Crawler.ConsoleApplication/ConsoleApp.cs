using Crawler.Logic;
using System;
using System.Linq;

namespace Crawler.ConsoleApplication
{
    public class ConsoleApp
    {
        private readonly Printer _printer;
        private readonly CrawlerService _service;

        public ConsoleApp(Printer printer, CrawlerService service)
        {
            _printer = printer;
            _service = service;
        }

        public void Interract()
        {
            Console.WriteLine("Enter the url");

            string url = DeleteSlashAtTheEnd(Console.ReadLine());

            Console.WriteLine("\nWait...\n");

            try
            {
                var crawlingResults = _service.Crawl(url);

                _printer.PrintUniqueLinks(crawlingResults
                    .Where(x => x.IsInHtml && !x.IsInSitemap)
                    .Select(x => x.Url), "html");

                _printer.PrintUniqueLinks(crawlingResults
                    .Where(x => !x.IsInHtml && x.IsInSitemap)
                    .Select(x => x.Url), "sitemap");

                var timeOfResponseResults = _service.GetTimeOfResponses(crawlingResults);

                _printer.PrintTimeOfResponse(timeOfResponseResults.OrderBy(x => x.Time));

                _printer.PrintCountOfLinks(crawlingResults.Count(x => x.IsInHtml), crawlingResults.Count(x => x.IsInSitemap));
            }
            catch (ArgumentException err)
            {
                Console.WriteLine(err.Message);
            }
        }

        private string DeleteSlashAtTheEnd(string url)
        {
            return url.EndsWith("/") ?
                url.Substring(0, url.Length - 1) :
                url;
        }
    }
}
