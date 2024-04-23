//using DIKUArcade.EventBus;
using DIKUArcade.Events;
using DIKUArcade.State;
using Breakout.States;
using DIKUArcade.Input;
using System;

namespace Breakout.States {
    public class StateMachine : IGameEventProcessor {
        public IGameState ActiveState { get; private set; }
        public StateMachine() {
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, this);            
            GameRunning.GetInstance();
            ActiveState = GameRunning.GetInstance();
        }
        
        public void SwitchState(GameStateType stateType) {
            Console.WriteLine(stateType);
            switch (stateType) {
                case GameStateType.GameRunning:
                    ActiveState = GameRunning.GetInstance();
                    break;
                default:
                    break;
            }
        }
        
        
        public void ProcessEvent(GameEvent gameEvent){
            if (gameEvent.EventType == GameEventType.GameStateEvent) {
                if (gameEvent.Message == "CHANGE_STATE" && gameEvent.StringArg1 != null) {
                    if(gameEvent.StringArg2 == "new"){
                        ActiveState = GameRunning.NewGame();
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
