using System;
using System.Collections.Generic;

namespace Mastermind.Business
{
    public class RandomCodeGenerator : ICodeGenerator
    {
        
        public List<Peg> GenerateSecretCode()
        {
            var secretCode = new List<Peg>();
            
            var random = new Random();
            
            for (var i = 0; i < 4; i++)
            {
                var randomFlag = random.Next(0, 6);
                secretCode.Add((Peg)randomFlag);
            }

            return secretCode;
        }
        
    //TODO: config file 
    }
}