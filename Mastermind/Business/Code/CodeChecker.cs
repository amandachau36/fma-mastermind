using System.Collections.Generic;
using System.Linq;
using Mastermind.Business.CodeGenerator;
using Mastermind.Business.Turns;

namespace Mastermind.Business.Code
{
    public class CodeChecker
    {
        private readonly ICodeGenerator _codeGenerator;
        public List<Peg> SecretCode { get; private set; } = new List<Peg>();

        public CodeChecker(ICodeGenerator codeGenerator)
        {
            _codeGenerator = codeGenerator;
        }

        public void GenerateSecretCode()
        {
            SecretCode = _codeGenerator.GenerateSecretCode();
        }
        
        public List<FeedBack> CheckGuess(List<Peg> guess)
        {
            var localGuess = new List<Peg>(guess); //TODO: is this the best way to clone things 
            
            var secretCode = new List<Peg>(SecretCode);

            var feedback = CheckForCorrectlyPositionedColours(localGuess, secretCode);

            var whiteFeedback = CheckForCorrectColoursAtIncorrectPositions(localGuess, secretCode);

            feedback.AddRange(whiteFeedback);
            
            return feedback; //TODO: randomize 
        }

        private List<FeedBack> CheckForCorrectlyPositionedColours(List<Peg> guess, List<Peg> secretCode)
        {
            var feedback = new List<FeedBack>();
            
            for (var i = guess.Count -1; i >= 0; i--)
            {
                if (guess[i] != secretCode[i]) continue;
                
                feedback.Add(FeedBack.Black);
                guess.RemoveAt(i);
                secretCode.RemoveAt(i);
            }

            return feedback;
        }

        private List<FeedBack> CheckForCorrectColoursAtIncorrectPositions(List<Peg> guess, List<Peg> secretCode)
        {
            var feedback = new List<FeedBack>();
            
            foreach (var peg in guess.Where(secretCode.Contains))
            {
                feedback.Add(FeedBack.White);
                secretCode.Remove(peg);
            }
            
            return feedback;

        }
        
    }
}