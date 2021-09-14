using System;
using System.Linq;
using System.Threading.Tasks;
using Crawler.Services.Exceptions;
using Crawler.Services.Models.ResponseModels;
using Crawler.Services.Extensions;

namespace Crawler.Services
{
    public class TestService
    {
        private readonly CrawlerService _crawlerService;
        private readonly DbHandler _dbHandler;

        public TestService(CrawlerService crawlerService, DbHandler dbHandler)
        {
            _crawlerService = crawlerService;
            _dbHandler = dbHandler;
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
             string url = _dbHandler.GetTestById(id)?.Url ?? "";

             if (url == "")
             {
                throw new Exception("There is no such id");
             }

             return new TestResultsServiceModel()
             {
                 Url = url,
                 Results = _dbHandler.GetTestResultsByTestId(id)
             };

        }

        public TestsServiceModel GetTests(int page = 1 , int pageSize = 10)
        {
            if (page < 1)
            {
                page = 1;
            }

            var testResults = _dbHandler.GetAllTests()
                .Paginate(page, pageSize);
                

            PageInfoModel pageInfo = new PageInfoModel { PageNumber = page, PageSize = pageSize, TotalItems = _dbHandler.GetAllTests().Count() };

            return new TestsServiceModel() { Tests = testResults, PageInfo = pageInfo };
        }
    }
}
