namespace Crawler.Models
{
    public class TestResult
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool? IsInHtml { get; set; }
        public bool? IsInSitemap { get; set; }
        public int? ResponseTime { get; set; }
        public int TestId { get; set; }
        public Test Test { get; set; }
    }
}
