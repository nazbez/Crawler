using ConsoleTables;
using Crawler.Logic;
using System.Collections.Generic;

namespace Crawler.ConsoleApplication
{
    public class Printer
    {
        public void PrintDifference(IEnumerable<CrawlingResult> result)
        {
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

        public void PrintTimeOfResponse(IEnumerable<CrawlingResult> result)
        {
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
    }
}
