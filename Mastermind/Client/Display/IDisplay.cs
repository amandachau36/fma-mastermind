using System.Collections.Generic;
using Mastermind.DataAccess.Enums;

namespace Mastermind.Client.Display
{
    public interface IDisplay
    {
        void Display(string message);

        void Display<T>(List<T> list);
    }
}