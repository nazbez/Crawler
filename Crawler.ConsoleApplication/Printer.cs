using ConsoleTables;
using Crawler.Logic.Models;
using System;
using System.Collections.Generic;


namespace Crawler.ConsoleApplication
{
    public class Printer
    {
        public virtual void PrintHtmlLinks(IEnumerable<string> result)
        {
            Console.WriteLine($"\nLinks found only by html crawler\n");

            PrintLinks(result);
        }

        public virtual void PrintSitemapLinks(IEnumerable<string> result)
        {
            Console.WriteLine($"\nLinks found only by sitemap crawler\n");

            PrintLinks(result);
        }



        private void PrintLinks(IEnumerable<string> result)
        {
            ConsoleTable table = new ConsoleTable("Number", "Url");

            int count = 1;

            foreach(var item in result)
            {
                table.AddRow($"{count++}", $"{item}");
            }

            if (count == 1)
            {
                table.AddRow("None", "None");
            }

            table.Write(Format.Alternative);
        }

        public virtual void PrintTimeOfResponse(IEnumerable<TimeOfResponseResult> result)
        {
            Console.WriteLine("\nAll links with their time of response\n");

            ConsoleTable table = new("Number", "Url", "Time");

            int count = 1;

            foreach (var item in result)
            {
                var isErrorInResponse = item.Time == -1;

                if (isErrorInResponse)
                {
                    table.AddRow($"{count++}", $"{item.Url}", $"{item.ErrorMsg}");

                    continue;
                }

                table.AddRow($"{count++}", $"{item.Url}", $"{item.Time}");
            }

            var isTableEmpty = count == 1;

            if (isTableEmpty)
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
