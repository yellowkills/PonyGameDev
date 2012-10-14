using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WhenRobotsAttack
{
    public static class ReadFromFile
    {

        public static List<Tuple<string, string>> read(string pathToFile) 
        {

            List<Tuple<string, string>> list = new List<Tuple<string, string>>();
        
            System.IO.StreamReader file = new System.IO.StreamReader(GameManager.path + pathToFile);

            string line = "";
            string[] tok = null;
        
            while((line = file.ReadLine()) != null) 
            {
                tok = line.Split(',');
                list.Add(new Tuple<string, string>(tok[0], tok[1]));
            }
        
            return list;

        }

        public static List<int[,]> readMap(string pathToFile)
        {
            System.IO.StreamReader file = new System.IO.StreamReader(GameManager.path + pathToFile);

            int[,] tileNums;
            List<string[]> list = new List<string[]>();
            string[] tok = null;
            string line = null;
            int index;
            List<int[,]> layers = new List<int[,]>();


            List<string> linesFromFile = new List<string>();
            List<string> linesToLayer = new List<string>();
            while ((line = file.ReadLine()) != null) 
                linesFromFile.Add(line);

                        
            while(linesFromFile.Count != 0)
            {
                index = linesFromFile.IndexOf("---");
                for (int i = 0; i < index; i++)
                    linesToLayer.Add(linesFromFile[i]);
                linesFromFile.RemoveRange(0, index+1);


                for(int i = 0; i < linesToLayer.Count; i++)
                {
                    tok = linesToLayer[i].Split('|');
                    list.Add(tok);
                } 

                tileNums = new int[list[0].Length,list.Count];

                for (int y = 0; y < list.Count; y++)
                {
                    for (int x = 0; x < list[y].Length; x++)
                    {
                        tileNums[x,y] = Convert.ToInt32(list[y][x]);
                    }
                }

                layers.Add(tileNums);

                list.Clear();
                linesToLayer.Clear();
            }


            return layers;
            
        }
    }
}
