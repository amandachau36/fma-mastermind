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

            var colours = new List<Peg>();
            
            foreach (Match match in colourMatches)
            {
                foreach (Capture capture in match.Captures)
                {
                    var peg = ConvertToPeg(capture.Value);
                    
                    colours.Add(peg);
                }
            }
            
            ThrowInvalidInputExceptionForInvalidListLengths(colours.Count);
            
            return colours; 
            
        }

        private Peg ConvertToPeg(string stringPeg) 
        {
            if (!Enum.TryParse(stringPeg, true, out Peg peg))
            {
                throw new InvalidInputException($"Error: {stringPeg} is not an invalid colour!");
            }

            var maxPegFlag = _mastermindConfig[DataConstants.NumberOfColours] - 1;

            if ((int) peg > maxPegFlag)
            {
                throw new InvalidInputException($"Error: {stringPeg} is not an invalid colour!");
            }

            return peg;
        }
        
        
        private void ThrowInvalidInputExceptionForInvalidListLengths(int lengthOfList)
        {
            if (lengthOfList != _mastermindConfig[DataConstants.CodeLength])
            {
                throw new InvalidInputException($"Error: you must pass in {_mastermindConfig[DataConstants.CodeLength]} colours!");  
            }

        }
        
        
    }
}


