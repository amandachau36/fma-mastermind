using System.Collections.Generic;
using System.Linq;
using Mastermind.Business.Code;
using Mastermind.Business.CodeGenerator;
using Mastermind.DataAccess.Enums;
using Xunit;

namespace Mastermind.Tests.UnitTests.Business
{
    public class CodeUnitTests
    {
        [Fact]
        public void It_Should_Return_A_KnownSecretCode()
        {
            //arrange
            var codeGenerator = new StaticCodeGenerator(new List<Peg> {Peg.Red, Peg.Blue, Peg.Green, Peg.Yellow});
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
        public void It_Should_Return_BlackForEveryCorrectlyPositionedColour(List<Peg> guess, List<FeedBack> expectedFeedback)
        {
            //arrange
            var codeGenerator = new StaticCodeGenerator(new List<Peg> {Peg.Red, Peg.Blue, Peg.Green, Peg.Yellow});
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
                new List<FeedBack>(),
            };
            
            yield return new object[]
            {
                new List<Peg>{ Peg.Red, Peg.Orange, Peg.Orange, Peg.Orange},
                new List<FeedBack> { FeedBack.Black },
            };
            
            yield return new object[]
            {
                new List<Peg>{ Peg.Red, Peg.Orange, Peg.Orange, Peg.Yellow},
                new List<FeedBack> { FeedBack.Black, FeedBack.Black },
            };
            
            yield return new object[]
            {
                new List<Peg>{ Peg.Red, Peg.Blue, Peg.Green, Peg.Yellow},
                new List<FeedBack> { FeedBack.Black, FeedBack.Black, FeedBack.Black, FeedBack.Black  },
            };
        }
        
                
        [Theory]
        [MemberData(nameof(GetGuesses2))]
        public void It_Should_Return_WhiteForEveryCorrectColourAtIncorrectPosition(List<Peg> guess, List<FeedBack> expectedFeedback)
        {
            //arrange
            var codeGenerator = new StaticCodeGenerator(new List<Peg> {Peg.Red, Peg.Blue, Peg.Green, Peg.Yellow});
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
                new List<FeedBack>(),
            };
        
            yield return new object[]
            {
                new List<Peg>{ Peg.Orange, Peg.Red, Peg.Orange, Peg.Orange},
                new List<FeedBack> { FeedBack.White },
            };
            
            yield return new object[]
            {
                new List<Peg>{ Peg.Orange, Peg.Red, Peg.Orange, Peg.Blue},
                new List<FeedBack> { FeedBack.White, FeedBack.White },
            };
            
            yield return new object[]
            {
                new List<Peg>{ Peg.Yellow, Peg.Red, Peg.Blue, Peg.Green},
                new List<FeedBack> { FeedBack.White, FeedBack.White, FeedBack.White, FeedBack.White },
            };
        }
        
        
        [Theory]
        [MemberData(nameof(GetGuesses3))]
        public void It_Should_Return_WhiteAndBlackPegsCorrectly_WhenGiveAGuess(List<Peg> guess, List<FeedBack> expectedFeedback)
        {
            //arrange
            var codeGenerator = new StaticCodeGenerator(new List<Peg> {Peg.Red, Peg.Blue, Peg.Green, Peg.Blue});
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
                new List<FeedBack>(),
            };
            
            yield return new object[]
            {
                new List<Peg>{ Peg.Blue, Peg.Orange, Peg.Orange, Peg.Blue},
                new List<FeedBack>{ FeedBack.Black, FeedBack.White},
            };
        
            yield return new object[]
            {
                new List<Peg>{ Peg.Blue, Peg.Red, Peg.Green, Peg.Orange},
                new List<FeedBack> { FeedBack.Black, FeedBack.White, FeedBack.White },
            };
            
            yield return new object[]
            {
                new List<Peg>{ Peg.Blue, Peg.Red, Peg.Green, Peg.Blue},
                new List<FeedBack> { FeedBack.Black, FeedBack.Black, FeedBack.White, FeedBack.White },
            };
            
            yield return new object[]
            {
                new List<Peg>{ Peg.Blue, Peg.Red, Peg.Blue, Peg.Green},
                new List<FeedBack> { FeedBack.White, FeedBack.White, FeedBack.White, FeedBack.White },
            };
        }

        
    }

    internal class StaticCodeGenerator : ICodeGenerator
    {
        private readonly List<Peg> _secretCode;

        public StaticCodeGenerator(List<Peg> secretCode)
        {
            _secretCode = secretCode;
        }
        public List<Peg> GenerateSecretCode()
        {
            return _secretCode;
        }
    }
    
    internal class NonRandomizer : IFeedbackRandomizer
    {
        public List<FeedBack> Randomize(List<FeedBack> feedback)
        {
            return feedback;
        }
    }
}

// var codeGenerator = new Mock<ICodeGenerator>();
// codeGenerator
// .Setup(x => x.GenerateSecretCode())
// .Returns(() =>  v;
