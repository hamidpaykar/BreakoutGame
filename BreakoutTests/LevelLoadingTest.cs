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
using Breakout.LoadLevel;


namespace BreakoutTest
{

    // Setting up the test fixture for the LevelLoader class tests
    public class TestsLevelLoader
    {

        // Method to set up any required objects or state before each test
        [SetUp]
        public void Setup()
        {
            var windowArgs = new WindowArgs()
            {
                Title = "Breakout Test v0.1"
            };

            var game = new Game(windowArgs, false); // Creating a new game instance
        }

        // Test method to check if a level is loaded correctly with the given file name
        [Test]
        public void loadsLevel()
        {
            Level level = new Level("d", "level3.txt"); // Loading level3.txt
            Assert.AreEqual("level3.txt", level.FileName); // Asserting that the file name is correctly set
        }

        // Test method to check if a non-existent level file is not loaded
        [Test]
        public void DoesNotLoadLevel()
        {
            Level level = new Level("d", "devel3.txt"); // Attempting to load a non-existent level file
            Assert.AreEqual("", level.FileName); // Asserting that the file name is empty since the file does not exist
        }

        // Test method to check if the correct amount of blocks are loaded from the level file
        [Test]
        public void CorrectAmountOfBlocks()
        {
            Level level = new Level("d", "central-mass.txt"); // Loading central-mass.txt
            int amount = 0;

            // Iterating through the blocks in the level and counting them
            level.blocks.Iterate((Block block) =>
            {
                amount++;
            });

            // Asserting that the number of blocks is 72
            Assert.AreEqual(72, amount);
        }
    }
}
