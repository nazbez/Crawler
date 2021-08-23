using Crawler.DbLogic;
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

        public IEnumerable<TestModel> GetTests()
        {
            var tests = _dbHandler.GetAllTests()
                .Select(x => new TestModel() 
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
                    ResponseTime = x.ResponseTime
                });

            var onlyInHtml = _dbHandler.GetUrlsFoundOnlyInHtmlByTestId(id);

            var onlyInSitemap = _dbHandler.GetUrlsFoundOnlyInSitemapByTestId(id);

            return new TestResultsViewModel()
            {
                Url = url,
                TestResults = testResults,
                OnlyInHtml = onlyInHtml,
                OnlyInSitemap = onlyInSitemap
            };
        }

    }
}
