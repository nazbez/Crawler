namespace Crawler.Logic
{
    public class Validator
    {
        public virtual bool IsValid(string url)
        {
            return url.StartsWith("https://") || url.StartsWith("http://");
        }
    }
}
