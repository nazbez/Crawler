using Crawler.Services.Models.ResponseModels;
using Crawler.WebApplication.Models;
using System.Collections.Generic;
using System.Linq;

namespace Crawler.WebApplication.Services
{
    public class Mapper
    {
        public IEnumerable<TestViewModel> MapTests(TestsServiceModel testsModel)
        {
            var tests = testsModel.Tests.Select(x => new TestViewModel()
            {
                Id = x.Id,
                Url = x.Url,
                SaveTime = x.SaveTime
            });

            return tests;
        }

        public TestResultsViewModel MapTestResults(TestResultsServiceModel testResultsModel)
        {
            var url = testResultsModel.Url;

            var testResults = testResultsModel.Results
                .Select(x => new TestResultModel()
                {
                    Url = x.Url,
                    ResponseTime = x.ResponseTime,
                    InHtml = x.InHtml,
                    InSitemap = x.InSitemap
                });

            return new TestResultsViewModel()
            {
                Url = url,
                TestResults = testResults
            };
        }
    }
}
