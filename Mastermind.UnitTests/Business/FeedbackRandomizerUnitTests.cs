using System;
using System.Collections.Generic;
using Mastermind.Business.Code;
using Mastermind.DataAccess.Enums;
using Xunit;

namespace Mastermind.UnitTests.Business
{
    public class FeedbackRandomizerUnitTests
    {
        [Fact]
        public void It_Should_Return_A_SecretCodeOfColouredPegs()
        {
            //arrange
            var feedbackRandomizer = new FeedbackRandomizer();
            var feedback = new List<FeedBack>
            {
                FeedBack.Black,
                FeedBack.Black,
                FeedBack.Black,
                FeedBack.Black,
                FeedBack.Black,
                FeedBack.White,
                FeedBack.White,
                FeedBack.White,
                FeedBack.White,
                FeedBack.White,
            };
            
            //act
            var randomizeFeedback = feedbackRandomizer.Randomize(feedback);
        
            //assert
            Assert.NotEqual(feedback, randomizeFeedback );
            
        }
    }
}