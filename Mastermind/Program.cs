using System.Collections.Generic;
using Mastermind.Business.BoardGame;
using Mastermind.Business.Code;
using Mastermind.Business.CodeGenerator;
using Mastermind.Client;
using Mastermind.Client.Display;
using Mastermind.Client.InputCollector;
using Mastermind.Client.InputProcessor;
using Mastermind.DataAccess;
using Mastermind.DataAccess.Enums;

namespace Mastermind
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ConfigurationLoader.LoadMastermindConfiguration("/Users/amanda.chau/fma/Mastermind/Mastermind/Config/standardConfig.json");
            
            var randomCodeGenerator = new RandomCodeGenerator(config); 
            var feedbackRandomizer = new FeedbackRandomizer(); 
            var codeChecker = new CodeChecker(randomCodeGenerator, feedbackRandomizer);
            
            var game = new Game(config, codeChecker);
            
            var gameEngine = new GameEngine(new ConsoleDisplay(), new ConsoleInputCollector(), new ConsoleInputProcessor(config), game);
            
            gameEngine.PlayGame();
            
            
            // var config = ConfigurationLoader.LoadMastermindConfiguration("/Users/amanda.chau/fma/Mastermind/Mastermind/Config/standardConfig.json");
            //
            // var staticCodeGenerate = new  StaticCodeGenerator(new List<Peg>{Peg.Blue, Peg.Green, Peg.Orange, Peg.Red}); 
            //
            // var feedbackRandomizer = new FeedbackRandomizer(); 
            //
            // var codeChecker = new CodeChecker(staticCodeGenerate, feedbackRandomizer);
            //
            // var game = new Game(config, codeChecker);
            //
            // var inputHappy = new List<string>
            // {
            //     "blue, blue, green, green",
            //     "Red, blue, red, RED",
            //     "Blue, yellow, Green, red",
            //     "Blue, Green, PURPLE, Red", 
            //     "blue, green, orange, red"
            // };
            //
            // var inputUnhappy = new List<string>
            // {
            //     "blue, yellow, pink, orange",
            //     "",
            //     "blue, blue, blue",
            //     "green, green, orange, orange, orange", 
            //     "yellow, orange, red, rainbow", 
            //     "blue, green, orange, red"
            // };
            //
            // var consoleDisplay = new ConsoleDisplay();
            //
            // var gameEngine = new GameEngine(consoleDisplay, new ConsoleDemoInputCollector(inputUnhappy, consoleDisplay), new ConsoleInputProcessor(config), game);
            //
            // gameEngine.PlayGame();
            
        }
    }
}