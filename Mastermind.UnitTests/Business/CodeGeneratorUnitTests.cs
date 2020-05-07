using System;
using Mastermind.Business.CodeGenerator;
using Mastermind.DataAccess;
using Mastermind.DataAccess.Enums;
using Mastermind.DataAccess.MastermindConfigurationBuilder;
using Xunit;

namespace Mastermind.UnitTests.Business
{
    public class CodeGeneratorUnitTests
    {
        [Fact]
        public void It_Should_Return_A_SecretCodeWithALengthOfFour()
        {
            //arrange
            var config = new MastermindConfig
            {
                [Constants.CodeLength] = 4, [Constants.NumberOfColours] = 6, [Constants.NumberOfTurns] = 8
            };
            var randomCodeGenerator = new RandomCodeGenerator(config);  
            
            //act
            var secretCode = randomCodeGenerator.GenerateSecretCode();
        
            //assert
            Assert.Equal(4, secretCode.Count);
            
        }
        
        [Fact]
        public void It_Should_Return_A_SecretCodeOfColouredPegs()
        {
            //arrange
            var config = new MastermindConfig
            {
                [Constants.CodeLength] = 4, [Constants.NumberOfColours] = 6, [Constants.NumberOfTurns] = 8
            };
            var randomCodeGenerator = new RandomCodeGenerator(config);
            
            //act
            var secretCode = randomCodeGenerator.GenerateSecretCode();
        
            //assert
            foreach (var peg in secretCode)
            {
                Assert.True( Enum.IsDefined(typeof(Peg), peg)); 
            }
        }
        
        //TODO: Test randomness
        
        
      
    }
}