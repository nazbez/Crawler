using Crawler.Logic;
using System.Linq;

namespace Crawler.ConsoleApplication
{
    public class ConsoleApp
    {
        private readonly ConsoleImitator _console;
        private readonly CrawlerService _service;
        private readonly Printer _printer;

        public ConsoleApp(ConsoleImitator console, CrawlerService service, Printer printer)
        {
            _console = console;
            _service = service;
            _printer = printer;
        }

        public void Interract()
        {
            _console.WriteLine("Enter the url adress");

            string url = _console.ReadLine();

            if (!IsValid(url))
            {
                _console.WriteLine("Error! Invalid input");
                return;
            }

            _console.WriteLine("\nWait please...\n");

            url = DeleteSlashAtEnd(url);

            var result = _service.Crawl(url);

            _printer.PrintDifference(result.Where(x => x.IsInHtml && !x.IsInSitemap), "html");

            _printer.PrintDifference(result.Where(x => !x.IsInHtml && x.IsInSitemap), "sitemap");

            _printer.PrintTimeOfResponse(result);

            _printer.PrintCountOfLinks(result.Where(x => x.IsInHtml).ToList(), "html");

            _printer.PrintCountOfLinks(result.Where(x => x.IsInSitemap).ToList(), "sitemap");
        }

        private bool IsValid(string url)
        {
            return url.StartsWith("https://") || url.StartsWith("http://");
        }

        private string DeleteSlashAtEnd(string url)
        {
            return url.EndsWith('/') ?
                url.Substring(0, url.Length - 1) :
                url;
        }
    }
}
