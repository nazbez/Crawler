using System.Collections.Generic;
using System.Linq;
using Crawler.Logic.Models;

namespace Crawler.Logic
{
    public class CrawlerLinksHandler
    {
        private readonly Timer _timer;
        public CrawlerLinksHandler(Timer timer)
        {
            _timer = timer;
        }

        public virtual IEnumerable<TimeOfResponseResult> GetResultOfCrawling(IEnumerable<string> links)
        {
            List<TimeOfResponseResult> result = new List<TimeOfResponseResult>() { };

            foreach(var url in links)
            {
                int time = _timer.CheckTimeResponse(url);

                result.Add(new TimeOfResponseResult() 
                { 
                    Url = url, 
                    Time = time, 
                });
            }

            return result.OrderBy(x => x.Time);
        }
    }
}
