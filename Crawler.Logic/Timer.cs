using System;
using System.Diagnostics;
using System.Net;

namespace Crawler.Logic
{
    public class Timer
    {
        public virtual double CheckTimeResponse(string url)
        {
			try
			{
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

				Stopwatch timer = new Stopwatch();

				timer.Start();

				HttpWebResponse response = (HttpWebResponse)request.GetResponse();

				timer.Stop();

				TimeSpan timeTaken = timer.Elapsed;

				return timeTaken.TotalMilliseconds;
				
			}
			catch (Exception)
			{
				return 0;
			}
		}
    }
}
