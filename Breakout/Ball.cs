using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
namespace Breakout.Balls;
/// <summary>
/// Represents the ball in breakout.
/// </summary>
public class Ball : Entity
{
    private static Vec2F direction = new Vec2F(0.0f, 0.01f);
    private float originalVectorLength;
    private static Vec2F extent = new Vec2F(0.04f, 0.04f);
    private bool isStarted = false;

    public float OriginalVectorLength{
        get { return this.originalVectorLength; }
    }

    public bool IsStarted{
        get { return this.isStarted;}
    }

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



}
    //Decrements health by 1 and returns false if Block has positive health

