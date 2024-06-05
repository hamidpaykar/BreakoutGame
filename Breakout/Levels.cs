using DIKUArcade.Events;
using System.IO;
using Breakout.LoadLevel;
using System.Collections.Generic;
using System;

namespace Breakout.Levels {
    public static class LevelHolder {
        private static List<Level> levels = new List<Level>();
        private static List<int> levelNumbers = new List<int>();
        private static int totalLevels;
        public static int TotalLevels{
            get{return totalLevels;}
        }
        public static List<int> LevelNumbers{
            get{return levelNumbers;}
        }
        public static List<Level> Levels{
            get{return levels;}
        }
        public static void loadLevels(){
            DirectoryInfo d = new DirectoryInfo(Path.Combine("Assets", "Levels")); //Assuming Test is your Folder
            int currentNumber = 1;
            FileInfo[] Files = d.GetFiles("*.txt"); //Getting Text files
            string str = "";

            foreach(FileInfo file in Files )
            {
            levels.Add(new Level(totalLevels.ToString(), file.Name));
            levelNumbers.Add(totalLevels);
            totalLevels += 1;
            }
            Console.WriteLine(levels.Count);
        }
        public static void removeLevel(int levelNumber){
            levelNumbers.Remove(levelNumber);
            totalLevels -= 1;
        }

    }
}
