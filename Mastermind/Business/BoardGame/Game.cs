using System.Collections.Generic;
using System.Linq;
using Mastermind.Business.Code;
using Mastermind.Business.Turns;
using Mastermind.DataAccess;
using Mastermind.DataAccess.Enums;
using Mastermind.DataAccess.MastermindConfigurationBuilder;


namespace Mastermind.Business.BoardGame
{
    public class Game
    {
        private readonly CodeChecker _codeChecker;
        
        private readonly MastermindConfig _config;
        public bool IsWinner { get; private set; }
        public bool IsGameOver { get; private set; }

        public int RemainingTurns { get; private set; }
        public List<Turn> Turns { get; private set;  } = new List<Turn>();

        public Game(MastermindConfig config, CodeChecker codeChecker) 
        {
            _config = config;
            _codeChecker = codeChecker;
            RemainingTurns = config[DataConstants.NumberOfTurns];
        }

        public void StartNewGame()
        {
            _codeChecker.GenerateSecretCode();
            IsWinner = false;
            IsGameOver = false;
            Turns = new List<Turn>();
        }
        
        public void CheckGuess(List<Peg> guess)
        {
            var feedback = _codeChecker.CheckGuess(guess);
            
            UpdateTurnHistory(guess, feedback);
            
            UpdateGameStatus(feedback);
        }

        public List<Peg> GetSecretCode()
        {
            return _codeChecker.SecretCode;
        }

        private void UpdateTurnHistory(List<Peg> guess, List<FeedBack> feedback)
        {
            var turn = new Turn(guess, feedback);  
            
            Turns.Add(turn);
        }
        
        private void UpdateGameStatus(List<FeedBack> feedback)
        {

            RemainingTurns -= 1;
            
            if (IsGuessCorrect(feedback)) 
            {
                IsWinner = true;
                IsGameOver = true;
                return; 
            }

            if (IsNoTurnsRemaining())
            {
                IsGameOver = true;
            }
            
        }

        private bool IsGuessCorrect(List<FeedBack> feedback)
        {
            return feedback.Count == _config[DataConstants.CodeLength] && feedback.All(x => x == FeedBack.Black);
        }

        private bool IsNoTurnsRemaining()
        {
            return RemainingTurns <= 0;
        }
        
        
        
    }
}