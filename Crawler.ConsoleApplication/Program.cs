using Crawler.Logic;
using Crawler.Logic.Models;
using Crawler.Logic.Parsers;
using System.Collections.Generic;

namespace Crawler.ConsoleApplication
{
    public class Program
    {
        static void Main(string[] args)
        {
            Printer printer = new Printer();

            HtmlCrawler htmlCrawler = new HtmlCrawler(new ParserHtml(), new Downloader());
            SitemapCrawler sitemapCrawler = new SitemapCrawler(new ParserSitemap(), new Downloader());

            CrawlerService service = new CrawlerService(htmlCrawler, sitemapCrawler, new Validator(), new Timer());


            ConsoleApp app = new ConsoleApp(printer, service);

            app.Interract();
        }
    }
}
