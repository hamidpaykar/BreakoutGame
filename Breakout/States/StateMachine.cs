using DIKUArcade.Events;
using DIKUArcade.State;
using DIKUArcade.Input;
using System;

namespace Breakout.States
{
    /// <summary>
    /// Statemachine, controlling which state is displayed currently.
    /// </summary>
    public class StateMachine : IGameEventProcessor
    {
        public IGameState ActiveState { get; private set; }
        private int currentlevel = 1;
        public StateMachine()
        {
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, this);

            MainMenu.GetInstance();
            GameRunning.GetInstance(currentlevel);
            GamePaused.GetInstance();
            GameOver.GetInstance();
            ActiveState = MainMenu.GetInstance();
        }

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


        public void ProcessEvent(GameEvent gameEvent)
        {
            if (gameEvent.EventType == GameEventType.GameStateEvent)
            {
                if (gameEvent.Message == "CHANGE_STATE" && gameEvent.StringArg1 != null)
                {
                    if(gameEvent.StringArg2 != "pause" && gameEvent.StringArg1 == "GAME_RUNNING")
                    {
                        ActiveState = GameRunning.NewGame(int.Parse(gameEvent.StringArg2));
                        currentlevel = int.Parse(gameEvent.StringArg2);
                    }
                    else{
                        GameStateType newState = StateTransformer.TransformStringToState(gameEvent.StringArg1);
                        SwitchState(newState);
                    }
                    
                }
            }
        }
    }
}
