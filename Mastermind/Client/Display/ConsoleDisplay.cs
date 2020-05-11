using System;
using System.Collections.Generic;
using System.Linq;

namespace Mastermind.Client.Display
{
    public class ConsoleDisplay : IDisplay
    {
        public void Display(string message)
        {
            Console.WriteLine(message);
        }

        public void Display<T>(List<T> list) 
        {
            var stringList = string.Join(", ", list.Select(x => x.ToString()));

            Console.WriteLine(stringList);
        }
    }
}