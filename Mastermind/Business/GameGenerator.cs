using Mastermind.Business.BoardGame;
using Mastermind.Business.Code;
using Mastermind.DataAccess;

namespace Mastermind.Business
{
    public static class GameGenerator //TODO: Static or non static 
    {
        //private static MastermindConfiguration _mastermindConfiguration;

        public static MastermindConfiguration GetConfiguration(string path)
        {
            return ConfigurationLoader.LoadMastermindConfiguration(path); //tightly coupled? is this necessary
        }

        // public static ICodeGenerator GenerateRandomCodeGenerator()
        // {
        //     return new RandomCodeGenerator(_mastermindConfiguration); //TODO: but then this is tightly coupled 
        // }
        
        public static Game GenerateGame(MastermindConfiguration mastermindConfiguration, CodeChecker codeChecker) //TODO: should this be the path or the config file? //TODO: should there be two separate config files????? 
        {
            var game = new Game(mastermindConfiguration, codeChecker);
            
            game.StartNewGame();

            return game;
        }
    }
}