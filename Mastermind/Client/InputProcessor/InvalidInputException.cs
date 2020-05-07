using System;

namespace Mastermind.Client.InputProcessor
{
    public class InvalidInputException : Exception
    {
        public InvalidInputException(string message) : base(message)
        {
            
        }
    }
}