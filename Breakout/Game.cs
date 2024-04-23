
using System.Collections.Generic;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Graphics;
using System;

namespace Breakout
{
    

public class Game : DIKUGame {

    public Game(WindowArgs windowArgs) : base(windowArgs) {
        window.SetKeyEventHandler(KeyHandler);
    }

    private void KeyHandler(KeyboardAction action, KeyboardKey key) {
        Console.WriteLine("");
    }

    public override void Render() { 
                Console.WriteLine("");        
    }

    public override void Update() {
        Console.WriteLine("");
     }
}
}