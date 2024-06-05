using System;
using DIKUArcade.GUI;
namespace Breakout
{
    /// <summary>
    /// Creates a window, where the game can be played
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // var windowArgs = new WindowArgs() { Title = "Galaga v0.1" };
            // var game = new Game(windowArgs);
            // game.Run();

            var windowArgs = new WindowArgs()
        {
            Title = "Breakout"
        };

        var game = new Game(windowArgs);
        game.Run();
        }
    }
}
