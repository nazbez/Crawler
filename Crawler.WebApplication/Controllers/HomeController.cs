using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Crawler.Services;
using Crawler.WebApplication.Models;
using Crawler.WebApplication.Services;


namespace Crawler.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly TestService _testService;
        private readonly Mapper _mapper;

        public HomeController(TestService testService, Mapper mapper)
        {
            _testService = testService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index(int pageNumber = 1)
        {
            var test = _testService.GetTests(pageNumber);

            ViewData["PageNumber"] = test.PageInfo.PageNumber;
            ViewData["PageSize"] = test.PageInfo.PageSize;
            ViewData["AllPages"] = test.PageInfo.TotalPages;

            return View(_mapper.MapTests(test));
        }
        
        [HttpPost]
        public async Task<IActionResult> Index(UserInputModel input)
        {
            try
            {
                await _testService.CreateTestAsync(input.Url);

            }
            catch (System.Exception err)
            {
                ModelState.AddModelError("Url", err.Message);
            }

            return Index();
        }
    }
}
