using Crawler.Services;
using Crawler.WebApplication.Models;
using System.Linq;
using System.Collections.Generic;

namespace Crawler.WebApplication.Services
{
    public class DbMapper
    {
        private readonly DbHandler _dbHandler;

        public DbMapper(DbHandler dbHandler)
        {
            _dbHandler = dbHandler;
        }

        public IEnumerable<TestViewModel> GetTests()
        {
            var tests = _dbHandler.GetAllTests()
                .Select(x => new TestViewModel() 
                { 
                    Id = x.Id, 
                    Url = x.Url, 
                    SaveTime = x.SaveTime 
                });

            return tests;
        }

        public TestResultsViewModel GetTestResults(int id)
        {
            var url = _dbHandler.GetUrlByTestId(id);

            var testResults = _dbHandler.GetTestResultsByTestId(id)
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
