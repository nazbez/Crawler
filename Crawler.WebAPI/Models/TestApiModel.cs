using Crawler.Services.Models.ResponseModels;
using System.Collections.Generic;

namespace Crawler.WebAPI.Models
{
    public class TestApiModel
    {
        public IEnumerable<TestModel> Tests { get; set; }
        public PageInfoModel PageInfo { get; set; }
    }
}
