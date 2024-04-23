using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
namespace Breakout.Block;
public class Block : Entity
{
    private int health;
    private int value;
    private Vec2F initialPosition;
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
    public Block(DynamicShape shape, IBaseImage image, int value, int health) : base(shape, image)
    {
        this.health = health;
        this.value = value;
        initialPosition = new Vec2F(Shape.Position.X, Shape.Position.Y);;
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
