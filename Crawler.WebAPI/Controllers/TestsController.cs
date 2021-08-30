using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crawler.Services;
using Crawler.Services.Models.RequestModels;
using Crawler.Services.Models.ResponseModels;

namespace Crawler.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly TestService _testService;
        public TestsController(TestService testService)
        {
            _testService = testService;
        }

        [HttpPost]
        public async Task<ActionResult<int>> PostTest(UserInputModel userInput)
        {
            if (userInput == null)
            {
                return BadRequest();
            }

            return await _testService.CreateTestAsync(userInput);
        }

        [HttpGet]
        public ActionResult<IEnumerable<TestModel>> GetTests()
        {
            var tests = _testService.GetTests();

            return tests.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<TestResultsModel> GetTests(int id)
        {
            return _testService.GetTestResults(id);
        }
    }
}
