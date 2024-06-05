using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using System;
using System.Collections.Generic;
using DIKUArcade.Timers;
using DIKUArcade.Events;
using Breakout.Balls;
using DIKUArcade.GUI;
using System.Diagnostics;
using DIKUArcade.Timers;
using DIKUArcade.Physics;

namespace Breakout.Blocks
{
    //// <summary>
    /// Enumeration for different types of powerups
    /// </summary>
    public enum PowerUpType
    {
        HalfSpeed,
        DoubleSpeed,
        DoubleSizeBall,
        BigPaddle,
        HardBall
    }

    public class Timer
    {
        private Stopwatch stopwatch;

        public Timer()
        {
            stopwatch = new Stopwatch();
        }

        public void Start()
        {
            stopwatch.Start();
        }

        public void Stop()
        {
            stopwatch.Stop();
        }

        public void Reset()
        {
            stopwatch.Reset();
        }

        public double MeasureTimeSeconds()
        {
            return stopwatch.Elapsed.TotalSeconds;
        }
    }
/// <summary>
    /// Power up block class, has the same capabilities but can apply powerups.
    /// </summary>
    public class PowerUpBlock : Block
    {
        private PowerUpType powerUpType;
        private static Random random = new Random();
        private Timer powerUpTimer;
        private bool powerUpActive;
        private long powerUpTimeStamp = 0;

        public bool PowerUpActive
        {
            get { return powerUpActive; }
        }
        public long PowerUpTimeStamp{
            get{return powerUpTimeStamp;}
        }
        private Entity powerUpImage;
        private Entity powerUpEffect;

        // Dictionary mapping power-up types to their corresponding image file paths
        private static Dictionary<PowerUpType, string> powerUpImages = new Dictionary<PowerUpType, string>
        {
            { PowerUpType.HalfSpeed, Path.Combine("Assets", "Images", "HalfSpeedPowerUp.png") },
            { PowerUpType.DoubleSpeed, Path.Combine("Assets", "Images", "DoubleSpeedPowerUp.png") },
            { PowerUpType.DoubleSizeBall, Path.Combine("Assets", "Images", "BigPowerUp.png") },
            { PowerUpType.BigPaddle, Path.Combine("Assets", "Images", "WidePowerUp.png") },
            { PowerUpType.HardBall, Path.Combine("Assets", "Images", "WallPowerUp.png") },
        };

        // Constructor for the PowerUpBlock
        public PowerUpBlock(DynamicShape shape, string fileName, int value, int health) : base(shape, fileName, value, health)
        {
            powerUpTimer = new Timer(); // Initialize the timer
            powerUpActive = false; // No power-up is active initially

            // Select a random power-up type
            Array values = Enum.GetValues(typeof(PowerUpType));
            powerUpType = (PowerUpType)values.GetValue(random.Next(values.Length));

            // Create the visual representation of the power-up
            powerUpImage = new Entity(new DynamicShape(shape.Position, new Vec2F(0.1f, 0.1f)), new Image(powerUpImages[powerUpType]));
        }

        // Override the Render method to include the power-up image if active
    

        // Override the Hit method to apply power-up effects when the block is hit and destroyed
        public override bool Hit(Ball ball)
        {
            bool isDead = base.Hit(ball); // Call the base class Hit method
            if (isDead)
            {
                powerUpTimer.Start(); // Start the timer for the power-up duration
                //powerUpActive = true; // Set the power-up active flag
                //Image = new Image(powerUpImages[powerUpType]);
                //Shape = new DynamicShape(shape.Position, new Vec2F(0.1f, 0.1f));
                SpawnPowerUpEffect(); // Spawn the visual effect of the power-up
            }
            return isDead;
        }
        public override bool forDeletion(Ball ball, Player player){
            bool delete = false;
            if (powerUpTimeStamp > 0 && (StaticTimer.GetElapsedMilliseconds()-powerUpTimeStamp)>=3000)
            {
                delete = true;
                DeactivatePowerUp(ball);
                powerUpTimer.Stop(); // Stop the timer
                powerUpTimer.Reset(); // Reset the timer for the next power-up
            }
            return delete;
        }

        // Method to apply the power-up effect based on the power-up type
        private void ApplyPowerUp(Ball ball)
        {
            switch (powerUpType)
            {
                case PowerUpType.HalfSpeed:
                    ball.Player.HalfSpeed();
                    break;
                case PowerUpType.DoubleSpeed:
                    ball.Player.DoubleSpeed();
                    break;
                case PowerUpType.DoubleSizeBall:
                    ball.DoubleSize();
                    break;
                case PowerUpType.BigPaddle:
                    ball.Player.DoublePaddleSize();
                    break;
                case PowerUpType.HardBall:
                    ball.ActivateHardBall();
                    break;
            }
            powerUpTimeStamp = StaticTimer.GetElapsedMilliseconds();
            //Console.WriteLine(powerUpTimeStamp);
        }

        // Method to deactivate the power-up effect after its duration ends
        public void DeactivatePowerUp(Ball ball)
        {
            switch (powerUpType)
            {
                case PowerUpType.HalfSpeed:
                    ball.Player.ResetSpeed();
                    break;
                case PowerUpType.DoubleSpeed:
                    ball.Player.ResetSpeed();
                    break;
                case PowerUpType.DoubleSizeBall:
                    ball.ResetSize();
                    break;
                case PowerUpType.BigPaddle:
                    ball.Player.ResetPaddleSize(); // Change this line
                    break;
                case PowerUpType.HardBall:
                    ball.DeactivateHardBall();
                    break;
            }
            powerUpActive = false; // Reset the power-up active flag
            //powerUpTimeStamp = 0;
        }

        // Method to spawn the visual effect entity for the power-up
        private void SpawnPowerUpEffect()
        {
            // Create a new Entity object with the image of the power-up effect
            // Give it a direction of (0, -1) for a constant negative vertical speed
            //powerUpEffect = new Entity(new DynamicShape(this.Shape.Position, new Vec2F(0.1f, 0.1f), new Vec2F(0, -1)), new Image(powerUpImages[powerUpType]));
            Shape = new DynamicShape(this.Shape.Position, new Vec2F(0.1f, 0.1f), new Vec2F(0, -0.01f));
            Image = new Image(powerUpImages[powerUpType]);
        }

        // Update method to manage the power-up effect and its collision with the player
        public override void Update(Ball ball, Player player)
        {
            // If the power-up is active and 3 seconds have passed, deactivate the power-up
            //Console.WriteLine(powerUpTimeStamp);
             
            if(CollisionDetection.Aabb(Shape.AsDynamicShape(), player.Shape.AsDynamicShape()).Collision){
                ApplyPowerUp(ball); // Apply the power-up effect
                powerUpActive = true;
                powerUpEffect = null;
                Shape = new DynamicShape(this.Shape.Position, new Vec2F(0.0f, 0.0f), new Vec2F(0, -0.01f));
            
            } 

            if (powerUpTimeStamp > 0 && (StaticTimer.GetElapsedMilliseconds()-powerUpTimeStamp)>=3000)
            {
                Console.WriteLine("Entered deactivate");  
                DeactivatePowerUp(ball);
                powerUpTimer.Stop(); // Stop the timer
                powerUpTimer.Reset(); // Reset the timer for the next power-up
            }

            // Update the position of the power-up effect
            if (health < 1)
            {
                Shape.Move();
            }
        }
    }
}
