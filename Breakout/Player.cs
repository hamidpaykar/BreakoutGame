using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System;


namespace Breakout
{
    /// <summary>
    /// Class representing the player, storing information and functionalities regarding the player. 
    /// </summary>
    public class Player
    {
        private float moveLeft = 0.0f;
        private float moveRight = 0.0f;
        const float MOVEMENT_SPEED = 0.01f;

        const float DefaultSpeed = 0.01f;

        private float originalPaddleSize;

        const float DefaultPaddleSize = 0.1f; // Declare DefaultPaddleSize here


        public float Speed { get; private set; } = DefaultSpeed;

        private Entity entity;
        private DynamicShape shape;
        private int lives;
        public Shape Shape{
            get { return shape;}
        }
        public int Lives{
            get { return lives;}
        }
        public Player(DynamicShape shape, IBaseImage image)
        {
            entity = new Entity(shape, image);
            lives = 1;
            this.shape = shape;
            if (shape == null)
            {
                this.shape = new DynamicShape(new Vec2F(0.0f, 0.0f), new Vec2F(0.1f, 0.1f));
            }

            originalPaddleSize = shape.Extent.X;
        }




        // PADDLE POWER-UP ----------------------------

        public float PaddleSize { get; set; }

        public void DoublePaddleSize()
        {
            shape.Extent.X *= 2;
        }

        public void ResetPaddleSize()
        {
            PaddleSize = DefaultPaddleSize;
            shape.Extent.X = DefaultPaddleSize; 
        }

        // --------------------------------------------

        //speed power-up ------------------------------

        public void HalfSpeed()
        {
            Speed = DefaultSpeed / 2;
            Console.WriteLine("2");
        }

        public void DoubleSpeed()
        {
            Speed *= 2;
            Console.WriteLine("2");
        }

        public void ResetSpeed()
        {
            Speed = DefaultSpeed;
        }

        // --------------------------------------------

        // Hazard effect ------------------------------

        public void ActivateSlimJim()
        {
            // Decrease the width of the paddle from both sides
            shape.Position.X += shape.Extent.X / 4;
            shape.Extent.X /= 2;
            Console.WriteLine("3");
        }


        public void DeactivateSlimJim()
        {
            // Reset the width of the paddle and its position
            shape.Position.X -= shape.Extent.X / 2;
            shape.Extent.X = originalPaddleSize;

        }


        // --------------------------------------------
        public void Render()
        {
            // TODO: render the player entity
            entity.RenderEntity();
        }

        public void Move()
        {
            // Move the shape based on the current direction
            shape.Move();

            //Console.WriteLine($"Before Adjustment - X: {shape.Position.X}, Width: {shape.Extent.X}, Direction: {shape.Direction.X}");

            // Check if the player is beyond the right boundary
            if (shape.Position.X < 0.0f)
            {
                shape.Position.X = 0.0f;
            }
            else if (shape.Position.X + shape.Extent.X > 1.0f)
            {
                shape.Position.X = 1.0f - shape.Extent.X;
            }

            //Console.WriteLine($"After Adjustment - X: {shape.Position.X}, Width: {shape.Extent.X}, Direction: {shape.Direction.X}");
        }



        


        public Vec2F GetPosition()
        {
            return new Vec2F(shape.Position.X + shape.Extent.X / 2.0f, shape.Position.Y + shape.Extent.Y / 2.0f);
        }
        public bool looseLifePoint(){
            bool isDead = false;
            if(lives >=0){
                lives-=1;
            }
            if(lives <0){
                isDead = true;
            }
            return isDead;
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
            //Console.WriteLine($"Updated Direction: {shape.Direction.X}");  // Debug output
        }
    }
}
