using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Crawler.Services;
using Crawler.Services.Models.RequestModels;
using Crawler.Services.Models.ResponseModels;
using Crawler.Services.Exceptions;

//namespace Crawler.WebAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class TestsController : ControllerBase
//    {
//        private readonly TestService _testService;
//        public TestsController(TestService testService)
//        {
//            _testService = testService;
//        }

//        /// <summary>
//        /// Get a collection of tests filtered by pagination
//        /// </summary>
//        /// <param name="pageParameters">Values of page size and page number</param>
//        /// <returns>Collection of tests and info about the page</returns>
//        [HttpGet]
//        public ActionResult<TestsPageModel> GetTests([FromQuery] PageParameters pageParameters)
//        {
//            var tests = _testService.GetTests(pageParameters.PageNumber, pageParameters.PageSize);

//            return tests;
//        }

//        /// <summary>
//        /// Get test results by test id
//        /// </summary>
//        /// <param name="id">Item id which results needed</param>
//        /// <returns>Collection with results and tested url</returns>
//        [HttpGet("{id}")]
//        public ActionResult<TestResultsModel> GetTestResults(int id)
//        {
//            try
//            {
//                return _testService.GetTestResults(id);
//            }
//            catch (CrawlerApiException err)
//            {
//                return BadRequest(err.Message);
//            }
//        }

//        /// <summary>
//        /// Create a new test
//        /// </summary>
//        /// <param name="userInput">Url input</param>
//        /// <returns>The created test id</returns>
//        [HttpPost]
//        public async Task<ActionResult<int>> PostTest(UserInputModel userInput)
//        {
//            if (userInput == null)
//            {
//                return BadRequest("Input is null");
//            }

//            try
//            {
//                return await _testService.CreateTestAsync(userInput);
//            }
//            catch (CrawlerApiException err)
//            {
//                return BadRequest(err.Message);
//            }
//        }
//    }
//}
