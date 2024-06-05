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
    /// Responsible for holding together the all subparts of breakout 
    /// </summary>
    public class Game : DIKUGame, IGameEventProcessor
    {
        private StateMachine stateMachine;
        private Player player;


        public Game(WindowArgs windowArgs) : base(windowArgs)
        {
            LevelHolder.loadLevels();
            stateMachine = new StateMachine();
            BreakoutBus.GetBus().InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent, GameEventType.PlayerEvent, GameEventType.GameStateEvent });
            BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateMachine);
            window.SetKeyEventHandler(KeyHandler);
        }

        /// <summary>
        /// Instansiates the game in its entirety. 
        /// </summary>
        public Game(WindowArgs windowArgs, bool initEventBus) : base(windowArgs)
        {
            stateMachine = new StateMachine();
            if (initEventBus){
            BreakoutBus.GetBus().InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent, GameEventType.PlayerEvent, GameEventType.GameStateEvent });
            }
            BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateMachine);
            window.SetKeyEventHandler(KeyHandler);
        } 

        private void KeyHandler(KeyboardAction action, KeyboardKey key)
        {  
            stateMachine.ActiveState.HandleKeyEvent(action, key);
        }


        public override void Update()
        {
            stateMachine.ActiveState.UpdateState();
            BreakoutBus.GetBus().ProcessEventsSequentially();

        }

        public override void Render()
        {
            stateMachine.ActiveState.RenderState();
        }

        public void ProcessEvent(GameEvent gameEvent)
        {
            if (gameEvent.Message == "CLOSE_WINDOW")
            {
                window.CloseWindow();
            }
            stateMachine.ProcessEvent(gameEvent);
        }
    }
}