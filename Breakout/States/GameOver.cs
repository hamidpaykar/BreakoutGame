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
    public class GameOver : IGameState
    {
        private static GameOver instance = null;
        private Entity backGroundImage = new Entity(new DynamicShape(new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 1.0f)), new Image(Path.Combine("Assets", "Images", "shipit_titlescreen.png")));
        private string[] menuText = {"Back to menu", "Quit" };
        private Text GameOverStatus = new Text("You lost", new Vec2F(0.40f, 0.45f), new Vec2F(0.3f, 0.3f));
        private Text[] menuButtons = {new Text("Back to menu", new Vec2F(0.44f, 0.25f), new Vec2F(0.2f, 0.2f)), new Text("Quit", new Vec2F(0.44f, 0.35f), new Vec2F(0.2f, 0.2f))};
        private int activeMenuButton = 0;
        private int maxMenuButtons;

        public GameOver(){
            maxMenuButtons = menuButtons.Length - 1;
            
        }

        public static GameOver GetInstance()
        {
            if (GameOver.instance == null)
            {
                GameOver.instance = new GameOver();
                GameOver.instance.ResetState();
            }
            Score.reset();
            return GameOver.instance;
        }
        public void ResetState() 
        {

        }

        /// <summary>
        /// Update all variables that are being used by this GameState.
        /// </summary>
        public void UpdateState()
        {
           /*  for (int i = 0; i < menuButtons.Length; i++)
            {
                if (i == activeMenuButton)
                {
                    menuButtons[i].SetText(menuText[i] + " ⬅︎ ");
                }
                else
                {
                    menuButtons[i].SetText(menuText[i]);
                }
            } */
        }


        /// <summary>
        /// Render all entities in this GameState
        /// </summary>
        public void RenderState()
        {
            
            backGroundImage.RenderEntity();
            if(Score.IsWin){
                GameOverStatus.SetText("You won!");
                Score.resetWin();
            }
            GameOverStatus.SetColor(new Vec3I(255, 255, 0));
            GameOverStatus.RenderText();
            for (int i = 0; i < menuButtons.Length; i++)
            {
                if (i == activeMenuButton)
                {
                    menuButtons[i].SetColor(new Vec3I(255, 255, 0));
                }
                else
                {
                    menuButtons[i].SetColor(new Vec3I(255, 255, 255));
                }
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
            RenderState();
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
                        StringArg1 = "MAIN_MENU"
                    }
                );
            }
            else if (activeMenuButton == 1)
            {
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