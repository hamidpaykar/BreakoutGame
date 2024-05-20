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

[TestFixture]
public class TestsHardened
{
    public Block block;
    public int initialHealth = 3;
    [OneTimeSetUp]
    public void Setup()
    {
        var windowArgs = new WindowArgs()
        {
            Title = "Galaga v0.1"
        };

        var game = new Game(windowArgs);
        block = new HardenedBlock(new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "blue-block.png")), initialHealth, 3);
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
    //Testing the hit function of the block. It is hit 3 times and should be alive the first to and die after the third.
    public void isDoubleHealth()
    {
        Assert.AreEqual(2*initialHealth, block.Health);
    }
    [Test]
    //Testing the hit function of the block. It is hit 3 times and should be alive the first to and die after the third.
    public void changesImageWhenHit()
    {   
        Image oldImage = block.Image;
        block.Hit();
        block.Hit();
        block.Hit();
        Image newImage = block.Image;
        Assert.AreEqual(oldImage, newImage);
    }
}