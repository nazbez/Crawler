
namespace Crawler.WebApplication.Models
{
    public class TestResultModel
    {
        public string Url { get; set; }
        public int ResponseTime { get; set; }
        public bool InHtml { get; set; }
        public bool InSitemap { get; set; }
    }
}
