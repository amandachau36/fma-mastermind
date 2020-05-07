using System.Collections.Generic;

namespace Mastermind.DataAccess.MastermindConfigurationBuilder
{
    public class MastermindConfig
    {
        private readonly Dictionary<string, int> _config = new Dictionary<string, int>();
        public int this[string key]
        {
            get => _config[key];
            set => _config[key] = value;
        }
    }
}