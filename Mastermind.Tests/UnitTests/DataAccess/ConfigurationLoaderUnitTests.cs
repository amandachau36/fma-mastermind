using System;
using System.IO;
using Mastermind.DataAccess;
using Xunit;


namespace Mastermind.Tests.UnitTests.DataAccess
{
    public class ConfigurationLoaderUnitTests
    {
        [Fact]
         public void It_Should_Return_AConfigurationObject_When_GivenAConfigFile()  //TODO: Component test? separate folder??   
         {
             var config = ConfigurationLoader.LoadMastermindConfiguration(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ConfigFiles", "StandardConfig.json"));
             
             //assert
             Assert.Equal(4, config[Constants.CodeLength]);
             Assert.Equal(6, config[Constants.NumberOfColours]);
             Assert.Equal(8, config[Constants.NumberOfTurns]);
         }

    }
}