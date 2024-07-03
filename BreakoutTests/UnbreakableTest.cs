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

    // Setting up the test fixture for the UnbreakableBlock class tests
    [TestFixture]
    public class TestsUnbreakable
    {
        public Block block; // Declaring a public Block object
        public Ball ball; // Declaring a public Ball object

        // Method to set up any required objects or state before all tests are run
        [OneTimeSetUp]
        public void Setup()
        {
            var windowArgs = new WindowArgs()
            {
                Title = "Galaga v0.1"
            };

            var game = new Game(windowArgs, false); // Creating a new game instance

            // Initializing the ball with a specific position and image
            ball = new Ball(new Vec2F(0.5f - 0.2f / 2, 0.1f), new Image(Path.Combine("Assets", "Images", "ball.png")));

            // Initializing the unbreakable block with a specific shape, image, and health
            block = new UnbreakableBlock(new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                "blue-block.png", 3, 3);
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

            // Initializing the ball with a specific position and image
            ball = new Ball(new Vec2F(0.5f - 0.2f / 2, 0.1f), new Image(Path.Combine("Assets", "Images", "ball.png")));

            // Initializing the unbreakable block with a specific shape, image, and health
            block = new UnbreakableBlock(new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                "blue-block.png", 3, 3);
        }

        // Test method to check the hit function of the unbreakable block
        [Test]
        public void test1()
        {
            int health = block.Health; // Storing the initial health of the block
            block.Hit(ball); // Hitting the block with the ball
            bool isDead = block.Hit(ball); // Hitting the block with the ball again and checking if it is dead
            Assert.IsFalse(isDead); // Asserting that the block is not dead
            Assert.AreEqual(int.MaxValue, block.Health); // Asserting that the block's health is still maximum (unbreakable)
        }
    }
}
