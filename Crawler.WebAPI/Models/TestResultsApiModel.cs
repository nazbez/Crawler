using System.Collections.Generic;

namespace Crawler.WebAPI.Models
{
    public class TestResultsApiModel
    {
        public string Url { get; set; }
        public IEnumerable<TestResultsModel> Results { get; set; }
    }
}
