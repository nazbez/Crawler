using Microsoft.AspNetCore.Mvc;
using Crawler.WebApplication.Services;
using Crawler.Services;

namespace Crawler.WebApplication.Controllers
{
    public class ResultController : Controller
    {
        private readonly DbMapper _dbMapper;
        private readonly TestService _testService;

        public ResultController(DbMapper dbMapper, TestService testService)
        {
            _dbMapper = dbMapper;
            _testService = testService;
        }

        public IActionResult Index(int id)
        {
            var testResults = _testService.GetTestResults(id);

            return View(_dbMapper.MapTestResults(testResults));
        }
    }
}
