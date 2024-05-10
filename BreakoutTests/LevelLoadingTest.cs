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
using Breakout.LoadLevel;
namespace BreakoutTest;

//for the test to work a the Asset folder is copied into GalagaTest and copied again into a folder Galaga
//which is pased into Galagatests
public class TestsLevelLoader
{
    [SetUp]
    public void Setup()
    {
        var windowArgs = new WindowArgs()
        {
            Title = "Breakout Test v0.1"
        };

        var game = new Game(windowArgs, false);
    }

    [Test]
    public void loadsLevel()
    {
        Level level = new Level("d", "level3.txt");
        Assert.AreEqual("level3.txt", level.FileName);

    }
    [Test]
    public void DoesNotLoadLevel()
    {
        Level level = new Level("d", "devel3.txt");
        Assert.AreEqual("", level.FileName);
        
    }
    [Test]
    public void CorrectAmountOfBlocks()
    {
        Level level = new Level("d", "central-mass.txt");
        int amount = 0;
        level.blocks.Iterate((Block block) =>
            {
                amount++;
            });
        Assert.AreEqual(72, amount);
    }
}