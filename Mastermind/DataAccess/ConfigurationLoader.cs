using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Mastermind.Business.Code;
using Microsoft.Extensions.Configuration;

namespace Mastermind.DataAccess
{
    public static class ConfigurationLoader  //TODO: why should this be static, also should validation class be static???  now they are tightly coupled . . . . 
    {
        public static MastermindConfiguration LoadMastermindConfiguration(string jsonFile)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config"))
                .AddJsonFile(jsonFile)
                .Build();

            ConfigValidator.Validate(config);
            
            var codeLength = int.Parse(config[Constants.CodeLength]);
            
            var numberOfColours = int.Parse(config[Constants.NumberOfColours]);

            return new MastermindConfiguration(codeLength, numberOfColours);
        }

    }
}