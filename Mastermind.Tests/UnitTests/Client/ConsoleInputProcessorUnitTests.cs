using System;
using System.Collections.Generic;
using Mastermind.Client.InputProcessor;
using Mastermind.DataAccess;
using Mastermind.DataAccess.Enums;
using Mastermind.DataAccess.MastermindConfigurationBuilder;
using Xunit;

namespace Mastermind.Tests.UnitTests.Client
{
    public class ConsoleInputProcessorUnitTests
    {
        [Fact]
        public void It_Should_Process_Input_When_Given_ValidInput()
        {
            //arrange
            var config = new MastermindConfig
            {
                [DataConstants.CodeLength] = 4, [DataConstants.NumberOfColours] = 6, [DataConstants.NumberOfTurns] = 8
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
                [DataConstants.CodeLength] = 4, [DataConstants.NumberOfColours] = 6, [DataConstants.NumberOfTurns] = 8
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
                "Error: Rainbow is an invalid colour!"
            };

            
        }
        
    }
}