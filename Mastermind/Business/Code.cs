using System.Collections.Generic;
using System.Linq;

namespace Mastermind.Business
{
    public class Code
    {
        private readonly ICodeGenerator _codeGenerator;
        public List<Peg> SecretCode { get; private set; } = new List<Peg>();

        public Code(ICodeGenerator codeGenerator)
        {
            _codeGenerator = codeGenerator;
        }

        public void GenerateSecretCode()
        {
            SecretCode = _codeGenerator.GenerateSecretCode();
        }
        
        public List<FeedBack> CheckGuess(List<Peg> guess)
        {
            
            var secretCode = SecretCode;

            var feedback = CheckForCorrectlyPositionedColours(guess, secretCode);

            var whiteFeedback = CheckForCorrectColoursAtIncorrectPositions(guess, secretCode);

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