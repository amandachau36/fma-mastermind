using System.Collections.Generic;
using System.Linq;
using Mastermind.Business.Code;
using Mastermind.Business.Turns;

namespace Mastermind.Business.BoardGame
{
    public class Board
    {
        private readonly CodeChecker _codeChecker;
        public bool IsWinner { get; private set; }
        public bool IsGameOver { get; private set; }
        
        public int maxNumberOfTurns = 8;
        public List<Turn> Turns { get; } = new List<Turn>();

        public Board(CodeChecker codeChecker) //TODO: should board just be game? 
        {
            _codeChecker = codeChecker;
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

            if (feedback.Count == 4 && feedback.All(x => x == FeedBack.Black) ) //TODO no magic numbers / use config
            {
                IsWinner = true;
                IsGameOver = true;
                return; 
            }

            if (Turns.Count >= maxNumberOfTurns)
            {
                IsGameOver = true;
            }
            
        }
        
        
    }
}