using Crawler.DbLogic;
using Crawler.WebApplication.Models;
using System.Linq;

namespace Crawler.WebApplication.Services
{
    public class DbService
    {
        private readonly DbHandler _dbHandler;

        public DbService(DbHandler dbHandler)
        {
            _dbHandler = dbHandler;
        }

        public TestsViewModel GetTests()
        {
            var tests = _dbHandler.GetAllTests()
                .Select(x => new TestModel() 
                { 
                    Id = x.Id, 
                    Url = x.Url, 
                    SaveTime = x.SaveTime 
                });

            return new TestsViewModel { Tests = tests };
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
