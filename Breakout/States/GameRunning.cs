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
using Breakout.Scores;
using DIKUArcade.Timers;
using Breakout.Levels;
namespace Breakout.States {
    public class GameRunning : IGameState {
        private static GameRunning instance;
        private Block b;
        private Level currentlevel;
        //private List<Level> levels = new List<Level>();
        private Ball ball;
        private Player player;
        private const int pointsForWin = 200;
        private string[] menuText = { "Lives: ", "Time: "};
        private Text[] menuButtons = {new Text("Lives: ", new Vec2F(0.0f, 0.0f), new Vec2F(0.2f, 0.2f)), new Text("Time: ", new Vec2F(0.70f, 0.0f), new Vec2F(0.2f, 0.2f)), new Text("Score: ", new Vec2F(0.0f, 0.7f), new Vec2F(0.2f, 0.2f))};
        private long timeStart;
        Random rnd = new Random();
        public static GameRunning GetInstance(int level) {
            return instance ?? (instance = new GameRunning(level));
        }
        public static GameRunning NewGame(int level){
            instance = new GameRunning(level);
            return (instance);
        }

        private GameRunning(int level) {
            InitializeGame(level);
        }

        private void InitializeGame(int level = 0) {
            /* levels.Add(new Level("1", "central-mass.txt"));
            levels.Add(new Level("2", "columns.txt"));
            levels.Add(new Level("3", "level1.txt"));
            levels.Add(new Level("4", "level2.txt"));
            levels.Add(new Level("5", "level3.txt"));
            levels.Add(new Level("6", "wall.txt"));
            levels.Add(new Level("7", "level4.txt")); */

            
            currentlevel = LevelHolder.Levels[level-1];
            ball = new Ball(new Vec2F(0.5f - 0.2f / 2, 0.1f), new Image(Path.Combine("Assets", "Images", "ball.png")));
            player = new Player(
                new DynamicShape(new Vec2F(0.5f - 0.3f / 2, 0.1f), new Vec2F(0.3f, 0.06f)),
                new Image(Path.Combine("Assets", "Images", "Player.png"))
            );
            timeStart = StaticTimer.GetElapsedMilliseconds();
        }
        public void UpdateState() {   
            ball.firstPush();
            ball.Shape.Move();
            player.Move();
            CheckForCollision();
            isWin();
        }

         public void RenderState() {
            ball.RenderEntity();
            player.Render();
            currentlevel.blocks.RenderEntities();
            menuButtons[0].SetText("Lives: " + player.Lives);
            menuButtons[0].SetColor(new Vec3I(255, 255, 0));
            menuButtons[0].RenderText();
            menuButtons[2].SetText("Score: " + Score.GetScore());
            menuButtons[2].SetColor(new Vec3I(255, 255, 0));
            menuButtons[2].RenderText();
            StaticTimer.ResumeTimer();  
            if(currentlevel.Time != 0){
                long timeElapsedMilliseconds = StaticTimer.GetElapsedMilliseconds();
                long timeElapsedSeconds = (timeElapsedMilliseconds-timeStart)/1000;
                long timeLeft = currentlevel.Time - timeElapsedSeconds;
                menuButtons[1].SetText("Time: " + timeLeft.ToString());
                if(timeLeft<=0){
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                            EventType = GameEventType.GameStateEvent,
                            Message = "CHANGE_STATE",
                            StringArg1 = "GAME_OVER"
                        });
                }
                menuButtons[1].SetColor(new Vec3I(255, 255, 0));
                menuButtons[1].RenderText();
                
            }
        }

        public void ResetState(){

        }

        private bool isWin(){
            bool win = false;
            if(Score.GetScore() >= pointsForWin){
                win = true;
                Score.setWin();
                BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                            EventType = GameEventType.GameStateEvent,
                            Message = "CHANGE_STATE",
                            StringArg1 = "GAME_OVER"
                        });
            }
            return win;
        }
        public void CheckForCollision(){
            if(isBottomCollision()){
                bool isPlayerDead = player.looseLifePoint();
                if(isPlayerDead){
                     BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                            EventType = GameEventType.GameStateEvent,
                            Message = "CHANGE_STATE",
                            StringArg1 = "GAME_OVER"
                        });
                }
                ball.DeleteEntity();
                ball = new Ball(new Vec2F(player.Shape.Position.X, 0.1f), new Image(Path.Combine("Assets", "Images", "ball.png")));
            }
            bool wallCollision = isWallCollision();
            bool topCollision = isTopCollision();
            CollisionDirection colDir = CollisionDirection.CollisionDirUnchecked;
            bool blockCollision = false;
            bool paddleCollision = false;
            DynamicShape b = ball.Shape.AsDynamicShape();
            DynamicShape playerShape = player.Shape.AsDynamicShape();
            currentlevel.blocks.Iterate((Block block) =>
            {
                Shape blockShape = block.Shape;
                CollisionData collision = CollisionDetection.Aabb(b, blockShape);
                if(collision.Collision){
                    bool isDead = block.Hit();
                    if(isDead){
                        Score.increaseScore(block.Value);
                        block.DeleteEntity();
                        
                    }
                    colDir = collision.CollisionDir;
                    blockCollision = true;
                }
            });
            //Console.WriteLine(CollisionDetection.Aabb(b, playerShape).CollisionDir);
            if(CollisionDetection.Aabb(b, playerShape).Collision){
                paddleCollision = true;
                bool negativeNoise = false;
                bool positiveNoise = false;
                if(playerShape.Direction.X > 0){
                    positiveNoise = true;
                }
                else if(playerShape.Direction.X < 0){
                    negativeNoise = true;
                }
                b.ChangeDirection(alterDirection(b, false, true, negativeNoise, positiveNoise));
            }
            if(wallCollision){
                b.ChangeDirection(alterDirection(b, true, false, false, false));
            }
            if(topCollision){
                b.ChangeDirection(alterDirection(b, false, true, false, false));
            }
            else if(blockCollision){
                bool alterX = false;
                bool alterY = false;
                if(colDir == CollisionDirection.CollisionDirLeft){
                    alterX = true;
                }
                else if(colDir == CollisionDirection.CollisionDirRight){
                    alterX = true;
                }
                else if (colDir == CollisionDirection.CollisionDirUp){
                    alterY = true;
                }
                else if (colDir == CollisionDirection.CollisionDirDown){
                    alterY = true;
                }
                b.ChangeDirection(alterDirection(b, alterX, alterY, false, false));
            }
        }
        
        public Vec2F alterDirection(DynamicShape ball, bool alterX, bool alterY, bool noiseXMinus, bool noiseXPlus){
            float normFactor = 100.0f;
            float x = 0;
            float y = 0;
            if(alterX){
                x = ball.Direction.X *(-1.0f);
                y = ball.Direction.Y;
            }
            if(alterY){
                x = ball.Direction.X;
                y = ball.Direction.Y *(-1.0f);
            }
            if(noiseXMinus){
                float noiseXNeg = (float)rnd.NextDouble()/normFactor;
                x -= noiseXNeg;
            }
            if(noiseXPlus){
                float noiseXPos = (float)rnd.NextDouble()/normFactor;
                x += noiseXPos;
            }
            
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
            if(ball.Shape.Position.X <=0 || ball.Shape.Position.X >= 1){
                isCollision = true;
            }
            return isCollision;
        }
        private bool isTopCollision(){
            return (ball.Shape.Position.Y >= 1);
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
                        case KeyboardKey.Escape:
                            BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                            EventType = GameEventType.GameStateEvent,
                            Message = "CHANGE_STATE",
                            StringArg1 = "GAME_PAUSED",
                        });
                            StaticTimer.PauseTimer();
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

