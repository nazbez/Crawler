using System;
using System.Diagnostics;
using System.Net;

namespace Crawler.Logic
{
    public class Timer
    {
        public virtual int CheckTimeResponse(string url)
        {
			try
			{
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

				Stopwatch timer = new Stopwatch();

				timer.Start();

				var response = request.GetResponse();

				timer.Stop();

				return (int)timer.ElapsedMilliseconds;			
			}
			catch (WebException)
			{
				return -1;
			}
            catch (UriFormatException)
            {
				return -1;
            }
		}
    }
}
