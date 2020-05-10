using System.Collections.Generic;
using Mastermind.DataAccess.Enums;

namespace Mastermind.Business.CodeGenerator
{
    public class StaticCodeGenerator : ICodeGenerator
    {
        private readonly List<Peg> _secretCode;

        public StaticCodeGenerator(List<Peg> secretCode)
        {
            _secretCode = secretCode;
        }
        public List<Peg> GenerateSecretCode()
        {
            return _secretCode;
        }
    }

}