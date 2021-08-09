namespace Crawler.DbModels
{
    public class TestResult
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool? InHtml { get; set; }
        public bool? InSitemap { get; set; }
        public int? ResponseTime { get; set; }
        public int TestId { get; set; }
        public Test Test { get; set; }
    }
}
