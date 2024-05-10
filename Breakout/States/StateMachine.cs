using DIKUArcade.Events;
using DIKUArcade.State;
using DIKUArcade.Input;
using System;

namespace Breakout.States
{
    public class StateMachine : IGameEventProcessor
    {
        public IGameState ActiveState { get; private set; }
        public StateMachine()
        {
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, this);

            MainMenu.GetInstance();
            GameRunning.GetInstance();
            GamePaused.GetInstance();
            ActiveState = MainMenu.GetInstance();
        }

        public void SwitchState(GameStateType stateType)
        {
            switch (stateType)
            {
                case GameStateType.GameRunning:
                    ActiveState = GameRunning.GetInstance();
                    break;
                case GameStateType.GamePaused:
                    ActiveState = GamePaused.GetInstance();
                    break;
                case GameStateType.MainMenu:
                    ActiveState = MainMenu.GetInstance();
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
                    GameStateType newState = StateTransformer.TransformStringToState(gameEvent.StringArg1);
                    SwitchState(newState);
                }
            }
        }
    }
}
