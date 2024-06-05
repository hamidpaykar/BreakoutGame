using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using System;
using System.Collections.Generic; 
using Breakout.Balls;
namespace Breakout.Blocks;
    /// <summary>
    /// Stores a block in breakout, with necessary information about the block.
    /// This includes it position, health and points earned for breaking it.
    /// Inlcudes a hit function that should be called when contact is made between the ball and the block.
    /// </summary>
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
    public virtual bool Hit(Ball ball){
        bool isDead = false;
        this.health -=1;
        if(this.health < 1 ){
            isDead = true;
        }
        return isDead;
    }
    public virtual bool forDeletion(Ball ball, Player player){
        bool delete = false;
        if(this.health < 1){
            delete = true;
        }
        return delete;
    }
    public virtual void Update(Ball ball, Player player){

    }

}
