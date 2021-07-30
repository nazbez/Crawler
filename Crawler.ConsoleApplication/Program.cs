using Crawler.Logic;

namespace Crawler.ConsoleApplication
{
    public class Program
    {
        static void Main(string[] args)
        {
            ConsoleApp app = new ConsoleApp(new ConsoleImitator(),
                new CrawlerService(new HtmlCrawler(new ParserHtml(), new Downloader()),
                new SitemapCrawler(new ParserSitemap(), new Downloader()),
                new CrawlerLinksHandler(new Timer())), new Printer());

            app.Interract();

        }
    }
}
