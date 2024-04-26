using System.Collections.Generic;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Graphics;
using System;
using Breakout.States;
using DIKUArcade.Events;

namespace Breakout
{
public class Game : DIKUGame, IGameEventProcessor {
    private StateMachine stateMachine;

    public Game(WindowArgs windowArgs) : base(windowArgs) {
        stateMachine = new StateMachine();
        stateMachine.SwitchState(GameStateType.GameRunning);
        BreakoutBus.GetBus().InitializeEventBus(new List<GameEventType> {GameEventType.InputEvent, GameEventType.PlayerEvent, GameEventType.GameStateEvent});
        BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, this);
        BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
        BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateMachine);
        window.SetKeyEventHandler(KeyHandler);
    }

   public void KeyHandler(KeyboardAction action, KeyboardKey key) {
             stateMachine.ActiveState.HandleKeyEvent(action, key);
        }

        public override void Update() {
            stateMachine.ActiveState.UpdateState();
            BreakoutBus.GetBus().ProcessEventsSequentially();
        }

        public override void Render() {
            stateMachine.ActiveState.RenderState();
        }

        public void ProcessEvent(GameEvent gameEvent) {
            if(gameEvent.Message=="CLOSE_WINDOW"){
                window.CloseWindow();
            }
            stateMachine.ProcessEvent(gameEvent);
        }
}
}