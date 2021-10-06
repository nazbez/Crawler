using System.Linq;
using Crawler.WebAPI.Models;
using Crawler.Services.Models.ResponseModels;

namespace Crawler.WebAPI.Services
{
    public class Mapper
    {
        public TestApiModel MapTests(TestsServiceModel testsModel)
        {
            var tests = testsModel.Tests
                .Select(x => new TestModel() 
                { 
                    Url = x.Url, 
                    Id = x.Id, 
                    SaveTime = x.SaveTime 
                });

            return new TestApiModel()
            {
                Tests = tests,
                PageInfo = testsModel.PageInfo
            };
        }

        public TestResultsApiModel MapTestResults(TestResultsServiceModel testResultsModel)
        {
            var testResults = testResultsModel.Results
                .Select(x => new TestResultsModel()
                {
                    Url = x.Url,
                    InHtml = x.InHtml,
                    InSitemap = x.InSitemap,
                    ResponseTime = x.ResponseTime
                });

            return new TestResultsApiModel()
            {
                Url = testResultsModel.Url,
                Results = testResults
            };
        }
    }
}
