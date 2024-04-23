using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.State;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Input;
using DIKUArcade.Physics;
using System;
namespace Breakout.States {
    public class GameRunning : IGameState {
        private static GameRunning instance;
        public static GameRunning GetInstance() {
            return instance ?? (instance = new GameRunning());
        }
        public static GameRunning NewGame(){
            instance = new GameRunning();
            return (instance);
        }

        private GameRunning() {
            InitializeGame();
        }

        private void InitializeGame() {
    }

         public void UpdateState() {            
        }

         public void RenderState() {
        }

        public void ResetState(){

        }

         public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            /* switch (action) {
                case KeyboardAction.KeyPress:
                    
                    if (key == KeyboardKey.Left) {
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                        EventType = GameEventType.PlayerEvent,
                        Message = "MOVE_LEFT",
                        StringArg1 = "true",
                        To=player

                    });
                    } else if (key == KeyboardKey.Right) {
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                        EventType = GameEventType.PlayerEvent,
                        Message = "MOVE_RIGHT",
                        StringArg1 = "true",
                        To=player
                    });
                    } else if (key==KeyboardKey.Escape){
                       BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                            EventType = GameEventType.GameStateEvent,
                            Message = "CHANGE_STATE",
                            StringArg1 = "GAME_PAUSED",

                        });
                    }
                    break;
                case KeyboardAction.KeyRelease:
                    if (key == KeyboardKey.Left) {
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                        EventType = GameEventType.PlayerEvent,
                        Message = "MOVE_LEFT",
                        StringArg1 = "false",
                        To=player
                    });
                    } else if (key == KeyboardKey.Right) {
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                        EventType = GameEventType.PlayerEvent,
                        Message = "MOVE_RIGHT",
                        StringArg1 = "false",
                        To=player
                    });
                    } else if (key == KeyboardKey.Space) {
                        Vec2F playerPos = player.GetPosition();
                        playerShots.AddEntity(new PlayerShot(playerPos, playerShotImage));
                    }
                    break;
            } */
        }
    }
}

