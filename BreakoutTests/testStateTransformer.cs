using NUnit.Framework;
using System;
using Galaga;
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
using DIKUArcade.State;

namespace GalagaTests;

//for the test to work a the Asset folder is copied into GalagaTest and copied again into a folder Galaga
//which is pased into Galagatests
public class TestStateTransformer
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    [Test]
    public void TestStringToGameRunning()
    {
        Assert.AreEqual(GameStateType.GameRunning, StateTransformer.TransformStringToState("GAME_RUNNING"));
    }


    [Test]
    public void TestGameRunningToString()
    {
        Assert.Equals(StateTransformer.TransformStateToString(GameStateType.GameRunning),"GAME_RUNNING");
    }

    [Test]
    public void TestStringToGamePaused()
    {
        Assert.Equals(StateTransformer.TransformStringToState("GAME_PAUSED"), GameStateType.GamePaused);

    }

    [Test]
    public void TestGamePausedToString()
    {
        Assert.Equals(StateTransformer.TransformStateToString(GameStateType.GamePaused),"GAME_PAUSED");
    }

    [Test]
    public void TestStringToMainMenu()
    {
        Assert.Equals(StateTransformer.TransformStringToState("MAIN_MENU"), GameStateType.MainMenu);

    }

    [Test]
    public void TestMainMenuToString()
    {
        Assert.Equals(StateTransformer.TransformStateToString(GameStateType.MainMenu),"MAIN_MENU");
    }


    
}