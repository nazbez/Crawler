using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crawler.Services.Models.RequestModels;
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

        public async Task<int> CreateTestAsync(UserInputModel userInput)
        {
            try
            {
                return await _crawlerService.InterractAsync(userInput.Url);
            }
            catch (Exception err)
            {
                throw new CrawlerApiException(err.Message);
            }
        }

        public TestResultsModel GetTestResults(int id)
        {
            string url = _dbHandler.GetTestById(id)?.Url ?? "";

            if (url == "")
            {
                return null; 
            }
            
            var results = _dbHandler.GetTestResultsByTestId(id)
                .Select(x => new ResultModel
                {
                    Url = x.Url,
                    InHtml = x.InHtml,
                    InSitemap = x.InSitemap,
                    ResponseTime = x.ResponseTime
                });

            return new TestResultsModel
            {
                Url = url,
                Results = results
            };
        }

        public TestsPageModel GetTests(int page = 1 , int pageSize = 10)
        {
            var testResults =  _dbHandler.GetAllTests()
                .Paginate(page, pageSize)
                .Select(x => new TestModel()
                {
                    Url = x.Url,
                    Id = x.Id,
                    SaveTime = x.SaveTime
                });

            PageInfoModel pageInfo = new PageInfoModel { PageNumber = page, PageSize = pageSize, TotalItems = _dbHandler.GetAllTests().Count() };

            return new TestsPageModel() { Tests = testResults, PageInfo = pageInfo };
        }
    }
}
