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
    /// <summary>
    /// Represents the state, when the game is running. 
    /// Can either transition to GamePaused or to GameOver, if the player loses or wins during the gameplay
    /// </summary>
    public class GameRunning : IGameState {
        private static GameRunning instance;
        private Block b;
        private Level currentlevel;
        private Ball ball;
        private Player player;
        private const int pointsForWin = 200;
        private Text[] displayedInformation = {new Text("Lives: ", new Vec2F(0.0f, 0.0f), new Vec2F(0.2f, 0.2f)), new Text("Time: ", new Vec2F(0.70f, 0.0f), new Vec2F(0.2f, 0.2f)), new Text("Score: ", new Vec2F(0.0f, 0.7f), new Vec2F(0.2f, 0.2f))};
        private long timeStart;
        Random rnd = new Random();
        /// <summary>
        /// Returns the active GameRunning state, using the active level if one exist.
        /// If it does not exist, a new GameRunning state is created. 
        /// </summary>
        public static GameRunning GetInstance(int level) {
            return instance ?? (instance = new GameRunning(level));
        }
        /// <summary>
        /// Starts a new game.
        ///Differentiates itself from GetInstance as GetInstance returns the current game.
        /// </summary>
        public static GameRunning NewGame(int level){
            instance = new GameRunning(level);
            return (instance);
        }

        private GameRunning(int level) {
            InitializeGame(level);
        }

        private void InitializeGame(int level = 0) {
            currentlevel = LevelHolder.Levels[level-1];
            ball = new Ball(new Vec2F(0.5f - 0.2f / 2, 0.1f), new Image(Path.Combine("Assets", "Images", "ball.png")));
            player = new Player(
                new DynamicShape(new Vec2F(0.5f - 0.3f / 2, 0.1f), new Vec2F(0.3f, 0.06f)),
                new Image(Path.Combine("Assets", "Images", "Player.png"))
            );
            ball.Player = player;
            timeStart = StaticTimer.GetElapsedMilliseconds();
        }
        /// <summary>
        /// Updates the state of the active Game. Starting the ball, ensuring the ball keeps on moving.
        /// Ensuring the player can move. Checks for collisions, and checks is the game is won. 
        /// </summary>
        public void UpdateState() {   
            ball.firstPush();
            ball.Shape.Move();
            player.Move();
            CheckForCollision();
            isWin();
        }
    /// <summary>
    /// Renders the state. Hereunder the dynamic information displays are updated. 
    /// </summary>
         public void RenderState() {
            ball.RenderEntity();
            player.Render();
            currentlevel.blocks.RenderEntities();
            displayedInformation[0].SetText("Lives: " + player.Lives);
            displayedInformation[0].SetColor(new Vec3I(255, 255, 0));
            displayedInformation[0].RenderText();
            displayedInformation[2].SetText("Score: " + Score.GetScore());
            displayedInformation[2].SetColor(new Vec3I(255, 255, 0));
            displayedInformation[2].RenderText();
            StaticTimer.ResumeTimer();  
            if(currentlevel.Time != 0){
                long timeElapsedMilliseconds = StaticTimer.GetElapsedMilliseconds();
                long timeElapsedSeconds = (timeElapsedMilliseconds-timeStart)/1000;
                long timeLeft = currentlevel.Time - timeElapsedSeconds;
                displayedInformation[1].SetText("Time: " + timeLeft.ToString());
                if(timeLeft<=0){
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                            EventType = GameEventType.GameStateEvent,
                            Message = "CHANGE_STATE",
                            StringArg1 = "GAME_OVER"
                        });
                }
                displayedInformation[1].SetColor(new Vec3I(255, 255, 0));
                displayedInformation[1].RenderText();
                
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
        private void CheckForCollision(){
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
                ball.Player = player; 
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
                block.Update(ball, player);
                Shape blockShape = block.Shape;
                CollisionData collision = CollisionDetection.Aabb(b, blockShape);
                if(collision.Collision){
                    bool isDead = block.Hit(ball);
                    if(isDead){
                        Score.increaseScore(block.Value);                        
                    }
                    colDir = collision.CollisionDir;
                    blockCollision = true;
                }
                if(block.forDeletion(ball, player)){
                        block.DeleteEntity();
                    }
            });
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
        
        private Vec2F alterDirection(DynamicShape ball, bool alterX, bool alterY, bool noiseXMinus, bool noiseXPlus){
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

