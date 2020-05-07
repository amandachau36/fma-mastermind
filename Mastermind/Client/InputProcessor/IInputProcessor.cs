using System.Collections.Generic;
using Mastermind.DataAccess.Enums;

namespace Mastermind.Client.InputProcessor
{
    public interface IInputProcessor
    {
        List<Peg> Process(string input);
    }
}