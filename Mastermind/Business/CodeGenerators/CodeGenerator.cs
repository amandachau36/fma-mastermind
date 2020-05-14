using System.Collections.Generic;
using Mastermind.DataAccess.Enums;

namespace Mastermind.Business.CodeGenerators
{
    public class CodeGenerator : ICodeGenerator
    {
        private readonly List<Peg> _secretCode;

        public CodeGenerator(List<Peg> secretCode) 
        {
            _secretCode = secretCode;
        }
        public List<Peg> GenerateSecretCode()
        {
            return _secretCode;
        }
    }

}