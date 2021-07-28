using System;


namespace Crawler.Logic
{
    public class ConsoleImitator
    {
        public virtual string ReadLine()
        {
            return Console.ReadLine();
        }

        public virtual void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}
