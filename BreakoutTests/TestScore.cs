using NUnit.Framework; 
using System; 
using Breakout; 
using DIKUArcade.Entities; 
using DIKUArcade.Graphics; 
using DIKUArcade.Math; 
using DIKUArcade.GUI; 
using DIKUArcade; 
using DIKUArcade.Input; 
using DIKUArcade.Events; 
using System.Collections.Generic; 
using System.IO; 
using DIKUArcade.Physics; 
using Breakout.Blocks; 
using Breakout.Scores; 

namespace BreakoutTest
{
    // Setting up the test fixture for the Score class tests
    [TestFixture]
    public class TestScore
    {
        // Method to set up any required objects or state before each test
        [SetUp]
        public void Setup()
        {
            // This method can be used to initialize objects or settings before each test runs
        }

        // Test method to verify that points are correctly added to the score
        [Test]
        public void pointsAreAdded()
        {
            int scoreBefore = Score.GetScore(); // Get the current score before adding points
            int pointsAdded = 1; // Define the number of points to add
            Score.increaseScore(pointsAdded); // Increase the score by the defined points
            // Check if the score after adding points is equal to the score before plus the points added
            Assert.AreEqual(scoreBefore + pointsAdded, Score.GetScore());
        }

        // Test method to verify that the score reset functionality works correctly
        [Test]
        public void resetWorks()
        {
            Assert.AreEqual(1, Score.GetScore()); // Check if the current score is 1
            Score.reset(); // Reset the score to 0
            Assert.AreEqual(0, Score.GetScore()); // Check if the score is now 0 after reset
        }
    }
}
