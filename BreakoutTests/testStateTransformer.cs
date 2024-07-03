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
using DIKUArcade.State;
using Breakout.States;

namespace BreakoutTest
{

    // Setting up the test fixture for the StateTransformer class tests
    public class TestStateTransformer
    {

        // Method to set up any required objects or state before each test
        [SetUp]
        public void Setup()
        {
            // This method can be used to initialize objects or settings before each test runs
        }

        // Test method to check if the string "GAME_RUNNING" is correctly transformed to GameStateType.GameRunning
        [Test]
        public void TestStringToGameRunning()
        {
            Assert.AreEqual(GameStateType.GameRunning, StateTransformer.TransformStringToState("GAME_RUNNING"));
        }

        // Test method to check if GameStateType.GameRunning is correctly transformed to the string "GAME_RUNNING"
        [Test]
        public void TestGameRunningToString()
        {
            Assert.AreEqual(StateTransformer.TransformStateToString(GameStateType.GameRunning), "GAME_RUNNING");
        }

        // Test method to check if the string "GAME_PAUSED" is correctly transformed to GameStateType.GamePaused
        [Test]
        public void TestStringToGamePaused()
        {
            Assert.AreEqual(StateTransformer.TransformStringToState("GAME_PAUSED"), GameStateType.GamePaused);
        }

        // Test method to check if GameStateType.GamePaused is correctly transformed to the string "GAME_PAUSED"
        [Test]
        public void TestGamePausedToString()
        {
            Assert.AreEqual(StateTransformer.TransformStateToString(GameStateType.GamePaused), "GAME_PAUSED");
        }

        // Test method to check if the string "MAIN_MENU" is correctly transformed to GameStateType.MainMenu
        [Test]
        public void TestStringToMainMenu()
        {
            Assert.AreEqual(StateTransformer.TransformStringToState("MAIN_MENU"), GameStateType.MainMenu);
        }

        // Test method to check if GameStateType.MainMenu is correctly transformed to the string "MAIN_MENU"
        [Test]
        public void TestMainMenuToString()
        {
            Assert.AreEqual(StateTransformer.TransformStateToString(GameStateType.MainMenu), "MAIN_MENU");
        }
    }
}
