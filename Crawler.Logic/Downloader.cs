using System.Text;
using System.Net;
using System;

namespace Crawler.Logic
{
    public class Downloader
    {
        private readonly string _url;

        public Downloader(string url)
        {
            _url = url;
        }

        public string Download()
        {
            try
            {
                WebClient webClient = new WebClient();

                byte[] data = webClient.DownloadData(_url);

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
