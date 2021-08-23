using Crawler.WebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using Crawler.WebApplication.Services;

namespace Crawler.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly CrawlerService _crawlerService;
        private readonly DbService _dbService;

        public HomeController(CrawlerService crawlerService, DbService dbService)
        {
            _crawlerService = crawlerService;
            _dbService = dbService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var tests = _dbHandler.GetAllTests().Select(x => new TestModel() { Id = x.Id, Url = x.Url, SaveTime = (DateTime)x.SaveTime });

            return View(new TestsViewModel() { Tests = tests});
        }

        [HttpPost]
        public IActionResult Index(UserInputModel input)
        {
            try
            {
                _crawlerService.Interract(input.Url);
            }
            catch (ArgumentException err)
            {
                ModelState.AddModelError("Url", err.Message);
            }

            return Index();
        }
    }
}
