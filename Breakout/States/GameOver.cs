using DIKUArcade.State;
using DIKUArcade.Input;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System;
using System.IO;
using DIKUArcade.State;
using DIKUArcade.Events;
using Breakout.Scores;

namespace Breakout.States
{
    /// <summary>
    /// Represents the state Game over, when the players actiones have lead to either a win or a loss
    /// Renders the text You Won! or You lost, based on the current score. 
    /// State can transition back main menu, restarting the game or quit the game entirely.
    /// </summary>
    public class GameOver : IGameState
    {
        private static GameOver instance = null;
        // Background image entity
        private Entity backGroundImage = new Entity(new DynamicShape(new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 1.0f)),
            new Image(Path.Combine("Assets", "Images", "shipit_titlescreen.png")));

        // Menu text array
        private string[] menuText = { "Back to menu", "Quit" };

        // Game over status text
        private Text GameOverStatus = new Text("You lost", new Vec2F(0.40f, 0.45f), new Vec2F(0.3f, 0.3f));

        // Menu button text array
        private Text[] menuButtons = {new Text("Back to menu", new Vec2F(0.44f, 0.25f), new Vec2F(0.2f, 0.2f)),
            new Text("Quit", new Vec2F(0.44f, 0.35f), new Vec2F(0.2f, 0.2f))};

        // Active menu button index
        private int activeMenuButton = 0;

        // Max menu buttons
        private int maxMenuButtons;

        private GameOver()
        {
            // Initialize max menu buttons
            maxMenuButtons = menuButtons.Length - 1;
        }

        /// <summary>
        /// When called a new instance of GameOver is created, if one is not already.
        /// Score is reset, as the game is over 
        /// </summary>
        public static GameOver GetInstance()
        {
            // If instance is null, create a new one and reset the state
            if (GameOver.instance == null)
            {
                GameOver.instance = new GameOver();
                GameOver.instance.ResetState();
            }
            // Reset score
            Score.reset();
            return GameOver.instance;
        }

        public void ResetState()
        {
            // No implementation
        }

        /// <summary>
        /// Update all variables that are being used by this GameState.
        /// </summary>
        public void UpdateState()
        {
            // No implementation
        }

        /// <summary>
        /// Render all entities in this GameState
        /// </summary>
        public void RenderState()
        {
            // Render background image
            backGroundImage.RenderEntity();

            // Check if it's a win
            if (Score.IsWin)
            {
                // Set game over status text to "You won!"
                GameOverStatus.SetText("You won!");
                // Reset win score
                Score.resetWin();
            }

            // Set game over status text color
            GameOverStatus.SetColor(new Vec3I(255, 255, 0));
            // Render game over status text
            GameOverStatus.RenderText();

            // Render menu buttons
            for (int i = 0; i < menuButtons.Length; i++)
            {
                // Check if it's the active menu button
                if (i == activeMenuButton)
                {
                    // Set active menu button color
                    menuButtons[i].SetColor(new Vec3I(255, 255, 0));
                }
                else
                {
                    // Set inactive menu button color
                    menuButtons[i].SetColor(new Vec3I(255, 255, 255));
                }
                // Render menu button text
                menuButtons[i].RenderText();
            }
        }

        /// <summary>
        /// Each state can react to key events, delegated from the host StateMachine.
        /// </summary>
        /// <param name="action">Enumeration representing key press/release.</param>
        /// <param name="key">Enumeration representing the keyboard key.</param>
        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key)
        {
            // Check if it's a key press
            if (action == KeyboardAction.KeyPress)
            {
                // Handle up and down arrow keys
                switch (key)
                {
                    case KeyboardKey.Up:
                        // Decrement active menu button index
                        if (activeMenuButton > 0)
                        {
                            activeMenuButton--;
                        }
                        else
                        {
                            // Wrap around to max menu buttons
                            activeMenuButton = maxMenuButtons;
                        }
                        break;
                    case KeyboardKey.Down:
                        // Increment active menu button index
                        if (activeMenuButton < maxMenuButtons)
                        {
                            activeMenuButton++;
                        }
                        else
                        {
                            // Wrap around to 0
                            activeMenuButton = 0;
                        }
                        break;
                    case KeyboardKey.Enter:
                        // Execute menu action
                        ExecuteMenuAction();
                        break;
                }
            }
            // Update and render state
            UpdateState();
            RenderState();
        }

        private void ExecuteMenuAction()
        {
            // Check active menu button index
            if (activeMenuButton == 0)
            {
                // Register event to change state to main menu
                BreakoutBus.GetBus().RegisterEvent(
                    new GameEvent
                    {
                        EventType = GameEventType.GameStateEvent,
                        Message = "CHANGE_STATE",
                        StringArg1 = "MAIN_MENU"
                    }
                );
            }
            else if (activeMenuButton == 1)
            {
                // Register event to close window
                BreakoutBus.GetBus().RegisterEvent(
                    new GameEvent
                    {
                        EventType = GameEventType.GameStateEvent,
                        Message = "CLOSE_WINDOW",
                    }
                );
            }
        }
    }
}
