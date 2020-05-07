using System;

namespace Mastermind.Client.InputCollector
{
    public class ConsoleInputCollector : ICollector
    {
        public string Collect()
        {
            return Console.ReadLine();
        }
    }
}