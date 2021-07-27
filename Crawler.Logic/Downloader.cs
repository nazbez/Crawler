using System.Text;
using System.Net;
using System;
using System.Linq;

namespace Crawler.Logic
{
    public class Downloader
    {
        public virtual string Download(string url)
        {
            try
            {
                WebClient webClient = new WebClient();

                byte[] data = webClient.DownloadData(url);

                string download = Encoding.ASCII.GetString(data);

                return string.Join("" , download.SkipWhile(x=> x != '<'));
            }
            catch (WebException)
            {
                return string.Empty;
            }
        }
    }
}
