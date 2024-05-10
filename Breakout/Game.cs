using System.Collections.Generic;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Graphics;
using System;
using DIKUArcade.Math;
using Breakout.States;
using System.IO;
using Breakout;
using DIKUArcade.Physics;


namespace Breakout
{
    public class Game : DIKUGame, IGameEventProcessor
    {
        private StateMachine stateMachine;
        private Player player;

        public Game(WindowArgs windowArgs) : base(windowArgs)
        {
            stateMachine = new StateMachine();
            stateMachine.SwitchState(GameStateType.GameRunning);
            BreakoutBus.GetBus().InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent, GameEventType.PlayerEvent, GameEventType.GameStateEvent });
            BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateMachine);
            window.SetKeyEventHandler(KeyHandler);


            // Initialize the player
            player = new Player(
                new DynamicShape(new Vec2F(0.5f - 0.2f / 2, 0.1f), new Vec2F(0.3f, 0.06f)),
                new Image(Path.Combine("Assets", "Images", "Player.png"))
            );
        }

        public Game(WindowArgs windowArgs, bool initEventBus) : base(windowArgs)
        {
            stateMachine = new StateMachine();
            stateMachine.SwitchState(GameStateType.GameRunning);
            if (initEventBus){
            BreakoutBus.GetBus().InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent, GameEventType.PlayerEvent, GameEventType.GameStateEvent });
            }
            BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateMachine);
            window.SetKeyEventHandler(KeyHandler);


            // Initialize the player
            player = new Player(
                new DynamicShape(new Vec2F(0.5f - 0.2f / 2, 0.1f), new Vec2F(0.3f, 0.06f)),
                new Image(Path.Combine("Assets", "Images", "Player.png"))
            );
        }

        public void KeyHandler(KeyboardAction action, KeyboardKey key)
        {
            switch (action)
            {
                case KeyboardAction.KeyPress:
                    switch (key)
                    {
                        case KeyboardKey.Left:
                            player.SetMoveLeft(true);
                            break;
                        case KeyboardKey.Right:
                            player.SetMoveRight(true);
                            break;
                    }
                    break;
                case KeyboardAction.KeyRelease:
                    switch (key)
                    {
                        case KeyboardKey.Left:
                            player.SetMoveLeft(false);
                            break;
                        case KeyboardKey.Right:
                            player.SetMoveRight(false);
                            break;
                    }
                    break;
            }
        }


        public override void Update()
        {
            stateMachine.ActiveState.UpdateState();
            BreakoutBus.GetBus().ProcessEventsSequentially();
            //player.Move();
        }

        public override void Render()
        {
            stateMachine.ActiveState.RenderState();
            //player.Render();
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