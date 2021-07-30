using ConsoleTables;
using Crawler.Logic;
using System.Collections.Generic;

namespace Crawler.ConsoleApplication
{
    public class Printer
    {
        private readonly ConsoleImitator _console;
        public Printer()
        {
            _console = new ConsoleImitator();
        }
        public virtual void PrintDifference(IEnumerable<CrawlingResult> result, string typeOfCrawler)
        {
            _console.WriteLine($"\nLinks found only by {typeOfCrawler} crawler\n");

            ConsoleTable table = new ConsoleTable("Number", "Url");

            int count = 0;

            foreach(var item in result)
            {
                table.AddRow($"{++count}", $"{item.Url}");
            }

            if (count == 0)
            {
                table.AddRow("None", "None");
            }
            table.Write(Format.Alternative);
        }

        public virtual void PrintTimeOfResponse(IEnumerable<CrawlingResult> result)
        {
            _console.WriteLine("\nAll links with their time of response\n");

            ConsoleTable table = new ConsoleTable("Number", "Url", "Time");

            int count = 0;

            foreach (var item in result)
            {
                table.AddRow($"{++count}", $"{item.Url}", $"{item.Time}");
            }

            if (count == 0)
            {
                table.AddRow("None", "None", "None");
            }

            table.Write(Format.Alternative);
        }

        public virtual void PrintCountOfLinks(List<CrawlingResult> result, string typeOfCrawler)
        {
            _console.WriteLine($"\nCount of links founded by {typeOfCrawler} crawler: {result.Count}");
        }
    }
}
