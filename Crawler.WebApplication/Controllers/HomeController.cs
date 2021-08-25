using Crawler.WebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using Crawler.WebApplication.Services;
using System.Threading.Tasks;
using Crawler.Services;

namespace Crawler.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly CrawlerService _crawlerService;
        private readonly DbMapper _dbMapper;

        public HomeController(CrawlerService crawlerService, DbMapper dbMapper)
        {
            _crawlerService = crawlerService;
            _dbMapper = dbMapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var testsView = _dbMapper.GetTests();

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
