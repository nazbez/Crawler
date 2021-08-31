namespace Crawler.Services.Models.ResponseModels
{
    public class ResultModel
    {
        public string Url { get; set; }
        public int ResponseTime { get; set; }
        public bool InHtml { get; set; }
        public bool InSitemap { get; set; }
    }
}
