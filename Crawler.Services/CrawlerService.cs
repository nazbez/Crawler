using Crawler.DbModels;
using Crawler.Logic;
using Crawler.Logic.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Crawler.Services
{
    public class CrawlerService
    {
        private readonly CrawlerHandler _crawlerHandler;
        private readonly IRepository<TestResult> _testResultRepository;

        public CrawlerService(CrawlerHandler crawlerHandler, IRepository<TestResult> testResultRepository)
        {
            _crawlerHandler = crawlerHandler;
            _testResultRepository = testResultRepository;
        }

        public async Task<int> InterractAsync(string url)
        {
            url = DeleteSlashAtTheEnd(url);

            var crawledLinks = _crawlerHandler.GetLinksFromHtmlAndSitemap(url);

            var responseTimeResults = _crawlerHandler.GetResponseTime(crawledLinks);

            return await SaveResultAsync(url, crawledLinks, responseTimeResults);
        }

        private string DeleteSlashAtTheEnd(string url)
        {
            return url.EndsWith("/") ?
                url.Substring(0, url.Length - 1) :
                url;
        }

        public async Task<int> SaveResultAsync(string url, IEnumerable<CrawlingResult> crawlingResults, IEnumerable<TimeOfResponseResult> responseResults)
        {
            Test test = new Test { Url = url, };

            var testResults = TransformToTestResultCollection(test, crawlingResults, responseResults);

            _testResultRepository.AddRange(testResults);
            await _testResultRepository.SaveChangesAsync();

            return test.Id;
        }

        private IEnumerable<TestResult> TransformToTestResultCollection(Test test, IEnumerable<CrawlingResult> crawlingResults,
            IEnumerable<TimeOfResponseResult> responseResults)
        {
            return crawlingResults.Join(responseResults,
                x => x.Url,
                y => y.Url,
                (x, y) => new TestResult
                {
                    Url = x.Url,
                    InHtml = x.IsInHtml,
                    InSitemap = x.IsInSitemap,
                    ResponseTime = y.Time,
                    Test = test
                });
        }
    }
}
