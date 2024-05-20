using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using System;
using System.Collections.Generic; 
namespace Breakout.Blocks;

public class UnbreakableBlock : Block{
    public UnbreakableBlock(DynamicShape shape, string fileName, int value, int health) : base(shape, fileName, value, int.MaxValue) {
        
    }
    public bool Hit(){
        return false;
    }

}