namespace Mastermind.Client
{
    public static class ClientConstants
    {
        public const string Welcome = "Welcome to Mastermind";

        public const string SecretCode = "Secret Code:";

        public const string PromptGuess = ". Please enter your guess: ";

        public const string GuessesLeft = "\nGuesses left: ";

        public const string Feedback = "Feedback:";

        public const string Winner = "\nYou win!";

        public const string Loser = "\nGame over, you lose!";

        public static readonly string Border = new string('=', 33);
    }
}