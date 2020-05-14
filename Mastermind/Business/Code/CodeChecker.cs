using System.Collections.Generic;
using System.Linq;
using Mastermind.Business.CodeGenerators;
using Mastermind.DataAccess.Enums;

namespace Mastermind.Business.Code
{
    public class CodeChecker
    {
        private readonly ICodeGenerator _codeGenerator;
        
        private readonly IFeedbackRandomizer _feedbackRandomizer;
        public List<Peg> SecretCode { get; private set; } = new List<Peg>();

        public CodeChecker(ICodeGenerator codeGenerator, IFeedbackRandomizer feedbackRandomizer)
        {
            _codeGenerator = codeGenerator;
            _feedbackRandomizer = feedbackRandomizer;
        }

        public void GenerateSecretCode()
        {
            SecretCode = _codeGenerator.GenerateSecretCode();
        }
        
        public List<Feedback> CheckGuess(List<Peg> guess)
        {
            var correctlyPositionedColours = GetCorrectlyPositionedColours(guess);
        
            var blackFeedback = GetBlackFeedback(correctlyPositionedColours);

            var whiteFeedback = GetCorrectColoursAtIncorrectPositions(guess, correctlyPositionedColours);
            
            var allFeedback = blackFeedback.Concat(whiteFeedback).ToList();
                 
            return Randomize(allFeedback);
        }
        
        private List<Peg> GetCorrectlyPositionedColours(List<Peg> guess)
        {
            return guess.Where((colour, index) => colour == SecretCode[index]).ToList();
        }
        
        private List<Feedback> GetBlackFeedback(List<Peg> correctlyPositionedColours)
        {
            return correctlyPositionedColours.Select(p => Feedback.Black).ToList();
        }
        
        private List<Feedback> GetCorrectColoursAtIncorrectPositions(List<Peg> guess, List<Peg> correctlyPositionColours)
        {
            var remainingGuessColours = GetRemainingColours(guess, correctlyPositionColours);

            var remainingSecretCodeColours = GetRemainingColours(SecretCode, correctlyPositionColours);
            
            var whiteFeedback = new List<Feedback>();
                
            foreach (var peg in remainingGuessColours.Where(remainingSecretCodeColours.Contains))
            {
                whiteFeedback.Add(Feedback.White);
                remainingSecretCodeColours.Remove(peg);
            }
            
            return whiteFeedback;
        }

        private List<Peg> GetRemainingColours(List<Peg> colouredPegs, List<Peg> correctlyPositionColours)
        {
            var remainingColours = colouredPegs.ToList();

            foreach (var colour in correctlyPositionColours)
            {
                remainingColours.Remove(colour);
            }

            return remainingColours;
        }
        
        
        private List<Feedback> Randomize(List<Feedback> feedback)
        {
            return _feedbackRandomizer.Randomize(feedback); 
        }
        
        
    }
}