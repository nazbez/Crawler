using System;
using System.Net;

namespace Crawler.Logic
{
    public class Validator
    {

        public virtual string IsValid(string url)
        {
            Uri uriResult;

            bool result = Uri.TryCreate(url, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (!result)
            {
                return "Invalid input!";
            }

            try
            {
                WebClient wc = new WebClient();
                string document = wc.DownloadString(url);
                return "";
            }
            catch (WebException err)
            {
                return err.Message;
            }
        }
    }
}
