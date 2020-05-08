using System;
using System.Collections.Generic;
using System.Linq;
using Mastermind.Business.BoardGame;
using Mastermind.Client.Display;
using Mastermind.Client.InputCollector;
using Mastermind.Client.InputProcessor;
using Mastermind.DataAccess.Enums;

namespace Mastermind.Client
{
    public class GameEngine
    {
        private readonly IDisplay _display;
        private readonly ICollector _collector;
        private readonly IInputProcessor _processor;
        
        private readonly Game _game;

        private bool _collectingInputIsComplete;

        public GameEngine(IDisplay display, ICollector collector, IInputProcessor processor, Game game)  //need to inject config?  
        {
            _display = display;
            _collector = collector;
            _processor = processor;
            _game = game;
        }

        public void PlayGame()
        {
            StartGame();

            while (!_game.IsGameOver) 
            { 
                var processedInput = CollectAndProcessGuess();
                
                CheckGuess(processedInput);
                
                ProvideFeedbackToUser();

            }

            DisplayWinnerOrLoser();
           
        }

        private void StartGame()
        {
            _game.StartNewGame();
            
            _display.Display(ClientConstants.Welcome);

            _display.Display(ClientConstants.Border);
            
            _display.Display(ClientConstants.SecretCode);
            
            _display.Display(_game.GetSecretCode());

            _display.Display(ClientConstants.Border);
        }

        private List<Peg> CollectAndProcessGuess()
        {
            _collectingInputIsComplete = false;

            var processedInput = new List<Peg>();

            while (!_collectingInputIsComplete)
            {
                processedInput = TryToProcessInput();
            }

            return processedInput;
        }
        
        private List<Peg> TryToProcessInput()
        {
            var processedInput = new List<Peg>();  //TODO: is okay practice? 
            
            try
            {
                _display.Display(ClientConstants.GuessesLeft + _game.RemainingTurns + ClientConstants.PromptGuess);
                
                var input = _collector.Collect();

                processedInput = _processor.Process(input);

                _collectingInputIsComplete = true;

            }
            catch (Exception e)
            {
                _display.Display(e.Message);
            }
            
            return processedInput;
        }
        
        private void CheckGuess(List<Peg> processedInput)
        {
            _game.CheckGuess(processedInput);
        }

        private void ProvideFeedbackToUser()
        {
            _display.Display(ClientConstants.Feedback); 
                
            _display.Display(_game.Turns.Last().FeedBack);
        }

        private void DisplayWinnerOrLoser()
        {
            _display.Display(_game.IsWinner 
                ? ClientConstants.Winner 
                : ClientConstants.Loser);
        }
    }
}