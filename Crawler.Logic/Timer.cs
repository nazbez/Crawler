using Crawler.Logic.Models;
using System;
using System.Diagnostics;
using System.Net;

namespace Crawler.Logic
{
    public class Timer
    {
        public virtual TimeOfResponseResult CheckTimeResponse(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

				Stopwatch timer = new Stopwatch();

				timer.Start();

				var response = request.GetResponse();

				timer.Stop();

                return new TimeOfResponseResult()
                {
                    Url = url,
                    Time = (int)timer.ElapsedMilliseconds,
                    ErrorMsg = ""
                };
            }
			catch (WebException err)
			{
                return new TimeOfResponseResult()
                {
                    Url = url,
                    Time = -1,
                    ErrorMsg = err.Message
                };
            }
            catch (UriFormatException err)
            {
                return new TimeOfResponseResult()
                {
                    Url = url,
                    Time = -1,
                    ErrorMsg = err.Message
                };
            }
		}
    }
}
