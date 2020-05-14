using System.Collections.Generic;
using System.Threading;
using Mastermind.Client.Display;

namespace Mastermind.Client.InputCollector
{
    public class ConsoleDemoInputCollector : ICollector
    {
        private readonly List<string> _input;
        private readonly IDisplay _display;

        private int _inputIndex; 

        public ConsoleDemoInputCollector(List<string> input, IDisplay display)
        {
            _input = input;
            _display = display;
        }
        public string Collect()  
        {
            Thread.Sleep(1000);
            
            var i = _inputIndex;
            _display.Display(_input[i]);
            _inputIndex ++;
            
            Thread.Sleep(2000);
            
            return _input[i]; 
        }
    }
}