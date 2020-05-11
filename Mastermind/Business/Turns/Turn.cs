using System.Collections.Generic;
using Mastermind.Business.Code;
using Mastermind.DataAccess.Enums;

namespace Mastermind.Business.Turns
{
    public class Turn
    {
        public List<Peg> Guess { get; } 
        public List<Feedback> FeedBack { get; }
        public Turn(List<Peg> guess, List<Feedback> feedback)
        {
            Guess = guess;
            FeedBack = feedback; 
        }
        
    }
}