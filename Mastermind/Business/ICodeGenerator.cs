using System.Collections.Generic;

namespace Mastermind.Business
{
    public interface ICodeGenerator
    {
        List<Peg> GenerateSecretCode();
        
    }
}