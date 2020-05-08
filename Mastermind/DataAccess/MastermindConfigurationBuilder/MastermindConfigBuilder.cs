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
            MastermindConfig[DataConstants.CodeLength] = codeLength;
        }

        public void BuildNumberOfColours(int numberOfColours)
        {
            MastermindConfig[DataConstants.NumberOfColours] = numberOfColours;
        }

        public void BuildNumberOfTurns(int numberOfTurns)
        {
            MastermindConfig[DataConstants.NumberOfTurns] = numberOfTurns;
        }
        

    }
}
