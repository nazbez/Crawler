namespace Crawler.Logic
{
    public class CrawlingResult
    {
        public string Url { get; set; }
        public double Time { get; set; }
        public bool IsInSitemap { get; set; }
        public bool IsInHtml { get; set; }

        public CrawlingResult(string url, double time, bool isInSitemap, bool isInHtml)
        {
            Url = url;
            Time = time;
            IsInSitemap = isInSitemap;
            IsInHtml = isInHtml;
        }
    }
}
