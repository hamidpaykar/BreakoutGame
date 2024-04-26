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

namespace BreakoutTests;

//for the test to work a the Asset folder is copied into GalagaTest and copied again into a folder Galaga
//which is pased into Galagatests
public class TestsPlayer
{
    public Player player;
    [SetUp]
    public void Setup()
    {
        var windowArgs = new WindowArgs()
        {
            Title = "Breakout v0.1"
        };

        var game = new Game(windowArgs);
        player = new Player(
            new DynamicShape(new Vec2F(0.1f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image("Breakout\\Assets\\Images\\Player.png"));
    }

    [Test]
    //doing all test in 1 function, as it becomes problematic as many windows are opened resulting in a crash
    public void test1()
    {
        //TODO: Implement player tests
        Vec2F pos1 = player.GetPosition();
        player.SetMoveLeft(true);
        player.Move();

        Vec2F pos2 = player.GetPosition();
        player.SetMoveLeft(false);
        player.Move();

        Vec2F pos3 = player.GetPosition();

        player.SetMoveRight(true);
        player.Move();

        Vec2F pos4 = player.GetPosition();
        player.SetMoveRight(false);
        player.Move();

        Vec2F pos5 = player.GetPosition();
        //First testing if setting move left to true, moves the player to the left
        //Second testing if setting moveLeft to false stops it from moving
        //Third test, same as first, but with moveRight
        //fourth test same as second but with moveRight
        //fifth testing if y stays the same

        //These test test direction at the same time as dynamicShapes move function uses the direction to move.
        //SO if the direction function works, the positions should be correct. 
        Assert.True(pos2.X < pos1.X & pos3.X == pos2.X & pos3.X < pos4.X & pos4.X == pos5.X & pos1.Y == pos5.Y);

    }
}