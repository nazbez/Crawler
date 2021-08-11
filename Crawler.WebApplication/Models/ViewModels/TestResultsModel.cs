using System.Collections.Generic;

namespace Crawler.WebApplication.Models
{
    public class TestResultsModel
    {
        public string Url { get; set; }
        public IEnumerable<TimeResponseResultsModel> TestResults { get; set; }
        public IEnumerable<string> UrlsFoundOnlyInSitemap { get; set; }
        public IEnumerable<string> UrlsFoundOnlyInHtml { get; set; }
    }
}
