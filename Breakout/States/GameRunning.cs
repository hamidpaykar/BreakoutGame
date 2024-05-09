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
using Breakout.Blocks;
using Breakout.LoadLevel;
using Breakout.Balls;

namespace Breakout.States {
    public class GameRunning : IGameState {
        private static GameRunning instance;
        private Block b;
        private Level currentlevel;
        private List<Level> levels = new List<Level>();
        private Ball ball;

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
            levels.Add(new Level("1", "central-mass.txt"));
            levels.Add(new Level("2", "columns.txt"));
            levels.Add(new Level("3", "level1.txt"));
            levels.Add(new Level("4", "level2.txt"));
            levels.Add(new Level("5", "level3.txt"));
            levels.Add(new Level("6", "wall.txt"));
            currentlevel = levels[0];
            //ball = new Ball(new Vec2F(0.5f - 0.2f / 2, 0.1f), new Image(Path.Combine("Assets", "Images", "ball.png")));
            ball = new Ball(new Vec2F(0.5f - 0.2f / 2, 0.1f), new Image(Path.Combine("Assets", "Images", "ball.png")));

        }

        public void UpdateState() {   
            ball.firstPush();
        }

         public void RenderState() {
            ball.RenderEntity();
            currentlevel.blocks.RenderEntities();
        }

        public void ResetState(){

        }

        public void CheckForCollision(){
            if(isWallCollision()){
                
            }
        }

        public bool isWallCollision(){
            bool isCollision = false;
            if(ball.Shape.Position.X <=0 || ball.Shape.Position.Y >= 1){
                isCollision = true;
            }
            return isCollision;
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

