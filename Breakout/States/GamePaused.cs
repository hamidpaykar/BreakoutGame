using DIKUArcade.State;
using DIKUArcade.Input;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.States {
    public class GamePaused : IGameState {
    private static GamePaused instance = null;
    private Entity backGroundImage;
    private Text[] menuButtons;
    private int activeMenuButton;
    private int maxMenuButtons;
    public static GamePaused GetInstance() {
        if (GamePaused.instance == null) {
        GamePaused.instance = new GamePaused();
        GamePaused.instance.ResetState();
        }
        return GamePaused.instance;
    }
        public void ResetState(){

        }

        /// <summary>
        /// Update all variables that are being used by this GameState.
        /// </summary>
        public void UpdateState(){

        }
        
        /// <summary>
        /// Render all entities in this GameState
        /// </summary>
        public void RenderState(){

        }
        
        /// <summary>
        /// Each state can react to key events, delegated from the host StateMachine.
        /// </summary>
        /// <param name="action">Enumeration representing key press/release.</param>
        /// <param name="key">Enumeration representing the keyboard key.</param>
        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key){


        }
    }
}

