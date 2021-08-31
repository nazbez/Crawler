using System.Collections.Generic;


namespace Crawler.Services.Models.ResponseModels
{
    public class TestsPageModel
    {
        public IEnumerable<TestModel> Tests { get; set; }
        public PageInfoModel PageInfo { get; set; }
    }
}
