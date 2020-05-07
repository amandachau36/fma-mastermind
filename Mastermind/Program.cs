
using Mastermind.Business.BoardGame;
using Mastermind.Business.Code;
using Mastermind.Business.CodeGenerator;
using Mastermind.Client;
using Mastermind.Client.Display;
using Mastermind.Client.InputCollector;
using Mastermind.Client.InputProcessor;
using Mastermind.DataAccess;

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
            game.StartNewGame();
            
            var gameEngine = new GameEngine(new ConsoleDisplay(), new ConsoleInputCollector(), new ConsoleInputProcessor(config), game);
            
            gameEngine.PlayGame();
            
        }
    }
}