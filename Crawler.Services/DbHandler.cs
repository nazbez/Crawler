using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Crawler.Logic.Models;
using Crawler.DbModels;

namespace Crawler.Services
{
    public class DbHandler
    {
        private readonly IRepository<Test> _testRepository;
        private readonly IRepository<TestResult> _testResultRepository;

        public DbHandler(IRepository<Test> testRepository, IRepository<TestResult> testResultRepository)
        {
            _testRepository = testRepository;
            _testResultRepository = testResultRepository;
        }

        public async Task<int> SaveResultAsync(string url, IEnumerable<CrawlingResult> crawlingResults, IEnumerable<TimeOfResponseResult> responseResults) 
        {
            Test test = new Test { Url = url,};

            var testResults = TransformToTestResultCollection(test, crawlingResults, responseResults);

            _testResultRepository.AddRange(testResults);
            await _testResultRepository.SaveChangesAsync();

            return test.Id;
        }

        public IEnumerable<Test> GetAllTests()
        {
            return _testRepository.GetAll();
        }

        public IEnumerable<TestResult> GetTestResultsByTestId(int id)
        {
            return _testResultRepository
                             .GetAll()
                             .Where(x => x.TestId == id)
                             .OrderBy(x => x.ResponseTime);
        }

        public IEnumerable<string> GetUrlsFoundOnlyInHtmlByTestId(int id)
        {
            return _testResultRepository.GetAll()
                .Where(x => x.TestId == id)
                .Where(x => !x.InSitemap && x.InHtml)
                .Select(x => x.Url);
        }

        public IEnumerable<string> GetUrlsFoundOnlyInSitemapByTestId(int id)
        {
            return _testResultRepository.GetAll()
               .Where(x => x.TestId == id)
               .Where(x => x.InSitemap && !x.InHtml)
               .Select(x => x.Url);
        }

        public string GetUrlByTestId(int id)
        {
            return _testRepository.GetById(id).Url;
        }

        public Test GetTestById(int id)
        {
            return _testRepository.GetById(id);

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
