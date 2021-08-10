using System.Collections.Generic;
using Crawler.DbModels;

namespace Crawler.WebApplication.Models
{
    public class TestResultsModel
    {
        public string Url { get; set; }
        public IEnumerable<TestResult> TestResults { get; set; }
        public IEnumerable<string> UrlsFoundOnlyInSitemap { get; set; }
        public IEnumerable<string> UrlsFoundOnlyInHtml { get; set; }
    }
}
