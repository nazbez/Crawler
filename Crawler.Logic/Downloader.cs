using System.Text;
using System.Net;
using System;

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

                return download;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
