using System;
using Mastermind.Business.Code;
using Mastermind.Business.CodeGenerator;
using Mastermind.DataAccess;
using Xunit;

namespace Mastermind.UnitTests.Business
{
    public class CodeGeneratorUnitTests
    {
        [Fact]
        public void It_Should_Return_A_SecretCodeWithALengthOfFour()
        {
            //arrange
            var randomCodeGenerator = new RandomCodeGenerator(new MastermindConfiguration(4, 6));  
            
            //act
            var secretCode = randomCodeGenerator.GenerateSecretCode();

            //assert
            Assert.Equal(4, secretCode.Count);
            
        }
        
        [Fact]
        public void It_Should_Return_A_SecretCodeOfColouredPegs()
        {
            //arrange
            var randomCodeGenerator = new RandomCodeGenerator(new MastermindConfiguration(4, 6));
            
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