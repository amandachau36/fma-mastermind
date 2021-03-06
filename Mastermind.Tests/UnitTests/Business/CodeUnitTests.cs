using System.Collections.Generic;
using System.Linq;
using Mastermind.Business.Code;
using Mastermind.Business.CodeGenerators;
using Mastermind.DataAccess.Enums;
using Xunit;

namespace Mastermind.Tests.UnitTests.Business
{
    public class CodeUnitTests
    {
        [Fact]
        public void It_Should_Return_A_KnownSecretCode_When_A_StaticCodeGeneratorIsUsed()
        {
            //arrange
            var codeGenerator = new CodeGenerator(new List<Peg> {Peg.Red, Peg.Blue, Peg.Green, Peg.Yellow});
            var nonRandomizer = new NonRandomizer();
            var code = new CodeChecker(codeGenerator, nonRandomizer);
            
            //act
            code.GenerateSecretCode();
        
            //assert
            var expectedSecretCode = new List<Peg>{ Peg.Red, Peg.Blue, Peg.Green, Peg.Yellow};
            Assert.True(code.SecretCode.SequenceEqual(expectedSecretCode));
        }
        
        [Theory]
        [MemberData(nameof(GetGuesses))]
        public void It_Should_Return_BlackForEveryCorrectlyPositionedColour_When_Given_ValidGuesses(List<Peg> guess, List<Feedback> expectedFeedback)
        {
            //arrange
            var codeGenerator = new CodeGenerator(new List<Peg> {Peg.Red, Peg.Blue, Peg.Green, Peg.Yellow});
            var nonRandomizer = new NonRandomizer();
            var code = new CodeChecker(codeGenerator, nonRandomizer);
            code.GenerateSecretCode();
        
            //act
            var feedback = code.CheckGuess(guess);
            
            //assert
            Assert.Equal(expectedFeedback, feedback);
        }
        
        public static IEnumerable<object[]> GetGuesses()
        {
            yield return new object[]
            {
                new List<Peg>{ Peg.Orange, Peg.Orange, Peg.Orange, Peg.Orange},
                new List<Feedback>(),
            };
            
            yield return new object[]
            {
                new List<Peg>{ Peg.Red, Peg.Orange, Peg.Orange, Peg.Orange},
                new List<Feedback> { Feedback.Black },
            };
            
            yield return new object[]
            {
                new List<Peg>{ Peg.Red, Peg.Orange, Peg.Orange, Peg.Yellow},
                new List<Feedback> { Feedback.Black, Feedback.Black },
            };
            
            yield return new object[]
            {
                new List<Peg>{ Peg.Red, Peg.Blue, Peg.Green, Peg.Yellow},
                new List<Feedback> { Feedback.Black, Feedback.Black, Feedback.Black, Feedback.Black  },
            };
        }
        
                
        [Theory]
        [MemberData(nameof(GetGuesses2))]
        public void It_Should_Return_WhiteForEveryCorrectColourAtIncorrectPosition_When_Given_ValidGuesses(List<Peg> guess, List<Feedback> expectedFeedback)
        {
            //arrange
            var codeGenerator = new CodeGenerator(new List<Peg> {Peg.Red, Peg.Blue, Peg.Green, Peg.Yellow});
            var nonRandomizer = new NonRandomizer();
            var code = new CodeChecker(codeGenerator, nonRandomizer);
            code.GenerateSecretCode();
        
            //act
            var feedback = code.CheckGuess(guess);
            
            //assert
            Assert.Equal(expectedFeedback, feedback );
        }
        
        public static IEnumerable<object[]> GetGuesses2()
        {
            yield return new object[]
            {
                new List<Peg>{ Peg.Orange, Peg.Orange, Peg.Orange, Peg.Orange},
                new List<Feedback>(),
            };
        
            yield return new object[]
            {
                new List<Peg>{ Peg.Orange, Peg.Red, Peg.Orange, Peg.Orange},
                new List<Feedback> { Feedback.White },
            };
            
            yield return new object[]
            {
                new List<Peg>{ Peg.Orange, Peg.Red, Peg.Orange, Peg.Blue},
                new List<Feedback> { Feedback.White, Feedback.White },
            };
            
            yield return new object[]
            {
                new List<Peg>{ Peg.Yellow, Peg.Red, Peg.Blue, Peg.Green},
                new List<Feedback> { Feedback.White, Feedback.White, Feedback.White, Feedback.White },
            };
        }
        
        
        [Theory]
        [MemberData(nameof(GetGuesses3))]
        public void It_Should_Return_WhiteAndBlackPegsCorrectly_When_Given_ValidGuesses(List<Peg> guess, List<Feedback> expectedFeedback)
        {
            //arrange
            var codeGenerator = new CodeGenerator(new List<Peg> {Peg.Red, Peg.Blue, Peg.Green, Peg.Blue});
            var nonRandomizer = new NonRandomizer();
            var code = new CodeChecker(codeGenerator, nonRandomizer);
            code.GenerateSecretCode();
        
            //act
            var feedback = code.CheckGuess(guess);
            
            //assert
            Assert.Equal(expectedFeedback, feedback );
        }
        
        public static IEnumerable<object[]> GetGuesses3()
        {
            yield return new object[]
            {
                new List<Peg>{ Peg.Orange, Peg.Orange, Peg.Orange, Peg.Orange},
                new List<Feedback>(),
            };
            
            yield return new object[]
            {
                new List<Peg>{ Peg.Blue, Peg.Orange, Peg.Orange, Peg.Blue},
                new List<Feedback>{ Feedback.Black, Feedback.White},
            };
        
            yield return new object[]
            {
                new List<Peg>{ Peg.Blue, Peg.Red, Peg.Green, Peg.Orange},
                new List<Feedback> { Feedback.Black, Feedback.White, Feedback.White },
            };
            
            yield return new object[]
            {
                new List<Peg>{ Peg.Blue, Peg.Red, Peg.Green, Peg.Blue},
                new List<Feedback> { Feedback.Black, Feedback.Black, Feedback.White, Feedback.White },
            };
            
            yield return new object[]
            {
                new List<Peg>{ Peg.Blue, Peg.Red, Peg.Blue, Peg.Green},
                new List<Feedback> { Feedback.White, Feedback.White, Feedback.White, Feedback.White },
            };
        }
    }

 
    internal class NonRandomizer : IFeedbackRandomizer
    {
        public List<Feedback> Randomize(List<Feedback> feedback)
        {
            return feedback;
        }
    }
}


