using System.Collections.Generic;
using System.Linq;
using Mastermind.Business.Code;
using Mastermind.Business.Turns;
using Mastermind.DataAccess;

namespace Mastermind.Business.BoardGame
{
    public class Game
    {
        private readonly CodeChecker _codeChecker;
        
        private readonly MastermindConfiguration _config;
        public bool IsWinner { get; private set; }
        public bool IsGameOver { get; private set; }
        public List<Turn> Turns { get; private set;  } = new List<Turn>();

        public Game(MastermindConfiguration config, CodeChecker codeChecker) 
        {
            _config = config;
            _codeChecker = codeChecker;
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
            
            CheckGameStatus(feedback);
            
        }

        private void UpdateTurnHistory(List<Peg> guess, List<FeedBack> feedback)
        {
            var turn = new Turn(guess, feedback);  
            
            Turns.Add(turn);
        }
        
        private void CheckGameStatus(List<FeedBack> feedback)
        {


            if (feedback.Count == _config.CodeLength && feedback.All(x => x == FeedBack.Black) ) 
            {
                IsWinner = true;
                IsGameOver = true;
                return; 
            }

            if (Turns.Count >= _config.NumberOfTurns)
            {
                IsGameOver = true;
            }
            
        }
        
        
    }
}