using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Crawler.Services;
using Crawler.WebAPI.Models;
using Crawler.WebAPI.Services;

namespace Crawler.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly TestService _testService;
        private readonly Mapper _mapper;
        public TestsController(TestService testService, Mapper mapper)
        {
            _testService = testService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a collection of tests filtered by pagination
        /// </summary>
        /// <param name="pageParameters">Values of page size and page number</param>
        /// <returns>Collection of tests and info about the page</returns>
        [HttpGet]
        public ActionResult<ResponseModel> GetTests([FromQuery] PageParameters pageParameters)
        {
            var tests = _testService.GetTests(pageParameters.PageNumber, pageParameters.PageSize);

            return new ResponseModel { Object = _mapper.MapTests(tests) };
        }

        /// <summary>
        /// Get test results by test id
        /// </summary>
        /// <param name="id">Item id which results needed</param>
        /// <returns>Collection with results and tested url</returns>
        [HttpGet("{id}")]
        public ActionResult<ResponseModel> GetTestResults(int id)
        {
            return new ResponseModel { Object = _mapper.MapTestResults(_testService.GetTestResults(id)) };
        }

        /// <summary>
        /// Create a new test
        /// </summary>
        /// <param name="userInput">Url input</param>
        /// <returns>The created test id</returns>
        [HttpPost]
        public async Task<ActionResult<ResponseModel>> PostTest(UserInputModel userInput)
        {
            if (userInput == null)
            {
                return BadRequest();
            }

            return new ResponseModel { Object = await _testService.CreateTestAsync(userInput.Url) };
        }
    }
}
