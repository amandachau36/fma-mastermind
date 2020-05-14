using System.Collections.Generic;
using FluentAssertions;
using Mastermind.Business.BoardGame;
using Mastermind.Business.Code;
using Mastermind.Business.CodeGenerators;
using Mastermind.Business.Turns;
using Mastermind.DataAccess;
using Mastermind.DataAccess.Enums;
using Mastermind.DataAccess.MastermindConfigurationBuilder;
using Xunit;

namespace Mastermind.Tests.UnitTests.Business
{
    public class GameUnitTests
    {
        [Fact]
        public void It_Should_Contain_AListOfTurns_When_AGuessIsChecked()
        {
            //arrange 
            var codeGenerator = new CodeGenerator(new List<Peg> {Peg.Red, Peg.Blue, Peg.Green, Peg.Yellow});
            var nonRandomizer = new NonRandomizer();
            var code = new CodeChecker(codeGenerator, nonRandomizer);
            var config = new MastermindConfig
            {
                [DataConstants.CodeLength] = 4, [DataConstants.NumberOfColours] = 6, [DataConstants.NumberOfTurns] = 8
            };
            var game = new Game(config, code);
            game.StartNewGame();
            
            //act
            var guess = new List<Peg> {Peg.Blue, Peg.Orange, Peg.Orange, Peg.Yellow};
            var feedback = game.CheckGuess(guess);
            game.UpdateGame(guess, feedback);
            
            //assert
            var expectedTurn = new Turn(
                new List<Peg> {Peg.Blue, Peg.Orange, Peg.Orange, Peg.Yellow},
                new List<Feedback> {Feedback.Black, Feedback.White});
        
            var turn = game.Turns[0]; 
            
            turn.Should().BeEquivalentTo(expectedTurn);
        }
        
        [Theory]
        [MemberData(nameof(GetGuesses))]
        public void It_Should_SetIsWinnerAndIsGameOver_ToTrue_When_GuessIsCorrect(List<Peg> guess, bool expectedIsWinner, bool expectedIsGameOver)
        {
            //arrange 
            var codeGenerator = new CodeGenerator(new List<Peg> {Peg.Red, Peg.Blue, Peg.Green, Peg.Yellow});
            var nonRandomizer = new NonRandomizer();
            var code = new CodeChecker(codeGenerator, nonRandomizer);
            var config = new MastermindConfig
            {
                [DataConstants.CodeLength] = 4, [DataConstants.NumberOfColours] = 6, [DataConstants.NumberOfTurns] = 8
            };
            var game = new Game(config, code);
            game.StartNewGame();
           
            
            //act
            var feedback = game.CheckGuess(guess);
            game.UpdateGame(guess, feedback);
            
            //assert
            Assert.Equal(expectedIsWinner, game.IsWinner);
            Assert.Equal(expectedIsGameOver, game.IsGameOver);
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
        public void It_Should_SetIsGameOver_ToTrue_When_MaxNumberOfGuessesAreReached(int numberOfTurns, bool expectedIsGameOver)
        {
             //arrange 
             var codeGenerator = new CodeGenerator(new List<Peg> {Peg.Red, Peg.Blue, Peg.Green, Peg.Yellow});
             var nonRandomizer = new NonRandomizer();
             var code = new CodeChecker(codeGenerator, nonRandomizer);
             var config = new MastermindConfig
             {
                 [DataConstants.CodeLength] = 4, [DataConstants.NumberOfColours] = 6, [DataConstants.NumberOfTurns] = 8
             };
             var game = new Game(config, code);
             game.StartNewGame();
        
        
             for (var i = 0; i < numberOfTurns; i++)
             {
                 var guess = new List<Peg> {Peg.Blue, Peg.Orange, Peg.Orange, Peg.Yellow};
                 var feedback = game.CheckGuess(guess);
                 game.UpdateGame(guess, feedback);
             }
             
            
             //act
             var finalGuess = new List<Peg> {Peg.Orange, Peg.Orange, Peg.Orange, Peg.Orange};
             var finalFeedback = game.CheckGuess(finalGuess);
             game.UpdateGame(finalGuess, finalFeedback);
        
             
             
             //assert
             Assert.Equal(expectedIsGameOver, game.IsGameOver);
            
        }
        
        public static IEnumerable<object[]> GetNumberOfTurns()
        {
            yield return new object[]
            {
                7, 
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