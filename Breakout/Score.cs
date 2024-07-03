using DIKUArcade.Events;

namespace Breakout.Scores
{
    /// <summary>
    /// Stores the score obtained by playing across levels. 
    /// </summary>
    public static class Score
    {
        private static int score = 0; // Variable to hold the current score
        private static bool isWin = false; // Variable to hold the win status

        // Property to get the current score
        public static int Points
        {
            get { return score; }
        }

        // Property to get the current win status
        public static bool IsWin
        {
            get { return isWin; }
        }

        // Method to get the current score
        public static int GetScore()
        {
            return score;
        }

        // Method to increase the score by a specified value
        public static void increaseScore(int value)
        {
            score += value;
        }

        // Method to reset the score to zero
        public static void reset()
        {
            score = 0;
        }

        // Method to reset the win status to false
        public static void resetWin()
        {
            isWin = false;
        }

        // Method to set the win status to true
        public static void setWin()
        {
            isWin = true;
        }
    }
}
