namespace Crawler.Logic.Models
{
    public class CrawlingResult
    {
        public string Url { get; set; }
        public bool IsInHtml { get; set; }
        public bool IsInSitemap { get; set; }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(CrawlingResult))
            {
                var comparedObj = (CrawlingResult)obj;

                return comparedObj.Url == Url 
                    && comparedObj.IsInHtml == IsInHtml 
                    && comparedObj.IsInSitemap == IsInSitemap;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Url.GetHashCode();
        }
    }
}
