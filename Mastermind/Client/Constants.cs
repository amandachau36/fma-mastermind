namespace Mastermind.Client
{
    public static class Constants
    {
        public const string Welcome = "Welcome to Mastermind";

        public const string SecretCode = "Secret Code:";

        public const string PromptGuess = "\nPlease enter your guess: ";

        public const string Feedback = "Feedback:";

        public const string Winner = "\nYou win!";

        public const string Loser = "\nGame over, you lose!";

        public const string CodeLength = "Code Length";  //TODO: is there a way I can have access to the ones in Data Acesss
        
        public const string NumberOfColours = "Number of Colours";
        
        public const string NumberOfTurns = "Number of Turns";

        public static readonly string Border = new string('=', 33);
    }
}