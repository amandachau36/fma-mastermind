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
        public List<Turn> Turns { get; private set;  }

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
        
        public List<Feedback> CheckGuess(List<Peg> guess)
        {
            return _codeChecker.CheckGuess(guess);
        }

        public void UpdateGame(List<Peg> guess, List<Feedback> feedback)
        {
            UpdateTurnHistory(guess, feedback);  
            
            UpdateGameStatus(feedback);
        }
        

        public List<Peg> GetSecretCode()
        {
            return _codeChecker.SecretCode;
        }

        private void UpdateTurnHistory(List<Peg> guess, List<Feedback> feedback)
        {
            var turn = new Turn(guess, feedback);  
            
            Turns.Add(turn);
        }
        
        private void UpdateGameStatus(List<Feedback> feedback)
        {

            RemainingTurns -= 1;
            
            if (IsGuessCorrect(feedback)) 
            {
                IsWinner = true;
                IsGameOver = true;
                return; 
            }

            if (HasGameEnded())
            {
                IsGameOver = true;
            }
            
        }

        private bool IsGuessCorrect(List<Feedback> feedback)
        {
            return IsFeedbackLengthEqualToSecretCodeLength(feedback) && IsAllFeedbackBlack(feedback);
        }

        private bool IsFeedbackLengthEqualToSecretCodeLength(List<Feedback> feedback)
        {
            return feedback.Count == _config[DataConstants.CodeLength];
        }
        
        private bool IsAllFeedbackBlack(List<Feedback> feedback)
        {
            return feedback.All(x => x == Feedback.Black);
        }
        
        private bool HasGameEnded()
        {
            return RemainingTurns <= 0;
        }
        
    }
}