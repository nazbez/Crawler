using System.Collections.Generic;

namespace Crawler.Services.Models.ResponseModels
{
    public class TestResultsModel
    {
        public string Url { get; set; }
        public IEnumerable<ResultModel> Results { get; set; }
    }
}
