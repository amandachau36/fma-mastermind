using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mastermind.Business.BoardGame;
using Mastermind.Business.Code;
using Mastermind.Business.CodeGenerator;
using Mastermind.Client;
using Mastermind.Client.Display;
using Mastermind.Client.InputCollector;
using Mastermind.Client.InputProcessor;
using Mastermind.DataAccess;
using Mastermind.DataAccess.Enums;
using Mastermind.Tests.UnitTests.Business;
using Moq;
using Xunit;
using Constants = Mastermind.Client.Constants;

namespace Mastermind.Tests.ComponentTests
{
    public class ComponentTests
    {
        [Fact]
        public void It_Should_DisplayFeedbackAndWinningMessageWhenGiven_ValidInput()
        {
            var config = ConfigurationLoader.LoadMastermindConfiguration(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ConfigFiles", "StandardConfig.json"));
            
            var staticCodeGenerator = new StaticCodeGenerator(new List<Peg>
            {
                Peg.Blue,
                Peg.Green, 
                Peg.Yellow,
                Peg.Green
            }); 
            var feedbackRandomizer = new NonRandomizer();
            
            var codeChecker = new CodeChecker(staticCodeGenerator, feedbackRandomizer);

            var game = new Game(config, codeChecker);

            var consoleDisplayStub = new ConsoleDisplayStub();
            
            var mockCollector = new Mock<ICollector>();
            mockCollector.SetupSequence(x => x.Collect())
                .Returns("blue, red, green, red")
                .Returns("green, yellow, green, blue")
                .Returns("Red, RED, red. Red")
                .Returns("Blue, Green, Yellow, Green");

            var gameEngine = new GameEngine(
                consoleDisplayStub, 
                mockCollector.Object, 
                new ConsoleInputProcessor(config), 
                game);
            
           //Act
           gameEngine.PlayGame();
           
           //Assert
            var expectedMessages = new List<string>
            { 
                Constants.Welcome,
                Constants.Border,
                Constants.SecretCode,
                "Blue, Green, Yellow, Green",
                Constants.Border,
                Constants.PromptGuess,
                Constants.Feedback,
                "Black, White",
                Constants.PromptGuess,
                Constants.Feedback,
                "White, White, White, White",
                Constants.PromptGuess,
                Constants.Feedback,
                "",
                Constants.PromptGuess,
                Constants.Feedback,
                "Black, Black, Black, Black",
                Constants.Winner
                
            };
        
            for (var i = 0; i < consoleDisplayStub.Messages.Count; i++)
            {
                Assert.Equal(expectedMessages[i], consoleDisplayStub.Messages[i]);
            }
            //Assert.True(consoleDisplay.Messages.SequenceEqual(expectedMessages)); 
        }
        
        [Fact]
        public void It_Should_DisplayFeedbackAndErrorMessagesWhenGiven_InValidInput()
        {
            var config = ConfigurationLoader.LoadMastermindConfiguration(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ConfigFiles", "StandardConfig.json"));
            
            var staticCodeGenerator = new StaticCodeGenerator(new List<Peg>
            {
                Peg.Blue,
                Peg.Green, 
                Peg.Yellow,
                Peg.Green
            }); 
            var feedbackRandomizer = new NonRandomizer();
            
            var codeChecker = new CodeChecker(staticCodeGenerator, feedbackRandomizer);

            var game = new Game(config, codeChecker);

            var consoleDisplayStub = new ConsoleDisplayStub();
            
            var mockCollector = new Mock<ICollector>();
            mockCollector.SetupSequence(x => x.Collect())
                .Returns("Blue, green, reD, rainbow")
                .Returns("Blue, red, red, red, red")
                .Returns("Blue, blue")
                .Returns("")
                .Returns("hello")
                .Returns("BLUE, green, YeLLoW, Green");

            var gameEngine = new GameEngine(
                consoleDisplayStub, 
                mockCollector.Object, 
                new ConsoleInputProcessor(config), 
                game);
            
           //Act
           gameEngine.PlayGame();
           
           //Assert
            var expectedMessages = new List<string>
            { 
                Constants.Welcome,
                Constants.Border,
                Constants.SecretCode,
                "Blue, Green, Yellow, Green",
                Constants.Border,
                Constants.PromptGuess,
                "Error: rainbow is not an invalid colour!",
                Constants.PromptGuess,
                $"Error: you must pass in {config[Constants.CodeLength]} colours!",
                Constants.PromptGuess,
                $"Error: you must pass in {config[Constants.CodeLength]} colours!",
                Constants.PromptGuess,
                $"Error: you must pass in {config[Constants.CodeLength]} colours!",
                Constants.PromptGuess,
                "Error: hello is not an invalid colour!",
                Constants.PromptGuess,
                Constants.Feedback,
                "Black, Black, Black, Black",
                Constants.Winner
                
            };
        
            for (var i = 0; i < consoleDisplayStub.Messages.Count; i++)
            {
                Assert.Equal(expectedMessages[i], consoleDisplayStub.Messages[i]);
            }
            //Assert.True(consoleDisplay.Messages.SequenceEqual(expectedMessages)); 
        }
        
        [Fact]
        public void It_Should_DisplayLostGame_When_Given_MaxNumberOfIncorrectGuesses()
        {
            var config = ConfigurationLoader.LoadMastermindConfiguration(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ConfigFiles", "StandardConfig.json"));
            
            var staticCodeGenerator = new StaticCodeGenerator(new List<Peg>
            {
                Peg.Blue,
                Peg.Green, 
                Peg.Yellow,
                Peg.Green
            }); 
            var feedbackRandomizer = new NonRandomizer();
            
            var codeChecker = new CodeChecker(staticCodeGenerator, feedbackRandomizer);

            var game = new Game(config, codeChecker);

            var consoleDisplayStub = new ConsoleDisplayStub();
            
            var mockCollector = new Mock<ICollector>();
            mockCollector.SetupSequence(x => x.Collect())
                .Returns("blue, red, green, red")
                .Returns("green, yellow, green, blue")
                .Returns("Red, RED, red. Red")
                .Returns("Red, RED, red. Red")
                .Returns("Red, RED, red. Red")
                .Returns("Red, RED, red. Red")
                .Returns("Red, RED, red. Red")
                .Returns("Red, RED, red. Red");

            var gameEngine = new GameEngine(
                consoleDisplayStub, 
                mockCollector.Object, 
                new ConsoleInputProcessor(config), 
                game);
            
           //Act
           gameEngine.PlayGame();
           
           //Assert
            var expectedMessages = new List<string>
            { 
                Constants.Welcome,
                Constants.Border,
                Constants.SecretCode,
                "Blue, Green, Yellow, Green",
                Constants.Border,
                Constants.PromptGuess,
                Constants.Feedback,
                "Black, White",
                Constants.PromptGuess,
                Constants.Feedback,
                "White, White, White, White",
                Constants.PromptGuess,
                Constants.Feedback,
                "",
                Constants.PromptGuess,
                Constants.Feedback,
                "",
                Constants.PromptGuess,
                Constants.Feedback,
                "",
                Constants.PromptGuess,
                Constants.Feedback,
                "",
                Constants.PromptGuess,
                Constants.Feedback,
                "",
                Constants.PromptGuess,
                Constants.Feedback,
                "",
                Constants.Loser
                
            };
        
            for (var i = 0; i < consoleDisplayStub.Messages.Count; i++)
            {
                Assert.Equal(expectedMessages[i], consoleDisplayStub.Messages[i]);
            }
            //Assert.True(consoleDisplay.Messages.SequenceEqual(expectedMessages)); 
        }
        private class ConsoleDisplayStub : IDisplay
        {
            public List<string> Messages = new List<string>();
            public void Display(string message)
            {
                Messages.Add(message);
            }

            public void Display<T>(List<T> list)
            {
                var stringList = string.Join(", ", list.Select(x => x.ToString()));

                Messages.Add(stringList);
            }
        }
    }
}