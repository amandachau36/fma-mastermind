using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Mastermind.Business.BoardGame;
using Mastermind.Business.Code;
using Mastermind.Business.Turns;
using Mastermind.DataAccess;
using Xunit;

namespace Mastermind.UnitTests.Business
{
    public class BoardUnitTests
    {
        [Fact]
        public void It_Should_Contain_AListOfTurns()
        {
            //arrange 
            var codeGenerator = new StaticCodeGenerator(new List<Peg> {Peg.Red, Peg.Blue, Peg.Green, Peg.Yellow});
            var code = new CodeChecker(codeGenerator);
            var config = new MastermindConfiguration(4,6, 8 );
            var board = new Game(config, code);
            code.GenerateSecretCode();
            
            //act
            var guess = new List<Peg> {Peg.Blue, Peg.Orange, Peg.Orange, Peg.Yellow};
            board.CheckGuess(guess);
            
            //assert
            var expectedTurn = new Turn(
                new List<Peg> {Peg.Blue, Peg.Orange, Peg.Orange, Peg.Yellow},
                new List<FeedBack> {FeedBack.Black, FeedBack.White});

            board.Turns.Last().Should().BeEquivalentTo(expectedTurn);
           
        }
        
        [Theory]
        [MemberData(nameof(GetGuesses))]
        public void It_Should_Set_IsWinner_And_IsGameOver_To_True_OnlyWhenGuessIsCorrect(List<Peg> guess, bool expectedIsWinner, bool expectedIsGameOver)
        {
            //arrange 
            var codeGenerator = new StaticCodeGenerator(new List<Peg> {Peg.Red, Peg.Blue, Peg.Green, Peg.Yellow});
            var code = new CodeChecker(codeGenerator);
            var config = new MastermindConfiguration(4,6, 8 );
            var board = new Game(config, code);
            code.GenerateSecretCode();
            
            //act
            board.CheckGuess(guess);
            
            //assert
            Assert.Equal(expectedIsWinner, board.IsWinner);
            Assert.Equal(expectedIsGameOver, board.IsGameOver);
        }
        
        public static IEnumerable<object[]> GetGuesses()
        {
            yield return new object[]
            {
                new List<Peg>{ Peg.Orange, Peg.Orange, Peg.Orange, Peg.Orange},
                false,
                false
            };
            
            yield return new object[]
            {
                new List<Peg>{ Peg.Red, Peg.Blue, Peg.Green, Peg.Blue},
                false,
                false
            };
            
            
            yield return new object[]
            {
                new List<Peg>{ Peg.Blue, Peg.Green, Peg.Yellow, Peg.Red},
                false,
                false,
            };
            
            yield return new object[]
            {
                new List<Peg>{Peg.Red, Peg.Blue, Peg.Green, Peg.Yellow},
                true,
                true
            };
        }
        
        [Theory]
        [MemberData(nameof(GetNumberOfTurns))]
        public void It_Should_Set_IsGameOver_To_True_MaxNumberOfGuessesAreReached(int numberOfTurns, bool expectedIsGameOver)
        {
             //arrange 
             var codeGenerator = new StaticCodeGenerator(new List<Peg> {Peg.Red, Peg.Blue, Peg.Green, Peg.Yellow});
             var code = new CodeChecker(codeGenerator);
             var config = new MastermindConfiguration(4,6, 8 );
             var board = new Game(config, code);
             code.GenerateSecretCode();
        
        
             for (var i = 0; i < numberOfTurns; i++)
             {
                 var guess = new List<Peg> {Peg.Blue, Peg.Orange, Peg.Orange, Peg.Yellow};
                 board.CheckGuess(guess);
             }
             
            
             //act
             var finalGuess = new List<Peg> {Peg.Orange, Peg.Orange, Peg.Orange, Peg.Orange};
             board.CheckGuess(finalGuess);
        
             
             
             //assert
             Assert.Equal(expectedIsGameOver, board.IsGameOver);
            
        }
        
        public static IEnumerable<object[]> GetNumberOfTurns()
        {
            yield return new object[]
            {
                7, //TODO: no magic numbers 
                true,
            };
            
            yield return new object[]
            {
                6,
                false,
            };
            
        }
        
    }
}