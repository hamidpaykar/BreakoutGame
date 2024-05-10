using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using System;
using System.Collections.Generic; 
using Breakout.Blocks;

namespace Breakout.LoadLevel;
public class Level
{
    private string name;
    private int time;
    private string powerUp;
    private string unbreakable;
    private string hardened;
    private string fileName;
    public EntityContainer<Block> blocks;

    private Dictionary<char, string> legend;

    public string Name{
        get {return name;}
    }

    public string FileName{
        get {return fileName;}
    }

    public Level(string name, string fileName)
    {
        //this.name = name;
        this.fileName = fileName;
        this.blocks = new EntityContainer<Block>(25*12);
        legend = getLegend(fileName, "Legend");
        getMeta(fileName, "Meta");
        getFormation(fileName, "Map");

    }

    private Dictionary<char, string> getLegend(string fileName, string tag){
        String line;
        string content = "";
        Dictionary<char, string> legend =  
                       new Dictionary<char, string>(); 
        try
        {
            //Pass the file path and file name to the StreamReader constructor
            StreamReader sr = new StreamReader(Path.Combine("Assets", "Levels", fileName));
            //Read the first line of text
            line = sr.ReadLine();
            //Continue to read until you reach end of file
            bool flag = false;
            while (line != null)
            {
                if(line.Contains(tag)){
                    flag = !flag;
                }
                if(flag && line.Contains(")")){
                    legend.Add(line[0], line.Substring(3));
                }
                //Read the next line
                line = sr.ReadLine();
            }
            //close the file
            sr.Close();
        }
        catch(Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
            this.fileName = "";
        }
        finally
        {
            Console.WriteLine("Executing finally block.");
        }
        return legend;
    }

    private void getMeta(string fileName, string tag){
        String line;
        
        string content = "";
        Dictionary<char, string> legend =  
                       new Dictionary<char, string>(); 
        try
        {
            //Pass the file path and file name to the StreamReader constructor
            StreamReader sr = new StreamReader(Path.Combine("Assets", "Levels", fileName));
            //Read the first line of text
            line = sr.ReadLine();
            //Continue to read until you reach end of file
            bool flag = false;
            while (line != null)
            {
                if(line.Contains(tag)){
                    flag = !flag;
                }
                if(flag && !(line.Contains("Meta"))){
                    if(line.Contains("Name")){
                        this.name = line.Substring(6);
                    }
                    else if(line.Contains("Time")){
                        this.time = int.Parse(line.Substring(6));
                    }
                    else if(line.Contains("PowerUp")){
                        this.powerUp = line.Substring(9);
                    }
                    else if(line.Contains("Unbreakable")){
                        this.unbreakable = line.Substring(13);
                    }
                    else if (line.Contains("Hardened")){
                        this.hardened = line.Substring(10);
                    }
                }
                
                //write the line to console window
                //Read the next line
                line = sr.ReadLine();
            }
            //close the file
            sr.Close();
        }
        catch(Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
        finally
        {
            Console.WriteLine("Executing finally block.");
        }
    }

    private void getFormation(string fileName, string tag){
        String line;
        int currentValue = 0;
        string content = "";
        int row = 0;
        float totalWidth = 12.0F;
        float totalHeight = 25.0F;
        //float width = 0.08F;
        //float height = 0.04F;
        float width = 1.0F / totalWidth;
        float height = 1.0F /totalHeight;
        Dictionary<char, string> legend =  
                       new Dictionary<char, string>(); 
        try
        {
            //Pass the file path and file name to the StreamReader constructor
            StreamReader sr = new StreamReader(Path.Combine("Assets", "Levels", fileName));
            //Read the first line of text
            line = sr.ReadLine();
            //Continue to read until you reach end of file
            bool flag = false;
            while (line != null)
            {
                if(line.Contains(tag)){
                    flag = !flag;
                }
                if(flag && !(line.Contains("Map"))){
                    for (int i = 0; i < line.Length; i++)
                    {
                        if (line[i]!='-'){
                            float y = ((totalHeight-row)*height)-height;
                            float x = ((totalWidth-i)*width) - width;
                            this.blocks.AddEntity(new Block(new DynamicShape(new Vec2F(x, y), new Vec2F(width,height)),
                new Image(Path.Combine("Assets", "Images", this.legend[line[i]])), currentValue, 3));
                currentValue++;
                        }
                    }
                    row++;
                }
                
                //write the line to console window
                //Read the next line
                line = sr.ReadLine();
            }
            //close the file
            sr.Close();
        }
        catch(Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
        finally
        {
            Console.WriteLine("Executing finally block.");
        }
    }

}
