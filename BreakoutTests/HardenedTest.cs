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
using Breakout.Balls;



namespace BreakoutTest
{

    // Setting up the test fixture for the HardenedBlock class tests
    [TestFixture]
    public class TestsHardened
    {
        public Block block; // Declaring a public Block object
        public Ball ball; // Declaring a public Ball object
        public int initialHealth = 3; // Initial health for the hardened block

        // Method to set up any required objects or state before all tests are run
        [OneTimeSetUp]
        public void Setup()
        {
            var windowArgs = new WindowArgs()
            {
                Title = "Galaga v0.1"
            };

            var game = new Game(windowArgs, false); // Creating a new game instance

            // Initializing the hardened block with a specific shape, image, and health
            block = new HardenedBlock(new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                "blue-block.png", initialHealth, 3);

            // Initializing the ball with a specific position and image
            ball = new Ball(new Vec2F(0.5f - 0.2f / 2, 0.1f), new Image(Path.Combine("Assets", "Images", "ball.png")));
        }

        // Method to set up any required objects or state before each test
        [SetUp]
        public void Setup2()
        {
            var windowArgs = new WindowArgs()
            {
                Title = "Galaga v0.1"
            };

            var game = new Game(windowArgs, false); // Creating a new game instance

            // Initializing the hardened block with a specific shape, image, and health
            block = new HardenedBlock(new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                "blue-block.png", 3, 3);

            // Initializing the ball with a specific position and image
            ball = new Ball(new Vec2F(0.5f - 0.2f / 2, 0.1f), new Image(Path.Combine("Assets", "Images", "ball.png")));
        }

        // Test method to check if the block's health is double the initial health
        [Test]
        public void isDoubleHealth()
        {
            Assert.AreEqual(2 * initialHealth, block.Health); // Asserting that the block's health is double the initial health
        }

        // Test method to check if the block's image changes after being hit three times
        [Test]
        public void changesImageWhenHit()
        {
            IBaseImage oldImage = block.Image; // Storing the old image of the block
            block.Hit(ball); // Hitting the block with the ball
            block.Hit(ball); // Hitting the block with the ball again
            block.Hit(ball); // Hitting the block with the ball for the third time
            bool isDead = block.Hit(ball); // Checking if the block is dead after three hits
            IBaseImage newImage = block.Image; // Storing the new image of the block

            Assert.AreEqual(false, isDead); // Asserting that the block is not dead
            Assert.AreEqual(oldImage, newImage); // Asserting that the block's image has not changed
        }
    }
}
