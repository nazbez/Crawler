namespace Crawler.Logic
{
    public class CrawlingResult
    {
        public string Url { get; set; }
        public int Time { get; set; }
        public bool IsInSitemap { get; set; }
        public bool IsInHtml { get; set; }
    }
}
