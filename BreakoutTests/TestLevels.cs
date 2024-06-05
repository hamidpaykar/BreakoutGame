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
using Breakout.Scores;
using Breakout.Levels;
namespace BreakoutTest;

//for the test to work a the Asset folder is copied into GalagaTest and copied again into a folder Galaga
//which is pased into Galagatests

[TestFixture]
public class TestLevels
{

    [SetUp]
    public void Setup()
    {
        
    }

    [Test]
    public void totalLevelsIsCorrect()
    {
        Assert.AreEqual(6, LevelHolder.TotalLevels);

    }
    [Test]
    public void levelsCountIsCorrect()
    {
        Assert.AreEqual(6, LevelHolder.Levels.Count);

    }

}