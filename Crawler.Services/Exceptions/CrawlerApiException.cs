using System;

namespace Crawler.Services.Exceptions
{
    public class CrawlerApiException : Exception
    {
        public CrawlerApiException(string message) 
            : base(message)
        {

        }
    }
}
