using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Mastermind.Business.BoardGame;
using Mastermind.Business.Code;
using Mastermind.Business.CodeGenerator;
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
        public void It_Should_Contain_AListOfTurns()
        {
            //arrange 
            var codeGenerator = new StaticCodeGenerator(new List<Peg> {Peg.Red, Peg.Blue, Peg.Green, Peg.Yellow});
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
            game.CheckGuess(guess);
            
            //assert
            var expectedTurn = new Turn(
                new List<Peg> {Peg.Blue, Peg.Orange, Peg.Orange, Peg.Yellow},
                new List<FeedBack> {FeedBack.Black, FeedBack.White});
        
            var turn = game.Turns[0]; 
            
            turn.Should().BeEquivalentTo(expectedTurn);
        }
        
        [Theory]
        [MemberData(nameof(GetGuesses))]
        public void It_Should_Set_IsWinner_And_IsGameOver_To_True_OnlyWhenGuessIsCorrect(List<Peg> guess, bool expectedIsWinner, bool expectedIsGameOver)
        {
            //arrange 
            var codeGenerator = new StaticCodeGenerator(new List<Peg> {Peg.Red, Peg.Blue, Peg.Green, Peg.Yellow});
            var nonRandomizer = new NonRandomizer();
            var code = new CodeChecker(codeGenerator, nonRandomizer);
            var config = new MastermindConfig
            {
                [DataConstants.CodeLength] = 4, [DataConstants.NumberOfColours] = 6, [DataConstants.NumberOfTurns] = 8
            };
            var game = new Game(config, code);
            game.StartNewGame();
           
            
            //act
            game.CheckGuess(guess);
            
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
        public void It_Should_Set_IsGameOver_To_True_MaxNumberOfGuessesAreReached(int numberOfTurns, bool expectedIsGameOver)
        {
             //arrange 
             var codeGenerator = new StaticCodeGenerator(new List<Peg> {Peg.Red, Peg.Blue, Peg.Green, Peg.Yellow});
             var nonRandomizer = new NonRandomizer();
             var code = new CodeChecker(codeGenerator, nonRandomizer);
             var config = new MastermindConfig
             {
                 [DataConstants.CodeLength] = 4, [DataConstants.NumberOfColours] = 6, [DataConstants.NumberOfTurns] = 8
             };
             var game = new Game(config, code);
             code.GenerateSecretCode();
        
        
             for (var i = 0; i < numberOfTurns; i++)
             {
                 var guess = new List<Peg> {Peg.Blue, Peg.Orange, Peg.Orange, Peg.Yellow};
                 game.CheckGuess(guess);
             }
             
            
             //act
             var finalGuess = new List<Peg> {Peg.Orange, Peg.Orange, Peg.Orange, Peg.Orange};
             game.CheckGuess(finalGuess);
        
             
             
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