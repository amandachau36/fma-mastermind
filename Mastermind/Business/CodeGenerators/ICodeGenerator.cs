using System.Collections.Generic;
using Mastermind.DataAccess.Enums;

namespace Mastermind.Business.CodeGenerators
{
    public interface ICodeGenerator
    {
        List<Peg> GenerateSecretCode();
        
    }
}