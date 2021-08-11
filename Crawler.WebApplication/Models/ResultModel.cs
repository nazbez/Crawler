using System;

namespace Crawler.WebApplication.Models
{
    public class ResultModel
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public DateTime? SaveTime { get; set; }
    }
}
