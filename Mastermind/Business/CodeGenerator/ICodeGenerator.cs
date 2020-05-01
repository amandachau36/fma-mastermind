using System.Collections.Generic;
using Mastermind.Business.Code;

namespace Mastermind.Business.CodeGenerator
{
    public interface ICodeGenerator
    {
        List<Peg> GenerateSecretCode();
        
    }
}