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

namespace BreakoutTest;

//for the test to work a the Asset folder is copied into GalagaTest and copied again into a folder Galaga
//which is pased into Galagatests
public class TestsBlock
{
    public Block block;
    [SetUp]
    public void Setup()
    {
        var windowArgs = new WindowArgs()
        {
            Title = "Galaga v0.1"
        };

        var game = new Game(windowArgs);
        block = new Block(new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "blue-block.png")), 3, 3);
    }

    [Test]
    //doing all test in 1 function, as it becomes problematic as many windows are opened resulting in a crash
    public void test1()
    {
        bool isDead = block.Hit();
        Assert.IsFalse(isDead);

        isDead = block.Hit();
        Assert.IsFalse(isDead);

        isDead = block.Hit();
        Assert.IsTrue(isDead);
    }
    
}