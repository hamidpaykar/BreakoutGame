using NUnit.Framework;
using System;
using Breakout;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.GUI;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Events;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.Collections.Generic;
using System;
using System.IO;
using DIKUArcade.Physics;
using Breakout.Blocks;
using Breakout.Scores;
using Breakout.Levels;


namespace BreakoutTest
{

    // Setting up the test fixture for the Levels class tests
    [TestFixture]
    public class TestLevels
    {

        // Method to set up any required objects or state before each test
        [SetUp]
        public void Setup()
        {
            // This method can be used to initialize objects or settings before each test runs
        }

        // Test method to check if the total number of levels is correct
        [Test]
        public void totalLevelsIsCorrect()
        {
            Assert.AreEqual(6, LevelHolder.TotalLevels); // Asserting that the total number of levels is 6
        }

        // Test method to check if the count of levels in the Levels collection is correct
        [Test]
        public void levelsCountIsCorrect()
        {
            Assert.AreEqual(6, LevelHolder.Levels.Count); // Asserting that the number of levels in the Levels collection is 6
        }
    }
}
