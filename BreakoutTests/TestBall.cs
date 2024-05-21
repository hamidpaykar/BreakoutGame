using NUnit.Framework;
using System;
using Breakout;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.GUI;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Events;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.Collections.Generic;
using System;
using System.IO;
using DIKUArcade.Physics;
using Breakout.Blocks;
using Breakout.Balls;

namespace BreakoutTest;

//for the test to work a the Asset folder is copied into GalagaTest and copied again into a folder Galaga
//which is pased into Galagatests

[TestFixture]
public class TestBall
{
    public Block block;
    public Ball ball;
    [OneTimeSetUp]
    public void Setup()
    {
        var windowArgs = new WindowArgs()
        {
            Title = "Galaga v0.1"
        };

        var game = new Game(windowArgs);
        block = new Block(new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "blue-block.png")), 3, 3);
        ball = new Ball(new Vec2F(0.5f - 0.2f / 2, 0.1f), new Image(Path.Combine("Assets", "Images", "ball.png")));

    }

    [SetUp]
    public void Setup2()
    {
        var windowArgs = new WindowArgs()
        {
            Title = "Galaga v0.1"
        };

        var game = new Game(windowArgs, false);
        block = new Block(new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "blue-block.png")), 3, 3);
        
    }

    [Test]
    //Testing the if first push function actually works and sets the isStarted field to true.
    public void testFirstPush()
    {
        Vec2F positionPrevious = ball.Shape.Position
        ball.firstPush();
        Vec2F positionAfter = ball.Shape.Position
        Assert.AreNotEqual(positionAfter, positionPrevious);
        Assert.AreEqual(true, ball.isStarted);
        
    }
}