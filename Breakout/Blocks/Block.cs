using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using System;
using System.Collections.Generic; 
namespace Breakout.Blocks;
public class Block : Entity
{
    protected int health;
    private int value;
    private Vec2F initialPosition;
    private string fileName;
    public int Health{
        get {return health;}
    }

    public int Value{
        get {return this.value;}
        set {this.value=value;}
    }


    public Vec2F InitialPosition{
        get{return initialPosition;}
    }
    public Block(DynamicShape shape, string fileName, int value, int health) : base(shape, new Image(Path.Combine("Assets", "Images", fileName)))
    {
        this.health = health;
        this.value = value;
        initialPosition = new Vec2F(Shape.Position.X, Shape.Position.Y);
        this.fileName = fileName;
    }
    //Decrements health by 1 and returns false if Block has positive health
    public bool Hit(){
        bool isDead = false;
        this.health -=1;
        if(this.health < 1){
            isDead = true;
        }
        return isDead;
    }

}
