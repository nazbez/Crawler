using Crawler.WebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using Crawler.WebApplication.Services;
using System.Threading.Tasks;

namespace Crawler.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly CrawlerService _crawlerService;
        private readonly DbMapper _dbService;

        public HomeController(CrawlerService crawlerService, DbMapper dbService)
        {
            _crawlerService = crawlerService;
            _dbService = dbService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var testsView = _dbService.GetTests();

            return View(testsView);
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserInputModel input)
        {
            try
            {
                await _crawlerService.InterractAsync(input.Url);
            }
            catch (ArgumentException err)
            {
                ModelState.AddModelError("Url", err.Message);
            }

            return Index();
        }
    }
}
