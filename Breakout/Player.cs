using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System;


namespace Breakout
{
    public class Player
    {
        private float moveLeft = 0.0f;
        private float moveRight = 0.0f;
        const float MOVEMENT_SPEED = 0.01f;
        private Entity entity;
        private DynamicShape shape;
        public Player(DynamicShape shape, IBaseImage image)
        {
            entity = new Entity(shape, image);
            this.shape = shape;
            if (shape == null)
            {
                this.shape = new DynamicShape(new Vec2F(0.0f, 0.0f), new Vec2F(0.1f, 0.1f));
            }
        }
        public void Render()
        {
            // TODO: render the player entity
            entity.RenderEntity();
        }

        public void Move()
        {
            // Move the shape based on the current direction
            shape.Move();

            Console.WriteLine($"Before Adjustment - X: {shape.Position.X}, Width: {shape.Extent.X}, Direction: {shape.Direction.X}");

            // Check if the player is beyond the right boundary
            if (shape.Position.X < 0.0f)
            {
                shape.Position.X = 0.0f;
            }
            else if (shape.Position.X + shape.Extent.X > 1.0f)
            {
                shape.Position.X = 1.0f - shape.Extent.X;
            }

            Console.WriteLine($"After Adjustment - X: {shape.Position.X}, Width: {shape.Extent.X}, Direction: {shape.Direction.X}");
        }


        public Vec2F GetPosition()
        {
            return new Vec2F(shape.Position.X + shape.Extent.X / 2.0f, shape.Position.Y + shape.Extent.Y / 2.0f);
        }

        public void SetMoveLeft(bool value)
        {
            moveLeft = value ? -MOVEMENT_SPEED : 0.0f;
            UpdateDirection();
        }

        public void SetMoveRight(bool value)
        {
            moveRight = value ? MOVEMENT_SPEED : 0.0f;
            UpdateDirection();
        }

        private void UpdateDirection()
        {
            shape.Direction.X = moveRight + moveLeft;
            Console.WriteLine($"Updated Direction: {shape.Direction.X}");  // Debug output
        }
    }
}
