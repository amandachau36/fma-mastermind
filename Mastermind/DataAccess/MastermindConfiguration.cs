namespace Mastermind.DataAccess
{
    public class MastermindConfiguration
    {
        public int CodeLength { get;}
        public int NumberOfColours { get; }
        public MastermindConfiguration(int codeLength, int numberOfColours)
        {
            CodeLength = codeLength;
            NumberOfColours = numberOfColours;
        }
    }
}