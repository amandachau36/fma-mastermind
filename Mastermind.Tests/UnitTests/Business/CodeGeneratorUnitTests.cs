using System;
using Mastermind.Business.CodeGenerator;
using Mastermind.DataAccess;
using Mastermind.DataAccess.Enums;
using Mastermind.DataAccess.MastermindConfigurationBuilder;
using Xunit;

namespace Mastermind.Tests.UnitTests.Business
{
    public class CodeGeneratorUnitTests
    {
        [Fact]
        public void It_Should_Return_A_SecretCodeWithALengthOfFour_When_CodeLengthInConfigIsFour()
        {
            //arrange
            var config = new MastermindConfig
            {
                [DataConstants.CodeLength] = 4, [DataConstants.NumberOfColours] = 6, [DataConstants.NumberOfTurns] = 8
            };
            var randomCodeGenerator = new RandomCodeGenerator(config);  
            
            //act
            var secretCode = randomCodeGenerator.GenerateSecretCode();
        
            //assert
            Assert.Equal(4, secretCode.Count);
            
        }
        
        [Fact]
        public void It_Should_Return_A_SecretCodeOfColouredPegsWithFlagsFiveOrLess_When_NumberOfColoursInConfigIsSix()
        {
            //arrange
            var config = new MastermindConfig
            {
                [DataConstants.CodeLength] = 4, [DataConstants.NumberOfColours] = 6, [DataConstants.NumberOfTurns] = 8
            };
            var randomCodeGenerator = new RandomCodeGenerator(config);
            
            //act
            var secretCode = randomCodeGenerator.GenerateSecretCode();
        
            //assert
            foreach (var peg in secretCode)
            {
                Assert.True( Enum.IsDefined(typeof(Peg), peg) && (int)peg < config[DataConstants.NumberOfColours] );  
            }
        }
        
        //TODO: Test randomness
        
        
      
    }
}