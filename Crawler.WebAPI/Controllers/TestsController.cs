using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Crawler.Services;
using Crawler.Services.Models.RequestModels;
using Crawler.Services.Models.ResponseModels;
using System;

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
                return BadRequest("Input is null");
            }

            try
            {
                return await _testService.CreateTestAsync(userInput);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }

        }

        [HttpGet]
        public ActionResult<TestsPageModel> GetTests([FromQuery] PageParameters pageParameters)
        {
            var tests = _testService.GetTests(pageParameters.PageNumber, pageParameters.PageSize);

            return tests;
        }

        [HttpGet("{id}")]
        public ActionResult<TestResultsModel> GetTestResults(int id)
        {
            return _testService.GetTestResults(id);
        }
    }
}
