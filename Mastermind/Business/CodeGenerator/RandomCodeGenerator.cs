using System;
using System.Collections.Generic;
using Mastermind.Business.Code;
using Mastermind.DataAccess;
using Mastermind.DataAccess.Enums;
using Mastermind.DataAccess.MastermindConfigurationBuilder;

namespace Mastermind.Business.CodeGenerator
{
    public class RandomCodeGenerator : ICodeGenerator
    {
        private readonly MastermindConfig _config;

        public RandomCodeGenerator(MastermindConfig mastermindConfiguration)
        {
            _config = mastermindConfiguration;
        }
        public List<Peg> GenerateSecretCode()
        {
            var secretCode = new List<Peg>();
            
            var random = new Random();
            
            for (var i = 0; i < _config[DataConstants.CodeLength]; i++)   
            {
                var randomFlag = random.Next(0, _config[DataConstants.NumberOfColours]);
                secretCode.Add((Peg)randomFlag);
            }

            return secretCode;
        }
        
    }
}