using System;
using System.Linq;
using Mastermind.DataAccess.Enums;
using Mastermind.DataAccess.MastermindConfigurationBuilder;
using Microsoft.Extensions.Configuration;

namespace Mastermind.DataAccess
{
    public static class ConfigurationLoader  
    {
        public static MastermindConfig LoadMastermindConfiguration(string path)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile(path)
                .Build();
            
            var codeLength = ProcessCodeLength(config[DataConstants.CodeLength]);
            var numberOfColours = ProcessNumberOfColours(config[DataConstants.NumberOfColours]);
            var numberOfTurns = ProcessNumberOfTurns(config[DataConstants.NumberOfTurns]);
            
            var configBuilder = new MastermindConfigBuilder(); 
            configBuilder.BuildCodeLength(codeLength); 
            configBuilder.BuildNumberOfColours(numberOfColours); 
            configBuilder.BuildNumberOfTurns(numberOfTurns);
            
            return configBuilder.MastermindConfig;
        }
        
        private static int ProcessCodeLength(string codeLength) 
        {
            var processedCodeLength = ConvertToInt(DataConstants.CodeLength, codeLength);
            
            ThrowExceptionIfValueIsInvalid(DataConstants.CodeLength, processedCodeLength, DataConstants.MinimumCodeLength, DataConstants.MaximumCodeLength);
            
            return processedCodeLength; 
        }

        private static int ProcessNumberOfColours(string numberOfColours)
        {
            var processedNumberOfColours = ConvertToInt(DataConstants.NumberOfColours, numberOfColours);

            var maxValueOfPeg = Enum.GetValues(typeof(Peg)).Cast<int>().Max();

            var maximumNumberOfColours = maxValueOfPeg + 1;

            ThrowExceptionIfValueIsInvalid(DataConstants.NumberOfColours, processedNumberOfColours, DataConstants.MinimumNumberOfColours, maximumNumberOfColours);

            return processedNumberOfColours;
        }


        private static int ProcessNumberOfTurns(string numberOfTurns)
        {
            var processedNumberOfTurns = ConvertToInt(DataConstants.NumberOfTurns, numberOfTurns);

            ThrowExceptionIfValueIsInvalid(DataConstants.NumberOfTurns, processedNumberOfTurns, DataConstants.MinimumNumberOfTurns, DataConstants.MaximumNumberOfTurns);
            
            return processedNumberOfTurns;
        }
        
        private static int ConvertToInt(string configKey, string configValue)
        {
            if (!int.TryParse(configValue, out var result))
            {
                throw new InvalidMastermindConfigurationException(
                    $"Not a valid {configKey}: {configValue}. Must be an int.");
            }

            return result;
        }

        private static void ThrowExceptionIfValueIsInvalid(string configKey, int processedValue, int minValue, int maxValue) 
        {
            if (processedValue < minValue || processedValue > maxValue)
            {
                throw new InvalidMastermindConfigurationException(
                    $"Not a valid {configKey}: {processedValue}. Must be between {minValue} - {maxValue}"); 
            }
        }
    }
}