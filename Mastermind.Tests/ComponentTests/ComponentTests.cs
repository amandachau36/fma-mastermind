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
                ClientConstants.Welcome,
                ClientConstants.Border,
                ClientConstants.SecretCode,
                "Blue, Green, Yellow, Green",
                ClientConstants.Border,
                ClientConstants.GuessesLeft + 8 + ClientConstants.PromptGuess,
                ClientConstants.Feedback,
                "Black, White",
                ClientConstants.GuessesLeft + 7 + ClientConstants.PromptGuess,
                ClientConstants.Feedback,
                "White, White, White, White",
                ClientConstants.GuessesLeft + 6 + ClientConstants.PromptGuess,
                ClientConstants.Feedback,
                "",
                ClientConstants.GuessesLeft + 5 + ClientConstants.PromptGuess,
                ClientConstants.Feedback,
                "Black, Black, Black, Black",
                ClientConstants.Winner
                
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
                .Returns("blue, green, pink, red")
                .Returns("grey")
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
                ClientConstants.Welcome,
                ClientConstants.Border,
                ClientConstants.SecretCode,
                "Blue, Green, Yellow, Green",
                ClientConstants.Border,
                ClientConstants.GuessesLeft + 8 + ClientConstants.PromptGuess,
                "Error: rainbow is not an invalid colour!",
                ClientConstants.GuessesLeft + 8 + ClientConstants.PromptGuess,
                $"Error: you must pass in {config[DataConstants.CodeLength]} colours!",
                ClientConstants.GuessesLeft + 8 + ClientConstants.PromptGuess,
                $"Error: you must pass in {config[DataConstants.CodeLength]} colours!",
                ClientConstants.GuessesLeft + 8 + ClientConstants.PromptGuess,
                $"Error: you must pass in {config[DataConstants.CodeLength]} colours!",
                ClientConstants.GuessesLeft + 8 + ClientConstants.PromptGuess,
                "Error: hello is not an invalid colour!",
                ClientConstants.GuessesLeft + 8 + ClientConstants.PromptGuess,
                "Error: pink is not an invalid colour!",
                ClientConstants.GuessesLeft + 8 + ClientConstants.PromptGuess,
                "Error: grey is not an invalid colour!",
                ClientConstants.GuessesLeft + 8 + ClientConstants.PromptGuess,
                ClientConstants.Feedback,
                "Black, Black, Black, Black",
                ClientConstants.Winner
                
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
                ClientConstants.Welcome,
                ClientConstants.Border,
                ClientConstants.SecretCode,
                "Blue, Green, Yellow, Green",
                ClientConstants.Border,
                ClientConstants.GuessesLeft + 8 + ClientConstants.PromptGuess,
                ClientConstants.Feedback,
                "Black, White",
                ClientConstants.GuessesLeft + 7 + ClientConstants.PromptGuess,
                ClientConstants.Feedback,
                "White, White, White, White",
                ClientConstants.GuessesLeft + 6 + ClientConstants.PromptGuess,
                ClientConstants.Feedback,
                "",
                ClientConstants.GuessesLeft + 5 + ClientConstants.PromptGuess,
                ClientConstants.Feedback,
                "",
                ClientConstants.GuessesLeft + 4 + ClientConstants.PromptGuess,
                ClientConstants.Feedback,
                "",
                ClientConstants.GuessesLeft + 3 + ClientConstants.PromptGuess,
                ClientConstants.Feedback,
                "",
                ClientConstants.GuessesLeft + 2 + ClientConstants.PromptGuess,
                ClientConstants.Feedback,
                "",
                ClientConstants.GuessesLeft + 1 + ClientConstants.PromptGuess,
                ClientConstants.Feedback,
                "",
                ClientConstants.Loser
            };
        
            for (var i = 0; i < consoleDisplayStub.Messages.Count; i++)
            {
                Assert.Equal(expectedMessages[i], consoleDisplayStub.Messages[i]);
            }
            //Assert.True(consoleDisplay.Messages.SequenceEqual(expectedMessages)); 
        }
        
        
        [Fact]
        public void It_Should_DisplayDemo_When_UsingConsoleDemoInputCollector_GivenInput()
        {
            var config = ConfigurationLoader.LoadMastermindConfiguration(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ConfigFiles", "StandardConfig.json"));

            var staticCodeGenerate = new  StaticCodeGenerator(new List<Peg>{Peg.Blue, Peg.Green, Peg.Orange, Peg.Red}); 
            
            var feedbackRandomizer = new NonRandomizer();
            
            var codeChecker = new CodeChecker(staticCodeGenerate, feedbackRandomizer);
            
            var game = new Game(config, codeChecker);
            
            var inputHappy = new List<string>
            {
                "blue, blue, green, green",
                "Red, blue, red, RED",
                "Blue, yellow, Green, red",
                "Blue, Green, PURPLE, Red", 
                "blue, green, orange, red"
            };
            
            var consoleDisplayStub = new ConsoleDisplayStub();
            
            var gameEngine = new GameEngine(consoleDisplayStub, new ConsoleDemoInputCollector(inputHappy, consoleDisplayStub), new ConsoleInputProcessor(config), game);
            
            
           //Act
           gameEngine.PlayGame();
           
           //Assert
            var expectedMessages = new List<string>
            { 
                ClientConstants.Welcome,
                ClientConstants.Border,
                ClientConstants.SecretCode,
                "Blue, Green, Orange, Red",
                ClientConstants.Border,
                ClientConstants.GuessesLeft + 8 + ClientConstants.PromptGuess,
                inputHappy[0],
                ClientConstants.Feedback,
                "Black, White",
                ClientConstants.GuessesLeft + 7 + ClientConstants.PromptGuess,
                inputHappy[1],
                ClientConstants.Feedback,
                "Black, White",
                ClientConstants.GuessesLeft + 6 + ClientConstants.PromptGuess,
                inputHappy[2],
                ClientConstants.Feedback,
                "Black, Black, White",
                ClientConstants.GuessesLeft + 5 + ClientConstants.PromptGuess,
                inputHappy[3],
                ClientConstants.Feedback,
                "Black, Black, Black",
                ClientConstants.GuessesLeft + 4 + ClientConstants.PromptGuess,
                inputHappy[4],
                ClientConstants.Feedback,
                "Black, Black, Black, Black",
                ClientConstants.Winner
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

            public void Display<T>(List<T> list) where T : struct, IConvertible
            {
                var stringList = string.Join(", ", list.Select(x => x.ToString()));

                Messages.Add(stringList);
            }
        }
    }
}