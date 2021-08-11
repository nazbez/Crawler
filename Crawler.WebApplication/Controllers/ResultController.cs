using Microsoft.AspNetCore.Mvc;
using Crawler.Logic;
using Crawler.WebApplication.Models;
using System.Linq;

namespace Crawler.WebApplication.Controllers
{
    public class ResultController : Controller
    {
        private readonly DbHandler _dbHandler;

        public ResultController(DbHandler dbHandler)
        {
            _dbHandler = dbHandler;
        }

        [HttpGet]
        public IActionResult Index(int id)
        {
            var testUrl = _dbHandler.GetUrlByTestId(id);

            var testResults = _dbHandler.GetTestResultsByTestId(id)
                .Select(x => new TimeResponseResultsModel() 
                { 
                    Url = x.Url, 
                    ResponseTime = x.ResponseTime
                })
                .ToList();

            var urlsFoundOnlyInSitemap = _dbHandler.GetUrlsFoundOnlyInSitemapByTestId(id);

            var urlsFoundOnlyInHtml = _dbHandler.GetUrlsFoundOnlyInHtmlByTestId(id);

            return View(new TestResultsModel() 
            { 
                Url = testUrl, 
                TestResults = testResults, 
                UrlsFoundOnlyInSitemap = urlsFoundOnlyInSitemap, 
                UrlsFoundOnlyInHtml= urlsFoundOnlyInHtml 
            });
        }
    }
}

