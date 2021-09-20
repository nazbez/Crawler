using Crawler.Services;
using Crawler.WebApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace Crawler.WebApplication.Controllers
{
    public class ResultController : Controller
    {
        private readonly Mapper _dbMapper;
        private readonly TestService _testService;

        public ResultController(Mapper dbMapper, TestService testService)
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
