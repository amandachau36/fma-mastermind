using System.Collections.Generic;
using Mastermind.DataAccess.Enums;

namespace Mastermind.Business.Code
{
    public interface IFeedbackRandomizer
    {
        public List<Feedback> Randomize(List<Feedback> feedback);
    }
}