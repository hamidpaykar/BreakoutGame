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
        private Player player;
        Random rnd = new Random();
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
            player = new Player(
                new DynamicShape(new Vec2F(0.5f - 0.3f / 2, 0.1f), new Vec2F(0.3f, 0.06f)),
                new Image(Path.Combine("Assets", "Images", "Player.png"))
            );

        }

        public void UpdateState() {   
            ball.firstPush();
            ball.Shape.Move();
            player.Move();
            CheckForCollision();
        }

         public void RenderState() {
            ball.RenderEntity();
            player.Render();
            currentlevel.blocks.RenderEntities();
        }

        public void ResetState(){

        }

        public void CheckForCollision(){
            if(isBottomCollision()){
                ball.DeleteEntity();
                ball = new Ball(new Vec2F(player.Shape.Position.X, 0.1f), new Image(Path.Combine("Assets", "Images", "ball.png")));
            }
            bool wallCollision = isWallCollision();
            bool blockCollision = false;
            bool paddleCollision = false;
            DynamicShape b = ball.Shape.AsDynamicShape();
            currentlevel.blocks.Iterate((Block block) =>
            {
                Shape blockShape = block.Shape;
                if(CollisionDetection.Aabb(b, blockShape).Collision){
                    bool isDead = block.Hit();
                    if(isDead){
                        block.DeleteEntity();
                    }
                    blockCollision = true;
                }
            });
            if(CollisionDetection.Aabb(b, player.Shape.AsDynamicShape()).Collision){
                paddleCollision = true;
            }
            if(wallCollision){
                b.ChangeDirection(alterDirection(b, true));
            }
            else if(blockCollision || paddleCollision){
                b.ChangeDirection(alterDirection(b, false));
            }
        }
        public Vec2F alterDirection(DynamicShape ball, bool alterX){
            float normFactor = 100.0f;
            float x;
            float y;
            if(alterX){
                x = ball.Direction.X *(-1.0f);
                y = ball.Direction.Y;
            }
            else{
                x = ball.Direction.X;
                y = ball.Direction.Y *(-1.0f);
            }
            float noiseXPos = (float)rnd.NextDouble()/normFactor;
            float noiseYPos = (float)rnd.NextDouble()/normFactor;
            float noiseXNeg = (float)rnd.NextDouble()/normFactor;
            float noiseYNeg = (float)rnd.NextDouble()/normFactor;
            x += noiseXPos;
            y += noiseYPos;
            x -= noiseXNeg;
            y -= noiseYNeg;
            float vectorLength = getVectorLength(x, y);
            x = normalizeVectorCoordinate(x, vectorLength)/normFactor;
            y = normalizeVectorCoordinate(y, vectorLength)/normFactor;
            return new Vec2F(x, y);
        }
        private float normalizeVectorCoordinate(float x, float vectorLength){
            return x/vectorLength;
        }
        private float getVectorLength(float x, float y){
            return (float)Math.Sqrt(Math.Pow(x,2) + Math.Pow(y,2));
        }

        private bool isWallCollision(){
            bool isCollision = false;
            if(ball.Shape.Position.X <=0 || ball.Shape.Position.Y >= 1 || ball.Shape.Position.X >= 1){
                isCollision = true;
            }
            return isCollision;
        }
        private bool isBottomCollision(){
            return (ball.Shape.Position.Y <= 0);
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
    }
}

