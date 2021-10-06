using Crawler.DbModels;
using Crawler.Services.Exceptions;
using Crawler.Services.Extensions;
using Crawler.Services.Models.ResponseModels;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Crawler.Services
{
    public class TestService
    {
        private readonly CrawlerService _crawlerService;
        private readonly IRepository<Test> _testRepository;
        private readonly IRepository<TestResult> _testResultRepository;

        public TestService(CrawlerService crawlerService, IRepository<Test> testRepository, IRepository<TestResult> testResultRepository)
        {
            _crawlerService = crawlerService;
            _testRepository = testRepository;
            _testResultRepository = testResultRepository;
        }

        public async Task<int> CreateTestAsync(string url)
        {
            try
            {
                return await _crawlerService.InterractAsync(url);
            }
            catch (Exception err)
            {
                throw new CrawlerApiException(err.Message);
            }
        }

        public TestResultsServiceModel GetTestResults(int id)
        {
             string url = _testRepository.GetById(id)?.Url ?? "";

             if (url == "")
             {
                throw new Exception("There is no such id");
             }

             return new TestResultsServiceModel()
             {
                 Url = url,
                 Results = _testResultRepository
                             .GetAll()
                             .Where(x => x.TestId == id)
                             .OrderBy(x => x.ResponseTime)
             };

        }

        public TestsServiceModel GetTests(int page = 1 , int pageSize = 10)
        {
            if (page < 1)
            {
                page = 1;
            }

            var testResults = _testRepository.GetAll()
                .Paginate(page, pageSize);
                

            PageInfoModel pageInfo = new PageInfoModel { PageNumber = page, PageSize = pageSize, TotalItems = _testRepository.GetAll().Count() };

            return new TestsServiceModel() { Tests = testResults, PageInfo = pageInfo };
        }
    }
}
