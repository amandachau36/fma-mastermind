namespace Mastermind.DataAccess
{
    public class MastermindConfiguration
    {
        public int CodeLength { get;}
        public int NumberOfColours { get; }
        public int NumberOfTurns { get;}
        public MastermindConfiguration(int codeLength, int numberOfColours, int numberOfTurns)
        {
            CodeLength = codeLength;
            NumberOfColours = numberOfColours;
            NumberOfTurns = numberOfTurns;
        }
    }
}