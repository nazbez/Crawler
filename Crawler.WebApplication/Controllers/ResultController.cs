using Microsoft.AspNetCore.Mvc;
using Crawler.WebApplication.Services;

namespace Crawler.WebApplication.Controllers
{
    public class ResultController : Controller
    {
        private readonly DbMapper _dbService;

        public ResultController(DbMapper dbService)
        {
            _dbService = dbService;    
        }

        public IActionResult Index(int id)
        {
            var testResultsView = _dbService.GetTestResults(id);

            return View(testResultsView);
        }
    }
}
