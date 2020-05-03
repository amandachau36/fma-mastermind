using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Mastermind.Business;
using Mastermind.Business.Code;
using Mastermind.Business.Turns;
using Xunit;

namespace Mastermind.UnitTests.Business
{
    public class GameGeneratorUnitTests
    {
        [Fact]
        public void It_Should_Generate_A_NewGame_With_Correct_Configuration() //TODO: integration test? 
        {
            //arrange
            var config = GameGenerator.GetConfiguration("standardConfig.json");
            var staticCodeGenerator = new StaticCodeGenerator(new List<Peg>{Peg.Blue, Peg.Blue, Peg.Orange, Peg.Orange});
            var codeChecker = new CodeChecker(staticCodeGenerator);
            var game = GameGenerator.GenerateGame(config, codeChecker);
            
            game.CheckGuess(new List<Peg>{ Peg.Blue, Peg.Orange, Peg.Blue, Peg.Orange});
            
            //assert
            Assert.False(game.IsWinner);
            Assert.False(game.IsGameOver);
            game.Turns.Last().Should().BeEquivalentTo(new Turn(new List<Peg>{ Peg.Blue, Peg.Orange, Peg.Blue, Peg.Orange}, new List<FeedBack>{FeedBack.Black, FeedBack.Black, FeedBack.White, FeedBack.White} ));
            
        }
    }
}