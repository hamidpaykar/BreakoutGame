using Breakout.States;

using System.Collections.Generic;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Graphics;
using System;
using DIKUArcade.Math;
using System.IO;
using DIKUArcade.Physics;
using Breakout.Levels;

namespace Breakout
{
    /// <summary>
    /// Responsible for holding together all subparts of breakout 
    /// </summary>
    public class Game : DIKUGame, IGameEventProcessor
    {
        private StateMachine stateMachine; // State machine to manage game states
        private Player player; // Player object

        // Constructor for the Game class
        public Game(WindowArgs windowArgs) : base(windowArgs)
        {
            LevelHolder.loadLevels(); // Load all levels
            stateMachine = new StateMachine(); // Initialize the state machine
            // Initialize the event bus with specific game event types
            BreakoutBus.GetBus().InitializeEventBus(new List<GameEventType> {
                GameEventType.InputEvent,
                GameEventType.PlayerEvent,
                GameEventType.GameStateEvent
            });
            // Subscribe the game and state machine to the event bus
            BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateMachine);
            window.SetKeyEventHandler(KeyHandler); // Set the key event handler
        }

        /// <summary>
        /// Instantiates the game in its entirety.
        /// </summary>
        public Game(WindowArgs windowArgs, bool initEventBus) : base(windowArgs)
        {
            stateMachine = new StateMachine(); // Initialize the state machine
            if (initEventBus)
            {
                // Initialize the event bus with specific game event types
                BreakoutBus.GetBus().InitializeEventBus(new List<GameEventType> {
                    GameEventType.InputEvent,
                    GameEventType.PlayerEvent,
                    GameEventType.GameStateEvent
                });
            }
            // Subscribe the game and state machine to the event bus
            BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateMachine);
            window.SetKeyEventHandler(KeyHandler); // Set the key event handler
        }

        // Method to handle key events
        private void KeyHandler(KeyboardAction action, KeyboardKey key)
        {
            stateMachine.ActiveState.HandleKeyEvent(action, key); // Delegate key events to the active state
        }

        // Override of the Update method to update game state
        public override void Update()
        {
            stateMachine.ActiveState.UpdateState(); // Update the active state
            BreakoutBus.GetBus().ProcessEventsSequentially(); // Process events sequentially
        }

        // Override of the Render method to render game state
        public override void Render()
        {
            stateMachine.ActiveState.RenderState(); // Render the active state
        }

        // Implementation of the ProcessEvent method from IGameEventProcessor
        public void ProcessEvent(GameEvent gameEvent)
        {
            if (gameEvent.Message == "CLOSE_WINDOW")
            {
                window.CloseWindow(); // Close the window if the message is to close it
            }
            stateMachine.ProcessEvent(gameEvent); // Process the event in the state machine
        }
    }
}
