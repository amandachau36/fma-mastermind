using System;
using System.Collections.Generic;
using Mastermind.Business.Code;
using Mastermind.DataAccess.Enums;
using Xunit;

namespace Mastermind.Tests.UnitTests.Business
{
    public class FeedbackRandomizerUnitTests
    {
        [Fact]
        public void It_Should_Return_RandomizeFeedback_When_Given_Feedback()
        {
            //arrange
            var feedbackRandomizer = new FeedbackRandomizer();
            var feedback = new List<Feedback>
            {
                Feedback.Black,
                Feedback.Black,
                Feedback.Black,
                Feedback.Black,
                Feedback.Black,
                Feedback.White,
                Feedback.White,
                Feedback.White,
                Feedback.White,
                Feedback.White,
            };
            
            //act
            var randomizeFeedback = feedbackRandomizer.Randomize(feedback);
        
            //assert
            Assert.NotEqual(feedback, randomizeFeedback );
            
        }
    }
}