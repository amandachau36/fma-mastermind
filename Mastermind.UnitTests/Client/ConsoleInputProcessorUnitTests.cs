using System;
using System.Collections.Generic;
using Mastermind.Client;
using Mastermind.Client.InputProcessor;
using Mastermind.DataAccess.Enums;
using Mastermind.DataAccess.MastermindConfigurationBuilder;
using Xunit;

namespace Mastermind.UnitTests.Client
{
    public class ConsoleInputProcessorUnitTests
    {
        [Fact]
        public void It_Should_Process_ValidInput()
        {
            //arrange
            var config = new MastermindConfig
            {
                [Constants.CodeLength] = 4, [Constants.NumberOfColours] = 6, [Constants.NumberOfTurns] = 8
            };
            
            var consoleInputProcessor = new ConsoleInputProcessor(config);
            
            //act
            var input = "Blue, Red, Green, Red";
            var guess = consoleInputProcessor.Process(input);
        
            //assert
            var expectedGuess = new List<Peg>{ Peg.Blue, Peg.Red, Peg.Green, Peg.Red};
            
            Assert.Equal(expectedGuess, guess);
        }
        
        
        [Theory]
        [MemberData(nameof(GetUserInput))]
        public void It_Should_Throw_An_InvalidInputException_When_Given_InvalidInput(string input, string errorMessage)
        {
            //arrange
            var config = new MastermindConfig
            {
                [Constants.CodeLength] = 4, [Constants.NumberOfColours] = 6, [Constants.NumberOfTurns] = 8
            };
            var consoleInputProcessor = new ConsoleInputProcessor(config);
            
            //act
            Action actual = () => consoleInputProcessor.Process(input);
        
      
            //assert
            var exception = Assert.Throws<InvalidInputException>(actual);
            Assert.Equal(errorMessage, exception.Message);
        }
        
        public static IEnumerable<object[]> GetUserInput()
        {
     
            yield return new object[]
            {
                "Blue, Red",
                "Error: you must pass in 4 colours!"
            };
            
            yield return new object[]
            {
                "Blue, Orange, Orange, Yellow, Red",
                "Error: you must pass in 4 colours!"
            };
            
            yield return new object[]
            {
                " ",
                "Error: you must pass in 4 colours!"
            };
            
            yield return new object[]
            {
                "",
                "Error: you must pass in 4 colours!"
            };
            
            yield return new object[]
            {
                "Red, Red, Rainbow, Red",
                "Error: Rainbow is not an invalid colour!"
            };

            
        }
        
    }
}