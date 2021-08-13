using System.Collections.Generic;

namespace Crawler.WebApplication.Models
{
    public class TestResultsViewModel
    {
        public string Url { get; set; }
        public IEnumerable<TestResultModel> TestResults { get; set; }
        public IEnumerable<string> OnlyInSitemap { get; set; }
        public IEnumerable<string> OnlyInHtml { get; set; }
    }
}
