using System;
using System.Collections.Generic;
using Mastermind.Business.Code;
using Mastermind.DataAccess;

namespace Mastermind.Business.CodeGenerator
{
    public class RandomCodeGenerator : ICodeGenerator
    {
        private readonly MastermindConfiguration _config;

        public RandomCodeGenerator(MastermindConfiguration mastermindConfiguration)
        {
            _config = mastermindConfiguration;
        }
        public List<Peg> GenerateSecretCode()
        {
            var secretCode = new List<Peg>();
            
            var random = new Random();
            
            for (var i = 0; i < _config.CodeLength; i++)   
            {
                var randomFlag = random.Next(0, _config.NumberOfColours);
                secretCode.Add((Peg)randomFlag);
            }

            return secretCode;
        }
        
    }
}