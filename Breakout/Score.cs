using DIKUArcade.Events;

namespace Breakout.Scores {
    /// <summary>
    /// Stores the score uptained by playing across levels. 
    /// </summary>
    public static class Score {
        private static int score = 0;
        private static bool isWin = false;
        
        public static int Points{
            get {return score;}
        }

        public static bool IsWin{
            get {return isWin;}
        }
        public static int GetScore() {
            return score;
        }
        public static void increaseScore(int value){
            score += value;
        }
        public static void reset(){
            score = 0;
        }
        public static void resetWin(){
            isWin = false;
        }
        public static void setWin(){
            isWin = true;
        }

    }
}
