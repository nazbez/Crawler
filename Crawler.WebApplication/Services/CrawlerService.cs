using Crawler.Logic;

namespace Crawler.WebApplication.Services
{
    public class CrawlerService
    {
        private readonly CrawlerHandler _crawlerHandler;
        private readonly DbHandler _dbHandler;

        public CrawlerService(CrawlerHandler crawlerHandler, DbHandler dbHandler)
        {
            _crawlerHandler = crawlerHandler;
            _dbHandler = dbHandler;
        }

        public void Crawl(string url)
        {
            var resultOfCrawling = _crawlerHandler.GetLinksFromHtmlAndSitemap(url);

            var resultOfResponsing = _crawlerHandler.GetResponseTime(resultOfCrawling);

            _dbHandler.SaveResultAsync(url, resultOfCrawling, resultOfResponsing).Wait();
        }
    }
}
