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
using Breakout.States;
using DIKUArcade.State;
using DIKUArcade.Input;
using NUnit.Framework;
using System;
using Breakout;
using Breakout.States;

namespace BreakoutTests {
[TestFixture]
public class StateMachineTesting {
private StateMachine stateMachine;
[SetUp]
public void InitiateStateMachine() {
DIKUArcade.GUI.Window.CreateOpenGLContext();
/*
Here you should:
(1) Initialize a BreakoutBus with proper GameEventTypes
(2) Instantiate the StateMachine
(3) Subscribe the BreakoutBus to proper GameEventTypes
and GameEventProcessors
*/
//BreakoutBus.GetBus().InitializeEventBus(new List<GameEventType> {GameEventType.InputEvent, GameEventType.PlayerEvent, GameEventType.GameStateEvent});
stateMachine = new StateMachine();
BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateMachine);
}
[Test]
public void TestInitialState() {
Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>());
}
[Test]
public void TestEventGamePaused() {
    BreakoutBus.GetBus().RegisterEvent(
    new GameEvent{
    EventType = GameEventType.GameStateEvent,
    Message = "CHANGE_STATE",
    StringArg1 = "GAME_PAUSED"
    }
    );
    BreakoutBus.GetBus().ProcessEventsSequentially();
    Assert.That(stateMachine.ActiveState, Is.InstanceOf<GamePaused>());
}
[Test]
public void TestEventGameRunning() {
BreakoutBus.GetBus().RegisterEvent(
new GameEvent{
EventType = GameEventType.GameStateEvent,
Message = "CHANGE_STATE",
StringArg1 = "GAME_RUNNING",
StringArg2 = "1"
}
);
BreakoutBus.GetBus().ProcessEventsSequentially();
Assert.That(stateMachine.ActiveState, Is.InstanceOf<GameRunning>());
}
[Test]
public void TestGameOver() {
    BreakoutBus.GetBus().RegisterEvent(
    new GameEvent{
    EventType = GameEventType.GameStateEvent,
    Message = "CHANGE_STATE",
    StringArg1 = "GAME_OVER"
    }
    );
    BreakoutBus.GetBus().ProcessEventsSequentially();
    Assert.That(stateMachine.ActiveState, Is.InstanceOf<GameOver>());
}
[Test]
public void TestEventMainMenu() {
    BreakoutBus.GetBus().RegisterEvent(
    new GameEvent{
    EventType = GameEventType.GameStateEvent,
    Message = "CHANGE_STATE",
    StringArg1 = "MAIN_MENU"
    }
    );
    BreakoutBus.GetBus().ProcessEventsSequentially();
    Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>());
}
}
}