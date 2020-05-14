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

        public GameEngine(IDisplay display, ICollector collector, IInputProcessor processor, Game game) 
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

                var feedback = _game.CheckGuess(processedInput);
                
                _game.UpdateGame(processedInput,feedback);
                
                ProvideFeedbackToUser(feedback); 
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
            var processedInput = new List<Peg>(); 
            
            try
            {
                _display.Display(ClientConstants.GuessesLeft + _game.RemainingTurns + ClientConstants.PromptGuess);
                
                var input = _collector.Collect();

                processedInput = _processor.Process(input);

                _collectingInputIsComplete = true;
            }
            catch (Exception e)
            {
                _display.DisplayError(e.Message);
            }
            
            return processedInput;
        }
        
        private void ProvideFeedbackToUser(List<Feedback> feedback)
        {
            _display.Display(ClientConstants.Feedback); 
                
            _display.Display(feedback);
            
            _display.Display(ClientConstants.Border);
        }

        private void DisplayWinnerOrLoser()
        {
            _display.DisplayResult(_game.IsWinner 
                ? ClientConstants.Winner 
                : ClientConstants.Loser);
        }
    }
}