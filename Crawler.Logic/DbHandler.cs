using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Crawler.DbModels;
using Crawler.Logic.Models;

namespace Crawler.Logic
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
            Test test = new Test { Url = url };
            await _testRepository.AddAsync(test);
            await _testRepository.SaveChangesAsync();

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
