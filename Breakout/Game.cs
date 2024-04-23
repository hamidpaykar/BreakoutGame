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

    private void KeyHandler(KeyboardAction action, KeyboardKey key) {
        Console.WriteLine("");
    }

    public override void Render() { 
                Console.WriteLine("");        
    }

    public override void Update() {
        Console.WriteLine("");
     }
    public void ProcessEvent(GameEvent gameEvent){

    }
}
}