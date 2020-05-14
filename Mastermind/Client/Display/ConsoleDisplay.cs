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

        public void DisplayError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            
            Console.WriteLine(message);
            
            Console.ResetColor();
        }

        public void DisplayResult(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            
            Console.WriteLine(message);
            
            Console.ResetColor();
        }

        public void Display<T>(List<T> list) 
        {
            var stringList = string.Join(", ", list.Select(x => x.ToString()));

            Console.WriteLine(stringList);
        }
    }
}