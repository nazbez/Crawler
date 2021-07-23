using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;

namespace Crawler.Logic
{
    public class Timer
    {
        public Dictionary<string, double> CheckTimeResponse(IEnumerable<string> urls)
        {
			// Dictionary that stores the value of address and time
			Dictionary<string, double> result = new Dictionary<string, double> { };

			// The addresses in the loop are moved
			foreach (var url in urls)
			{
				try
				{
					// A request is being created
					HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

					// A timer is created to detect time
					Stopwatch timer = new Stopwatch();

					// We start the timer
					timer.Start();

					// We get a response
					HttpWebResponse response =  (HttpWebResponse)request.GetResponse();

					// We stop the timer
					timer.Stop();

					// We pass the time and add it to the dictionary,
					// together with the address
					TimeSpan timeTaken = timer.Elapsed;
					result.Add(url, timeTaken.TotalMilliseconds);
				}
				catch (Exception)
				{
					continue;
				}
			}

			return result;
		}
    }
}
