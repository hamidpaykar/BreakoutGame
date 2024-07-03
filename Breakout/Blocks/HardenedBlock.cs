using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using System;
using System.Collections.Generic;
using Breakout.Balls;

namespace Breakout.Blocks
{
    /// <summary>
    /// Extension/variant of the block class, with double the healthpoints and the possibility to display
    /// that the block is damaged using a new image.
    /// </summary>
    public class HardenedBlock : Block
    {
        private IBaseImage hurtBlock; // Image to display when the block is damaged
        private int healthForBreaking; // Health threshold to change the block image

        // Constructor for HardenedBlock
        public HardenedBlock(DynamicShape shape, string fileName, int value, int health)
            : base(shape, fileName, value, 2 * health)
        { // Initializes with double health
            // Construct the filename for the damaged block image
            string hurtFile = fileName.Substring(0, fileName.IndexOf(".")) + "-damaged.png";
            hurtBlock = new Image(Path.Combine("Assets", "Images", hurtFile));
            healthForBreaking = health / 2; // Set the health threshold for image change
        }

        // Override the Hit method to handle damage and image change
        public override bool Hit(Ball ball)
        {
            bool isDead = false;
            this.health -= 1; // Decrease the block's health
            if (this.health <= healthForBreaking)
            {
                Image = hurtBlock; // Change the image to the damaged version
            }
            if (this.health < 1)
            {
                isDead = true; // Mark the block as dead if health is below 1
            }
            return isDead;
        }
    }
}
