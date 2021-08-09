using System;
using System.Collections.Generic;

namespace Crawler.Models
{
    public class Test
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public DateTime? SaveTime { get; set; }
        public List<TestResult> TestResults { get; set; }
    }
}
