using DIKUArcade.Events;
using DIKUArcade.State;
using DIKUArcade.Input;
using System;

namespace Breakout.States
{
    /// <summary>
    /// State machine controlling which state is displayed currently.
    /// </summary>
    public class StateMachine : IGameEventProcessor
    {
        public IGameState ActiveState { get; private set; } // Property to get the currently active state
        private int currentlevel = 1; // Variable to track the current level

        // Constructor to initialize the state machine
        public StateMachine()
        {
            // Subscribe the state machine to game state and input events
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, this);

            // Initialize instances of game states
            MainMenu.GetInstance();
            GameRunning.GetInstance(currentlevel);
            GamePaused.GetInstance();
            GameOver.GetInstance();
            ActiveState = MainMenu.GetInstance(); // Set the initial active state to MainMenu
        }

        // Method to switch the active state based on the given state type
        private void SwitchState(GameStateType stateType)
        {
            switch (stateType)
            {
                case GameStateType.GameRunning:
                    ActiveState = GameRunning.GetInstance(currentlevel);
                    break;
                case GameStateType.GamePaused:
                    ActiveState = GamePaused.GetInstance();
                    break;
                case GameStateType.MainMenu:
                    ActiveState = MainMenu.GetInstance();
                    break;
                case GameStateType.GameOver:
                    ActiveState = GameOver.GetInstance();
                    break;
                default:
                    break;
            }
        }

        // Method to process game events and switch states accordingly
        public void ProcessEvent(GameEvent gameEvent)
        {
            if (gameEvent.EventType == GameEventType.GameStateEvent)
            {
                if (gameEvent.Message == "CHANGE_STATE" && gameEvent.StringArg1 != null)
                {
                    if (gameEvent.StringArg2 != "pause" && gameEvent.StringArg1 == "GAME_RUNNING")
                    {
                        ActiveState = GameRunning.NewGame(int.Parse(gameEvent.StringArg2)); // Start a new game with the given level
                        currentlevel = int.Parse(gameEvent.StringArg2); // Update the current level
                    }
                    else
                    {
                        GameStateType newState = StateTransformer.TransformStringToState(gameEvent.StringArg1); // Transform the state string to GameStateType
                        SwitchState(newState); // Switch to the new state
                    }
                }
            }
        }
    }
}
