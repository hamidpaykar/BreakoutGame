
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

namespace BreakoutTests
{

    // Setting up the test fixture for the Player class tests
    public class TestsPlayer
    {
        public Player player; // Declaring a public Player object

        // Method to set up any required objects or state before each test
        [SetUp]
        public void Setup()
        {
            var windowArgs = new WindowArgs()
            {
                Title = "Breakout v0.1"
            };

            var game = new Game(windowArgs, false); // Creating a new game instance

            // Initializing the player with a specific position and image
            player = new Player(
                new DynamicShape(new Vec2F(0.1f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "player.png")));
        }

        // Test method to check the player's movement
        [Test]
        public void test1()
        {
            // Store the initial position of the player
            Vec2F pos1 = player.GetPosition();

            // Move the player to the left
            player.SetMoveLeft(true);
            player.Move();
            Vec2F pos2 = player.GetPosition();

            // Stop moving the player
            player.SetMoveLeft(false);
            player.Move();
            Vec2F pos3 = player.GetPosition();

            // Move the player to the right
            player.SetMoveRight(true);
            player.Move();
            Vec2F pos4 = player.GetPosition();

            // Stop moving the player
            player.SetMoveRight(false);
            player.Move();
            Vec2F pos5 = player.GetPosition();

            // First test: Check if setting move left to true moves the player to the left
            // Second test: Check if setting move left to false stops the player from moving
            // Third test: Same as first, but with move right
            // Fourth test: Same as second, but with move right
            // Fifth test: Check if the y-coordinate stays the same

            // These tests also check the direction, as DynamicShape's move function uses the direction to move.
            // So if the direction function works, the positions should be correct.
            Assert.True(pos2.X < pos1.X & pos3.X == pos2.X & pos3.X < pos4.X & pos4.X == pos5.X & pos1.Y == pos5.Y);
        }
    }
}
