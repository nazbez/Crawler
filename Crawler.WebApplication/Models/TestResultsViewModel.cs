using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crawler.WebApplication.Models
{
    public class TestResultsViewModel
    {
        public string Url { get; set; }
        public IEnumerable<TestResultModel> TestResults { get; set; }
    }
}
