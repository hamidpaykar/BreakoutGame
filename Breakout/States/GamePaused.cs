using DIKUArcade.State;
using DIKUArcade.Input;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System;
using System.IO;
using DIKUArcade.State;
using DIKUArcade.Events;

namespace Breakout.States
{
    /// <summary>
    /// Represents the Paused state. The state should be active when the player has paused the game. 
    /// Transitions either back to the current game that is running or back to main menu.  
    /// </summary>
    public class GamePaused : IGameState
    {
        // Static instance of GamePaused
        private static GamePaused instance = null;

        // Background image for the paused state
        private Entity backGroundImage = new Entity(new DynamicShape(new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 1.0f)), new Image(Path.Combine("Assets", "Images", "shipit_titlescreen.png")));

        // Menu text for the paused state
        private string[] menuText = { "Resume", "Main menu" };

        // Menu buttons for the paused state
        private Text[] menuButtons = { new Text("Resume", new Vec2F(0.44f, 0.25f), new Vec2F(0.2f, 0.2f)), new Text("Main Menu", new Vec2F(0.44f, 0.35f), new Vec2F(0.2f, 0.2f)) };

        // Active menu button index
        private int activeMenuButton = 0;

        // Maximum number of menu buttons
        private int maxMenuButtons;

        // Private constructor for GamePaused
        private GamePaused()
        {
            maxMenuButtons = menuButtons.Length - 1;
        }

        /// <summary>
        /// When called a new instance of GamePaused is created, if one is not already.
        /// </summary>
        public static GamePaused GetInstance()
        {
            // If instance is null, create a new instance
            if (GamePaused.instance == null)
            {
                GamePaused.instance = new GamePaused();
                GamePaused.instance.ResetState();
            }
            // Return the instance
            return GamePaused.instance;
        }

        // Reset the state
        public void ResetState()
        {
            // No implementation
        }

        /// <summary>
        /// Update all variables that are being used by this GameState.
        /// </summary>
        public void UpdateState()
        {
            // Update menu buttons
            for (int i = 0; i < menuButtons.Length; i++)
            {
                // If the button is active, add an arrow to the text
                if (i == activeMenuButton)
                {
                    menuButtons[i].SetText(menuText[i] + " ⬅︎ ");
                }
                else
                {
                    menuButtons[i].SetText(menuText[i]);
                }
            }
        }


        /// <summary>
        /// Render all entities in this GameState
        /// </summary>
        public void RenderState()
        {
            // Render the background image
            backGroundImage.RenderEntity();

            // Render each menu button
            foreach (var button in menuButtons)
            {
                // Set the text color to white
                button.SetColor(new Vec3I(255, 255, 255));
                button.RenderText();
            }
        }

        /// <summary>
        /// Each state can react to key events, delegated from the host StateMachine.
        /// </summary>
        /// <param name="action">Enumeration representing key press/release.</param>
        /// <param name="key">Enumeration representing the keyboard key.</param>
        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key)
        {
            // If a key is pressed
            if (action == KeyboardAction.KeyPress)
            {
                // Handle different key presses
                switch (key)
                {
                    case KeyboardKey.Up:
                        // If the active button is not the first, move up
                        if (activeMenuButton > 0)
                        {
                            activeMenuButton--;
                        }
                        // If the active button is the first, move to the last button
                        else
                        {
                            activeMenuButton = maxMenuButtons;
                        }
                        break;
                    case KeyboardKey.Down:
                        // If the active button is not the last, move down
                        if (activeMenuButton < maxMenuButtons)
                        {
                            activeMenuButton++;
                        }
                        // If the active button is the last, move to the first button
                        else
                        {
                            activeMenuButton = 0;
                        }
                        break;
                    case KeyboardKey.Enter:
                        // Execute the active menu action
                        ExecuteMenuAction();
                        break;
                }
            }
            // Update the state
            UpdateState();
        }


        private void ExecuteMenuAction()
        {
            // If the active button is the first, change state to GAME_RUNNING
            if (activeMenuButton == 0)
            {
                BreakoutBus.GetBus().RegisterEvent(
                    new GameEvent
                    {
                        EventType = GameEventType.GameStateEvent,
                        Message = "CHANGE_STATE",
                        StringArg1 = "GAME_RUNNING",
                        StringArg2 = "pause"
                    }
                );
            }
            // If the active button is the second, change state to MAIN_MENU
            else if (activeMenuButton == 1)
            {
                BreakoutBus.GetBus().RegisterEvent(
                    new GameEvent
                    {
                        EventType = GameEventType.GameStateEvent,
                        Message = "CHANGE_STATE",
                        StringArg1 = "MAIN_MENU"
                    }
                );
            }
        }
    }
}
