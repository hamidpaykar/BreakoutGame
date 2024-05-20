using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using System;
using System.Collections.Generic; 
namespace Breakout.Blocks;

public class HardenedBlock : Block{
    private IBaseImage hurtBlock;
    private int healthForBreaking;
    public HardenedBlock(DynamicShape shape, string fileName, int value, int health) : base(shape, fileName, value, 2*health) {
        string hurtFile = fileName.Substring(0, fileName.IndexOf(".")) + "-damaged.png";
        hurtBlock = new Image(Path.Combine("Assets", "Images", hurtFile));
        healthForBreaking = health/2;
    }
    public bool Hit(){
        bool isDead = false;
        this.health -=1;
        if(this.health <= healthForBreaking){
            Image = hurtBlock;
        }
        if(this.health < 1){
            isDead = true;
        }
        return isDead;
    }

}