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
    public class MainMenu : IGameState
    {
        private static MainMenu instance = null;
        private Entity backGroundImage = new Entity(new DynamicShape(new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 1.0f)), new Image(Path.Combine("Assets", "Images", "shipit_titlescreen.png")));
        private string[] menuText = { "New game", "Quit" };

        private Text[] menuButtons = {new Text("New game", new Vec2F(0.44f, 0.25f), new Vec2F(0.2f, 0.2f)), new Text("Quit", new Vec2F(0.44f, 0.35f), new Vec2F(0.2f, 0.2f))};
        private int activeMenuButton = 0;
        private int maxMenuButtons;

        public MainMenu(){
            maxMenuButtons = menuButtons.Length - 1;
        }

        public static MainMenu GetInstance()
        {
            if (MainMenu.instance == null)
            {
                MainMenu.instance = new MainMenu();
                MainMenu.instance.ResetState();
            }
            return MainMenu.instance;
        }
        public void ResetState() 
        {

        }

        /// <summary>
        /// Update all variables that are being used by this GameState.
        /// </summary>
        public void UpdateState()
        {
            for (int i = 0; i < menuButtons.Length; i++)
            {
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
            backGroundImage.RenderEntity();

            foreach (var button in menuButtons)
            {
                button.SetColor(new Vec3I(255, 255, 255)); // Set text color to white
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
            if (action == KeyboardAction.KeyPress)
            {
                switch (key)
                {
                    case KeyboardKey.Up:
                        if (activeMenuButton > 0)
                        {
                            activeMenuButton--;
                        }
                        else
                        {
                            activeMenuButton = maxMenuButtons;
                        }
                        break;
                    case KeyboardKey.Down:
                        if (activeMenuButton < maxMenuButtons)
                        {
                            activeMenuButton++;
                        }
                        else
                        {
                            activeMenuButton = 0;
                        }
                        break;
                    case KeyboardKey.Enter:
                        ExecuteMenuAction();
                        break;
                }
            }
            UpdateState();
        }


        private void ExecuteMenuAction()
        {
            if (activeMenuButton == 0)
            {
                BreakoutBus.GetBus().RegisterEvent(
                    new GameEvent
                    {
                        EventType = GameEventType.GameStateEvent,
                        Message = "CHANGE_STATE",
                        StringArg1 = "GAME_RUNNING"
                    }
                );
            }
            else if (activeMenuButton == 1)
            {
                Console.WriteLine("Exit, not implemented yet");
            }
        }
    }
}