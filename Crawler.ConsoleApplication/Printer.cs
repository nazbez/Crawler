using ConsoleTables;
using Crawler.Logic.Models;
using System.Collections.Generic;
using System;


namespace Crawler.ConsoleApplication
{
    public class Printer
    {
        public virtual void PrintUniqueLinks(IEnumerable<string> result, string typeOfCrawler)
        {
            Console.WriteLine($"\nLinks found only by {typeOfCrawler} crawler\n");

            ConsoleTable table = new ConsoleTable("Number", "Url");

            int count = 0;

            foreach(var item in result)
            {
                table.AddRow($"{++count}", $"{item}");
            }

            if (count == 0)
            {
                table.AddRow("None", "None");
            }

            table.Write(Format.Alternative);
        }

        public virtual void PrintTimeOfResponse(IEnumerable<TimeOfResponseResult> result)
        {
            Console.WriteLine("\nAll links with their time of response\n");

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

        public virtual void PrintCountOfLinks(int countOfHtml, int countOfSitemap)
        {
            Console.WriteLine($"\nCount of links founded by html crawler: {countOfHtml}");
            Console.WriteLine($"\nCount of links founded by sitemap crawler: {countOfSitemap}");
        }
    }
}
