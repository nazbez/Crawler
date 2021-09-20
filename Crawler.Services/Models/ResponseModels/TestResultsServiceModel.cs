using Crawler.DbModels;
using System.Collections.Generic;

namespace Crawler.Services.Models.ResponseModels
{
    public class TestResultsServiceModel
    {
        public string Url { get; set; }
        public IEnumerable<TestResult> Results { get; set; }
    }
}
