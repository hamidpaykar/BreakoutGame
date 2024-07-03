using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using System;
using System.Collections.Generic;
using Breakout.Blocks;

namespace Breakout.LoadLevel
{

    /// <summary>
    /// Class for representing a level, originally described in a txt file. 
    /// Information about the level to render it is stored in Level class. 
    /// </summary>
    public class Level
    {
        private string name = ""; // Name of the level
        private string hazard = ""; // Hazard type in the level
        private int time = 0; // Time limit for the level
        private string powerUp = ""; // Power-up type in the level
        private string unbreakable = ""; // Unbreakable block type in the level
        private string hardened = ""; // Hardened block type in the level
        private string fileName = ""; // File name of the level definition
        public EntityContainer<Block> blocks; // Container for blocks in the level

        private Dictionary<char, string> legend; // Legend for block types in the level

        public string Name
        {
            get { return name; }
        }

        public int Time
        {
            get { return time; }
        }

        public string FileName
        {
            get { return fileName; }
        }

        /// <summary>
        /// Instantiating a new level, with a name using a filename to fetch the level txt file.
        /// </summary>
        public Level(string name, string fileName)
        {
            this.fileName = fileName;
            this.blocks = new EntityContainer<Block>(25 * 12); // Initialize the block container with a size of 300
            legend = getLegend(fileName, "Legend"); // Get the legend section from the file
            getMeta(fileName, "Meta"); // Get the metadata section from the file
            getFormation(fileName, "Map"); // Get the map formation from the file
        }

        // Method to extract the legend from the level file
        private Dictionary<char, string> getLegend(string fileName, string tag)
        {
            string line;
            Dictionary<char, string> legend = new Dictionary<char, string>();
            try
            {
                StreamReader sr = new StreamReader(Path.Combine("Assets", "Levels", fileName));
                line = sr.ReadLine();
                bool flag = false;
                while (line != null)
                {
                    if (line.Contains(tag))
                    {
                        flag = !flag;
                    }
                    if (flag && line.Contains(")"))
                    {
                        legend.Add(line[0], line.Substring(3));
                    }
                    line = sr.ReadLine();
                }
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                this.fileName = "";
            }
            return legend;
        }

        // Method to extract metadata from the level file
        private void getMeta(string fileName, string tag)
        {
            string line;
            try
            {
                StreamReader sr = new StreamReader(Path.Combine("Assets", "Levels", fileName));
                line = sr.ReadLine();
                bool flag = false;
                while (line != null)
                {
                    if (line.Contains(tag))
                    {
                        flag = !flag;
                    }
                    if (flag && !(line.Contains("Meta")))
                    {
                        if (line.Contains("Name"))
                        {
                            this.name = line.Substring(6);
                        }
                        else if (line.Contains("Time"))
                        {
                            this.time = int.Parse(line.Substring(6));
                        }
                        else if (line.Contains("PowerUp"))
                        {
                            this.powerUp = line.Substring(9);
                        }
                        else if (line.Contains("Unbreakable"))
                        {
                            this.unbreakable = line.Substring(13);
                        }
                        else if (line.Contains("Hardened"))
                        {
                            this.hardened = line.Substring(10);
                        }
                        else if (line.Contains("Hazard"))
                        {
                            this.hazard = line.Substring(7);
                        }
                    }
                    line = sr.ReadLine();
                }
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        // Method to extract the block formation from the level file
        private void getFormation(string fileName, string tag)
        {
            string line;
            int row = 0;
            float totalWidth = 12.0F;
            float totalHeight = 25.0F;
            float width = 1.0F / totalWidth;
            float height = 1.0F / totalHeight;
            try
            {
                StreamReader sr = new StreamReader(Path.Combine("Assets", "Levels", fileName));
                line = sr.ReadLine();
                bool flag = false;
                while (line != null)
                {
                    if (line.Contains(tag))
                    {
                        flag = !flag;
                    }
                    if (flag && !(line.Contains("Map")))
                    {
                        for (int i = 0; i < line.Length; i++)
                        {
                            if (line[i] != '-')
                            {
                                float y = ((totalHeight - row) * height) - height;
                                float x = ((totalWidth - i) * width) - width;
                                Block newBlock = new Block(new DynamicShape(new Vec2F(x, y), new Vec2F(width, height)), this.legend[line[i]], 3, 3);

                                if (this.unbreakable.Length > 0 && line[i] == this.unbreakable[0])
                                {
                                    newBlock = new UnbreakableBlock(new DynamicShape(new Vec2F(x, y), new Vec2F(width, height)), this.legend[line[i]], 3, 3);
                                }
                                if (this.hardened.Length > 0 && line[i] == this.hardened[0])
                                {
                                    newBlock = new HardenedBlock(new DynamicShape(new Vec2F(x, y), new Vec2F(width, height)), this.legend[line[i]], 3, 3);
                                }
                                if (this.powerUp.Length > 0 && line[i] == this.powerUp[0])
                                {
                                    newBlock = new PowerUpBlock(new DynamicShape(new Vec2F(x, y), new Vec2F(width, height)), this.legend[line[i]], 3, 3);
                                }
                                if (this.hazard.Length > 0 && line[i] == this.hazard[0])
                                {
                                    newBlock = new HazardBlock(new DynamicShape(new Vec2F(x, y), new Vec2F(width, height)), this.legend[line[i]], 3, 3);
                                }
                                this.blocks.AddEntity(newBlock);
                            }
                        }
                        row++;
                    }
                    line = sr.ReadLine();
                }
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        // Method to decrease the level time and check if the time is over
        public bool looseTime(int time)
        {
            bool isTimeOver = false;
            this.time -= time;
            if (this.time < 1)
            {
                isTimeOver = true;
            }
            return isTimeOver;
        }
    }
}
