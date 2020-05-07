using System;
using System.Collections.Generic;
using System.IO;
using Mastermind.DataAccess;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Mastermind.Tests.UnitTests.DataAccess
{
    public class ConfigValidatorUnitTests
    {
        [Theory]
        [MemberData(nameof(GetConfiguration))]
        public void It_Should_ThrowInvalidConfigurationError_WhenGivenInvalidConfiguration(string path, string errorMessage)
        {

            // TODO: put in here instead - instead in config file/easier to read
            // var config1 = new MastermindConfig( 2, 3, 4) 
              
            Action actual = () => ConfigurationLoader.LoadMastermindConfiguration(path);  //TODO: should I make the process methods public so I can test them without the path? 
            
            //assert
            var exception = Assert.Throws<InvalidMastermindConfigurationException>(actual);
            Assert.Equal(errorMessage, exception.Message);

        }
        
        public static IEnumerable<object[]> GetConfiguration()
        {
            yield return new object[]
            {
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ConfigFiles", "InvalidConfig1.json"),
                $"Not a valid {Constants.CodeLength}: four. Must be an int.",
            };

            yield return new object[]
            {
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ConfigFiles", "InvalidConfig2.json"),
                $"Not a valid {Constants.CodeLength}: 1. Must be between 3 - 6",
            };
        
            yield return new object[]
            {
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ConfigFiles", "InvalidConfig3.json"),
                $"Not a valid {Constants.NumberOfColours}: six. Must be an int.",
            };
            
            yield return new object[]
            {
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ConfigFiles", "InvalidConfig4.json"),
                $"Not a valid {Constants.NumberOfColours}: 9. Must be between 2 - 8",
            };
            
            yield return new object[]
            {
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ConfigFiles", "InvalidConfig5.json"),
                $"Not a valid {Constants.NumberOfTurns}: ten. Must be an int.",
            };
            
            yield return new object[]
            {
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ConfigFiles", "InvalidConfig6.json"),
                $"Not a valid {Constants.NumberOfTurns}: 1000. Must be between 3 - 20",
            };
            
        }

    }
}