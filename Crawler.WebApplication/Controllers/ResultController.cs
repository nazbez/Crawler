using Microsoft.AspNetCore.Mvc;
using Crawler.WebApplication.Services;

namespace Crawler.WebApplication.Controllers
{
    public class ResultController : Controller
    {
        private readonly DbMapper _dbMapper;

        public ResultController(DbMapper dbMapper)
        {
            _dbMapper = dbMapper;    
        }

        public IActionResult Index(int id)
        {
            var testResults = _dbMapper.GetTestResults(id);

            return View(testResults);
        }
    }
}
