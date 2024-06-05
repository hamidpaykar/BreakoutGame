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
namespace BreakoutTest;

//for the test to work a the Asset folder is copied into GalagaTest and copied again into a folder Galaga
//which is pased into Galagatests

[TestFixture]
public class TestScore
{

    [SetUp]
    public void Setup()
    {
        
    }

    [Test]
    //Testing the hit function of the block. It is hit 3 times and should be alive the first to and die after the third.
    public void pointsAreAdded()
    {
        int scoreBefore = Score.GetScore();
        int pointsAdded = 1;
        Score.increaseScore(pointsAdded);
        Assert.AreEqual(scoreBefore+pointsAdded, Score.GetScore());
    }
    [Test]
    //Testing the hit function of the block. It is hit 3 times and should be alive the first to and die after the third.
    public void resetWorks()
    {   
        Assert.AreEqual(1, Score.GetScore());
        Score.reset();
        Assert.AreEqual(0, Score.GetScore());


    }
}