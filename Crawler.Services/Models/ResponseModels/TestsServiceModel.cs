using System.Collections.Generic;
using Crawler.DbModels;

namespace Crawler.Services.Models.ResponseModels
{
    public class TestsServiceModel
    {
        public IEnumerable<Test> Tests { get; set; }
        public PageInfoModel PageInfo { get; set; }
    }
}
