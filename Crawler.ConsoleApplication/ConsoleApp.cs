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

            url = DeleteSlashAtEnd(url);

            var result = _service.Crawl(url);

            _console.WriteLine("\nLinks found only by html crawler\n");

            _printer.PrintDifference(result.Where(x => x.IsInHtml && !x.IsInSitemap));

            _console.WriteLine("\nLinks found only by sitemap crawler\n");

            _printer.PrintDifference(result.Where(x => !x.IsInHtml && x.IsInSitemap));

            _console.WriteLine("\nAll links with their time of response\n");

            _printer.PrintTimeOfResponse(result);

            _console.WriteLine($"\nCount of links founded by html crawler: {result.Where(x => x.IsInHtml).ToList().Count}");

            _console.WriteLine($"\nCount of links founded by sitemap crawler: {result.Where(x => x.IsInSitemap).ToList().Count}");

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
