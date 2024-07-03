using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System;



namespace Breakout
{
    public class Player
    {
        // Movement-related fields
        private float moveLeft = 0.0f;
        private float moveRight = 0.0f;
        const float MOVEMENT_SPEED = 0.01f;
        const float DefaultSpeed = 0.01f;

        // Paddle size-related fields
        private float originalPaddleSize;
        const float DefaultPaddleSize = 0.1f;

        // Current speed of the player
        public float Speed { get; private set; } = DefaultSpeed;

        private Entity entity;
        private DynamicShape shape;
        private int lives;

        // Public properties to access shape and lives
        public Shape Shape { get { return shape; } }
        public int Lives { get { return lives; } }

        // Constructor
        public Player(DynamicShape shape, IBaseImage image)
        {
            entity = new Entity(shape, image);
            lives = 1;
            this.shape = shape;
            // Default shape if none provided
            if (shape == null)
            {
                this.shape = new DynamicShape(new Vec2F(0.0f, 0.0f), new Vec2F(0.1f, 0.1f));
            }

            originalPaddleSize = shape.Extent.X;
        }

        // PADDLE POWER-UP METHODS

        public float PaddleSize { get; set; }

        // Doubles the width of the paddle
        public void DoublePaddleSize()
        {
            shape.Extent.X *= 2;
        }

        // Resets the paddle size to default
        public void ResetPaddleSize()
        {
            PaddleSize = DefaultPaddleSize;
            shape.Extent.X = DefaultPaddleSize;
        }

        // SPEED POWER-UP METHODS

        // Halves the player's speed
        public void HalfSpeed()
        {
            Speed = DefaultSpeed / 2;
        }

        // Doubles the player's speed
        public void DoubleSpeed()
        {
            Speed *= 2;
        }

        // Resets the player's speed to default
        public void ResetSpeed()
        {
            Speed = DefaultSpeed;
        }

        // HAZARD EFFECT METHODS

        // Activates the SlimJim effect (narrows the paddle)
        public void ActivateSlimJim()
        {
            shape.Position.X += shape.Extent.X / 4;
            shape.Extent.X /= 2;
        }

        // Deactivates the SlimJim effect (restores original paddle size)
        public void DeactivateSlimJim()
        {
            shape.Position.X -= shape.Extent.X / 2;
            shape.Extent.X = originalPaddleSize;
        }

        // Renders the player entity
        public void Render()
        {
            entity.RenderEntity();
        }

        // Moves the player and handles boundary checks
        public void Move()
        {
            shape.Move();

            // Boundary checks
            if (shape.Position.X < 0.0f)
            {
                shape.Position.X = 0.0f;
            }
            else if (shape.Position.X + shape.Extent.X > 1.0f)
            {
                shape.Position.X = 1.0f - shape.Extent.X;
            }
        }

        // Returns the center position of the player
        public Vec2F GetPosition()
        {
            return new Vec2F(shape.Position.X + shape.Extent.X / 2.0f, shape.Position.Y + shape.Extent.Y / 2.0f);
        }

        // Decreases player's life and checks if the player is dead
        public bool looseLifePoint()
        {
            bool isDead = false;
            if (lives >= 0)
            {
                lives -= 1;
            }
            if (lives < 0)
            {
                isDead = true;
            }
            return isDead;
        }

        // Sets the left movement direction
        public void SetMoveLeft(bool value)
        {
            moveLeft = value ? -MOVEMENT_SPEED : 0.0f;
            UpdateDirection();
        }

        // Sets the right movement direction
        public void SetMoveRight(bool value)
        {
            moveRight = value ? MOVEMENT_SPEED : 0.0f;
            UpdateDirection();
        }

        // Updates the player's movement direction
        private void UpdateDirection()
        {
            shape.Direction.X = moveRight + moveLeft;
        }
    }
}