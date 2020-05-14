using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Mastermind.DataAccess;
using Mastermind.DataAccess.Enums;
using Mastermind.DataAccess.MastermindConfigurationBuilder;

namespace Mastermind.Client.InputProcessor
{
    public class ConsoleInputProcessor : IInputProcessor
    {
        private readonly MastermindConfig _mastermindConfig;

        public ConsoleInputProcessor(MastermindConfig mastermindConfig)  
        {
            _mastermindConfig = mastermindConfig;
        }
        public List<Peg> Process(string input)
        {
            var colourMatches = Regex.Matches(input, "[a-zA-z]+");

            var colouredPegs = new List<Peg>();
            
            foreach (Match match in colourMatches)
            {
                foreach (Capture capture in match.Captures)
                {
                    var peg = ConvertToPeg(capture.Value);
                    colouredPegs.Add(peg);
                }
            }

            if (colouredPegs.Count != _mastermindConfig[DataConstants.CodeLength])
            {
                ThrowInvalidInputExceptionForInvalidListLengths();
            }
            
            return colouredPegs; 
            
        }

        private Peg ConvertToPeg(string stringPeg) 
        {
            if (!Enum.TryParse(stringPeg, true, out Peg peg))
            {
                ThrowInvalidInputExceptionForInvalidColours(stringPeg);
            }

            var maxPegFlag = _mastermindConfig[DataConstants.NumberOfColours] - 1;

            if ((int) peg > maxPegFlag)
            {
                ThrowInvalidInputExceptionForInvalidColours(stringPeg);
            }

            return peg;
        }
        
        
        private void ThrowInvalidInputExceptionForInvalidListLengths()
        {
            throw new InvalidInputException($"Error: you must pass in {_mastermindConfig[DataConstants.CodeLength]} colours!");
        }

        private void ThrowInvalidInputExceptionForInvalidColours(string stringPeg)
        {
            throw new InvalidInputException($"Error: {stringPeg} is an invalid colour!"); 
        }
        
        
    }
}


