using System;
using System.Linq;
using Mastermind.Business.Code;
using Microsoft.Extensions.Configuration;

namespace Mastermind.DataAccess
{
    public static class ConfigValidator
    {
        
        public static void Validate(IConfiguration config)
        {
            ValidateCodeLength(config[Constants.CodeLength]); //TODO: is it best just to pass in the whole object
 
            ValidateNumberOfColours(config[Constants.NumberOfColours]);
        }

        private static void ValidateCodeLength(string codeLength) //TODO: refactor this 
        {
            if (!int.TryParse(codeLength, out var result))
            {
                throw new InvalidMastermindConfigurationException(
                    $"Not a valid {Constants.CodeLength}: {codeLength}. Must be an int between 3 - 6");
            }
            
            if (result < 3 || result > 6)
            {
                throw new InvalidMastermindConfigurationException(
                    $"Not a valid {Constants.CodeLength}: {codeLength}. Must be an int between 3 - 6"); 
            }
        }

        private static void ValidateNumberOfColours(string numberOfColours)
        {

            if (!int.TryParse(numberOfColours, out var result))
            {
                throw new InvalidMastermindConfigurationException(
                    $"Not a valid {Constants.NumberOfColours}: {numberOfColours}. Must be an int.");
            }

            var maxNumOfColours = result;

            var maxNumOfColoursDefinedByEnum = Enum.GetValues(typeof(Peg)).Cast<int>().Max() + 1;

            if (maxNumOfColours > maxNumOfColoursDefinedByEnum)
            {
                throw new InvalidMastermindConfigurationException(
                    $"Not a valid {Constants.NumberOfColours}: {numberOfColours}. " +
                    $"Must be {maxNumOfColoursDefinedByEnum} or less.");
            }

        }

    }
}