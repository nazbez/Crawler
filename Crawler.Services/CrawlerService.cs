using Crawler.Logic;
using System.Threading.Tasks;

namespace Crawler.Services
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

        public async Task<int> InterractAsync(string url)
        {
            url = DeleteSlashAtTheEnd(url);

            var crawledLinks = _crawlerHandler.GetLinksFromHtmlAndSitemap(url);

            var responseTimeResults = _crawlerHandler.GetResponseTime(crawledLinks);

            return await _dbHandler.SaveResultAsync(url, crawledLinks, responseTimeResults);
        }

        private string DeleteSlashAtTheEnd(string url)
        {
            return url.EndsWith("/") ?
                url.Substring(0, url.Length - 1) :
                url;
        }
    }
}
