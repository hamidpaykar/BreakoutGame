using DIKUArcade.Events;

namespace Breakout {
    public static class BreakoutBus {
        /// <summary>
        /// Stores the eventBus, which can be called to control the state of the game
        /// </summary>
        private static GameEventBus eventBus;

        
        public static GameEventBus GetBus() {
            return eventBus ??= new GameEventBus();
        }
    }
}
