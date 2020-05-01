using System;
using System.Collections.Generic;
using System.IO;
using Mastermind.Business.Code;
using Mastermind.DataAccess;
using Mastermind.UnitTests.Business;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Mastermind.UnitTests.DataAccess
{
    public class ConfigValidatorUnitTests
    {
        [Theory]
        [MemberData(nameof(GetConfiguration))]
        public void It_Should_ThrowInvalidConfigurationError_WhenGivenInvalidConfiguration(IConfiguration configuration, string errorMessage)
        {

            Action actual = () => ConfigValidator.Validate(configuration);
            
            //assert
            var exception = Assert.Throws<InvalidMastermindConfigurationException>(actual);
            Assert.Equal(errorMessage, exception.Message);

        }

        private static IConfiguration GetConfig(string file)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DataAccess"))
                .AddJsonFile(file)
                .Build();

            return config;
        }

        public static IEnumerable<object[]> GetConfiguration()
        {
            yield return new object[]
            {
                GetConfig("InvalidConfig1.json"),
                $"Not a valid {Constants.CodeLength}: four. Must be an int between 3 - 6",
            };

            yield return new object[]
            {
                GetConfig("InvalidConfig2.json"),
                $"Not a valid {Constants.CodeLength}: 1. Must be an int between 3 - 6",
            };
        
            yield return new object[]
            {
                GetConfig("InvalidConfig3.json"),
                $"Not a valid {Constants.NumberOfColours}: six. Must be an int.",
            };
            
            yield return new object[]
            {
                GetConfig("InvalidConfig4.json"),
                $"Not a valid {Constants.NumberOfColours}: 9. Must be 8 or less.",
            };
            
        }

    }
}