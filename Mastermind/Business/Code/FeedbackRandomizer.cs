using System;
using System.Collections.Generic;
using System.Linq;
using Mastermind.DataAccess.Enums;

namespace Mastermind.Business.Code
{
    public class FeedbackRandomizer : IFeedbackRandomizer
    {
        public List<Feedback> Randomize(List<Feedback> feedback)
        {
            var random = new Random();
            
            return feedback.OrderBy(a => random.Next()).ToList();
            
        }
    }
}