using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Crawler.WebApplication.Models;
using Crawler.Logic;

namespace Crawler.WebApplication.Controllers
{
    public class ResultController : Controller
    {
        private readonly DbHandler _dbHandler;
        public ResultController(DbHandler dbHandler)
        {
            _dbHandler = dbHandler;    
        }
        public IActionResult Index(int id)
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

            return View(new TestResultsViewModel()
            {
                Url = url,
                TestResults = testResults,
                OnlyInHtml = onlyInHtml,
                OnlyInSitemap = onlyInSitemap
            });
        }
    }
}
