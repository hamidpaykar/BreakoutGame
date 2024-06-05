using System;


namespace Breakout.States
{
    public enum GameStateType
    {
        GameRunning,
        GamePaused,
        MainMenu,
        GameOver,
        LevelSelector
    }
    public class StateTransformer
    {
        public static GameStateType TransformStringToState(string state)
        {
            GameStateType transformedState = GameStateType.GameRunning;
            switch (state)
            {
                case "GAME_RUNNING":
                    break;
                case "GAME_PAUSED":
                    transformedState = GameStateType.GamePaused;
                    break;
                case "MAIN_MENU":
                    transformedState = GameStateType.MainMenu;
                    break;
                case "GAME_OVER":
                    transformedState = GameStateType.GameOver;
                    break;
                case "LEVEL_SELECTOR":
                    transformedState = GameStateType.LevelSelector;
                    break;
                default:
                    throw new ArgumentException(String.Format("{0} is not at valid string code", state), "state");
                    break;
            }
            return transformedState;
        }

        public static string TransformStateToString(GameStateType state)
        {
            string transformedState = "GAME_RUNNING";
            switch (state)
            {
                case GameStateType.GameRunning:
                    break;
                case GameStateType.GamePaused:
                    transformedState = "GAME_PAUSED";
                    break;
                case GameStateType.MainMenu:
                    transformedState = "MAIN_MENU";
                    break;
                case GameStateType.GameOver:
                    transformedState = "GAME_OVER";
                    break;
                case GameStateType.LevelSelector:
                    transformedState = "LEVEL_SELECTOR";
                    break;
                default:
                    throw new ArgumentException(String.Format("{0} is not at valid state", state), "state");
                    break;

            }
            return transformedState;
        }
    }
}
