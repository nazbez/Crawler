using Crawler.DbModels;
using System.Collections.Generic;

namespace Crawler.Services.Models.ResponseModels
{
    public class TestsServiceModel
    {
        public IEnumerable<Test> Tests { get; set; }
        public PageInfoModel PageInfo { get; set; }
    }
}
