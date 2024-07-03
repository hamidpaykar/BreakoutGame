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
namespace Breakout.States
{
    /// <summary>
    /// Represents the state, when the game is running. 
    /// Can either transition to GamePaused or to GameOver, if the player loses or wins during the gameplay
    /// </summary>
    public class GameRunning : IGameState
    {
        private static GameRunning instance;
        private Block b;
        private Level currentlevel;
        private Ball ball;
        private Player player;
        private const int pointsForWin = 200;
        private Text[] displayedInformation = { new Text("Lives: ", new Vec2F(0.0f, 0.0f), new Vec2F(0.2f, 0.2f)), new Text("Time: ", new Vec2F(0.70f, 0.0f), new Vec2F(0.2f, 0.2f)), new Text("Score: ", new Vec2F(0.0f, 0.7f), new Vec2F(0.2f, 0.2f)) };
        private long timeStart;
        Random rnd = new Random();
        /// <summary>
        /// Returns the active GameRunning state, using the active level if one exist.
        /// If it does not exist, a new GameRunning state is created. 
        /// </summary>
        public static GameRunning GetInstance(int level)
        {
            // Returns the active GameRunning state if it exists, otherwise creates a new one
            return instance ?? (instance = new GameRunning(level));
        }
        /// <summary>
        /// Starts a new game.
        ///Differentiates itself from GetInstance as GetInstance returns the current game.
        /// </summary>
        public static GameRunning NewGame(int level)
        {
            // Starts a new game and returns the new GameRunning state
            instance = new GameRunning(level);
            return (instance);
        }

        private GameRunning(int level)
        {
            // Initializes the game with the given level
            InitializeGame(level);
        }

        private void InitializeGame(int level = 0)
        {
            // Initializes the game with the given level
            currentlevel = LevelHolder.Levels[level - 1];
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
        public void UpdateState()
        {
            // Starts the ball
            ball.firstPush();
            // Updates the ball's position
            ball.Shape.Move();
            // Updates the player's position
            player.Move();
            // Checks for collisions
            CheckForCollision();
            // Checks if the game is won
            isWin();
        }
        /// <summary>
        /// Renders the state. Hereunder the dynamic information displays are updated. 
        /// </summary>
        public void RenderState()
        {
            // Renders the ball
            ball.RenderEntity();
            // Renders the player
            player.Render();
            // Renders the blocks
            currentlevel.blocks.RenderEntities();
            // Updates and renders the displayed information
            displayedInformation[0].SetText("Lives: " + player.Lives);
            displayedInformation[0].SetColor(new Vec3I(255, 255, 0));
            displayedInformation[0].RenderText();
            displayedInformation[2].SetText("Score: " + Score.GetScore());
            displayedInformation[2].SetColor(new Vec3I(255, 255, 0));
            displayedInformation[2].RenderText();
            // Resumes the timer
            StaticTimer.ResumeTimer();
            if (currentlevel.Time != 0)
            {
                // Updates and renders the time left
                long timeElapsedMilliseconds = StaticTimer.GetElapsedMilliseconds();
                long timeElapsedSeconds = (timeElapsedMilliseconds - timeStart) / 1000;
                long timeLeft = currentlevel.Time - timeElapsedSeconds;
                displayedInformation[1].SetText("Time: " + timeLeft.ToString());
                if (timeLeft <= 0)
                {
                    // If time is up, registers a game over event
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent
                    {
                        EventType = GameEventType.GameStateEvent,
                        Message = "CHANGE_STATE",
                        StringArg1 = "GAME_OVER"
                    });
                }
                displayedInformation[1].SetColor(new Vec3I(255, 255, 0));
                displayedInformation[1].RenderText();

            }
        }

        public void ResetState()
        {

        }

        private bool isWin()
        {
            // Checks if the game is won
            bool win = false;
            if (Score.GetScore() >= pointsForWin)
            {
                win = true;
                Score.setWin();
                BreakoutBus.GetBus().RegisterEvent(new GameEvent
                {
                    EventType = GameEventType.GameStateEvent,
                    Message = "CHANGE_STATE",
                    StringArg1 = "GAME_OVER"
                });
            }
            return win;
        }

        private void CheckForCollision()
        {
            // Checks for collisions
            if (isBottomCollision())
            {
                // If ball hits the bottom, player loses a life
                bool isPlayerDead = player.looseLifePoint();
                if (isPlayerDead)
                {
                    // Register a game event to change the game state to GAME_OVER
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent
                    {
                        EventType = GameEventType.GameStateEvent,
                        Message = "CHANGE_STATE",
                        StringArg1 = "GAME_OVER"
                    });
                }
                // Resets the ball
                ball.DeleteEntity();
                // Creates a new ball at the player's position
                ball = new Ball(new Vec2F(player.Shape.Position.X, 0.1f), new Image(Path.Combine("Assets", "Images", "ball.png")));
                ball.Player = player;
            }
            // Checks for wall collision
            bool wallCollision = isWallCollision();
            // Checks for top collision
            bool topCollision = isTopCollision();
            // Initializes the collision direction
            CollisionDirection colDir = CollisionDirection.CollisionDirUnchecked;
            // Initializes block collision flag
            bool blockCollision = false;
            // Initializes paddle collision flag
            bool paddleCollision = false;
            // Converts ball shape to dynamic shape
            DynamicShape b = ball.Shape.AsDynamicShape();
            // Converts player shape to dynamic shape
            DynamicShape playerShape = player.Shape.AsDynamicShape();

            // Iterates through all blocks in the current level
            currentlevel.blocks.Iterate((Block block) =>
            {
                // Updates the block with the ball and player
                block.Update(ball, player);
                // Gets the block shape
                Shape blockShape = block.Shape;
                // Checks for collision between the ball and the block
                CollisionData collision = CollisionDetection.Aabb(b, blockShape);
                if (collision.Collision)
                {
                    // Checks if the block is dead after the collision
                    bool isDead = block.Hit(ball);
                    if (isDead)
                    {
                        // Increases the score by the block's value
                        Score.increaseScore(block.Value);
                    }
                    // Updates the collision direction
                    colDir = collision.CollisionDir;
                    // Sets the block collision flag
                    blockCollision = true;
                }
                // Checks if the block should be deleted
                if (block.forDeletion(ball, player))
                {
                    // Deletes the block
                    block.DeleteEntity();
                }
            });
            // Checks for collision between the ball and the player
            if (CollisionDetection.Aabb(b, playerShape).Collision)
            {
                // Sets the paddle collision flag
                paddleCollision = true;
                // Checks the direction of the player shape
                bool negativeNoise = false;
                bool positiveNoise = false;
                if (playerShape.Direction.X > 0)
                {
                    // Sets positive noise flag
                    positiveNoise = true;
                }
                else if (playerShape.Direction.X < 0)
                {
                    // Sets negative noise flag
                    negativeNoise = true;
                }
                // Changes the ball direction based on the player shape direction
                b.ChangeDirection(alterDirection(b, false, true, negativeNoise, positiveNoise));
            }
            // Checks for wall collision
            if (wallCollision)
            {
                // Changes the ball direction based on the wall collision
                b.ChangeDirection(alterDirection(b, true, false, false, false));
            }
            // Checks for top collision
            if (topCollision)
            {
                // Changes the ball direction based on the top collision
                b.ChangeDirection(alterDirection(b, false, true, false, false));
            }
            else if (blockCollision)
            {
                // Initializes flags to alter the ball direction
                bool alterX = false;
                bool alterY = false;
                // Checks the collision direction
                if (colDir == CollisionDirection.CollisionDirLeft)
                {
                    // Sets alterX flag
                    alterX = true;
                }
                else if (colDir == CollisionDirection.CollisionDirRight)
                {
                    // Sets alterX flag
                    alterX = true;
                }
                else if (colDir == CollisionDirection.CollisionDirUp)
                {
                    // Sets alterY flag
                    alterY = true;
                }
                else if (colDir == CollisionDirection.CollisionDirDown)
                {
                    // Sets alterY flag
                    alterY = true;
                }
                // Changes the ball direction based on the block collision
                b.ChangeDirection(alterDirection(b, alterX, alterY, false, false));
            }
        }



        private Vec2F alterDirection(DynamicShape ball, bool alterX, bool alterY, bool noiseXMinus, bool noiseXPlus)
        {
            // Calculate a normalization factor for the new direction
            float normFactor = 100.0f;

            // Initialize the new direction's x and y coordinates
            float x = 0;
            float y = 0;

            // If the x direction needs to be altered
            if (alterX)
            {
                // Reverse the x direction
                x = ball.Direction.X * (-1.0f);
                // Keep the y direction the same
                y = ball.Direction.Y;
            }

            // If the y direction needs to be altered
            if (alterY)
            {
                // Keep the x direction the same
                x = ball.Direction.X;
                // Reverse the y direction
                y = ball.Direction.Y * (-1.0f);
            }

            // If there's a chance to add noise to the x direction
            if (noiseXMinus)
            {
                // Generate a random noise value
                float noiseXNeg = (float)rnd.NextDouble() / normFactor;
                // Subtract the noise from the x direction
                x -= noiseXNeg;
            }

            // If there's a chance to add noise to the x direction
            if (noiseXPlus)
            {
                // Generate a random noise value
                float noiseXPos = (float)rnd.NextDouble() / normFactor;
                // Add the noise to the x direction
                x += noiseXPos;
            }

            // Calculate the length of the new direction vector
            float vectorLength = getVectorLength(x, y);

            // Normalize the x and y coordinates
            x = normalizeVectorCoordinate(x, vectorLength) / normFactor;
            y = normalizeVectorCoordinate(y, vectorLength) / normFactor;

            // Return the new direction as a Vec2F object
            return new Vec2F(x, y);
        }


        private float normalizeVectorCoordinate(float x, float vectorLength)
        {
            // Normalizes the vector coordinate
            return x / vectorLength;
        }
        private float getVectorLength(float x, float y)
        {
            // Calculates the vector length
            return (float)Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
        }

        private bool isWallCollision()
        {
            // Checks for wall collision
            bool isCollision = false;
            if (ball.Shape.Position.X <= 0 || ball.Shape.Position.X >= 1)
            {
                isCollision = true;
            }
            return isCollision;
        }
        private bool isTopCollision()
        {
            // Checks for top collision
            return (ball.Shape.Position.Y >= 1);
        }
        private bool isBottomCollision()
        {
            // Checks for bottom collision
            return (ball.Shape.Position.Y <= 0);
        }

        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key)
        {
            // Handles keyboard events
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
                            BreakoutBus.GetBus().RegisterEvent(new GameEvent
                            {
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
