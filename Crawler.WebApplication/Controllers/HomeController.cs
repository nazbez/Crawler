using Crawler.WebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using Crawler.Logic;
using Crawler.WebApplication.Services;
using System.Linq;

namespace Crawler.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly DbHandler _dbHandler;
        private readonly CrawlerService _crawlerService;


        public HomeController(DbHandler dbHandler, CrawlerService crawlerService)
        {
            _dbHandler = dbHandler;
            _crawlerService = crawlerService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new TestsModel() { Tests = _dbHandler.GetAllTests()
                .Select(x => new ResultModel() 
                { 
                    Id = x.Id, 
                    Url = x.Url, 
                    SaveTime = x.SaveTime 
                })
            });
        }

        [HttpPost]
        public IActionResult Index(UserInputModel input)
        {
            try
            {
                _crawlerService.Crawl(input.Url);
            }
            catch (Exception err)
            {
                ModelState.AddModelError("Url", err.Message);
            }

            return View(new TestsModel() { Tests = _dbHandler.GetAllTests()
                .Select(x => new ResultModel()
                {
                    Id = x.Id,
                    Url = x.Url,
                    SaveTime = x.SaveTime
                })
            });
        }
    }
}
