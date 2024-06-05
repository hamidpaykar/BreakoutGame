using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using System;
using System.Collections.Generic;
using DIKUArcade.Timers;
using DIKUArcade.Events;
using System.Diagnostics;
using Breakout.Balls;
using DIKUArcade.GUI;
using System.Diagnostics;
using DIKUArcade.Physics;


namespace Breakout.Blocks
{
    // 
    /// <summary>
    /// Enumeration for different types of hazards
    /// </summary>
    public enum HazardType
    {
        Slowness,
        SlimJim,
    }


    public class HazardBlock : Block
    {
        private HazardType hazardType;
        private static Random random = new Random();

        private Timer hazardTimer;
        private bool hazardActive;
        private long powerUpTimeStamp = 0;

        private Entity hazardImage;
        private Entity hazardEffect;

        // Dictionary mapping hazard types to their corresponding image file paths
        private static Dictionary<HazardType, string> hazardImages = new Dictionary<HazardType, string>
        {
            { HazardType.Slowness, Path.Combine("Assets", "HazardImages", "Slowness.png") },
            { HazardType.SlimJim, Path.Combine("Assets", "HazardImages", "SlimJim.png") },
        };


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

        // Constructor for the HazardBlock
        /// <summary>
    /// Power up block class, has the same capabilities but can apply hazards.
    /// </summary>
        public HazardBlock(DynamicShape shape, string fileName, int value, int health) : base(shape, fileName, value, health)
        {
            hazardTimer = new Timer(); // Initialize the timer
            hazardActive = false; // No hazard is active initially
            
            // Select a random hazard type
            Array values = Enum.GetValues(typeof(HazardType));
            hazardType = (HazardType)values.GetValue(random.Next(values.Length));

            // Create the visual representation of the hazard
            hazardImage = new Entity(new DynamicShape(shape.Position, new Vec2F(0.1f, 0.1f)), new Image(hazardImages[hazardType]));
        }

        // Override the Render method to include the hazard image if active
        public void Render()
        {
            if (hazardActive)
            {
                hazardImage.RenderEntity();
            }
        }

        // Override the Hit method to apply hazard effects when the block is hit and destroyed
        public override bool Hit(Ball ball)
        {
            bool isDead = base.Hit(ball); // Call the base class Hit method
            if (isDead)
            {
                hazardTimer.Start(); // Start the timer for the hazard duration
                hazardActive = true; // Set the hazard active flag
                SpawnHazardEffect(); // Spawn the visual effect of the hazard
            }
            return isDead;
        }

        // Method to apply the hazard effect based on the hazard type
        private void ApplyHazard(Ball ball)
        {
            switch (hazardType)
            {
                case HazardType.Slowness:
                    ball.Player.HalfSpeed();
                    break;
                case HazardType.SlimJim:
                    ball.Player.ActivateSlimJim();
                    break;
            }
            powerUpTimeStamp = StaticTimer.GetElapsedMilliseconds();

        }

        // Method to deactivate the hazard effect after its duration ends
        public void DeactivateHazard(Ball ball)
        {
            switch (hazardType)
            {
                case HazardType.Slowness:
                    ball.Player.ResetSpeed();
                    break;
                case HazardType.SlimJim:
                    ball.Player.DeactivateSlimJim();
                    break;
            }
            hazardActive = false; // Reset the hazard active flag
        }

        // Method to spawn the visual effect entity for the hazard
        private void SpawnHazardEffect()
        {
            // Create a new Entity object with the image of the hazard effect
            // Give it a direction of (0, -1) for a constant negative vertical speed
            //hazardEffect = new Entity(new DynamicShape(this.Shape.Position, new Vec2F(0.1f, 0.1f), new Vec2F(0, -1)), new Image(hazardImages[hazardType]));
            Shape = new DynamicShape(this.Shape.Position, new Vec2F(0.1f, 0.1f), new Vec2F(0, -0.01f));
            Image = new Image(hazardImages[hazardType]);
        }

        // Update method to manage the hazard effect and its collision with the player
        public void Update(Ball ball, Player player)
        {
            // If the power-up is active and 3 seconds have passed, deactivate the power-up
            //Console.WriteLine(powerUpTimeStamp);
             
            if(CollisionDetection.Aabb(Shape.AsDynamicShape(), player.Shape.AsDynamicShape()).Collision){
                ApplyHazard(ball); // Apply the hazard effect
                hazardActive = true;
                hazardEffect = null;
                Shape = new DynamicShape(this.Shape.Position, new Vec2F(0.0f, 0.0f), new Vec2F(0, -0.01f));
            
            } 

            if (powerUpTimeStamp > 0 && (StaticTimer.GetElapsedMilliseconds()-powerUpTimeStamp)>=3000)
            {
                Console.WriteLine("Entered deactivate");  
                DeactivateHazard(ball);
                hazardTimer.Stop(); // Stop the timer
                hazardTimer.Reset(); // Reset the timer for the next power-up
            }

            // Update the position of the power-up effect
            if (health < 1)
            {
                Shape.Move();
            }
        }
    }
}