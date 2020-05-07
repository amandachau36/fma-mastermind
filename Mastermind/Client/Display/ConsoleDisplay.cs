using System;
using System.Collections.Generic;
using System.Linq;
using Mastermind.DataAccess.Enums;

namespace Mastermind.Client.Display
{
    public class ConsoleDisplay : IDisplay
    {
        public void Display(string message)
        {
            Console.WriteLine(message);
        }

        public void Display<T>(List<T> list) //TODO: is this an okay use of generics? 
        {
            var stringList = string.Join(", ", list.Select(x => x.ToString()));

            Console.WriteLine(stringList);

        }
    }
}