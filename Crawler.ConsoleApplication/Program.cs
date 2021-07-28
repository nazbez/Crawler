using Crawler.Logic;

namespace Crawler.ConsoleApplication
{
    public class Program
    {
        static void Main(string[] args)
        {
            ConsoleApp application = new ConsoleApp(new ConsoleImitator(), 
                new CrawlerLinksHandler(new Timer()),
                new HtmlCrawler(new ParserHtml(), new Downloader()), 
                new SitemapCrawler(new ParserSitemap(), new Downloader()));

            application.Interract();
        }
    }
}
