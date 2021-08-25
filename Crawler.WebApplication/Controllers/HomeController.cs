using Crawler.WebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using Crawler.WebApplication.Services;
using System.Threading.Tasks;
using Crawler.Services;
using System.Linq;

namespace Crawler.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly CrawlerService _crawlerService;
        private readonly DbMapper _dbMapper;

        private const int countRowsOnPage = 10;

        public HomeController(CrawlerService crawlerService, DbMapper dbMapper)
        {
            _crawlerService = crawlerService;
            _dbMapper = dbMapper;
        }

        [HttpGet]
        public IActionResult Index(int numberPage = 1)
        {
            ViewData["NumberPage"] = numberPage;

            ViewData["AllTests"] = _dbMapper.GetTests();

            int lastIndex = numberPage * countRowsOnPage;

            var testsView = _dbMapper.GetTests().Skip(lastIndex - 10).Take(10);

            return View(testsView);
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserInputModel input, int numberPage)
        {
            try
            {
                await _crawlerService.InterractAsync(input.Url);
            }
            catch (ArgumentException err)
            {
                ModelState.AddModelError("Url", err.Message);
            }

            return Index(numberPage);
        }

        public IActionResult GetNextPage(int numberPage)
        {
            if (numberPage * countRowsOnPage <= _dbMapper.GetTests().Count())
            {
                numberPage++;
            }

            return RedirectToAction("Index", new { numberPage = numberPage });
        }

        public IActionResult GetPreviousPage(int numberPage)
        {
            if (numberPage - 1 > 0)
            {
                numberPage--;
            }

            return RedirectToAction("Index", new { numberPage = numberPage});
        }
    }
}
