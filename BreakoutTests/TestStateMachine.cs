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
using Breakout.States;
using DIKUArcade.State;
using DIKUArcade.Input;
using NUnit.Framework;
using System;
using Breakout;
using Breakout.States;


namespace BreakoutTests
{
    // Setting up the test fixture for the StateMachine class tests
    [TestFixture]
    public class StateMachineTesting
    {
        private StateMachine stateMachine; // Declaring a private StateMachine object

        // Method to initialize the StateMachine before each test
        [SetUp]
        public void InitiateStateMachine()
        {
            DIKUArcade.GUI.Window.CreateOpenGLContext(); // Creating an OpenGL context for the window

            /*
            Here you should:
            (1) Initialize a BreakoutBus with proper GameEventTypes
            (2) Instantiate the StateMachine
            (3) Subscribe the BreakoutBus to proper GameEventTypes
            and GameEventProcessors
            */

            // Initialize the BreakoutBus with the required GameEventTypes
            // BreakoutBus.GetBus().InitializeEventBus(new List<GameEventType> {
            //     GameEventType.InputEvent, 
            //     GameEventType.PlayerEvent, 
            //     GameEventType.GameStateEvent
            // });

            stateMachine = new StateMachine(); // Instantiating the StateMachine
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateMachine); // Subscribing the stateMachine to GameStateEvent
        }

        // Test method to check if the initial state is MainMenu
        [Test]
        public void TestInitialState()
        {
            Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>()); // Asserting that the initial state is MainMenu
        }

        // Test method to check if the state changes to GamePaused on receiving the appropriate event
        [Test]
        public void TestEventGamePaused()
        {
            BreakoutBus.GetBus().RegisterEvent(
                new GameEvent
                {
                    EventType = GameEventType.GameStateEvent,
                    Message = "CHANGE_STATE",
                    StringArg1 = "GAME_PAUSED"
                }
            );
            BreakoutBus.GetBus().ProcessEventsSequentially(); // Processing events sequentially
            Assert.That(stateMachine.ActiveState, Is.InstanceOf<GamePaused>()); // Asserting that the state has changed to GamePaused
        }

        // Test method to check if the state changes to GameRunning on receiving the appropriate event
        [Test]
        public void TestEventGameRunning()
        {
            BreakoutBus.GetBus().RegisterEvent(
                new GameEvent
                {
                    EventType = GameEventType.GameStateEvent,
                    Message = "CHANGE_STATE",
                    StringArg1 = "GAME_RUNNING",
                    StringArg2 = "1"
                }
            );
            BreakoutBus.GetBus().ProcessEventsSequentially(); // Processing events sequentially
            Assert.That(stateMachine.ActiveState, Is.InstanceOf<GameRunning>()); // Asserting that the state has changed to GameRunning
        }

        // Test method to check if the state changes to GameOver on receiving the appropriate event
        [Test]
        public void TestGameOver()
        {
            BreakoutBus.GetBus().RegisterEvent(
                new GameEvent
                {
                    EventType = GameEventType.GameStateEvent,
                    Message = "CHANGE_STATE",
                    StringArg1 = "GAME_OVER"
                }
            );
            BreakoutBus.GetBus().ProcessEventsSequentially(); // Processing events sequentially
            Assert.That(stateMachine.ActiveState, Is.InstanceOf<GameOver>()); // Asserting that the state has changed to GameOver
        }

        // Test method to check if the state changes to MainMenu on receiving the appropriate event
        [Test]
        public void TestEventMainMenu()
        {
            BreakoutBus.GetBus().RegisterEvent(
                new GameEvent
                {
                    EventType = GameEventType.GameStateEvent,
                    Message = "CHANGE_STATE",
                    StringArg1 = "MAIN_MENU"
                }
            );
            BreakoutBus.GetBus().ProcessEventsSequentially(); // Processing events sequentially
            Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>()); // Asserting that the state has changed to MainMenu
        }
    }
}
