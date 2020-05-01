using System.Collections.Generic;
using Mastermind.Business.Code;
using Mastermind.DataAccess;
using Mastermind.UnitTests.Business;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Mastermind.UnitTests.DataAccess
{
    public class ConfigurationLoaderUnitTests
    {
        [Fact]
        public void It_Should_Return_AConfigurationObject_When_GivenAConfigFile()  //TODO: Integration Test 
        {
            var config = ConfigurationLoader.LoadMastermindConfiguration("standardConfig.json");
            
            //assert
            Assert.Equal(4, config.CodeLength);
            Assert.Equal(6, config.NumberOfColours);
        }

    }
}