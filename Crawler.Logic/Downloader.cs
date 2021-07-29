using System.Net;

namespace Crawler.Logic
{
    public class Downloader
    {
        public virtual string Download(string url)
        {
            try
            {
                WebClient webClient = new WebClient();

                string download = webClient.DownloadString(url);

                return download;
            }
            catch (WebException)
            {
                return string.Empty;
            }
        }
    }
}
