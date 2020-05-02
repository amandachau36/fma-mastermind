using System.Collections.Generic;
using Mastermind.Business.Code;

namespace Mastermind.Business.Turns
{
    public class Turn
    {
        public List<Peg> Guess { get; } //TODO: make private?
        public List<FeedBack> FeedBack { get; }
        public Turn(List<Peg> guess, List<FeedBack> feedback)
        {
            Guess = guess;
            FeedBack = feedback; 
        }
        
    }
}