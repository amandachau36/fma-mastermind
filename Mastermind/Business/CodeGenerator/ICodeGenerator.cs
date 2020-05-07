using System.Collections.Generic;
using Mastermind.DataAccess.Enums;

namespace Mastermind.Business.CodeGenerator
{
    public interface ICodeGenerator
    {
        List<Peg> GenerateSecretCode();
        
    }
}