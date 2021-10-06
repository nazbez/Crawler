
namespace Crawler.WebAPI.Models
{
    public class TestResultsModel
    {
        public string Url { get; set; }
        public int ResponseTime { get; set; }
        public bool InHtml { get; set; }
        public bool InSitemap { get; set; }
    }
}
