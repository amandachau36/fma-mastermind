namespace Mastermind.DataAccess.MastermindConfigurationBuilder
{
    public class MastermindConfigBuilder 
    {
        public MastermindConfig MastermindConfig { get; }

        public MastermindConfigBuilder()
        {
            MastermindConfig = new MastermindConfig();
        }

        public void BuildCodeLength(int codeLength)
        {
            MastermindConfig[Constants.CodeLength] = codeLength;
        }

        public void BuildNumberOfColours(int numberOfColours)
        {
            MastermindConfig[Constants.NumberOfColours] = numberOfColours;
        }

        public void BuildNumberOfTurns(int numberOfTurns)
        {
            MastermindConfig[Constants.NumberOfTurns] = numberOfTurns;
        }
        

    }
}
