using System;

namespace Mastermind.DataAccess
{
    public class InvalidMastermindConfigurationException : Exception
    {
        public InvalidMastermindConfigurationException(string message) : base(message)
        {
            
        }
    }
}