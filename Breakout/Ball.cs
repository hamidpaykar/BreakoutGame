using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
namespace Breakout.Balls;

public class Ball : Entity
{
    private static Vec2F direction = new Vec2F(0.0f, 0.1f);
    private static Vec2F extent = new Vec2F(0.05f, 0.05f);
    private bool isStarted = false;

    public Ball(Vec2F position, IBaseImage image)
        : base(new DynamicShape(position, extent, direction), image)
    {

    }
    public void firstPush(){
        if(!isStarted){
            isStarted = true;
            Shape.Move();
        }
        
    }

    public void blockHit(){
        
    }
}
    //Decrements health by 1 and returns false if Block has positive health

